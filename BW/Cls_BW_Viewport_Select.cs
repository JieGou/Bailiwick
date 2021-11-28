//Now the following code does 2 things we want to do very often: finding out which entities in ModelSpace 
//are visible in which Viewport; and determining a given entity in ModelSpace is visible in which Viewports:



using System.Collections.Generic;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

//[assembly: CommandClass(typeof(EntitiesInsideViewport.MyCommands))]


namespace MyFirstProject.BW
{
    public class Cls_BW_Viewport_Select
    {

        /// <summary>
        /// iBWave Scale blocks and text per viewport scale
        /// </summary>
        /// <param name="fltr"></param>
        /// <param name="sclBlks"></param>
        /// <param name="sclTxt"></param>
        public void SelectByViewportFdEx(SelectionFilter fltr, int sclBlks, int sclTxt)
        {
            Document dwg = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor ed = dwg.Editor;

            //Save current layout name
            string curLayout = LayoutManager.Current.CurrentLayout;

            try
            {
                //Get viewport information on current layout
                Cls_BW_Viewport_Info[] vports = GetViewportInfoOnCurrentLayout();

                if (vports == null) return;

                // this fails in a form???
                // Switch to modelspace
                using (dwg.LockDocument())
                {
                    LayoutManager.Current.CurrentLayout = "Model";
                }

                //Select entities in modelspace that are visible in the viewport that is locked
                foreach (Cls_BW_Viewport_Info vInfo in vports)
                {
                    ObjectId[] ents = SelectEntitisInModelSpaceByViewport(
                        dwg,
                        vInfo.BoundaryInModelSpace,
                        fltr
                        );
                    
                    using (dwg.LockDocument())
                    using (Transaction acTrans = dwg.TransactionManager.StartTransaction())
                    {
                        BlockTable bt = acTrans.GetObject(
                                          dwg.Database.BlockTableId,
                                          OpenMode.ForRead
                                      ) as BlockTable;

                        BlockTableRecord btrModelSpace = acTrans.GetObject(
                                                bt[BlockTableRecord.ModelSpace],
                                                OpenMode.ForRead
                                            ) as BlockTableRecord;

                        DrawOrderTable dot = acTrans.GetObject(
                                                btrModelSpace.DrawOrderTableId,
                                                OpenMode.ForWrite
                                            ) as DrawOrderTable;

                        Viewport vp = acTrans.GetObject(vInfo.ViewportId, OpenMode.ForRead) as Viewport;

                        ObjectIdCollection objToMove = new ObjectIdCollection();

                        foreach (ObjectId i in ents)
                        {                  
                            Entity acEnt = acTrans.GetObject(i, OpenMode.ForWrite) as Entity;

                            if (acEnt is MText)
                            {
                                MText mt = (MText)acEnt;
                                mt.BackgroundFill = true;
                                mt.UseBackgroundColor = true;
                                //TODO: use rbg for darker purple
                                mt.ColorIndex = 215;
                                mt.Width = 0;                               
                                mt.Transparency = new Autodesk.AutoCAD.Colors.Transparency(210); // = 17

                                mt.TextHeight = (1 / vp.CustomScale) / sclTxt;                        

                                mt.Draw();
                                objToMove.Add(i);
                            }

                            if (acEnt is BlockReference)
                            {                                
                                BlockReference br = (BlockReference)acEnt;
                                double s = (1 / vp.CustomScale) / sclBlks;

                                br.ScaleFactors = new Scale3d (
                                    s,
                                    s,
                                    s
                                    );                   

                                br.Draw();
                                objToMove.Add(i);
                            }                            
           
                        }              
                 
                        dot.MoveToTop(objToMove);
                        acTrans.Commit();
                    }                    

                    ed.WriteMessage("\n{0} entit{1} found via Viewport \"{2}\"",
                        ents.Length,
                        ents.Length > 1 ? "ies" : "y",
                        vInfo.ViewportId.ToString());
                }

                Autodesk.AutoCAD.Internal.Utils.PostCommandPrompt();
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("\nCommand \"VpSelect\" failed:");
                ed.WriteMessage("\n{0}\n{1}", ex.Message, ex.StackTrace);
            }
            finally
            {
                //Restore back to original layout
                using (dwg.LockDocument())
                {
                    if (LayoutManager.Current.CurrentLayout != curLayout)
                    {
                        LayoutManager.Current.CurrentLayout = curLayout;
                    }
                    Autodesk.AutoCAD.Internal.Utils.PostCommandPrompt();
                }
            }

        }







        //Use viewport boundary as selecting window/polygon
        //to find entities in modelspace visible in each viewport 
        public void SelectByViewport(SelectionFilter fltr)
        {
            Document dwg = Application.DocumentManager.MdiActiveDocument;
            Editor ed = dwg.Editor;

            //Save current layout name
            string curLayout = LayoutManager.Current.CurrentLayout;

            try
            {
                //Get viewport information on current layout
                Cls_BW_Viewport_Info[] vports = GetViewportInfoOnCurrentLayout();

                if (vports == null) return;

                // this fails in a form???
                // Switch to modelspace
                using (dwg.LockDocument())
                {
                    LayoutManager.Current.CurrentLayout = "Model";
                }

                //Select entities in modelspace that are visible in the viewport that is locked
                foreach (Cls_BW_Viewport_Info vInfo in vports)
                {
                    ObjectId[] ents = SelectEntitisInModelSpaceByViewport(dwg, vInfo.BoundaryInModelSpace,
                        fltr);

                    ed.WriteMessage("\n{0} entit{1} found via Viewport \"{2}\"",
                        ents.Length,
                        ents.Length > 1 ? "ies" : "y",
                        vInfo.ViewportId.ToString());
                }
                Autodesk.AutoCAD.Internal.Utils.PostCommandPrompt();
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("\nCommand \"VpSelect\" failed:");
                ed.WriteMessage("\n{0}\n{1}", ex.Message, ex.StackTrace);
            }
            finally
            {
                //Restore back to original layout
                using (dwg.LockDocument())
                {
                    if (LayoutManager.Current.CurrentLayout != curLayout)
                    {
                        LayoutManager.Current.CurrentLayout = curLayout;
                    }
                    Autodesk.AutoCAD.Internal.Utils.PostCommandPrompt();
                }
            }

        }

        /// <summary>
        /// Determine a given entity in modelspace is visible in
        /// locked viewports in current layout
        /// </summary>  
        public void FindContainingViewport()
        {
            Document dwg = Application.DocumentManager.MdiActiveDocument;

            Editor ed = dwg.Editor;

            //Switch to modelspace
            string curLayout = LayoutManager.Current.CurrentLayout;

            try
            {
                //Get viewport information on current layout
                Cls_BW_Viewport_Info[] vports = GetViewportInfoOnCurrentLayout();

                if (vports == null) return;

                //Pick an entity in modelspace
                LayoutManager.Current.CurrentLayout = "Model";

                ObjectId entId = PickEntity(ed);

                if (entId.IsNull)
                {
                    ed.WriteMessage("\n*Cancel*");
                }
                else
                {
                    //Find viewport in which the selected entity is visible
                    List<ObjectId> lst = new List<ObjectId>();

                    foreach (Cls_BW_Viewport_Info vpInfo in vports)
                    {
                        if (IsEntityInsideViewportBoundary(dwg, entId, vpInfo.BoundaryInModelSpace))
                        {
                            lst.Add(vpInfo.ViewportId);
                            ed.WriteMessage(
                                "\nSelected entity is visible in viewport \"{0}\"",
                               vpInfo.ViewportId.ToString());
                        }
                    }

                    if (lst.Count == 0)
                        ed.WriteMessage(
                            "\nSelected entity is not visible in all viewports");
                    else
                        ed.WriteMessage(
                            "\nSelected entity is visible in {0} viewport{1}.",
                            lst.Count, lst.Count > 1 ? "s" : "");
                }
                Autodesk.AutoCAD.Internal.Utils.PostCommandPrompt();
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("\nCommand \"GetViewports\" failed:");
                ed.WriteMessage("\n{0}\n{1}", ex.Message, ex.StackTrace);
            }
            finally
            {
                //Restore back to original layout
                if (LayoutManager.Current.CurrentLayout != curLayout)
                {
                    LayoutManager.Current.CurrentLayout = curLayout;
                }

                Autodesk.AutoCAD.Internal.Utils.PostCommandPrompt();
            }

        }



        private Cls_BW_Viewport_Info[] GetViewportInfoOnCurrentLayout()
        {
            string layoutName = LayoutManager.Current.CurrentLayout;

            if (layoutName.ToUpper() == "MODEL")
            {
                Application.ShowAlertDialog("Please set a layout as active layout!");
                return null;
            }
            else
            {
                Document dwg = Application.DocumentManager.MdiActiveDocument;

                Cls_BW_Viewport_Info[] vports = Cls_BW_CadHelper.SelectLockedViewportInfoOnLayout(dwg, layoutName);

                if (vports.Length == 0)
                {
                    Application.ShowAlertDialog(
                        "No locked viewport found on layout \"" + 
                        layoutName + 
                        "\"."
                        );
                    return null;
                }
                else
                {
                    return vports;
                }
            }
        }



        private ObjectId[] SelectEntitisInModelSpaceByViewport(
            Document dwg,
            Point3dCollection boundaryInModelSpace,
            SelectionFilter fltr 
            )
        {
            ObjectId[] ids = null;

            using (dwg.LockDocument())
            using (Transaction tran = dwg.TransactionManager.StartTransaction())
            {
                //Zoom to the extents of the viewport boundary in modelspace
                //before calling Editor.SelectXxxxx()

                ZoomToWindow(boundaryInModelSpace);

                Cls_BW_TP_Common.acSSPrompt = dwg.Editor.SelectCrossingPolygon(boundaryInModelSpace,
                    fltr);

                if (Cls_BW_TP_Common.acSSPrompt.Status == PromptStatus.OK)
                {
                    ids = Cls_BW_TP_Common.acSSPrompt.Value.GetObjectIds();          
                }

                //Restored to previous view (view before zoomming)
                tran.Abort();
            }
            return ids; // object id's of all entities in the viewport's boundry
        }

      

        private void ZoomToWindow(Point3dCollection boundaryInModelSpace)
        {
            Extents3d ext = GetViewportBoundaryExtentsInModelSpace(boundaryInModelSpace);

            double[] p1 = new double[] { ext.MinPoint.X, ext.MinPoint.Y, 0.00 };
            double[] p2 = new double[] { ext.MaxPoint.X, ext.MaxPoint.Y, 0.00 };
                
            dynamic acadApp = Application.AcadApplication;
            acadApp.ZoomWindow(p1, p2);
        }



        private Extents3d GetViewportBoundaryExtentsInModelSpace(
            Point3dCollection points
            )
        {
            Extents3d ext = new Extents3d();

            foreach (Point3d p in points)
            {
                ext.AddPoint(p);
            }
            return ext;
        }



        private ObjectId PickEntity(Editor ed)
        {
            PromptEntityOptions opt =
                                new PromptEntityOptions("\nSelect an entity:");

            PromptEntityResult res = ed.GetEntity(opt);

            if (res.Status == PromptStatus.OK)
            {
                return res.ObjectId;
            }
            else
            {
                return ObjectId.Null;
            }
        }



        private bool IsEntityInsideViewportBoundary(
            Document dwg,
            ObjectId entId,
            Point3dCollection boundaryInModelSpace
            )
        {
            bool inside = false;

            using (Transaction tran = dwg.TransactionManager.StartTransaction())
            {
                //Zoom to the extents of the viewport boundary in modelspace
                //before calling Editor.SelectXxxxx()

                ZoomToWindow(boundaryInModelSpace);

                PromptSelectionResult res = dwg.Editor.SelectCrossingPolygon(boundaryInModelSpace);

                if (res.Status == PromptStatus.OK)
                {
                    foreach (ObjectId id in res.Value.GetObjectIds())
                    {
                        if (id == entId)
                        {
                            inside = true;
                            break;
                        }
                    }
                }

                //Restored to previous view (before zoomming)
                tran.Abort();
            }
            return inside;

        }

    }

}
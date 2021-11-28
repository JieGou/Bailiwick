using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW
{
    public class Cls_BW_TP_Layouts
    {

        const string annoScalesDict = "ACDB_ANNOTATIONSCALES";

        #region Layouts Tab

        public static void ChangeTitleBlocks(string BlkName)
        {
            Dictionary<string, ObjectId> dict = new Dictionary<string, ObjectId>();

            Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database acCurDb = acDoc.Database;

            //get the selected layouts name, id's
            using (acDoc.LockDocument())
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                DBDictionary lays =
                    acTrans.GetObject(acCurDb.LayoutDictionaryId,
                        OpenMode.ForRead) as DBDictionary;

                foreach (DBDictionaryEntry item in lays)
                {
                    Layout l = (Layout)acTrans.GetObject((ObjectId)item.Value, OpenMode.ForRead);

                    if (l.LayoutName != "Model" & l.TabSelected == true)
                    {
                        dict.Add(item.Key.ToString(), item.Value);
                    }
                }
                acTrans.Commit();
            }

            // erase objects 
            foreach (KeyValuePair<string, ObjectId> entry in dict)
            {
                using (acDoc.LockDocument())
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    Layout l = (Layout)acTrans.GetObject((ObjectId)entry.Value, OpenMode.ForWrite);

                    BlockTable acBlkTbl;
                    acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord acBlkTblRec;
                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.PaperSpace],
                                                    OpenMode.ForWrite) as BlockTableRecord;

                    LayoutManager.Current.CurrentLayout = l.LayoutName;

                    acDocEd.ZoomExtents();

                    // viewports and stamp
                    EraseByCrossWind(acTrans, acDocEd, new Point3d(31.1853, 0.3877, 0), new Point3d(32.7489, 21.7734, 0));

                    // title block
                    EraseByCrossWind(acTrans, acDocEd, new Point3d(35.1525, 22.675, 0), new Point3d(34.4786, 22.4766, 0));

                    acDocEd.Regen();

                    acTrans.Commit();
                }

            }

            List<ObjectId> vps = new List<ObjectId>();

            //  insert new title block
            foreach (KeyValuePair<string, ObjectId> entry in dict)
            {
                using (acDoc.LockDocument())
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    Layout l = (Layout)acTrans.GetObject((ObjectId)entry.Value, OpenMode.ForWrite);

                    BlockTable acBlkTbl;
                    acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord acBlkTblRec;
                    acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.PaperSpace],
                                                    OpenMode.ForWrite) as BlockTableRecord;

                    LayoutManager.Current.CurrentLayout = l.LayoutName;

                    acDocEd.ZoomExtents();

                    vps = InsertingTitleBlock(BlkName, acTrans, acCurDb, acBlkTbl, acDocEd, acBlkTblRec);

                    //acDocEd.Regen();

                    acTrans.Commit();
                }

                // turn on the viewports
                using (acDoc.LockDocument())
                using (Transaction tr = acCurDb.TransactionManager.StartTransaction())
                {
                    foreach (ObjectId id in vps)
                    {
                        Viewport v = tr.GetObject(id, OpenMode.ForWrite) as Viewport;
                        v.On = true;
                    }
                    tr.Commit();
                }
            }
            //BW.Cls_BW_Main.BW_UpdateTitleBlockDateField();
            //BW.Cls_BW_Main.BW_ZoomAllLayouts();
        }

        private static void EraseByCrossWind(Transaction transaction, Editor acDocEd, Point3d p1, Point3d p2)
        {
            PromptSelectionResult acSSPrompt = acDocEd.SelectCrossingWindow
                (p1, p2);

            // If the prompt status is OK, objects were selected
            if (acSSPrompt.Status == PromptStatus.OK)
            {
                SelectionSet acSSet = acSSPrompt.Value;

                ObjectId[] ids = { };

                try
                {
                    ids = acSSet.GetObjectIds();
                }
                catch (Autodesk.AutoCAD.Runtime.Exception ex)
                {
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(ex.ToString());
                    return;
                }

                EraseEnt(transaction, acSSet, ids);
            }
            else
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
            }
        }
        private static void EraseEnt(Transaction transaction, SelectionSet acSSet, ObjectId[] ids)
        {
            foreach (ObjectId id1 in ids)
            {
                Entity ent = (Entity)transaction.GetObject(id1, OpenMode.ForWrite);
                ent.Erase();
            }
        }


        private static List<ObjectId> InsertingTitleBlock(
            string BlkName,
            Transaction tr,
            Autodesk.AutoCAD.DatabaseServices.Database db,
            BlockTable bt,
            Editor ed,
            BlockTableRecord ms
            )
        {
            if (!bt.Has(BlkName))
            {
                ed.WriteMessage("\nBlock \"" + BlkName + "\" not found.");
                return new List<ObjectId>();
            }

            var btrBlkOne = (BlockTableRecord)tr.GetObject(bt[BlkName], OpenMode.ForRead);

            Point3d nPt = new Point3d(0, 0, 0);

            var br = new BlockReference(nPt, btrBlkOne.ObjectId);
            br.TransformBy(ed.CurrentUserCoordinateSystem);
            ms.AppendEntity(br);
            tr.AddNewlyCreatedDBObject(br, true);

            if (btrBlkOne.Annotative == AnnotativeStates.True)
            {
                var ocm = db.ObjectContextManager;
                var occ = ocm.GetContextCollection(annoScalesDict);
                br.AddContext(occ.CurrentContext);
            }
            else
            {
                br.ScaleFactors = new Scale3d(br.UnitFactor);
            }

            List<ObjectId> vps = new List<ObjectId>();

            using (DBObjectCollection dbObjCol = new DBObjectCollection())
            {
                br.Explode(dbObjCol);

                foreach (DBObject dbObj in dbObjCol)
                {
                    Entity acEnt = dbObj as Entity;

                    ms.AppendEntity(acEnt);
                    tr.AddNewlyCreatedDBObject(dbObj, true);

                    acEnt = tr.GetObject(dbObj.ObjectId, OpenMode.ForWrite) as Entity;

                    if (acEnt is Viewport)
                    {
                        Viewport vp = (Viewport)acEnt;

                        if (vp.BlockName.Contains("Paper_Space"))
                        {
                            try
                            {
                                vps.Add(dbObj.ObjectId);
                                //vp.On = true;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    }
                }
            }

            // ed.Regen();
            return vps;
        }


        #endregion

    }
}

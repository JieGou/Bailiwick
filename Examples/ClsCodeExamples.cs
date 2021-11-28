using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.Customization;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

namespace MyFirstProject.Examples
{

    // Hosting WPF content inside an AutoCAD palette
    //http://www.through-the-interface.typepad.com/through_the_interface/2009/08/hosting-wpf-content-inside-an-autocad-palette.html




    public static class ClsCodeExamples
    {


        public class MyCommands
        {  
            public void MyCommand() // This method can have any name
            {
                InsertBlock(Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database, "C:/88NutPlan.dwg", new Point3d(0, 0, 0));
            }

            public static void InsertBlock(Autodesk.AutoCAD.DatabaseServices.Database db, string FileName, Point3d InsertionPoint, double Rotation = 0)
            {
                ObjectId blkid = ObjectId.Null;
                using (Autodesk.AutoCAD.DatabaseServices.Database bdb = new Autodesk.AutoCAD.DatabaseServices.Database(false, true))
                {
                    bdb.ReadDwgFile(FileName, System.IO.FileShare.Read, true, "");
                    blkid = db.Insert(System.IO.Path.GetFileNameWithoutExtension(FileName), bdb, true);

                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);

                        BlockTableRecord btr = default(BlockTableRecord);
                        btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);


                        using (btr)
                        {

                            BlockReference bref = new BlockReference(InsertionPoint, blkid);

                            Matrix3d mat = Matrix3d.Identity;

                            bref.TransformBy(mat);

                            bref.Layer = "0";

                            bref.Rotation = Rotation;

                            btr.AppendEntity(bref);

                            tr.AddNewlyCreatedDBObject(bref, true);

                            if (bref.IsDynamicBlock)
                            {

                                DynamicBlockReferencePropertyCollection dynBrefColl = bref.DynamicBlockReferencePropertyCollection;

                                foreach (DynamicBlockReferenceProperty dynBrefProps in dynBrefColl)
                                {
                                    if (dynBrefProps.PropertyName.ToUpper() == "VISIBILITY1")
                                    {

                                        dynBrefProps.Value = "M36";
                                    }
                                }
                            }

                        }
                        tr.Commit();
                    }
                }
            }
        }


        //Here are the commands to show or hide centerlines for circles.
        [CommandMethod("gw_ex_ShowCircleCenterline3")]
        public static void ShowCircleCenterline3_Method()
        {
            // drawing
            CenterlineCircle_DrawableOverrule3.AddOverrule(
                RXClass.GetClass(typeof(Circle)),
                CenterlineCircle_DrawableOverrule3.Instance,
                true
                );

            // exploding
            CenterlineCircle_DrawableOverrule3.AddOverrule(
              RXClass.GetClass(typeof(Circle)),
              CenterlineCircle_DrawableOverrule3.CircleTransformOverrule.theOverrule,
              true
              );
        }

        [CommandMethod("gw_ex_HideCircleCenterline3")]
        public static void HideCircleCenterline3_Method()
        {           
            // drawing
            CenterlineCircle_DrawableOverrule3.RemoveOverrule(
                RXClass.GetClass(typeof(Circle)),
                CenterlineCircle_DrawableOverrule3.Instance
                );

            // exploding
            CenterlineCircle_DrawableOverrule3.RemoveOverrule(
                RXClass.GetClass(typeof(Circle)),
                CenterlineCircle_DrawableOverrule3.CircleTransformOverrule.theOverrule
                );

        }


   

        public static void AddLightweightPolyline()
        {
            // Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database acCurDb = acDoc.Database;

            // Start a transaction
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Open the Block table for read
                BlockTable acBlkTbl;
                acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
                                                OpenMode.ForRead) as BlockTable;

                // Open the Block table record Model space for write
                BlockTableRecord acBlkTblRec;
                acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                OpenMode.ForWrite) as BlockTableRecord;

                // Create a polyline with two segments (3 points)
                using (Polyline acPoly = new Polyline())
                {
                    acPoly.AddVertexAt(0, new Point2d(2, 4), 0, 0, 0);
                    acPoly.AddVertexAt(1, new Point2d(4, 2), 0, 0, 0);
                    acPoly.AddVertexAt(2, new Point2d(6, 4), 0, 0, 0);

                    // Add the new object to the block table record and the transaction
                    acBlkTblRec.AppendEntity(acPoly);
                    acTrans.AddNewlyCreatedDBObject(acPoly, true);
                }

                // Save the new object to the database
                acTrans.Commit();
            }
        }


        [CommandMethod("gw_ex_InsertFile")]
        static public void Gw_Ex_InsertFile(string FileName)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            BlockReference br;
            ObjectId id;

            string dwgFileToImport = @"K:\Engineering\3M\2019\wap icon 2019.dwg";
            string blockName = "testwap";

            using (doc.LockDocument())
            using (Transaction acTrans = db.TransactionManager.StartTransaction())
            {
                using (Autodesk.AutoCAD.DatabaseServices.Database tempDb = new Autodesk.AutoCAD.DatabaseServices.Database(false, true))
                {
                    BlockTable bt = (BlockTable) acTrans.GetObject(db.BlockTableId, OpenMode.ForWrite, false);

                    if (!bt.Has(blockName))
                    {
                        if (System.IO.File.Exists(dwgFileToImport))
                        {
                            // 'read in the file into the temp database
                            tempDb.ReadDwgFile(dwgFileToImport, System.IO.FileShare.Read, true, null);
                            //  'insert the tempdb into the current drawing db, id is the new block id
                            id = db.Insert(blockName, tempDb, true);
                        }
                    }
                    else
                    {
                        Application.ShowAlertDialog("Block already exists in drawing!");
                    }
                }
                acTrans.Commit();
            }


        }

       //Private Shared Function InsertFile(ByVal FileName as String, ByVal dwgdb As Database, ByVal tr As Transaction) As BlockReference

       // Dim br As BlockReference
       // Dim id As ObjectId

        //'use a temporary database 
        //Using TempDB As New Database(False, True)

        //    'Get block table
        //    Dim bt As BlockTable = tr.GetObject(dwgdb.BlockTableId, OpenMode.ForWrite, False)

    //        'Create unique block name
    //        Dim BlockName As String = FileName.Replace("\", "").Replace(":", "").Replace(".", "")

    //        'check if block already exists
    //        If Not bt.Has(BlockName) Then
    //            'check if file exists
    //            If IO.File.Exists(FileName) Then
    //                'read in the file into the temp database
    //                TempDB.ReadDwgFile(FileName, IO.FileShare.Read, True, Nothing)
    //                'insert the tempdb into the current drawing db, id is the new block id
    //                id = dwgdb.Insert(BlockName, TempDB, True)
    //            Else
    //                'Throw exception for missing file 
    //                Throw New System.Exception(String.Format("File {0} is not found for library item {1}", FileName, item.PartNo))
    //            End If

    //        Else
    //            id = bt.Item(BlockName)
    //        End If

    //        'create a new block reference
    //        br = New BlockReference(New Point3d(0, 0, 0), id)
    //    End Using

    //    Return br


    //End Function




        [CommandMethod("gw_ex_AddScale")]
        static public void Gw_Ex_addScale()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            try
            {
                ObjectContextManager ocm = db.ObjectContextManager;

                if (ocm != null)
                {
                    // Now get the Annotation Scaling context collection
                    // (named ACDB_ANNOTATIONSCALES_COLLECTION)

                    ObjectContextCollection occ = ocm.GetContextCollection("ACDB_ANNOTATIONSCALES");

                    if (occ != null)
                    {
                        // Create a brand new scale context
                        AnnotationScale asc = new AnnotationScale();

                        asc.Name = "MyScale 1:28";
                        asc.PaperUnits = 1;
                        asc.DrawingUnits = 28;

                        // Add it to the drawing's context collection
                        occ.AddContext(asc);
                    }
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(ex.ToString());
            }
        }

        [CommandMethod("gw_ex_AttachScale")]
        static public void Gw_Ex_attachScale()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;
            ObjectContextManager ocm = db.ObjectContextManager;

            ObjectContextCollection occ = ocm.GetContextCollection("ACDB_ANNOTATIONSCALES");
            Transaction tr = doc.TransactionManager.StartTransaction();

            using (tr)
            {
                PromptEntityOptions opts = new PromptEntityOptions("\nSelect entity: ");

                opts.SetRejectMessage(
                    "\nEntity must support annotation scaling."
                );

                opts.AddAllowedClass(typeof(DBText), false);
                opts.AddAllowedClass(typeof(MText), false);
                opts.AddAllowedClass(typeof(Dimension), false);
                opts.AddAllowedClass(typeof(Leader), false);
                opts.AddAllowedClass(typeof(Table), false);
                opts.AddAllowedClass(typeof(Hatch), false);

                PromptEntityResult per = ed.GetEntity(opts);

                if (per.ObjectId != ObjectId.Null)
                {
                    DBObject obj = tr.GetObject(per.ObjectId, OpenMode.ForRead);

                    if (obj != null)
                    {
                        obj.UpgradeOpen();
                        obj.Annotative = AnnotativeStates.True;
                        obj.AddContext(occ.GetContext("1:1"));
                        obj.AddContext(occ.GetContext("1:2"));
                        obj.AddContext(occ.GetContext("1:10"));
                        ObjectContext oc = occ.GetContext("MyScale 1:28");

                        if (oc != null)
                        {
                            obj.AddContext(oc);
                        }
                    }
                }
                tr.Commit();
            }
        }




        [CommandMethod("gw_ex_SelectObjectsByCrossingWindow")]
        public static void Gw_Ex_SelectObjectsByCrossingWindow()
        {
            // Get the current document editor
            Editor acDocEd = Application.DocumentManager.MdiActiveDocument.Editor;

            // Create a crossing window from (2,2,0) to (10,8,0)
            PromptSelectionResult acSSPrompt;
            acSSPrompt = acDocEd.SelectCrossingWindow(new Point3d(2, 2, 0),
                                                      new Point3d(10, 8, 0));

            // If the prompt status is OK, objects were selected
            if (acSSPrompt.Status == PromptStatus.OK)
            {
                SelectionSet acSSet = acSSPrompt.Value;

                Application.ShowAlertDialog("Number of objects selected: " +
                                            acSSet.Count.ToString());
            }
            else
            {
                Application.ShowAlertDialog("Number of objects selected: 0");
            }
        }



        [CommandMethod("gw_ex_myLongFunction")]
        public static void Gw_Ex_myLongFunction()
        {
            //show the progress of the function
            ProgressMeter pm = new ProgressMeter();
            pm.Start("Long process");
            pm.SetLimit(100);

            try
            {
                //start a long process
                for (int i = 0; i < 100; i++)
                {
                    //did user press ESCAPE?
                    if (HostApplicationServices.Current.UserBreak())
                        throw new Autodesk.AutoCAD.Runtime.Exception(
                            ErrorStatus.UserBreak, "ESCAPE pressed");

                    //update progress bar
                    pm.MeterProgress();

                    //delay 10 miliseconds
                    System.Threading.Thread.Sleep(10);
                }
            }

            catch (System.Exception ex)
            {
                //some error
                Application.DocumentManager.MdiActiveDocument.
                    Editor.WriteMessage(ex.Message);
            }
            finally
            {
                pm.Stop();
            }
        }



        [CommandMethod("gw_ex_InsertingABlock")]
        public static void Gw_Ex_InsertingABlock()
        {
            // Get the current database and start a transaction
            Autodesk.AutoCAD.DatabaseServices.Database acCurDb;
            acCurDb = Application.DocumentManager.MdiActiveDocument.Database;

            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Open the Block table for read
                BlockTable acBlkTbl;
                acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                ObjectId blkRecId = ObjectId.Null;

                if (!acBlkTbl.Has("CircleBlock"))
                {
                    using (BlockTableRecord acBlkTblRec = new BlockTableRecord())
                    {
                        acBlkTblRec.Name = "CircleBlock";

                        // Set the insertion point for the block
                        acBlkTblRec.Origin = new Point3d(0, 0, 0);

                        // Add a circle to the block
                        using (Circle acCirc = new Circle())
                        {
                            acCirc.Center = new Point3d(0, 0, 0);
                            acCirc.Radius = 2;

                            acBlkTblRec.AppendEntity(acCirc);

                            acBlkTbl.UpgradeOpen();
                            acBlkTbl.Add(acBlkTblRec);
                            acTrans.AddNewlyCreatedDBObject(acBlkTblRec, true);
                        }

                        blkRecId = acBlkTblRec.Id;
                    }
                }
                else
                {
                    blkRecId = acBlkTbl["CircleBlock"];
                }

                // Insert the block into the current space
                if (blkRecId != ObjectId.Null)
                {
                    using (BlockReference acBlkRef = new BlockReference(new Point3d(0, 0, 0), blkRecId))
                    {
                        BlockTableRecord acCurSpaceBlkTblRec;
                        acCurSpaceBlkTblRec = acTrans.GetObject(acCurDb.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;

                        acCurSpaceBlkTblRec.AppendEntity(acBlkRef);
                        acTrans.AddNewlyCreatedDBObject(acBlkRef, true);
                    }
                }

                // Save the new object to the database
                acTrans.Commit();

                // Dispose of the transaction
            }
        }

        // jig multiple entities
        // based on Kean Walmsley example (jig mtext)
        [CommandMethod("gw_ex_MoveCopyJig")]
        public static void Gw_Ex_MoveCopyJig()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var db = HostApplicationServices.WorkingDatabase;
            Editor ed = doc.Editor;
            try
            {
                using (Transaction tr = doc.TransactionManager.StartTransaction())
                {
                    SelectionSet sset = ed.GetSelection().Value;
                    Point3d basept = ed.GetPoint("\nBase point: ").Value;
                    IEnumerable<Entity> objs = sset.GetObjectIds().Select(x => (Entity)tr.GetObject(x, OpenMode.ForRead)).ToArray();
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
                    foreach (Entity e in objs)
                    {
                        Entity c = (Entity)e.Clone();// you may want to use DeepCloneObjects instead
                        btr.AppendEntity(c);
                        tr.AddNewlyCreatedDBObject(c, true);

                    }
                    ed.SetImpliedSelection(sset);

                    PromptSelectionResult psr = ed.SelectImplied();

                    // drag our jig

                    if (psr.Status == PromptStatus.OK)
                    {
                        // jig whole selection

                        PromptPointResult ppr =
                          ed.Drag(
                            psr.Value,
                            "\nSpecify displacement point: ",
                            delegate (Point3d pt, ref Matrix3d mat)
                            {
                                // If no change has been made, say so
                                if (basept == pt)
                                    return SamplerStatus.NoChange;
                                else
                                {
                                    // Otherwise we return the displacement
                                    // matrix for the current position
                                    mat = Matrix3d.Displacement(basept.GetVectorTo(pt)
                                      );
                                }
                                return SamplerStatus.OK;
                            }
                          );

                        // Assuming it works, transform our MText
                        // appropriately

                        if (ppr.Status == PromptStatus.OK)
                        {
                            // Get the final translation matrix

                            Matrix3d mat = Matrix3d.Displacement(basept.GetVectorTo(ppr.Value)
                              );

                            foreach (Entity en in objs)
                            {
                                en.UpgradeOpen();
                                en.TransformBy(mat);
                            }
                        }
                        // Finally we commit our transaction
                        tr.Commit();
                    }
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(ex.ToString());
            }
        }



        [CommandMethod("gw_ex_ResetLayer")]
        public static void Gw_Ex_ResetLayerSettings()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
           Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            if (db.Visretain == false)
            {
                ed.WriteMessage("\nVISRETAIN is 0 Changes to layer setting will be reset by a reload of Xref.");
                return;
            }

            PromptResult pr = ed.GetString("\nEnter variable name: ");
            if (pr.Status != PromptStatus.OK)
                return;
            String layerName = pr.StringResult;

            XrefGraph xg = db.GetHostDwgXrefGraph(false);

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                LayerTable layerTable = tr.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                LinetypeTable lineTypeTable = tr.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;

                if (layerTable.Has(layerName))
                {
                    LayerTableRecord layerTableRec = tr.GetObject(layerTable[layerName], OpenMode.ForWrite) as LayerTableRecord;
                    if (layerTableRec.IsDependent || layerName.Contains("|"))
                    {
                        // Split it up, so we know the xref name
                        // and the root layer name

                        int sepIdx = layerName.IndexOf("|");
                        string xrefName = layerName.Substring(0, sepIdx);
                        string rootName = layerName.Substring(sepIdx + 1);

                        // If the xref is the same as the last loaded,
                        // this saves us some effort
                        // we get the node for our xref,
                        // so we can get its filename

                        XrefGraphNode xgn = xg.GetXrefNode(xrefName);

                        if (xgn != null)
                        {
                            // Create an xrefed database, loading our
                            // drawing into it

                            using (Autodesk.AutoCAD.DatabaseServices.Database xdb = new Autodesk.AutoCAD.DatabaseServices.Database(false, true))
                            {
                                xdb.ReadDwgFile(xgn.Database.Filename, System.IO.FileShare.Read, true, null);

                                // Start a transaction in our loaded database
                                // to get at the layer name

                                Transaction tr2 = xdb.TransactionManager.StartTransaction();
                                using (tr2)
                                {
                                    // Open the layer table
                                    LayerTable lt2 = tr2.GetObject(xdb.LayerTableId, OpenMode.ForRead) as LayerTable;

                                    // Add our layer (which we get via its
                                    // unmangled name) to the list to clone

                                    if (lt2.Has(rootName))
                                    {
                                        LayerTableRecord ltr2 = tr2.GetObject(lt2[rootName], OpenMode.ForRead) as LayerTableRecord;

                                        // Reset the layer color
                                        layerTableRec.Color = ltr2.Color;

                                        LinetypeTableRecord lineTypeRec2 = tr2.GetObject(ltr2.LinetypeObjectId, OpenMode.ForRead) as LinetypeTableRecord;
                                        String lineTypeRecName = String.Format("{0}|{1}", xrefName, lineTypeRec2.Name);

                                        // Reset the line type
                                        if (lineTypeTable.Has(lineTypeRecName))
                                        {
                                            layerTableRec.LinetypeObjectId = lineTypeTable[lineTypeRecName];
                                        }
                                    }

                                    tr2.Commit();
                                }
                            }
                        }
                    }
                    else
                    {
                        ed.WriteMessage("\nNot a dependent layer. Layers from an Xref drawing are dependent");
                    }
                }

                tr.Commit();
            }
        }



        [CommandMethod("gw_ex_SetLineType", CommandFlags.UsePickSet)]
        public static void Gw_Ex_SetLineType()
        {
            var doc = Application.DocumentManager.MdiActiveDocument;

            if (doc == null)
                return;

            var ed = doc.Editor;

            // Get the pickfirst selection set or ask the user to
            // select some entities
            var psr = ed.GetSelection();

            if (psr.Status != PromptStatus.OK || psr.Value.Count == 0)
                return;

            using (var tr = doc.TransactionManager.StartTransaction())
            {
                // Get the IDs of the selected objects
                var ids = psr.Value.GetObjectIds();

                // Loop through in read-only mode, checking whether the
                // selected entities have the same linetype
                // (if so, it'll be set in ltId, otherwise different will
                // be true)

                var ltId = ObjectId.Null;

                bool different = false;

                foreach (ObjectId id in ids)
                {
                    // Get the entity for read
                    var ent = (Entity)tr.GetObject(id, OpenMode.ForRead);

                    // On the first iteration we store the linetype Id
                    if (ltId == ObjectId.Null)
                        ltId = ent.LinetypeId;
                    else
                    {
                        // On subsequent iterations we check against the
                        // first one and set different to be true if they're
                        // not the same

                        if (ltId != ent.LinetypeId)
                        {
                            different = true;
                            break;
                        }
                    }
                }

                // Now we can display our linetype dialog with the common
                // linetype selected (if they have the same one)
                var ltd = new Autodesk.AutoCAD.Windows.LinetypeDialog();

                if (!different)
                    ltd.Linetype = ltId;

                var dr = ltd.ShowDialog();

                if (dr != System.Windows.Forms.DialogResult.OK)
                    return; // We might also commit before returning

                // Assuming we have a different linetype selected
                // (or the entities in the selected have different
                // linetypes to start with) then we'll loop through
                // to set the new linetype

                if (different || ltId != ltd.Linetype)
                {
                    foreach (ObjectId id in ids)
                    {
                        // This time we need write access
                        var ent = (Entity)tr.GetObject(id, OpenMode.ForWrite);

                        // Set the linetype if it's not the same
                        if (ent.LinetypeId != ltd.Linetype)
                            ent.LinetypeId = ltd.Linetype;
                    }
                }
                tr.Commit();
            }
        }






        /// <summary>
        /// not used?
        /// copy entities from another open drawing?
        /// </summary>
        private static void CopyAms()
        {
            DocumentCollection acDocMgr = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager;
            Document acNewDoc = acDocMgr.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database acDbNewDoc = acNewDoc.Database;

            using (DocumentLock acLckDoc = acNewDoc.LockDocument())
            {
                using (Transaction acTrans = acDbNewDoc.TransactionManager.StartTransaction())
                {
                    BlockTable acBlkTblNewDoc = acTrans.GetObject(acDbNewDoc.BlockTableId, OpenMode.ForRead) as BlockTable;

                    BlockTableRecord acBlkTblRecNewDoc = acTrans.GetObject(acBlkTblNewDoc[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;

                    IdMapping acIdMap = new IdMapping();
                   // AirMagDb.WblockCloneObjects(idCol, acBlkTblRecNewDoc.ObjectId, acIdMap, DuplicateRecordCloning.Ignore, false);

                    acTrans.Commit();
                }
            }
            acDocMgr.MdiActiveDocument = acNewDoc;
        }








        /// <summary>
        /// Example that can add layers to drawing
        /// or change the color of the layer if it exists already
        /// </summary>
        [CommandMethod("gw_ex_SetAllLayersToColor")]
        public static void Gw_Ex_SetAllLayersToColor()
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database acCurDb = acDoc.Database;

            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Open the Layer table for read
                LayerTable acLyrTbl;
                acLyrTbl = acTrans.GetObject(acCurDb.LayerTableId, OpenMode.ForRead) as LayerTable;

                // Define an array of layer names
                string[] sLayerNames = new string[3];
                sLayerNames[0] = "ACIRed";
                sLayerNames[1] = "TrueBlue";
                sLayerNames[2] = "ColorBookYellow";

                // Define an array of colors for the layers
                Color[] acColors = new Color[3];
                acColors[0] = Color.FromColorIndex(ColorMethod.ByAci, 1);
                acColors[1] = Color.FromRgb(23, 54, 232);
                acColors[2] = Color.FromNames("PANTONE Yellow 0131 C", "PANTONE(R) pastel coated");

                int nCnt = 0;
                // Add or change each layer in the drawing
                foreach (string sLayerName in sLayerNames)
                {
                    if (acLyrTbl.Has(sLayerName) == false)
                    {
                        using (LayerTableRecord acLyrTblRec = new LayerTableRecord())
                        {
                            // Assign the layer a name
                            acLyrTblRec.Name = sLayerName;

                            // Upgrade the Layer table for write
                            if (acLyrTbl.IsWriteEnabled == false) acLyrTbl.UpgradeOpen();

                            // Append the new layer to the Layer table and the transaction
                            acLyrTbl.Add(acLyrTblRec);
                            acTrans.AddNewlyCreatedDBObject(acLyrTblRec, true);

                            // Set the color of the layer
                            acLyrTblRec.Color = acColors[nCnt];
                        }
                    }
                    else
                    {
                        // Open the layer if it already exists for write
                        LayerTableRecord acLyrTblRec = acTrans.GetObject(acLyrTbl[sLayerName], OpenMode.ForWrite) as LayerTableRecord;

                        // Set the color of the layer
                        acLyrTblRec.Color = acColors[nCnt];
                    }
                    nCnt = nCnt + 1;
                }

                // Save the changes and dispose of the transaction
                acTrans.Commit();
            }
        }


        /// <summary>
        /// Color a block by selecting the block and the color dialog box
        /// </summary>
        [CommandMethod("gw_ex_ColorBlock")]
        public static void Gw_Ex_ColorBlock()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            // Ask the user to select a block
            PromptEntityOptions peo = new PromptEntityOptions("\nSelect a block:");

            peo.AllowNone = false;
            peo.SetRejectMessage("\nMust select a block.");
            peo.AddAllowedClass(typeof(BlockReference), false);

            PromptEntityResult per = ed.GetEntity(peo);

            if (per.Status != PromptStatus.OK)
                return;

            // Open the entity using a transaction
            Transaction tr = db.TransactionManager.StartTransaction();

            using (tr)
            {
                try
                {
                    Entity ent =
                        (Entity)tr.GetObject(
                        per.ObjectId,
                        OpenMode.ForRead
                      );

                    // Should always be a block reference,
                    // but let's make sure
                    BlockReference br = ent as BlockReference;

                    if (br != null)
                    {
                        // Select the new color
                        Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
                        cd.IncludeByBlockByLayer = true;
                        cd.ShowDialog();

                        // Change the color of the block itself
                        br.UpgradeOpen();
                        br.Color = cd.Color;

                        // Change every entity to be of color ByBlock
                        BlockTableRecord btr =
                          (BlockTableRecord)tr.GetObject(
                            br.BlockTableRecord,
                            OpenMode.ForRead
                          );

                        // Iterate through the BlockTableRecord contents
                        foreach (ObjectId id in btr)
                        {
                            // Open the entity
                            Entity ent2 =
                              (Entity)tr.GetObject(id, OpenMode.ForWrite);

                            // Change each entity's color to ByBlock
                            ent2.Color =
                              Color.FromColorIndex(ColorMethod.ByBlock, 0);
                        }

                    }

                    // Commit if there hasn't been a problem
                    // (even if no objects changed: it's just quicker)
                    tr.Commit();
                }
                catch (Autodesk.AutoCAD.Runtime.Exception e)
                {
                    // Something went wrong
                    ed.WriteMessage(e.ToString());
                }
            }
        }





        /// <summary>
        /// creating-non-rectangular-paperspace-viewports
        /// http://through-the-interface.typepad.com/through_the_interface/2009/12/creating-non-rectangular-paperspace-viewports-in-autocad-using-net.html
        /// </summary>
        [CommandMethod("gw_ex_NRVPS")]
        public static void Gw_Ex_CreateNonRectangularViewports()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            // We're accessing drawing objects, so we need a transaction

            Transaction tr = db.TransactionManager.StartTransaction();

            using (tr)
            {
                // Get the primary paperspace from the block table

                BlockTable bt =
                  (BlockTable)tr.GetObject(
                    db.BlockTableId,
                    OpenMode.ForRead
                  );

                BlockTableRecord ps =
                    (BlockTableRecord)tr.GetObject(
                    bt[BlockTableRecord.PaperSpace],
                    OpenMode.ForWrite
                  );

                // Create a variety of objects for our clip boundaries
                DBObjectCollection objs = new DBObjectCollection();

                // An ellipse...

                Ellipse el =
                  new Ellipse(
                    new Point3d(3.5, 4.7, 0),
                    Vector3d.ZAxis,
                    new Vector3d(1.4, 0.03, 0),
                    0.35, 0, 0
                  );

                objs.Add(el);


                // A circle...
                Circle cir =
                  new Circle(
                    new Point3d(3.4, 1.9, 0),
                    Vector3d.ZAxis,
                    0.9
                  );

                objs.Add(cir);


                // A closed polyline...

                Polyline pl = new Polyline(6);

                pl.AddVertexAt(0, new Point2d(4.92, 5.29), 0, 0, 0);
                pl.AddVertexAt(1, new Point2d(5.16, 6.02), 0, 0, 0);
                pl.AddVertexAt(2, new Point2d(6.12, 6.49), 0, 0, 0);
                pl.AddVertexAt(3, new Point2d(7.29, 6.26), -0.27, 0, 0);
                pl.AddVertexAt(4, new Point2d(8.11, 5.53), -0.47, 0, 0);
                pl.AddVertexAt(5, new Point2d(7.75, 5.41), 0, 0, 0);
                pl.Closed = true;
                objs.Add(pl);

                // A closed spline...
                Point3dCollection pts =
                  new Point3dCollection(
                    new Point3d[] {
              new Point3d (5.5, 2.06, 0),
              new Point3d (5.26, 2.62, 0),
              new Point3d (5.66, 4.16, 0),
              new Point3d (8.56, 4.21, 0),
              new Point3d (7.2, 0.86, 0),
              new Point3d (6.44, 2.85, 0),
              new Point3d (5.62, 1.8, 0),
              new Point3d (5.5, 2.06, 0)
                    }
                  );

                Spline sp = new Spline(pts, 2, 0.5);
                objs.Add(sp);


                // Add each to the paperspace blocktablerecord
                // and create/add an associated viewport object

                foreach (DBObject obj in objs)
                {
                    Entity ent = obj as Entity;
                    if (ent != null)
                    {
                        // Add our boundary to paperspace and the
                        // transaction

                        ObjectId id = ps.AppendEntity(ent);
                        tr.AddNewlyCreatedDBObject(obj, true);

                        // Create our viewport, adding that also
                        Viewport vp = new Viewport();
                        ps.AppendEntity(vp);
                        tr.AddNewlyCreatedDBObject(vp, true);

                        // Set the boundary entity and turn the
                        // viewport/clipping on

                        vp.NonRectClipEntityId = id;
                        vp.NonRectClipOn = true;
                        vp.On = true;
                    }
                }

                tr.Commit();

            }

            // Let's take a look at the results in paperspace

            db.TileMode = false;
        }




        /// <summary>
        /// Iterating through a polyline's vertices
        /// http://www.through-the-interface.typepad.com/through_the_interface/2007/04/iterating_throu.html
        /// </summary>
        [CommandMethod("gw_ex_ListVertices")]
        static public void Gw_Ex_ListVertices()
        {
            Document doc =
              Application.DocumentManager.MdiActiveDocument;

            Editor ed = doc.Editor;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;

            PromptEntityResult per =
              ed.GetEntity("Select a polyline");

            if (per.Status == PromptStatus.OK)
            {
                Transaction tr =
                  db.TransactionManager.StartTransaction();

                using (tr)
                {
                    DBObject obj =
                      tr.GetObject(per.ObjectId, OpenMode.ForRead);

                    // If a "lightweight" (or optimized) polyline

                    Polyline lwp = obj as Polyline;

                    if (lwp != null)
                    {

                        // Use a for loop to get each vertex, one by one

                        int vn = lwp.NumberOfVertices;

                        for (int i = 0; i < vn; i++)
                        {

                            // Could also get the 3D point here

                            Point2d pt = lwp.GetPoint2dAt(i);

                            ed.WriteMessage("\n" + pt.ToString());

                        }
                    }
                    else
                    {
                        // If an old-style, 2D polyline

                        Polyline2d p2d = obj as Polyline2d;

                        if (p2d != null)
                        {
                            // Use foreach to get each contained vertex

                            foreach (ObjectId vId in p2d)
                            {
                                Vertex2d v2d =
                                  (Vertex2d)tr.GetObject(
                                    vId,
                                    OpenMode.ForRead
                                  );

                                ed.WriteMessage(
                                  "\n" + v2d.Position.ToString()

                                );
                            }
                        }
                        else
                        {
                            // If an old-style, 3D polyline
                            Polyline3d p3d = obj as Polyline3d;
                            if (p3d != null)
                            {
                                // Use foreach to get each contained vertex

                                foreach (ObjectId vId in p3d)
                                {
                                    PolylineVertex3d v3d =
                                      (PolylineVertex3d)tr.GetObject(
                                        vId,
                                        OpenMode.ForRead
                                      );

                                    ed.WriteMessage(
                                      "\n" + v3d.Position.ToString()
                                    );

                                }
                            }
                        }
                    }

                    // Committing is cheaper than aborting

                    tr.Commit();

                }

            }

        }



        [CommandMethod("gw_ex_SetLayerColor")]
        public static void Gw_Ex_SetLayerColor()
        {
            // Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database acCurDb = acDoc.Database;

            // Start a transaction
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Open the Layer table for read
                LayerTable acLyrTbl;
                acLyrTbl = acTrans.GetObject(acCurDb.LayerTableId,
                                                OpenMode.ForRead) as LayerTable;

                // Define an array of layer names
                string[] sLayerNames = new string[3];
                sLayerNames[0] = "ACIRed";
                sLayerNames[1] = "TrueBlue";
                sLayerNames[2] = "ColorBookYellow";

                // Define an array of colors for the layers
                Color[] acColors = new Color[3];
                acColors[0] = Color.FromColorIndex(ColorMethod.ByAci, 1);
                acColors[1] = Color.FromRgb(23, 54, 232);
                acColors[2] = Color.FromNames("PANTONE Yellow 0131 C",
                                                "PANTONE(R) pastel coated");

                int nCnt = 0;

                // Add or change each layer in the drawing
                foreach (string sLayerName in sLayerNames)
                {
                    if (acLyrTbl.Has(sLayerName) == false)
                    {
                        using (LayerTableRecord acLyrTblRec = new LayerTableRecord())
                        {
                            // Assign the layer a name
                            acLyrTblRec.Name = sLayerName;

                            // Upgrade the Layer table for write
                            if (acLyrTbl.IsWriteEnabled == false) acLyrTbl.UpgradeOpen();

                            // Append the new layer to the Layer table and the transaction
                            acLyrTbl.Add(acLyrTblRec);
                            acTrans.AddNewlyCreatedDBObject(acLyrTblRec, true);

                            // Set the color of the layer
                            acLyrTblRec.Color = acColors[nCnt];
                        }
                    }
                    else
                    {
                        // Open the layer if it already exists for write
                        LayerTableRecord acLyrTblRec = acTrans.GetObject(acLyrTbl[sLayerName], OpenMode.ForWrite) as LayerTableRecord;

                        // Set the color of the layer
                        acLyrTblRec.Color = acColors[nCnt];
                    }

                    nCnt = nCnt + 1;
                }

                // Save the changes and dispose of the transaction
                acTrans.Commit();
            }
        }





        [CommandMethod("gw_ex_ListLayouts")]
        public static void Gw_Ex_ListLayouts()
        {
            // Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database acCurDb = acDoc.Database;

            // Get the layout dictionary of the current database
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                DBDictionary lays =
                    acTrans.GetObject(acCurDb.LayoutDictionaryId,
                        OpenMode.ForRead) as DBDictionary;

                acDoc.Editor.WriteMessage("\nLayouts:");

                // Step through and list each named layout and Model
                foreach (DBDictionaryEntry item in lays)
                {
                    acDoc.Editor.WriteMessage("\n  " + item.Key);
                }

                // Abort the changes to the database
                acTrans.Abort();
            }
        }


        /// <summary>
        /// Create a new layout with the LayoutManager
        /// </summary>
        [CommandMethod("gw_ex_CreateLayout")]
        public static void Gw_Ex_CreateLayout()
        {
            // Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database acCurDb = acDoc.Database;

            // Get the layout and plot settings of the named pagesetup
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Reference the Layout Manager
                LayoutManager acLayoutMgr = LayoutManager.Current;

                // Create the new layout with default settings
                ObjectId objID = acLayoutMgr.CreateLayout("newLayout");

                // Open the layout
                Layout acLayout = acTrans.GetObject(objID,
                                                    OpenMode.ForRead) as Layout;

                // Set the layout current if it is not already
                if (acLayout.TabSelected == false)
                {
                    acLayoutMgr.CurrentLayout = acLayout.LayoutName;
                }

                // Output some information related to the layout object
                acDoc.Editor.WriteMessage("\nTab Order: " + acLayout.TabOrder +
                                          "\nTab Selected: " + acLayout.TabSelected +
                                          "\nBlock Table Record ID: " +
                                          acLayout.BlockTableRecordId.ToString());

                // Save the changes made
                acTrans.Commit();
            }
        }

        /// <summary>
        /// Import a layout from an external drawing
        /// </summary>        
        [CommandMethod("gw_ex_ImportLayout")]
        public static void Gw_Ex_ImportLayout()
        {
            // Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database acCurDb = acDoc.Database;

            // Specify the layout name and drawing file to work with
            string layoutName = "MAIN AND SECOND FLOOR PLAN";
            string filename = "C:\\AutoCAD\\Sample\\Sheet Sets\\Architectural\\A-01.dwg";

            // Create a new database object and open the drawing into memory
            Autodesk.AutoCAD.DatabaseServices.Database acExDb = new Autodesk.AutoCAD.DatabaseServices.Database(false, true);
            acExDb.ReadDwgFile(filename, FileOpenMode.OpenForReadAndAllShare, true, "");

            // Create a transaction for the external drawing
            using (Transaction acTransEx = acExDb.TransactionManager.StartTransaction())
            {
                // Get the layouts dictionary
                DBDictionary layoutsEx =
                    acTransEx.GetObject(acExDb.LayoutDictionaryId,
                                        OpenMode.ForRead) as DBDictionary;

                // Check to see if the layout exists in the external drawing
                if (layoutsEx.Contains(layoutName) == true)
                {
                    // Get the layout and block objects from the external drawing
                    Layout layEx =
                        layoutsEx.GetAt(layoutName).GetObject(OpenMode.ForRead) as Layout;
                    BlockTableRecord blkBlkRecEx =
                        acTransEx.GetObject(layEx.BlockTableRecordId,
                                            OpenMode.ForRead) as BlockTableRecord;

                    // Get the objects from the block associated with the layout
                    ObjectIdCollection idCol = new ObjectIdCollection();
                    foreach (ObjectId id in blkBlkRecEx)
                    {
                        idCol.Add(id);
                    }

                    // Create a transaction for the current drawing
                    using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                    {
                        // Get the block table and create a new block
                        // then copy the objects between drawings
                        BlockTable blkTbl =
                            acTrans.GetObject(acCurDb.BlockTableId,
                                              OpenMode.ForWrite) as BlockTable;

                        using (BlockTableRecord blkBlkRec = new BlockTableRecord())
                        {
                            int layoutCount = layoutsEx.Count - 1;

                            blkBlkRec.Name = "*Paper_Space" + layoutCount.ToString();
                            blkTbl.Add(blkBlkRec);
                            acTrans.AddNewlyCreatedDBObject(blkBlkRec, true);
                            acExDb.WblockCloneObjects(idCol,
                                                      blkBlkRec.ObjectId,
                                                      new IdMapping(),
                                                      DuplicateRecordCloning.Ignore,
                                                      false);

                            // Create a new layout and then copy properties between drawings
                            DBDictionary layouts =
                                acTrans.GetObject(acCurDb.LayoutDictionaryId,
                                                  OpenMode.ForWrite) as DBDictionary;

                            using (Layout lay = new Layout())
                            {
                                lay.LayoutName = layoutName;
                                lay.AddToLayoutDictionary(acCurDb, blkBlkRec.ObjectId);
                                acTrans.AddNewlyCreatedDBObject(lay, true);
                                lay.CopyFrom(layEx);

                                DBDictionary plSets =
                                    acTrans.GetObject(acCurDb.PlotSettingsDictionaryId,
                                                      OpenMode.ForRead) as DBDictionary;

                                // Check to see if a named page setup was assigned to the layout,
                                // if so then copy the page setup settings
                                if (lay.PlotSettingsName != "")
                                {
                                    // Check to see if the page setup exists
                                    if (plSets.Contains(lay.PlotSettingsName) == false)
                                    {
                                        plSets.UpgradeOpen();

                                        using (PlotSettings plSet = new PlotSettings(lay.ModelType))
                                        {
                                            plSet.PlotSettingsName = lay.PlotSettingsName;
                                            plSet.AddToPlotSettingsDictionary(acCurDb);
                                            acTrans.AddNewlyCreatedDBObject(plSet, true);

                                            DBDictionary plSetsEx =
                                                acTransEx.GetObject(acExDb.PlotSettingsDictionaryId,
                                                                    OpenMode.ForRead) as DBDictionary;

                                            PlotSettings plSetEx =
                                                plSetsEx.GetAt(lay.PlotSettingsName).GetObject(
                                                               OpenMode.ForRead) as PlotSettings;

                                            plSet.CopyFrom(plSetEx);
                                        }
                                    }
                                }
                            }
                        }

                        // Regen the drawing to get the layout tab to display
                        acDoc.Editor.Regen();

                        // Save the changes made
                        acTrans.Commit();
                    }
                }
                else
                {
                    // Display a message if the layout could not be found in the specified drawing
                    acDoc.Editor.WriteMessage("\nLayout '" + layoutName +
                                              "' could not be imported from '" + filename + "'.");
                }

                // Discard the changes made to the external drawing file
                acTransEx.Abort();
            }

            // Close the external drawing file
            acExDb.Dispose();
        }





        [CommandMethod("gw_ex_SelectDynamicBlocks")]
        static public void Gw_Ex_SelectDynamicBlocks()
        {
            var doc = Application.DocumentManager.MdiActiveDocument;
            var ed = doc.Editor;

            var pso =
              new PromptStringOptions(
                "\nName of dynamic block to search for"
              );

            pso.AllowSpaces = true;

            var pr = ed.GetString(pso);

            if (pr.Status != PromptStatus.OK)
                return;

            string blkName = pr.StringResult;

            List<string> blkNames = new List<string>();

            blkNames.Add(blkName);

            var tr = doc.TransactionManager.StartTransaction();

            using (tr)
            {
                var bt =
                  (BlockTable)tr.GetObject(
                    doc.Database.BlockTableId,
                    OpenMode.ForRead
                  );


                // Start by getting access to our block, if it exists                
                if (!bt.Has(blkName))
                {
                    ed.WriteMessage(
                      "\nCannot find block called \"{0}\".", blkName
                    );
                    return;
                }

                // Get the anonymous block names
                var btr =
                  (BlockTableRecord)tr.GetObject(
                    bt[blkName], OpenMode.ForRead
                  );

                if (!btr.IsDynamicBlock)
                {
                    ed.WriteMessage(
                      "\nCannot find a dynamic block called \"{0}\".", blkName
                    );
                    return;
                }


                // Get the anonymous blocks and add them to our list
                var anonBlks = btr.GetAnonymousBlockIds();

                foreach (ObjectId bid in anonBlks)
                {
                    var btr2 =
                      (BlockTableRecord)tr.GetObject(bid, OpenMode.ForRead);

                    blkNames.Add(btr2.Name);
                }
                tr.Commit();
            }



            // Build a conditional filter list so that only
            // entities with the specified properties are
            // selected

            SelectionFilter sf =
              new SelectionFilter(CreateFilterListForBlocks(blkNames));
            PromptSelectionResult psr = ed.SelectAll(sf);

            ed.WriteMessage(
              "\nFound {0} entit{1}.",
              psr.Value.Count,
              (psr.Value.Count == 1 ? "y" : "ies")
            );

        }

        private static TypedValue[] CreateFilterListForBlocks(List<string> blkNames)
        {
            // If we don't have any block names, return null
            if (blkNames.Count == 0)
                return null;

            // If we only have one, return an array of a single value
            if (blkNames.Count == 1)
                return new TypedValue[] {
          new TypedValue(
            (int)DxfCode.BlockName,
           blkNames[0]
          )
         };


            // We have more than one block names to search for...
            // Create a list big enough for our block names plus
            // the containing "or" operators

            List<TypedValue> tvl =
              new List<TypedValue>(blkNames.Count + 2);

            // Add the initial operator

            tvl.Add(
              new TypedValue(
                (int)DxfCode.Operator,
                "<or"
              )
            );


            // Add an entry for each block name, prefixing the
            // anonymous block names with a reverse apostrophe

            foreach (var blkName in blkNames)
            {
                tvl.Add(
                  new TypedValue(
                    (int)DxfCode.BlockName,
                    (blkName.StartsWith("*") ? "`" + blkName : blkName)
                  )
                );
            }

            // Add the final operator
            tvl.Add(
              new TypedValue(
                (int)DxfCode.Operator,
                "or>"
              )
            );

            // Return an array from the list
            return tvl.ToArray();
        }


        [CommandMethod("gw_ex_FilterMtextWildcard")]
        public static void Gw_Ex_FilterMtextWildcard()
        {
            // Get the current document editor
            Editor acDocEd = Application.DocumentManager.MdiActiveDocument.Editor;

            // Create a TypedValue array to define the filter criteria
            TypedValue[] acTypValAr = new TypedValue[2];
            acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "MTEXT"), 0);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.Text, "*The*"), 1);

            // Assign the filter criteria to a SelectionFilter object
            SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);

            // Request for objects to be selected in the drawing area
            PromptSelectionResult acSSPrompt;
            acSSPrompt = acDocEd.GetSelection(acSelFtr);

            // If the prompt status is OK, objects were selected
            if (acSSPrompt.Status == PromptStatus.OK)
            {
                SelectionSet acSSet = acSSPrompt.Value;

                Application.ShowAlertDialog("Number of objects selected: " +
                                            acSSet.Count.ToString());
            }
            else
            {
                Application.ShowAlertDialog("Number of objects selected: 0");
            }
        }


        [CommandMethod("gw_ex_MTextGreeting")]
        public static void Gw_Ex_MTextGreeting()
        {
            // Get the current document and database, and start a transaction
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database acCurDb = acDoc.Database;

            // Starts a new transaction with the Transaction Manager
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Open the Block table record for read
                BlockTable acBlkTbl;
                acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
                                             OpenMode.ForRead) as BlockTable;

                // Open the Block table record Model space for write
                BlockTableRecord acBlkTblRec;
                acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                OpenMode.ForWrite) as BlockTableRecord;

                /* Creates a new MText object and assigns it a location,
                text value and text style */
                using (MText objText = new MText())
                {
                    // Specify the insertion point of the MText object
                    objText.Location = new Autodesk.AutoCAD.Geometry.Point3d(2, 2, 0);

                    // Set the text string for the MText object
                    objText.Contents = "Greetings, Welcome to AutoCAD .NET";

                    // Set the text style for the MText object
                    objText.TextStyleId = acCurDb.Textstyle;

                    // Appends the new MText object to model space
                    acBlkTblRec.AppendEntity(objText);

                    // Appends to new MText object to the active transaction
                    acTrans.AddNewlyCreatedDBObject(objText, true);
                }

                // Saves the changes to the database and closes the transaction
                acTrans.Commit();
            }
        }



        [CommandMethod("gw_ex_ListAttributes")]
        public static void Gw_Ex_ListAttributes()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Autodesk.AutoCAD.DatabaseServices.Database db = HostApplicationServices.WorkingDatabase;
            Transaction tr = db.TransactionManager.StartTransaction();

            // Start the transaction
            try
            {
                // Build a filter list so that only
                // block references are selected
                TypedValue[] filList = new TypedValue[1] { new TypedValue((int)DxfCode.Start, "INSERT") };

                SelectionFilter filter = new SelectionFilter(filList);

                PromptSelectionOptions opts = new PromptSelectionOptions();

                opts.MessageForAdding = "Select block references: ";

                PromptSelectionResult res = ed.GetSelection(opts, filter);


                // Do nothing if selection is unsuccessful
                if (res.Status != PromptStatus.OK)
                    return;

                SelectionSet selSet = res.Value;
                ObjectId[] idArray = selSet.GetObjectIds();

                foreach (ObjectId blkId in idArray)
                {
                    BlockReference blkRef = (BlockReference)tr.GetObject(blkId, OpenMode.ForRead);

                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(blkRef.BlockTableRecord, OpenMode.ForRead);

                    ed.WriteMessage("\nBlock: " + btr.Name);
                    btr.Dispose();

                    AttributeCollection attCol = blkRef.AttributeCollection;

                    foreach (ObjectId attId in attCol)
                    {
                        AttributeReference attRef = (AttributeReference)tr.GetObject(attId, OpenMode.ForRead);

                        string str = ("\n  Attribute Tag: "
                            + attRef.Tag
                            + "\n    Attribute String: "
                            + attRef.TextString
                          );

                        ed.WriteMessage(str);
                    }
                }
                tr.Commit();
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                ed.WriteMessage(("Exception: " + ex.Message));
            }
            finally
            {
                tr.Dispose();
            }
        }




        [CommandMethod("gw_ex_GettingAttributes")]
        public static void Gw_Ex_GettingAttributes()
        {
            Autodesk.AutoCAD.DatabaseServices.Database acCurDb = Application.DocumentManager.MdiActiveDocument.Database;

            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Open the Block table for read
                BlockTable acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                ObjectId blkRecId = ObjectId.Null;

                //if (!acBlkTbl.Has("TESTBLOCK"))
                //{
                //    using (BlockTableRecord acBlkTblRec = new BlockTableRecord())
                //    {
                //        acBlkTblRec.Name = "TESTBLOCK";

                //        // Set the insertion point for the block
                //        acBlkTblRec.Origin = new Point3d(0, 0, 0);

                //        // Add an attribute definition to the block
                //        using (AttributeDefinition acAttDef = new AttributeDefinition())
                //        {
                //            acAttDef.Position = new Point3d(5, 5, 0);
                //            acAttDef.Prompt = "Attribute Prompt";
                //            acAttDef.Tag = "AttributeTag";
                //            acAttDef.TextString = "Attribute Value";
                //            acAttDef.Height = 1;
                //            acAttDef.Justify = AttachmentPoint.MiddleCenter;
                //            acBlkTblRec.AppendEntity(acAttDef);

                //            acBlkTbl.UpgradeOpen();
                //            acBlkTbl.Add(acBlkTblRec);
                //            acTrans.AddNewlyCreatedDBObject(acBlkTblRec, true);
                //        }

                //        blkRecId = acBlkTblRec.Id;
                //    }
                //}
                //else
                //{
                //    blkRecId = acBlkTbl["CircleBlockWithAttributes"];
                //}

                //// Create and insert the new block reference
                //if (blkRecId != ObjectId.Null)
                {
                    BlockTableRecord acBlkTblRec = acTrans.GetObject(blkRecId, OpenMode.ForRead) as BlockTableRecord;

                    using (BlockReference acBlkRef = new BlockReference(new Point3d(5, 5, 0), acBlkTblRec.Id))
                    {
                        BlockTableRecord acCurSpaceBlkTblRec = acTrans.GetObject(acCurDb.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;

                        acCurSpaceBlkTblRec.AppendEntity(acBlkRef);
                        acTrans.AddNewlyCreatedDBObject(acBlkRef, true);

                        // Verify block table record has attribute definitions associated with it
                        if (acBlkTblRec.HasAttributeDefinitions)
                        {
                            // Add attributes from the block table record
                            foreach (ObjectId objID in acBlkTblRec)
                            {
                                DBObject dbObj = acTrans.GetObject(objID, OpenMode.ForRead) as DBObject;

                                if (dbObj is AttributeDefinition)
                                {
                                    AttributeDefinition acAtt = dbObj as AttributeDefinition;

                                    if (!acAtt.Constant)
                                    {
                                        using (AttributeReference acAttRef = new AttributeReference())
                                        {
                                            acAttRef.SetAttributeFromBlock(acAtt, acBlkRef.BlockTransform);
                                            acAttRef.Position = acAtt.Position.TransformBy(acBlkRef.BlockTransform);

                                            acAttRef.TextString = acAtt.TextString;

                                            acBlkRef.AttributeCollection.AppendAttribute(acAttRef);
                                            acTrans.AddNewlyCreatedDBObject(acAttRef, true);
                                        }
                                    }
                                }
                            }

                            // Display the tags and values of the attached attributes
                            string strMessage = "";
                            AttributeCollection attCol = acBlkRef.AttributeCollection;

                            foreach (ObjectId objID in attCol)
                            {
                                DBObject dbObj = acTrans.GetObject(objID, OpenMode.ForRead) as DBObject;

                                AttributeReference acAttRef = dbObj as AttributeReference;

                                strMessage = strMessage + "Tag: " + acAttRef.Tag + "\n" +
                                                "Value: " + acAttRef.TextString + "\n";

                                // Change the value of the attribute
                                acAttRef.TextString = "NEW VALUE!";
                            }

                            Application.ShowAlertDialog("The attributes for blockReference " + acBlkRef.Name + " are:\n" + strMessage);

                            strMessage = "";
                            foreach (ObjectId objID in attCol)
                            {
                                DBObject dbObj = acTrans.GetObject(objID, OpenMode.ForRead) as DBObject;

                                AttributeReference acAttRef = dbObj as AttributeReference;

                                strMessage = strMessage + "Tag: " + acAttRef.Tag + "\n" +
                                                "Value: " + acAttRef.TextString + "\n";
                            }

                            Application.ShowAlertDialog("The attributes for blockReference " + acBlkRef.Name + " are:\n" + strMessage);
                        }
                    }
                }

                // Save the new object to the database
                acTrans.Commit();

                // Dispose of the transaction
            }
        }




        [CommandMethod("gw_ex_ExplodeEntities", CommandFlags.UsePickSet)]
        public static void Gw_Ex_ExplodeEntities()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;

            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            // Ask user to select entities
            PromptSelectionOptions pso = new PromptSelectionOptions();

            pso.MessageForAdding = "\nSelect objects to explode: ";
            pso.AllowDuplicates = false;
            pso.AllowSubSelections = true;
            pso.RejectObjectsFromNonCurrentSpace = true;
            pso.RejectObjectsOnLockedLayers = false;

            PromptSelectionResult psr = ed.GetSelection(pso);

            if (psr.Status != PromptStatus.OK)
                return;

            // Check whether to erase the original(s)
            bool eraseOrig = false;

            if (psr.Value.Count > 0)
            {
                PromptKeywordOptions pko = new PromptKeywordOptions("\nErase original objects?");

                pko.AllowNone = true;
                pko.Keywords.Add("Yes");
                pko.Keywords.Add("No");
                pko.Keywords.Default = "No";

                PromptResult pkr = ed.GetKeywords(pko);

                if (pkr.Status != PromptStatus.OK)
                    return;

                eraseOrig = (pkr.StringResult == "Yes");
            }

            Transaction tr = db.TransactionManager.StartTransaction();

            using (tr)
            {
                // Collect our exploded objects in a single collection
                DBObjectCollection objs = new DBObjectCollection();

                foreach (SelectedObject so in psr.Value)
                {
                    Entity ent = (Entity)tr.GetObject(so.ObjectId, OpenMode.ForRead);

                    if (ent.GetType() == typeof(BlockReference))
                    {
                        BlockReference blkRef = (BlockReference)ent;

                        //Debug.WriteLine("Block " + blkRef.Name);

                        // Explode the object into our collection
                        blkRef.Explode(objs);

                        //if (blkRef.IsDynamicBlock)
                        //{
                        //    //Debug.WriteLine("DynBlock " + blkRef.Name);
                        //    // Explode the object into our collection
                        //   // blkRef.Explode(objs);
                        //}                        

                        if (eraseOrig)
                        {
                            ent.UpgradeOpen();
                            ent.Erase();
                        }
                    }
                }

                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
                foreach (DBObject obj in objs)
                {
                    Entity ent = (Entity)obj;
                    btr.AppendEntity(ent);
                    tr.AddNewlyCreatedDBObject(ent, true);
                }

                tr.Commit();
            }

        }


        [CommandMethod("gw_ex_CopyObjectsBetweenDatabases", CommandFlags.Session)]
        public static void Gw_Ex_CopyObjectsBetweenDatabases()
        {
            ObjectIdCollection acObjIdColl = new ObjectIdCollection();

            // Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database acCurDb = acDoc.Database;

            // Lock the current document
            using (DocumentLock acLckDocCur = acDoc.LockDocument())
            {
                // Start a transaction
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    // Open the Block table record for read
                    BlockTable acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // Open the Block table record Model space for write
                    BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    // Create a circle that is at (0,0,0) with a radius of 5
                    using (Circle acCirc1 = new Circle())
                    {
                        acCirc1.Center = new Point3d(0, 0, 0);
                        acCirc1.Radius = 5;

                        // Add the new object to the block table record and the transaction
                        acBlkTblRec.AppendEntity(acCirc1);
                        acTrans.AddNewlyCreatedDBObject(acCirc1, true);

                        // Create a circle that is at (0,0,0) with a radius of 7
                        using (Circle acCirc2 = new Circle())
                        {
                            acCirc2.Center = new Point3d(0, 0, 0);
                            acCirc2.Radius = 7;

                            // Add the new object to the block table record and the transaction
                            acBlkTblRec.AppendEntity(acCirc2);
                            acTrans.AddNewlyCreatedDBObject(acCirc2, true);

                            // Add all the objects to copy to the new document
                            acObjIdColl = new ObjectIdCollection();
                            acObjIdColl.Add(acCirc1.ObjectId);
                            acObjIdColl.Add(acCirc2.ObjectId);
                        }
                    }

                    // Save the new objects to the database
                    acTrans.Commit();
                }

                // Unlock the document
            }

            // Change the file and path to match a drawing template on your workstation
            string sLocalRoot = Application.GetSystemVariable("LOCALROOTPREFIX") as string;
            string sTemplatePath = sLocalRoot + "Template\\acad.dwt";

            // Create a new drawing to copy the objects to
            DocumentCollection acDocMgr = Application.DocumentManager;
            Document acNewDoc = acDocMgr.Add(sTemplatePath);
            Autodesk.AutoCAD.DatabaseServices.Database acDbNewDoc = acNewDoc.Database;

            // Lock the new document
            using (DocumentLock acLckDoc = acNewDoc.LockDocument())
            {
                // Start a transaction in the new database
                using (Transaction acTrans = acDbNewDoc.TransactionManager.StartTransaction())
                {
                    // Open the Block table for read
                    BlockTable acBlkTblNewDoc = acTrans.GetObject(acDbNewDoc.BlockTableId, OpenMode.ForRead) as BlockTable;

                    // Open the Block table record Model space for read
                    BlockTableRecord acBlkTblRecNewDoc = acTrans.GetObject(acBlkTblNewDoc[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;

                    // Clone the objects to the new database
                    IdMapping acIdMap = new IdMapping();
                    acCurDb.WblockCloneObjects(acObjIdColl, acBlkTblRecNewDoc.ObjectId, acIdMap, DuplicateRecordCloning.Ignore, false);

                    // Save the copied objects to the database
                    acTrans.Commit();
                }

                // Unlock the document
            }

            // Set the new document current
            acDocMgr.MdiActiveDocument = acNewDoc;
        }



        [CommandMethod("gw_ex_ImportBlocks")]
        public static void Gw_Ex_ImportBlocks()
        {
            DocumentCollection dm = Application.DocumentManager;
            Editor ed = dm.MdiActiveDocument.Editor;
            Autodesk.AutoCAD.DatabaseServices.Database destDb = dm.MdiActiveDocument.Database;
            Autodesk.AutoCAD.DatabaseServices.Database sourceDb = new Autodesk.AutoCAD.DatabaseServices.Database(false, true);
            PromptResult sourceFileName;

            try
            {
                // Get name of DWG from which to copy blocks
                sourceFileName = ed.GetString("\nEnter the name of the source drawing: ");

                // Read the DWG into a side database
                sourceDb.ReadDwgFile(sourceFileName.StringResult,
                                    System.IO.FileShare.Read,
                                    true,
                                    "");


                // Create a variable to store the list of block identifiers
                ObjectIdCollection blockIds = new ObjectIdCollection();

                Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = sourceDb.TransactionManager;


                using (Transaction myT = tm.StartTransaction())
                {
                    // Open the block table
                    BlockTable bt = (BlockTable)tm.GetObject(sourceDb.BlockTableId, OpenMode.ForRead, false);

                    // Check each block in the block table
                    foreach (ObjectId btrId in bt)
                    {
                        BlockTableRecord btr = (BlockTableRecord)tm.GetObject(btrId, OpenMode.ForRead, false);

                        // Only add named & non-layout blocks to the copy list
                        if (!btr.IsAnonymous && !btr.IsLayout)
                            blockIds.Add(btrId);

                        btr.Dispose();
                    }

                }

                // Copy blocks from source to destination database
                IdMapping mapping = new IdMapping();

                sourceDb.WblockCloneObjects(blockIds,
                                            destDb.BlockTableId,
                                            mapping,
                                            DuplicateRecordCloning.Replace,
                                            false);

                ed.WriteMessage("\nCopied "
                                + blockIds.Count.ToString()
                                + " block definitions from "
                                + sourceFileName.StringResult
                                + " to the current drawing.");

            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                ed.WriteMessage("\nError during copy: " + ex.Message);
            }
            sourceDb.Dispose();
        }



        [CommandMethod("gw_ex_InsertTable_BOM")]
        public static void Gw_Ex_InsertTable_BOM()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            PromptPointResult pr = ed.GetPoint("\nEnter table insertion point: ");

            if (pr.Status == PromptStatus.OK)
            {
                Autodesk.AutoCAD.DatabaseServices.Table tb = new Autodesk.AutoCAD.DatabaseServices.Table();

                tb.TableStyle = db.Tablestyle;
                tb.SetSize(5, 3);
                tb.SetRowHeight(3);
                tb.SetColumnWidth(15);
                tb.Position = pr.Value;

                // Create a 2-dimensional array
                // of our table contents
                string[,] str = new string[5, 3];

                str[0, 0] = "Part No.";
                str[0, 1] = "Name ";
                str[0, 2] = "Material ";

                str[1, 0] = "1876-1";
                str[1, 1] = "Flange";
                str[1, 2] = "Perspex";

                str[2, 0] = "0985-4";
                str[2, 1] = "Bolt";
                str[2, 2] = "Steel";

                str[3, 0] = "3476-K";
                str[3, 1] = "Tile";
                str[3, 2] = "Ceramic";

                str[4, 0] = "8734-3";
                str[4, 1] = "Kean";
                str[4, 2] = "Mostly water";

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        tb.Cells[i, j].TextHeight = 1;
                        tb.Cells[i, j].TextString = str[i, j];
                        tb.Cells[i, j].Alignment = CellAlignment.MiddleCenter;
                    }
                }

                tb.GenerateLayout();
                Transaction tr = doc.TransactionManager.StartTransaction();

                using (tr)
                {
                    BlockTable bt = (BlockTable)tr.GetObject(doc.Database.BlockTableId, OpenMode.ForRead);
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                    btr.AppendEntity(tb);
                    tr.AddNewlyCreatedDBObject(tb, true);
                    tr.Commit();
                }
            }
        }









        class BlockJig : EntityJig
        {
            // Member variables
            private Matrix3d _ucs;
            private Point3d _pos;
            private Dictionary<ObjectId, ObjectId> _atts;
            private Transaction _tr;

            // Constructor
            public BlockJig(
              Matrix3d ucs,
              Transaction tr,
              BlockReference br,
              Dictionary<ObjectId, ObjectId> atts
            ) : base(br)
            {
                _ucs = ucs;
                _pos = br.Position;
                _atts = atts;
                _tr = tr;
            }


            protected override bool Update()
            {
                var br = (BlockReference)Entity;

                // Transform to the current UCS
                br.Position = _pos.TransformBy(_ucs);
                if (br.AttributeCollection.Count > 0)
                {
                    foreach (ObjectId id in br.AttributeCollection)
                    {
                        var obj = _tr.GetObject(id, OpenMode.ForRead);
                        var ar = obj as AttributeReference;

                        if (ar != null)
                        {
                            ar.UpgradeOpen();

                            // Open the associated attribute definition
                            var defId = _atts[ar.ObjectId];
                            var obj2 = _tr.GetObject(defId, OpenMode.ForRead);
                            var ad = (AttributeDefinition)obj2;
                            // Use it to set positional information on the
                            // reference
                            ar.SetAttributeFromBlock(ad, br.BlockTransform);
                            ar.AdjustAlignment(br.Database);
                        }
                    }
                }
                return true;
            }


            protected override SamplerStatus Sampler(JigPrompts prompts)
            {
                var opts = new JigPromptPointOptions("\nSelect insertion point:");

                opts.BasePoint = Point3d.Origin;
                opts.UserInputControls = UserInputControls.NoZeroResponseAccepted;

                var ppr = prompts.AcquirePoint(opts);
                var ucsPt = ppr.Value.TransformBy(_ucs.Inverse());
                if (_pos == ucsPt)
                    return SamplerStatus.NoChange;
                _pos = ucsPt;
                return SamplerStatus.OK;
            }

            public PromptStatus Run()
            {
                var doc = Application.DocumentManager.MdiActiveDocument;
                if (doc == null)
                    return PromptStatus.Error;

                return doc.Editor.Drag(this).Status;
            }
        }

        const string annoScalesDict = "ACDB_ANNOTATIONSCALES";

        [CommandMethod("gw_ex_InsertBlockwithJig")]
        public static void Gw_Ex_InsertBlockwithJig()
        {
            var doc = Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;
            var ed = doc.Editor;

            var pso = new PromptStringOptions("\nEnter block name: ");
            var pr = ed.GetString(pso);

            if (pr.Status != PromptStatus.OK)
                return;

            using (var tr = doc.TransactionManager.StartTransaction())
            {
                var bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);

                if (!bt.Has(pr.StringResult))
                {
                    ed.WriteMessage("\nBlock \"" + pr.StringResult + "\" not found.");
                    return;
                }

                var ms = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
                var btr = (BlockTableRecord)tr.GetObject(bt[pr.StringResult], OpenMode.ForRead);

                // Block needs to be inserted to current space before
                // being able to append attribute to it

                var br = new BlockReference(new Point3d(), btr.ObjectId);
                br.TransformBy(ed.CurrentUserCoordinateSystem);

                ms.AppendEntity(br);

                tr.AddNewlyCreatedDBObject(br, true);

                if (btr.Annotative == AnnotativeStates.True)
                {
                    var ocm = db.ObjectContextManager;
                    var occ = ocm.GetContextCollection(annoScalesDict);
                    br.AddContext(occ.CurrentContext);
                }
                else
                {
                    br.ScaleFactors = new Scale3d(br.UnitFactor);
                }

                // Instantiate our map between attribute references
                // and their definitions

                var atts = new Dictionary<ObjectId, ObjectId>();

                if (btr.HasAttributeDefinitions)
                {
                    foreach (ObjectId id in btr)
                    {
                        var obj = tr.GetObject(id, OpenMode.ForRead);

                        var ad = obj as AttributeDefinition;

                        if (ad != null && !ad.Constant)
                        {
                            var ar = new AttributeReference();

                            // Set the initial positional information

                            ar.SetAttributeFromBlock(ad, br.BlockTransform);
                            ar.TextString = ad.TextString;

                            // Add the attribute to the block reference
                            // and transaction

                            var arId = br.AttributeCollection.AppendAttribute(ar);

                            tr.AddNewlyCreatedDBObject(ar, true);

                            // Initialize our dictionary with the ObjectIds of
                            // the attribute reference & definition

                            atts.Add(arId, ad.ObjectId);
                        }
                    }
                }

                // Run the jig
                var jig =
                  new BlockJig(
                    ed.CurrentUserCoordinateSystem, tr, br, atts
                  );

                if (jig.Run() != PromptStatus.OK)
                    return;

                // Commit changes if user accepted, otherwise discard

                tr.Commit();
            }
        }










        [CommandMethod("gw_ex_UpdateAttribute")]
        public static void Gw_Ex_UpdateAttribute()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            // Have the user choose the block and attribute
            // names, and the new attribute value

            PromptResult pr =
              ed.GetString(
                "\nEnter name of block to search for: "
              );

            if (pr.Status != PromptStatus.OK)
                return;

            string blockName = pr.StringResult.ToUpper();

            pr =
              ed.GetString(
                "\nEnter tag of attribute to update: "
              );

            if (pr.Status != PromptStatus.OK)
                return;

            string attbName = pr.StringResult.ToUpper();

            pr =
              ed.GetString(
                "\nEnter new value for attribute: "
              );

            if (pr.Status != PromptStatus.OK)
                return;

            string attbValue = pr.StringResult;

            UpdateAttributesInDatabase(
              db,
              blockName,
              attbName,
              attbValue
            );

        }


        private static void UpdateAttributesInDatabase(

        Autodesk.AutoCAD.DatabaseServices.Database db,

          string blockName,

          string attbName,

          string attbValue

        )

        {

            Document doc =

              Application.DocumentManager.MdiActiveDocument;

            Editor ed = doc.Editor;


            // Get the IDs of the spaces we want to process

            // and simply call a function to process each


            ObjectId msId, psId;

            Transaction tr =

              db.TransactionManager.StartTransaction();

            using (tr)

            {

                BlockTable bt =

                  (BlockTable)tr.GetObject(

                    db.BlockTableId,

                    OpenMode.ForRead

                  );

                msId =

                  bt[BlockTableRecord.ModelSpace];

                psId =

                  bt[BlockTableRecord.PaperSpace];


                // Not needed, but quicker than aborting

                tr.Commit();

            }

            int msCount =

              UpdateAttributesInBlock(

                msId,

                blockName,

                attbName,

                attbValue

              );

            int psCount =

              UpdateAttributesInBlock(

                psId,

                blockName,

                attbName,

                attbValue

              );

            ed.Regen();


            // Display the results


            ed.WriteMessage(

              "\nProcessing file: " + db.Filename

            );

            ed.WriteMessage(

              "\nUpdated {0} instance{1} of " +

              "attribute {2} in the modelspace.",

              msCount,

              msCount == 1 ? "" : "s",

              attbName

            );

            ed.WriteMessage(

              "\nUpdated {0} instance{1} of " +

              "attribute {2} in the default paperspace.",

              psCount,

              psCount == 1 ? "" : "s",

              attbName

            );

        }


        private static int UpdateAttributesInBlock(
          ObjectId btrId,
          string blockName,
          string attbName,
          string attbValue
        )
        {
            // Will return the number of attributes modified
            int changedCount = 0;
            Document doc =
              Application.DocumentManager.MdiActiveDocument;

            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;

            Editor ed = doc.Editor;
            Transaction tr =
              doc.TransactionManager.StartTransaction();

            using (tr)
            {
                BlockTableRecord btr =
                  (BlockTableRecord)tr.GetObject(
                    btrId,
                    OpenMode.ForRead
                  );

                // Test each entity in the container...

                foreach (ObjectId entId in btr)
                {
                    Entity ent =
                      tr.GetObject(entId, OpenMode.ForRead)
                      as Entity;

                    if (ent != null)
                    {
                        BlockReference br = ent as BlockReference;

                        if (br != null)
                        {
                            BlockTableRecord bd =
                              (BlockTableRecord)tr.GetObject(
                                br.BlockTableRecord,
                                OpenMode.ForRead                                
                                );
                            
                            // ... to see whether it's a block with
                            // the name we're after

                            if (bd.Name.ToUpper() == blockName)
                            {
                                // Check each of the attributes...
                                foreach (ObjectId arId in br.AttributeCollection)
                                {
                                    DBObject obj =
                                      tr.GetObject(                                          
                                          arId,
                                        OpenMode.ForRead
                                      );

                                    AttributeReference ar =
                                      obj as AttributeReference;

                                    if (ar != null)
                                    {
                                        // ... to see whether it has
                                        // the tag we're after

                                        if (ar.Tag.ToUpper() == attbName)
                                        {
                                            // If so, update the value
                                            // and increment the counter

                                            ar.UpgradeOpen();
                                            ar.TextString = attbValue;
                                            ar.DowngradeOpen();
                                            changedCount++;
                                        }
                                    }
                                }
                            }
                            
                            // Recurse for nested blocks
                            changedCount +=
                              UpdateAttributesInBlock(
                                br.BlockTableRecord,
                                blockName,
                                attbName,
                                attbValue
                              );

                        }
                    }
                }

                tr.Commit();
            }

            return changedCount;
        }

        /// <summary>
        /// select all using filters
        /// ... CIRCLE,LINE
        /// ... 0,Layer1,Layer2
        /// </summary>
        [CommandMethod("gw_ex_LayerSelection")]
        public static void Gw_Ex_LayerSelection()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            TypedValue[] filterlist = new TypedValue[2];

            //select circle and line
            filterlist[0] = new TypedValue(0, "CIRCLE,LINE");

            //8 = DxfCode.LayerName
            filterlist[1] = new TypedValue(8, "0,Layer1,Layer2");

            SelectionFilter filter = new SelectionFilter(filterlist);

            PromptSelectionResult selRes = ed.SelectAll(filter);

            if (selRes.Status != PromptStatus.OK)
            {
                ed.WriteMessage("\nerror in getting the selectAll");
                return;
            }

            ObjectId[] ids = selRes.Value.GetObjectIds();

            ed.WriteMessage("No entity found: " + ids.Length.ToString() + "\n");

        }



        //public void ImportDrawingAsBlock(Document DestDocument, string sourceDrawing, Point3d InsertionPoint)
        //{
        //    Database DestDb = DestDocument.Database;
        //    Editor Ed = DestDocument.Editor;
        //    DocumentLock DocumentLock = DestDocument.LockDocument();
        //    string blockname = sourceDrawing.Remove(0, sourceDrawing.LastIndexOf("\\") + 1);
        //    blockname = blockname.Substring(0, blockname.Length - 4); // remove the extension

        //    using (DocumentLock)
        //    {
        //        ObjectId sourceBlockID = ObjectId.Null;
        //        Database InMemoryDb = new Database(false, true);

        //        using (InMemoryDb)
        //        {
        //            // Load the drawing into temporary inmemory database
        //            if (sourceDrawing.LastIndexOf(".dwg") > 0)
        //            {
        //                InMemoryDb.ReadDwgFile(sourceDrawing, System.IO.FileShare.Read, true, "");
        //            }
        //            else if (sourceDrawing.LastIndexOf(".dxf") > 0)
        //            {
        //                InMemoryDb.DxfIn("@" + sourceDrawing, @"c:\log.txt");
        //                throw new System.Exception("Importing .dxf is not implemented.");
        //            }
        //            else
        //            {
        //                Ed.WriteMessage("This is not a valid drawing extension : " + sourceDrawing.LastIndexOf(".dxf").ToString());
        //                return;
        //            }

        //            // Open transaction to the destination document (Mdi.ActiveDocument in most cases, but we dont know)
        //            using (Transaction Tr = DestDocument.TransactionManager.StartTransaction())
        //            {
        //                // Get the BlockTable of the destination DWG (DB)
        //                BlockTable DestDb_BlockTable = (BlockTable)Tr.GetObject(DestDb.BlockTableId, OpenMode.ForRead);

        //                // If the destgination DWG already contains this block definition
        //                // we will create a block reference and not a copy of the same definition
        //                if (DestDb_BlockTable.Has(blockname))
        //                {
        //                    Ed.WriteMessage("Block " + blockname + " already exists.\n Attempting to create block reference...");

        //                    BlockTableRecord DestDb_CurrentSpace = (BlockTableRecord)DestDb.CurrentSpaceId.GetObject(OpenMode.ForWrite);
        //                    using (DestDb_CurrentSpace)
        //                    {
        //                        BlockTableRecord DestDbBlockDefinition = (BlockTableRecord)Tr.GetObject(DestDb_BlockTable[blockname], OpenMode.ForRead);
        //                        sourceBlockID = DestDbBlockDefinition.ObjectId;

        //                        // Create a block reference to the existing block definition
        //                        using (BlockReference Bref = new BlockReference(InsertionPoint, sourceBlockID))
        //                        {
        //                            Matrix3d Mat = Matrix3d.Identity;
        //                            Bref.TransformBy(Mat);
        //                            Bref.ScaleFactors = new Scale3d(1, 1, 1);
        //                            DestDb_CurrentSpace.AppendEntity(Bref);
        //                            Tr.AddNewlyCreatedDBObject(Bref, true);
        //                            Ed.Regen();
        //                            Tr.Commit();
        //                        }
        //                    }
        //                    return;
        //                }
        //                else //(bt.Has(blockname))
        //                {
        //                    // There is not such block definition, so we are inserting/creating new one

        //                    sourceBlockID = DestDb.Insert(blockname, InMemoryDb, true);
        //                }

        //                // We continue here the creation of the new block definition of the sourceDWG
        //                DestDb_BlockTable.UpgradeOpen();
        //                BlockTableRecord btrec = (BlockTableRecord)sourceBlockID.GetObject(OpenMode.ForRead);
        //                btrec.UpgradeOpen();
        //                btrec.Name = blockname;
        //                btrec.DowngradeOpen();

        //                BlockTableRecord CurrentSpaceRec = (BlockTableRecord)DestDb.CurrentSpaceId.GetObject(OpenMode.ForWrite);
        //                using (CurrentSpaceRec)
        //                {
        //                    using (BlockReference bref = new BlockReference(InsertionPoint, sourceBlockID))
        //                    {
        //                        Matrix3d mat = Matrix3d.Identity;
        //                        bref.TransformBy(mat);
        //                        bref.ScaleFactors = new Scale3d(1, 1, 1);
        //                        CurrentSpaceRec.AppendEntity(bref);
        //                        Tr.AddNewlyCreatedDBObject(bref, true);

        //                        using (BlockTableRecord btAttRec = (BlockTableRecord)bref.BlockTableRecord.GetObject(OpenMode.ForRead))
        //                        {
        //                            Autodesk.AutoCAD.DatabaseServices.AttributeCollection atcoll = bref.AttributeCollection;
        //                            foreach (ObjectId subid in btAttRec)
        //                            {
        //                                Entity ent = (Entity)subid.GetObject(OpenMode.ForRead);
        //                                AttributeDefinition attDef = ent as AttributeDefinition;

        //                                if (attDef != null)
        //                                {
        //                                    //Ed.WriteMessage("\nValue: " + attDef.TextString);
        //                                    AttributeReference attRef = new AttributeReference();

        //                                    attRef.SetPropertiesFrom(attDef);
        //                                    attRef.Visible = attDef.Visible;
        //                                    attRef.SetAttributeFromBlock(attDef, bref.BlockTransform);
        //                                    attRef.HorizontalMode = attDef.HorizontalMode;
        //                                    attRef.VerticalMode = attDef.VerticalMode;
        //                                    attRef.Rotation = attDef.Rotation;
        //                                    attRef.TextStyle = attDef.TextStyle;
        //                                    attRef.Position = attDef.Position + InsertionPoint.GetAsVector();
        //                                    attRef.Tag = attDef.Tag;
        //                                    attRef.FieldLength = attDef.FieldLength;
        //                                    attRef.TextString = attDef.TextString;
        //                                    attRef.AdjustAlignment(DestDb);
        //                                    atcoll.AppendAttribute(attRef);
        //                                    Tr.AddNewlyCreatedDBObject(attRef, true);
        //                                }
        //                            }
        //                        }
        //                        bref.DowngradeOpen();
        //                    }
        //                }

        //                btrec.DowngradeOpen();
        //                DestDb_BlockTable.DowngradeOpen();
        //                Ed.Regen();
        //                Tr.Commit();
        //            }
        //        }
        //    }
        //}

        //public static BlockReference CreateBlockReferenceInCurrentSpace(string blockName, Point3d InsertionPoint)
        //{
        //    // Open transaction to the destination document (Mdi.ActiveDocument in most cases, but we dont know)
        //    Transaction Tr = Db.TransactionManager.StartTransaction();
        //    // Get the BlockTable of the destination DWG (DB)
        //    BlockTable Bt = (BlockTable)Tr.GetObject(Db.BlockTableId, OpenMode.ForRead);

        //    // If the destgination DWG already contains this block definition
        //    // we will create a block reference and not a copy of the same definition
        //    if (!Bt.Has(blockName))
        //    {
        //        Ed.WriteMessage("Block definition for " + blockName + " does not exists for CreateBlockReferenceInCurrentSpace() .\n Aborting...");
        //    }
        //    Ed.WriteMessage("Block " + blockName + " already exists.\n Attempting to create block reference...");

        //    BlockTableRecord CurrentSpace = (BlockTableRecord)Db.CurrentSpaceId.GetObject(OpenMode.ForWrite);

        //    BlockTableRecord BtRec = (BlockTableRecord)Tr.GetObject(Bt[blockName], OpenMode.ForRead);
        //    ObjectId sourceBlockID = BtRec.ObjectId;

        //    // Create a block reference to the existing block definition
        //    BlockReference Bref = new BlockReference(InsertionPoint, sourceBlockID);

        //    Matrix3d Mat = Matrix3d.Identity;
        //    Bref.TransformBy(Mat);
        //    Bref.ScaleFactors = new Scale3d(1, 1, 1);
        //    CurrentSpace.AppendEntity(Bref);
        //    Tr.AddNewlyCreatedDBObject(Bref, true);
        //    Ed.Regen();

        //    BtRec.Dispose();
        //    Bt.Dispose();
        //    CurrentSpace.Dispose();

        //    Tr.Commit();
        //    Tr.Dispose();

        //    return Bref;
        //}

        //public static void ImportDynamicBlock(string dynamicBlockDWGPath, Point3d BasePoint, Hashtable dynamicBlockProperties, Hashtable dynamicBlockAttributes)
        //{
        //    Document DestDocument = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
        //    Editor editor = DestDocument.Editor;
        //    Database DestDb = DestDocument.Database;
        //    DocumentLock DocumentLock = DestDocument.LockDocument();

        //    string blockname = dynamicBlockDWGPath.Remove(0, dynamicBlockDWGPath.LastIndexOf("\\") + 1);
        //    blockname = blockname.Remove(blockname.LastIndexOf(".dwg"));

        //    using (DocumentLock)
        //    {
        //        ObjectId sourceBlockID = ObjectId.Null;
        //        Database InMemoryDb = new Database(false, true);

        //        using (InMemoryDb)
        //        {
        //            // Load the DWG into temporary inmemory database
        //            InMemoryDb.ReadDwgFile(dynamicBlockDWGPath, System.IO.FileShare.Read, true, "");

        //            //blkid = curdb.Insert(path, db, true);  // old deprecated

        //            // Open transaction to the destination document (Mdi.ActiveDocument in most cases, but we dont know)
        //            using (Transaction tr = DestDocument.TransactionManager.StartTransaction())
        //            {
        //                // Get the BlockTable of the destination DWG (DB)
        //                BlockTable DestDb_BlockTable = (BlockTable)tr.GetObject(DestDb.BlockTableId, OpenMode.ForRead);

        //                // If the destgination DWG already contains this block definition
        //                // we will create a block reference and not a copy of the same definition
        //                if (DestDb_BlockTable.Has(blockname))
        //                {
        //                    //editor.WriteMessage("Block " + blockname + " already exists.\n Attempting to create block reference...");

        //                    BlockTableRecord DestDb_CurrentSpace = (BlockTableRecord)DestDb.CurrentSpaceId.GetObject(OpenMode.ForWrite);
        //                    using (DestDb_CurrentSpace)
        //                    {
        //                        BlockTableRecord DestDbDynamicBlockDefinition = (BlockTableRecord)tr.GetObject(DestDb_BlockTable[blockname], OpenMode.ForRead);
        //                        sourceBlockID = DestDbDynamicBlockDefinition.ObjectId;

        //                        // Create a block reference to the existing block definition

        //                        using (BlockReference bref = new BlockReference(BasePoint, sourceBlockID))
        //                        {

        //                            //Matrix3d mat = Matrix3d.Identity;
        //                            //bref.TransformBy(mat);
        //                            bref.ScaleFactors = new Scale3d(1, 1, 1);
        //                            DestDb_CurrentSpace.AppendEntity(bref);
        //                            tr.AddNewlyCreatedDBObject(bref, true);

        //                            // Only continue is we have a valid dynamic block
        //                            if (bref != null && bref.IsDynamicBlock)
        //                            {


        //                                DynamicBlockReferencePropertyCollection pc = bref.DynamicBlockReferencePropertyCollection;
        //                                // Loop through, getting the info for each property

        //                                foreach (DynamicBlockReferenceProperty prop in pc)
        //                                {
        //                                    if (dynamicBlockProperties.ContainsKey(prop.PropertyName.ToString()))
        //                                    {
        //                                        prop.Value = dynamicBlockProperties[prop.PropertyName];
        //                                    }
        //                                }
        //                                bref.Position = BasePoint;
        //                            }

        //                            using (BlockTableRecord btAttRec = (BlockTableRecord)bref.BlockTableRecord.GetObject(OpenMode.ForRead))
        //                            {
        //                                Autodesk.AutoCAD.DatabaseServices.AttributeCollection atcoll = bref.AttributeCollection;
        //                                foreach (ObjectId subid in btAttRec)
        //                                {
        //                                    Entity ent = (Entity)subid.GetObject(OpenMode.ForRead);
        //                                    AttributeDefinition attDef = ent as AttributeDefinition;

        //                                    if (attDef != null)
        //                                    {
        //                                        AttributeReference attRef = new AttributeReference();

        //                                        attRef.SetPropertiesFrom(attDef);
        //                                        attRef.Visible = attDef.Visible;
        //                                        attRef.SetAttributeFromBlock(attDef, bref.BlockTransform);
        //                                        attRef.HorizontalMode = attDef.HorizontalMode;
        //                                        attRef.VerticalMode = attDef.VerticalMode;
        //                                        attRef.Rotation = attDef.Rotation;
        //                                        attRef.TextStyle = attDef.TextStyle;
        //                                        attRef.Position = attDef.Position + BasePoint.GetAsVector();
        //                                        attRef.Tag = attDef.Tag;
        //                                        attRef.FieldLength = attDef.FieldLength;
        //                                        //attRef.TextString = attDef.TextString;
        //                                        attRef.AdjustAlignment(DestDb);
        //                                        atcoll.AppendAttribute(attRef);

        //                                        if (dynamicBlockAttributes.ContainsKey(attRef.Tag))
        //                                        {
        //                                            attRef.TextString = dynamicBlockAttributes[attRef.Tag].ToString();
        //                                        }
        //                                        else
        //                                        {
        //                                            attRef.TextString = attDef.TextString;
        //                                        }


        //                                        tr.AddNewlyCreatedDBObject(attRef, true);
        //                                    }
        //                                }
        //                            }
        //                            tr.Commit();
        //                            editor.Regen();
        //                        }
        //                    }
        //                    return;
        //                }
        //                else //(bt.Has(blockname))
        //                {
        //                    // There is not such block definition, so we are inserting/creating new one
        //                    sourceBlockID = DestDb.Insert(dynamicBlockDWGPath, InMemoryDb, false);
        //                }

        //                // We continue here the creation of the new block definition of the sourceDWG
        //                DestDb_BlockTable.UpgradeOpen();
        //                BlockTableRecord btrec = (BlockTableRecord)sourceBlockID.GetObject(OpenMode.ForRead);
        //                btrec.UpgradeOpen();
        //                btrec.Name = blockname;
        //                btrec.DowngradeOpen();

        //                BlockTableRecord currentSpaceBlockTableRecord = (BlockTableRecord)DestDb.CurrentSpaceId.GetObject(OpenMode.ForWrite);
        //                using (currentSpaceBlockTableRecord)
        //                {
        //                    // We have created the block definition up there, and now we create the block reference to this block definition
        //                    using (BlockReference bref = new BlockReference(BasePoint, sourceBlockID))
        //                    {
        //                        //bref.ScaleFactors = new Scale3d(1, 1, 1);
        //                        currentSpaceBlockTableRecord.AppendEntity(bref);
        //                        tr.AddNewlyCreatedDBObject(bref, true);

        //                        // Only continue is we have a valid dynamic block
        //                        if (bref != null && bref.IsDynamicBlock)
        //                        {


        //                            DynamicBlockReferencePropertyCollection pc = bref.DynamicBlockReferencePropertyCollection;
        //                            // Loop through, getting the info for each property
        //                            foreach (DynamicBlockReferenceProperty prop in pc)
        //                            {
        //                                if (dynamicBlockProperties.ContainsKey(prop.PropertyName))
        //                                {
        //                                    prop.Value = dynamicBlockProperties[prop.PropertyName];
        //                                }
        //                            }
        //                            bref.Position = BasePoint;
        //                        }

        //                        // Copy the attributes
        //                        using (BlockTableRecord btAttRec = (BlockTableRecord)bref.BlockTableRecord.GetObject(OpenMode.ForRead))
        //                        {
        //                            Autodesk.AutoCAD.DatabaseServices.AttributeCollection atcoll = bref.AttributeCollection;
        //                            foreach (ObjectId subid in btAttRec)
        //                            {
        //                                Entity ent = (Entity)subid.GetObject(OpenMode.ForRead);
        //                                AttributeDefinition attDef = ent as AttributeDefinition;

        //                                if (attDef != null)
        //                                {
        //                                    AttributeReference attRef = new AttributeReference();

        //                                    attRef.SetPropertiesFrom(attDef);
        //                                    attRef.Visible = attDef.Visible;
        //                                    attRef.SetAttributeFromBlock(attDef, bref.BlockTransform);
        //                                    attRef.HorizontalMode = attDef.HorizontalMode;
        //                                    attRef.VerticalMode = attDef.VerticalMode;
        //                                    attRef.Rotation = attDef.Rotation;
        //                                    attRef.TextStyle = attDef.TextStyle;
        //                                    attRef.Position = attDef.Position + BasePoint.GetAsVector();
        //                                    attRef.Tag = attDef.Tag;
        //                                    attRef.FieldLength = attDef.FieldLength;
        //                                    attRef.TextString = attDef.TextString;
        //                                    attRef.AdjustAlignment(DestDb);
        //                                    atcoll.AppendAttribute(attRef);

        //                                    if (dynamicBlockAttributes.ContainsKey(attRef.Tag))
        //                                    {
        //                                        attRef.TextString = dynamicBlockAttributes[attRef.Tag].ToString();
        //                                    }
        //                                    else
        //                                    {
        //                                        attRef.TextString = attDef.TextString;
        //                                    }

        //                                    tr.AddNewlyCreatedDBObject(attRef, true);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }

        //                btrec.DowngradeOpen();
        //                DestDb_BlockTable.DowngradeOpen();
        //                editor.Regen();
        //                tr.Commit();
        //            }
        //        }
        //    }
        //}

        //private static void SetDynamicBlockCustomProp(ObjectId BlockID, string propName, string propValue)
        //{
        //    Document myDwg = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
        //    Transaction myTrans = myDwg.TransactionManager.StartTransaction();
        //    BlockReference myBlockRef = (BlockReference)BlockID.GetObject(OpenMode.ForWrite);
        //    if (myBlockRef.IsDynamicBlock)
        //    {
        //        DynamicBlockReferencePropertyCollection myDynamicPropsCollection = myBlockRef.DynamicBlockReferencePropertyCollection;
        //        foreach (DynamicBlockReferenceProperty myDynamicProp in myDynamicPropsCollection)
        //        {
        //            if (myDynamicProp.PropertyName.ToUpper() == propName.ToUpper())
        //            {
        //                myDynamicProp.Value = propValue;
        //            }
        //        }
        //    }
        //    myTrans.Commit();
        //    myTrans.Dispose();
        //}




        //private int UpdateBlock(Database db, ObjectId btrId, string blockName, string blockNewName)
        //{
        //    Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //    int changedCount = 0;
        //    Transaction tr = db.TransactionManager.StartTransaction();
        //    using (tr)
        //    {
        //        BlockTableRecord btr = (BlockTableRecord)tr.GetObject(btrId, OpenMode.ForWrite);
        //        // Test each entity in the container...
        //        foreach (ObjectId entId in btr)
        //        {
        //            Entity ent = tr.GetObject(entId, OpenMode.ForWrite) as Entity;
        //            if (ent != null)
        //            {
        //                BlockReference br = ent as BlockReference;
        //                if (br != null)
        //                {
        //                    BlockTableRecord bd = (BlockTableRecord)tr.GetObject(br.BlockTableRecord, OpenMode.ForWrite);
        //                    // ... to see whether it's a block with
        //                    // the name we're after
        //                    if (bd.Name.ToUpper() == blockName)
        //                    {
        //                        ed.WriteMessage("\nModifying block : " + bd.Name);
        //                        bd.Name = blockNewName;
        //                        ed.WriteMessage("\nBlock is changed to : " + bd.Name);
        //                        changedCount++;
        //                    }
        //                }
        //            }
        //        }
        //        tr.Commit();
        //    }
        //    return changedCount;
        //}


    }



}

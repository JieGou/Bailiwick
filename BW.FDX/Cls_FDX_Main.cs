using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;



namespace MyFirstProject.BW.FDX
{
    public static class Cls_FDX_Main
    {

        #region FedExG

        public static string counter { get; set; }

        public static void BtnLblFedExAp_Click_Sub(string chkBxApOrTest)
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database acCurDb = acDoc.Database;

            int cnt = 0;

            using (acDoc.LockDocument())
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                PromptEntityOptions p = new PromptEntityOptions("Select " + chkBxApOrTest + " Text");
                p.SetRejectMessage("Pick Mtext....");
                p.AddAllowedClass(typeof(MText), true);

                PromptEntityResult acSSPrompt;

                do
                {
                    acSSPrompt = acDoc.Editor.GetEntity(p);
                    {
                        if (acSSPrompt.ObjectId != ObjectId.Null)
                        {
                            Entity acEnt = acTrans.GetObject(acSSPrompt.ObjectId, OpenMode.ForWrite) as Entity;

                            if (acEnt is MText)
                            {
                                MText mt = (MText)acEnt;

                                if (chkBxApOrTest == "AP")
                                {
                                    mt.Contents = "AP-" + counter.PadLeft(2, '0');
                                    mt.BackgroundFill = true;
                                    mt.UseBackgroundColor = true;
                                    mt.Draw();
                                }
                                if (chkBxApOrTest == "Test Point")
                                {
                                    mt.Contents = "TEST " + counter.PadLeft(2, '0');
                                    mt.Draw();
                                }
                                if (chkBxApOrTest == "Sensor")
                                {
                                    mt.Contents = "SN-" + counter.PadLeft(2, '0');
                                    mt.BackgroundFill = true;
                                    mt.UseBackgroundColor = true;
                                    mt.Draw();
                                }

                                cnt = int.Parse(counter);
                                cnt++;

                                counter = cnt.ToString();
                            }
                        }
                    }

                } while (acSSPrompt.Status == PromptStatus.OK);
                acTrans.Commit();
            }
        }


        /// <summary>
        /// select ibwave entities
        /// </summary>
        public static void BtnSelectMoveIBWave_Click_Sub()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            PromptPointResult pt = ed.GetPoint("\nPick insertion point for iBWaves: ");

            TypedValue[] tvs = new TypedValue[] {

                new TypedValue((int)DxfCode.Operator, "<or" ),

                new TypedValue( (int)DxfCode.Operator, "<and" ),
                new TypedValue( (int)DxfCode.BlockName, "WIFI*" ),
                new TypedValue( (int)DxfCode.Operator, "and>" ),

                new TypedValue( (int)DxfCode.Operator, "<and" ),
                new TypedValue((int)DxfCode.Start, "MTEXT"),
                new TypedValue((int)DxfCode.LayerName, "iBwave_PartsId"),
                new TypedValue( (int)DxfCode.Operator, "and>" ),

                new TypedValue( (int)DxfCode.Operator, "or>" )

            };

            SelectionFilter acSelFtr = new SelectionFilter(tvs);

            PromptSelectionResult acSSPrompt = ed.GetSelection(acSelFtr);

            if (acSSPrompt.Status == PromptStatus.OK)
            {
                ObjectId[] ids = acSSPrompt.Value.GetObjectIds();

                ObjectIdCollection idCol = new ObjectIdCollection(ids);

                using (DocumentLock acLckDoc = doc.LockDocument())
                {
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);

                        string blkName = "";

                        string blockNameBinary = DateTime.Now.ToBinary().ToString();

                        SymbolUtilityServices.ValidateSymbolName(blockNameBinary, false);

                        blkName = blockNameBinary;

                        BlockTableRecord btr = new BlockTableRecord();
                        btr.Name = blkName;

                        btr.Origin = pt.Value; // set insertion point of block

                        bt.UpgradeOpen();
                        ObjectId btrId = bt.Add(btr);
                        tr.AddNewlyCreatedDBObject(btr, true);

                        DBObjectCollection ents = new DBObjectCollection();
                        DBObjectCollection entsTxt = new DBObjectCollection();

                        foreach (ObjectId id in idCol)
                        {
                            Entity ent = (Entity)tr.GetObject(id, OpenMode.ForRead);
                            Entity nEnt = (Entity)ent.Clone();

                            //if (nEnt is Circle)
                            //{
                            //    nEnt.ColorIndex = 4;
                            //    Circle c = (Circle)nEnt;
                            //    c.Radius = 36;
                            //}
                            //if (nEnt is DBText)
                            //{
                            //    nEnt.ColorIndex = 4;
                            //    DBText t = (DBText)nEnt;
                            //    t.Height = 24;
                            //    entsTxt.Add(nEnt);
                            //}

                            ents.Add(nEnt);
                        }

                        foreach (Entity ent in ents)
                        {
                            btr.AppendEntity(ent);
                            tr.AddNewlyCreatedDBObject(ent, true);
                        }

                        // Add a block reference to the model space
                        BlockTableRecord ms = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                        BlockReference br = new BlockReference(pt.Value, btrId); //Point3d.Origin

                        ms.AppendEntity(br);
                        tr.AddNewlyCreatedDBObject(br, true);

                        tr.Commit();

                        ed.WriteMessage("\nCreated block named \"{0}\" containing {1} entities.", blkName, ents.Count);
                    }
                }
            }
            else
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
            }
        }

        public static void BtnSelectBWaveText_Click_Sub()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;
                 
            TypedValue[] tvs = new TypedValue[] {

                new TypedValue((int)DxfCode.Operator, "<or" ),

                //new TypedValue( (int)DxfCode.Operator, "<and" ),
                //new TypedValue( (int)DxfCode.BlockName, "WIFI*" ),
                //new TypedValue( (int)DxfCode.Operator, "and>" ),

                new TypedValue( (int)DxfCode.Operator, "<and" ),
                new TypedValue((int)DxfCode.Start, "MTEXT"),
                new TypedValue((int)DxfCode.LayerName, "iBwave_PartsId"),
                new TypedValue( (int)DxfCode.Operator, "and>" ),

                new TypedValue( (int)DxfCode.Operator, "or>" )

            };

            SelectionFilter acSelFtr = new SelectionFilter(tvs);

            PromptSelectionResult acSSPrompt = ed.GetSelection(acSelFtr);

            if (acSSPrompt.Status == PromptStatus.OK)
            {
                ObjectId[] ids = acSSPrompt.Value.GetObjectIds();

                ObjectIdCollection idCol = new ObjectIdCollection(ids);

                using (DocumentLock acLckDoc = doc.LockDocument())
                {
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        DBObjectCollection entsVan = new DBObjectCollection();
                        DBObjectCollection entsOfc = new DBObjectCollection();
                        DBObjectCollection entsFlr = new DBObjectCollection();
                        DBObjectCollection entsOut = new DBObjectCollection();

                        foreach (ObjectId id in idCol)
                        {
                            MText ent = (MText)tr.GetObject(id, OpenMode.ForRead);
                   //         Entity nEnt = (Entity)ent.Clone();

                            //if (nEnt is Circle)
                            //{
                            //    nEnt.ColorIndex = 4;
                            //    Circle c = (Circle)nEnt;
                            //    c.Radius = 36;
                            //}
                            //if (nEnt is DBText)
                            //{
                            //    nEnt.ColorIndex = 4;
                            //    DBText t = (DBText)nEnt;
                            //    t.Height = 24;
                            //    entsTxt.Add(nEnt);
                            //}

                            if (ent.Text.Contains("VAN"))
                                entsVan.Add(ent);

                            if (ent.Text.Contains("OFC"))
                                entsOfc.Add(ent);

                            if (ent.Text.Contains("FLR"))
                                entsFlr.Add(ent);

                            if (ent.Text.Contains("OUT"))
                                entsOut.Add(ent);

                        }

                        tr.Commit();


                        ed.WriteMessage("\nCable Count (2 Per) \"{0}\" containing {1} entities. = {2} Feet Cable", "VAN", entsVan.Count, entsVan.Count * 500);
                        ed.WriteMessage("\nCable Count (1 Per) \"{0}\" containing {1} entities. = {2} Feet Cable", "OFC", entsOfc.Count, entsOfc.Count * 250);
                        ed.WriteMessage("\nCable Count (2 Per) \"{0}\" containing {1} entities. = {2} Feet Cable", "FLR", entsFlr.Count, entsFlr.Count * 500);
                        ed.WriteMessage("\nCable Count (1 Per) \"{0}\" containing {1} entities. = {2} Feet Cable", "OUT", entsOut.Count, entsOut.Count * 250);

                        ed.WriteMessage("\nJack Count (4 Per) \"{0}\" containing {1} entities. = {2} Jack Count", "VAN", entsVan.Count, entsVan.Count * 4);
                        ed.WriteMessage("\nJack Count (2 Per) \"{0}\" containing {1} entities. = {2} Jack Count", "OFC", entsOfc.Count, entsOfc.Count * 2);
                        ed.WriteMessage("\nJack Count (4 Per) \"{0}\" containing {1} entities. = {2} Jack Count", "FLR", entsFlr.Count, entsFlr.Count * 4);
                        ed.WriteMessage("\nJack Count (2 Per) \"{0}\" containing {1} entities. = {2} Jack Count", "OUT", entsOut.Count, entsOut.Count * 2);

                    }
                }
            }
            else
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
            }
        }

        #endregion




    }
}

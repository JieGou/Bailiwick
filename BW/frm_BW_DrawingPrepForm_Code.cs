using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
namespace MyFirstProject.BW
{
    public static class Frm_BW_DrawingPrepForm_Code
    {     
     



        /// <summary>
        /// Change the color of all layers
        /// </summary>   
        public static void Bw_LayerIterator(Autodesk.AutoCAD.Colors.Color clr)
        {
            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            using (doc.LockDocument())
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                SymbolTable symTable = (SymbolTable)transaction.GetObject(database.LayerTableId, OpenMode.ForRead);
                foreach (ObjectId id in symTable)
                {
                    LayerTableRecord acLyrTblRec = (LayerTableRecord)transaction.GetObject(id, OpenMode.ForWrite);

                    acLyrTblRec.Color = clr;
                }
                transaction.Commit();
            }
        }

        public static void Bw_ModelSpaceIterator(Autodesk.AutoCAD.Colors.Color clr,
            Autodesk.AutoCAD.Colors.Color clrG1, Autodesk.AutoCAD.Colors.Color clrG2)
        {
            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            using (doc.LockDocument())
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                BW.Cls_BW_HandleAutocadObjTypes cls = new Cls_BW_HandleAutocadObjTypes();

                BlockTableRecord btRecord = (BlockTableRecord)transaction.GetObject(SymbolUtilityServices.GetBlockModelSpaceId(database), OpenMode.ForRead);
                foreach (ObjectId id in btRecord)
                {
                    //Autodesk.AutoCAD.Runtime.Exception: 'eOnLockedLayer'
                    Entity entity = (Entity)transaction.GetObject(id, OpenMode.ForWrite);
                    
                    cls.ProcessEntityType(entity, clr, clrG1, clrG2);
                }
                transaction.Commit();
            }
        }

        public static void Bw_BlockIterator(
            Autodesk.AutoCAD.Colors.Color clr,
            Autodesk.AutoCAD.Colors.Color clrG1,
            Autodesk.AutoCAD.Colors.Color clrG2
            )
        {
            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            using (doc.LockDocument())
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                BW.Cls_BW_HandleAutocadObjTypes cls = new Cls_BW_HandleAutocadObjTypes();

                BlockTable blkTable = (BlockTable)transaction.GetObject(database.BlockTableId, OpenMode.ForRead);
                foreach (ObjectId id in blkTable)
                {
                    BlockTableRecord btRecord = (BlockTableRecord)transaction.GetObject(id, OpenMode.ForRead);
                    if (!(btRecord.IsFromExternalReference || btRecord.IsLayout))
                    {
                        foreach (ObjectId id1 in btRecord)
                        {                           
                            Entity entity = (Entity)transaction.GetObject(id1, OpenMode.ForWrite);
                            
                            cls.ProcessEntityType(entity, clr, clrG1, clrG2);
                        }
                    }
                }
                transaction.Commit();
            }
        }

        public static void Bw_ThawAndUnlockLayers()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            doc.LyrsLockOrUnlockAllLayers(false);
            doc.LyrsFreezeOrFreezeAllLayers(false);
        }

        public static void Bw_PurgeUnreferencedLayers()
        {
            string msg = Environment.NewLine + "The following layers have been deleted:" + Environment.NewLine;

            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database acCurDb = doc.Database;
            Editor ed = doc.Editor;

            using (doc.LockDocument())
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                LayerTable acLyrTbl = acTrans.GetObject(acCurDb.LayerTableId, OpenMode.ForRead) as LayerTable;

                ObjectIdCollection acObjIdColl = new ObjectIdCollection();

                foreach (ObjectId acObjId in acLyrTbl)
                {
                    acObjIdColl.Add(acObjId);
                }

                acCurDb.Purge(acObjIdColl);

                foreach (ObjectId acObjId in acObjIdColl)
                {
                    SymbolTableRecord acSymTblRec = acTrans.GetObject(acObjId, OpenMode.ForWrite) as SymbolTableRecord;

                    try
                    {
                        acSymTblRec.Erase(true);
                        msg += acSymTblRec.Name + Environment.NewLine;
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception Ex)
                    {
                        Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Error:\n" + Ex.Message);
                    }
                }
                acTrans.Commit();
                ed.WriteMessage(msg + Environment.NewLine);
            }
        }

        public static void Bw_DeleteOffFrznLyrs()
        {
            string msg = Environment.NewLine + "The following layers have been deleted:" + Environment.NewLine;

            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            LayerTableRecord layer;

            using (doc.LockDocument())
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                LayerTable layerTable = (LayerTable)tr.GetObject(db.LayerTableId, OpenMode.ForRead);

                foreach (ObjectId acObjId in layerTable)
                {
                    if (db.Clayer == acObjId) // cannot delete current layer
                        continue;

                    layer = (LayerTableRecord)tr.GetObject(acObjId, OpenMode.ForWrite);

                    if (layer.Name == "0")
                        continue;

                    if (layer.Name.ToLower() == "defpoints")
                        continue;

                    if (layer.IsOff | layer.IsFrozen)
                    {
                        try
                        {
                            layer.IsLocked = false;

                            var blockTable = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                            foreach (var btrId in blockTable)
                            {
                                var block = (BlockTableRecord)tr.GetObject(btrId, OpenMode.ForRead);
                                foreach (var entId in block)
                                {
                                    var ent = (Entity)tr.GetObject(entId, OpenMode.ForRead);
                                    if (ent.Layer == layer.Name)
                                    {
                                        ent.UpgradeOpen();
                                        ent.Erase();
                                    }
                                }
                            }
                            layer.Erase();
                            msg += layer.Name + Environment.NewLine;
                        }
                        catch (System.Exception e)
                        {
                            Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Error:\n" + "Error: " + e.Message);
                        }
                    }
                }
                tr.Commit();
                ed.WriteMessage(msg + Environment.NewLine);
            }
        }

        public static void Bw_PurgeRegApps()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            int count = PurgeDatabase(db);

            ed.WriteMessage(
              "\nPurged {0} object{1} from " +
              "the current database.",
              count,
              count == 1 ? "" : "s"
            );

        }
        private static int PurgeDatabase(Autodesk.AutoCAD.DatabaseServices.Database db)
        {
            string msg = "\nRegistered applications being purged: " + Environment.NewLine;

            int idCount = 0;
            Transaction tr = db.TransactionManager.StartTransaction();
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;

            using (doc.LockDocument())
            using (tr)
            {
                // Create the list of objects to "purge"
                ObjectIdCollection idsToPurge = new ObjectIdCollection();

                // Add all the Registered Application names
                RegAppTable rat = (RegAppTable)tr.GetObject(
                    db.RegAppTableId,
                    OpenMode.ForRead
                );

                foreach (ObjectId raId in rat)
                {
                    if (raId.IsValid)
                    {
                        idsToPurge.Add(raId);
                    }
                }

                // Call the Purge function to filter the list
                db.Purge(idsToPurge);

                // Erase each of the objects we've been
                // allowed to
                foreach (ObjectId id in idsToPurge)
                {
                    DBObject obj = tr.GetObject(id, OpenMode.ForWrite);

                    // Let's just add to me "debug" code
                    // to list the registered applications
                    // we're erasing
                    RegAppTableRecord ratr = obj as RegAppTableRecord;

                    if (ratr != null)
                    {
                        msg += (
                          ratr.Name + Environment.NewLine
                        );
                    }

                    obj.Erase();
                }

                // Return the number of objects erased
                // (i.e. purged)
                idCount = idsToPurge.Count;
                tr.Commit();
                ed.WriteMessage(
                          msg
                        );
            }
            return idCount;
        }




        class dmp
        {
            public string block { get; set; }
            public List<string> Atts { get; set; }
        }

        public static void ListBlockAtts()
        {
            List<dmp> lstDmp = new List<dmp>();

            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database acCurDb = acDoc.Database;
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                BlockTable acBlkTble;
                acBlkTble = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;
                foreach (ObjectId objId in acBlkTble)
                {
                    BlockTableRecord btr;
                    btr = acTrans.GetObject(objId, OpenMode.ForRead) as BlockTableRecord;

                    if (btr.HasAttributeDefinitions)
                    {
                        dmp d = new dmp();
                        d.Atts = new List<string>();

                        acDoc.Editor.WriteMessage("\n" + btr.Name + ", ");
                        d.block = btr.Name;

                        foreach (ObjectId id in btr)
                        {
                            Entity e = (Entity)acTrans.GetObject(id, OpenMode.ForRead);
                            if (e is AttributeDefinition)
                            {
                                AttributeDefinition a = (AttributeDefinition)e;
                                acDoc.Editor.WriteMessage(a.Tag + ", ");
                                d.Atts.Add(a.Tag);
                            }
                        }
                        lstDmp.Add(d);
                    }
                }
                acDoc.Editor.WriteMessage("\n");
                acTrans.Commit();
            }

            //PropertyInfo[] pi;

            //foreach (dmp d in lstDmp)
            //{
            //    switch (d.block)
            //    {
            //        case "wap":

            //            pi = typeof(MMM.Cls_MMM_Waps_Atts).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //            //      Type[] tt = typeof(Int_3M_AllAtts).GetInterfaces();
            //            //      PropertyInfo[] pis2 = tt[0].GetProperties(BindingFlags.Public | BindingFlags.Instance);

            //            foreach (var n in pi)
            //            {
            //                if (!d.atts.Contains(n.Name))
            //                {
            //              //      n.
            //                    acDoc.Editor.WriteMessage("");
            //                }
            //                foreach (string s in d.atts)
            //                {
            //                    if (!n.Name.Contains(s))
            //                    {
            //                        acDoc.Editor.WriteMessage("");
            //                    }
            //                }
            //            }

            //            break;

            //    }


        }


    }
}

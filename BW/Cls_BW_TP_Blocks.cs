using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using OfficeOpenXml;

namespace MyFirstProject.BW
{
    public class Cls_BW_TP_Blocks
    {
        #region blocks tab page

        class dmp
        {
            public string block { get; set; }
            public List<string> atts { get; set; }
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
                        d.atts = new List<string>();

                        d.block = btr.Name;

                        foreach (ObjectId id in btr)
                        {
                            Entity e = (Entity)acTrans.GetObject(id, OpenMode.ForRead);
                            if (e is AttributeDefinition)
                            {
                                AttributeDefinition a = (AttributeDefinition)e;
                                d.atts.Add(a.Tag.ToUpper());
                            }
                        }
                        lstDmp.Add(d);
                    }
                }
                acTrans.Commit();
            }

            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            string xlsxFileName = doc.Name.ToLower();

            xlsxFileName = xlsxFileName.Replace(".dwg", "") + " Att Dump " +
                DateTime.Now.ToString("yMMdd", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + ".xlsx";

            FileInfo newFile = new FileInfo(xlsxFileName);

            ExcelPackage xlPackage = new ExcelPackage();

            int col = 1;

            using (xlPackage = new ExcelPackage(newFile))
            {
                if (xlPackage.Workbook.Worksheets.Count > 0)
                    xlPackage.Workbook.Worksheets.Delete(1);

                xlPackage.Workbook.Worksheets.Add("Att Dump");

                ExcelWorksheet wsWAP = xlPackage.Workbook.Worksheets[1];

                List<dmp> lst = lstDmp.Where(x => x.block.Contains("wap")).OrderBy(x => x.block).ToList();
                ToXlFile(acDoc, ref col, wsWAP, lst);

                lst = lstDmp.Where(x => x.block.Contains("data")).OrderBy(x => x.block).ToList();
                ToXlFile(acDoc, ref col, wsWAP, lst);

                lst = lstDmp.Where(x => x.block.Contains("voice")).OrderBy(x => x.block).ToList();
                ToXlFile(acDoc, ref col, wsWAP, lst);

                lst = lstDmp.Where(x => x.block.Contains("plc")).OrderBy(x => x.block).ToList();
                ToXlFile(acDoc, ref col, wsWAP, lst);

                lst = lstDmp.Where(x => x.block.Contains("fiber")).OrderBy(x => x.block).ToList();
                ToXlFile(acDoc, ref col, wsWAP, lst);

                wsWAP.Cells.AutoFitColumns();

                xlPackage.Save();

                MessageBox.Show("Att Dump Saved To: " + xlsxFileName + Environment.NewLine +
                    "Att Dump Saved for file: " + doc.Database.Filename);
            }
        }

        private static void ToXlFile(Document acDoc, ref int col, ExcelWorksheet wsWAP, List<dmp> lst)
        {
            foreach (dmp d in lst)
            {
                Print(acDoc, d, wsWAP, col);
                col++;
            }
        }

        private static int dmpRowCntr { get; set; } = 1;
        private static void Print(Document acDoc, dmp d, ExcelWorksheet wsWAP, int col)
        {
            dmpRowCntr = 1;

            PropertyInfo[] pi;

            wsWAP.Cells[dmpRowCntr, col].Value = "Block: " + d.block;
            dmpRowCntr++;

            pi = typeof(BW.Int_BW_AllAtts).GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            //      Type[] tt = typeof(Int_3M_AllAtts).GetInterfaces();
            //      PropertyInfo[] pis2 = tt[0].GetProperties(BindingFlags.Public | BindingFlags.Instance);

            List<string> lstProps = new List<string>();

            foreach (var p in pi)
            {
                lstProps.Add(p.Name.ToUpper());

                if (!d.atts.Contains(p.Name.ToUpper()))
                {
                    wsWAP.Cells[dmpRowCntr, col].Value = "Not in Attributes: " + p.Name;
                    dmpRowCntr++;
                }
            }

            foreach (string s in d.atts)
            {
                if (!lstProps.Contains(s))
                {
                    wsWAP.Cells[dmpRowCntr, col].Value = "Not in Class: " + s;
                    dmpRowCntr++;
                }
            }
        }
                

        public static void BlockDefAttributeSort()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var acDb = HostApplicationServices.WorkingDatabase;
            var acEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            List<string> order = GetAttributeOrderList();

            try
            {
                using (doc.LockDocument())
                using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
                {
                    BlockTable acBlockTable = (BlockTable)acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead);
                    foreach (ObjectId objId in acBlockTable)
                    {
                        BlockTableRecord blockDef = objId.GetObject(OpenMode.ForRead) as BlockTableRecord;
                        if (!blockDef.HasAttributeDefinitions)
                            continue;

                        switch (blockDef.Name)
                        {
                            #region block names
                            case "voice 1 single":
                            case "voice 1 single wall":
                            case "data 1 single camera":
                            case "data 1 single":
                            case "data 2 dual":
                            case "data 3 triple":
                            case "data 4 quad":
                            case "data 5 quint":
                            case "data 6 sextet":
                            case "fiber 6 sextet":
                            case "plc 1 single":
                            case "plc 2 dual":
                            case "plc 3 triple":
                            case "plc 4 quad":
                            case "plc 5 quint":
                            case "plc 6 sextet":

                            case "dyn voice 1 single":
                            case "dyn voice 1 single wall":
                            case "dyn data 1 single camera":
                            case "dyn data 1 single":
                            case "dyn data 2 dual":
                            case "dyn data 3 triple":
                            case "dyn data 4 quad":
                            case "dyn data 5 quint":
                            case "dyn data 6 sextet":
                            case "dyn plc 1 single":
                            case "dyn plc 2 dual":
                            case "dyn plc 3 triple":
                            case "dyn plc 4 quad":
                            case "dyn plc 5 quint":
                            case "dyn plc 6 sextet":

                            case "wap":
                            case "wap data 2 dual": 
                                #endregion

                                blockDef.UpgradeOpen();

                                foreach (string tag in order)
                                {
                                    //Get the right att Definition
                                    AttributeDefinition attDef = GetTheCorrectAttributeDef(tag, acTrans, blockDef);

                                    if (attDef != null)
                                    {
                                        AttributeDefinition newAtt = (AttributeDefinition)attDef.Clone();

                                        blockDef.AppendEntity(newAtt);
                                        acTrans.AddNewlyCreatedDBObject(newAtt, true);
                                        attDef.Erase();
                                    }
                                }
                                break;
                        }

                    }
                    acTrans.Commit();
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                acEd.WriteMessage(ex.ToString());
            }
        }

        private static List<string> GetAttributeOrderList()
        {
            List<string> order = new List<string>();

            // all other blocks may contain sub-set of atts
            order.Add("LABEL1");
            order.Add("LABEL2");
            order.Add("WAP");
            order.Add("APNUMBER");
            order.Add("OLDWAOIDENT");
            order.Add("SITE");
            order.Add("BLDG");
            order.Add("BUILDING");
            order.Add("FLOOR");
            order.Add("CLOSETINFO");
            order.Add("DEPARTMENTSUITE");
            order.Add("ANTENNA");
            order.Add("MOUNT");
            order.Add("DISPLAYCODE");
            order.Add("AFF");
            order.Add("DATATOTAL");
            order.Add("VOICETOTAL");
            order.Add("SINGLE");
            order.Add("DUAL");
            order.Add("TRIPLE");
            order.Add("QUAD");
            order.Add("QUINT");
            order.Add("SEXTET");
            order.Add("HOUSING");
            order.Add("JACKCOLOR");
            order.Add("JACKTYPE");
            order.Add("KEYNOTE");
            order.Add("PLATFORM");
            order.Add("DEVICE");
            order.Add("CABLECOMBINATIONS");
            order.Add("CONDUIT");
            order.Add("CABLE1");
            order.Add("CABLE1QUANT");
            order.Add("CABLE2");
            order.Add("CABLE2QUANT");
            order.Add("CABLE3");
            order.Add("CABLE3QUANT");
            order.Add("PATCH1CLR");
            order.Add("PATCH1LEN");
            order.Add("PATCH1CAT");
            order.Add("PATCH1QUANT");
            order.Add("PATCH2CLR");
            order.Add("PATCH2LEN");
            order.Add("PATCH2CAT");
            order.Add("PATCH2QUANT");
            order.Add("PATCH3CLR");
            order.Add("PATCH3LEN");
            order.Add("PATCH3CAT");
            order.Add("PATCH3QUANT");
            order.Add("PATCH4CLR");
            order.Add("PATCH4LEN");
            order.Add("PATCH4CAT");
            order.Add("PATCH4QUANT");
            order.Add("WRLESSPECIALFEED1");
            order.Add("SPECIALFEED2");
            order.Add("AMREPORTNAME");
            order.Add("AMREPORTDATE");
            order.Add("AMAPNUM");
            order.Add("WAOTYPE");
            order.Add("DISTANCEFROMCLOSETINFEET");
            order.Add("ISLONGRUN");

            // wap
            order.Add("BRAND");
            order.Add("SERIES");

            return order;
        }

        private static AttributeDefinition GetTheCorrectAttributeDef(string tag, Transaction acTrans, BlockTableRecord blockDef)
        {
            AttributeDefinition attDef = null;

            foreach (var attId in blockDef)
            {
                attDef = acTrans.GetObject(attId, OpenMode.ForWrite) as AttributeDefinition;

                if (attDef == null)
                    continue;

                if (attDef.Tag == tag)
                {
                    break;
                }
                else
                {
                    attDef = null;
                }
            }
            return attDef;
        }
        

        public static void BlockRefAttributeSort(PromptSelectionResult acSSPrompt)
        {
            if (acSSPrompt == null)
                return;

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

            Cls_BW_Main.frm_MMM_Host_Form.Hide();

            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var acDb = HostApplicationServices.WorkingDatabase;
            var acEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            List<string> order = GetAttributeOrderList();

            List<ObjectId> LstObjectIdWapD = new List<ObjectId>();
            
            foreach (ObjectId id1 in ids)
            {
                using (doc.LockDocument())
                using (Transaction tr = acDb.TransactionManager.StartTransaction())
                {
                    BlockReference blkRef = (BlockReference)tr.GetObject(id1, OpenMode.ForRead);
                    Autodesk.AutoCAD.DatabaseServices.AttributeCollection attColWap = blkRef.AttributeCollection;

                    string nam = "";
                    nam = blkRef.Name;

                    if (blkRef.IsDynamicBlock)
                    {
                        BlockTableRecord relName = (BlockTableRecord)tr.GetObject(blkRef.DynamicBlockTableRecord, OpenMode.ForRead);
                        nam = relName.Name;
                    }

                    switch (nam)
                    {
                        #region block names
                        case "voice 1 single":
                        case "voice 1 single wall":
                        case "data 1 single camera":
                        case "data 1 single":
                        case "data 2 dual":
                        case "data 3 triple":
                        case "data 4 quad":
                        case "data 5 quint":
                        case "data 6 sextet":
                        case "fiber 6 sextet":
                        case "plc 1 single":
                        case "plc 2 dual":
                        case "plc 3 triple":
                        case "plc 4 quad":
                        case "plc 5 quint":
                        case "plc 6 sextet":


                        case "dyn voice 1 single":
                        case "dyn voice 1 single wall":
                        case "dyn data 1 single camera":
                        case "dyn data 1 single":
                        case "dyn data 2 dual":
                        case "dyn data 3 triple":
                        case "dyn data 4 quad":
                        case "dyn data 5 quint":
                        case "dyn data 6 sextet":
                        case "dyn plc 1 single":
                        case "dyn plc 2 dual":
                        case "dyn plc 3 triple":
                        case "dyn plc 4 quad":
                        case "dyn plc 5 quint":
                        case "dyn plc 6 sextet":

                        case "wap":
                        case "wap data 2 dual": 
                            #endregion

                            blkRef.UpgradeOpen();

                            // get block def, to add missing attributes
                            BlockTableRecord blockDef = new BlockTableRecord();
                            BlockTable acBlockTable = (BlockTable)tr.GetObject(acDb.BlockTableId, OpenMode.ForRead);
                            foreach (ObjectId objId in acBlockTable)
                            {
                                blockDef = objId.GetObject(OpenMode.ForRead) as BlockTableRecord;
                                if (blockDef.Name == nam)
                                    break;
                            }

                            foreach (string tag in order)
                            {
                                AttributeDefinition attDef = GetTheCorrectAttributeDef(tag, tr, blockDef);

                                AttributeReference attRef = GetTheCorrectAttributeRef(tag, tr, blkRef);

                                if (attRef != null & attDef != null) // atts in both def and ref?
                                {
                                    Debug.WriteLine("atts in def: " + attDef.Tag);
                                    Debug.WriteLine("atts in ref: " + attRef.Tag);

                                    AttributeReference newAtt = (AttributeReference)attRef.Clone();

                                    var arId = blkRef.AttributeCollection.AppendAttribute(newAtt);

                                    tr.AddNewlyCreatedDBObject(newAtt, true);
                                    attRef.Erase();
                                }
                                else if (attRef == null & attDef != null) // add att to block reference
                                {
                                    Debug.WriteLine("atts NOT in ref: " + attDef.Tag);

                                    // add the attribute if it doesn't exist in 
                                    // the block reference but exists in the block definition
                                    var ad = attDef as AttributeDefinition;

                                    if (ad != null && !ad.Constant)
                                    {
                                        var ar = new AttributeReference();

                                        ar.SetAttributeFromBlock(ad, blkRef.BlockTransform);
                                        ar.TextString = ad.TextString;

                                        var arId = blkRef.AttributeCollection.AppendAttribute(ar);

                                        tr.AddNewlyCreatedDBObject(ar, true);
                                    }
                                }
                            }
                            break;
                    }
                    tr.Commit();
                }

            }
            Cls_BW_Main.frm_MMM_Host_Form.Show();
        }

        private static AttributeReference GetTheCorrectAttributeRef(string tag, Transaction acTrans, BlockReference blockRef)
        {
            AttributeReference attRef = null;

            AttributeCollection attCol = blockRef.AttributeCollection;

            foreach (ObjectId attId in attCol)
            {
                if (attId.IsErased)
                    continue;

                attRef = acTrans.GetObject(attId, OpenMode.ForWrite) as AttributeReference;

                if (attRef == null)
                    continue;

                if (attRef.Tag == tag)
                {
                    break;
                }
                else
                {
                    attRef = null;
                }
            }
            return attRef;
        }


        private static void SwapAttributeDefinitions()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            // Select a block reference
            PromptEntityOptions peo = new PromptEntityOptions("\nSelect a block reference:");
            peo.SetRejectMessage("\nMust be block reference...");
            peo.AddAllowedClass(typeof(BlockReference), true);

            PromptEntityResult per = ed.GetEntity(peo);

            if (per.Status != PromptStatus.OK)
                return;

            Transaction tr = db.TransactionManager.StartTransaction();

            using (doc.LockDocument())
            using (tr)
            {
                // Open the block reference and its connected BTR
                BlockReference br =
                  (BlockReference)tr.GetObject(
                    per.ObjectId,
                    OpenMode.ForRead
                  );

                BlockTableRecord btr =
                  (BlockTableRecord)tr.GetObject(
                    br.BlockTableRecord,
                    OpenMode.ForRead
                  );

                // If the block definition has attribute definitions...

                if (btr.HasAttributeDefinitions)
                {
                    // We want to find the first two

                    ObjectId attId1 = ObjectId.Null;
                    ObjectId attId2 = ObjectId.Null;

                    // Loop through and get them
                    // (We could clearly extend this to work differently,
                    // picking up attributes by name.)

                    foreach (ObjectId id in btr)
                    {
                        DBObject obj = tr.GetObject(id, OpenMode.ForRead);

                        if (obj is AttributeDefinition)
                        {
                            if (attId1 == ObjectId.Null)
                                attId1 = id;
                            else
                            {
                                attId2 = id;
                                break;
                            }
                        }
                    }

                    // If we have to attribute definitions...
                    if (attId1 != ObjectId.Null && attId2 != ObjectId.Null)
                    {
                        // Open the first and swap it with the second

                        AttributeDefinition ad =
                          (AttributeDefinition)tr.GetObject(
                            attId1,
                            OpenMode.ForWrite
                          );

                        ad.SwapIdWith(attId2, true, true);
                        ed.WriteMessage(
                          "\nOrder of first two attribute definitions swapped."
                        );
                    }

                    // Don't forget to commit the transaction

                    tr.Commit();
                }
            }
        }


        private static void SwapAttributeReferences()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            // Select a block reference
            PromptEntityOptions peo = new PromptEntityOptions("\nSelect a block reference:");
            peo.SetRejectMessage("\nMust be block reference...");
            peo.AddAllowedClass(typeof(BlockReference), true);

            PromptEntityResult per = ed.GetEntity(peo);

            if (per.Status != PromptStatus.OK)
                return;

            Transaction tr = db.TransactionManager.StartTransaction();

            using (doc.LockDocument())
            using (tr)
            {
                // This time we just open the block reference

                BlockReference br =
                  (BlockReference)tr.GetObject(
                    per.ObjectId,
                    OpenMode.ForRead
                  );

                // If the block reference has attribute references...

                if (br.AttributeCollection.Count > 1)
                {
                    // Once again we just want the first two
                    // (We could clearly extend this to work differently,
                    // picking up attributes by name.)

                    ObjectId attId1 = br.AttributeCollection[0];
                    ObjectId attId2 = br.AttributeCollection[1];

                    // Open the first and swap it with the second

                    AttributeReference ar =
                      (AttributeReference)tr.GetObject(
                        attId1,
                        OpenMode.ForWrite
                      );

                    ar.SwapIdWith(attId2, true, true);
                    ed.WriteMessage(
                      "\nOrder of first two attribute references swapped."
                    );

                }

                // Don't forget to commit the transaction
                tr.Commit();

            }
        }


        #endregion
                

        #region MatchAtts

        class AttsInfo
        {
            public string SITE { get; set; }
            public string BUILDING { get; set; }
            public string BLDG { get; set; }
            public string FLOOR { get; set; }
            public string CLOSETINFO { get; set; }

            public string ANTENNA { get; set; }
            public string MOUNT { get; set; }
            public string WRLSSPECIALFEED1 { get; set; }
            public string WAP { get; set; }
            public string LABEL1 { get; set; }


        }
        public static void MatchAtts()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            PromptEntityOptions peoSource = new PromptEntityOptions("\nSelect Source block reference:");
            peoSource.SetRejectMessage("\nMust be block reference...");
            peoSource.AddAllowedClass(typeof(BlockReference), true);

            PromptEntityOptions peoDest = new PromptEntityOptions("\nSelect Dest block reference:");
            peoDest.SetRejectMessage("\nMust be block reference...");
            peoDest.AddAllowedClass(typeof(BlockReference), true);

            PromptEntityResult perSource;
            PromptEntityResult perDest;

            bool loop = true;

            do
            {
                perSource = ed.GetEntity(peoSource);
                perDest = ed.GetEntity(peoDest);

                if (perSource.Status == PromptStatus.OK && perDest.Status == PromptStatus.OK)
                {
                    using (doc.LockDocument())
                    using (Transaction acTrans = db.TransactionManager.StartTransaction())
                    {
                        BlockReference blkSource = acTrans.GetObject(perSource.ObjectId, OpenMode.ForRead) as BlockReference;
                        BlockReference blkDest = acTrans.GetObject(perDest.ObjectId, OpenMode.ForWrite) as BlockReference;
                        AttributeCollection attColSource = blkSource.AttributeCollection;

                        AttsInfo d = new AttsInfo();

                        foreach (ObjectId id in attColSource)
                        {
                            Entity e = (Entity)acTrans.GetObject(id, OpenMode.ForRead);
                            if (e is AttributeReference)
                            {
                                AttributeReference a = (AttributeReference)e;

                                switch (a.Tag.ToUpper())
                                {
                                    case "SITE":
                                        d.SITE = a.TextString;
                                        break;
                                    case "BUILDING":
                                        d.BUILDING = a.TextString;
                                        break;
                                    case "BLDG":
                                        d.BLDG = a.TextString;
                                        break;
                                    case "FLOOR":
                                        d.FLOOR = a.TextString;
                                        break;
                                    case "CLOSETINFO":
                                        d.CLOSETINFO = a.TextString;
                                        break;

                                    case "ANTENNA":
                                        d.ANTENNA = a.TextString;
                                        break;
                                    case "MOUNT":
                                        d.MOUNT = a.TextString;
                                        break;
                                    case "WRLSSPECIALFEED1":
                                        d.WRLSSPECIALFEED1 = a.TextString;
                                        break;
                                    case "WAP":
                                        d.WAP = a.TextString;
                                        break;
                                    case "LABEL1":
                                        d.LABEL1 = a.TextString;
                                        break;
                                }
                            }
                        }

                        AttributeCollection attColDest = blkDest.AttributeCollection;

                        foreach (ObjectId id in attColDest)
                        {
                            Entity e = (Entity)acTrans.GetObject(id, OpenMode.ForWrite);
                            if (e is AttributeReference)
                            {
                                AttributeReference a = (AttributeReference)e;

                                switch (a.Tag.ToUpper())
                                {
                                    case "SITE":
                                        a.TextString = d.SITE;
                                        break;
                                    case "BUILDING":
                                        a.TextString = d.BUILDING;
                                        break;
                                    case "BLDG":
                                        a.TextString = d.BLDG;
                                        break;
                                    case "FLOOR":
                                        a.TextString = d.FLOOR;
                                        break;
                                    case "CLOSETINFO":
                                        a.TextString = d.CLOSETINFO;
                                        break;


                                    case "ANTENNA":
                                        a.TextString = d.ANTENNA;
                                        break;
                                    case "MOUNT":
                                        a.TextString = d.MOUNT;
                                        break;
                                    case "WRLSSPECIALFEED1":
                                        a.TextString = d.WRLSSPECIALFEED1;
                                        break;
                                    case "DISPLAYCODE":
                                        a.TextString = d.WRLSSPECIALFEED1;
                                        break;
                                    case "APNUMBER":
                                        a.TextString = d.WAP;
                                        break;
                                    case "WAP":
                                        a.TextString = d.WAP;
                                        break;
                                    case "LABEL1":
                                        a.TextString = d.LABEL1;
                                        break;

                                }
                            }
                        }
                        acTrans.Commit();
                    }
                }
                else
                {
                    loop = false;
                }
            } while (loop == true);
        }

        #endregion
        


        /// <summary>
        /// does not work with dynamic blocks as is
        /// </summary>
        public class Commands
        {
            // Set up some formatting constants for the table

            const double colWidth = 15.0;
            const double rowHeight = 3.0;
            const double textHeight = 1.0;

            const CellAlignment cellAlign =
              CellAlignment.MiddleCenter;

            // Helper function to set text height
            // and alignment of specific cells,
            // as well as inserting the text

            static public void SetCellText(Table tb, int row, int col, string value)
            {
                tb.Cells[row, col].Alignment = cellAlign;
                tb.Cells[row, col].TextHeight = textHeight;
                tb.Cells[row, col].Value = value;
            }


            [CommandMethod("BAT")]
            static public void BlockAttributeTable()
            {
                Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
                Editor ed = doc.Editor;

                // Ask for the name of the block to find
                PromptStringOptions opt =
                  new PromptStringOptions(
                    "\nEnter name of block to list: "
                  );

                PromptResult pr = ed.GetString(opt);

                if (pr.Status == PromptStatus.OK)
                {
                    string blockToFind =
                      pr.StringResult.ToUpper();

                    bool embed = false;
                    
                    // Ask whether to embed or link the data
                    PromptKeywordOptions pko =
                      new PromptKeywordOptions(
                        "\nEmbed or link the attribute values: "
                      );


                    pko.AllowNone = true;
                    pko.Keywords.Add("Embed");
                    pko.Keywords.Add("Link");
                    pko.Keywords.Default = "Embed";

                    PromptResult pkr = ed.GetKeywords(pko);


                    if (pkr.Status == PromptStatus.None || pkr.Status == PromptStatus.OK)
                    {
                        if (pkr.Status == PromptStatus.None || pkr.StringResult == "Embed")
                            embed = true;
                        else
                            embed = false;
                    }


                    Transaction tr = doc.TransactionManager.StartTransaction();

                    using (doc.LockDocument())
                    using (tr)
                    {
                        // Let's check the block exists
                        BlockTable bt =
                          (BlockTable)tr.GetObject(
                            doc.Database.BlockTableId,
                            OpenMode.ForRead
                          );


                        if (!bt.Has(blockToFind))
                        {
                            ed.WriteMessage(
                              "\nBlock "
                              + blockToFind
                              + " does not exist."
                            );
                        }
                        else
                        {
                            // And go through looking for
                            // attribute definitions

                            StringCollection colNames = new StringCollection();
                            
                            BlockTableRecord bd =
                              (BlockTableRecord)tr.GetObject(
                                bt[blockToFind],
                                OpenMode.ForRead
                              );

                            foreach (ObjectId adId in bd)
                            {
                                DBObject adObj =
                                  tr.GetObject(
                                    adId,
                                    OpenMode.ForRead
                                  );


                                // For each attribute definition we find...
                                AttributeDefinition ad = adObj as AttributeDefinition;

                                if (ad != null)
                                {
                                    // ... we add its name to the list
                                    colNames.Add(ad.Tag);
                                }
                            }

                            if (colNames.Count == 0)
                            {
                                ed.WriteMessage(
                                  "\nThe block "
                                  + blockToFind
                                  + " contains no attribute definitions."
                                );
                            }
                            else
                            {
                                // Ask the user for the insertion point
                                // and then create the table

                                PromptPointResult ppr =
                                  ed.GetPoint(
                                    "\nEnter table insertion point: "
                                  );


                                if (ppr.Status == PromptStatus.OK)
                                {
                                    Table tb = new Table();

                                    tb.TableStyle = db.Tablestyle;
                                    tb.SetSize(1, colNames.Count);
                                    //tb.NumRows = 1;
                                    //tb.NumColumns = colNames.Count;
                                    tb.SetRowHeight(rowHeight);
                                    tb.SetColumnWidth(colWidth);
                                    tb.Position = ppr.Value;


                                    // Let's add our column headings
                                    for (int i = 0; i < colNames.Count; i++)
                                    {
                                        SetCellText(tb, 0, i, colNames[i]);
                                    }


                                    // Now let's search for instances of
                                    // our block in the modelspace

                                    BlockTableRecord ms =
                                      (BlockTableRecord)tr.GetObject(
                                        bt[BlockTableRecord.ModelSpace],
                                        OpenMode.ForRead
                                      );


                                    int rowNum = 1;

                                    foreach (ObjectId objId in ms)
                                    {
                                        DBObject obj =
                                          tr.GetObject(
                                            objId,
                                            OpenMode.ForRead
                                          );

                                        BlockReference br = obj as BlockReference;

                                        if (br != null)
                                        {
                                            BlockTableRecord btr =
                                              (BlockTableRecord)tr.GetObject(
                                                br.BlockTableRecord,
                                                OpenMode.ForRead
                                              );

                                            using (btr)
                                            {
                                                if (btr.Name.ToUpper() == blockToFind)
                                                {
                                                    // We have found one of our blocks,
                                                    // so add a row for it in the table

                                                    tb.InsertRows(
                                                      rowNum,
                                                      rowHeight,
                                                      1
                                                    );


                                                    // Assume that the attribute refs
                                                    // follow the same order as the
                                                    // attribute defs in the block

                                                    int attNum = 0;

                                                    foreach (ObjectId arId in br.AttributeCollection)
                                                    {
                                                        DBObject arObj =
                                                          tr.GetObject(
                                                            arId,
                                                            OpenMode.ForRead
                                                          );

                                                        AttributeReference ar = arObj as AttributeReference;

                                                        if (ar != null)
                                                        {
                                                            // Embed or link the values

                                                            string strCell;

                                                            if (embed)
                                                            {
                                                                strCell = ar.TextString;
                                                            }
                                                            else
                                                            {
                                                                string strArId = arId.ToString();

                                                                strArId =
                                                                  strArId.Trim(
                                                                    new char[] { '(', ')' }
                                                                  );

                                                                strCell =
                                                                  "%<\\AcObjProp Object("
                                                                    + "%<\\_ObjId "
                                                                    + strArId
                                                                    + ">%).TextString>%";
                                                            }

                                                            SetCellText(
                                                              tb,
                                                              rowNum,
                                                              attNum,
                                                              strCell
                                                            );

                                                        }

                                                        attNum++;

                                                    }

                                                    rowNum++;

                                                }

                                            }

                                        }

                                    }

                                    tb.GenerateLayout();


                                    ms.UpgradeOpen();

                                    ms.AppendEntity(tb);

                                    tr.AddNewlyCreatedDBObject(tb, true);

                                    tr.Commit();

                                }

                            }

                        }

                    }

                }

            }

        }

   




}
}

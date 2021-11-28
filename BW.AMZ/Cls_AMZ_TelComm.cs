using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

namespace MyFirstProject.BW.AMZ
{
    public class Cls_AMZ_TelComm
    {



        public static readonly Cls_AMZ_TelComm_Atts AMZ_CurrTelCommAtts = new Cls_AMZ_TelComm_Atts();
        public static readonly List<Cls_AMZ_TelComm_Atts> AMZ_LstTelCommAtts = new List<Cls_AMZ_TelComm_Atts>();

        // autocad table
        public static Cls_AMZ_TelComm_IdfTableCounts AMZ_ClsTelCommBomCounts = new BW.AMZ.Cls_AMZ_TelComm_IdfTableCounts();
        public static readonly List<Cls_AMZ_TelComm_IdfTableCounts> lstIDF = new List<Cls_AMZ_TelComm_IdfTableCounts>();

        public static readonly List<Cls_AMZ_IdfCameras> AMZ_LstIdfCameras = new List<Cls_AMZ_IdfCameras>();
        public static readonly List<ClsAPCFilterList> LstClsAPCFilterListTelComm = new List<ClsAPCFilterList>();



        public static readonly List<ClsAPCFilterList> AMZ_LstClsAPCFilterList = new List<ClsAPCFilterList>();


        public static List<BW.AMZ.Cls_AMZ_TelComm_Atts> FilterIDFsTelComm = new List<BW.AMZ.Cls_AMZ_TelComm_Atts>();


        public static readonly List<Cls_AMZ_IdfAreas> AMZ_LstIdfAreas = new List<Cls_AMZ_IdfAreas>();

        public class ClsAPCFilterList
        {
            public string Count { get; set; }
            public string ACP { get; set; }
        }


        public static readonly List<ClsNetworkFilterList> AMZ_LstClsNetworkFilterList = new List<ClsNetworkFilterList>();
        public class ClsNetworkFilterList
        {
            public string Count { get; set; }
            public string Isc { get; set; }
        }



        public static void AMZ_InsertTable_BOM(string IDF)
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            PromptPointResult pr = ed.GetPoint("\nEnter table insertion point: ");

            if (pr.Status == PromptStatus.OK)
            {
                Autodesk.AutoCAD.DatabaseServices.Table tb = new Autodesk.AutoCAD.DatabaseServices.Table();

                tb.TableStyle = db.Tablestyle;
                tb.SetSize(16, 3);
                tb.SetRowHeight(34.5);
                tb.SetColumnWidth(65);
                tb.Position = pr.Value;


                CellRange mcells = CellRange.Create(tb, 1, 1, 1, 2);
                tb.MergeCells(mcells);
                Cell mc = tb.Cells[1, 0];
                mc.Contents.Add();

                mcells = CellRange.Create(tb, 15, 0, 15, 2);
                tb.MergeCells(mcells);
                mc = tb.Cells[16, 0];
                mc.Contents.Add();




                string[,] str = new string[16, 3]; // rows, cols

                str[0, 0] = "WORKSTATION DEVICE\r\nSUMMARY " + IDF;
                str[0, 1] = "";
                str[0, 2] = "";

                str[1, 0] = "";
                str[1, 1] = "FLOOR";
                str[1, 2] = "";

                str[2, 0] = "DEVICE\r\nTYPE";
                str[2, 1] = "DEVICE\r\nQTY";
                str[2, 2] = "PORT\r\nQTY";

                str[3, 0] = "";
                str[3, 1] = AMZ_ClsTelCommBomCounts.VP_Qty_Dev.ToString();
                str[3, 2] = "=B4"; // AMZ_ClsTelCommBomCounts.VP_Qty.ToString();

                str[4, 0] = "";
                str[4, 1] = AMZ_ClsTelCommBomCounts.Drop1_Qty_Dev.ToString();
                str[4, 2] = "=B5"; //AMZ_ClsTelCommBomCounts.Drop1_Qty.ToString();

                str[5, 0] = "";
                str[5, 1] = AMZ_ClsTelCommBomCounts.Drop2_Qty_Dev.ToString();
                str[5, 2] = "=B6*2"; //AMZ_ClsTelCommBomCounts.Drop2_Qty.ToString(); 

                str[6, 0] = "";
                str[6, 1] = AMZ_ClsTelCommBomCounts.Drop3_Qty_Dev.ToString();
                str[6, 2] = "=B7*3"; //AMZ_ClsTelCommBomCounts.Drop3_Qty.ToString();

                str[7, 0] = "";
                str[7, 1] = AMZ_ClsTelCommBomCounts.Drop4_Qty_Dev.ToString();
                str[7, 2] = "=B8*4"; //AMZ_ClsTelCommBomCounts.Drop4_Qty.ToString();

                str[8, 0] = "";
                str[8, 1] = AMZ_ClsTelCommBomCounts.Drop5_Qty_Dev.ToString();
                str[8, 2] = "=B9*5";  //AMZ_ClsTelCommBomCounts.Drop5_Qty.ToString();

                str[9, 0] = "";
                str[9, 1] = AMZ_ClsTelCommBomCounts.Drop6_Qty_Dev.ToString();
                str[9, 2] = "=B10*6"; //AMZ_ClsTelCommBomCounts.Drop6_Qty.ToString();

                str[10, 0] = "";
                str[10, 1] = AMZ_ClsTelCommBomCounts.TimeClock_Qty_Dev.ToString();
                str[10, 2] = "=B11"; //AMZ_ClsTelCommBomCounts.TimeClock_Qty.ToString();

                str[11, 0] = "SUB\r\nTOTAL";
                str[11, 1] = "=Sum(B4:B11)";
                str[11, 2] = "=Sum(C4:C11)";

                int camCnt = AMZ_LstIdfCameras.Where(x => x.idf_Number == IDF).Count();
                str[12, 0] = "CAM";
                str[12, 1] = camCnt.ToString();
                str[12, 2] = "=B13";

                str[13, 0] = "WAP";
                str[13, 1] = "0";
                str[13, 2] = "=B14";

                str[14, 0] = "TOTAL";
                str[14, 1] = "=Sum(B12:B14)";
                str[14, 2] = "=Sum(C12:C14)";

                str[15, 0] = "NOTE:\r\nSUMMARY IS REFERENCE\r\nONLY AND SHALL NOT BE\r\nUTILIZED FOR BIDDING\r\nPURPOSES." +
                " INFORMATION\r\nDESCRIBED ON THE FLOOR\r\nPLANS WITH ASSOCIATED\r\nSHEET AND FLAG NOTES, " +
                "\r\nINCLUDING REFERENCES\r\nTO OTHER DRAWING\r\nSHEETS, SHALL TAKE\r\nPRECEDENCE.";
                str[15, 1] = ".";
                str[15, 2] = ".";


                for (int rows = 0; rows < 16; rows++)
                {
                    for (int cols = 0; cols < 3; cols++)
                    {
                        tb.Cells[rows, cols].TextHeight = 9;
                        tb.Cells[rows, cols].TextString = str[rows, cols];
                        tb.Cells[rows, cols].Alignment = CellAlignment.MiddleCenter;
                    }
                }

                tb.Rows[0].Height = 44;
                tb.Rows[15].Height = 200;

                tb.GenerateLayout();

                using (doc.LockDocument())
                using (Transaction tr = doc.TransactionManager.StartTransaction())
                {
                    BlockTable bt = (BlockTable)tr.GetObject(doc.Database.BlockTableId, OpenMode.ForRead);
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                    Cell second;

                    //
                    // Loop through the blocks in the drawing, creating rows
                    foreach (var id in bt)
                    {
                        var btrTblBlks = (BlockTableRecord)tr.GetObject(id, OpenMode.ForRead);

                        // Only care about user-insertable blocks

                        if (!btrTblBlks.IsLayout && !btrTblBlks.IsAnonymous)
                        {
                            if (btrTblBlks.Name == "TellCommTableBlock_1")
                            {
                                // The second will contain a thumbnail of the block
                                second = tb.Cells[3, 0];
                                second.BlockTableRecordId = id;
                            }
                            if (btrTblBlks.Name == "TellCommTableBlock_2")
                            {
                                // The second will contain a thumbnail of the block
                                second = tb.Cells[4, 0];
                                second.BlockTableRecordId = id;
                            }
                            if (btrTblBlks.Name == "TellCommTableBlock_3")
                            {
                                // The second will contain a thumbnail of the block
                                second = tb.Cells[5, 0];
                                second.BlockTableRecordId = id;
                            }
                            if (btrTblBlks.Name == "TellCommTableBlock_4")
                            {
                                // The second will contain a thumbnail of the block
                                second = tb.Cells[6, 0];
                                second.BlockTableRecordId = id;
                            }
                            if (btrTblBlks.Name == "TellCommTableBlock_5")
                            {
                                // The second will contain a thumbnail of the block
                                second = tb.Cells[7, 0];
                                second.BlockTableRecordId = id;
                            }
                            if (btrTblBlks.Name == "TellCommTableBlock_6")
                            {
                                // The second will contain a thumbnail of the block
                                second = tb.Cells[8, 0];
                                second.BlockTableRecordId = id;
                            }
                            if (btrTblBlks.Name == "TellCommTableBlock_7")
                            {
                                // The second will contain a thumbnail of the block
                                second = tb.Cells[9, 0];
                                second.BlockTableRecordId = id;
                            }
                            if (btrTblBlks.Name == "TellCommTableBlock_8")
                            {
                                // The second will contain a thumbnail of the block
                                second = tb.Cells[10, 0];
                                second.BlockTableRecordId = id;
                            }
                        }
                    }
                    //


                    btr.AppendEntity(tb);
                    tr.AddNewlyCreatedDBObject(tb, true);
                    tr.Commit();
                }
            }
        }
        

        public static void AMZ_TelCommBomCount(string idf)
        {
            lstIDF.Clear();

            AMZ_ClsTelCommBomCounts = new BW.AMZ.Cls_AMZ_TelComm_IdfTableCounts();

            //       Cls_AMZ_Main.AMZ_ClsTelCommBomCounts.VP = Cls_AMZ_Main.AMZ_LstTelCommAtts.Where(x => x.BlockName == "Drop" & x.).Sum(item => int.Parse(item.Data));
            AMZ_ClsTelCommBomCounts.Drop1_Qty_Dev = AMZ_LstTelCommAtts.Where(x => x.BlockName == "Drop" & x.IDF == idf & x.Num == "1").Count();
            AMZ_ClsTelCommBomCounts.Drop2_Qty_Dev = AMZ_LstTelCommAtts.Where(x => x.BlockName == "Drop" & x.IDF == idf & x.Num == "2").Count();
            AMZ_ClsTelCommBomCounts.Drop3_Qty_Dev = AMZ_LstTelCommAtts.Where(x => x.BlockName == "Drop" & x.IDF == idf & x.Num == "3").Count();
            AMZ_ClsTelCommBomCounts.Drop4_Qty_Dev = AMZ_LstTelCommAtts.Where(x => x.BlockName == "Drop" & x.IDF == idf & x.Num == "4").Count();
            AMZ_ClsTelCommBomCounts.Drop5_Qty_Dev = AMZ_LstTelCommAtts.Where(x => x.BlockName == "Drop" & x.IDF == idf & x.Num == "5").Count();
            AMZ_ClsTelCommBomCounts.Drop6_Qty_Dev = AMZ_LstTelCommAtts.Where(x => x.BlockName == "Drop" & x.IDF == idf & x.Num == "6").Count();
            AMZ_ClsTelCommBomCounts.TimeClock_Qty_Dev = AMZ_LstTelCommAtts.Where(x => x.BlockName == "IP WALL CLOCK" & x.IDF == idf).Count();

            //       Cls_AMZ_Main.AMZ_ClsTelCommBomCounts.VP = Cls_AMZ_Main.AMZ_LstTelCommAtts.Where(x => x.BlockName == "Drop" & x.).Sum(item => int.Parse(item.Data));
            AMZ_ClsTelCommBomCounts.Drop1_Qty = AMZ_LstTelCommAtts.Where(x => x.BlockName == "Drop" & x.IDF == idf & x.Num == "1").Sum(item => int.Parse(item.Num));
            AMZ_ClsTelCommBomCounts.Drop2_Qty = AMZ_LstTelCommAtts.Where(x => x.BlockName == "Drop" & x.IDF == idf & x.Num == "2").Sum(item => int.Parse(item.Num));
            AMZ_ClsTelCommBomCounts.Drop3_Qty = AMZ_LstTelCommAtts.Where(x => x.BlockName == "Drop" & x.IDF == idf & x.Num == "3").Sum(item => int.Parse(item.Num));
            AMZ_ClsTelCommBomCounts.Drop4_Qty = AMZ_LstTelCommAtts.Where(x => x.BlockName == "Drop" & x.IDF == idf & x.Num == "4").Sum(item => int.Parse(item.Num));
            AMZ_ClsTelCommBomCounts.Drop5_Qty = AMZ_LstTelCommAtts.Where(x => x.BlockName == "Drop" & x.IDF == idf & x.Num == "5").Sum(item => int.Parse(item.Num));
            AMZ_ClsTelCommBomCounts.Drop6_Qty = AMZ_LstTelCommAtts.Where(x => x.BlockName == "Drop" & x.IDF == idf & x.Num == "6").Sum(item => int.Parse(item.Num));
            AMZ_ClsTelCommBomCounts.TimeClock_Qty = AMZ_LstTelCommAtts.Where(x => x.BlockName == "IP WALL CLOCK" & x.IDF == idf).Count();

            lstIDF.Add(AMZ_ClsTelCommBomCounts);

        }
        

        public static void AMZ_SelectBlocksTelComm()
        {
            AMZ_LstTelCommAtts.Clear();

            Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            TypedValue[] acTypValAr = new TypedValue[7];
            acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "INSERT"), 0);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.Operator, "<OR"), 1);

            acTypValAr.SetValue(new TypedValue((int)DxfCode.BlockName, "Drop"), 2);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.BlockName, "Ip Wall Clock"), 3);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.BlockName, "TMC"), 4);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.BlockName, "Video Wall"), 5);

            acTypValAr.SetValue(new TypedValue((int)DxfCode.Operator, "OR>"), 6);

            SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);

            PromptSelectionResult acSSPrompt = acDocEd.GetSelection(acSelFtr);

            if (acSSPrompt.Status == PromptStatus.OK)
            {
                AMZ_FillTelCommList(acSSPrompt);
            }
            else
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
            }
        }
        private static void AMZ_FillTelCommList(PromptSelectionResult acSSPrompt)
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

            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
            Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            using (doc.LockDocument())
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                foreach (ObjectId id1 in ids)
                {
                    BlockReference blkRef = (BlockReference)transaction.GetObject(id1, OpenMode.ForRead);

                    var AMZ_CurrTelCommAtts = new Cls_AMZ_TelComm_Atts();
                    AMZ_CurrTelCommAtts.BlockName = blkRef.Name;
                    AMZ_CurrTelCommAtts.Handle = blkRef.Handle;
                    AMZ_CurrTelCommAtts.InsertionPtOfBlock = blkRef.Position;
                    AMZ_LstTelCommAtts.Add(AMZ_CurrTelCommAtts);

                    AttributeCollection attCol = blkRef.AttributeCollection;

                    foreach (ObjectId attId in attCol)
                    {
                        AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForRead);

                        switch (attRef.Tag)
                        {
                            case "IDF":
                                AMZ_CurrTelCommAtts.IDF = attRef.TextString;
                                break;
                            case "IDF#":
                                AMZ_CurrTelCommAtts.IDF = attRef.TextString;
                                break;
                            case "#":
                                AMZ_CurrTelCommAtts.Num = attRef.TextString;
                                break;
                            case "NETDEV1":
                                AMZ_CurrTelCommAtts.NetDev1 = attRef.TextString;
                                break;
                            case "NETDEV2":
                                AMZ_CurrTelCommAtts.NetDev2 = attRef.TextString;
                                break;
                            case "DEVINFO":
                                AMZ_CurrTelCommAtts.DevInfo = attRef.TextString;
                                break;
                            case "MOUNT":
                                AMZ_CurrTelCommAtts.Mount = attRef.TextString;
                                break;
                            case "RF6":
                                AMZ_CurrTelCommAtts.RF6 = attRef.TextString;
                                break;
                            case "AFF":
                                AMZ_CurrTelCommAtts.AFF = attRef.TextString;
                                break;
                            case "DATA":
                                AMZ_CurrTelCommAtts.Data = attRef.TextString;
                                break;
                            default:
                                break;
                        }
                    }
                }
                transaction.Commit();

                // LstAtts_DevTag.OrderBy(x => x.BlockName).ThenBy(x => x.DoorNumber);

                // blockname not null
                AMZ_LstTelCommAtts.Sort((x, y) => x.BlockName.CompareTo(y.BlockName));
            }
        }


        public static void GetLstFilterAPCsTelComm()
        {
            var DistinctClosetNames = AMZ_LstTelCommAtts.Select(x => x.IDF).Distinct().ToList();
            LstClsAPCFilterListTelComm.Clear();

            foreach (string s in DistinctClosetNames)
            {
                if (s != null)
                {
                    string txt = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.Panel == s).Count().ToString();
                    ClsAPCFilterList l = new ClsAPCFilterList();
                    l.Count = txt;
                    l.ACP = s;
                    LstClsAPCFilterListTelComm.Add(l);
                }
            }

            LstClsAPCFilterListTelComm.Sort((x, y) => x.ACP.CompareTo(y.ACP));
        }


        #region Drops Table
        
        public static readonly List<Drops> lstDrps = new List<Drops>();

        public class Drops
        {
            public int amzDropTtl { get; set; }
            public int camDropTtl { get; set; }
            public int wapDropTtl { get; set; }
            public int allDropTtl { get; set; }


        }


        public static readonly List<DropsRows> lstDropsRows = new List<DropsRows>();
        
        public class DropsRows
        {
            public int RowNum { get; set; }
            public string PanelNum { get; set; }
            public string Ports { get; set; }
            public string Cable { get; set; }
            public string Dest { get; set; }
        }

        public static void AMZ_ModifyTable_Ports()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            if (doc == null)
                return;

            var ed = doc.Editor;
            
            var per = ed.GetEntity("\nSelect table");
            if (per.Status != PromptStatus.OK)
                return;

            using (doc.LockDocument())
            using (var tr = doc.TransactionManager.StartTransaction())
            {
                var tb = tr.GetObject(per.ObjectId, OpenMode.ForRead) as Table;

                if (tb != null)
                {                    
                    tb.UpgradeOpen();

                    if (tb.Rows.Count > 4)
                    {
                        tb.DeleteRows(4, tb.Rows.Count - 1);
                    }

                    // copy text hiegt from existing cell
                    double? txtHt = tb.Rows[3].TextHeight;
                    
                    foreach (DropsRows d in lstDropsRows)
                    {
                        AddRow(tb, txtHt, d);
                    }
                    
                    // merge cells
                    var disct = lstDropsRows.GroupBy(x => x.PanelNum);
                    foreach (var pnlR in disct)
                    {
                        CellRange mcells;
                        mcells = CellRange.Create(tb, pnlR.First().RowNum, 0, pnlR.Last().RowNum, 0); // col 1
                        tb.MergeCells(mcells);
                        mcells = CellRange.Create(tb, pnlR.First().RowNum, 1, pnlR.Last().RowNum, 1); // col 2
                        tb.MergeCells(mcells);
                    }

                }

                // Commit the transaction
                tr.Commit();
            }
        }


        private static void AddRow(Table tb, double? txtHt, DropsRows d)
        {
            tb.InsertRows(tb.Rows.Count, 0.1, 1);
            tb.Cells[tb.Rows.Count - 1, 0].Value = d.PanelNum;
            tb.Cells[tb.Rows.Count - 1, 1].Value = "-";
            tb.Cells[tb.Rows.Count - 1, 2].Value = d.Ports;
            tb.Cells[tb.Rows.Count - 1, 3].Value = d.Cable;
            tb.Cells[tb.Rows.Count - 1, 4].Value = d.Dest;

            tb.Cells[tb.Rows.Count - 1, 0].TextHeight = txtHt;
            tb.Cells[tb.Rows.Count - 1, 1].TextHeight = txtHt;
            tb.Cells[tb.Rows.Count - 1, 2].TextHeight = txtHt;
            tb.Cells[tb.Rows.Count - 1, 3].TextHeight = txtHt;
            tb.Cells[tb.Rows.Count - 1, 4].TextHeight = txtHt;

            tb.Cells[tb.Rows.Count - 1, 0].Alignment = CellAlignment.MiddleCenter;
            tb.Cells[tb.Rows.Count - 1, 1].Alignment = CellAlignment.MiddleCenter;
            tb.Cells[tb.Rows.Count - 1, 2].Alignment = CellAlignment.MiddleCenter;
            tb.Cells[tb.Rows.Count - 1, 3].Alignment = CellAlignment.MiddleCenter;
            tb.Cells[tb.Rows.Count - 1, 4].Alignment = CellAlignment.MiddleCenter;
        }

        public static void BtnGetDataFromTable_Click_Sub()
        {
            Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            PromptEntityOptions promptEntity = new PromptEntityOptions("Select Table");

            PromptEntityResult res = acDocEd.GetEntity(promptEntity);

            if (res.Status == PromptStatus.OK)
            {
                using (doc.LockDocument())
                using (Transaction tr = doc.TransactionManager.StartTransaction())
                {
                    Table table = (Table)tr.GetObject(res.ObjectId, OpenMode.ForRead);

                    Cls_AMZ_TelComm.lstDrps.Clear();
                    Cls_AMZ_TelComm.Drops drps = new Cls_AMZ_TelComm.Drops();

                    drps.amzDropTtl = int.Parse(table.Cells[11, 2].Value.ToString());
                    drps.camDropTtl = int.Parse(table.Cells[12, 2].Value.ToString());
                    drps.wapDropTtl = int.Parse(table.Cells[13, 2].Value.ToString());
                    drps.allDropTtl = int.Parse(table.Cells[14, 2].Value.ToString());

                    Cls_AMZ_TelComm.lstDrps.Add(drps);
                }
            }
            else
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
            }
        }


        public static void BtnGenerateRowsForDropTable_Click_Sub(bool LastItmsOnNewSwitch, bool CamsFirst)
        {
            rowCntr = 4; //4 rows exist index = 0

            Cls_AMZ_TelComm.lstDropsRows.Clear();

            int strt = 1;
            int end = -1;

            decimal numSwitches;
            decimal Switches = (decimal)Cls_AMZ_TelComm.lstDrps[0].allDropTtl / 45;

            string x = Switches.ToString();
            int value;

            if (int.TryParse(x, out value))
            {
                numSwitches = Switches + 1;
            }
            else
            {
                numSwitches = Math.Ceiling(Switches);
            }
            
            if (Cls_AMZ_TelComm.lstDrps[0].allDropTtl - 2 >= (numSwitches * 45))
            {
                numSwitches++;
            }

            bool futureConn = false;

            int wapsLeft = Cls_AMZ_TelComm.lstDrps[0].wapDropTtl;
            decimal SwitchesForWap = (decimal)Cls_AMZ_TelComm.lstDrps[0].wapDropTtl / 45;
            decimal numSwitchesForWap = Math.Ceiling(SwitchesForWap);
            decimal portsLeftAfterWaps = numSwitchesForWap * 45;

            int amzLeft = Cls_AMZ_TelComm.lstDrps[0].amzDropTtl;
            decimal portsLeftAfterAmzs = 0;

            int camLeft = Cls_AMZ_TelComm.lstDrps[0].camDropTtl;
            decimal portsLeftAfterCams = 0;

            int cntrCurrSwitch = 1;
            int AddSwitch = 1;

            Spare46 = false;
            addedCamSpares = false;
            endDoneCams = false;
            endDoneAmz = false;

            for (int s = 1; s < numSwitches + AddSwitch; s++)
            {
                if (CamsFirst)
                {             
                    AddWaps(CamsFirst, ref strt, ref end, ref wapsLeft, numSwitchesForWap, ref portsLeftAfterWaps, ref cntrCurrSwitch);
                                   
                    AddCams(CamsFirst, LastItmsOnNewSwitch, ref strt, ref end, ref portsLeftAfterWaps, ref camLeft, ref portsLeftAfterCams, amzLeft, ref cntrCurrSwitch, ref AddSwitch);
                                   
                    AddAmzs(CamsFirst, ref strt, ref end, ref portsLeftAfterCams, ref amzLeft, ref portsLeftAfterAmzs, ref cntrCurrSwitch);
                           
                    AddFuts(CamsFirst, LastItmsOnNewSwitch, ref strt, ref end, ref futureConn, wapsLeft, camLeft, amzLeft, ref portsLeftAfterAmzs, cntrCurrSwitch);
                }
                else
                {
                    Spare46 = true;

                    AddWaps(ref strt, ref end, ref wapsLeft, numSwitchesForWap, ref portsLeftAfterWaps, ref cntrCurrSwitch);

                    AddAmzs(LastItmsOnNewSwitch, ref strt, ref end, ref portsLeftAfterWaps, ref amzLeft, ref portsLeftAfterAmzs, camLeft, ref cntrCurrSwitch, ref AddSwitch);

                    AddCams(ref strt, ref end, ref portsLeftAfterAmzs, ref camLeft, ref portsLeftAfterCams, ref cntrCurrSwitch);

                    AddFuts(ref strt, ref end, ref futureConn, wapsLeft, amzLeft, camLeft, ref portsLeftAfterCams, cntrCurrSwitch);
                }
            }
        }


        #region new table, waps, cctv, amz

        static bool Spare46;
        static bool addedCamSpares;

        private static void AddWaps(bool CamsFirst, ref int strt, ref int end, ref int wapsLeft, decimal numSwitchesForWap, ref decimal portsLeftAfterWaps, ref int cntrCurrSwitch)
        { 
            if (wapsLeft > 0)
            {
                if (numSwitchesForWap == 1)
                {
                    end = Cls_AMZ_TelComm.lstDrps[0].wapDropTtl;
                    AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "GROUND FLOOR WIRELESS ACCESS POINT (WAP) CABLING");
                    wapsLeft = wapsLeft - Cls_AMZ_TelComm.lstDrps[0].wapDropTtl;
                    portsLeftAfterWaps = portsLeftAfterWaps - Cls_AMZ_TelComm.lstDrps[0].wapDropTtl;

                    if (end == 45)
                    {
                        AddRow(46, 46, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "IDF UPS NMC CONNECTION");
                        AddRow(47, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                        Spare46 = true;
                        cntrCurrSwitch++;
                    }
                }
                else
                {
                    do
                    { // fill as many switches as needed                      
                        strt = 1;
                        end = 45;
                        AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "GROUND FLOOR WIRELESS ACCESS POINT (WAP) CABLING");
                        if (!Spare46)
                        {
                            AddRow(46, 46, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "IDF UPS NMC CONNECTION");
                            AddRow(47, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                            Spare46 = true;
                        }
                        else
                        {
                            AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                        }
                        wapsLeft = wapsLeft - 45;
                        portsLeftAfterWaps = portsLeftAfterWaps - 45;
                        cntrCurrSwitch++;
                    } while (cntrCurrSwitch < numSwitchesForWap);

                    strt = 1; // next partial switch
                    end = wapsLeft;
                    AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "GROUND FLOOR WIRELESS ACCESS POINT (WAP) CABLING");
                    wapsLeft = wapsLeft - end;
                    portsLeftAfterWaps = portsLeftAfterWaps - end;
                }
            }
        }

        static bool endDoneCams;

        private static void AddCams(bool CamsFirst, bool LastItmsOnNewSwitch, ref int strt, ref int end, ref decimal portsLeftAfterWaps, ref int camLeft, ref decimal portsLeftAfterCams, int amzLeft, ref int cntrCurrSwitch, ref int AddSwitch)
        {
            if (camLeft > 0 | LastItmsOnNewSwitch)
            {
                if (portsLeftAfterWaps > 0)
                {
                    if (camLeft > portsLeftAfterWaps)
                    {
                        strt = end + 1;
                        end = end + (int)portsLeftAfterWaps;
                        AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "CCTV CAMERA CABLING CONNECTIONS");
                        camLeft = camLeft - (int)portsLeftAfterWaps;
                        portsLeftAfterWaps = 0;
                    }
                    else if (camLeft > 0)
                    {
                        strt = end + 1;
                        end = end + Cls_AMZ_TelComm.lstDrps[0].camDropTtl;
                        AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "CCTV CAMERA CABLING CONNECTIONS");
                        portsLeftAfterCams = portsLeftAfterWaps - camLeft;
                        camLeft = 0;
                    }

                    if (!endDoneCams & end == 45)
                    {
                        if (!Spare46)
                        {
                            AddRow(46, 46, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "IDF UPS NMC CONNECTION");
                            AddRow(47, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                            Spare46 = true;
                        }
                        else
                        {
                            AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                            endDoneCams = true;
                        }
                        cntrCurrSwitch++;
                    }
                }
                if (camLeft > 0 && camLeft < 45)
                {
                    strt = 1;
                    end = camLeft;
                    AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "CCTV CAMERA CABLING CONNECTIONS");
                    portsLeftAfterCams = 45 - camLeft;
                    camLeft = 0;

                }
                if (camLeft == 45)
                {
                    strt = 1;
                    end = 45;
                    AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "CCTV CAMERA CABLING CONNECTIONS");
                    if (!Spare46)
                    {
                        AddRow(46, 46, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "IDF UPS NMC CONNECTION");
                        AddRow(47, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                        Spare46 = true;
                    }
                    else
                    {
                        AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                    }
                    camLeft = 0;
                    portsLeftAfterCams = 0;
                    cntrCurrSwitch++;
                }
                else if (camLeft > 45)
                {
                    do
                    { // fill as many switches as needed                      
                        strt = 1;
                        end = 45;
                        AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "CCTV CAMERA CABLING CONNECTIONS");
                        if (!Spare46)
                        {
                            AddRow(46, 46, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "IDF UPS NMC CONNECTION");
                            AddRow(47, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                            Spare46 = true;
                        }
                        else
                        {
                            AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                        }
                        amzLeft = amzLeft - 45;
                        //       portsLeftAfterAmzs = portsLeftAfterAmzs - 45;
                        cntrCurrSwitch++;
                    } while (camLeft > 45 | camLeft == 45);

                    if (amzLeft != 0)
                    {
                        strt = 1; // partial switch
                        end = camLeft;
                        AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "CCTV CAMERA CABLING CONNECTIONS");
                        camLeft = camLeft - end;
                        portsLeftAfterCams = 45 - end;
                    }
                }
                if (!addedCamSpares && LastItmsOnNewSwitch)
                {
                    strt = end + 1;
                    end = 45;
                    AddRow(strt, end, cntrCurrSwitch, "FUTURE CONNECTIVITY", "FUTURE CCTV CAMERA CABLING CONNECTIONS");
                    if (!Spare46)
                    {
                        AddRow(46, 46, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "IDF UPS NMC CONNECTION");
                        AddRow(47, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                        Spare46 = true;
                    }
                    else
                    {
                        AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                    }

                    addedCamSpares = true;

                    portsLeftAfterCams = 0;

                    cntrCurrSwitch++;

                    if (amzLeft > 45)
                    {
                        AddSwitch++;
                    }
                }
            }
        }

        static bool endDoneAmz;

        private static void AddAmzs(bool CamsFirst, ref int strt, ref int end, ref decimal portsLeftAfterCams, ref int amzLeft, ref decimal portsLeftAfterAmzs, ref int cntrCurrSwitch)
        {
            if (amzLeft > 0)
            {
                if (portsLeftAfterCams > 0)
                {
                    if (amzLeft > portsLeftAfterCams)
                    {
                        strt = end + 1;
                        end = end + (int)portsLeftAfterCams;
                        AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "GROUND FLOOR AMAZON CONNECTIONS");
                        amzLeft = amzLeft - (int)portsLeftAfterCams;
                        portsLeftAfterCams = 0;
                    }
                    else
                    {
                        strt = end + 1;
                        end = end + Cls_AMZ_TelComm.lstDrps[0].amzDropTtl;
                        AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "GROUND FLOOR AMAZON CONNECTIONS");
                        portsLeftAfterAmzs = portsLeftAfterCams - amzLeft;
                        amzLeft = 0;
                    }

                    if (!endDoneAmz & end == 45)
                    {
                        if (!Spare46)
                        {
                            AddRow(46, 46, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "IDF UPS NMC CONNECTION");
                            AddRow(47, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                            Spare46 = true;
                        }
                        else
                        {
                            AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                            endDoneAmz = true;
                        }
                        cntrCurrSwitch++;
                    }
                }

                if (amzLeft > 0 && amzLeft < 45)
                {
                    strt = 1;
                    end = amzLeft;
                    AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "GROUND FLOOR AMAZON CONNECTIONS");
                    amzLeft = amzLeft - end;
                    portsLeftAfterAmzs = end;
                }

                if (amzLeft == 45)
                {
                    strt = 1;
                    end = 45;
                    AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "GROUND FLOOR AMAZON CONNECTIONS");
                    if (!Spare46)
                    {
                        AddRow(46, 46, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "IDF UPS NMC CONNECTION");
                        AddRow(47, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                        Spare46 = true;
                    }
                    else
                    {
                        AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                    }
                    amzLeft = 0;
                    portsLeftAfterAmzs = 0;
                    cntrCurrSwitch++;
                }
                else if (amzLeft > 45)
                {
                    do
                    { // fill as many switches as needed                      
                        strt = 1;
                        end = 45;
                        AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "GROUND FLOOR AMAZON CONNECTIONS");
                        if (!Spare46)
                        {
                            AddRow(46, 46, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "IDF UPS NMC CONNECTION");
                            AddRow(47, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                            Spare46 = true;
                        }
                        else
                        {
                            AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                        }
                        amzLeft = amzLeft - 45;
                        //       portsLeftAfterAmzs = portsLeftAfterAmzs - 45;
                        cntrCurrSwitch++;
                    } while (amzLeft > 45 | amzLeft == 45);

                    if (amzLeft != 0)
                    {
                        strt = 1; // partial switch
                        end = amzLeft;
                        AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "GROUND FLOOR AMAZON CONNECTIONS");
                        amzLeft = amzLeft - end;
                        portsLeftAfterAmzs = 45 - end;
                    }
                }
            }
        }

        private static void AddFuts(bool CamsFirst, bool LastItmsOnNewSwitch, ref int strt, ref int end, ref bool futureConn, int wapsLeft, int amzLeft, int camLeft, ref decimal portsLeftAfterAmzs, int cntrCurrSwitch)
        {
            if (!futureConn && wapsLeft == 0 && amzLeft == 0 && camLeft == 0)
            {
                if (portsLeftAfterAmzs > 0)
                {
                    strt = end + 1; // (int)portsLeftAfterCams; //??
                    end = 45;
                    AddRow(strt, end, cntrCurrSwitch, "FUTURE CONNECTIVITY", "FUTURE GROUND FLOOR CONNECTIVITY");
                    if (!Spare46)
                    {
                        AddRow(46, 46, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "IDF UPS NMC CONNECTION");
                        AddRow(47, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                        Spare46 = true;
                    }
                    else if (futureConn != true)
                    {
                        AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                        futureConn = true;                  
                    }
                    portsLeftAfterAmzs = 0;
                }
                if (portsLeftAfterAmzs == 0)
                {
                    if (LastItmsOnNewSwitch)
                    {           
                        strt = 1;
                        if (end == 45 & futureConn)
                        {
                            cntrCurrSwitch++;
                        }
                    }
                    else
                    {
                        if (futureConn | Spare46)
                            return;

                        strt = end + 1;
                    }


                    end = 45;
                    AddRow(strt, end, cntrCurrSwitch, "FUTURE CONNECTIVITY", "FUTURE GROUND FLOOR CONNECTIVITY");
                    if (!Spare46)
                    {
                        AddRow(46, 46, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "IDF UPS NMC CONNECTION");
                        AddRow(47, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                        Spare46 = true;
                    }
                    else
                    {
                        AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                    }
                }


            }
        }

        #endregion



        #region original table, waps, amz, cctv


        private static void AddFuts(ref int strt, ref int end, ref bool futureConn, int wapsLeft, int amzLeft, int camLeft, ref decimal portsLeftAfterCams, int cntrCurrSwitch)
        {
            if (!futureConn && wapsLeft == 0 && amzLeft == 0 && camLeft == 0)
            {
                futureConn = true;

                if (portsLeftAfterCams > 0)
                {
                    strt = end + 1; // (int)portsLeftAfterCams; //??
                    end = 45;
                    AddRow(strt, end, cntrCurrSwitch, "FUTURE CONNECTIVITY", "FUTURE GROUND FLOOR CONNECTIVITY");
                    if (!Spare46)
                    {
                        AddRow(46, 46, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "IDF UPS NMC CONNECTION");
                        AddRow(47, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                        Spare46 = true;
                    }
                    else
                    {
                        AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                    }
                    portsLeftAfterCams = 0;
                }
                else if (portsLeftAfterCams == 0)
                {
                    strt = 1;
                    end = 45;
                    AddRow(strt, end, cntrCurrSwitch, "FUTURE CONNECTIVITY", "FUTURE GROUND FLOOR CONNECTIVITY");
                    if (!Spare46)
                    {
                        AddRow(46, 46, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "IDF UPS NMC CONNECTION");
                        AddRow(47, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                        Spare46 = true;
                    }
                    else
                    {
                        AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                    }
                }


            }
        }
     
   
        private static void AddCams(ref int strt, ref int end, ref decimal portsLeftAfterAmzs, ref int camLeft, ref decimal portsLeftAfterCams, ref int cntrCurrSwitch)
        {
            if (camLeft > 0)
            {
                if (portsLeftAfterAmzs > 0)
                {
                    if (camLeft > portsLeftAfterAmzs)
                    {
                        strt = end + 1;
                        end = end + (int)portsLeftAfterAmzs;
                        AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "CCTV CAMERA CABLING CONNECTIONS");
                        camLeft = camLeft - (int)portsLeftAfterAmzs;
                        portsLeftAfterAmzs = 0;
                    }
                    else
                    {
                        strt = end + 1;
                        end = end + Cls_AMZ_TelComm.lstDrps[0].camDropTtl;
                        AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "CCTV CAMERA CABLING CONNECTIONS");
                        portsLeftAfterCams = portsLeftAfterAmzs - camLeft;
                        camLeft = 0;
                    }

                    if (end == 45)
                    {
                        AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                        cntrCurrSwitch++;
                    }
                }

                if (camLeft > 0 && camLeft < 45)
                {
                    strt = 1;
                    end = camLeft;
                    AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "CCTV CAMERA CABLING CONNECTIONS");
                    camLeft = camLeft - end;
                    portsLeftAfterCams = end;
                }

                if (camLeft == 45)
                {
                    strt = 1;
                    end = 45;
                    AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "CCTV CAMERA CABLING CONNECTIONS");
                    AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                    camLeft = 0;
                    portsLeftAfterCams = 0;
                    cntrCurrSwitch++;
                }
                else if (camLeft > 45)
                {
                    do
                    { // fill as many switches as needed                      
                        strt = 1;
                        end = 45;
                        AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "CCTV CAMERA CABLING CONNECTIONS");
                        AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                        camLeft = camLeft - 45;
                        //       portsLeftAfterAmzs = portsLeftAfterAmzs - 45;
                        cntrCurrSwitch++;
                    } while (camLeft > 45 | camLeft == 45);

                    if (camLeft != 0)
                    {
                        strt = 1; // partial switch
                        end = camLeft;
                        AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "CCTV CAMERA CABLING CONNECTIONS");
                        camLeft = camLeft - end;
                        portsLeftAfterCams = 45 - end;
                    }
                }
            }
        }

        private static void AddAmzs(bool LastItmsOnNewSwitch, ref int strt, ref int end, ref decimal portsLeftAfterWaps, ref int amzLeft, ref decimal portsLeftAfterAmzs, int camLeft, ref int cntrCurrSwitch, ref int AddSwitch)
        {
            if (amzLeft > 0)
            {
                if (portsLeftAfterWaps > 0)
                {
                    if (amzLeft > portsLeftAfterWaps)
                    {
                        strt = end + 1;
                        end = end + (int)portsLeftAfterWaps;
                        AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "GROUND FLOOR AMAZON CONNECTIONS");
                        amzLeft = amzLeft - (int)portsLeftAfterWaps;
                        portsLeftAfterWaps = 0;
                    }
                    else
                    {
                        strt = end + 1;
                        end = end + Cls_AMZ_TelComm.lstDrps[0].amzDropTtl;
                        AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "GROUND FLOOR AMAZON CONNECTIONS");
                        portsLeftAfterAmzs = portsLeftAfterWaps - amzLeft;
                        amzLeft = 0;
                    }

                    if (end == 45)
                    {
                        AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                        cntrCurrSwitch++;
                    }
                }
                if (amzLeft > 0 && amzLeft < 45)
                {
                    strt = 1;
                    end = amzLeft;
                    AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "GROUND FLOOR AMAZON CONNECTIONS");
                    portsLeftAfterAmzs = 45 - amzLeft;
                    amzLeft = 0;

                }
                if (amzLeft == 45)
                {
                    strt = 1;
                    end = 45;
                    AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "GROUND FLOOR AMAZON CONNECTIONS");
                    AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                    amzLeft = 0;
                    portsLeftAfterAmzs = 0;
                    cntrCurrSwitch++;
                }
                else if (amzLeft > 45)
                {
                    do
                    { // fill as many switches as needed                      
                        strt = 1;
                        end = 45;
                        AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "GROUND FLOOR AMAZON CONNECTIONS");
                        AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                        amzLeft = amzLeft - 45;
                        //       portsLeftAfterAmzs = portsLeftAfterAmzs - 45;
                        cntrCurrSwitch++;
                    } while (amzLeft > 45 | amzLeft == 45);

                    if (amzLeft != 0)
                    {
                        strt = 1; // partial switch
                        end = amzLeft;
                        AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "GROUND FLOOR AMAZON CONNECTIONS");
                        amzLeft = amzLeft - end;
                        portsLeftAfterAmzs = 45 - end;
                    }
                }
                if (LastItmsOnNewSwitch)
                {
                    strt = end + 1;
                    end = 45;
                    AddRow(strt, end, cntrCurrSwitch, "FUTURE CONNECTIVITY", "FUTURE GROUND FLOOR CONNECTIVITY");
                    AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                    portsLeftAfterAmzs = 0;

                    cntrCurrSwitch++;

                    if (camLeft > 45)
                    {
                        AddSwitch++;
                    }
                }
            }
        }

        private static void AddWaps(ref int strt, ref int end, ref int wapsLeft, decimal numSwitchesForWap, ref decimal portsLeftAfterWaps, ref int cntrCurrSwitch)
        {
            if (wapsLeft > 0)
            {
                if (numSwitchesForWap == 1)
                {
                    end = Cls_AMZ_TelComm.lstDrps[0].wapDropTtl;
                    AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "GROUND FLOOR WIRELESS ACCESS POINT (WAP) CABLING");
                    wapsLeft = wapsLeft - Cls_AMZ_TelComm.lstDrps[0].wapDropTtl;
                    portsLeftAfterWaps = portsLeftAfterWaps - Cls_AMZ_TelComm.lstDrps[0].wapDropTtl;

                    if (end == 45)
                    {
                        AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                        cntrCurrSwitch++;
                    }
                }
                else
                {
                    do
                    { // fill as many switches as needed                      
                        strt = 1;
                        end = 45;
                        AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "GROUND FLOOR WIRELESS ACCESS POINT (WAP) CABLING");
                        AddRow(46, 48, cntrCurrSwitch, "NOT USED - RESERVED FOR AMAZON IT", "NOT USED - RESERVED FOR AMAZON IT");
                        wapsLeft = wapsLeft - 45;
                        portsLeftAfterWaps = portsLeftAfterWaps - 45;
                        cntrCurrSwitch++;
                    } while (cntrCurrSwitch < numSwitchesForWap);

                    strt = 1; // partial switch
                    end = wapsLeft;
                    AddRow(strt, end, cntrCurrSwitch, "CATEGORY 6 4-PAIR UTP", "GROUND FLOOR WIRELESS ACCESS POINT (WAP) CABLING");
                    wapsLeft = wapsLeft - end;
                    portsLeftAfterWaps = portsLeftAfterWaps - end;
                }
            }
        }

        static int rowCntr = 0;

        private static void AddRow(int strt, int end, int i, string cable, string dest)
        {
            Cls_AMZ_TelComm.DropsRows row = new Cls_AMZ_TelComm.DropsRows();
            row.PanelNum = i.ToString();

            if (strt == end)
            {
                row.Ports = strt.ToString();
            }
            else
            {
                row.Ports = strt.ToString() + "-" + end.ToString();
            }

            row.Cable = cable;
            row.Dest = dest;
            row.RowNum = rowCntr;

            Cls_AMZ_TelComm.lstDropsRows.Add(row);

            rowCntr++;
        }

        #endregion



        #endregion




        private static List<string> GetAttributeOrderList()
        {
            List<string> order = new List<string>();
              
            order.Add("IDF#"); 

            return order;
        }

        private static AttributeDefinition GetTheCorrectAttributeDef(string tag, Transaction acTrans, string nam, Autodesk.AutoCAD.DatabaseServices.Database acDb)
        {
            // get block def, to add missing attributes
            BlockTableRecord blockDef = new BlockTableRecord();
            BlockTable acBlockTable = (BlockTable)acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead);
            foreach (ObjectId objId in acBlockTable)
            {
                blockDef = objId.GetObject(OpenMode.ForRead) as BlockTableRecord;
                if (blockDef.Name == nam)
                    break;
            }

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

                    blkRef.UpgradeOpen();

                    AttributeReference attRef1 = GetTheCorrectAttributeRef("DEVINFO", tr, blkRef);
                    if (attRef1 != null)
                        attRef1.Tag = "#";
                    AttributeDefinition attDef1 = GetTheCorrectAttributeDef("DEVINFO", tr, nam, acDb);
                    if (attDef1 != null)
                        attDef1.Prompt = "How Many Drops?";

                    AttributeReference attRef2 = GetTheCorrectAttributeRef("DATA", tr, blkRef);
                    if (attRef2 != null)
                        attRef2.Tag = "IDF#";
                    AttributeDefinition attDef2 = GetTheCorrectAttributeDef("DATA", tr, nam, acDb);
                    if (attDef2 != null)
                        attDef2.Prompt = "Which IDF?";

                    nam = "Drop";

                    switch (nam)
                    {                            
                        case "Drop":
                     
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
                                AttributeDefinition attDef = GetTheCorrectAttributeDef(tag, tr, blockDef.Name, acDb);

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


    }
}

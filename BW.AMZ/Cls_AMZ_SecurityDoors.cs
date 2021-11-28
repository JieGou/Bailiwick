using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using OfficeOpenXml;
using OfficeOpenXml.Style;

using MoreLinq;

namespace MyFirstProject.BW.AMZ
{
    public class Cls_AMZ_SecurityDoors
    {

        public static readonly List<ClsDevTypFilterList> AMZ_LstClsDevTypFilterList = new List<ClsDevTypFilterList>();
        public class ClsDevTypFilterList
        {
            public string Count { get; set; }
            public string DeviceType { get; set; }
        }

        public static readonly List<ClsAPCFilterList> AMZ_LstClsAPCFilterList = new List<ClsAPCFilterList>();
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
        
        public static Cls_AMZ_SecurityDoors_Atts AMZ_CurrDeviceTagAtts = new Cls_AMZ_SecurityDoors_Atts();
        
        public static readonly List<Cls_AMZ_SecurityDoors_Atts> doorContact_SortedXY = new List<Cls_AMZ_SecurityDoors_Atts>();

        public static readonly List<Cls_AMZ_SecurityDoors_Atts> AMZ_LstAtts_DevTag = new List<Cls_AMZ_SecurityDoors_Atts>();
        
        public static readonly List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> AMZ_LstPortAssignmentsSecurity = new List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts>();

        public static readonly List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> AMZ_LstFilterIDFsSecurity = new List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts>();
        

        public static void AMZ_SelectBlocks()
        {
            Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            TypedValue[] acTypValAr = new TypedValue[3];
            acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "INSERT"), 0);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.BlockName, "DEVICE TAG*"), 1);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.BlockName, "Device Tag*"), 2);

            SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);

            Cls_AMZ_Main.acSSPrompt = acDocEd.GetSelection(acSelFtr);

            if (Cls_AMZ_Main.acSSPrompt.Status == PromptStatus.OK)
            {
                AMZ_FillSecDoorsList(Cls_AMZ_Main.acSSPrompt);
            }
            else
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
            }
        }

        public static void AMZ_FillSecDoorsList(PromptSelectionResult acSSPrompt)
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

            AMZ_FillSecDoorsList_Sub(ids);
        }

        public static void AMZ_FillSecDoorsList_Sub(ObjectId[] ids)
        {
            Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Clear();

            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
            Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            using (doc.LockDocument())
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                foreach (ObjectId id1 in ids)
                {
                    BlockReference blkRef = null;
                    try
                    {
                        // Autodesk.AutoCAD.Runtime.Exception: 'eWasErased'
                        blkRef = (BlockReference)transaction.GetObject(id1, OpenMode.ForRead);
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception ex)
                    {
                        Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(ex.ToString());
                        continue;
                    }

                    if (blkRef.Name.Contains("DEVICE TAG") | blkRef.Name.Contains("Device Tag") | blkRef.Name.Contains("DOOR TAGS"))
                    {
                        var clsAttsDeviceTag = new Cls_AMZ_SecurityDoors_Atts();
                        clsAttsDeviceTag.BlockName = blkRef.Name;
                        clsAttsDeviceTag.Handle = blkRef.Handle;
                        clsAttsDeviceTag.InsertionPtOfBlock = blkRef.Position;
                        Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Add(clsAttsDeviceTag);

                        AttributeCollection attCol = blkRef.AttributeCollection;

                        foreach (ObjectId attId in attCol)
                        {
                            AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForRead);

                            switch (attRef.Tag)
                            {
                                case "DOORTYPE":
                                    clsAttsDeviceTag.DoorType = attRef.TextString;
                                    break;
                                case "MODULE":
                                    clsAttsDeviceTag.Module = attRef.TextString;
                                    break;
                                case "FUNCTION":
                                    clsAttsDeviceTag.Function = attRef.TextString;
                                    break;
                                case "DEVICEDESC":
                                    clsAttsDeviceTag.DeviceDesc = attRef.TextString;
                                    break;
                                case "DESCRIPTION":
                                    clsAttsDeviceTag.Description = attRef.TextString;
                                    break;
                                case "DETAILNUMBER":
                                    clsAttsDeviceTag.DetailNumber = attRef.TextString;
                                    break;
                                case "DOORNUMBER":
                                    clsAttsDeviceTag.DoorNumber = attRef.TextString; //.PadLeft(4, '0');
                                    break;
                                case "PORTNUMBER":
                                    clsAttsDeviceTag.PortNumber = attRef.TextString;
                                    break;
                                case "BOARDADDRESS":
                                    clsAttsDeviceTag.BoardAddress = attRef.TextString;
                                    break;
                                case "PANEL":
                                    clsAttsDeviceTag.Panel = attRef.TextString;
                                    break;
                                case "CONTROLLER":
                                    clsAttsDeviceTag.Controller = attRef.TextString;
                                    break;
                                case "ISC":
                                    clsAttsDeviceTag.Isc = attRef.TextString;
                                    break;
                                case "SITECODE":
                                    clsAttsDeviceTag.SiteCode = attRef.TextString;
                                    break;
                                case "CABLE_ID":
                                    clsAttsDeviceTag.Cable_Id = attRef.TextString;
                                    break;
                                case "FLOOR":
                                    clsAttsDeviceTag.Floor = attRef.TextString;
                                    break;
                                case "DEVICEID":
                                    clsAttsDeviceTag.DeviceID = attRef.TextString;
                                    break;
                                case "DEVICE_TYPE":
                                    clsAttsDeviceTag.Device_Type = attRef.TextString;
                                    break;
                                case "DOORHWSET":
                                    clsAttsDeviceTag.DoorHwset = attRef.TextString;
                                    break;

                                case "PROGRAM-MATRIX-NAME":
                                    clsAttsDeviceTag.ProgramMatrixName = attRef.TextString;
                                    break;
                                case "PROGRAM-MATRIX-DEVICE-ID":
                                    clsAttsDeviceTag.ProgramMatrixDeviceId = attRef.TextString;
                                    break;
                                case "PIN":
                                    clsAttsDeviceTag.Pin = attRef.TextString;
                                    break;
                                case "PAIRED-MASTER":
                                    clsAttsDeviceTag.PairedMaster = attRef.TextString;
                                    break;
                                case "PAIRED-SLAVE":
                                    clsAttsDeviceTag.PairedSlave = attRef.TextString;
                                    break;
                                case "LOCK-TYPE":
                                    clsAttsDeviceTag.LockType = attRef.TextString;
                                    break;
                                case "REX-TYPE":
                                    clsAttsDeviceTag.RexType = attRef.TextString;
                                    break;
                                case "REX-SUPERVISION":
                                    clsAttsDeviceTag.RexSupervision = attRef.TextString;
                                    break;
                                case "ASSUME-DOOR-USED":
                                    clsAttsDeviceTag.AssumeDoorUsed = attRef.TextString;
                                    break;
                                case "INPUT-SUPERVISION":
                                    clsAttsDeviceTag.InputSupervision = attRef.TextString;
                                    break;
                                case "LINKAGE":
                                    clsAttsDeviceTag.Linkage = attRef.TextString;
                                    break;
                                case "AUX-INPUT-OUTPUTS":
                                    clsAttsDeviceTag.AuxInputOutputs = attRef.TextString;
                                    break;
                                case "TEMPLATE":
                                    clsAttsDeviceTag.Template = attRef.TextString;
                                    break;

                                default:
                                    break;
                            }

                        }
                    }


                }
                transaction.Commit();

                // LstAtts_DevTag.OrderBy(x => x.BlockName).ThenBy(x => x.DoorNumber);

                // blockname not null
                Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Sort((x, y) => x.BlockName.CompareTo(y.BlockName));
            }
        }

        public static readonly BW.AMZ.Cls_AMZ_SecurityDoors_BomCounts AMZ_ClsSecDrsBomCounts = new BW.AMZ.Cls_AMZ_SecurityDoors_BomCounts();

        public static void AMZ_SecDrsBomCount()
        {
            AMZ_ClsSecDrsBomCounts.DEVICE_TAG_080517_CR_1320 = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.BlockName.Contains("DEVICE TAG 080517 CR 1320")).Count();
            AMZ_ClsSecDrsBomCounts.DEVICE_TAG_080517_DC_1100 = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.BlockName.Contains("DEVICE TAG 080517 DC 1100")).Count();
            AMZ_ClsSecDrsBomCounts.DEVICE_TAG_080517_DC_1320 = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.BlockName.Contains("DEVICE TAG 080517 DC 1320")).Count();
            AMZ_ClsSecDrsBomCounts.DEVICE_TAG_080517_EL_1320 = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.BlockName.Contains("DEVICE TAG 080517 EL 1320")).Count();
            AMZ_ClsSecDrsBomCounts.DEVICE_TAG_080517_HN_1200 = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.BlockName.Contains("DEVICE TAG 080517 HN 1200")).Count();
            AMZ_ClsSecDrsBomCounts.DEVICE_TAG_080517_HN_1320 = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.BlockName.Contains("DEVICE TAG 080517 HN 1320")).Count();
            AMZ_ClsSecDrsBomCounts.DEVICE_TAG_080517_IC = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.BlockName.Contains("DEVICE TAG 080517 IC")).Count();
            AMZ_ClsSecDrsBomCounts.DEVICE_TAG_080517_MD_1100 = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.BlockName.Contains("DEVICE TAG 080517 MD 1100")).Count();
            AMZ_ClsSecDrsBomCounts.DEVICE_TAG_080517_RL_1320 = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.BlockName.Contains("DEVICE TAG 080517 RL 1320")).Count();
            AMZ_ClsSecDrsBomCounts.DEVICE_TAG_080517_RX_1320 = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.BlockName.Contains("DEVICE TAG 080517 RX 1320")).Count();
            AMZ_ClsSecDrsBomCounts.DEVICE_TAG_101717_FA_1320 = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.BlockName.Contains("DEVICE TAG 101717 FA 1320")).Count();
            AMZ_ClsSecDrsBomCounts.DEVICE_TAG_101817_ER_1320 = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.BlockName.Contains("DEVICE TAG 101817 ER 1320")).Count();
            AMZ_ClsSecDrsBomCounts.DEVICE_TAG_101817_ES_1320 = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.BlockName.Contains("DEVICE TAG 101817 ES 1320")).Count();
            AMZ_ClsSecDrsBomCounts.DEVICE_TAG_102617_MD_1100 = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.BlockName.Contains("DEVICE TAG 102617 MD 1100")).Count();
        }



        public static void ShowBomCountsInDgv(DataGridView dgv)
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            dgv.Columns.Add("BomItem", "BomItem");
            dgv.Columns.Add("ItemQty", "ItemQty");
            dgv.Columns[0].Width = 500;
            dgv.Columns[1].Width = 100;

            PropertyInfo[] properties = typeof(BW.AMZ.Cls_AMZ_SecurityDoors_BomCounts).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object[] o = { ".", "." };
                o[0] = property.Name;
                o[1] = property.GetValue(AMZ_ClsSecDrsBomCounts, null);
                dgv.Rows.Add(o);
            }

            dgv.Refresh();
        }

        public static void GetLstFilterNetworks()
        {
            var DistinctNetworkNames = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Select(x => x.Isc).Distinct().ToList();
            AMZ_LstClsNetworkFilterList.Clear();

            foreach (string s in DistinctNetworkNames)
            {
                if (s != null)
                {
                    string txt = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.Isc == s).Count().ToString();
                    ClsNetworkFilterList l = new ClsNetworkFilterList();
                    l.Count = txt;
                    l.Isc = s;
                    AMZ_LstClsNetworkFilterList.Add(l);
                }
            }

            AMZ_LstClsNetworkFilterList.Sort((x, y) => x.Isc.CompareTo(y.Isc));
        }



        public static void GetLstFilterAPCs()
        {
            var DistinctClosetNames = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Select(x => x.Panel).Distinct().ToList();
            AMZ_LstClsAPCFilterList.Clear();

            foreach (string s in DistinctClosetNames)
            {
                if (s != null)
                {
                    string txt = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.Panel == s).Count().ToString();
                    ClsAPCFilterList l = new ClsAPCFilterList();
                    l.Count = txt;
                    l.ACP = s;
                    AMZ_LstClsAPCFilterList.Add(l);
                }
            }

            AMZ_LstClsAPCFilterList.Sort((x, y) => x.ACP.CompareTo(y.ACP));
        }
        public static void NumberDoorTypes(bool chkBxShowClr)
        {
            List<string> dev = new List<string>();
            dev.Add("DC");
            dev.Add("MD");
            dev.Add("RX");
            dev.Add("EDR");
            dev.Add("AO");
            dev.Add("PF");
            dev.Add("BF");
            dev.Add("TP");
            dev.Add("HN");
            dev.Add("EL");
            dev.Add("ES");
            dev.Add("RL");
            dev.Add("FA");
            dev.Add("GR");

            dev.Add("CR");

            dev.Add("I");
            dev.Add("KP");
            dev.Add("DR");
            dev.Add("ER");
            dev.Add("IC");

            int clr = 0;

            foreach (string s in dev)
            {
                clr++;

                if (clr == 7)
                {
                    clr++;
                }

                NumberDoors(s, 1, clr, chkBxShowClr);
            }
        }

        #region ports sig


        public static void MkPortAssignments(string type)
        {
            ////OpenFileDialog openFileDialog1 = new OpenFileDialog();
            ////openFileDialog1.InitialDirectory = @"\\my.bailiwick.com@SSL\DavWWWRoot\sites\MMM\009\Internal Project Documents\Site Documents\03 ENG Master Documents\"; // @"K:\3M\2019\";
            ////openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ////openFileDialog1.FilterIndex = 2;
            //////openFileDialog1.RestoreDirectory = true;


            ////if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //  {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            string xlsxFileName = doc.Name.ToLower();

            xlsxFileName = xlsxFileName.Replace(".dwg", "") + " Port Assignments " +
                DateTime.Now.ToString("yMMdd", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + ".xlsx";

            FileInfo newFile = new FileInfo(xlsxFileName);
            //FileInfo templateFile = new FileInfo(
            //    @"C:\Users\gwilliams\Desktop\CP_170815\WAP Autofill 4.23.18.xlsx"
            //    //@"K:\Engineering\3M\2019\Autofill BOM for CAT6 and 6A V1 - dans copy.xlsx"
            //    //@"\\my.bailiwick.com@SSL\DavWWWRoot\sites\MMM\009\Internal Project Documents\Site Documents\03 ENG Master Documents\Autofill BOM for CAT6 and 6A V1.xlsx"
            //    //openFileDialog1.FileName
            //    );


            ExcelPackage pkg = new ExcelPackage();

            bool res = CreateFilePortAssignments(type, newFile, pkg);

            MessageBox.Show("Port Assignments Saved To: " + xlsxFileName + Environment.NewLine +
                "Port Assignments Saved for file: " + doc.Database.Filename);


        }
        public static bool CreateFilePortAssignments(
             string type,
             FileInfo newFile,
             ExcelPackage xlPackage//,
                                   //List<Cls_AMZ_SecurityDoors_DeviceTag> data
         )
        {
            try
            {
                using (xlPackage = new ExcelPackage(newFile))
                {
                    if (xlPackage.Workbook.Worksheets.Count > 0)
                        xlPackage.Workbook.Worksheets.Delete(1);

                    xlPackage.Workbook.Worksheets.Add("XL Pull " + type);

                    ExcelWorksheet ws = xlPackage.Workbook.Worksheets[1];

                    //wsWAP.Cells["A1"].Value = clsBomCnts.Wap_C_DC_DropCeilingMount2802i;             

                    PropertyInfo[] propInfo = typeof(BW.AMZ.Int_AMZ_SecurityDoorsAtts).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    //      Type[] tt = typeof(Int_3M_AllAtts).GetInterfaces();
                    //      PropertyInfo[] pis2 = tt[0].GetProperties(BindingFlags.Public | BindingFlags.Instance);

                    int row = 1;
                    int col = 1;
                    foreach (var n in propInfo)
                    {
                        if (n.Name != "BlockName" & n.Name != "Handle" & n.Name != "InsertionPtOfBlock")
                        {
                            ws.Cells[row, col].Value = n.Name;
                        }
                        col++;
                    }

                    string currAcp = "";
                    string prevAcp = "";

                    string currMod = "";
                    string prevMod = "";

                    foreach (Cls_AMZ_SecurityDoors_Atts dt in AMZ_LstPortAssignmentsSecurity)
                    {
                        currMod = dt.Module;
                        currAcp = dt.Panel;

                        row++;
                        col = 1;

                        foreach (var n in propInfo)
                        {
                            var typ = dt.GetType();
                            var property = typ.GetProperty(n.Name);

                            if (property.Name != "BlockName" & property.Name != "Handle" & property.Name != "InsertionPtOfBlock")
                            {
                                ws.Cells[row, col].Value = property.GetValue(dt);

                                ws.Cells[row, col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                ws.Cells[row, col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                ws.Cells[row, col].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                ws.Cells[row, col].Style.Border.Top.Style = ExcelBorderStyle.Thin;

                                if (prevMod != null && prevMod != currMod)
                                {
                                    // highlight the cell
                                    ws.Cells[row, col].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                }

                                if (prevAcp != null && prevAcp != currAcp)
                                {
                                    // highlight the cell
                                    ws.Cells[row, col].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                                }

                            }

                            col++;
                        }

                        prevMod = dt.Module;
                        prevAcp = dt.Panel;
                    }

                    //ws.Cells[2, 1].LoadFromCollection(Cls_AMZ_Main.AMZ_LstPortAssignmentsSecurity);

                    ws.Cells.AutoFitColumns();

                    xlPackage.Save();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static void AMZ_AssignPorts()
        {
            AMZ_LstPortAssignmentsSecurity.Clear();

            string SiteCode = AMZ_LstAtts_DevTag.Where(x => x.SiteCode != null).FirstOrDefault().SiteCode.ToString();
            string Isc = "No 1320, 1100 or 1200;";

            // each panel
            foreach (ClsAPCFilterList acp in AMZ_LstClsAPCFilterList)
            {
                AMZ_LstFilterIDFsSecurity.Clear();
                var nLst = AMZ_LstAtts_DevTag.Where(x =>
                ((x.Module == "LNL-1320" | x.Module == "LNL-1100" | x.Module == "LNL-1200") & x.Panel == acp.ACP)).ToList();
                AMZ_LstFilterIDFsSecurity.AddRange(nLst);
                
                if (acp.ACP.Contains("ACP"))
                {
                    if (acp.ACP == "ACP.01" | acp.ACP == "ACP.02")
                    {
                        int i = 1;

                        while (i <= 5)
                        {
                            Cls_AMZ_SecurityDoors_Atts spare = new Cls_AMZ_SecurityDoors_Atts();
                            spare.Panel = acp.ACP;
                            spare.Module = "LNL-3300";
                            if (AMZ_LstFilterIDFsSecurity.Count > 0)
                            {
                                spare.SiteCode = AMZ_LstFilterIDFsSecurity.Where(x => x.SiteCode != null).FirstOrDefault().SiteCode.ToString();
                                spare.Isc = AMZ_LstFilterIDFsSecurity.Where(x => x.Isc != null).FirstOrDefault().Isc.ToString();
                            }
                            else
                            {
                                spare.SiteCode = SiteCode;
                                spare.Isc = Isc;
                            }

                            spare.Description = "Spare";
                            AMZ_LstPortAssignmentsSecurity.Insert(AMZ_LstPortAssignmentsSecurity.Count(), spare);
                            i++;
                        }

                        i = 1;

                        while (i <= 8)
                        {
                            Cls_AMZ_SecurityDoors_Atts spare = new Cls_AMZ_SecurityDoors_Atts();
                            spare.Panel = acp.ACP;
                            spare.Module = "LNL-8000";

                            if (AMZ_LstFilterIDFsSecurity.Count > 0)
                            {
                                spare.SiteCode = AMZ_LstFilterIDFsSecurity.Where(x => x.SiteCode != null).FirstOrDefault().SiteCode.ToString();
                                spare.Isc = AMZ_LstFilterIDFsSecurity.Where(x => x.Isc != null).FirstOrDefault().Isc.ToString();
                            }
                            else
                            {
                                spare.SiteCode = SiteCode;
                                spare.Isc = Isc;
                            }

                            spare.Description = "Spare";
                            AMZ_LstPortAssignmentsSecurity.Insert(AMZ_LstPortAssignmentsSecurity.Count(), spare);
                            i++;
                        }
                    }

                    AMZ_AssignPorts_1320(acp);

                    AMZ_AssignPorts_1100(acp);

                    AMZ_AssignPorts_1200(acp);

                }
            }
        }

        /// <summary>
        /// assing ports 1320 module
        /// </summary>
        /// <param name="acp"></param>
        private static void AMZ_AssignPorts_1320(ClsAPCFilterList acp)
        {
            AMZ_LstFilterIDFsSecurity.Clear();
            var nLst = AMZ_LstAtts_DevTag
                .Where(x => (x.Module == "LNL-1320" & x.Panel == acp.ACP &
                (x.DoorType == "ACD" | x.DoorType == "TS"))).ToList();
            AMZ_LstFilterIDFsSecurity.AddRange(nLst);

            var doors = AMZ_LstFilterIDFsSecurity.DistinctBy(c => c.DoorNumber).ToList();

            foreach (Cls_AMZ_SecurityDoors_Atts d in doors)
            {
                List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> cr1 =
                    AMZ_LstFilterIDFsSecurity.Where(x => (x.DoorNumber == d.DoorNumber &
                    x.Device_Type == "CR" & x.PortNumber == "1")).ToList();

                List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> cr2 =
                    AMZ_LstFilterIDFsSecurity.Where(x => (x.DoorNumber == d.DoorNumber &
                    x.Device_Type == "CR" & x.PortNumber == "2")).ToList();

                if (cr1.Count > 0)
                {
                    ProgramMatrix(cr1[0]);

                    AMZ_LstPortAssignmentsSecurity.Insert(AMZ_LstPortAssignmentsSecurity.Count(), cr1[0]);
                }
                else
                {
                    AddSpareToModule(acp, 1, "LNL-1320");
                }
                if (cr2.Count > 0)
                {
                    ProgramMatrix(cr2[0]);

                    AMZ_LstPortAssignmentsSecurity.Insert(AMZ_LstPortAssignmentsSecurity.Count(), cr2[0]);
                }
                else
                {
                    AddSpareToModule(acp, 2, "LNL-1320");
                }

                List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> inDC = AMZ_LstFilterIDFsSecurity.Where(x => (x.DoorNumber == d.DoorNumber & x.Device_Type == "DC")).ToList();
                List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> inMD = AMZ_LstFilterIDFsSecurity.Where(x => (x.DoorNumber == d.DoorNumber & x.Device_Type == "MD")).ToList();
                List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> inRX = AMZ_LstFilterIDFsSecurity.Where(x => (x.DoorNumber == d.DoorNumber & x.Device_Type == "RX")).ToList();
                List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> inEDR = AMZ_LstFilterIDFsSecurity.Where(x => (x.DoorNumber == d.DoorNumber & x.Device_Type == "EDR")).ToList();
                List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> inAO = AMZ_LstFilterIDFsSecurity.Where(x => (x.DoorNumber == d.DoorNumber & x.Device_Type == "AO")).ToList();
                List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> inPF = AMZ_LstFilterIDFsSecurity.Where(x => (x.DoorNumber == d.DoorNumber & x.Device_Type == "PF")).ToList();
                List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> inBF = AMZ_LstFilterIDFsSecurity.Where(x => (x.DoorNumber == d.DoorNumber & x.Device_Type == "BF")).ToList();
                List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> inTP = AMZ_LstFilterIDFsSecurity.Where(x => (x.DoorNumber == d.DoorNumber & x.Device_Type == "TP")).ToList();

                AddInput1320(inDC, "1", acp);
                AddInput1320(inMD, "2", acp);
                AddInput1320(inRX, "3", acp);
                AddInput1320(inEDR, "4", acp);
                AddInput1320(inAO, "5", acp);
                AddInput1320(inPF, "6", acp);
                AddInput1320(inBF, "7", acp);
                AddInput1320(inTP, "8", acp);


                List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> outHn = AMZ_LstFilterIDFsSecurity.Where(x => (x.DoorNumber == d.DoorNumber & x.Device_Type == "HN")).ToList();
                List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> outEL = AMZ_LstFilterIDFsSecurity.Where(x => (x.DoorNumber == d.DoorNumber & x.Device_Type == "EL")).ToList();
                List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> outES = AMZ_LstFilterIDFsSecurity.Where(x => (x.DoorNumber == d.DoorNumber & x.Device_Type == "ES")).ToList();
                List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> outRL = AMZ_LstFilterIDFsSecurity.Where(x => (x.DoorNumber == d.DoorNumber & x.Device_Type == "RL")).ToList();
                List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> outFA = AMZ_LstFilterIDFsSecurity.Where(x => (x.DoorNumber == d.DoorNumber & x.Device_Type == "FA")).ToList();
                List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> outGR = AMZ_LstFilterIDFsSecurity.Where(x => (x.DoorNumber == d.DoorNumber & x.Device_Type == "GR")).ToList();

                AddOutput1320(outHn, "1", acp);
                AddOutput1320(outEL, "2", acp);
                AddOutput1320(outES, "3", acp);
                AddOutput1320(outRL, "4", acp);
                AddOutput1320(outFA, "5", acp);
                AddOutput1320(outGR, "6", acp);
            }

        }

        /// <summary>
        /// sets up program matrix id
        /// </summary>
        /// <param name="acp"></param>
        private static void ProgramMatrix(Cls_AMZ_SecurityDoors_Atts clsDevTag)
        {
            clsDevTag.ProgramMatrixDeviceId =
            clsDevTag.SiteCode + " " +
            clsDevTag.Isc + " " +
            clsDevTag.PortNumber + " " +
            clsDevTag.DeviceID + " " +
            clsDevTag.DoorNumber + " " +
            clsDevTag.DeviceDesc;
        }

        /// <summary>
        /// add output to 1320 module
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="port"></param>
        /// <param name="acp"></param>
        private static void AddOutput1320(List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> lst, string port, ClsAPCFilterList acp)
        {
            if (lst.Count > 0)
            {
                lst[0].ProgramMatrixDeviceId =
                lst[0].SiteCode + " " +
                lst[0].Isc + " " +
                lst[0].PortNumber + " " +
                lst[0].DeviceID + " " +
                lst[0].DoorNumber + " " +
                lst[0].DeviceDesc;

                lst[0].PortNumber = port;

                AMZ_LstPortAssignmentsSecurity.Insert(AMZ_LstPortAssignmentsSecurity.Count(), lst[0]);
            }
            else
            {
                Cls_AMZ_SecurityDoors_Atts spare = new Cls_AMZ_SecurityDoors_Atts();
                spare.PortNumber = port;
                //    spare.BoardAddress = "Spare";
                spare.Module = "LNL-1320";
                spare.Panel = acp.ACP;
                spare.SiteCode = AMZ_LstFilterIDFsSecurity.Where(x => x.SiteCode != null).FirstOrDefault().SiteCode.ToString();
                spare.Isc = AMZ_LstFilterIDFsSecurity.Where(x => x.Isc != null).FirstOrDefault().Isc.ToString();
                spare.Description = "Spare";
                AMZ_LstPortAssignmentsSecurity.Insert(AMZ_LstPortAssignmentsSecurity.Count(), spare);
            }

        }

        /// <summary>
        /// add input to 1320 module
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="port"></param>
        /// <param name="acp"></param>
        private static void AddInput1320(List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> lst, string port, ClsAPCFilterList acp)
        {
            if (lst.Count > 0)
            {
                lst[0].ProgramMatrixDeviceId =
                lst[0].SiteCode + " " +
                lst[0].Isc + " " +
                lst[0].PortNumber + " " +
                lst[0].DeviceID + " " +
                lst[0].DoorNumber + " " +
                lst[0].DeviceDesc;

                lst[0].PortNumber = port;

                AMZ_LstPortAssignmentsSecurity.Insert(AMZ_LstPortAssignmentsSecurity.Count(), lst[0]);
            }
            else
            {
                Cls_AMZ_SecurityDoors_Atts spare = new Cls_AMZ_SecurityDoors_Atts();
                spare.PortNumber = port;
                //    spare.BoardAddress = "Spare";
                spare.Module = "LNL-1320";
                spare.Panel = acp.ACP;
                spare.SiteCode = AMZ_LstFilterIDFsSecurity.Where(x => x.SiteCode != null).FirstOrDefault().SiteCode.ToString();
                spare.Isc = AMZ_LstFilterIDFsSecurity.Where(x => x.Isc != null).FirstOrDefault().Isc.ToString();
                spare.Description = "Spare";
                AMZ_LstPortAssignmentsSecurity.Insert(AMZ_LstPortAssignmentsSecurity.Count(), spare);
            }

        }

        /// <summary>
        /// add port assignments to 1100 module
        /// </summary>
        /// <param name="acp"></param>
        /// <param name="cntr1100"></param>
        /// <param name="moduleCount"></param>
        private static void AMZ_AssignPorts_1100(ClsAPCFilterList acp)
        {
            int i = 1;

            AMZ_LstFilterIDFsSecurity.Clear();
            var nLst = AMZ_LstAtts_DevTag
                .Where(x => (x.Module == "LNL-1100" &
                x.Panel == acp.ACP & !x.DoorNumber.Contains("IDF"))).ToList();
            AMZ_LstFilterIDFsSecurity.AddRange(nLst);

            if (AMZ_LstFilterIDFsSecurity.Count > 0)
            {
                AMZ_LstFilterIDFsSecurity.Sort((x, y) => x.DoorNumber.CompareTo(y.DoorNumber));

                foreach (Cls_AMZ_SecurityDoors_Atts d in AMZ_LstFilterIDFsSecurity)
                {
                    if (i <= 16)
                    {
                        d.PortNumber = i.ToString().PadLeft(2, '0');
                        i++;
                    }
                    else
                    {
                        i = 1;
                        d.PortNumber = i.ToString().PadLeft(2, '0');
                        i++;
                    }

                    ProgramMatrix(d);
                }

                // add to list
                AMZ_LstPortAssignmentsSecurity.InsertRange(AMZ_LstPortAssignmentsSecurity.Count(), AMZ_LstFilterIDFsSecurity);

                while (i <= 16 & i != 17)
                {
                    i = AddSpareToModule(acp, i, "LNL-1100");
                }
            }

            // last 1100
            if (AMZ_LstFilterIDFsSecurity.Count() > 0)
            {
                if (Last1100(acp))
                {
                    i = 1;

                    while (i <= 16 & i != 17)
                    {
                        i = AddSpareToModule(acp, i, "LNL-1100");
                    }

                    Last1100(acp);
                }
            }

            // idf enclosure monitoring
            AMZ_LstFilterIDFsSecurity.Clear();
            nLst = AMZ_LstAtts_DevTag
              .Where(x => (x.Module == "LNL-1100" & x.Panel == acp.ACP & x.DoorNumber.Contains("IDF")))
              .ToList();
            AMZ_LstFilterIDFsSecurity.AddRange(nLst);

            if (AMZ_LstFilterIDFsSecurity.Count > 0)
            {
                AMZ_LstFilterIDFsSecurity.Sort((x, y) => x.DoorNumber.CompareTo(y.DoorNumber));

                foreach (Cls_AMZ_SecurityDoors_Atts d in AMZ_LstFilterIDFsSecurity)
                {
                    if (i <= 16)
                    {
                        d.PortNumber = i.ToString().PadLeft(2, '0');
                        i++;
                    }
                    else
                    {
                        i = 1;
                        d.PortNumber = i.ToString().PadLeft(2, '0');
                        i++;
                    }

                    ProgramMatrix(d);
                }

                // add to list
                AMZ_LstPortAssignmentsSecurity.InsertRange(AMZ_LstPortAssignmentsSecurity.Count(), AMZ_LstFilterIDFsSecurity);

                while (i <= 16 & i != 17)
                {
                    i = AddSpareToModule(acp, i, "LNL-1100");
                }
            }

            // last 1100
            if (AMZ_LstFilterIDFsSecurity.Count() > 0)
            {
                if (Last1100(acp))
                {
                    i = 1;

                    while (i <= 16 & i != 17)
                    {
                        i = AddSpareToModule(acp, i, "LNL-1100");
                    }

                    Last1100(acp);
                }
            }
        }

        /// <summary>
        /// add spare to module
        /// </summary>
        /// <param name="acp"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static int AddSpareToModule(ClsAPCFilterList acp, int i, string module)
        {
            Cls_AMZ_SecurityDoors_Atts spare = new Cls_AMZ_SecurityDoors_Atts();
            spare.PortNumber = i.ToString().PadLeft(2, '0');
            //    spare.BoardAddress = "Spare";
            spare.Module = module;
            spare.Panel = acp.ACP;
            spare.SiteCode = AMZ_LstFilterIDFsSecurity.Where(x => x.SiteCode != null).FirstOrDefault().SiteCode.ToString();
            spare.Isc = AMZ_LstFilterIDFsSecurity.Where(x => x.Isc != null).FirstOrDefault().Isc.ToString();
            spare.Description = "Spare";
            AMZ_LstPortAssignmentsSecurity.Insert(AMZ_LstPortAssignmentsSecurity.Count(), spare);
            i++;
            return i;
        }

        /// <summary>
        /// adds power fail, battery fail and tamper proof to 1100 module
        /// </summary>
        /// <param name="acp"></param>
        private static bool Last1100(ClsAPCFilterList acp)
        {
            List<Cls_AMZ_SecurityDoors_Atts> lst = new List<Cls_AMZ_SecurityDoors_Atts>();

            lst.Add(AMZ_LstPortAssignmentsSecurity[AMZ_LstPortAssignmentsSecurity.Count() - 1]);
            lst.Add(AMZ_LstPortAssignmentsSecurity[AMZ_LstPortAssignmentsSecurity.Count() - 2]);
            lst.Add(AMZ_LstPortAssignmentsSecurity[AMZ_LstPortAssignmentsSecurity.Count() - 3]);

            var empty = lst.Where(x => x.Description != "Spare").ToList();

            if (empty.Count > 0)
            {
                // we don't have 3 spares so add another spare module

                return true;
            }

            Cls_AMZ_SecurityDoors_Atts last = AMZ_LstPortAssignmentsSecurity[AMZ_LstPortAssignmentsSecurity.Count() - 1];

            if (last.Device_Type == null)
            {
                last.Panel = acp.ACP;
                last.Module = "LNL-1100";
                last.DoorNumber = acp.ACP;
                last.Function = "Power Fail";
                last.Device_Type = "PF";
            }

            last = AMZ_LstPortAssignmentsSecurity[AMZ_LstPortAssignmentsSecurity.Count() - 2];

            if (last.Device_Type == null)
            {
                last.Panel = acp.ACP;
                last.Module = "LNL-1100";
                last.DoorNumber = acp.ACP;
                last.Function = "Battery Fail";
                last.Device_Type = "BF";
            }

            last = AMZ_LstPortAssignmentsSecurity[AMZ_LstPortAssignmentsSecurity.Count() - 3];

            if (last.Device_Type == null)
            {
                last.Panel = acp.ACP;
                last.Module = "LNL-1100";
                last.DoorNumber = acp.ACP;
                last.Function = "Tamper Switch";
                last.Device_Type = "TP";
            }

            return false; // we have 3 spares
        }

        /// <summary>
        /// add port assignments to 1200
        /// </summary>
        /// <param name="acp"></param>
        private static void AMZ_AssignPorts_1200(ClsAPCFilterList acp)
        {
            int i = 1;
            AMZ_LstFilterIDFsSecurity.Clear();
            var nLst = AMZ_LstAtts_DevTag
                .Where(x => (x.Module == "LNL-1200" & x.Panel == acp.ACP & !x.DoorNumber.Contains("IDF")))
                .ToList();
            AMZ_LstFilterIDFsSecurity.AddRange(nLst);

            if (AMZ_LstFilterIDFsSecurity.Count > 0)
            {
                AMZ_LstFilterIDFsSecurity.Sort((x, y) => x.DoorNumber.CompareTo(y.DoorNumber));

                foreach (Cls_AMZ_SecurityDoors_Atts d in AMZ_LstFilterIDFsSecurity)
                {
                    if (i <= 16)
                    {
                        d.PortNumber = i.ToString().PadLeft(2, '0');
                        i++;
                    }
                    else
                    {
                        i = 1;
                        d.PortNumber = i.ToString().PadLeft(2, '0');
                        i++;
                    }

                    ProgramMatrix(d);
                }

                // add to list
                AMZ_LstPortAssignmentsSecurity.InsertRange(AMZ_LstPortAssignmentsSecurity.Count(), AMZ_LstFilterIDFsSecurity);

                while (i <= 16 & i != 17)
                {
                    i = AddSpareToModule(acp, i, "LNL-1200");
                }
            }
            // idf
            AMZ_LstFilterIDFsSecurity.Clear();
            nLst = AMZ_LstAtts_DevTag
                .Where(x => (x.Module == "LNL-1200" & x.Panel == acp.ACP & x.DoorNumber.Contains("IDF")))
                .ToList();
            AMZ_LstFilterIDFsSecurity.AddRange(nLst);

            if (AMZ_LstFilterIDFsSecurity.Count > 0)
            {
                AMZ_LstFilterIDFsSecurity.Sort((x, y) => x.DoorNumber.CompareTo(y.DoorNumber));

                foreach (Cls_AMZ_SecurityDoors_Atts d in AMZ_LstFilterIDFsSecurity)
                {
                    if (i <= 16)
                    {
                        d.PortNumber = i.ToString().PadLeft(2, '0');
                        i++;
                    }
                    else
                    {
                        i = 1;
                        d.PortNumber = i.ToString().PadLeft(2, '0');
                        i++;
                    }

                    ProgramMatrix(d);
                }

                // add to list
                AMZ_LstPortAssignmentsSecurity.InsertRange(AMZ_LstPortAssignmentsSecurity.Count(), AMZ_LstFilterIDFsSecurity);

                while (i <= 16 & i != 17)
                {
                    i = AddSpareToModule(acp, i, "LNL-1200");
                }
            }

        }


        #endregion


        public static void GetLstFilterDevTyp()
        {
            var DistinctClosetNames = AMZ_LstAtts_DevTag.Select(x => x.Device_Type).Distinct().ToList();
            AMZ_LstClsDevTypFilterList.Clear();

            foreach (string s in DistinctClosetNames)
            {
                if (s != null)
                {
                    string txt = AMZ_LstAtts_DevTag.Where(x => x.Device_Type == s).Count().ToString();
                    ClsDevTypFilterList l = new ClsDevTypFilterList();
                    l.Count = txt;
                    l.DeviceType = s;
                    AMZ_LstClsDevTypFilterList.Add(l);
                }
            }

            AMZ_LstClsDevTypFilterList.Sort((x, y) => x.DeviceType.CompareTo(y.DeviceType));
        }




        #region Number Security Doors


        public static void NumberDoors(string dev, int num, int colorIndx, bool showColor)
        {
            List<Cls_AMZ_SecurityDoors_Atts> doorContact = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.Device_Type == dev).ToList();

            doorContact_SortedXY.Clear();
            var nLst = doorContact.OrderByDescending(x => x.InsertionPtOfBlock.Y).ThenBy(x => x.InsertionPtOfBlock.X).ToList();
            doorContact_SortedXY.AddRange(nLst);

            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            List<Handle> LstHandle = new List<Handle>();
            LstHandle.AddRange(doorContact_SortedXY.Select(x => x.Handle).ToArray());

            Handle[] mainArr = LstHandle.ToArray();

            List<ObjectId> LstObjectId = new List<ObjectId>();

            foreach (Handle h in mainArr)
            {
                ObjectId id = Cls_BW_Utility.ObjectIDFromHandle(database, h.ToString());
                LstObjectId.Add(id);
            }

            using (doc.LockDocument())
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                foreach (ObjectId idD in LstObjectId)
                {
                    BlockReference blkRef1 = (BlockReference)transaction.GetObject(idD, OpenMode.ForRead);
                    Autodesk.AutoCAD.DatabaseServices.AttributeCollection attCol = blkRef1.AttributeCollection;

                    foreach (ObjectId attId in attCol)
                    {
                        AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForWrite);//attRef.UpgradeOpen();

                        switch (attRef.Tag.ToUpper())
                        {
                            case "DEVICEID":
                                attRef.TextString = num.ToString().PadLeft(3, '0');
                                num++;

                                if (showColor)
                                {
                                    attRef.ColorIndex = colorIndx;
                                }

                                break;

                            default:
                                break;
                        }

                    }
                }
                transaction.Commit();
            }
        }

        #endregion



 
        #region door numbers for dc's ect...

        public static int DoorNum { get; set; }
        //  nudDoorNum.Text = (int.Parse(nudDoorNum.Text) + 1).ToString();

        public static List<ObjectId> AssignDoorNums(string pre, string num)
        {
            DoorNum = int.Parse(num);

            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            List<ObjectId> PickedIDs = new List<ObjectId>();

            List<string> blkNames = new List<string>();
            blkNames.Add("Device Tag");
            blkNames.Add("DOOR TAGS");

            SelectionFilter sf = new SelectionFilter(
                BW.Cls_BW_Utility.CreateFilterListForBlocks(blkNames)
                );

            PromptSelectionResult acSSPrompt;

            bool loop = true;

            do
            {              
                ed.SetImpliedSelection(new ObjectId[0]);

                acSSPrompt = ed.GetSelection(sf);

                if (acSSPrompt.Status == PromptStatus.OK)
                {
                    SelectionSet acSSet = acSSPrompt.Value;
                    ObjectId[] ids = { };

                    ids = acSSet.GetObjectIds();

                    using (doc.LockDocument())
                    using (Transaction transaction = database.TransactionManager.StartTransaction())
                    {
                        foreach (ObjectId i in ids)
                        {
                            BlockReference blkRef1 = (BlockReference)transaction.GetObject(i, OpenMode.ForRead);

                            Autodesk.AutoCAD.DatabaseServices.AttributeCollection attCol = blkRef1.AttributeCollection;

                            foreach (ObjectId attId in attCol)
                            {
                                AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForWrite);//attRef.UpgradeOpen();

                                switch (attRef.Tag.ToUpper())
                                {
                                    case "DOORNUMBER":
                                        PickedIDs.Add(i);
                                        attRef.TextString = pre + DoorNum.ToString().PadLeft(3, '0');
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }

                        transaction.Commit();
               
                        DoorNum++;
                    }
                }
                else
                {
                    loop = false;
                }

            } while (loop == true);
                  
            return PickedIDs;
        }

        #endregion






        public static void SwapAttributeReferences()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;
              
            bool loop = true;

            do
            {
                PromptEntityOptions peoSource = new PromptEntityOptions("\nSelect source block reference:");
                peoSource.SetRejectMessage("\nMust be block reference...");
                peoSource.AddAllowedClass(typeof(BlockReference), true);
                PromptEntityResult perSource = ed.GetEntity(peoSource);

                PromptEntityOptions peoDest = new PromptEntityOptions("\nSelect destination block reference:");
                peoDest.SetRejectMessage("\nMust be block reference...");
                peoDest.AddAllowedClass(typeof(BlockReference), true);
                PromptEntityResult perDest = ed.GetEntity(peoDest);

                if (perSource.Status == PromptStatus.OK & perDest.Status == PromptStatus.OK)
                {
                    Transaction tr = db.TransactionManager.StartTransaction();

                    using (doc.LockDocument())
                    using (tr)
                    {
                        BlockReference brSource = (BlockReference)tr.GetObject(perSource.ObjectId, OpenMode.ForRead);
                        BlockReference brDest = (BlockReference)tr.GetObject(perDest.ObjectId, OpenMode.ForRead);

                        AttributeReference source = GetTheCorrectAttributeRef("ISC", tr, brSource);
                        AttributeReference dest = GetTheCorrectAttributeRef("ISC", tr, brDest);
                        if (source != null & dest != null)
                            dest.TextString = source.TextString;

                        source = GetTheCorrectAttributeRef("PANEL", tr, brSource);
                        dest = GetTheCorrectAttributeRef("PANEL", tr, brDest);
                        if (source != null & dest != null)
                            dest.TextString = source.TextString;

                        source = GetTheCorrectAttributeRef("DEVICENUMBER", tr, brSource);
                        dest = GetTheCorrectAttributeRef("DEVICEID", tr, brDest);
                        if (source != null & dest != null)
                            dest.TextString = source.TextString;

                        source = GetTheCorrectAttributeRef("DOORNUMBER", tr, brSource);
                        dest = GetTheCorrectAttributeRef("DOORNUMBER", tr, brDest);
                        if (source != null & dest != null)
                            dest.TextString = source.TextString;

                        tr.Commit();
                    }
                }
                else
                {
                    loop = false;
                }

            } while (loop == true);
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

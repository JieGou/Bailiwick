using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;


namespace MyFirstProject.BW.TGT
{
    public static class Cls_TGT_MainForm
    {
        private static PromptSelectionResult acSSPrompt;

        // attributes
        public static readonly List<Cls_TGT_Aps> LstAtts_APs = new List<Cls_TGT_Aps>();
        public static readonly List<Cls_TGT_Decoy> LstAtts_Decoy = new List<Cls_TGT_Decoy>();
        public static readonly List<Cls_TGT_Drop> LstAtts_Drop = new List<Cls_TGT_Drop>();
        public static readonly List<Cls_TGT_Phone> LstAtts_Phone = new List<Cls_TGT_Phone>();
        public static readonly List<Cls_TGT_Speaker> LstAtts_Speaker = new List<Cls_TGT_Speaker>();

        //tables in autocad and grid displays
        public static readonly List<Cls_TGT_TblCounts> LstTbl_Counts = new List<Cls_TGT_TblCounts>();
        public static readonly List<Cls_TGT_TblVoiceAndData> LstTbl_VoiceAndData = new List<Cls_TGT_TblVoiceAndData>();
        public static readonly List<Cls_TGT_TblVoiceAndDataT4> LstTbl_VoiceAndDataT4 = new List<Cls_TGT_TblVoiceAndDataT4>();
        public static readonly List<Cls_TGT_TblVoiceAndDataTVS> LstTbl_VoiceAndDataTVS = new List<Cls_TGT_TblVoiceAndDataTVS>();
        public static readonly List<Cls_TGT_TblPagingCounts> LstTbl_PagingCounts = new List<Cls_TGT_TblPagingCounts>();

        // data extraction to excel file
        public static readonly List<Int_TGT_AllAtts> LstXlPull = new List<Int_TGT_AllAtts>();




        public static void BtnTestData_Click_Sub()
        {
            LstTbl_Counts.Clear();
            LstTbl_VoiceAndData.Clear();
            LstTbl_VoiceAndDataT4.Clear();
            LstTbl_VoiceAndDataTVS.Clear();
            LstTbl_PagingCounts.Clear();

            Cls_TGT_TblCounts cls_Counts = new Cls_TGT_TblCounts();
            cls_Counts.Count = "..";
            cls_Counts.Distance_Of_Run = "..";
            cls_Counts.END = "..";
            cls_Counts.IDF = "..";
            cls_Counts.Name = "..";
            LstTbl_Counts.Add(cls_Counts);

            Cls_TGT_TblVoiceAndData cls_vd = new Cls_TGT_TblVoiceAndData();
            cls_vd.Cable_Number = "..";
            cls_vd.IDF = "..";
            cls_vd.Owner = "..";
            cls_vd.Phone_Extension = "..";
            cls_vd.Phone_Type = "..";
            LstTbl_VoiceAndData.Add(cls_vd);

            Cls_TGT_TblVoiceAndDataT4 cls_vdT4 = new Cls_TGT_TblVoiceAndDataT4();
            cls_vdT4.Cable_Number = "..";
            cls_vdT4.IDF = "..";
            cls_vdT4.Owner = "..";
            cls_vdT4.Phone_Extension = "..";
            cls_vdT4.Phone_Type = "..";
            LstTbl_VoiceAndDataT4.Add(cls_vdT4);

            Cls_TGT_TblVoiceAndDataTVS cls_vdTVS = new Cls_TGT_TblVoiceAndDataTVS();
            cls_vdTVS.Assembly_Type = "..";
            cls_vdTVS.Cable_1 = "..";
            cls_vdTVS.Cable_2 = "..";
            cls_vdTVS.Cable_Number = "..";
            cls_vdTVS.Count = "..";
            cls_vdTVS.CSKU = "..";
            cls_vdTVS.IDF = "..";
            LstTbl_VoiceAndDataTVS.Add(cls_vdTVS);

            Cls_TGT_TblPagingCounts cls_pc = new Cls_TGT_TblPagingCounts();
            cls_pc.Counts = "..";
            cls_pc.Item_Description = "..";
            cls_pc.Item_Part_Number = "..";
            LstTbl_PagingCounts.Add(cls_pc);
        }




        public static void SelectBlocks()
        {
            Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Cls_TGT_BlksToSelect cls = new Cls_TGT_BlksToSelect();
            SelectionFilter acSelFtr = BW.Cls_BW_SelDynBlksAlso.SelectDynamicBlocks(cls.GetBlockNames());

            acSSPrompt = acDocEd.GetSelection(acSelFtr);

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

                FillAPsList(acSSPrompt);

            }
            else
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
            }
        }

        public static void FillAPsList(PromptSelectionResult acSSPrompt)
        {
            LstAtts_APs.Clear();
            LstAtts_Speaker.Clear();
            LstAtts_Drop.Clear();

            if (acSSPrompt == null | acSSPrompt.Status == PromptStatus.Error)
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
            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;

            using (doc.LockDocument())
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                foreach (ObjectId id1 in ids)
                {
                    BlockReference blkRef = (BlockReference)transaction.GetObject(id1, OpenMode.ForRead);

                    string nam = "";
                    nam = blkRef.Name;

                    if (blkRef.IsDynamicBlock)
                    {
                        BlockTableRecord relName = (BlockTableRecord)transaction.GetObject(blkRef.DynamicBlockTableRecord, OpenMode.ForRead);
                        nam = relName.Name;
                    }

                    if (nam.Contains("TVS-I") | nam.Contains("TVS-E"))
                    {
                        AttsTvsToLst(transaction, blkRef);
                    }

                    if (nam == "T-SPK Speaker-Paging" | nam == "T-SPK Speaker-Music")
                    {
                        AttsSpkrToLst(transaction, blkRef);
                    }

                    if (nam == "T1-COM-DATA" | nam == "T4-COM-DATA")
                    {
                        AttsDropToLst(transaction, blkRef);
                    }

                }
                transaction.Commit();
            }


            LstAtts_APs.Sort((x, y) => x.CABLE_ID.CompareTo(y.CABLE_ID));
            LstAtts_Speaker.Sort((x, y) => x.ZONE.CompareTo(y.ZONE));

            LstAtts_Drop.Sort((x, y) => x.CABLE_ID.CompareTo(y.CABLE_ID));
        }

        private static void AttsDropToLst(Transaction transaction, BlockReference blkRef)
        {
            var cls = new Cls_TGT_Drop();
            cls.Handle = blkRef.Handle;
            cls.InsertionPtOfBlock = blkRef.Position;
            cls.BlockName = blkRef.Name;
            LstAtts_Drop.Add(cls);

            AttributeCollection attCol = blkRef.AttributeCollection;

            foreach (ObjectId attId in attCol)
            {
                AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForRead);

                switch (attRef.Tag)
                {
                    case "CABLE-ID":
                        cls.CABLE_ID = attRef.TextString;
                        break;
                    case "IDF":
                        cls.IDF = attRef.TextString;
                        break;

                    case "CABLE-1":
                        cls.CABLE_1 = attRef.TextString;
                        break;
                    case "CABLE-2":
                        cls.CABLE_2 = attRef.TextString;
                        break;
                    case "DESCRIPTION":
                        if (attRef.TextString != null & attRef.TextString != "")
                        {
                            cls.DESCRIPTION = attRef.TextString;
                        }
                        else
                        {
                            cls.DESCRIPTION = "...";
                        }
                        break;
                    case "OWNER":
                        if (attRef.TextString != null & attRef.TextString != "")
                        {
                            cls.OWNER = attRef.TextString;
                        }
                        else
                        {
                            cls.OWNER = "...";
                        }
                        break;

                    case "VLAN":
                        cls.VLAN = attRef.TextString;
                        break;

                    case "PHONE-TYPE":
                        cls.PHONE_TYPE = attRef.TextString;
                        break;
                    case "PHONE-EXTENSION":
                        cls.PHONE_EXTENSION = attRef.TextString;
                        break;

                    default:
                        break;
                }

            }
        }

        private static void AttsSpkrToLst(Transaction transaction, BlockReference blkRef)
        {
            var cls = new Cls_TGT_Speaker();
            cls.Handle = blkRef.Handle;
            cls.InsertionPtOfBlock = blkRef.Position;
            cls.BlockName = blkRef.Name;
            LstAtts_Speaker.Add(cls);

            AttributeCollection attCol = blkRef.AttributeCollection;

            foreach (ObjectId attId in attCol)
            {
                AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForRead);

                switch (attRef.Tag)
                {
                    case "MOUNT_TYPE":
                        cls.MOUNT_TYPE = attRef.TextString;
                        break;
                    case "ITEM_PART_#":
                        cls.ITEM_PART_NUM = attRef.TextString;
                        break;
                    case "ITEM_DESCRIPTION":
                        cls.ITEM_DESCRIPTION = attRef.TextString;
                        break;
                    case "COLOR_CODE":
                        cls.COLOR_CODE = attRef.TextString;
                        break;
                    case "COLOR_NAME":
                        cls.COLOR_NAME = attRef.TextString;
                        break;
                    case "SPEAKER_WATTAGE":
                        cls.SPEAKER_WATTAGE = attRef.TextString;
                        break;
                    case "ZONE":
                        cls.ZONE = attRef.TextString;
                        break;

                    default:
                        break;
                }

            }
        }

        private static void AttsTvsToLst(Transaction transaction, BlockReference blkRef)
        {
            var cls = new Cls_TGT_Aps();
            cls.Handle = blkRef.Handle;
            cls.InsertionPtOfBlock = blkRef.Position;
            cls.BlockName = blkRef.Name;
            LstAtts_APs.Add(cls);

            AttributeCollection attCol = blkRef.AttributeCollection;

            foreach (ObjectId attId in attCol)
            {
                AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForRead);

                switch (attRef.Tag)
                {
                    case "CABLE-ID":
                        cls.CABLE_ID = attRef.TextString;
                        break;
                    case "IDF":
                        cls.IDF = attRef.TextString;
                        break;
                    case "ASSEMBLY-TYPE":
                        cls.ASSEMBLY_TYPE = attRef.TextString;
                        break;
                    case "CSKU":
                        cls.CSKU = attRef.TextString;
                        break;
                    case "CABLE-1":
                        cls.CABLE_1 = attRef.TextString;
                        break;
                    case "CABLE-2":
                        cls.CABLE_2 = attRef.TextString;
                        break;
                    case "DESCRIPTION":
                        if (attRef.TextString != null & attRef.TextString != "")
                        {
                            cls.DESCRIPTION = attRef.TextString;
                        }
                        else
                        {
                            cls.DESCRIPTION = "...";
                        }
                        break;
                    case "OWNER":
                        if (attRef.TextString != null & attRef.TextString != "")
                        {
                            cls.OWNER = attRef.TextString;
                        }
                        else
                        {
                            cls.OWNER = "...";
                        }
                        break;

                    default:
                        break;
                }

            }
        }

        public static void CountsForTables()
        {
            LstTbl_Counts.Clear();
            LstTbl_VoiceAndData.Clear();
            LstTbl_VoiceAndDataT4.Clear();
            LstTbl_VoiceAndDataTVS.Clear();
            LstTbl_PagingCounts.Clear();

            FillTVS();
            FillSpeaker();

            FillVoiceAndData();
            FillVoiceAndDataT4();
        }


        private static void FillVoiceAndDataT4()
        {
            List<Cls_TGT_Drop> lst = LstAtts_Drop.Where(x => x.BlockName == "T4-COM-DATA").ToList();
            lst.Sort((x, y) => x.CABLE_ID.CompareTo(y.CABLE_ID));

            foreach (var v in lst)
            {
                Cls_TGT_TblVoiceAndDataT4 cls = new Cls_TGT_TblVoiceAndDataT4();

                cls.Cable_Number = v.CABLE_ID;
                cls.IDF = v.IDF;
                cls.Owner = v.OWNER;
                cls.Phone_Extension = v.PHONE_EXTENSION;
                cls.Phone_Type = v.PHONE_TYPE;
                LstTbl_VoiceAndDataT4.Add(cls);
            }


        }

        private static void FillVoiceAndData()
        {
            List<Cls_TGT_Drop> lst = LstAtts_Drop.Where(x => x.BlockName == "T1-COM-DATA").ToList();
            lst.Sort((x, y) => x.CABLE_ID.CompareTo(y.CABLE_ID));

            foreach (var v in lst)
            {
                Cls_TGT_TblVoiceAndData cls = new Cls_TGT_TblVoiceAndData();

                cls.Cable_Number = v.CABLE_ID;
                cls.IDF = v.IDF;
                cls.Owner = v.OWNER;
                cls.Phone_Extension = v.PHONE_EXTENSION;
                cls.Phone_Type = v.PHONE_TYPE;
                LstTbl_VoiceAndData.Add(cls);
            }
        }


        private static void FillSpeaker()
        {
            // what if more than one type???
            var prt = LstAtts_Speaker.Select(x => x.ITEM_PART_NUM).Distinct().ToList();
            var typ = LstAtts_Speaker.Select(x => x.ITEM_DESCRIPTION).Distinct().ToList();

            var cnt = LstAtts_Speaker.Count();

            Cls_TGT_TblPagingCounts cls = new Cls_TGT_TblPagingCounts();

            cls.Counts = cnt.ToString();

            // added count check (if no speakers exist, program didn't complete)
            if (prt.Count > 0 & typ.Count > 0)
            {
                cls.Item_Part_Number = prt[0];
                cls.Item_Description = typ[0];
            }
            else
            {
                cls.Item_Part_Number = "None";
                cls.Item_Description = "None";
            }


            LstTbl_PagingCounts.Add(cls);
        }

        private static void FillTVS()
        {
            //tvs
            // count dummies with different 
            List<Cls_TGT_Aps> Dmys = LstAtts_APs.Where(x => x.CABLE_ID == "").ToList();

            // what if more than one type???
            var prt = Dmys.Select(x => x.CSKU).Distinct().ToList();
            var typ = Dmys.Select(x => x.ASSEMBLY_TYPE).Distinct().ToList();

            foreach (string d in prt)
            {
                Cls_TGT_TblVoiceAndDataTVS cls = new Cls_TGT_TblVoiceAndDataTVS();

                // remove
                //    int cnt = LstAtts_APs.Select(x => x.CSKU = d).Count();

                cls.Count = Dmys.Count().ToString();
                cls.CSKU = d;
                cls.Assembly_Type = typ[0].ToString();
                cls.Cable_1 = "";
                cls.Cable_2 = "";
                cls.Cable_Number = "";
                cls.IDF = "";

                LstTbl_VoiceAndDataTVS.Add(cls);
            }

            List<Cls_TGT_Aps> tvs = LstAtts_APs.Where(x => x.CABLE_ID != "").ToList();
            tvs.Sort((x, y) => x.CABLE_ID.CompareTo(y.CABLE_ID));

            foreach (var v in tvs)
            {
                Cls_TGT_TblVoiceAndDataTVS cls = new Cls_TGT_TblVoiceAndDataTVS();

                cls.Count = "1";
                cls.CSKU = v.CSKU;
                cls.Assembly_Type = v.ASSEMBLY_TYPE;
                cls.Cable_1 = v.CABLE_1;
                cls.Cable_2 = v.CABLE_2;
                cls.Cable_Number = v.CABLE_ID;
                cls.IDF = v.IDF;

                LstTbl_VoiceAndDataTVS.Add(cls);
            }
        }




        #region define autocad tables and insert with utility function

        public static void TGT_InsertTable_BOM(int tabIndx)
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            PromptPointResult pr = ed.GetPoint("\nEnter table insertion point: ");

            if (pr.Status == PromptStatus.OK)
            {
                if (tabIndx == 0)
                {
                    Tbl_CountsTable(doc, db, pr);
                }
                if (tabIndx == 1)
                {
                    Tbl_VoiceAndDataTable(doc, db, pr);
                }
                if (tabIndx == 2)
                {
                    Tbl_VoiceAndDataTableT4(doc, db, pr);
                }
                if (tabIndx == 3)
                {
                    Tbl_VoiceAndDataTableTVS(doc, db, pr);
                }
                if (tabIndx == 4)
                {
                    Tbl_PagingCountsTable(doc, db, pr);
                }
            }

        }

        private static void Tbl_CountsTable(Document doc, Autodesk.AutoCAD.DatabaseServices.Database db, PromptPointResult pr)
        {
            Autodesk.AutoCAD.DatabaseServices.Table tb = new Autodesk.AutoCAD.DatabaseServices.Table();

            tb.TableStyle = db.Tablestyle;
            tb.SetSize(LstTbl_Counts.Count + 2, 5);
            tb.Position = pr.Value;

            // title
            tb.Cells[0, 0].TextString = "Counts";

            // headers
            tb.Cells[1, 0].TextString = "Count";
            tb.Cells[1, 1].TextString = "Name";
            tb.Cells[1, 2].TextString = "IDF";
            tb.Cells[1, 3].TextString = "Distance Of\r\nRun";
            tb.Cells[1, 4].TextString = "END";

            // title
            tb.Cells[0, 0].TextHeight = 0.1563;

            // headers
            tb.Cells[1, 0].TextHeight = 0.095;
            tb.Cells[1, 1].TextHeight = 0.095;
            tb.Cells[1, 2].TextHeight = 0.095;
            tb.Cells[1, 3].TextHeight = 0.095;
            tb.Cells[1, 4].TextHeight = 0.095;

            // col widths
            tb.Columns[0].Width = 0.4296;
            tb.Columns[1].Width = 0.6140;
            tb.Columns[2].Width = 0.4466;
            tb.Columns[3].Width = 0.9421;
            tb.Columns[4].Width = 0.4209;

            int row = 2;

            foreach (var v in LstTbl_Counts)
            {
                tb.Cells[row, 0].TextString = v.Count;
                tb.Cells[row, 0].TextHeight = 0.0703;
                tb.Cells[row, 0].Alignment = CellAlignment.MiddleCenter;

                tb.Cells[row, 1].TextString = v.Name;
                tb.Cells[row, 1].TextHeight = 0.0703;
                tb.Cells[row, 1].Alignment = CellAlignment.MiddleCenter;

                tb.Cells[row, 2].TextString = v.IDF;
                tb.Cells[row, 2].TextHeight = 0.0703;
                tb.Cells[row, 2].Alignment = CellAlignment.MiddleCenter;

                tb.Cells[row, 3].TextString = v.Distance_Of_Run;
                tb.Cells[row, 3].TextHeight = 0.0703;
                tb.Cells[row, 3].Alignment = CellAlignment.MiddleCenter;

                tb.Cells[row, 4].TextString = v.END;
                tb.Cells[row, 4].TextHeight = 0.0703;
                tb.Cells[row, 4].Alignment = CellAlignment.MiddleCenter;

                row++;
            }

            //// title
            //ObjectId id = GetTextStyle("LEGEND", doc, db);

            //if (!id.IsNull)
            //{
            //    tb.Cells[0, 0].TextStyleId = id;
            //}

            //tb.Cells[0, 0].TextHeight = 3 / 16;

            tb.GenerateLayout();

            BW.Cls_BW_Utility.InsertTable(doc, tb);
        }

        private static void Tbl_VoiceAndDataTable(Document doc, Autodesk.AutoCAD.DatabaseServices.Database db, PromptPointResult pr)
        {
            Autodesk.AutoCAD.DatabaseServices.Table tb = new Autodesk.AutoCAD.DatabaseServices.Table();

            tb.TableStyle = db.Tablestyle;
            tb.SetSize(LstTbl_VoiceAndData.Count + 2, 5);
            tb.Position = pr.Value;

            // title
            tb.Cells[0, 0].TextString = "Voice & Data";

            // headers
            tb.Cells[1, 0].TextString = "IDF";
            tb.Cells[1, 1].TextString = "Cable\r\nNumber";
            tb.Cells[1, 2].TextString = "Phone\r\nExtension";
            tb.Cells[1, 3].TextString = "Owner";
            tb.Cells[1, 4].TextString = "Phone\r\nType";

            // title
            tb.Cells[0, 0].TextHeight = 0.1563;

            // headers
            tb.Cells[1, 0].TextHeight = 0.095;
            tb.Cells[1, 1].TextHeight = 0.095;
            tb.Cells[1, 2].TextHeight = 0.095;
            tb.Cells[1, 3].TextHeight = 0.095;
            tb.Cells[1, 4].TextHeight = 0.095;

            // col widths
            tb.Columns[0].Width = 0.375;
            tb.Columns[1].Width = 0.5156;
            tb.Columns[2].Width = 0.6163;
            tb.Columns[3].Width = 0.8862;
            tb.Columns[4].Width = 0.4601;

            int row = 2;

            foreach (var v in LstTbl_VoiceAndData)
            {
                tb.Cells[row, 0].TextString = v.IDF;
                tb.Cells[row, 0].TextHeight = 0.0703;
                tb.Cells[row, 0].Alignment = CellAlignment.MiddleCenter;

                tb.Cells[row, 1].TextString = v.Cable_Number;
                tb.Cells[row, 1].TextHeight = 0.0703;
                tb.Cells[row, 1].Alignment = CellAlignment.MiddleCenter;

                tb.Cells[row, 2].TextString = v.Phone_Extension;
                tb.Cells[row, 2].TextHeight = 0.0703;
                tb.Cells[row, 2].Alignment = CellAlignment.MiddleCenter;

                tb.Cells[row, 3].TextString = v.Owner;
                tb.Cells[row, 3].TextHeight = 0.0703;
                tb.Cells[row, 3].Alignment = CellAlignment.MiddleCenter;

                tb.Cells[row, 4].TextString = v.Phone_Type;
                tb.Cells[row, 4].TextHeight = 0.0703;
                tb.Cells[row, 4].Alignment = CellAlignment.MiddleCenter;

                row++;
            }

            tb.GenerateLayout();

            BW.Cls_BW_Utility.InsertTable(doc, tb);
        }

        private static void Tbl_VoiceAndDataTableT4(Document doc, Autodesk.AutoCAD.DatabaseServices.Database db, PromptPointResult pr)
        {
            Autodesk.AutoCAD.DatabaseServices.Table tb = new Autodesk.AutoCAD.DatabaseServices.Table();

            tb.TableStyle = db.Tablestyle;
            tb.SetSize(LstTbl_VoiceAndDataT4.Count + 2, 5);
            tb.Position = pr.Value;

            // title
            tb.Cells[0, 0].TextString = "T4 Voice & Data";

            // headers
            tb.Cells[1, 0].TextString = "IDF";
            tb.Cells[1, 1].TextString = "Cable\r\nNumber";
            tb.Cells[1, 2].TextString = "Phone\r\nExtension";
            tb.Cells[1, 3].TextString = "Owner";
            tb.Cells[1, 4].TextString = "Phone\r\nType";

            // title
            tb.Cells[0, 0].TextHeight = 0.1563;

            // headers
            tb.Cells[1, 0].TextHeight = 0.095;
            tb.Cells[1, 1].TextHeight = 0.095;
            tb.Cells[1, 2].TextHeight = 0.095;
            tb.Cells[1, 3].TextHeight = 0.095;
            tb.Cells[1, 4].TextHeight = 0.095;

            // col widths
            tb.Columns[0].Width = 0.4680;
            tb.Columns[1].Width = 0.5156;
            tb.Columns[2].Width = 0.6163;
            tb.Columns[3].Width = 2.0475;
            tb.Columns[4].Width = 0.4481;

            int row = 2;

            foreach (var v in LstTbl_VoiceAndDataT4)
            {
                tb.Cells[row, 0].TextString = v.IDF;
                tb.Cells[row, 0].TextHeight = 0.0703;
                tb.Cells[row, 0].Alignment = CellAlignment.MiddleCenter;

                tb.Cells[row, 1].TextString = v.Cable_Number;
                tb.Cells[row, 1].TextHeight = 0.0703;
                tb.Cells[row, 1].Alignment = CellAlignment.MiddleCenter;

                tb.Cells[row, 2].TextString = v.Phone_Extension;
                tb.Cells[row, 2].TextHeight = 0.0703;
                tb.Cells[row, 2].Alignment = CellAlignment.MiddleCenter;

                tb.Cells[row, 3].TextString = v.Owner;
                tb.Cells[row, 3].TextHeight = 0.0703;
                tb.Cells[row, 3].Alignment = CellAlignment.MiddleCenter;

                tb.Cells[row, 4].TextString = v.Phone_Type;
                tb.Cells[row, 4].TextHeight = 0.0703;
                tb.Cells[row, 4].Alignment = CellAlignment.MiddleCenter;

                row++;
            }

            tb.GenerateLayout();

            BW.Cls_BW_Utility.InsertTable(doc, tb);
        }

        private static void Tbl_VoiceAndDataTableTVS(Document doc, Autodesk.AutoCAD.DatabaseServices.Database db, PromptPointResult pr)
        {
            Autodesk.AutoCAD.DatabaseServices.Table tb = new Autodesk.AutoCAD.DatabaseServices.Table();

            tb.TableStyle = db.Tablestyle;
            tb.SetSize(LstTbl_VoiceAndDataTVS.Count + 2, 7);
            tb.Position = pr.Value;

            tb.Cells[0, 0].TextString = "TVS Counts";

            // headers
            tb.Cells[1, 0].TextString = "Count";
            tb.Cells[1, 1].TextString = "IDF";
            tb.Cells[1, 2].TextString = "Cable\r\nNumber";
            tb.Cells[1, 3].TextString = "Assembly Type";
            tb.Cells[1, 4].TextString = "CSKU";
            tb.Cells[1, 5].TextString = "Cable\r\n1";
            tb.Cells[1, 6].TextString = "Cable\r\n2";

            // title
            tb.Cells[0, 0].TextHeight = 0.0996;

            // headers
            tb.Cells[1, 0].TextHeight = 0.0623;
            tb.Cells[1, 1].TextHeight = 0.0623;
            tb.Cells[1, 2].TextHeight = 0.0623;
            tb.Cells[1, 3].TextHeight = 0.0623;
            tb.Cells[1, 4].TextHeight = 0.0623;
            tb.Cells[1, 5].TextHeight = 0.0623;
            tb.Cells[1, 6].TextHeight = 0.0623;

            // col widths
            tb.Columns[0].Width = 0.2934;
            tb.Columns[1].Width = 0.2492;
            tb.Columns[2].Width = 0.3797;
            tb.Columns[3].Width = 1.1002;
            tb.Columns[4].Width = 0.8539;
            tb.Columns[5].Width = 0.2894;
            tb.Columns[6].Width = 0.2926;

            int row = 2;

            foreach (var v in LstTbl_VoiceAndDataTVS)
            {
                tb.Cells[row, 0].TextString = v.Count;
                tb.Cells[row, 0].TextHeight = 0.0448;
                tb.Cells[row, 0].Alignment = CellAlignment.MiddleCenter;

                tb.Cells[row, 1].TextString = v.IDF;
                tb.Cells[row, 1].TextHeight = 0.0448;
                tb.Cells[row, 1].Alignment = CellAlignment.MiddleCenter;

                tb.Cells[row, 2].TextString = v.Cable_Number;
                tb.Cells[row, 2].TextHeight = 0.0448;
                tb.Cells[row, 2].Alignment = CellAlignment.MiddleCenter;

                tb.Cells[row, 3].TextString = v.Assembly_Type;
                tb.Cells[row, 3].TextHeight = 0.0448;
                tb.Cells[row, 3].Alignment = CellAlignment.MiddleCenter;

                tb.Cells[row, 4].TextString = v.CSKU;
                tb.Cells[row, 4].TextHeight = 0.0448;
                tb.Cells[row, 4].Alignment = CellAlignment.MiddleCenter;

                tb.Cells[row, 5].TextString = v.Cable_1;
                tb.Cells[row, 5].TextHeight = 0.0448;
                tb.Cells[row, 5].Alignment = CellAlignment.MiddleCenter;

                tb.Cells[row, 6].TextString = v.Cable_2;
                tb.Cells[row, 6].TextHeight = 0.0448;
                tb.Cells[row, 6].Alignment = CellAlignment.MiddleCenter;

                row++;
            }

            tb.GenerateLayout();

            BW.Cls_BW_Utility.InsertTable(doc, tb);
        }

        private static void Tbl_PagingCountsTable(Document doc, Autodesk.AutoCAD.DatabaseServices.Database db, PromptPointResult pr)
        {
            Autodesk.AutoCAD.DatabaseServices.Table tb = new Autodesk.AutoCAD.DatabaseServices.Table();

            tb.TableStyle = db.Tablestyle;
            tb.SetSize(LstTbl_PagingCounts.Count + 2, 3);
            tb.Position = pr.Value;

            // title
            tb.Cells[0, 0].TextString = "Paging Counts";

            // headers
            tb.Cells[1, 0].TextString = "Count";
            tb.Cells[1, 1].TextString = "Item Part #";
            tb.Cells[1, 2].TextString = "Item Description";

            // title
            tb.Cells[0, 0].TextHeight = 0.1563;

            // headers
            tb.Cells[1, 0].TextHeight = 0.095;
            tb.Cells[1, 1].TextHeight = 0.095;
            tb.Cells[1, 2].TextHeight = 0.095;

            // col widths
            tb.Columns[0].Width = 0.5594;
            tb.Columns[1].Width = 0.9976;
            tb.Columns[2].Width = 1.3646;

            int row = 2;

            foreach (var v in LstTbl_PagingCounts)
            {
                tb.Cells[row, 0].TextString = v.Counts;
                tb.Cells[row, 0].TextHeight = 0.0703;
                tb.Cells[row, 0].Alignment = CellAlignment.MiddleCenter;

                tb.Cells[row, 1].TextString = v.Item_Part_Number;
                tb.Cells[row, 1].TextHeight = 0.0703;
                tb.Cells[row, 1].Alignment = CellAlignment.MiddleCenter;

                tb.Cells[row, 2].TextString = v.Item_Description;
                tb.Cells[row, 2].TextHeight = 0.0703;
                tb.Cells[row, 2].Alignment = CellAlignment.MiddleCenter;

                row++;
            }

            tb.GenerateLayout();

            BW.Cls_BW_Utility.InsertTable(doc, tb);
        }

        #endregion



        /// <summary>
        /// kwik fix for an asbuilt labeling project
        /// </summary>
        /// <param name="txtBx_NumToStartAt"></param>
        public static void UpdatePrintIdAttribute(String Prefix, TextBox txtBx_NumToStartAt)
        {
            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            PromptEntityOptions p = new PromptEntityOptions("Select Print ID Attribute");

            PromptEntityResult prmt;

            do
            {
                prmt = ed.GetEntity(p);
                {
                    if (prmt.ObjectId != ObjectId.Null)
                    {
                        using (doc.LockDocument())
                        using (Transaction tr = database.TransactionManager.StartTransaction())
                        {
                            BlockReference blkRef1 = (BlockReference)tr.GetObject(prmt.ObjectId, OpenMode.ForRead);
                            Autodesk.AutoCAD.DatabaseServices.AttributeCollection attCol = blkRef1.AttributeCollection;

                            int strt = int.Parse(txtBx_NumToStartAt.Text);

                            foreach (ObjectId attId in attCol)
                            {
                                AttributeReference attRef = (AttributeReference)tr.GetObject(attId, OpenMode.ForWrite);//attRef.UpgradeOpen();

                                switch (attRef.Tag)
                                {                                   
                                    case "Print_ID":
                                        attRef.TextString = Prefix + " " + strt.ToString().PadLeft(3, '0');
                                        strt = strt + 1;
                                        break;

                                        //default:
                                        //    break;

                                }
                            }

                            tr.Commit();
                            txtBx_NumToStartAt.Text = strt.ToString();
                        }
                    }
                }
            } while (prmt.Status == PromptStatus.OK);


        }

    }


}


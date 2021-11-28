using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

namespace MyFirstProject.BW
{
    public static class Cls_BW_TP_WAOs
    {


        public static void BtnAutoLabelWAOsAllSelected_Click_Sub(DataGridView dgvWaos, TextBox txtBxWaoAutoNum_NumToStartAt, bool chkBxRemNumsFromWAOs)
        {
            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                             
            int strt = int.Parse(txtBxWaoAutoNum_NumToStartAt.Text);

            int i;

            using (doc.LockDocument())
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                for (i = dgvWaos.CurrentRow.Index; i < dgvWaos.RowCount; i++)
                {
                    ObjectId idD = Cls_BW_Utility.ObjectIDFromHandle(database, dgvWaos[9, i].Value.ToString());

                    BlockReference blkRef1 = (BlockReference)transaction.GetObject(idD, OpenMode.ForRead);
                    Autodesk.AutoCAD.DatabaseServices.AttributeCollection attCol = blkRef1.AttributeCollection;

                    foreach (ObjectId attId in attCol)
                    {
                        AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForWrite);//attRef.UpgradeOpen();

                        switch (attRef.Tag)
                        {
                            case "LABEL1":

                                var splt = attRef.TextString.Split('/');

                                int cnt = 0;

                                for (int ii = 0; ii < splt.Length; ii++)
                                {
                                    if (splt[ii] != "")
                                    {
                                        cnt++;
                                    }
                                }

                                if (chkBxRemNumsFromWAOs)
                                {
                                    if (cnt == 1)
                                    {
                                        attRef.TextString = "XXX";
                                    }

                                    if (cnt == 2)
                                    {
                                        attRef.TextString = "XXX/XXX";
                                    }

                                    if (cnt == 3)
                                    {
                                        attRef.TextString = "XXX/XXX/XXX";
                                    }

                                    if (cnt == 4)
                                    {
                                        attRef.TextString = "XXX/XXX/XXX/XXX";
                                    }

                                    if (cnt == 5)
                                    {
                                        attRef.TextString = "XXX/XXX/XXX/XXX/XXX";
                                    }

                                    if (cnt == 6)
                                    {
                                        attRef.TextString = "XXX/XXX/XXX/XXX/XXX/XXX";
                                    }
                                }
                                else
                                {
                                    if (cnt == 1)
                                    {
                                        attRef.TextString =
                                             strt.ToString().PadLeft(3, '0');
                                        strt = strt + 1;
                                    }

                                    if (cnt == 2)
                                    {
                                        attRef.TextString =
                                            strt.ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 1).ToString().PadLeft(3, '0');
                                        strt = strt + 2;
                                    }

                                    if (cnt == 3)
                                    {
                                        attRef.TextString =
                                            strt.ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 1).ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 2).ToString().PadLeft(3, '0');
                                        strt = strt + 3;
                                    }

                                    if (cnt == 4)
                                    {
                                        attRef.TextString =
                                            strt.ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 1).ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 2).ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 3).ToString().PadLeft(3, '0');
                                        strt = strt + 4;
                                    }

                                    if (cnt == 5)
                                    {
                                        attRef.TextString =
                                            strt.ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 1).ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 2).ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 3).ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 4).ToString().PadLeft(3, '0');
                                        strt = strt + 5;
                                    }

                                    if (cnt == 6)
                                    {
                                        attRef.TextString =
                                            strt.ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 1).ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 2).ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 3).ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 4).ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 5).ToString().PadLeft(3, '0');
                                        strt = strt + 6;
                                    }
                                }
                                break;

                            default:
                                break;
                        }
                    }


                }
           //     txtBxWaoAutoNum_NumToStartAt.Text = "1"; // strt.ToString();
          //      txtBxWaoAutoNum_NumToStartAt.Refresh();

                transaction.Commit();

                //Cls_MMM_Main.LstAtts_waos.Clear();

                //Cls_MMM_Main.FillWAOsList(LstObjectId.ToArray(), false);

                //dgvWaos.DataSource = null;
                //dgvWaos.DataSource = Cls_MMM_Main.LstAtts_waos;
                //dgvWaos.AutoResizeColumns();
                //dgvWaoTypeFilter.SelectionChanged -= new System.EventHandler(dgvWaoTypeFilter_SelectionChanged);
                //dgvWaoTypeFilter.ClearSelection();
                //dgvWaoTypeFilter.SelectionChanged += new System.EventHandler(dgvWaoTypeFilter_SelectionChanged);
            }
        }



        public static void BtnAutoLabelWAOs_Click_Sub(DataGridView dgvWaos, TextBox txtBxWaoAutoNum_NumToStartAt, bool chkBxRemNumsFromWAOs)
        {
            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            Handle[] mainArr = Cls_BW_TP_WAOs.LstFilteredWaos.Select(x => x.Handle).ToArray();

            List<ObjectId> LstObjectId = new List<ObjectId>();


            // for refresh the grid
            foreach (Handle h in mainArr)
            {
                ObjectId id = Cls_BW_Utility.ObjectIDFromHandle(database, h.ToString());
                LstObjectId.Add(id);
            }


            int strt = int.Parse(txtBxWaoAutoNum_NumToStartAt.Text);

            int i;

            using (doc.LockDocument())
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                for (i = dgvWaos.CurrentRow.Index; i < dgvWaos.RowCount; i++)
                {
                    ObjectId idD = Cls_BW_Utility.ObjectIDFromHandle(database, dgvWaos[9, i].Value.ToString());

                    BlockReference blkRef1 = (BlockReference)transaction.GetObject(idD, OpenMode.ForRead);
                    Autodesk.AutoCAD.DatabaseServices.AttributeCollection attCol = blkRef1.AttributeCollection;

                    foreach (ObjectId attId in attCol)
                    {
                        AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForWrite);//attRef.UpgradeOpen();

                        switch (attRef.Tag)
                        {
                            case "LABEL1":

                                var splt = attRef.TextString.Split('/');

                                int cnt = 0;

                                for (int ii = 0; ii < splt.Length; ii++)
                                {
                                    if (splt[ii] != "")
                                    {
                                        cnt++;
                                    }
                                }

                                if (chkBxRemNumsFromWAOs)
                                {
                                    if (cnt == 1)
                                    {
                                        attRef.TextString = "XXX";
                                    }

                                    if (cnt == 2)
                                    {
                                        attRef.TextString = "XXX/XXX";
                                    }

                                    if (cnt == 3)
                                    {
                                        attRef.TextString = "XXX/XXX/XXX";
                                    }

                                    if (cnt == 4)
                                    {
                                        attRef.TextString = "XXX/XXX/XXX/XXX";
                                    }

                                    if (cnt == 5)
                                    {
                                        attRef.TextString = "XXX/XXX/XXX/XXX/XXX";
                                    }

                                    if (cnt == 6)
                                    {
                                        attRef.TextString = "XXX/XXX/XXX/XXX/XXX/XXX";
                                    }
                                }
                                else
                                {
                                    if (cnt == 1)
                                    {
                                        attRef.TextString =
                                             strt.ToString().PadLeft(3, '0');
                                        strt = strt + 1;
                                    }

                                    if (cnt == 2)
                                    {
                                        attRef.TextString =
                                            strt.ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 1).ToString().PadLeft(3, '0');
                                        strt = strt + 2;
                                    }

                                    if (cnt == 3)
                                    {
                                        attRef.TextString =
                                            strt.ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 1).ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 2).ToString().PadLeft(3, '0');
                                        strt = strt + 3;
                                    }

                                    if (cnt == 4)
                                    {
                                        attRef.TextString =
                                            strt.ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 1).ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 2).ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 3).ToString().PadLeft(3, '0');
                                        strt = strt + 4;
                                    }

                                    if (cnt == 5)
                                    {
                                        attRef.TextString =
                                            strt.ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 1).ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 2).ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 3).ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 4).ToString().PadLeft(3, '0');
                                        strt = strt + 5;
                                    }

                                    if (cnt == 6)
                                    {
                                        attRef.TextString =
                                            strt.ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 1).ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 2).ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 3).ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 4).ToString().PadLeft(3, '0') +
                                            "/" +
                                            (strt + 5).ToString().PadLeft(3, '0');
                                        strt = strt + 6;
                                    }
                                }
                                break;

                            default:
                                break;
                        }
                    }


                }
                txtBxWaoAutoNum_NumToStartAt.Text = "1"; // strt.ToString();
                txtBxWaoAutoNum_NumToStartAt.Refresh();

                transaction.Commit();

                //Cls_MMM_Main.LstAtts_waos.Clear();

                //Cls_MMM_Main.FillWAOsList(LstObjectId.ToArray(), false);

                //dgvWaos.DataSource = null;
                //dgvWaos.DataSource = Cls_MMM_Main.LstAtts_waos;
                //dgvWaos.AutoResizeColumns();
                //dgvWaoTypeFilter.SelectionChanged -= new System.EventHandler(dgvWaoTypeFilter_SelectionChanged);
                //dgvWaoTypeFilter.ClearSelection();
                //dgvWaoTypeFilter.SelectionChanged += new System.EventHandler(dgvWaoTypeFilter_SelectionChanged);
            }
        }

        public static void BtnLabelWAOsIndividually_Click_Sub(DataGridView dgvWaos, TextBox txtBxWaoAutoNum_NumToStartAt)
        {
            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


            //Handle[] mainArr = frm_BW_MainForm_AsControl_Code.LstAtts_waos.Select(x => x.Handle).ToArray();

            List<ObjectId> LstObjectId = new List<ObjectId>();

            //foreach (Handle h in mainArr)
            //{
            //    ObjectId id = Cls_BW_Utility.ObjectIDFromHandle(database, h.ToString());
            //    LstObjectId.Add(id);
            //}

            PromptEntityOptions p = new PromptEntityOptions("Select WAO Text");

            PromptEntityResult prmt;

            do
            {
                prmt = ed.GetEntity(p);
                {
                    if (prmt.ObjectId != ObjectId.Null)
                    {
                        LstObjectId.Add(prmt.ObjectId);
                        using (doc.LockDocument())
                        using (Transaction tr = database.TransactionManager.StartTransaction())
                        {
                            BlockReference blkRef1 = (BlockReference)tr.GetObject(prmt.ObjectId, OpenMode.ForRead);
                            Autodesk.AutoCAD.DatabaseServices.AttributeCollection attCol = blkRef1.AttributeCollection;

                            int strt = int.Parse(txtBxWaoAutoNum_NumToStartAt.Text);

                            foreach (ObjectId attId in attCol)
                            {
                                AttributeReference attRef = (AttributeReference)tr.GetObject(attId, OpenMode.ForWrite);//attRef.UpgradeOpen();

                                switch (attRef.Tag)
                                {
                                    #region label
                                    case "LABEL1":

                                        var splt = attRef.TextString.Split('/');

                                        int cnt = 0;

                                        for (int ii = 0; ii < splt.Length; ii++)
                                        {
                                            if (splt[ii] != "")
                                            {
                                                cnt++;
                                            }
                                        }

                                        if (cnt == 1)
                                        {
                                            attRef.TextString =
                                                 strt.ToString().PadLeft(3, '0');
                                            strt = strt + 1;
                                        }

                                        if (cnt == 2)
                                        {
                                            attRef.TextString =
                                                strt.ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 1).ToString().PadLeft(3, '0');
                                            strt = strt + 2;
                                        }

                                        if (cnt == 3)
                                        {
                                            attRef.TextString =
                                                strt.ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 1).ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 2).ToString().PadLeft(3, '0');
                                            strt = strt + 3;
                                        }

                                        if (cnt == 4)
                                        {
                                            attRef.TextString =
                                                strt.ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 1).ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 2).ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 3).ToString().PadLeft(3, '0');
                                            strt = strt + 4;
                                        }

                                        if (cnt == 5)
                                        {
                                            attRef.TextString =
                                                strt.ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 1).ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 2).ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 3).ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 4).ToString().PadLeft(3, '0');
                                            strt = strt + 5;
                                        }

                                        if (cnt == 6)
                                        {
                                            attRef.TextString =
                                                strt.ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 1).ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 2).ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 3).ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 4).ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 5).ToString().PadLeft(3, '0');
                                            strt = strt + 6;
                                        }

                                        break;

                                    default:
                                        break;
                                        #endregion
                                }
                            }

                            tr.Commit();
                            txtBxWaoAutoNum_NumToStartAt.Text = strt.ToString();
                        }
                    }
                }

            } while (prmt.Status == PromptStatus.OK);

            Cls_BW_TP_WAOs.LstAtts_waos.Clear();

            Cls_BW_TP_WAOs.FillWAOsList(Cls_BW_TP_Common.acSSPrompt, false);

            dgvWaos.DataSource = null;
            dgvWaos.DataSource = Cls_BW_TP_WAOs.LstAtts_waos;
            dgvWaos.Refresh();
        }

        #region closet lists

        public static readonly List<Cls_BW_TP_Common.ClsClosetFilterList> LstWAOsClosetFilter = new List<Cls_BW_TP_Common.ClsClosetFilterList>();

        public static void GetLstClosetFilterWAOs()
        {
            LstWAOsClosetFilter.Clear();

            var DistinctClosetNames = Cls_BW_TP_WAOs.LstAtts_waos.Select(x => x.ClosetInfo).Distinct().ToList();
            List<Cls_BW_TP_Common.ClsClosetFilterList> tmpLst = new List<Cls_BW_TP_Common.ClsClosetFilterList>();

            foreach (string s in DistinctClosetNames)
            {
                var newS = s;

                if (!s.Contains('-'))
                {
                    newS += "-0";
                }

                //string txt = Cls_BW_TP_WAOs.LstAtts_waos.Count(x => x.ClosetInfo == s).ToString();
                Cls_BW_TP_Common.ClsClosetFilterList l = new Cls_BW_TP_Common.ClsClosetFilterList
                {
                    //Count = txt,
                    Closet = s,
                    //ClosetFormatSort = (new String(s.Where(Char.IsDigit).ToArray())).PadLeft(4, '0')

                    ClosetFormatSort = newS.PadLeft(6, '0')
                };
                tmpLst.Add(l);
            }

            var tr = tmpLst.Where(x => x.Closet.StartsWith("TR")).ToList();
            tr.Sort((x, y) => x.ClosetFormatSort.CompareTo(y.ClosetFormatSort));
            var tc = tmpLst.Where(x => x.Closet.StartsWith("TC")).ToList();
            tc.Sort((x, y) => x.ClosetFormatSort.CompareTo(y.ClosetFormatSort));

            LstWAOsClosetFilter.AddRange(tr);
            LstWAOsClosetFilter.AddRange(tc);
        }

        public static readonly List<Cls_BW_TP_Common.ClsWaoFilterList> LstWaoBlockNames = new List<Cls_BW_TP_Common.ClsWaoFilterList>();

        public static void GetLstFilterWAOsTypeOfBlock()
        {
            var DistinctBlockNames = Cls_BW_TP_WAOs.LstAtts_waos.Select(x => x.BlockName).Distinct().ToList();
            LstWaoBlockNames.Clear();

            foreach (string s in DistinctBlockNames)
            {
                string txt = Cls_BW_TP_WAOs.LstAtts_waos.Count(x => x.BlockName == s).ToString().PadLeft(4, '0');
                Cls_BW_TP_Common.ClsWaoFilterList l = new Cls_BW_TP_Common.ClsWaoFilterList
                {
                    Count = txt,
                    BlkName = s
                };
                LstWaoBlockNames.Add(l);
            }
        }

        #endregion


        public static readonly List<Cls_BW_Waos_Atts> LstAtts_waos = new List<Cls_BW_Waos_Atts>();
        public static readonly List<Cls_BW_Waos_Atts> LstAtts_waos_NotCountedOnBom = new List<Cls_BW_Waos_Atts>();

        // filtered list WAO's
        public static readonly List<Cls_BW_Waos_Atts> LstFilteredWaos = new List<Cls_BW_Waos_Atts>();



        public static void FillWAOsList(PromptSelectionResult acSSPrompt, bool ShowDistUprLeft)
        {
            if (acSSPrompt == null || acSSPrompt.Status == PromptStatus.Error)
                return;

            LstAtts_waos.Clear();

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

            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            using (doc.LockDocument())
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                LayerTable lt = (LayerTable)transaction.GetObject(database.LayerTableId, OpenMode.ForRead);

                if (!lt.Has("_Runs"))
                {
                    LayerTableRecord ltr = new LayerTableRecord();
                    ltr.Name = "_Runs";
                    lt.UpgradeOpen();
                    lt.Add(ltr);
                    transaction.AddNewlyCreatedDBObject(ltr, true);
                }

                if (!lt.Has("_WaoULdist"))
                {
                    LayerTableRecord ltr = new LayerTableRecord();
                    ltr.Name = "_WaoULdist";
                    lt.UpgradeOpen();
                    lt.Add(ltr);
                    transaction.AddNewlyCreatedDBObject(ltr, true);
                }

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

                    switch (nam)
                    {
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

                            var clsWAOsAtts = new Cls_BW_Waos_Atts();
                            clsWAOsAtts.Handle = blkRef.Handle;
                            clsWAOsAtts.BlockName = nam;
                            clsWAOsAtts.InsertionPtOfBlock = blkRef.Position;


                            LstAtts_waos.Add(clsWAOsAtts);

                            AttributeCollection attCol = blkRef.AttributeCollection;

                            List<AttributeReference> WapAtts = new List<AttributeReference>();

                            foreach (ObjectId attId in attCol)
                            {
                                AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForRead);
                                WapAtts.Add(attRef);
                            }

                            Cls_BW_BlockAttsGetWao.GetWaoAttributes(clsWAOsAtts, WapAtts);
                    
                            break;
                    }
                }
                //TODO: Cls_BW_TP_Common.CalClosetDistAndPLine(ShowDistUprLeft, database, transaction);
                //if (Cls_BW_TP_WAOs.LstAtts_waos.Count != 0)
                //{
                //    Cls_BW_TP_Common.CalClosetDistAndPLine(ShowDistUprLeft, database, transaction);
                //}
                transaction.Commit();
                     
            }
        }



        public static void BtnWaosRemunberAllByCloset()
        {
            int strt = 1;

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            using (doc.LockDocument())
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                foreach (var acf in LstWAOsClosetFilter)
                {
                    // original
                    var itms = LstAtts_waos
                        .Where(x => x.ClosetInfo == acf.Closet)
                        //.OrderBy(x => x.Bldg)
                        //.ThenBy(x => x.Floor)
                        //.ThenByDescending(x => x.InsertionPtOfBlock.Y)
                        //.ThenBy(x => x.InsertionPtOfBlock.X)
                        .ToList();

                    //    var itms = LstAtts_waos
                    //.Where(x => x.ClosetInfo == acf.Closet)
                    //.OrderBy(x => x.Floor)
                    //.ThenByDescending(x => x.InsertionPtOfBlock.Y)
                    //.ThenBy(x => x.InsertionPtOfBlock.X)
                    //.ToList();

                    // added
                    var bldgs = LstAtts_waos
                        .Select(x => x.Bldg)
                        .Distinct()
                        .OrderBy(s => s)
                        .ToList();
               
                    // added for bldgs
                    foreach (var bld in bldgs)
                    {
                        var itmsInBldg = itms
                            .Where(x => x.Bldg == bld)
                            .OrderBy(x => x.Bldg)
                            .ThenBy(x => x.Floor)
                            .ThenByDescending(x => x.InsertionPtOfBlock.Y)
                            .ThenBy(x => x.InsertionPtOfBlock.X)
                            .ToList();

                        foreach (var itm in itmsInBldg)
                        {
                            ObjectId idD = Cls_BW_Utility.ObjectIDFromHandle(database, itm.Handle.ToString());

                            BlockReference blkRef1 = (BlockReference)transaction.GetObject(idD, OpenMode.ForRead);

                            Autodesk.AutoCAD.DatabaseServices.AttributeCollection attCol1 = blkRef1.AttributeCollection;
                            foreach (ObjectId attId in attCol1)
                            {
                                AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForWrite);//attRef.UpgradeOpen();

                                switch (attRef.Tag)
                                {
                                    case "LABEL1":

                                        var splt = attRef.TextString.Split('/');

                                        int cntLbl = 0;

                                        for (int ii = 0; ii < splt.Length; ii++)
                                        {
                                            if (splt[ii] != "")
                                            {
                                                cntLbl++;
                                            }
                                        }

                                        if (cntLbl == 1)
                                        {
                                            attRef.TextString =
                                                 strt.ToString().PadLeft(3, '0');
                                            strt = strt + 1;
                                        }

                                        else if (cntLbl == 2)
                                        {
                                            attRef.TextString =
                                                strt.ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 1).ToString().PadLeft(3, '0');
                                            strt = strt + 2;
                                        }

                                        else if (cntLbl == 3)
                                        {
                                            attRef.TextString =
                                                strt.ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 1).ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 2).ToString().PadLeft(3, '0');
                                            strt = strt + 3;
                                        }

                                        else if (cntLbl == 4)
                                        {
                                            attRef.TextString =
                                                strt.ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 1).ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 2).ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 3).ToString().PadLeft(3, '0');
                                            strt = strt + 4;
                                        }

                                        else if (cntLbl == 5)
                                        {
                                            attRef.TextString =
                                                strt.ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 1).ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 2).ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 3).ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 4).ToString().PadLeft(3, '0');
                                            strt = strt + 5;
                                        }

                                        else if (cntLbl == 6)
                                        {
                                            attRef.TextString =
                                                strt.ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 1).ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 2).ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 3).ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 4).ToString().PadLeft(3, '0') +
                                                "/" +
                                                (strt + 5).ToString().PadLeft(3, '0');
                                            strt = strt + 6;
                                        }

                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                        strt = 1;
                    }
                   
                }
                transaction.Commit();
            }
        }


    }
}

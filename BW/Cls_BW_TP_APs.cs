using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace MyFirstProject.BW
{
    public class Cls_BW_TP_APs
    {
        public static readonly List<Cls_BW_Waps_Atts> LstAtts_wap = new List<Cls_BW_Waps_Atts>();
        public static readonly List<Cls_BW_WapsData2Dual_Atts> LstAtts_wap_data_2_dual = new List<Cls_BW_WapsData2Dual_Atts>();
        public static readonly List<Cls_BW_WapsData2Dual_Atts> LstAtts_wap_data_2_dual_NotCountedOnBom = new List<Cls_BW_WapsData2Dual_Atts>();


        // filtered list AP's
        public static readonly List<Cls_BW_WapsData2Dual_Atts> LstFilteredApsDuals = new List<Cls_BW_WapsData2Dual_Atts>();
        public static readonly List<Cls_BW_Waps_Atts> LstFilteredAps = new List<Cls_BW_Waps_Atts>();

        public static readonly List<Cls_BW_TP_Common.ClsClosetFilterList> LstAPsClosetFilter = new List<Cls_BW_TP_Common.ClsClosetFilterList>();





        public static void Gw_SelectBlocks(bool ShowDistUprLeft)
        {
            Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            SelectionFilter acSelFtr = BW.Cls_BW_SelDynBlksAlso.SelectDynamicBlocks(BW.Cls_BW_BlksToSelect.GetBlockNames());

            Cls_BW_TP_Common.acSSPrompt = acDocEd.GetSelection(acSelFtr);

            if (Cls_BW_TP_Common.acSSPrompt.Status == PromptStatus.OK)
            {
                SelectionSet acSSet = Cls_BW_TP_Common.acSSPrompt.Value;
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

                //Cls_BW_TP_APs.FillAPsList(Cls_BW_TP_Common.acSSPrompt);
                //Cls_BW_TP_WAOs.FillWAOsList(Cls_BW_TP_Common.acSSPrompt, ShowDistUprLeft);
            }
            else
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
            }
        }


        public static void Gw_SelectBlockApsInPolyline(bool RunOrientation, int MaxRunLenInFeet)
        {
            Cls_BW_TP_APs.LstAtts_wap.Clear();
            Cls_BW_TP_APs.LstAtts_wap_data_2_dual.Clear();

            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.Database db = doc.Database;
            Editor ed = doc.Editor;

            TypedValue[] acTypValAr = new TypedValue[5];
            acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "INSERT"), 0);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.Operator, "<OR"), 1);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.BlockName, "wap"), 2);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.BlockName, "wap data 2 dual"), 3);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.Operator, "OR>"), 4);

            SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);

            var options = new PromptEntityOptions("\nSelect polyline: ");

            options.SetRejectMessage("\nMust be a polyline.");

            options.AddAllowedClass(typeof(Polyline), true);

            //options.AddAllowedClass(typeof(Polyline3d), true); //??

            var result = ed.GetEntity(options);

            if (result.Status != PromptStatus.OK) return;

            using (Transaction tr = db.TransactionManager.StartOpenCloseTransaction())
            {
                Polyline pline = (Polyline)tr.GetObject(result.ObjectId, OpenMode.ForRead);

                if (Cls_BW_Utility.IsSelfIntersecting(pline))
                {
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("IsSelfIntersecting - Fix the Pline");
                    return;
                }

                Point3dCollection polygon = new Point3dCollection();
                for (int i = 0; i < pline.NumberOfVertices; i++)
                {
                    polygon.Add(pline.GetPoint3dAt(i));
                }

                PromptSelectionResult selection = ed.SelectWindowPolygon(polygon, acSelFtr);

                if (selection.Status == PromptStatus.OK)
                {
                    //ed.SetImpliedSelection(selection.Value);
                    //  FillAPsList(selection);
                }
                else
                {
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
                }

                tr.Commit();
            }
        }

        public static void APsZoomAndSelect(DataGridView dgvW, DataGridView dgvWDD, bool hlgtWaps, bool hlgtWapsD)
        {
            // both rows must be selected
            // should use the dual and select the wap??
            ObjectId idD = Cls_BW_Utility.ObjectIDFromHandle(HostApplicationServices.WorkingDatabase,
                dgvWDD[12, dgvWDD.CurrentRow.Index].Value.ToString());
            ObjectId idW = Cls_BW_Utility.ObjectIDFromHandle(HostApplicationServices.WorkingDatabase,
                dgvW[7, dgvW.CurrentRow.Index].Value.ToString());

            ObjectId[] Ids;

            if (hlgtWapsD & hlgtWaps)
            {
                Ids = new ObjectId[2];
                Ids[0] = idD;
                Ids[1] = idW;
                ObjectIdCollection idCol = new ObjectIdCollection(Ids);
                Cls_BW_Utility.ZoomObjects(idCol);
                Autodesk.AutoCAD.Internal.Utils.SelectObjects(Ids);
            }
            else if (hlgtWapsD & !hlgtWaps)
            {
                Ids = new ObjectId[1];
                Ids[0] = idD;
                ObjectIdCollection idCol = new ObjectIdCollection(Ids);
                Cls_BW_Utility.ZoomObjects(idCol);
                Autodesk.AutoCAD.Internal.Utils.SelectObjects(Ids);
            }
            else if (hlgtWaps & !hlgtWapsD)
            {
                Ids = new ObjectId[1];
                Ids[0] = idW;
                ObjectIdCollection idCol = new ObjectIdCollection(Ids);
                Cls_BW_Utility.ZoomObjects(idCol);
                Autodesk.AutoCAD.Internal.Utils.SelectObjects(Ids);
            }
            else if (!hlgtWaps & !hlgtWapsD)
            {
                Ids = new ObjectId[2];
                Ids[0] = idD;
                Ids[1] = idW;
                ObjectIdCollection idCol = new ObjectIdCollection(Ids);
                Cls_BW_Utility.ZoomObjects(idCol);
            }

        }


        public static void WapsFindDuplicates()
        {
            var fltr = Cls_BW_TP_APs.LstAtts_wap_data_2_dual.Duplicates(x => x.Label1);

            string msg = "";

            if (fltr.Count() > 0)
            {
                foreach (string v in fltr)
                {
                    msg += v + Environment.NewLine;
                }
                MessageBox.Show(msg);
            }
            else
            {
                var frst = Cls_BW_TP_APs.LstAtts_wap_data_2_dual.First();
                var last = Cls_BW_TP_APs.LstAtts_wap_data_2_dual.Last();

                MessageBox.Show("No Duplicate AP's" +
                    ", FirstNum " + frst.Label1.ToString() +
                    ", LastNum " + last.Label1.ToString() +
                    ", Count " + Cls_BW_TP_APs.LstAtts_wap_data_2_dual.Count().ToString());
            }
        }


        public static void BtnAPsRemunberAllByCloset()
        {
            int cnt = 1;

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            using (doc.LockDocument())
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                foreach (var acf in Cls_BW_TP_APs.LstAPsClosetFilter)
                {
                    //var itms = Cls_BW_TP_APs.LstAtts_wap_data_2_dual
                    //    .Where(x => x.ClosetInfo == acf.Closet)
                    //    .OrderBy(x => x.Floor)
                    //    .ThenByDescending(x => x.InsertionPtOfBlock.Y)
                    //    .ThenBy(x => x.InsertionPtOfBlock.X)
                    //    .ToList();

                    var itms = Cls_BW_TP_APs.LstAtts_wap_data_2_dual
                        .Where(x => x.ClosetInfo == acf.Closet)
                        .OrderBy(x => x.Bldg)
                        .ThenBy(x => x.Floor)
                        .ThenByDescending(x => x.InsertionPtOfBlock.Y)
                        .ThenBy(x => x.InsertionPtOfBlock.X)
                        .ToList();

                    foreach (var itm in itms)
                    {
                        ObjectId idD = Cls_BW_Utility.ObjectIDFromHandle(database, itm.Handle.ToString());

                        BlockReference blkRef1 = (BlockReference)transaction.GetObject(idD, OpenMode.ForRead);

                        Autodesk.AutoCAD.DatabaseServices.AttributeCollection attCol1 = blkRef1.AttributeCollection;
                        foreach (ObjectId attId in attCol1)
                        {
                            AttributeReference attRef1 = (AttributeReference)transaction.GetObject(attId, OpenMode.ForWrite);//attRef.UpgradeOpen();

                            switch (attRef1.Tag)
                            {
                                case "WAP":
                                    attRef1.TextString = cnt.ToString().PadLeft(2, '0');
                                    break;
                                case "APNUMBER":
                                    attRef1.TextString = cnt.ToString().PadLeft(2, '0');
                                    break;
                                case "LABEL1":
                                    attRef1.TextString = cnt.ToString().PadLeft(3, '0') + "A/" +
                                        cnt.ToString().PadLeft(3, '0') + "B";
                                    break;
                                default:
                                    break;
                            }
                        }

                        TypedValue[] acTypValAr = new TypedValue[2];
                        acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "INSERT"), 0);
                        acTypValAr.SetValue(new TypedValue((int)DxfCode.BlockName, "wap"), 1);

                        SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);

                        //TODO: (auto labeling ap's) What if more than 1 wap is selected? have the user select the correct one?
                        ObjectId[] wapDuID = { blkRef1.ObjectId };
                        BW.Cls_BW_Utility.ZoomObjects(new ObjectIdCollection(wapDuID.ToArray()));
                        Extents3d bndBox = blkRef1.GeometricExtents;
                        PromptSelectionResult acSSPrompt = ed.SelectCrossingWindow(bndBox.MinPoint, bndBox.MaxPoint, acSelFtr, false);
                                                   
                        if (acSSPrompt.Status == PromptStatus.OK)
                        {
                            SelectionSet acSSet = acSSPrompt.Value;

                            ObjectId[] ids = { };

                            try
                            {
                                ids = acSSet.GetObjectIds();

                                if (ids.Count() > 1)
                                {
                                    Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("More than 1 WAP selected: abort transaction");
                                    transaction.Abort();
                                    return;
                                }
                            }
                            catch (Autodesk.AutoCAD.Runtime.Exception ex)
                            {
                                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(ex.ToString());
                            }

                            BlockReference blkRef2 = (BlockReference)transaction.GetObject(ids[0], OpenMode.ForRead);
                            Autodesk.AutoCAD.DatabaseServices.AttributeCollection attCol2 = blkRef2.AttributeCollection;
                            foreach (ObjectId attId in attCol2)
                            {
                                AttributeReference attRef2 = (AttributeReference)transaction.GetObject(attId, OpenMode.ForWrite);//attRef.UpgradeOpen();

                                switch (attRef2.Tag)
                                {
                                    case "WAP":
                                        attRef2.TextString = cnt.ToString().PadLeft(2, '0');
                                        break;
                                    case "APNUMBER":
                                        attRef2.TextString = cnt.ToString().PadLeft(2, '0');
                                        break;
                                    case "LABEL1":
                                        attRef2.TextString = cnt.ToString().PadLeft(3, '0') + "A/" +
                                            cnt.ToString().PadLeft(3, '0') + "B";
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
                        }
                        cnt++;
                    }
                }
                transaction.Commit();
            }
        }



        public static void BtnAPsRemunber_Click_Sub(int dgvWDD_Indx, int dgvWDD_RowCnt, TextBox txtBx)
        {
            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            List<Handle> LstHandle = new List<Handle>();
            LstHandle.AddRange(Cls_BW_TP_APs.LstFilteredApsDuals.Select(x => x.Handle).ToArray());
            LstHandle.AddRange(Cls_BW_TP_APs.LstFilteredAps.Select(x => x.Handle).ToArray());

            Handle[] mainArr = LstHandle.ToArray();

            List<ObjectId> LstObjectId = new List<ObjectId>();

            foreach (Handle h in mainArr)
            {
                ObjectId id = Cls_BW_Utility.ObjectIDFromHandle(database, h.ToString());
                LstObjectId.Add(id);
            }


            int i;

            using (doc.LockDocument())
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                // start from the selected item in the grid
                for (i = dgvWDD_Indx; i < dgvWDD_RowCnt; i++)
                {
                    ObjectId idD = Cls_BW_Utility.ObjectIDFromHandle(database,
                        Cls_BW_TP_APs.LstFilteredApsDuals[i].Handle.ToString());
                    //dgvWapData2Duals[10, i].Value.ToString());
                    ObjectId idW = Cls_BW_Utility.ObjectIDFromHandle(database,
                        Cls_BW_TP_APs.LstFilteredAps[i].Handle.ToString());
                    //dgvWaps[5, i].Value.ToString());

                    BlockReference blkRef1 = (BlockReference)transaction.GetObject(idD, OpenMode.ForRead);
                    Autodesk.AutoCAD.DatabaseServices.AttributeCollection attCol = blkRef1.AttributeCollection;

                    foreach (ObjectId attId in attCol)
                    {
                        AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForWrite);//attRef.UpgradeOpen();

                        switch (attRef.Tag)
                        {
                            case "WAP":
                                attRef.TextString = txtBx.Text.PadLeft(2, '0');
                                break;
                            case "APNUMBER":
                                attRef.TextString = txtBx.Text.PadLeft(2, '0');
                                break;
                            case "LABEL1":
                                attRef.TextString = txtBx.Text.PadLeft(3, '0') + "A/" +
                                    txtBx.Text.PadLeft(3, '0') + "B";
                                break;
                            default:
                                break;
                        }
                    }

                    BlockReference blkRef2 = (BlockReference)transaction.GetObject(idW, OpenMode.ForRead);
                    attCol = blkRef2.AttributeCollection;

                    foreach (ObjectId attId in attCol)
                    {
                        AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForWrite);//attRef.UpgradeOpen();

                        switch (attRef.Tag)
                        {
                            case "WAP":
                                attRef.TextString = txtBx.Text.PadLeft(2, '0'); ;
                                break;
                            case "APNUMBER":
                                attRef.TextString = txtBx.Text.PadLeft(2, '0'); ;
                                break;
                            case "LABEL1":
                                attRef.TextString = txtBx.Text.PadLeft(3, '0') + "A/" +
                                    txtBx.Text.PadLeft(3, '0') + "B";
                                break;
                            default:
                                break;
                        }
                    }
                    txtBx.Text = (int.Parse(txtBx.Text) + 1).ToString();
                    txtBx.Refresh();
                }
                transaction.Commit();


                //     dgvDisconnect();

                //     Cls_MMM_Main.FillAPsList(LstObjectId.ToArray());

                //     dgvReConnect(Cls_MMM_Main.LstFilteredAps, Cls_MMM_Main.LstFilteredApsDuals, Cls_MMM_Main.LstAtts_waos);
            }
        }

        public static void GetLstClosetFilterAPs()
        {
            LstAPsClosetFilter.Clear();

            var DistinctClosetNames = LstAtts_wap_data_2_dual.Select(x => x.ClosetInfo).Distinct().ToList();
            List<Cls_BW_TP_Common.ClsClosetFilterList> tmpLst = new List<Cls_BW_TP_Common.ClsClosetFilterList>();

            foreach (string s in DistinctClosetNames)
            {
                var newS = s;

                if (!s.Contains('-'))
                {
                    newS += "-0";
                }                            
         
                // string txt = LstAtts_wap_data_2_dual.Count(x => x.ClosetInfo == s).ToString();
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

            LstAPsClosetFilter.AddRange(tr);
            LstAPsClosetFilter.AddRange(tc);
        }



        public static void FillAPsList(PromptSelectionResult acSSPrompt)
        {
            if (acSSPrompt == null | acSSPrompt.Status == PromptStatus.Error)
                return;

            LstAtts_wap.Clear();
            LstAtts_wap_data_2_dual.Clear();

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

                    if (nam == "wap data 2 dual")
                    {
                        var clsAttsWapData2Dual = new Cls_BW_WapsData2Dual_Atts();
                        clsAttsWapData2Dual.Handle = blkRef.Handle;
                        clsAttsWapData2Dual.InsertionPtOfBlock = blkRef.Position;
                        LstAtts_wap_data_2_dual.Add(clsAttsWapData2Dual);

                        AttributeCollection attCol = blkRef.AttributeCollection;

                        List<AttributeReference> WapAtts = new List<AttributeReference>();

                        foreach (ObjectId attId in attCol)
                        {
                            AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForRead);
                            WapAtts.Add(attRef);
                        }

                        Cls_BW_BlockAttsGetWapDual.GetWapData2DualAttributes(clsAttsWapData2Dual, WapAtts);
                    }

                    if (nam == "wap")
                    {
                        var clsAttsWaps = new Cls_BW_Waps_Atts();
                        clsAttsWaps.Handle = blkRef.Handle;
                        clsAttsWaps.InsertionPtOfBlock = blkRef.Position;
                        LstAtts_wap.Add(clsAttsWaps);

                        AttributeCollection attCol = blkRef.AttributeCollection;

                        List<AttributeReference> WapAtts = new List<AttributeReference>();

                        foreach (ObjectId attId in attCol)
                        {
                            AttributeReference attRef = (AttributeReference)transaction.GetObject(attId, OpenMode.ForRead);
                            WapAtts.Add(attRef);
                        }

                        Cls_BW_BlockAttsGetWap.GetWapAttributes(clsAttsWaps, WapAtts);                        
                    }
                }

                if (LstAtts_wap.Count > 0)
                {
                    Cls_BW_Waps_Atts tst = LstAtts_wap.ElementAtOrDefault(0);

                    if (tst.Wap != "000")
                    {
                        LstAtts_wap.Sort((x, y) => x.Wap.CompareTo(y.Wap));
                        LstAtts_wap_data_2_dual.Sort((x, y) => x.Wap.CompareTo(y.Wap));
                    }
                    else
                    {
                        LstAtts_wap.Sort((x, y) => x.Label1.CompareTo(y.Label1));
                        LstAtts_wap_data_2_dual.Sort((x, y) => x.Label1.CompareTo(y.Label1));
                    }
                }

                transaction.Commit();
            }
        }
        
        public static void BtnLabelAPsIndividually_Sub(TextBox txtBx)
        {
            Cls_BW_Main.frm_MMM_Host_Form.Hide();

            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            List<ObjectId> LstObjectIdWap = new List<ObjectId>();
            List<ObjectId> LstObjectIdWapD = new List<ObjectId>();

            PromptEntityOptions pWap = new PromptEntityOptions("Select Wap:");
            PromptEntityResult perWap;

            PromptEntityOptions pWApD = new PromptEntityOptions("Select WapDual:");
            PromptEntityResult perWapD;

            do
            {
                perWap = ed.GetEntity(pWap);
                perWapD = ed.GetEntity(pWApD);
                {
                    if (perWap.ObjectId != ObjectId.Null & perWapD.ObjectId != ObjectId.Null)
                    {
                        LstObjectIdWap.Add(perWap.ObjectId);
                        LstObjectIdWapD.Add(perWapD.ObjectId);

                        using (doc.LockDocument())
                        using (Transaction tr = database.TransactionManager.StartTransaction())
                        {
                            BlockReference blkRefWap = (BlockReference)tr.GetObject(perWap.ObjectId, OpenMode.ForRead);
                            Autodesk.AutoCAD.DatabaseServices.AttributeCollection attColWap = blkRefWap.AttributeCollection;

                            BlockReference blkRefWapD = (BlockReference)tr.GetObject(perWapD.ObjectId, OpenMode.ForRead);
                            Autodesk.AutoCAD.DatabaseServices.AttributeCollection attColWapD = blkRefWapD.AttributeCollection;

                            int strt = int.Parse(txtBx.Text);

                            foreach (ObjectId attId in attColWap)
                            {
                                AttributeReference attRef = (AttributeReference)tr.GetObject(attId, OpenMode.ForWrite);//attRef.UpgradeOpen();

                                switch (attRef.Tag)
                                {
                                    case "APNUMBER":
                                        attRef.TextString = txtBx.Text.PadLeft(2, '0');
                                        break;
                                    case "WAP":
                                        attRef.TextString = txtBx.Text.PadLeft(2, '0');
                                        break;
                                    case "LABEL1":
                                        attRef.TextString = txtBx.Text.PadLeft(3, '0') + "A/" +
                                            txtBx.Text.PadLeft(3, '0') + "B";
                                        break;
                                    default:
                                        break;
                                }
                            }

                            foreach (ObjectId attId in attColWapD)
                            {
                                AttributeReference attRef = (AttributeReference)tr.GetObject(attId, OpenMode.ForWrite);//attRef.UpgradeOpen();

                                switch (attRef.Tag)
                                {
                                    case "APNUMBER":
                                        attRef.TextString = txtBx.Text.PadLeft(2, '0');
                                        break;
                                    case "WAP":
                                        attRef.TextString = txtBx.Text.PadLeft(2, '0');
                                        break;
                                    case "LABEL1":
                                        attRef.TextString = txtBx.Text.PadLeft(3, '0') + "A/" +
                                            txtBx.Text.PadLeft(3, '0') + "B";
                                        break;
                                    default:
                                        break;
                                }
                            }

                            tr.Commit();

                            txtBx.Text = (int.Parse(txtBx.Text) + 1).ToString();
                            txtBx.Refresh();
                        }
                    }
                }

            } while (perWap.Status == PromptStatus.OK & perWapD.Status == PromptStatus.OK);

            //Cls_MMM_Main.LstAtts_waos.Clear();


            //dgvWaos.DataSource = null;
            //dgvWaos.DataSource = Cls_MMM_Main.LstAtts_waos;
            //dgvWaos.Refresh();

            Cls_BW_Main.frm_MMM_Host_Form.Show();
        }




        public static void BtnSelAMsAPs_Click_Sub()
        {
            Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            ObjectId[] ids = { };     

            TypedValue[] tvs = new TypedValue[4];
            tvs.SetValue(new TypedValue((int)DxfCode.Operator, "<or"), 0);
            tvs.SetValue(new TypedValue((int)DxfCode.BlockName, "wap*"), 1);
            tvs.SetValue(new TypedValue((int)DxfCode.LayerName, "AIRMAGNET-APs"), 2);
            tvs.SetValue(new TypedValue((int)DxfCode.Operator, "or>"), 3);

            PromptSelectionResult acSSPrompt = acDocEd.GetSelection(new SelectionFilter(tvs));

            if (acSSPrompt.Status == PromptStatus.OK)
            {
                SelectionSet acSSet = acSSPrompt.Value;

                try
                {
                    ids = acSSet.GetObjectIds();
                }
                catch (Autodesk.AutoCAD.Runtime.Exception ex)
                {
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(ex.ToString());
                    return;
                }
            }
            else
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
            }                 
     
            Autodesk.AutoCAD.Internal.Utils.SelectObjects(ids);
        }


    }
}

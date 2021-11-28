using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System.Collections;

namespace MyFirstProject.BW
{
    public partial class Uc_BW_TP_APs : UserControl
    {
        public Uc_BW_TP_APs()
        {
            InitializeComponent();
        
        }
        

        private void DgvWaps_Scroll(object sender, ScrollEventArgs e)
        {
            dgvWapData2Duals.FirstDisplayedScrollingRowIndex = dgvWaps.FirstDisplayedScrollingRowIndex;
        }

        private void DgvWapData2Duals_Scroll(object sender, ScrollEventArgs e)
        {
            dgvWaps.FirstDisplayedScrollingRowIndex = dgvWapData2Duals.FirstDisplayedScrollingRowIndex;
        }

        private void DgvWaps_SelectionChanged(object sender, EventArgs e)
        {
            dgvWapData2Duals.SelectionChanged -= new EventHandler(DgvWapData2Duals_SelectionChanged);
            dgvWapData2Duals.ClearSelection();
            if (dgvWaps.CurrentRow != null)
            {
                dgvWapData2Duals.Rows[dgvWaps.CurrentRow.Index].Selected = true;
                dgvWapData2Duals.CurrentCell = dgvWapData2Duals[0, dgvWaps.CurrentCell.RowIndex];
            }
            dgvWapData2Duals.SelectionChanged += new EventHandler(DgvWapData2Duals_SelectionChanged);

            Cls_BW_TP_APs.APsZoomAndSelect(dgvWaps, dgvWapData2Duals, chkBxWap.Checked, chkBxWapDual.Checked);

            //  dgvWapData2Duals.FirstDisplayedCell = DataGridView1.Rows[rowId].Cells[0];
        }

        private void DgvWapData2Duals_SelectionChanged(object sender, EventArgs e)
        {
            dgvWaps.SelectionChanged -= new EventHandler(DgvWaps_SelectionChanged);
            dgvWaps.ClearSelection();
            if (dgvWapData2Duals.CurrentRow != null)
            {
                dgvWaps.Rows[dgvWapData2Duals.CurrentRow.Index].Selected = true;
                dgvWaps.CurrentCell = dgvWaps[0, dgvWapData2Duals.CurrentCell.RowIndex];
            }
            dgvWaps.SelectionChanged += new EventHandler(DgvWaps_SelectionChanged);

            Cls_BW_TP_APs.APsZoomAndSelect(dgvWaps, dgvWapData2Duals, chkBxWap.Checked, chkBxWapDual.Checked);
        }


        private void BtnAPsSetCloset_Click(object sender, EventArgs e)
        {
            Uc_BW_TP_WAOs frm = (Uc_BW_TP_WAOs)this.ParentForm.Controls.Find("uc_BW_TP_WAOs1", true)[0];

            //dgvWaoTypeFilter.SelectionChanged -= new System.EventHandler(dgvWaoTypeFilter_SelectionChanged);

            dgvWaps.Scroll -= new System.Windows.Forms.ScrollEventHandler(DgvWaps_Scroll);
            dgvWaps.SelectionChanged -= new System.EventHandler(DgvWaps_SelectionChanged);
            dgvWapData2Duals.Scroll -= new System.Windows.Forms.ScrollEventHandler(DgvWapData2Duals_Scroll);
            dgvWapData2Duals.SelectionChanged -= new System.EventHandler(DgvWapData2Duals_SelectionChanged);

            dgvWaps.DataSource = null;
            dgvWapData2Duals.DataSource = null;
            frm.dgvWaos.DataSource = null;

            this.ParentForm.ParentForm.Hide();

            Cls_BW_TP_Common.SetClosetForBlocks(
                chkBxRunOrientLRTB.Checked,
                int.Parse(txtBxMaxRunLengthInFeet.Text),
                false,
                chkBxUpdateSite.Checked,
                chkBxUpdateBuilding.Checked,
                chkBxUpdateFloor.Checked,
                chkBxCalcLengthsFromCloset.Checked
                );

            //  frm_BW_MainForm_AsControl_Code.GetLstFilterWAOsTypeOfBlock();

            //dgvWaoTypeFilter.DataSource = frm_BW_MainForm_AsControl_Code.LstWaoBlockNames;
            //dgvWaoTypeFilter.Columns[0].Width = 50;
            //dgvWaoTypeFilter.Columns[1].Width = 150;
            //dgvWaoTypeFilter.RowHeadersVisible = false;
            //dgvWaoTypeFilter.ReadOnly = true;
            //dgvWaoTypeFilter.ClearSelection();

            dgvWaps.DataSource = Cls_BW_TP_APs.LstAtts_wap;
            dgvWapData2Duals.DataSource = Cls_BW_TP_APs.LstAtts_wap_data_2_dual;
            frm.dgvWaos.DataSource = Cls_BW_TP_WAOs.LstAtts_waos;

            dgvWaps.Scroll += new System.Windows.Forms.ScrollEventHandler(DgvWaps_Scroll);
            dgvWaps.SelectionChanged += new System.EventHandler(DgvWaps_SelectionChanged);
            dgvWapData2Duals.Scroll += new System.Windows.Forms.ScrollEventHandler(DgvWapData2Duals_Scroll);
            dgvWapData2Duals.SelectionChanged += new System.EventHandler(DgvWapData2Duals_SelectionChanged);

            //  dgvWaoTypeFilter.SelectionChanged += new System.EventHandler(dgvWaoTypeFilter_SelectionChanged);

            this.ParentForm.ParentForm.Show();
        }

        private void BtnAPsWAOsGet_Click(object sender, EventArgs e)
        {
            BtnAPsGet_Click_Sub();
        }
        public void BtnAPsGet_Click_Sub()
        {
            DgvDisconnect();

            this.ParentForm.ParentForm.Hide();

            Cls_BW_TP_APs.Gw_SelectBlocks(false);
            Cls_BW_TP_APs.FillAPsList(Cls_BW_TP_Common.acSSPrompt);
            Cls_BW_TP_WAOs.FillWAOsList(Cls_BW_TP_Common.acSSPrompt, false);

            BtnAPsGet_Click_Sub_FltrAndCloset();

            // sort WAOs in correct closet order if closets assigned
            var tmp = Cls_BW_TP_WAOs.LstAtts_waos.ToList();
            Cls_BW_TP_WAOs.LstAtts_waos.Clear();

            if (Cls_BW_TP_WAOs.LstWAOsClosetFilter.Count > 0)
            {
                foreach (var acf in Cls_BW_TP_WAOs.LstWAOsClosetFilter)
                {
                    var itms = tmp
                        .Where(x => x.ClosetInfo == acf.Closet)
                        .OrderBy(x => x.Floor)
                        .ThenByDescending(x => x.InsertionPtOfBlock.Y)
                        .ThenBy(x => x.InsertionPtOfBlock.X)
                        .ToList();

                    Cls_BW_TP_WAOs.LstAtts_waos.AddRange(itms);
                }
            }
            else
            {
                Cls_BW_TP_WAOs.LstAtts_waos.AddRange(tmp);
            }


            DgvReConnect(Cls_BW_TP_APs.LstAtts_wap, Cls_BW_TP_APs.LstAtts_wap_data_2_dual, Cls_BW_TP_WAOs.LstAtts_waos);

            this.ParentForm.ParentForm.Show();
        }

        public void DgvDisconnect()
        {
            Uc_BW_TP_WAOs frm = (Uc_BW_TP_WAOs)this.ParentForm.Controls.Find("uc_BW_TP_WAOs1", true)[0];

            cBxAPClosetFilter.SelectedIndexChanged -= new System.EventHandler(CBxAPClosetFilter_SelectedIndexChanged);
            frm.cBxWaoClosetFilter.SelectedIndexChanged -= new System.EventHandler(frm.CBxWaoClosetFilter_SelectedIndexChanged);
            //dgvWaoTypeFilter.SelectionChanged -= new System.EventHandler(dgvWaoTypeFilter_SelectionChanged);

            dgvWaps.Scroll -= new System.Windows.Forms.ScrollEventHandler(DgvWaps_Scroll);
            dgvWaps.SelectionChanged -= new System.EventHandler(DgvWaps_SelectionChanged);
            dgvWapData2Duals.Scroll -= new System.Windows.Forms.ScrollEventHandler(DgvWapData2Duals_Scroll);
            dgvWapData2Duals.SelectionChanged -= new System.EventHandler(DgvWapData2Duals_SelectionChanged);

            dgvWaps.DataSource = null;
            dgvWapData2Duals.DataSource = null;
            frm.dgvWaos.DataSource = null;
        }

        public void DgvReConnect(object waps, object wapsDual, object waos)
        {
            Uc_BW_TP_WAOs frm = (Uc_BW_TP_WAOs)this.ParentForm.Controls.Find("uc_BW_TP_WAOs1", true)[0];

            dgvWaps.DataSource = waps;
            dgvWapData2Duals.DataSource = wapsDual;
            frm.dgvWaos.DataSource = waos;

            dgvWaps.Scroll += new System.Windows.Forms.ScrollEventHandler(DgvWaps_Scroll);
            dgvWaps.SelectionChanged += new System.EventHandler(DgvWaps_SelectionChanged);
            dgvWapData2Duals.Scroll += new System.Windows.Forms.ScrollEventHandler(DgvWapData2Duals_Scroll);
            dgvWapData2Duals.SelectionChanged += new System.EventHandler(DgvWapData2Duals_SelectionChanged);

            cBxAPClosetFilter.SelectedIndexChanged += new System.EventHandler(CBxAPClosetFilter_SelectedIndexChanged);
            frm.cBxWaoClosetFilter.SelectedIndexChanged += new System.EventHandler(frm.CBxWaoClosetFilter_SelectedIndexChanged);
            //dgvWaoTypeFilter.SelectionChanged += new System.EventHandler(dgvWaoTypeFilter_SelectionChanged);
        }


        private void CBxAPClosetFilter_SelectedIndexChanged(object sender, EventArgs e)
        {           
            DgvDisconnect();

            Cls_BW_TP_APs.LstFilteredAps.Clear();
            Cls_BW_TP_APs.LstFilteredApsDuals.Clear();      
            
            var nLst = Cls_BW_TP_APs.LstAtts_wap_data_2_dual.Where(x => x.ClosetInfo ==
            ((Cls_BW_TP_Common.ClsClosetFilterList)cBxAPClosetFilter.SelectedItem).Closet).ToList();

            Cls_BW_TP_APs.LstFilteredApsDuals.AddRange(nLst);

            List<ObjectId> Ids = new List<ObjectId>();

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
            Autodesk.AutoCAD.ApplicationServices.Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            using (doc.LockDocument())
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                foreach (BW.Cls_BW_WapsData2Dual_Atts c in Cls_BW_TP_APs.LstFilteredApsDuals)
                {
                    ObjectId idD = Cls_BW_Utility.ObjectIDFromHandle(
                        HostApplicationServices.WorkingDatabase,
                                 c.Handle.ToString());

                    Ids.Add(idD);

                    //Cls_BW_Waps_Atts fltr = Cls_BW_TP_APs.LstAtts_wap.Where(x => x.Label1 == c.Label1).SingleOrDefault();

                    //Ids.Add(Cls_BW_Utility.ObjectIDFromHandle(
                    //    HostApplicationServices.WorkingDatabase,
                    //             fltr.Handle.ToString()));

                    //Cls_BW_TP_APs.LstFilteredAps.Add(fltr);                    

                    BlockReference blkRef1 = (BlockReference)transaction.GetObject(idD, OpenMode.ForRead);

                    TypedValue[] acTypValAr = new TypedValue[2];
                    acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "INSERT"), 0);
                    acTypValAr.SetValue(new TypedValue((int)DxfCode.BlockName, "wap"), 1);

                    SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);

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
                                //transaction.Abort();
                                //return;
                            }
                            else
                            {
                                Ids.Add(ids[0]);
                                BlockReference blkRef2 = (BlockReference)transaction.GetObject(ids[0], OpenMode.ForRead);
                                Cls_BW_Waps_Atts fltr = Cls_BW_TP_APs.LstAtts_wap.Where(x => x.Handle == blkRef2.Handle).SingleOrDefault();
                                Cls_BW_TP_APs.LstFilteredAps.Add(fltr);
                            }
                        }
                        catch (Autodesk.AutoCAD.Runtime.Exception ex)
                        {
                            Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(ex.ToString());
                        }                      
                    }
                }
                transaction.Abort();
            }

            ObjectIdCollection idCol = new ObjectIdCollection(Ids.ToArray());
            Cls_BW_Utility.ZoomObjects(idCol);
            Autodesk.AutoCAD.Internal.Utils.SelectObjects(Ids.ToArray());

            DgvReConnect(Cls_BW_TP_APs.LstFilteredAps, Cls_BW_TP_APs.LstFilteredApsDuals, Cls_BW_TP_WAOs.LstAtts_waos);       
        }

        private void BtnAPsGet_Click_Sub_FltrAndCloset()
        {
            Uc_BW_TP_WAOs frm = (Uc_BW_TP_WAOs)this.ParentForm.Controls.Find("uc_BW_TP_WAOs1", true)[0];

            //  dgvWaoTypeFilter.SelectionChanged -= new System.EventHandler(dgvWaoTypeFilter_SelectionChanged);
            //            frm_BW_MainForm_AsControl_Code.GetLstFilterWAOsTypeOfBlock();
            //dgvWaoTypeFilter.DataSource = frm_BW_MainForm_AsControl_Code.LstWaoBlockNames;
            //dgvWaoTypeFilter.Columns[0].Width = 50;
            //dgvWaoTypeFilter.Columns[1].Width = 150;
            //dgvWaoTypeFilter.RowHeadersVisible = false;
            //dgvWaoTypeFilter.ReadOnly = true;

            Cls_BW_TP_APs.GetLstClosetFilterAPs();
            cBxAPClosetFilter.DataSource = null;
            cBxAPClosetFilter.DataSource = Cls_BW_TP_APs.LstAPsClosetFilter;
            cBxAPClosetFilter.DisplayMember = "Closet";

            Cls_BW_TP_WAOs.GetLstClosetFilterWAOs();
            frm.cBxWaoClosetFilter.DataSource = null;
            frm.cBxWaoClosetFilter.DataSource = Cls_BW_TP_WAOs.LstWAOsClosetFilter;
            frm.cBxWaoClosetFilter.DisplayMember = "Closet";

            //dgvWaoTypeFilter.ClearSelection();
            //dgvWaoTypeFilter.SelectionChanged += new System.EventHandler(dgvWaoTypeFilter_SelectionChanged);
        }

        private void BtnRefreshAps_Click(object sender, EventArgs e)
        {
            DgvDisconnect();
              
            Cls_BW_TP_APs.FillAPsList(Cls_BW_TP_Common.acSSPrompt);
            Cls_BW_TP_WAOs.FillWAOsList(Cls_BW_TP_Common.acSSPrompt, false);

            BtnAPsGet_Click_Sub_FltrAndCloset();

            DgvReConnect(Cls_BW_TP_APs.LstAtts_wap, Cls_BW_TP_APs.LstAtts_wap_data_2_dual, Cls_BW_TP_WAOs.LstAtts_waos);

        }

        private void BtnAPsSelectInPolyline_Click(object sender, EventArgs e)
        {

            BtnAPsSelectInPolyline_Click_Sub();
        }

        private void BtnAPsSelectInPolyline_Click_Sub()
        {
            DgvDisconnect();

            this.ParentForm.ParentForm.Hide();

            Cls_BW_TP_APs.Gw_SelectBlockApsInPolyline(
                chkBxRunOrientLRTB.Checked,
                int.Parse(txtBxMaxRunLengthInFeet.Text)
                );

            DgvReConnect(Cls_BW_TP_APs.LstAtts_wap, Cls_BW_TP_APs.LstAtts_wap_data_2_dual, Cls_BW_TP_WAOs.LstAtts_waos);

            this.ParentForm.ParentForm.Show();
        }

        private void BtnSelectByViewPort_Click(object sender, EventArgs e)
        {
            this.ParentForm.ParentForm.Hide();

            Cls_BW_Viewport_Select cls = new Cls_BW_Viewport_Select();
            cls.SelectByViewport(MyFirstProject.BW.Cls_BW_SelDynBlksAlso.SelectDynamicBlocks(BW.Cls_BW_BlksToSelect.GetBlockNames()));

            // select the blocks in vp into the MMM form
            this.DgvDisconnect();

            Cls_BW_TP_APs.LstAtts_wap.Clear();
            Cls_BW_TP_APs.LstAtts_wap_data_2_dual.Clear();
            Cls_BW_TP_WAOs.LstAtts_waos.Clear();

            Cls_BW_TP_APs.FillAPsList(Cls_BW_TP_Common.acSSPrompt);
            Cls_BW_TP_WAOs.FillWAOsList(Cls_BW_TP_Common.acSSPrompt, false);

            this.DgvReConnect(Cls_BW_TP_APs.LstAtts_wap, Cls_BW_TP_APs.LstAtts_wap_data_2_dual, Cls_BW_TP_WAOs.LstAtts_waos);

            this.ParentForm.ParentForm.Show();
        }

        private void BtnAPsZoomAndSelect_Click(object sender, EventArgs e)
        {
            Cls_BW_TP_APs.APsZoomAndSelect(dgvWaps, dgvWapData2Duals, chkBxWap.Checked, chkBxWapDual.Checked);
        }

        private void BtnRestoreOriginalAPsLst_Click(object sender, EventArgs e)
        {
            DgvDisconnect();

            DgvReConnect(Cls_BW_TP_APs.LstAtts_wap, Cls_BW_TP_APs.LstAtts_wap_data_2_dual, Cls_BW_TP_WAOs.LstAtts_waos);
        }

        private void BtnAPsRemunber_Click(object sender, EventArgs e)
        {
            Cls_BW_TP_APs.BtnAPsRemunber_Click_Sub(dgvWapData2Duals.CurrentRow.Index, dgvWapData2Duals.RowCount, txtBxAPsStartNumberForRenumber);
        }

        private void BtnLabelAPsIndividually_Click(object sender, EventArgs e)
        {
            Cls_BW_TP_APs.BtnLabelAPsIndividually_Sub(txtBxAPsStartNumberForRenumber);
        }

        private void BtnAPsRenumberAllByCloset_Click(object sender, EventArgs e)
        {
            Cls_BW_TP_APs.BtnAPsRemunberAllByCloset();
        }

        private void BtnWapsFindDups_Click(object sender, EventArgs e)
        {
            Cls_BW_TP_APs.WapsFindDuplicates();
        }


        private void BtnSelAMsAPs_Click(object sender, EventArgs e)
        {
            this.ParentForm.ParentForm.Hide();

            Cls_BW_TP_APs.BtnSelAMsAPs_Click_Sub();

            this.ParentForm.ParentForm.Show();
        }




        private void BtnFindMissingNums_Click(object sender, EventArgs e)
        {
            var list = Cls_BW_TP_APs.LstAtts_wap_data_2_dual.Select(x => x.Wap).ToList();

            IEnumerable lst =  Cls_BW_Utility.FindMissingNums(list);
        }





    }
}

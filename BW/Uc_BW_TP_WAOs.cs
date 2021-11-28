using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;

namespace MyFirstProject.BW
{
    public partial class Uc_BW_TP_WAOs : UserControl
    {
        public Uc_BW_TP_WAOs()
        {
            InitializeComponent();
        }

        private void BtnGetApsAndWaos_Click(object sender, EventArgs e)
        {
            Uc_BW_TP_APs frm = (Uc_BW_TP_APs)this.ParentForm.Controls.Find("uc_BW_TP_APs1", true)[0];
            frm.BtnAPsGet_Click_Sub();
        }

        private void BtnClearWaoFilter_Click(object sender, EventArgs e)
        {
            BtnClearWaoFilter_Click_Sub();
        }
        private void BtnClearWaoFilter_Click_Sub()
        {
            dgvWaos.DataSource = null;
            dgvWaos.DataSource = Cls_BW_TP_WAOs.LstAtts_waos;
            dgvWaos.AutoResizeColumns();

            //dgvWaoTypeFilter.SelectionChanged -= new System.EventHandler(dgvWaoTypeFilter_SelectionChanged);
            //dgvWaoTypeFilter.ClearSelection();
            //dgvWaoTypeFilter.SelectionChanged += new System.EventHandler(dgvWaoTypeFilter_SelectionChanged);
        }

        //private void dgvWaoTypeFilter_SelectionChanged(object sender, EventArgs e)
        //{
        //    if (tbCtlMain.SelectedTab == tpWAOs && dgvWaoTypeFilter.SelectedRows.Count != 0)
        //    {
        //        var Filter = frm_BW_MainForm_AsControl_Code.LstAtts_waos.Where(x => x.BlockName == frm_BW_MainForm_AsControl_Code.LstWaoBlockNames[dgvWaoTypeFilter.SelectedRows[0].Index].BlkName).ToList();
        //        dgvWaos.DataSource = Filter;
        //    }
        //}


        public void CBxWaoClosetFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cls_BW_TP_WAOs.LstFilteredWaos.Clear();
            var nLst = Cls_BW_TP_WAOs.LstAtts_waos.Where(x => x.ClosetInfo ==
                  ((Cls_BW_TP_Common.ClsClosetFilterList)cBxWaoClosetFilter.SelectedItem).Closet).ToList();
            Cls_BW_TP_WAOs.LstFilteredWaos.AddRange(nLst);
            

            List<ObjectId> Ids = new List<ObjectId>();

            foreach (BW.Cls_BW_Waos_Atts c in Cls_BW_TP_WAOs.LstFilteredWaos)
            {
                ObjectId idD = Cls_BW_Utility.ObjectIDFromHandle(
                    HostApplicationServices.WorkingDatabase,
                             c.Handle.ToString());

                Ids.Add(idD);
            }
            if (Ids.Count > 0)
            {
                ObjectIdCollection idCol = new ObjectIdCollection(Ids.ToArray());

                Cls_BW_Utility.ZoomObjects(idCol);
                Autodesk.AutoCAD.Internal.Utils.SelectObjects(Ids.ToArray());

                dgvWaos.DataSource = null;
                dgvWaos.DataSource = Cls_BW_TP_WAOs.LstFilteredWaos;
                dgvWaos.AutoResizeColumns();
            }
            else
            {
                MessageBox.Show("No Id's Selected!");
            }

        }

        private void BtnWAOsZoomAndSelect_Click(object sender, EventArgs e)
        {
            BtnWAOsZoomAndSelect_Click_Sub();
        }
        private void BtnWAOsZoomAndSelect_Click_Sub()
        {
            ObjectId idD = Cls_BW_Utility.ObjectIDFromHandle(HostApplicationServices.WorkingDatabase,
                dgvWaos[9, dgvWaos.CurrentRow.Index].Value.ToString());

            ObjectId[] Ids;

            if (chkBxSelectWAO.Checked)
            {
                Ids = new ObjectId[1];
                Ids[0] = idD;
                ObjectIdCollection idCol = new ObjectIdCollection(Ids);
                Cls_BW_Utility.ZoomObjects(idCol);
                Autodesk.AutoCAD.Internal.Utils.SelectObjects(Ids);
            }
        }


        private void BtnAutoLabelWAOs_Click(object sender, EventArgs e)
        {
            // x, y sort
            var res = Cls_BW_TP_WAOs.LstFilteredWaos.OrderByDescending(x => x.InsertionPtOfBlock.Y).ThenBy(x => x.InsertionPtOfBlock.X).ToList();
            dgvWaos.DataSource = null;
            dgvWaos.DataSource = res;        

            Cls_BW_TP_WAOs.BtnAutoLabelWAOs_Click_Sub(dgvWaos, txtBxWaoAutoNum_NumToStartAt, false);
        }

        private void BtnLabelWAOsIndividually_Click(object sender, EventArgs e)
        {
            this.ParentForm.ParentForm.Hide();
            Cls_BW_TP_WAOs.BtnLabelWAOsIndividually_Click_Sub(dgvWaos, txtBxWaoAutoNum_NumToStartAt);
            this.ParentForm.ParentForm.Show();
        }

        private void BtnPutXXXsInWaos_Click(object sender, EventArgs e)
        {
            Cls_BW_TP_WAOs.BtnAutoLabelWAOs_Click_Sub(dgvWaos, txtBxWaoAutoNum_NumToStartAt, true);
        }

        private void BtnLabelAllSelectedWAOsInOrder_Click(object sender, EventArgs e)
        {
            // x, y sort
            var res =  Cls_BW_TP_WAOs.LstAtts_waos.OrderByDescending(x => x.InsertionPtOfBlock.Y).ThenBy(x => x.InsertionPtOfBlock.X).ToList();
            dgvWaos.DataSource = null;
            dgvWaos.DataSource = res;

            Cls_BW_TP_WAOs.BtnAutoLabelWAOsAllSelected_Click_Sub(dgvWaos, txtBxWaoAutoNum_NumToStartAt, false);
        }

        private void BtnAutoLabelWaosByCloset_Click(object sender, EventArgs e)
        {
            Cls_BW_TP_WAOs.BtnWaosRemunberAllByCloset();
        }
    }
}

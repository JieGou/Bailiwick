using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyFirstProject.BW
{
    public partial class Uc_BW_TP_AirMAGs : UserControl
    {
        public Uc_BW_TP_AirMAGs()
        {
            InitializeComponent();   
        }

    

        #region AirMagnets Import block

        //static Document AirMagDoc;
        //static Autodesk.AutoCAD.DatabaseServices.Database AirMagDb;

        //AirMagDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
        //AirMagDb = AirMagDoc.Database;

        private void BtnAirMagLbls_Click(object sender, EventArgs e)
        {
            this.ParentForm.ParentForm.Hide();
            Cls_BW_TP_AirMAGs.BtnAirMagLbls_Click_Sub();
            this.ParentForm.ParentForm.Show();
        }



        private void BtnAirMagLblsImport_Click(object sender, EventArgs e)
        {
            Cls_BW_TP_AirMAGs.ImportBlocks();
        }

        #endregion


        #region Air Magnet labels for Zooming and Inserting APs



        private void BtnAirMagLblsGetForAP_Insert_Click(object sender, EventArgs e)
        {
            this.ParentForm.ParentForm.Hide();
            Cls_BW_TP_AirMAGs.BtnGetAirMagLblsForAP_Insert_Click_Sub(chkBxMoveAirMagLables.Checked, dgvAirMagLbls);
            this.ParentForm.ParentForm.Show();
        }


        private void BtnAirMagLblsZoomTo_Click(object sender, EventArgs e)
        {
            Cls_BW_TP_AirMAGs.BtnZoomToAmLbl_Click_Sub(dgvAirMagLbls);
        }


        private void BtnAirMagLblsClear_Click(object sender, EventArgs e)
        {
            dgvAirMagLbls.DataSource = null;
        }




        #endregion


        #region Air Magnet Xml File


        /// <summary>
        /// modify old air mag xml to test new bom functions (temp Func)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTstXML_Click(object sender, EventArgs e)
        {
            List<Cls_BW_AirMagnetAps> myList = Cls_BW_TP_AirMAGs.BtnTstXML_Click_Sub();

            dgvAirMagXmlFile.DataSource = myList;
        }


        private void BtnAirMagRptReadXmlFile_Click(object sender, EventArgs e)
        {
            Cls_BW_TP_AirMAGs.BtnAirMagRptReadXmlFile_Click_Sub(dgvAirMagXmlFile, dgvAirMagXmlFileRepInfo);
        }


        private void BtnInsertAP_Click(object sender, EventArgs e)
        {
            this.ParentForm.ParentForm.Hide();
            Cls_BW_TP_AirMAGs.BtnInsertAP_Click_Sub(chkBxAPsAutoInsByAirMagTxt.Checked, dgvAirMagXmlFile, dgvAirMagLbls);
            this.ParentForm.ParentForm.Show();
        }


        #endregion




    }
}

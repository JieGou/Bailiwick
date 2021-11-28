using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyFirstProject.BW
{
    public partial class Frm_BW_MainForm_AsControl : Form
    {
        public Frm_BW_MainForm_AsControl()
        {
            InitializeComponent();

            List<DataGridView> dgv = new List<DataGridView>();
            Cls_BW_Utility.FindControlsOfType(typeof(DataGridView), this.Controls, ref dgv);
            Cls_BW_Utility.DgvSetUp(dgv);
        }


        private void Frm_MMM_MainForm_Load(object sender, EventArgs e)
        {
            //Uc_BW_Database uc_BW_Database = new Uc_BW_Database();
            //uc_BW_Database.Dock = DockStyle.Fill;
            //tpDatabase.Controls.Add(uc_BW_Database);

            //Uc_BW_Bom_Database uc_BW_Bom_Database = new Uc_BW_Bom_Database();
            //uc_BW_Bom_Database.Dock = DockStyle.Fill;
            //tpBoM.Controls.Add(uc_BW_Bom_Database);

            //if (BW.Cls_BW_Utility.applicationSettings.LoadAppSettings())
            //{
            //    Location = BW.Cls_BW_Utility.applicationSettings.MMM_FormLocation;
            //}
        }


        private void FrmMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //// e.Cancel = true;

            //BW.Cls_BW_Utility.applicationSettings.MMM_FormLocation = this.Location;
            //BW.Cls_BW_Utility.applicationSettings.SaveAppSettings();
        }

    }
}




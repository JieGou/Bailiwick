using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyFirstProject.BW.UPS
{
    public partial class Frm_UPS_MainForm : Form
    {
        public Frm_UPS_MainForm()
        {
            InitializeComponent();

            List<DataGridView> dgv = new List<DataGridView>();
            Cls_BW_Utility.FindControlsOfType(typeof(DataGridView), this.Controls, ref dgv);
            Cls_BW_Utility.DgvSetUp(dgv);
        }

        private void Frm_UPS_MainForm_Load(object sender, EventArgs e)
        {
            if (BW.Cls_BW_Utility.applicationSettings.LoadAppSettings())
            {
                Location = BW.Cls_BW_Utility.applicationSettings.UPS_FormLocation;
            }
        }

        private void Frm_UPS_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // e.Cancel = true;

            // File.SetAttributes(helpFileInstall, helpFileAttsInstall);

            BW.Cls_BW_Utility.applicationSettings.UPS_FormLocation = this.Location;
            BW.Cls_BW_Utility.applicationSettings.SaveAppSettings();
        }



    }
}

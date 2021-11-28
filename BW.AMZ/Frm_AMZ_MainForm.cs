using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyFirstProject.BW.AMZ
{
    public partial class Frm_AMZ_MainForm : Form
    {
        public Frm_AMZ_MainForm()
        {
            InitializeComponent();

            Uc_AMZ_SecurityDoors uc_AMZ_SecurityDoors1 = new Uc_AMZ_SecurityDoors();
            Uc_AMZ_TelComm uc_AMZ_TelComm1 = new Uc_AMZ_TelComm();

            uc_AMZ_SecurityDoors1.Dock = DockStyle.Fill;
            tpSecurity.Controls.Add(uc_AMZ_SecurityDoors1);

            uc_AMZ_TelComm1.Dock = DockStyle.Fill;
            tpTelComm.Controls.Add(uc_AMZ_TelComm1);


            List<DataGridView> dgv = new List<DataGridView>();
            Cls_BW_Utility.FindControlsOfType(typeof(DataGridView), this.Controls, ref dgv);
            Cls_BW_Utility.DgvSetUp(dgv);
        }
          

        private void Frm_AMZ_MainForm_Load(object sender, EventArgs e)
        { 
            if (BW.Cls_BW_Utility.applicationSettings.LoadAppSettings())
            {
                Location = BW.Cls_BW_Utility.applicationSettings.AMZ_FormLocation;      
            }
        }
        
        private void FrmAMZMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //  e.Cancel = true;
            BW.Cls_BW_Utility.applicationSettings.AMZ_FormLocation = this.Location;
            BW.Cls_BW_Utility.applicationSettings.SaveAppSettings();
        }

    

    }
}

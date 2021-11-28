using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyFirstProject.BW.USB
{
    public partial class Frm_USB_MainForm : Form
    {
        public Frm_USB_MainForm()
        {
            InitializeComponent();

            List<DataGridView> dgv = new List<DataGridView>();
            Cls_BW_Utility.FindControlsOfType(typeof(DataGridView), this.Controls, ref dgv);
            Cls_BW_Utility.DgvSetUp(dgv);
        }

        private void BtnNumberWAOs_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cls_USB_MainForm.BtnLabelWAOsIndividually_Sub(TxtBxWAOsNumber, chkBxAB);
            this.Show();
        }

        private void Frm_USB_MainForm_Load(object sender, EventArgs e)
        {
            if (BW.Cls_BW_Utility.applicationSettings.LoadAppSettings())
            {
                Location = BW.Cls_BW_Utility.applicationSettings.USB_FormLocation;
            }
        }

        private void Frm_USB_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // e.Cancel = true;

            BW.Cls_BW_Utility.applicationSettings.USB_FormLocation = this.Location;
            BW.Cls_BW_Utility.applicationSettings.SaveAppSettings();
        }

        private void BtnSetAttWidth_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cls_USB_MainForm.SetAttrWidthFactor();
            this.Show();
        }
    }
}

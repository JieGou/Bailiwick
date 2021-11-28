using System;
using System.Windows.Forms;

namespace MyFirstProject.BW.MMM
{
    public partial class Frm_MMM_Host_Form : Form
    {
        public Frm_MMM_Host_Form()
        {
            InitializeComponent();
        }         

        private void Frm_MMM_Host_Form_Load(object sender, EventArgs e)
        {  
            // show form as control in another form
            Frm_BW_MainForm_AsControl frm = new Frm_BW_MainForm_AsControl();
            Cls_BW_Utility.ShowFormInContainerControl(this, frm);

            // assign menu to data extraction panel with buttons
            var uc = frm.Controls.Find("uc_BW_TP_AttrExtract1", true)[0];
            var pnl = uc.Controls.Find("Panel1", true)[0];
            pnl.ContextMenuStrip = contextMenuStrip1;


            if (BW.Cls_BW_Utility.applicationSettings.LoadAppSettings())
            {
                Location = BW.Cls_BW_Utility.applicationSettings.MMM_FormLocation;
            }   
        }

        private void Frm_MMM_Host_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            BW.Cls_BW_Utility.applicationSettings.MMM_FormLocation = this.Location;
            BW.Cls_BW_Utility.applicationSettings.SaveAppSettings();
        }



        private void BOMFilenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //      openFileDialog1.InitialDirectory = BW.Cls_BW_Utility.applicationSettings.BW_AirMagRepPath;  //@"K:\3M\";

            openFileDialog1.Filter = "xlsx files | *.xlsx"; //|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.FileName = "*.xlsx"; // System.IO.Path.GetFileName(BW.Cls_BW_Utility.applicationSettings.MMM_BomFileName);
            openFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(BW.Cls_BW_Utility.applicationSettings.MMM_BomFileName);


            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BW.Cls_BW_Utility.applicationSettings.MMM_BomFileName = openFileDialog1.FileName;
                BW.Cls_BW_Utility.applicationSettings.SaveAppSettings();
            }
            else
            {
                return;
            }
        }

        private void GenBOMToolStripMenuItem_Click(object sender, EventArgs e)
        {  
            Cls_BW_TP_BomCounts.CreateBomXlFileWithTemplateByCloset(BW.Cls_BW_Utility.applicationSettings.MMM_BomFileName);  
        }





    }
}

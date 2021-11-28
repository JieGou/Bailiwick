using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using Autodesk.AutoCAD.ApplicationServices;

namespace MyFirstProject.BW.THD
{
    public partial class Frm_THD_MainForm : Form
    {
        public Frm_THD_MainForm()
        {
            InitializeComponent();
            
            List<DataGridView> dgv = new List<DataGridView>();
            Cls_BW_Utility.FindControlsOfType(typeof(DataGridView), this.Controls, ref dgv);
            Cls_BW_Utility.DgvSetUp(dgv);
        }

        private void Frm_THD_MainForm_Load(object sender, EventArgs e)
        {
            if (BW.Cls_BW_Utility.applicationSettings.LoadAppSettings())
            {
                Location = BW.Cls_BW_Utility.applicationSettings.THD_FormLocation;
            }
        }

        private void Frm_THD_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // e.Cancel = true;

            BW.Cls_BW_Utility.applicationSettings.THD_FormLocation = this.Location;
            BW.Cls_BW_Utility.applicationSettings.SaveAppSettings();
        }

        private void BtnSelectBlocks_Click(object sender, EventArgs e)
        {
            this.Hide();

            Cls_THD_Frm.THD_SelectBlocks();
                         
            dgvDevTags.DataSource = null;
            Cls_THD_Frm.lst_THD_Atts.Sort((x, y) => x.Label1.CompareTo(y.Label1));
            //tCtlMain.SelectedTab = tpDataExtraction;
            dgvDevTags.DataSource = Cls_THD_Frm.lst_THD_Atts;
            dgvDevTags.ClearSelection();
            dgvDevTags.AutoResizeColumns();


            dgvNewLst.DataSource = null;
       //     Cls_THD_Frm.lst_THD_Atts.Sort((x, y) => x.Label1.CompareTo(y.Label1));
            //tCtlMain.SelectedTab = tpDataExtraction;
            dgvNewLst.DataSource = Cls_THD_Frm.lst_THD_DisplayAtts;
            dgvNewLst.ClearSelection();
            dgvNewLst.AutoResizeColumns();
            
            this.Show();
        }

        private void btnSaveDataToFile_Click(object sender, EventArgs e)
        {
            string xlsxFileName;

            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            if (doc is null)
            {
                MessageBox.Show("No Drawing open");
                return;
            }
            
            xlsxFileName = doc.Name.ToLower();

            xlsxFileName = xlsxFileName.Replace(".dwg", "") + " " + //+ cBxTitleBlockChoices.Text + " " +
                DateTime.Now.ToString("yMMdd", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + " xl pull.xlsx";

            FileInfo newFile = new FileInfo(xlsxFileName);

            BW.Cls_BW_Utility.CreateFileXlPull<Cls_THD_DisplayAtts>(newFile, Cls_THD_Frm.lst_THD_DisplayAtts, "XL Pull");

            MessageBox.Show("XL Pull Saved To: " + xlsxFileName + Environment.NewLine +
                "XL Pull Saved for file: " + doc.Database.Filename);
        }




    }
}

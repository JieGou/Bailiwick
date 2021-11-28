using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;

namespace MyFirstProject.BW.WLG
{
    public partial class Frm_WLG_MainForm : Form
    {
        public Frm_WLG_MainForm()
        {
            InitializeComponent();

            List<DataGridView> dgv = new List<DataGridView>();
            Cls_BW_Utility.FindControlsOfType(typeof(DataGridView), this.Controls, ref dgv);
            Cls_BW_Utility.DgvSetUp(dgv);
        }

        private void Frm_WLG_MainForm_Load(object sender, EventArgs e)
        {
            if (BW.Cls_BW_Utility.applicationSettings.LoadAppSettings())
            {
                Location = BW.Cls_BW_Utility.applicationSettings.WLG_FormLocation;
            }
        }

        private void Frm_WLG_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // e.Cancel = true;

            BW.Cls_BW_Utility.applicationSettings.WLG_FormLocation = this.Location;
            BW.Cls_BW_Utility.applicationSettings.SaveAppSettings();
        }

        
        private void BtnSelectDevices_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Test");
            //return;

            DgvAllDevices.DataSource = null;
            DgvAllDevices.Invalidate();

            DgvHardware.DataSource = null;
            DgvHardware.Invalidate();

            this.Hide();

            Cls_WLG_Frm.WLG_SelectBlocks();
            //dgvDataDrops.DataSource = Cls_WLG_Frm.lst_WLG_Atts.Where(x =>x.BlockName == "datapoint").ToList();
            //DgvCameras.DataSource = Cls_WLG_Frm.lst_WLG_Atts.Where(x => x.BlockName == "Cameras").ToList();
            //DgvAPs.DataSource = Cls_WLG_Frm.lst_WLG_Atts.Where(x => x.BlockName == "datapoint").ToList();
            
            DgvAllDevices.DataSource = Cls_WLG_Frm.lst_WLG_Atts;
            DgvAllDevices.Invalidate();

            DgvHardware.DataSource = Cls_WLG_Frm.lst_WLG_HrdWr;
            DgvHardware.Invalidate();

            this.Show();
        }

        private void BtnWrkStaDevSum_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Cls_WLG_AC_Tbls.WLG_InsertTable_RunList("IDF");

            Cls_WLG_AC_Tbls.ModifyTable_WrkStaDevSum();

            this.Show();
        }

        private void BtnXlPullList_Click(object sender, EventArgs e)
        {
            this.Hide();

            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            string xlsxFileName = doc.Name; //.ToLower();      

            xlsxFileName = xlsxFileName.Replace(".dwg", "") + uc_BW_TP_Layouts1.SetText.Replace("__ ", " ") + " Set " +
                DateTime.Now.ToString("yMMdd", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + " xl pull.xlsx";

            FileInfo newFile = new FileInfo(xlsxFileName);

            BW.Cls_BW_Utility.CreateFileXlPull<Int_WLG_AllAtts>(newFile, Cls_WLG_Frm.lst_WLG_RunLst, "XL Pull");

            MessageBox.Show("XL Pull Saved To: " + xlsxFileName + Environment.NewLine +
                "XL Pull Saved for file: " + doc.Database.Filename);

            this.Show();
        }

        private void BtnAutoLabelByColor_Click(object sender, EventArgs e)
        {
            Cls_WLG_Frm.BtnAutoLabelByColor();
        }

        private void BtnClearGrids_Click(object sender, EventArgs e)
        {
            Cls_WLG_Frm.lst_WLG_Atts.Clear();
            DgvAllDevices.DataSource = null;

            Cls_WLG_Frm.lst_WLG_HrdWr.Clear();
            DgvHardware.DataSource = null;
        }

        private void BtnReadRunList_Click(object sender, EventArgs e)
        {
            DgvLstRun.DataSource = null;
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //      openFileDialog1.InitialDirectory = BW.Cls_BW_Utility.applicationSettings.BW_AirMagRepPath;  //@"K:\3M\";

            openFileDialog1.Filter = "xlsx files | *.xlsx"; //|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //     Cls_WLG_Frm.readXLS(@"K:\Walgreens\2019\RAD\10-01-2018 Line Run Summary ver 1.0 rev 1.0.xlsx");
                //Cls_WLG_Frm.readXLS(@"K:\Walgreens\2019\RAD\18303 Celina Line Run Summary - LRS V2.7 071519.xlsx");


                Cls_WLG_Frm.readXLS_V2(openFileDialog1.FileName); // LRS V2


            //    Cls_WLG_Frm.readXLS_V1(openFileDialog1.FileName); // LRS V1


                Cls_WLG_Frm.lst_WLG_RunLst.Sort((x, y) => x.LineNumber.CompareTo(y.LineNumber));


                DgvLstRun.DataSource = Cls_WLG_Frm.lst_WLG_RunLst;
                DgvLstRun.AutoResizeColumns();

                //DgvLstRun.DataSource = Cls_WLG_Frm.lst_WLG_Depts;

            }
            else
            {
                return;
            }   
        }

        private void BtnFillAtts_Click(object sender, EventArgs e)
        {
            this.Hide();

            Cls_WLG_Frm.SetAttsFromRunListGridView(DgvLstRun.SelectedRows[0].Index, ChkBxUpdateDataDrop.Checked);

            this.Show();
        }

        private void BtnPickOriginalBlock_Click(object sender, EventArgs e)
        {
            Cls_WLG_Frm.lst_WLG_Atts.Clear();
            
            Cls_WLG_Frm.lst_WLG_HrdWr.Clear();
         
            DgvAllDevices.DataSource = null;
            DgvAllDevices.Invalidate();

            DgvHardware.DataSource = null;
            DgvHardware.Invalidate();

            this.Hide();

            Cls_WLG_Frm.BtnPickOriginalBlock();
     
            this.Show();

            DgvAllDevices.DataSource = Cls_WLG_Frm.lst_WLG_Atts;
            DgvAllDevices.AutoResizeColumns();

            DgvHardware.DataSource = Cls_WLG_Frm.lst_WLG_HrdWr;
            DgvHardware.AutoResizeColumns();

            if (Cls_WLG_Frm.lst_WLG_Atts.Count == 0)
            {
                MessageBox.Show("Cls_WLG_Frm.lst_WLG_Atts.Count == 0, return");
                return;
            }

            int i = Cls_WLG_Frm.lst_WLG_RunLst.FindIndex(x => x.OldNumber == Cls_WLG_Frm.lst_WLG_Atts[0].CableLabel);
            if (i == -1)
            {
                MessageBox.Show("Can't Find Old Number");
            }
            else
            {
                DgvLstRun.Rows[i].Selected = true;
                DgvLstRun.FirstDisplayedScrollingRowIndex = i;
                DgvLstRun.AutoResizeColumns();
                
                Cls_WLG_Frm.SetAttsFromRunListGridView(DgvLstRun.SelectedRows[0].Index, ChkBxUpdateDataDrop.Checked);
            }
        }


        private void BtnMergeLists_Click(object sender, EventArgs e)
        {
            DgvLstRun.DataSource = null;

            //Cls_WLG_Frm.BtnMergeLists_Click_V1();
            Cls_WLG_Frm.BtnMergeLists_Click_V2();
                        

            DgvLstRun.DataSource = Cls_WLG_Frm.lst_WLG_RunLst;
        }

        private void BtnCameraCounts_Click(object sender, EventArgs e)
        {
            this.Hide();

            Cls_WLG_AC_Tbls.WLG_InsertTable_CameraSummary();

            this.Show();
        }




    }
}

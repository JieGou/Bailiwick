using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

namespace MyFirstProject.BW.FDX
{
    public partial class Frm_FDX_MainForm : Form
    {
        public Frm_FDX_MainForm()
        {
            InitializeComponent();

            List<DataGridView> dgv = new List<DataGridView>();
            Cls_BW_Utility.FindControlsOfType(typeof(DataGridView), this.Controls, ref dgv);
            Cls_BW_Utility.DgvSetUp(dgv);
        }

        private void Frm_FDX_MainForm_Load(object sender, EventArgs e)
        {
            if (BW.Cls_BW_Utility.applicationSettings.LoadAppSettings())
            {
                Location = BW.Cls_BW_Utility.applicationSettings.FDX_FormLocation;
            }
        }

        private void Frm_FDX_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // e.Cancel = true;

            // File.SetAttributes(helpFileInstall, helpFileAttsInstall);

            BW.Cls_BW_Utility.applicationSettings.FDX_FormLocation = this.Location;
            BW.Cls_BW_Utility.applicationSettings.SaveAppSettings();
        }
        

        string FolderOrSite = "";


        #region Engineering

        private void BtnFedExG_Eng_Install_Click(object sender, EventArgs e)
        {
            if (chkBxFileFolderOrSharePoint.Checked)
                FolderOrSite = @"\\my.bailiwick.com@SSL\DavWWWRoot\sites\FDX\036\Internal Project Documents\Disposition\Engineering\2020 FDX Site Prints\Install";
            else
                FolderOrSite = @"https://my.bailiwick.com/sites/FDX/036/Internal%20Project%20Documents/Forms/AllItems.aspx?RootFolder=%2Fsites%2FFDX%2F036%2FInternal%20Project%20Documents%2FDisposition%2FEngineering%2F2020%20FDX%20Site%20Prints%2FInstall&FolderCTID=0x0120008303A8BBC19B294EB15B45D08FA8591B&View=%7BAB3B5B0A%2D27E5%2D44A4%2D8FCF%2DA9C33E3883E0%7D";
            
            Cls_BW_Utility.OpenExplorerToDirOrSharePoint(FolderOrSite, chkBxFileFolderOrSharePoint.Checked);            
        }

        private void BtnFedExG_Eng_DeviceDetail_Click(object sender, EventArgs e)
        {
            if (chkBxFileFolderOrSharePoint.Checked)
                FolderOrSite = @"\\my.bailiwick.com@SSL\DavWWWRoot\sites\FDX\036\Internal Project Documents\Disposition\Engineering\2020 Device Detail";
            else
                FolderOrSite = @"https://my.bailiwick.com/sites/FDX/036/Internal%20Project%20Documents/Forms/AllItems.aspx?RootFolder=%2Fsites%2FFDX%2F036%2FInternal%20Project%20Documents%2FDisposition%2FEngineering%2F2020%20Device%20Detail&FolderCTID=0x0120008303A8BBC19B294EB15B45D08FA8591B&View=%7BAB3B5B0A%2D27E5%2D44A4%2D8FCF%2DA9C33E3883E0%7D";

            Cls_BW_Utility.OpenExplorerToDirOrSharePoint(FolderOrSite, chkBxFileFolderOrSharePoint.Checked);
        }

        private void BtnFedExG_Eng_Final_Click(object sender, EventArgs e)
        {
            if (chkBxFileFolderOrSharePoint.Checked)
                FolderOrSite = @"\\my.bailiwick.com@SSL\DavWWWRoot\sites\FDX\036\Internal Project Documents\Disposition\Engineering\2020 FDX Site Prints\Final";
            else
                FolderOrSite = @"https://my.bailiwick.com/sites/FDX/036/Internal%20Project%20Documents/Forms/AllItems.aspx?RootFolder=%2Fsites%2FFDX%2F036%2FInternal%20Project%20Documents%2FDisposition%2FEngineering%2F2020%20FDX%20Site%20Prints%2FFinal&FolderCTID=0x0120008303A8BBC19B294EB15B45D08FA8591B&View=%7BAB3B5B0A%2D27E5%2D44A4%2D8FCF%2DA9C33E3883E0%7D";

            Cls_BW_Utility.OpenExplorerToDirOrSharePoint(FolderOrSite, chkBxFileFolderOrSharePoint.Checked);
        }

        private void BtnFedExG_Eng_FinalSite_Click(object sender, EventArgs e)
        {          
            if (chkBxFileFolderOrSharePoint.Checked)
                FolderOrSite = @"\\home.bailiwick.com@SSL\DavWWWRoot\Toolkits\engineering\Client Name\Fed Ex Ground\Site Prints";
            else
                FolderOrSite = @"https://home.bailiwick.com/Toolkits/engineering/Client%20Name/Fed%20Ex%20Ground/Site%20Prints";

            Cls_BW_Utility.OpenExplorerToDirOrSharePoint(FolderOrSite, chkBxFileFolderOrSharePoint.Checked);
        }

        private void BtnFedExG_Eng_BoM_Click(object sender, EventArgs e)
        {
            if (chkBxFileFolderOrSharePoint.Checked)
                FolderOrSite = @"\\my.bailiwick.com@SSL\DavWWWRoot\sites\FDX\GIF\Internal Project Documents\BOM Library\2020 BOM Library";
            else
                FolderOrSite = @"https://my.bailiwick.com/sites/FDX/GIF/Internal%20Project%20Documents/BOM%20Library/2020%20BOM%20Library";

            Cls_BW_Utility.OpenExplorerToDirOrSharePoint(FolderOrSite, chkBxFileFolderOrSharePoint.Checked);
        }

        #endregion


        #region Customer
        private void BtnFedExG_Cstmr_Sites_Click(object sender, EventArgs e)
        {
            if (chkBxFileFolderOrSharePoint.Checked)
                FolderOrSite = @"\\my.bailiwick.com@SSL\DavWWWRoot\sites\FDX\036\Project Documentation\Site Folders";
            else
                FolderOrSite = @"https://my.bailiwick.com/sites/FDX/036/Project%20Documentation/Site%20Folders";

            Cls_BW_Utility.OpenExplorerToDirOrSharePoint(FolderOrSite, chkBxFileFolderOrSharePoint.Checked);
        }

        private void BtnFedExG_Cstmr_Predictions_Click(object sender, EventArgs e)
        {
            if (chkBxFileFolderOrSharePoint.Checked)
            {
                FolderOrSite = @"\\my.bailiwick.com@SSL\DavWWWRoot\sites\FDX\036\Project Documentation\UPLOAD Prediction Drawings";
            }
            else
                FolderOrSite = @"https://my.bailiwick.com/sites/FDX/036/Project%20Documentation/UPLOAD%20Prediction%20Drawings";

            Cls_BW_Utility.OpenExplorerToDirOrSharePoint(FolderOrSite, chkBxFileFolderOrSharePoint.Checked);
        }


        #endregion
        

        #region ProjectManagement

        private void BtnFedExG_Bail_ProjSiteInfo_Click(object sender, EventArgs e)
        {
            if (chkBxFileFolderOrSharePoint.Checked)
                FolderOrSite = @"\\my.bailiwick.com@SSL\DavWWWRoot\sites\FDX\GIF\Site Information";
            else
                FolderOrSite = @"https://my.bailiwick.com/sites/FDX/GIF/Site%20Information";

            Cls_BW_Utility.OpenExplorerToDirOrSharePoint(FolderOrSite, chkBxFileFolderOrSharePoint.Checked);
        }

        private void BtnFedExG_Bail_OssSchedule_Click(object sender, EventArgs e)
        {
            if (chkBxFileFolderOrSharePoint.Checked)
                FolderOrSite = @"\\my.bailiwick.com@SSL\DavWWWRoot\sites\FDX\036\Internal Project Documents\Site Lists\2020";
            else
                FolderOrSite = @"https://my.bailiwick.com/sites/FDX/036/Internal%20Project%20Documents/Site%20Lists/2020";

            Cls_BW_Utility.OpenExplorerToDirOrSharePoint(FolderOrSite, chkBxFileFolderOrSharePoint.Checked);
        }

        private void BtnFedExG_Bail_OssMilestones_Click(object sender, EventArgs e)
        {
            FolderOrSite = @"https://my.bailiwick.com/sites/FDX/036/Lists/Test/Completion%20Dates.aspx";

            Cls_BW_Utility.OpenExplorerToDirOrSharePoint(FolderOrSite, false);
        }


        private void BtnFedExG_Bail_OssDeviceDetail_Click(object sender, EventArgs e)
        {
            FolderOrSite = @"https://my.bailiwick.com/sites/FDX/036/Lists/Device%20Detail/All%20Items%20with%20Count.aspx";

            Cls_BW_Utility.OpenExplorerToDirOrSharePoint(FolderOrSite, false);
        }

        private void BtnFedExG_Bail_OssMacRequest_Click(object sender, EventArgs e)
        {
            FolderOrSite = @"https://my.bailiwick.com/sites/FDX/036/MAC%20Request/Forms/AllItems.aspx";

            Cls_BW_Utility.OpenExplorerToDirOrSharePoint(FolderOrSite, false);
        }

        #endregion

        

        /// <summary>
        /// FedExG Label Ap's, Test Points or Sensors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLblFedExAp_Click(object sender, EventArgs e)
        {
            var ModuleType = new[] { grpBxTypeOfLabel }
              .SelectMany(g => g.Controls.OfType<RadioButton>()
                                       .Where(r => r.Checked)).ToList();

            this.Hide();
            Cls_FDX_Main.counter = txtBxAPStrtNumFedExG.Text;
            Cls_FDX_Main.BtnLblFedExAp_Click_Sub(ModuleType[0].Text);
            txtBxAPStrtNumFedExG.Text = Cls_FDX_Main.counter;
            this.Show();
        }

        /// <summary>
        /// change selected entities to remove annotative
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRemoveAnnotative_Click(object sender, EventArgs e)
        {
            this.Hide();
            BW.Cls_BW_Utility.RemoveAnnotativeProperties("TEXT,MTEXT,INSERT,HATCH,LEADER,DIMENSION,MULTILEADER,ATTDEF");
            this.Show();
        }


        
        string curLayout = "";

        private void BtnPlotPdf_Click(object sender, EventArgs e)
        {
            curLayout = LayoutManager.Current.CurrentLayout;

            this.Hide();

            Cls_BW_Plot_Pdf_Dwg cls = new Cls_BW_Plot_Pdf_Dwg();

            string TxtToAdd = "";

            if (ChkBxUseDropListForPdf.Checked)
            {
                TxtToAdd = cBxPdfNameChoices.Text;
            }

            cls.PlotPdf("MMddy", new List<Layout>(), "DWG To PDF.pc3", "ANSI_full_bleed_B_(11.00_x_17.00_Inches)", 0.4, 0.4, false, TxtToAdd);

            LayoutManager.Current.CurrentLayout = curLayout;

            this.Show();
        }

        private void btnPlotMultDwgsToOnePdf_Click(object sender, EventArgs e)
        {
            rtBxDwgsToPlotToPdf.Lines = Cls_BW_Utility.ReturnFilesList();

            List<string> dwgs = rtBxDwgsToPlotToPdf.Lines.ToList();

            if (dwgs.Count > 0)
            {
                this.Hide();

                string TxtToAdd = "";

                if (ChkBxUseDropListForPdf.Checked)
                {
                    TxtToAdd = cBxPdfNameChoices.Text;
                }

                Cls_BW_Plot_Pdf_Dwgs cls = new Cls_BW_Plot_Pdf_Dwgs();
                cls.pltCls = new Cls_BW_Plot_Pdf_Dwg();
                cls.PlotPdf("DWG To PDF.pc3", "ANSI_full_bleed_B_(11.00_x_17.00_Inches)", 0.4, 0.4, dwgs, TxtToAdd);

                this.Show();
            }
            else
            {
                MessageBox.Show("No Drawings Selected!");
            }
        }


        #region Help Files Buttons

        Process processHelpInstall;
        string helpFileInstall = @"K:\FedEx Ground\_Documents\Creating Install Print Guideline 190501.docx";
        FileAttributes helpFileAttsInstall;

        private void BtnHelpInstall_Click(object sender, EventArgs e)
        {
            if (!File.Exists(helpFileInstall))
            {
                MessageBox.Show("Not connected to 'K' Drive?");
                return;
            }

            helpFileAttsInstall = File.GetAttributes(helpFileInstall);
            File.SetAttributes(helpFileInstall, helpFileAttsInstall | FileAttributes.ReadOnly);

            processHelpInstall = Process.Start(helpFileInstall);
            processHelpInstall.EnableRaisingEvents = true;
            processHelpInstall.Exited += new EventHandler(helpFile_ExitedInstall);
        }

        private void helpFile_ExitedInstall(object sender, System.EventArgs e)
        {
            File.SetAttributes(helpFileInstall, helpFileAttsInstall);
        }

        private void BtnHelpFinal_Click(object sender, EventArgs e)
        {
            FolderOrSite = @"K:\FedEx Ground\_Documents\Creating Final Print Guidline 190118.docx";

            if (!File.Exists(FolderOrSite))
            {
                MessageBox.Show("Not connected to 'K' Drive?");
                return;
            }

            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
            info.FileName = "explorer";
            info.Arguments = FolderOrSite;
            info.UseShellExecute = false;

            Process.Start(info);
        }

        private void BtnHelpRework_Click(object sender, EventArgs e)
        {
            FolderOrSite = @"K:\FedEx Ground\_Documents\Creating Rework Print Guideline 190207.docx";

            if (!File.Exists(FolderOrSite))
            {
                MessageBox.Show("Not connected to 'K' Drive?");
                return;
            }

            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
            info.FileName = "explorer";
            info.Arguments = FolderOrSite;
            info.UseShellExecute = false;

            System.Diagnostics.Process.Start(info);
        }


        #endregion


        /// <summary>
        /// any locked vports will be scaled to new standards
        /// for new networks    
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BntScaleObjs_Click(object sender, EventArgs e)
        {           
            TypedValue[] tvs = new TypedValue[] {

                new TypedValue((int)DxfCode.Operator, "<or" ),

                new TypedValue( (int)DxfCode.Operator, "<and" ),
                new TypedValue( (int)DxfCode.BlockName, "WIFI*" ),
                new TypedValue( (int)DxfCode.Operator, "and>" ),

                new TypedValue( (int)DxfCode.Operator, "<and" ),
                new TypedValue((int)DxfCode.Start, "MTEXT"),
                new TypedValue((int)DxfCode.LayerName, "iBwave_PartsId"),         
                new TypedValue( (int)DxfCode.Operator, "and>" ),

                new TypedValue( (int)DxfCode.Operator, "<and" ),
                new TypedValue((int)DxfCode.Start, "MTEXT"),          
                new TypedValue((int)DxfCode.LayerName, "iBwave_PartsModel"),
                new TypedValue( (int)DxfCode.Operator, "and>" ),

                new TypedValue( (int)DxfCode.Operator, "or>" )

            };

            Cls_BW_Viewport_Select cls = new Cls_BW_Viewport_Select();

            cls.SelectByViewportFdEx(
                new SelectionFilter(tvs),
                int.Parse(TxtBxScaleBlks.Text),
                int.Parse(TxtBxScaleTxt.Text)
                );

        }

        private void BtnBomCounts_Click(object sender, EventArgs e)
        {
            this.Hide();

            Cls_FDX_Main.BtnSelectBWaveText_Click_Sub();

            this.Show();
        }




        #region New Bom Count Retro
        /// <summary>
        /// select and move the iBWave text and blocks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectMoveIBWave_Click(object sender, EventArgs e)
        {
            this.Hide();

            Cls_FDX_Main.BtnSelectMoveIBWave_Click_Sub();

            this.Show();
        }


        private void BtnXdataSet_Click(object sender, EventArgs e)
        {
            this.Hide();

            FedExRetroBom.SetXData();

            this.Show();
        }


        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            var r = FedExRetroBom.MakeAttach(
                 txtBxSiteCode.Text,
                 cBxType.SelectedItem.ToString(),
                 ChkBxBomQtyBackBoard.Checked,
                 ChkBxDropDown.Checked

                 );

            dataGridView3.DataSource = r;

            dataGridView3.AutoResizeColumns();
        }

        private void BtnXdataSelect_Click(object sender, EventArgs e)
        {
            this.Hide();

            FedExRetroBom.SelectWithXData();

            FedExRetroBom.LstBom.Count();

            var nLst =  FedExRetroBom.LstBom.OrderBy(x => x.ApSort).ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = nLst;
            dataGridView1.AutoResizeColumns();      

            this.Show();
        }

        private void BtnTotals_Click(object sender, EventArgs e)
        {
            var r = FedExRetroBom.BomTotalsSub();

            dataGridView2.DataSource = null;
            dataGridView2.DataSource = r;

            dataGridView2.AutoResizeColumns();
        }




        #endregion

        private void BtnZoomAndSelect_Click(object sender, EventArgs e)
        {            
         
            ObjectId idD = 
                Cls_BW_Utility.ObjectIDFromHandle(HostApplicationServices.WorkingDatabase,
                dataGridView1[18, dataGridView1.CurrentRow.Index].Value.ToString()
                );
         
            ObjectId[] Ids;
     
                Ids = new ObjectId[1];
                Ids[0] = idD;
           
                ObjectIdCollection idCol = new ObjectIdCollection(Ids);
                Cls_BW_Utility.ZoomObjects(idCol, 8000, 8000);
                Autodesk.AutoCAD.Internal.Utils.SelectObjects(Ids);    

        }

        private void BtnExPortToExcel_Click(object sender, EventArgs e)
        {
            List<FedExRetroBom.Int_Bom_Item> iLstXlPull = FedExRetroBom.LstBom.Cast<FedExRetroBom.Int_Bom_Item>().ToList();

            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            if (doc is null)
            {
                MessageBox.Show("No Drawing open");
                return;
            }

            string  xlsxFileName = doc.Name; //.ToLower();

            xlsxFileName = xlsxFileName.Replace(".dwg", " ") + //" " + cBxTitleBlockChoices.Text + " " +
                DateTime.Now.ToString("yMMdd", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + " xl pull.xlsx";

            FileInfo newFile = new FileInfo(xlsxFileName);

            BW.Cls_BW_Utility.CreateFileXlPull<FedExRetroBom.Int_Bom_Item>(newFile, iLstXlPull, "Retro Bom XL Pull");

            MessageBox.Show("XL Pull Saved To: " + xlsxFileName + Environment.NewLine +
                "XL Pull Saved for file: " + doc.Database.Filename);
        }
    }
}

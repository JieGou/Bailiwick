using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

namespace MyFirstProject.BW.TGT
{
    public partial class Frm_TGT_MainForm : Form
    {
        public Frm_TGT_MainForm()
        {
            InitializeComponent();

            List<DataGridView> dgv = new List<DataGridView>();
            Cls_BW_Utility.FindControlsOfType(typeof(DataGridView), this.Controls, ref dgv);
            Cls_BW_Utility.DgvSetUp(dgv);
        }

        private void Frm_TGT_MainForm_Load(object sender, EventArgs e)
        {
            if (BW.Cls_BW_Utility.applicationSettings.LoadAppSettings())
            {
                Location = BW.Cls_BW_Utility.applicationSettings.TGT_FormLocation;
            }
        }

        private void Frm_TGT_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // e.Cancel = true;

            BW.Cls_BW_Utility.applicationSettings.TGT_FormLocation = this.Location;
            BW.Cls_BW_Utility.applicationSettings.SaveAppSettings();
        }
        
        private void BtnGetBlocks_Click(object sender, EventArgs e)
        {
            this.Hide();
            
            Cls_TGT_MainForm.SelectBlocks();

            dgvDataExtraction.DataSource = null;

            var allAttributes = Cls_TGT_MainForm.LstAtts_APs.OfType<Int_TGT_AllAtts>()
         //     .Concat(Cls_TGT_MainForm.LstAtts_Decoy.OfType<Int_TGT_AllAtts>())
              .Concat(Cls_TGT_MainForm.LstAtts_Drop.OfType<Int_TGT_AllAtts>())
              .Concat(Cls_TGT_MainForm.LstAtts_Phone.OfType<Int_TGT_AllAtts>())
              .Concat(Cls_TGT_MainForm.LstAtts_Speaker.OfType<Int_TGT_AllAtts>())
              ;

            allAttributes.ToList().ForEach(n => Cls_TGT_MainForm.LstXlPull.Add((Int_TGT_AllAtts)n));

            //Cls_TGT_MainForm.LstXlPull.Sort((x, y) => x.ITEM.CompareTo(y.ITEM));
            tCtlMain.SelectedTab = tpDataExtraction;
            dgvDataExtraction.DataSource = Cls_TGT_MainForm.LstXlPull;
            dgvDataExtraction.ClearSelection();
            dgvDataExtraction.AutoResizeColumns();

            this.Show();
        }

        private void BtnInsertTable_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cls_TGT_MainForm.TGT_InsertTable_BOM(tCtlMain.SelectedIndex);
            this.Show();
        }

        private void BtnTestData_Click(object sender, EventArgs e)
        {
            Cls_TGT_MainForm.BtnTestData_Click_Sub();
        }
                        

        #region data Extraction to Excel        

        private void BtnSaveDataToFile_Click(object sender, EventArgs e)
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            string xlsxFileName = doc.Name.ToLower();

            xlsxFileName = xlsxFileName.Replace(".dwg", "") +
                DateTime.Now.ToString("yMMdd", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + " xl pull.xlsx";

            FileInfo newFile = new FileInfo(xlsxFileName);

            BW.Cls_BW_Utility.CreateFileXlPull<Int_TGT_AllAtts>(newFile, Cls_TGT_MainForm.LstXlPull, "XL Pull");

            MessageBox.Show("XL Pull Saved To: " + xlsxFileName + Environment.NewLine +
                "XL Pull Saved for file: " + doc.Database.Filename);
        }

        private void BtnDataExtract_Click(object sender, EventArgs e)
        {
            try
            {
                dgvDataExtraction.DataSource = null;

                var allAttributes = Cls_TGT_MainForm.LstAtts_APs.OfType<Int_TGT_AllAtts>()
                    //     .Concat(Cls_TGT_MainForm.LstAtts_Decoy.OfType<Int_TGT_AllAtts>())
                    .Concat(Cls_TGT_MainForm.LstAtts_Drop.OfType<Int_TGT_AllAtts>())
                    .Concat(Cls_TGT_MainForm.LstAtts_Phone.OfType<Int_TGT_AllAtts>())
                    .Concat(Cls_TGT_MainForm.LstAtts_Speaker.OfType<Int_TGT_AllAtts>())
                    ;

                if (chkBxClearGrid.Checked)
                {
                    Cls_TGT_MainForm.LstXlPull.Clear();
                }

                allAttributes.ToList().ForEach(n => Cls_TGT_MainForm.LstXlPull.Add((Int_TGT_AllAtts)n));

                //Cls_TGT_MainForm.LstXlPull.Sort((x, y) => x.ITEM.CompareTo(y.ITEM));

                dgvDataExtraction.DataSource = Cls_TGT_MainForm.LstXlPull;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        #endregion
        

        private void BtnCounts_Click(object sender, EventArgs e)
        {
            DgvDisconnect();
            Cls_TGT_MainForm.CountsForTables();
            DgvSetDatasource();
            DgvResizecolumns();
        }

        private void DgvResizecolumns()
        {
            dgvTVSCounts.AutoResizeColumns();
            dgvPagingCounts.AutoResizeColumns();
            dgvVoiceAndData.AutoResizeColumns();
            dgvT4VoiceAndData.AutoResizeColumns();
        }

        private void DgvSetDatasource()
        {
            dgvTVSCounts.DataSource = Cls_TGT_MainForm.LstTbl_VoiceAndDataTVS;
            dgvPagingCounts.DataSource = Cls_TGT_MainForm.LstTbl_PagingCounts;
            dgvVoiceAndData.DataSource = Cls_TGT_MainForm.LstTbl_VoiceAndData;
            dgvT4VoiceAndData.DataSource = Cls_TGT_MainForm.LstTbl_VoiceAndDataT4;
        }

        private void DgvDisconnect()
        {
            dgvTVSCounts.DataSource = null;
            dgvPagingCounts.DataSource = null;
            dgvVoiceAndData.DataSource = null;
            dgvT4VoiceAndData.DataSource = null;
        }



        #region OpexBid

        private void BtnOpexCounts_Click(object sender, EventArgs e)
        {
            this.Hide();
            FilterMtextWildcard();
            this.Show();
        }
        private void FilterMtextWildcard()
        {
            Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            TypedValue[] acTypValAr = new TypedValue[1];
            acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "MTEXT"), 0);
            //acTypValAr.SetValue(new TypedValue((int)DxfCode.LayerName, "T-DATA"), 1);
       //     acTypValAr.SetValue(new TypedValue((int)DxfCode.Text, "*AP*"), 1);

            SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);

            PromptSelectionResult acSSPrompt;

            bool loop = true;

            do
            {

                acSSPrompt = acDocEd.GetSelection(acSelFtr);

                if (acSSPrompt.Status == PromptStatus.OK)
                {
                    SelectionSet acSSet = acSSPrompt.Value;

                    ObjectId[] objIds = acSSet.GetObjectIds();
                    using (acDocEd.Document.LockDocument())
                    using (var Transaction = acDocEd.Document.Database.TransactionManager.StartTransaction())
                    {
                        //  string info = "";
                        Cls_TGT_Extract cls = new Cls_TGT_Extract();
                        foreach (ObjectId MTextObjId in objIds)
                        {
                            var current_MTextObj = Transaction.GetObject(MTextObjId, OpenMode.ForWrite) as MText;
                                              
                            if (cls.aType == ".")
                                cls.aType = current_MTextObj.Text;
                            else if (cls.bCloset_Num1 == ".")
                                cls.bCloset_Num1 = current_MTextObj.Text;
                            else if (cls.cDesc1 == ".")
                                cls.cDesc1 = current_MTextObj.Text;
                            else if (cls.dCloset_Num2 == ".")
                                cls.dCloset_Num2 = current_MTextObj.Text;
                            else if (cls.eDesc2 == ".")
                                cls.eDesc2 = current_MTextObj.Text;

                            current_MTextObj.ColorIndex = 3;

                        //    info = info + current_MTextObj.Text + ", ";
                                              
                        }
                        //  acDocEd.WriteMessage(info.Substring(0, info.Length - 1) + Environment.NewLine);

                        lstExt.Add(cls);
                        //dgv_Extract.DataSource = null;
                        //dgv_Extract.DataSource = lstExt;
                        Transaction.Commit();

                    }
                }
                //else
                //{
                //    Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
                //}
                else
                {
                    loop = false;
                    dgv_Extract.DataSource = null;
                    dgv_Extract.DataSource = lstExt;
                }
            } while (loop == true);
        }


        List<Cls_TGT_Extract> lstExt = new List<Cls_TGT_Extract>();

        //public interface Int_TGT_Extract
        //{
        //    string Type { get; set; }
        //    string Closet_Num1 { get; set; }
        //    string Desc1 { get; set; }
        //    string Closet_Num2 { get; set; }
        //    string Desc2 { get; set; }
            
        //}

        public class Cls_TGT_Extract //: Int_TGT_Extract 
        {
            public string aType { get; set; } = ".";
            public string bCloset_Num1 { get; set; } = ".";
            public string cDesc1 { get; set; } = ".";
            public string dCloset_Num2 { get; set; } = ".";
            public string eDesc2 { get; set; } = ".";

        }





        #endregion


        /// <summary>
        /// kwik fix for an asbuilt labels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void BtnPickPrintIdAttribute_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cls_TGT_MainForm.UpdatePrintIdAttribute(CBoxPrefix.Text, txtBx_NumToStartAt);
            this.Show();



        }



    }
}

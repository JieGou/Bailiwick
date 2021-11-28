using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using System.IO;

namespace MyFirstProject.BW
{
    public partial class Uc_BW_TP_AttrExtract : UserControl
    {
        public Uc_BW_TP_AttrExtract()
        {
            InitializeComponent();
        }


        public static readonly List<Int_BW_AllAtts> LstXlPull = new List<Int_BW_AllAtts>();

        private void BtnSaveDataToFile_Click(object sender, EventArgs e)
        {
            string xlsxFileName;

            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            if (doc is null)
            {
                MessageBox.Show("No Drawing open");
                return;
            }

            xlsxFileName = doc.Name; //.ToLower();

            xlsxFileName = xlsxFileName.Replace(".dwg", "") + //" " + cBxTitleBlockChoices.Text + " " +
                DateTime.Now.ToString("yMMdd", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + " xl pull.xlsx";

            FileInfo newFile = new FileInfo(xlsxFileName);

            BW.Cls_BW_Utility.CreateFileXlPull<Int_BW_AllAtts>(newFile, LstXlPull, "XL Pull");

            MessageBox.Show("XL Pull Saved To: " + xlsxFileName + Environment.NewLine +
                "XL Pull Saved for file: " + doc.Database.Filename);
        }

        private void BtnDataExtract_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!chkBxSeperateBomCountsPerCloset.Checked)
                //{
                //    Frm_BW_MainForm_AsControl_Code.BomCounts();
                //    Frm_BW_MainForm_AsControl_Code.ShowBomCountsInDgv(dgvExtractCountsWap, dgvExtractCountsWao);
                //}
                //else
                //{
                Cls_BW_TP_BomCounts.BomCountsPerCloset();
                Cls_BW_TP_BomCounts.ShowBomCountsInDgvByCloset(dgvExtractCountsWap, dgvExtractCountsWao);
                //}

                dgvAllAtts.DataSource = null;

                var both = Cls_BW_TP_APs.LstAtts_wap_data_2_dual.OfType<Int_BW_AllAtts>()
                    .Concat(Cls_BW_TP_WAOs.LstAtts_waos.OfType<Int_BW_AllAtts>());

                if (chkBxClearGrid.Checked)
                {
                    LstXlPull.Clear();
                }

                both.ToList().ForEach(n => LstXlPull.Add((Int_BW_AllAtts)n));

                LstXlPull.Sort((x, y) => x.Label1.CompareTo(y.Label1));

                dgvAllAtts.DataSource = LstXlPull;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }




        /// <summary>
        /// special request for cottage grove
        /// </summary>
        private void BtnTextExtractSpecial_Click(object sender, EventArgs e)
        {
            this.ParentForm.ParentForm.Hide();
            Cls_BW_TP_TextExtract.BtnTextExtractSpecial_Click();
            this.ParentForm.ParentForm.Show();
        }

        private void BtnCounts_Click(object sender, EventArgs e)
        {
      

            var wcntCat6 = Cls_BW_TP_APs.LstAtts_wap_data_2_dual.Where(x => x.CableCombinations.ToUpper() == "CAT6").Count();

            var wcntCat6A = Cls_BW_TP_APs.LstAtts_wap_data_2_dual.Where(x => x.CableCombinations.ToUpper() == "CAT6A").Count();



            int wc1 = Cls_BW_TP_WAOs.LstAtts_waos.Where(x => x.CableCombinations.ToUpper() == "CAT6" & x.Single != "0").Count();
            int wc2 = Cls_BW_TP_WAOs.LstAtts_waos.Where(x => x.CableCombinations.ToUpper() == "CAT6" & x.Dual != "0").Count();
            int wc3 = Cls_BW_TP_WAOs.LstAtts_waos.Where(x => x.CableCombinations.ToUpper() == "CAT6" & x.Triple != "0").Count();
            int wc4 = Cls_BW_TP_WAOs.LstAtts_waos.Where(x => x.CableCombinations.ToUpper() == "CAT6" & x.Quad != "0").Count();
            int wc5 = Cls_BW_TP_WAOs.LstAtts_waos.Where(x => x.CableCombinations.ToUpper() == "CAT6" & x.Quint != "0").Count();
            int wc6 = Cls_BW_TP_WAOs.LstAtts_waos.Where(x => x.CableCombinations.ToUpper() == "CAT6" & x.Sextet != "0").Count();

            int ttlWaoCnt = wc1 + wc2 + wc3 + wc4 + wc5 + wc6;


            var ocntCat6 = Cls_BW_TP_WAOs.LstAtts_waos.Where(x => x.CableCombinations.ToUpper() == "CAT6").Count();

            var ocntCat6A = Cls_BW_TP_WAOs.LstAtts_waos.Where(x => x.CableCombinations.ToUpper() == "CAT6A").Count();
        }





    }
}

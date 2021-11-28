using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace MyFirstProject.BW
{
    public partial class Uc_BW_TP_Word : UserControl
    {
        public Uc_BW_TP_Word()
        {
            InitializeComponent();
        }

        private void BtnWord_Click(object sender, EventArgs e)
        {
            Cls_BW_TP_Word.BtnWord_Click_Sub();
        }

        private void BtnGetAllPiDocs_Click(object sender, EventArgs e)
        {
            //rtBxReport.Clear();
            //string msg = Cls_BW_TP_Word.GetReport();
            //rtBxReport.AppendText(msg);
        }

        List<Cls_BW_TP_Word.ClsPi> l = new List<Cls_BW_TP_Word.ClsPi>();

        private void BtnGetPiList_Click(object sender, EventArgs e)
        {
            DgvPiList.DataSource = null;
            l = Cls_BW_TP_Word.GetReport(new List<Cls_BW_TP_Word.ClsPi>(), folder);      
            DgvPiList.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DgvPiList.DataSource = l;

        }

        private void BtnSavePiToXL_Click(object sender, EventArgs e)
        {
            string fn = "PI Report " +
            DateTime.Now.ToString("yMMdd", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + ".xlsx";

            FileInfo fi = new FileInfo(folder + "\\" + fn);

            BW.Cls_BW_Utility.CreateFilePiReport<Cls_BW_TP_Word.IntPi>(fi, l, "PI Report");

            MessageBox.Show("XL Pull Saved To: " + fi.FullName);

            //BW.Cls_BW_Utility.ProcessStart()
        }

        string folder { get; set; }

        private void BtnGetFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
              
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folder = fd.SelectedPath;
            }
            else
            {
                folder = "...";
            }
            LblFolder.Text = folder;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MyFirstProject.BW
{
    public partial class Uc_BW_Database : UserControl
    {
        public Uc_BW_Database()
        {
            InitializeComponent();

            List<DataGridView> dgv = new List<DataGridView>();
            Cls_BW_Utility.FindControlsOfType(typeof(DataGridView), this.Controls, ref dgv);
            Cls_BW_Utility.DgvSetUp(dgv);
        }

 



        #region database


        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            using (var db = new MyFirstProject.Database.Model1())
            {
                Refresh(db);
            }
        }
        private void BtnAddNewDatabaseRecord_Click(object sender, EventArgs e)
        {
            using (var db = new MyFirstProject.Database.Model1())
            {
                var tbl = new Database.DbTable();

                tbl.CadEngineer = txtBxCadEng.Text;
                tbl.Date = DateTime.Now;
                tbl.PDF = txtBxPdf.Text;

                db.Tables.Add(tbl);
                db.SaveChanges();


                Refresh(db);
            }

        }
        private void Refresh(MyFirstProject.Database.Model1 db)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = typeof(MyFirstProject.Database.DbTable);
            db.Tables.ToList().ForEach(n => bs.Add(n));
            dgvTestTable.DataSource = bs;
        }


        #endregion

    }
}

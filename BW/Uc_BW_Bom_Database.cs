using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MyFirstProject.BW
{
    public partial class Uc_BW_Bom_Database : UserControl
    {
        public Uc_BW_Bom_Database()
        {
            InitializeComponent();


            List<DataGridView> dgv = new List<DataGridView>();
            Cls_BW_Utility.FindControlsOfType(typeof(DataGridView), this.Controls, ref dgv);
            Cls_BW_Utility.DgvSetUp(dgv);
        }


        private void BtnRefreshBomTable_Click(object sender, EventArgs e)
        {
            using (var db = new MyFirstProject.Database.Model1())
            {
                RefreshBoM(db);
            }
        }
        private void RefreshBoM(MyFirstProject.Database.Model1 db)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = typeof(Database.BoM);
            db.BoM.ToList().ForEach(n => bs.Add(n));
            dgvBoM.DataSource = bs;
        }

        private void BtnAddBomItem_Click(object sender, EventArgs e)
        {
            using (var db = new MyFirstProject.Database.Model1())
            {
                var tblBomItem = new Database.BoM();

                tblBomItem.PartId = "Test 001";
                tblBomItem.ParentPartId = null;
                tblBomItem.Name = "Antenna";
                tblBomItem.Description = "Antenna Description";

                tblBomItem.Quantity = 1;
                tblBomItem.UnitOfMeasure = 1;
                tblBomItem.Type = 1;
                tblBomItem.Group = 1;

                db.BoM.Add(tblBomItem);
                db.SaveChanges();


                RefreshBoM(db);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using System.Reflection;
using Autodesk.AutoCAD.ApplicationServices;
using System.IO;
using Autodesk.AutoCAD.EditorInput;


namespace MyFirstProject.BW.AMZ
{
    public partial class Uc_AMZ_TelComm : UserControl
    {
        public Uc_AMZ_TelComm()
        {
            InitializeComponent();
        }


        #region TelComm


        private void BtnSaveXlPullTelComm_Click(object sender, EventArgs e)
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            string xlsxFileName = doc.Name.ToLower();

            xlsxFileName = xlsxFileName.Replace(".dwg", "") + " xl pull Tel " +
                DateTime.Now.ToString("yMMdd", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + ".xlsx";

            FileInfo newFile = new FileInfo(xlsxFileName);

            BW.Cls_BW_Utility.CreateFileXlPull<BW.AMZ.Int_AMZ_SecurityDoorsAtts>(newFile, Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag, "Tel");

            MessageBox.Show("XL Pull Saved To: " + xlsxFileName + Environment.NewLine +
                "XL Pull Saved for file: " + doc.Database.Filename);
        }

   


        private void BtnTelCommGet_Click(object sender, EventArgs e)
        {
            BtnTelCommGet_Click_Sub();
        }

        private void BtnTelCommGet_Click_Sub()
        {
            cBxAPCFilterTelComm.SelectedIndexChanged -= new System.EventHandler(CmbBxAPCFilterTelComm_SelectedIndexChanged);

            this.ParentForm.Hide();

            dgvTelComm.DataSource = null;
            Cls_AMZ_TelComm.AMZ_SelectBlocksTelComm();
            dgvTelComm.DataSource = Cls_AMZ_TelComm.AMZ_LstTelCommAtts;
            dgvTelComm.RowHeadersVisible = false;
            dgvTelComm.ReadOnly = true;
            dgvTelComm.ClearSelection();
            dgvTelComm.AutoResizeColumns();


            cBxAPCFilterTelComm.DataSource = null;
            Cls_AMZ_TelComm.GetLstFilterAPCsTelComm();
            cBxAPCFilterTelComm.DataSource = Cls_AMZ_TelComm.LstClsAPCFilterListTelComm;
            cBxAPCFilterTelComm.DisplayMember = "ACP";
            cBxAPCFilterTelComm.SelectedIndexChanged += new System.EventHandler(CmbBxAPCFilterTelComm_SelectedIndexChanged);

            this.ParentForm.Show();
        }



        private void CmbBxAPCFilterTelComm_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblCnt1100.Text = "";
            //lblCnt1200.Text = "";
            //lblCnt1320.Text = "";

            //var ModuleType = new[] { gBxModuleType }
            //           .SelectMany(g => g.Controls.OfType<RadioButton>()
            //                                    .Where(r => r.Checked)).ToList();

            Cls_AMZ_TelComm.FilterIDFsTelComm = Cls_AMZ_TelComm.AMZ_LstTelCommAtts.Where(x =>
               (x.IDF == cBxAPCFilterTelComm.Text)
                ).ToList();

            //Filter.Sort((x, y) => x.DoorNumber.CompareTo(y.DoorNumber));

            //if (ModuleType[0].Text == "LNL-1100")
            //{
            //    var distinctDoorNumbers = Filter.DistinctBy(c => c.DoorNumber);
            //    double moduleCount = Math.Truncate((double)(distinctDoorNumbers.Count() + 3) / 16) + 1;
            //    lblCnt1100.Text = moduleCount.ToString();
            //}
            //if (ModuleType[0].Text == "LNL-1200")
            //{
            //    var distinctDoorNumbers = Filter.DistinctBy(c => c.DoorNumber);
            //    double moduleCount = Math.Truncate((double)(distinctDoorNumbers.Count() + 3) / 16) + 1;
            //    lblCnt1200.Text = moduleCount.ToString();
            //}
            //if (ModuleType[0].Text == "LNL-1320")
            //{
            //    var distinctDoorNumbers = Filter.DistinctBy(c => c.DoorNumber);
            //    lblCnt1320.Text = distinctDoorNumbers.ToString();
            //}

            dgvTelComm.DataSource = null;
            dgvTelComm.DataSource = Cls_AMZ_TelComm.FilterIDFsTelComm;
            dgvTelComm.Refresh();

            // counts
            Cls_AMZ_TelComm.AMZ_TelCommBomCount(cBxAPCFilterTelComm.Text);

            DataGridView dgv = (DataGridView) this.ParentForm.Controls.Find("dgvTelCommCounts", true)[0];

            dgv.DataSource = null;

            dgv.Rows.Clear();
            dgv.Columns.Clear();
            dgv.Columns.Add("BomItem", "BomItem");
            dgv.Columns.Add("ItemQty", "ItemQty");
            dgv.Columns[0].Width = 500;
            dgv.Columns[1].Width = 100;

            PropertyInfo[] properties = typeof(BW.AMZ.Cls_AMZ_TelComm_IdfTableCounts).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object[] o = { ".", "." };
                o[0] = property.Name;
                o[1] = property.GetValue(Cls_AMZ_TelComm.AMZ_ClsTelCommBomCounts, null);
                dgv.Rows.Add(o);
            }

            dgv.Refresh();
        }





        private void BtnZoomAndSelTelComm_Click(object sender, EventArgs e)
        {
            BW.Cls_BW_Utility.BtnZoomToAndSelectBlock_Click_Sub(dgvTelComm, 10);
        }

        private void BtnZoomAndSelAllTelComm_Click(object sender, EventArgs e)
        {
            List<ObjectId> Ids = new List<ObjectId>();
            var fltredHandles = Cls_AMZ_TelComm.FilterIDFsTelComm.Select(x => x.Handle).ToList();

            foreach (Handle c in fltredHandles)
            {
                ObjectId idD = Cls_BW_Utility.ObjectIDFromHandle(
                    HostApplicationServices.WorkingDatabase,
                             c.ToString());

                Ids.Add(idD);
            }

            ObjectIdCollection idCol = new ObjectIdCollection(Ids.ToArray());

            Cls_BW_Utility.ZoomObjects(idCol);
            Autodesk.AutoCAD.Internal.Utils.SelectObjects(Ids.ToArray());
        }

        private void BtnRemoveFilterTelComm_Click(object sender, EventArgs e)
        {
            dgvTelComm.DataSource = null;
            dgvTelComm.DataSource = Cls_AMZ_TelComm.AMZ_LstTelCommAtts;
            dgvTelComm.Refresh();
        }

        private void BtnInsertAutoCadTable_Click(object sender, EventArgs e)
        {
            this.ParentForm.Hide();
            Cls_AMZ_TelComm.AMZ_InsertTable_BOM(cBxAPCFilterTelComm.Text);
            this.ParentForm.Show();
        }


        // area
        private void BtnGetNewArea_Click_1(object sender, EventArgs e)
        {
            this.ParentForm.Hide();

            Cls_BW_AreaCmd Cls_Area = Cls_BW_AreaCmd.MyCommands.GetNewArea();

            BW.AMZ.Cls_AMZ_IdfAreas aMZ_IdfAreas = new BW.AMZ.Cls_AMZ_IdfAreas();
            aMZ_IdfAreas.idf_Number = cBxAPCFilterTelComm.Text;
            aMZ_IdfAreas.idf_Area = Cls_Area.Area / 12;
            aMZ_IdfAreas.idf_Perimeter = Cls_Area.Perimeter / 12;

            Cls_AMZ_TelComm.AMZ_LstIdfAreas.Add(aMZ_IdfAreas);
            Cls_AMZ_TelComm.AMZ_LstIdfAreas.Sort((x, y) => x.idf_Number.CompareTo(y.idf_Number));

            ShowAreasInGrid();

            this.ParentForm.Show();
        }
        private void ShowAreasInGrid()
        {
            dgvIdfAreas.Rows.Clear();
            dgvIdfAreas.Columns.Clear();
            dgvIdfAreas.Columns.Add("IDF", "IDF");
            dgvIdfAreas.Columns.Add("Area", "Area");
            dgvIdfAreas.Columns.Add("Perimeter", "Perimeter");
            dgvIdfAreas.AutoResizeColumns();

            foreach (var area in Cls_AMZ_TelComm.AMZ_LstIdfAreas)
            {
                dgvIdfAreas.Rows.Add(area.idf_Number, area.idf_Area, area.idf_Perimeter);
            }

            dgvIdfAreas.Refresh();
        }

        private void DeleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int inx = dgvIdfAreas.CurrentRow.Index;
            Cls_AMZ_TelComm.AMZ_LstIdfAreas.RemoveAt(inx);
            ShowAreasInGrid();
        }

        private void DeleteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cls_AMZ_TelComm.AMZ_LstIdfAreas.Clear();
            ShowAreasInGrid();
        }


        #endregion





        #region Drops Table

        private void BtnGetDataFromTable_Click(object sender, EventArgs e)
        {
            this.ParentForm.Hide();

            Cls_AMZ_TelComm.BtnGetDataFromTable_Click_Sub();
            
            dgvTelComm.DataSource = null;
            dgvTelComm.DataSource = Cls_AMZ_TelComm.lstDrps;

            dgvTelComm.RowHeadersVisible = false;
            dgvTelComm.ReadOnly = true;
            dgvTelComm.ClearSelection();
            dgvTelComm.AutoResizeColumns();

            this.ParentForm.Show();
        }
     
        private void BtnGenerateRowsForDropTable_Click(object sender, EventArgs e)
        {
            Cls_AMZ_TelComm.BtnGenerateRowsForDropTable_Click_Sub(ChkBxStrtCamsOnNewSwitch.Checked, ChkBxCamsFirst.Checked);

            dgvTelComm.DataSource = null;
            dgvTelComm.DataSource = Cls_AMZ_TelComm.lstDropsRows;

            dgvTelComm.RowHeadersVisible = false;
            dgvTelComm.ReadOnly = true;
            dgvTelComm.ClearSelection();
            dgvTelComm.AutoResizeColumns();
        }


        private void BtnPortsTable_Click(object sender, EventArgs e)
        {
            this.ParentForm.Hide();

            Cls_AMZ_TelComm.AMZ_ModifyTable_Ports();

            this.ParentForm.Show();
        }
















        #endregion

        private void BtnAddAtts_Click(object sender, EventArgs e)
        {
            this.ParentForm.Hide();
            Editor acDocEd = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            TypedValue[] acTypValAr = new TypedValue[9];
            acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "INSERT"), 0);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.Operator, "<OR"), 1);

            acTypValAr.SetValue(new TypedValue((int)DxfCode.BlockName, "SINGLE"), 2);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.BlockName, "DUAL"), 3);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.BlockName, "TRIPLE"), 4);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.BlockName, "QUAD"), 5);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.BlockName, "WALL TV"), 6);
            acTypValAr.SetValue(new TypedValue((int)DxfCode.BlockName, "TMC"), 7);

            acTypValAr.SetValue(new TypedValue((int)DxfCode.Operator, "OR>"), 8);

            SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);

            PromptSelectionResult acSSPrompt = acDocEd.GetSelection(acSelFtr);

            if (acSSPrompt.Status == PromptStatus.OK)
            {
                Cls_AMZ_TelComm.BlockRefAttributeSort(acSSPrompt);
            }
            else
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Number of objects selected: 0");
            }

            this.ParentForm.Show();
        }
    }
}

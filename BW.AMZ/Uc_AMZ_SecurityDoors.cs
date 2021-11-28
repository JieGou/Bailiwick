using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

using MoreLinq;
using Autodesk.AutoCAD.DatabaseServices;
using System.IO;
using Autodesk.AutoCAD.ApplicationServices;

namespace MyFirstProject.BW.AMZ
{
    public partial class Uc_AMZ_SecurityDoors : UserControl
    {
        public Uc_AMZ_SecurityDoors()
        {
            InitializeComponent();
        }

        BindingSource bndngSource = new BindingSource();

        #region Security


        #region Number Security Doors


        private void BtnXYSort_Click(object sender, EventArgs e)
        {
            List<Cls_AMZ_SecurityDoors_Atts> doorContactOHD = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.Device_Type == "DC" & x.DoorType == "OHD").ToList();

            Cls_AMZ_SecurityDoors.doorContact_SortedXY.Clear();
            var nLst = doorContactOHD.OrderByDescending(x => x.InsertionPtOfBlock.Y).ThenBy(x => x.InsertionPtOfBlock.X).ToList();
            Cls_AMZ_SecurityDoors.doorContact_SortedXY.AddRange(nLst);

            dgvSecDevTags.DataSource = null;
            dgvSecDevTags.DataSource = Cls_AMZ_SecurityDoors.doorContact_SortedXY;
        }

        private void BtnNumberDoors_Click(object sender, EventArgs e)
        {
            Cls_AMZ_SecurityDoors.NumberDoorTypes(chkBxShowClr.Checked);
        }


        #endregion


        private void BtnAssignPorts_Click(object sender, EventArgs e)
        {
            Cls_AMZ_SecurityDoors.AMZ_AssignPorts();

            bndngSource = new BindingSource() { DataSource = Cls_AMZ_SecurityDoors.AMZ_LstPortAssignmentsSecurity };
            dgvSecDevTags.DataSource = null;
            dgvSecDevTags.DataSource = bndngSource;
        }


        private void LstBxDuplicates_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.Clear(); 
                var nLst = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x =>
            (x.Device_Type == cBxDeviceType.Text && x.DeviceID == lstBxDuplicates.Text)
            ).ToList();
            Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.AddRange(nLst);

            bndngSource = new BindingSource() { DataSource = Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity };
            dgvSecDevTags.DataSource = null;
            dgvSecDevTags.DataSource = bndngSource;
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            Cls_AMZ_SecurityDoors.AMZ_FillSecDoorsList(Cls_AMZ_Main.acSSPrompt);
            dgvSecDevTags.DataSource = null;
            dgvSecDevTags.DataSource = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag;
            CmbBxAPCFilter_SelectedIndexChanged_Sub();
        }

        /// <summary>
        /// not accurate counts btnCameraCount_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCameraCount_Click(object sender, EventArgs e)
        {
            Cls_AMZ_TelComm.AMZ_LstIdfCameras.Clear();

            List<Cls_AMZ_SecurityDoors_Atts> FilterIDFsSecurity = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.Module == "LNL-1100").ToList();

            FilterIDFsSecurity.Sort((x, y) => x.DoorNumber.CompareTo(y.DoorNumber));

            int totalCount = 0;
            string s = s = "Camera Count";

            foreach (var idf in Cls_AMZ_SecurityDoors.AMZ_LstClsAPCFilterList)
            {
                Cls_AMZ_IdfCameras cam = new Cls_AMZ_IdfCameras();

                int OHD = FilterIDFsSecurity.Where(x => x.Panel == idf.ACP & x.DoorType == "OHD").Count();
                int EXD = FilterIDFsSecurity.Where(x => x.Panel == idf.ACP & x.DoorType == "EXD").Count();
                int XD = FilterIDFsSecurity.Where(x => x.Panel == idf.ACP & x.DoorType == "XD").Count();
                //     int ACP = FilterIDFsSecurity.Where(x => x.Panel == idf.APC & x.DoorType == "ACP").Count();

                s = s + "\n\r" + idf.ACP + ": " + (OHD + EXD + XD).ToString().PadLeft(3, '0');

                totalCount = totalCount + (OHD + EXD + XD);

                cam.idf_Number = idf.ACP;
                cam.idf_OHD = OHD;
                cam.idf_EXD = EXD;
                cam.idf_EXD = XD;
                //    cam.idf_ACP = ACP;
                double res = ((OHD + EXD + XD) / 2);
                cam.idf_Total = (int)Math.Round(res, 0);
                if (cam.idf_Total > 0)
                {
                    Cls_AMZ_TelComm.AMZ_LstIdfCameras.Add(cam);
                }
            }

            totalCount = (int)totalCount / 2;

            //  lblCameraCount.Text = s + "\n\rTotalCount: " + totalCount.ToString();

            bndngSource = new BindingSource() { DataSource = FilterIDFsSecurity };
            dgvSecDevTags.DataSource = null;
            dgvSecDevTags.DataSource = bndngSource;
        }


        private void BtnSaveAttsToXLPull_Click(object sender, EventArgs e)
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            string xlsxFileName = doc.Name.ToLower();

            xlsxFileName = xlsxFileName.Replace(".dwg", "") + " xl pull Sec " +
                DateTime.Now.ToString("yMMdd", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + ".xlsx";

            FileInfo newFile = new FileInfo(xlsxFileName);

            BW.Cls_BW_Utility.CreateFileXlPull<BW.AMZ.Int_AMZ_SecurityDoorsAtts>(newFile, Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag, "Sec");

            MessageBox.Show("XL Pull Saved To: " + xlsxFileName + Environment.NewLine +
                "XL Pull Saved for file: " + doc.Database.Filename);
        }

        private void BtnSelectSecurityDoors_Click(object sender, EventArgs e)
        {
            BtnSecDoorsGet_Click_Sub();
        }
        private void BtnSecDoorsGet_Click_Sub()
        {
            this.ParentForm.Hide();

            dgvSecDevTags.DataSource = null;

            Cls_AMZ_SecurityDoors.AMZ_SelectBlocks();

            dgvSecDevTags.DataSource = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag;

            dgvSecDevTags.RowHeadersVisible = false;
            dgvSecDevTags.ReadOnly = true;
            dgvSecDevTags.ClearSelection();
            dgvSecDevTags.AutoResizeColumns();


            Cls_AMZ_SecurityDoors.AMZ_SecDrsBomCount();
            Cls_AMZ_SecurityDoors.ShowBomCountsInDgv((DataGridView) this.ParentForm.Controls.Find("dgvCounts", true)[0]);

            cBxAPCFilter.SelectedIndexChanged -= new System.EventHandler(CBxAPCFilter_SelectedIndexChanged);
            cBxNetwork.SelectedIndexChanged -= new System.EventHandler(CBxNetworkFilter_SelectedIndexChanged);
            cBxDeviceType.SelectedIndexChanged -= new System.EventHandler(CBxDeviceType_SelectedIndexChanged);

            cBxAPCFilter.DataSource = null;
            Cls_AMZ_SecurityDoors.GetLstFilterAPCs();
            cBxAPCFilter.DataSource = Cls_AMZ_SecurityDoors.AMZ_LstClsAPCFilterList;
            cBxAPCFilter.DisplayMember = "ACP";

            cBxNetwork.DataSource = null;
            Cls_AMZ_SecurityDoors.GetLstFilterNetworks();
            cBxNetwork.DataSource = Cls_AMZ_SecurityDoors.AMZ_LstClsNetworkFilterList;
            cBxNetwork.DisplayMember = "Isc";

            cBxDeviceType.DataSource = null;
            Cls_AMZ_SecurityDoors.GetLstFilterDevTyp();
            cBxDeviceType.DataSource = Cls_AMZ_SecurityDoors.AMZ_LstClsDevTypFilterList;
            cBxDeviceType.DisplayMember = "DeviceType";

            cBxDeviceType.SelectedIndexChanged += new System.EventHandler(CBxDeviceType_SelectedIndexChanged);
            cBxAPCFilter.SelectedIndexChanged += new System.EventHandler(CBxAPCFilter_SelectedIndexChanged);
            cBxNetwork.SelectedIndexChanged += new System.EventHandler(CBxNetworkFilter_SelectedIndexChanged);

            this.ParentForm.Show();
        }

        private void ChkBxDoorNumbers_AllOrOne_CheckedChanged(object sender, EventArgs e)
        {
            CmbBxAPCFilter_SelectedIndexChanged_Sub();
        }

        private void RbAllModules_Click(object sender, EventArgs e)
        {
            CmbBxAPCFilter_SelectedIndexChanged_Sub();
        }



        private void BtnInsertAllMudules_Click(object sender, EventArgs e)
        {
            this.ParentForm.Hide();

            var ModuleType = new[] { gBxModuleType }
                  .SelectMany(g => g.Controls.OfType<RadioButton>()
                                           .Where(r => r.Checked)).ToList();

            if (ModuleType[0].Text == "LNL-1320")
            {
                List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts> lst = new List<Cls_AMZ_SecurityDoors_Atts>();

                if (dgvSecDevTags.DataSource is BindingSource)
                {
                    lst = ((List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts>)bndngSource.List).ToList();
                }
                else if (dgvSecDevTags.DataSource is List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts>)
                {
                    lst = ((List<BW.AMZ.Cls_AMZ_SecurityDoors_Atts>)dgvSecDevTags.DataSource).ToList();
                }

                AMZ.Cls_AMZ_SecurityDoors_DrawJig j = new AMZ.Cls_AMZ_SecurityDoors_DrawJig();
                j.RunJig(lst);
            }


            // counts
            if (ModuleType[0].Text == "LNL-1100")
            {
                AMZ.Cls_AMZ_SecurityDoors_DrawJig j = new AMZ.Cls_AMZ_SecurityDoors_DrawJig();
                j.RunJig(int.Parse(lblCnt1100.Text), ModuleType[0].Text);
            }
            if (ModuleType[0].Text == "LNL-1200")
            {
                AMZ.Cls_AMZ_SecurityDoors_DrawJig j = new AMZ.Cls_AMZ_SecurityDoors_DrawJig();
                j.RunJig(int.Parse(lblCnt1200.Text), ModuleType[0].Text);
            }

            this.ParentForm.Show();
        }




        private void BtnFilterOnly1320_Click(object sender, EventArgs e)
        {
            var Filter = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.BlockName.Contains("CR 1320")).ToList();

            Filter.Sort((x, y) => x.DeviceID.CompareTo(y.DeviceID));

            dgvSecDevTags.DataSource = null;
            dgvSecDevTags.DataSource = Filter;
            dgvSecDevTags.Refresh();
        }

        private void BtnRemoveFilter_Click(object sender, EventArgs e)
        {
            dgvSecDevTags.DataSource = null;
            dgvSecDevTags.DataSource = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag;
            dgvSecDevTags.Refresh();
        }

        private void BtnZoomToAndSelectBlock_Click(object sender, EventArgs e)
        {
            BW.Cls_BW_Utility.BtnZoomToAndSelectBlock_Click_Sub(dgvSecDevTags, 32);
        }

        private void BtnZoomAndSelectAll_Click(object sender, EventArgs e)
        {
            List<ObjectId> Ids = new List<ObjectId>();
            var fltredHandles = Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.Select(x => x.Handle).ToList();

            if (fltredHandles.Count == 0)
            {
                fltredHandles = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Select(x => x.Handle).ToList();
            }

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




        private void CBxAPCFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbBxAPCFilter_SelectedIndexChanged_Sub();
        }
        private void CmbBxAPCFilter_SelectedIndexChanged_Sub()
        {
            lblAllModules.Text = "";
            lblCnt1100.Text = "";
            lblCnt1200.Text = "";
            lblCnt1320.Text = "";


            var ModuleType = new[] { gBxModuleType }
                       .SelectMany(g => g.Controls.OfType<RadioButton>()
                                                .Where(r => r.Checked)).ToList();

            Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.Clear();
            var nLst = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x =>
               (x.Module == ModuleType[0].Text & x.Panel == cBxAPCFilter.Text & x.Isc != "A1")
               ).ToList();
            Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.AddRange(nLst);

            Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.Sort((x, y) => x.DoorNumber.CompareTo(y.DoorNumber));

            if (ModuleType[0].Text == "All Modules")
            {
                Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.Clear();
                nLst = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x =>
                (x.Panel == cBxAPCFilter.Text)
                ).ToList();
                Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.AddRange(nLst);

                Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.Sort((x, y) => x.DoorNumber.CompareTo(y.DoorNumber));

                var distinctDoorNumbers = Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.DistinctBy(c => c.DoorNumber);
                double moduleCount = distinctDoorNumbers.Count();
                lblAllModules.Text = moduleCount.ToString();
            }


            // counts
            if (ModuleType[0].Text == "LNL-1100")
            {
                double moduleCount = Math.Truncate((double)(Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.Count() + 3) / 16) + 1;
                lblCnt1100.Text = moduleCount.ToString();
            }
            if (ModuleType[0].Text == "LNL-1200")
            {
                double moduleCount = Math.Truncate((double)(Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.Count() + 3) / 16) + 1;
                lblCnt1200.Text = moduleCount.ToString();
            }
            if (ModuleType[0].Text == "LNL-1320")
            {
                var distinctDoorNumbers = Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.DistinctBy(c => c.DoorNumber);
                double moduleCount = distinctDoorNumbers.Count();
           //     lblAllModules.Text = moduleCount.ToString();

        //        double moduleCount = Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.Count();
                lblCnt1320.Text = moduleCount.ToString();
            }


            dgvSecDevTags.ReadOnly = false;
            dgvSecDevTags.AllowDrop = true;

            if (chkBxDoorNumbers_AllOrOne.Checked)
            {
                bndngSource = new BindingSource() { DataSource = Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity };
                dgvSecDevTags.DataSource = null;
                dgvSecDevTags.DataSource = bndngSource;
            }
            else
            {
                bndngSource = new BindingSource() { DataSource = Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.DistinctBy(c => c.DoorNumber).ToList() };
                dgvSecDevTags.DataSource = null;
                dgvSecDevTags.DataSource = bndngSource;
            }


            dgvSecDevTags.Refresh();
        }

        private void CBxNetworkFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.Clear();
            var nLst = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.Isc.Contains(cBxNetwork.Text)).ToList();
            Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.AddRange(nLst);

            // no null device id's
            var hasNull = Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.Where(x => x.DeviceID == null).ToList();

            if (hasNull.Count == 0)
            {
                Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.Sort((x, y) => x.DeviceID.CompareTo(y.DeviceID));
            }
            else
            {
                MessageBox.Show(hasNull.Count.ToString() + ": Null Device ID(s) - Aborting Sort");
            }

            dgvSecDevTags.DataSource = null;
            dgvSecDevTags.DataSource = Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity;
            dgvSecDevTags.Refresh();
        }





        private void CBxDeviceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.Clear();
            var nLst = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag.Where(x => x.Device_Type != null & x.Device_Type == cBxDeviceType.Text).ToList();
            Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.AddRange(nLst);

            Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.Sort((x, y) => x.DeviceID.CompareTo(y.DeviceID));

            dgvSecDevTags.DataSource = null;
            dgvSecDevTags.DataSource = Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity;
            dgvSecDevTags.Refresh();

            var dups = Cls_AMZ_SecurityDoors.AMZ_LstFilterIDFsSecurity.GetDuplicates(x => x.DeviceID);

            lstBxDuplicates.Items.Clear();

            foreach (var d in dups)
            {
                lstBxDuplicates.Items.Add(d.DeviceID);
            }
        }


        #endregion


        #region port Assignments Xl file

        private void BtnSavePorts_Click(object sender, EventArgs e)
        {
            Cls_AMZ_SecurityDoors.MkPortAssignments("Ports");
        }
        
        #endregion



        #region door Numbers for DC's ect...

        private void BtnPickBlocksForDoorNumber_Click(object sender, EventArgs e)
        {
            this.ParentForm.Hide();

            dgvSecDevTags.DataSource = null;
            
            List<ObjectId> ids = Cls_AMZ_SecurityDoors.AssignDoorNums(TxtBxDoorNumPrefix.Text, nudDoorNum.Text);

            Cls_AMZ_SecurityDoors.AMZ_FillSecDoorsList_Sub(ids.ToArray());

            dgvSecDevTags.DataSource = Cls_AMZ_SecurityDoors.AMZ_LstAtts_DevTag;

            dgvSecDevTags.RowHeadersVisible = false;
            dgvSecDevTags.ReadOnly = true;
            dgvSecDevTags.ClearSelection();
            dgvSecDevTags.AutoResizeColumns();


            nudDoorNum.Text = Cls_AMZ_SecurityDoors.DoorNum.ToString();

            //Autodesk.AutoCAD.Internal.Utils.SelectObjects(ids.ToArray());

            this.ParentForm.Show();
        }



        #endregion






        private void BtnAttsTest_Click(object sender, EventArgs e)
        {
            this.ParentForm.Hide();

            Cls_AMZ_SecurityDoors.SwapAttributeReferences();

            this.ParentForm.Show();
        }




    }
}

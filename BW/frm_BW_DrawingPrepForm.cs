using System;
using System.Collections.Generic;
using System.Windows.Forms;



using Autodesk.AutoCAD.ApplicationServices;
using Microsoft.Win32;
using Autodesk.AutoCAD.Colors;
using System.Diagnostics;

namespace MyFirstProject.BW
{
    public partial class Frm_BW_DrawingPrepForm : Form
    {
        public Frm_BW_DrawingPrepForm()
        {
            InitializeComponent();

            List<DataGridView> dgv = new List<DataGridView>();
            Cls_BW_Utility.FindControlsOfType(typeof(DataGridView), this.Controls, ref dgv);
            Cls_BW_Utility.DgvSetUp(dgv);
        }

        
        private void FrmDrawingPrep_Load(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\BailiwickCUSTOMER");
            if (key != null)
            {
                cmbBxCustormer.SelectedItem = key.GetValue("Customer").ToString();
            }
            else
            {
                Registry.CurrentUser.CreateSubKey(@"SOFTWARE\BailiwickCUSTOMER");
            }

            RegistryKey keyBW = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\BailiwickCAD");
            if (keyBW != null)
            {
                cmbBxCustormer.Items.Clear();

                string[] s = keyBW.GetSubKeyNames();
                cmbBxCustormer.Items.Add("Bailiwick");
                cmbBxCustormer.Items.AddRange(s);              
            }
            else
            {
                Registry.CurrentUser.CreateSubKey(@"SOFTWARE\BailiwickCAD");
                cmbBxCustormer.Items.Clear();                
                cmbBxCustormer.Items.Add("Bailiwick");
                BtnSaveColorsToReg_Click_Sub("");
                cmbBxCustormer.SelectedItem = "Bailiwick";
            }


        


            if (BW.Cls_BW_Utility.applicationSettings.LoadAppSettings())
            {              
                Location = BW.Cls_BW_Utility.applicationSettings.BW_FormLocation;              
            }

        }
        

        private void FrmDrawingPrep_FormClosing(object sender, FormClosingEventArgs e)
        {
            //  e.Cancel = true;

            BW.Cls_BW_Utility.applicationSettings.BW_FormLocation = this.Location;
            BW.Cls_BW_Utility.applicationSettings.SaveAppSettings();
        }

        #region Dwg Prep

        private Autodesk.AutoCAD.Colors.Color _ColorForLayers { get; set; } = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.None, (short)8);
        private  Autodesk.AutoCAD.Colors.Color _ColorForBlocks { get; set; } = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.None, (short)8);
        private  Autodesk.AutoCAD.Colors.Color _ColorForModel { get; set; } = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.None, (short)8);

        private  Autodesk.AutoCAD.Colors.Color _ColorForHatchGrad1 { get; set; } = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.None, (short)8);
        private  Autodesk.AutoCAD.Colors.Color _ColorForHatchGrad2 { get; set; } = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.None, (short)8);

        //private  string _LineTypeName { get; set; }
        //private  ObjectId _LineTypeId { get; set; }

        // public static Autodesk.AutoCAD.Colors.Color _LineWeight;


        private void BtnLayersOnAll_Click(object sender, EventArgs e)
        {
            BW.Frm_BW_DrawingPrepForm_Code.Bw_ThawAndUnlockLayers();
        }

        private void BtnBw_PurgeUnreferencedLayers_Click(object sender, EventArgs e)
        {
            BW.Frm_BW_DrawingPrepForm_Code.Bw_PurgeUnreferencedLayers();
        }

        private void BtnPrepDrawing_Click(object sender, EventArgs e)
        {
            Cls_BW_HandleAutocadObjTypes.CirDia = double.Parse(TxtBxCirDia.Text);
            Cls_BW_HandleAutocadObjTypes.LinLen = double.Parse(TxtBxLinLen.Text);
            Cls_BW_HandleAutocadObjTypes.TxtHt = double.Parse(TxtBxTxtHt.Text);

            BW.Frm_BW_DrawingPrepForm_Code.Bw_LayerIterator(_ColorForLayers);
            BW.Frm_BW_DrawingPrepForm_Code.Bw_BlockIterator(_ColorForBlocks, _ColorForHatchGrad1, _ColorForHatchGrad2);
            BW.Frm_BW_DrawingPrepForm_Code.Bw_ModelSpaceIterator(_ColorForModel, _ColorForHatchGrad1, _ColorForHatchGrad2);
        }

        private void BtnSelectColorForLayer_Click(object sender, EventArgs e)
        {
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.IncludeByBlockByLayer = true;
            DialogResult res = cd.ShowDialog();
            if (res == DialogResult.OK)
            {
                _ColorForLayers = cd.Color;
                lblSelClrFoLayr.Text = _ColorForLayers.ToString();
                System.Drawing.Color color = _ColorForLayers.ColorValue;
                panel1.BackColor = color;
            }
        }

        private void BtnSelectColorForBlocks_Click(object sender, EventArgs e)
        {
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.IncludeByBlockByLayer = true;
            DialogResult res = cd.ShowDialog();
            if (res == DialogResult.OK)
            {
                _ColorForBlocks = cd.Color;
                lblSelClrForBlks.Text = _ColorForBlocks.ToString();
                System.Drawing.Color color = _ColorForBlocks.ColorValue;
                panel2.BackColor = color;
            }
        }

        private void BtnSelectColorForModel_Click(object sender, EventArgs e)
        {
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.IncludeByBlockByLayer = true;
            DialogResult res = cd.ShowDialog();
            if (res == DialogResult.OK)
            {
                _ColorForModel = cd.Color;
                lblSelClrForModl.Text = _ColorForModel.ToString();
                System.Drawing.Color color = _ColorForModel.ColorValue;
                panel3.BackColor = color;
            }
        }

        private void BtnGradient1_Click(object sender, EventArgs e)
        {
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.IncludeByBlockByLayer = true;
            DialogResult res = cd.ShowDialog();
            if (res == DialogResult.OK)
            {
                _ColorForHatchGrad1 = cd.Color;
                lblSelectGradient1.Text = _ColorForHatchGrad1.ToString();
                System.Drawing.Color color = _ColorForHatchGrad1.ColorValue;
                panel4.BackColor = color;
            }
        }

        private void BtnGradient2_Click(object sender, EventArgs e)
        {
            Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
            cd.IncludeByBlockByLayer = true;
            DialogResult res = cd.ShowDialog();
            if (res == DialogResult.OK)
            {
                _ColorForHatchGrad2 = cd.Color;
                lblSelectGradient2.Text = _ColorForHatchGrad2.ToString();
                System.Drawing.Color color = _ColorForHatchGrad2.ColorValue;
                panel5.BackColor = color;
            }
        }

        private void BtnSaveColorsToReg_Click_Sub(string customer)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\BailiwickCAD" + customer);

            key.SetValue("_ColorForLayers", _ColorForLayers.ColorIndex);
            key.SetValue("_ColorForBlocks", _ColorForBlocks.ColorIndex);
            key.SetValue("_ColorForModel", _ColorForModel.ColorIndex);
            key.SetValue("_ColorForHatchGrad1", _ColorForHatchGrad1.ColorIndex);
            key.SetValue("_ColorForHatchGrad2", _ColorForHatchGrad2.ColorIndex);

        //    key.SetValue("_LineTypeName", _LineTypeName);

            key.Close();
        }

        //     using (Transaction tr = db.TransactionManager.StartTransaction())
        //{
        //    LayerTable layerTable = tr.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
        //LinetypeTable lineTypeTable = tr.GetObject(db.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;

        //    if (layerTable.Has(layerName))
        //    {

        /// <summary>
        /// gets colors for customer from regestry
        /// Use:  \customer
        /// </summary>
        /// <param name="customer"></param>
        private void BtnGetColorsFromReg_Click_Sub(string customer) // \customer
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\BailiwickCAD" + customer);
            if (key != null)
            {
                _ColorForLayers = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.None, short.Parse(key.GetValue("_ColorForLayers").ToString()));
                _ColorForBlocks = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.None, short.Parse(key.GetValue("_ColorForBlocks").ToString()));
                _ColorForModel = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.None, short.Parse(key.GetValue("_ColorForModel").ToString()));
                _ColorForHatchGrad1 = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.None, short.Parse(key.GetValue("_ColorForHatchGrad1").ToString()));
                _ColorForHatchGrad2 = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.None, short.Parse(key.GetValue("_ColorForHatchGrad2").ToString()));

            //    _LineTypeName = key.GetValue("_LineTypeName").ToString();

                //Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
                //using (Transaction tr = database.TransactionManager.StartTransaction())
                //{
                //    //LayerTable layerTable = tr.GetObject(database.LayerTableId, OpenMode.ForRead) as LayerTable;
                //    LinetypeTable lineTypeTable = tr.GetObject(database.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;

                //    if (lineTypeTable.Has(_LineTypeName))
                //    {
                //        lblLineType.Text = _LineTypeName;
                //    }
                //    else
                //    {

                //        lblLineType.Text = "NONE";
                //    }
                //}

                //Autodesk.AutoCAD.DatabaseServices.Database database = HostApplicationServices.WorkingDatabase;
                //// Convert hexadecimal string to 64-bit integer

                ////l = l.Replace("(", "");
                ////l = l.Replace(")", "");

                //long lnD = Convert.ToInt64(l, 16);                
                //// Now create a Handle from the long integer
                //Handle hnD = new Handle(lnD);                
                //ObjectId idD = database.GetObjectId(false, hnD, 0);
                //_LineTypeName = idD;
                //Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                //using (var tr = doc.TransactionManager.StartTransaction())
                //{                   
                //    LinetypeTableRecord LtTblRec = (LinetypeTableRecord)tr.GetObject(_LineTypeName, OpenMode.ForRead);
                //    lblLineType.Text = LtTblRec.Comments;                
                //}


                lblSelClrFoLayr.Text = _ColorForLayers.ToString();
                panel1.BackColor = _ColorForLayers.ColorValue;

                lblSelClrForBlks.Text = _ColorForBlocks.ToString();
                panel2.BackColor = _ColorForBlocks.ColorValue;

                lblSelClrForModl.Text = _ColorForModel.ToString();
                panel3.BackColor = _ColorForModel.ColorValue;

                lblSelectGradient1.Text = _ColorForHatchGrad1.ToString();
                panel4.BackColor = _ColorForHatchGrad1.ColorValue;

                lblSelectGradient2.Text = _ColorForHatchGrad2.ToString();
                panel5.BackColor = _ColorForHatchGrad2.ColorValue;
            }
        }

        private void BtnDelOffFrznLayers_Click(object sender, EventArgs e)
        {
            BW.Frm_BW_DrawingPrepForm_Code.Bw_DeleteOffFrznLyrs();
        }

        private void BtnPurgeRegApps_Click(object sender, EventArgs e)
        {
            BW.Frm_BW_DrawingPrepForm_Code.Bw_PurgeRegApps();
        }


        //open the linetypetable and check whether linetypetable.has(linetype). in addition to reverting to any 
        //particular linetype - which may be perfectly fine - you can check for alternative linetypes and/or load a particular linetype file.

        private void BtnForceLineTypes_Click(object sender, EventArgs e)
        {
            Autodesk.AutoCAD.Windows.LinetypeDialog cd = new Autodesk.AutoCAD.Windows.LinetypeDialog();
            cd.IncludeByBlockByLayer = true;
            DialogResult res = cd.ShowDialog();
            if (res == DialogResult.OK)
            {
                Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

                using (var tr = doc.TransactionManager.StartTransaction())
                {
                    //_LineTypeId = cd.Linetype;
                    //LinetypeTableRecord LtTblRec = (LinetypeTableRecord)tr.GetObject(_LineTypeId, OpenMode.ForRead);

                    //_LineTypeName = LtTblRec.Name;
                    //lblLineType.Text = LtTblRec.Name;
                }

            }
        }

        private void BtnForceLineWeights_Click(object sender, EventArgs e)
        {
            Autodesk.AutoCAD.Windows.LineWeightDialog cd = new Autodesk.AutoCAD.Windows.LineWeightDialog();
            cd.IncludeByBlockByLayer = true;
            DialogResult res = cd.ShowDialog();
            //if (res == DialogResult.OK)
            //{
            //    _ColorForHatchGrad1 = cd.Color;
            //    lblSelectGradient1.Text = _ColorForHatchGrad1.ToString();
            //    System.Drawing.Color color = _ColorForHatchGrad1.ColorValue;
            //    panel4.BackColor = color;
            //    btnSaveColorsToReg_Click_Sub();
            //}
        }


        private void BtnSaveForCustomer_Click(object sender, EventArgs e)
        {      
            if (cmbBxCustormer.Text == "Bailiwick")
            {
                BtnSaveColorsToReg_Click_Sub("");
            }
            else
            {
                BtnSaveColorsToReg_Click_Sub("\\" + cmbBxCustormer.Text);
            }

            RegistryKey keyBW = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\BailiwickCAD");
            if (keyBW != null)
            {
                cmbBxCustormer.Items.Clear();

                string[] s = keyBW.GetSubKeyNames();
                cmbBxCustormer.Items.Add("Bailiwick");
                cmbBxCustormer.Items.AddRange(s);
            }

        }
        private void BtnDeleteSelected_Click(object sender, EventArgs e)
        {
            if (cmbBxCustormer.Text == "Bailiwick")
            {
                MessageBox.Show("Cannot Delete Customer Bailiwick!");
                return;
            }

            DialogResult res = MessageBox.Show("Are You Sure You Want To Delete '" + cmbBxCustormer.Text + " '", "Delete Customer?", MessageBoxButtons.YesNo);

            if (res == DialogResult.No)
            {
                return;
            }
            else
            {
                Registry.CurrentUser.DeleteSubKey(@"SOFTWARE\BailiwickCAD" + "\\" + cmbBxCustormer.Text);

                RegistryKey keyBW = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\BailiwickCAD");
                if (keyBW != null)
                {
                    cmbBxCustormer.Items.Clear();

                    string[] s = keyBW.GetSubKeyNames();
                    cmbBxCustormer.Items.Add("Bailiwick");
                    cmbBxCustormer.Items.AddRange(s);
                }
            }
        }

        private void CmbBxCustormer_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\BailiwickCUSTOMER");
            if (key != null)
            {
                key.SetValue("Customer", ((ComboBox)sender).Text);
            }
            
            if (((ComboBox)sender).Text == "Bailiwick")
            {
                BtnGetColorsFromReg_Click_Sub("");
            }
            else  
            {
                BtnGetColorsFromReg_Click_Sub("\\" + ((ComboBox)sender).Text);
            }
      
        }

        #endregion
        #region Tab Two

        private void BtnDumpAttsAllBlocks_Click(object sender, EventArgs e)
        {
            BW.Frm_BW_DrawingPrepForm_Code.ListBlockAtts();
        }





        private void ChkBxEraseCircles_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            if (cb.Checked)
            {
                Cls_BW_HandleAutocadObjTypes.EraseCircles = true;
            }
            else
            {
                Cls_BW_HandleAutocadObjTypes.EraseCircles = false;
            }
        }

        private void ChkBxEraseLines_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            if (cb.Checked)
            {
                Cls_BW_HandleAutocadObjTypes.EraseLines = true;
            }
            else
            {
                Cls_BW_HandleAutocadObjTypes.EraseLines = false;
            }
        }

        private void ChkBxEraseText_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            if (cb.Checked)
            {
                Cls_BW_HandleAutocadObjTypes.EraseText = true;
            }
            else
            {
                Cls_BW_HandleAutocadObjTypes.EraseText = false;
            }
        }



        #endregion

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\HelpCadProg\Documentation In Word.docx");
        }



    }




}


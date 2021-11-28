using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

using Autodesk.AutoCAD.DatabaseServices;

namespace MyFirstProject.BW
{
    public partial class Uc_BW_TP_Layouts : UserControl
    {

        [Description("Type of Dwg Set Issued"), Category("Data")]
        public string SetText
        {
            get { return cBxTitleBlockChoices.Text; }
            set { cBxTitleBlockChoices.Text = value; }
        }



        public Uc_BW_TP_Layouts()
        {
            InitializeComponent();
        }

        private void BtnChangeTitleBlockPhase_Click(object sender, EventArgs e)
        {
            this.ParentForm.ParentForm.Hide();

            Cls_BW_TP_Layouts.ChangeTitleBlocks(cBxTitleBlockChoices.Text);

            this.ParentForm.ParentForm.Show();
        }


        string curLay = "";

        List<Layout> layouts = new List<Layout>();

        List<Layout> layoutsSelectedForPlot = new List<Layout>();


        private void Uc_BW_TP_Layouts_SizeChanged(object sender, EventArgs e)
        {
            ChkLstBxSelectLayouts.Height = this.Height - 50;
        }
          

        private void BtnGetLayouts_Click(object sender, EventArgs e)
        { 
            ChkLstBxSelectLayouts.Items.Clear();

            layouts = Cls_BW_Utility.GetLayouts();
            
            foreach (Layout l in layouts)
            {
                ChkLstBxSelectLayouts.Items.Add(l.LayoutName);
            }  
        }
        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            layoutsSelectedForPlot.Clear();

            for (int i = 0; i < ChkLstBxSelectLayouts.Items.Count; i++)
            {
                ChkLstBxSelectLayouts.SetItemChecked(i, true);
            }

            layoutsSelectedForPlot.AddRange(layouts);
        }
        private void BtnClearLayoutList_Click(object sender, EventArgs e)
        {
            ChkLstBxSelectLayouts.Items.Clear();
            layoutsSelectedForPlot.Clear();
            layouts.Clear();
        }
        private void BtnPlotPdf_Click(object sender, EventArgs e)
        {
            curLay = LayoutManager.Current.CurrentLayout;

            layoutsSelectedForPlot.Clear();

            for (int i = 0; i < ChkLstBxSelectLayouts.CheckedItems.Count; i++)
            {
                layoutsSelectedForPlot.Add(
                    layouts.Where(x=> x.LayoutName == 
                    ChkLstBxSelectLayouts.CheckedItems[i].ToString()).FirstOrDefault()
                );
            }

            if (this.ParentForm.ParentForm != null)
            {
                this.ParentForm.ParentForm.Hide();
            }
            else
            {
                this.ParentForm.Hide();
            }

            Cls_BW_Plot_Pdf_Dwg cls = new Cls_BW_Plot_Pdf_Dwg();

            cls.PlotPdf(
                "yMMdd", //3m
                layoutsSelectedForPlot, // if this is empty it will use the selection checked box
                "DWG To PDF.pc3",
                "ARCH_full_bleed_D_(24.00_x_36.00_Inches)",
                0.3, 0.3,
                ChkBxPlotLineWeights.Checked,

                cBxTitleBlockChoices.Text,
                chkBxAllLayoutsSelectedLayouts.Checked               
                );

            if(LayoutManager.Current.CurrentLayout != curLay)
            {
                LayoutManager.Current.CurrentLayout = curLay;
            }

            if (this.ParentForm.ParentForm != null)
            {
                this.ParentForm.ParentForm.Show();
            }
            else
            {
                this.ParentForm.Show();
            }

        }



        private void BtnPlotMultDwgsToOnePdf_Click(object sender, EventArgs e)
        {
            rtBxDwgsToPlotToPdf.Lines = Cls_BW_Utility.ReturnFilesList();

            List<string> dwgs = rtBxDwgsToPlotToPdf.Lines.ToList();

            if (dwgs.Count > 0)
            {
                this.ParentForm.ParentForm.Hide();

                Cls_BW_Plot_Pdf_Dwgs cls = new Cls_BW_Plot_Pdf_Dwgs();
                cls.pltCls = new Cls_BW_Plot_Pdf_Dwg();                
                cls.PlotPdf("DWG To PDF.pc3", "ARCH_full_bleed_D_(24.00_x_36.00_Inches)", 0.3, 0.3, dwgs, cBxTitleBlockChoices.Text);

                this.ParentForm.ParentForm.Show();
            }
            else
            {
                MessageBox.Show("No Drawings Selected!");
            }
        }




        private void cBxTitleBlockChoices_TextUpdate(object sender, EventArgs e)
        {
            SetText = cBxTitleBlockChoices.Text;
        }


    }
}

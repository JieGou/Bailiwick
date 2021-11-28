namespace MyFirstProject.BW
{
    partial class Uc_BW_TP_Layouts
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtBxDwgsToPlotToPdf = new System.Windows.Forms.RichTextBox();
            this.btnPlotMultDwgsToOnePdf = new System.Windows.Forms.Button();
            this.btnPlotPdf = new System.Windows.Forms.Button();
            this.cBxTitleBlockChoices = new System.Windows.Forms.ComboBox();
            this.btnChangeTitleBlockPhase = new System.Windows.Forms.Button();
            this.chkBxAllLayoutsSelectedLayouts = new System.Windows.Forms.CheckBox();
            this.ChkLstBxSelectLayouts = new System.Windows.Forms.CheckedListBox();
            this.BtnGetLayouts = new System.Windows.Forms.Button();
            this.BtnSelectAll = new System.Windows.Forms.Button();
            this.BtnClearLayoutList = new System.Windows.Forms.Button();
            this.ChkBxPlotLineWeights = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // rtBxDwgsToPlotToPdf
            // 
            this.rtBxDwgsToPlotToPdf.Location = new System.Drawing.Point(83, 339);
            this.rtBxDwgsToPlotToPdf.Margin = new System.Windows.Forms.Padding(2);
            this.rtBxDwgsToPlotToPdf.Name = "rtBxDwgsToPlotToPdf";
            this.rtBxDwgsToPlotToPdf.Size = new System.Drawing.Size(384, 84);
            this.rtBxDwgsToPlotToPdf.TabIndex = 10;
            this.rtBxDwgsToPlotToPdf.Text = "";
            // 
            // btnPlotMultDwgsToOnePdf
            // 
            this.btnPlotMultDwgsToOnePdf.Location = new System.Drawing.Point(15, 339);
            this.btnPlotMultDwgsToOnePdf.Margin = new System.Windows.Forms.Padding(2);
            this.btnPlotMultDwgsToOnePdf.Name = "btnPlotMultDwgsToOnePdf";
            this.btnPlotMultDwgsToOnePdf.Size = new System.Drawing.Size(63, 83);
            this.btnPlotMultDwgsToOnePdf.TabIndex = 9;
            this.btnPlotMultDwgsToOnePdf.Text = "Make DSD Multiple Dwgs To One Pdf";
            this.btnPlotMultDwgsToOnePdf.UseVisualStyleBackColor = true;
            this.btnPlotMultDwgsToOnePdf.Click += new System.EventHandler(this.BtnPlotMultDwgsToOnePdf_Click);
            // 
            // btnPlotPdf
            // 
            this.btnPlotPdf.Location = new System.Drawing.Point(186, 55);
            this.btnPlotPdf.Margin = new System.Windows.Forms.Padding(2);
            this.btnPlotPdf.Name = "btnPlotPdf";
            this.btnPlotPdf.Size = new System.Drawing.Size(124, 34);
            this.btnPlotPdf.TabIndex = 8;
            this.btnPlotPdf.Text = "Plot Current Dwg To PDF";
            this.btnPlotPdf.UseVisualStyleBackColor = true;
            this.btnPlotPdf.Click += new System.EventHandler(this.BtnPlotPdf_Click);
            // 
            // cBxTitleBlockChoices
            // 
            this.cBxTitleBlockChoices.FormattingEnabled = true;
            this.cBxTitleBlockChoices.Items.AddRange(new object[] {
            "__ Survey",
            "__ Design",
            "__ Bid",
            "__ Install",
            "__ Final"});
            this.cBxTitleBlockChoices.Location = new System.Drawing.Point(2, 18);
            this.cBxTitleBlockChoices.Margin = new System.Windows.Forms.Padding(2);
            this.cBxTitleBlockChoices.Name = "cBxTitleBlockChoices";
            this.cBxTitleBlockChoices.Size = new System.Drawing.Size(154, 21);
            this.cBxTitleBlockChoices.TabIndex = 7;
            this.cBxTitleBlockChoices.Text = "Select Title Block To Insert";
            this.cBxTitleBlockChoices.TextUpdate += new System.EventHandler(this.cBxTitleBlockChoices_TextUpdate);
            // 
            // btnChangeTitleBlockPhase
            // 
            this.btnChangeTitleBlockPhase.Location = new System.Drawing.Point(2, 55);
            this.btnChangeTitleBlockPhase.Margin = new System.Windows.Forms.Padding(2);
            this.btnChangeTitleBlockPhase.Name = "btnChangeTitleBlockPhase";
            this.btnChangeTitleBlockPhase.Size = new System.Drawing.Size(153, 34);
            this.btnChangeTitleBlockPhase.TabIndex = 6;
            this.btnChangeTitleBlockPhase.Text = "Change Title Block Phase";
            this.btnChangeTitleBlockPhase.UseVisualStyleBackColor = true;
            this.btnChangeTitleBlockPhase.Click += new System.EventHandler(this.BtnChangeTitleBlockPhase_Click);
            // 
            // chkBxAllLayoutsSelectedLayouts
            // 
            this.chkBxAllLayoutsSelectedLayouts.AutoSize = true;
            this.chkBxAllLayoutsSelectedLayouts.Checked = true;
            this.chkBxAllLayoutsSelectedLayouts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxAllLayoutsSelectedLayouts.Location = new System.Drawing.Point(186, 20);
            this.chkBxAllLayoutsSelectedLayouts.Margin = new System.Windows.Forms.Padding(2);
            this.chkBxAllLayoutsSelectedLayouts.Name = "chkBxAllLayoutsSelectedLayouts";
            this.chkBxAllLayoutsSelectedLayouts.Size = new System.Drawing.Size(170, 17);
            this.chkBxAllLayoutsSelectedLayouts.TabIndex = 11;
            this.chkBxAllLayoutsSelectedLayouts.Text = "All Layouts / Selected Layouts";
            this.chkBxAllLayoutsSelectedLayouts.UseVisualStyleBackColor = true;
            // 
            // ChkLstBxSelectLayouts
            // 
            this.ChkLstBxSelectLayouts.CheckOnClick = true;
            this.ChkLstBxSelectLayouts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkLstBxSelectLayouts.FormattingEnabled = true;
            this.ChkLstBxSelectLayouts.Location = new System.Drawing.Point(489, 3);
            this.ChkLstBxSelectLayouts.Name = "ChkLstBxSelectLayouts";
            this.ChkLstBxSelectLayouts.Size = new System.Drawing.Size(202, 327);
            this.ChkLstBxSelectLayouts.TabIndex = 12;
            // 
            // BtnGetLayouts
            // 
            this.BtnGetLayouts.Location = new System.Drawing.Point(408, 3);
            this.BtnGetLayouts.Name = "BtnGetLayouts";
            this.BtnGetLayouts.Size = new System.Drawing.Size(75, 34);
            this.BtnGetLayouts.TabIndex = 13;
            this.BtnGetLayouts.Text = "Get Layouts >>>";
            this.BtnGetLayouts.UseVisualStyleBackColor = true;
            this.BtnGetLayouts.Click += new System.EventHandler(this.BtnGetLayouts_Click);
            // 
            // BtnSelectAll
            // 
            this.BtnSelectAll.Location = new System.Drawing.Point(408, 43);
            this.BtnSelectAll.Name = "BtnSelectAll";
            this.BtnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.BtnSelectAll.TabIndex = 14;
            this.BtnSelectAll.Text = "Select All";
            this.BtnSelectAll.UseVisualStyleBackColor = true;
            this.BtnSelectAll.Click += new System.EventHandler(this.BtnSelectAll_Click);
            // 
            // BtnClearLayoutList
            // 
            this.BtnClearLayoutList.Location = new System.Drawing.Point(408, 72);
            this.BtnClearLayoutList.Name = "BtnClearLayoutList";
            this.BtnClearLayoutList.Size = new System.Drawing.Size(75, 23);
            this.BtnClearLayoutList.TabIndex = 15;
            this.BtnClearLayoutList.Text = "Clear";
            this.BtnClearLayoutList.UseVisualStyleBackColor = true;
            this.BtnClearLayoutList.Click += new System.EventHandler(this.BtnClearLayoutList_Click);
            // 
            // ChkBxPlotLineWeights
            // 
            this.ChkBxPlotLineWeights.AutoSize = true;
            this.ChkBxPlotLineWeights.Location = new System.Drawing.Point(186, 110);
            this.ChkBxPlotLineWeights.Name = "ChkBxPlotLineWeights";
            this.ChkBxPlotLineWeights.Size = new System.Drawing.Size(109, 17);
            this.ChkBxPlotLineWeights.TabIndex = 16;
            this.ChkBxPlotLineWeights.Text = "Plot Line Weights";
            this.ChkBxPlotLineWeights.UseVisualStyleBackColor = true;
            // 
            // Uc_BW_TP_Layouts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ChkBxPlotLineWeights);
            this.Controls.Add(this.BtnClearLayoutList);
            this.Controls.Add(this.BtnSelectAll);
            this.Controls.Add(this.BtnGetLayouts);
            this.Controls.Add(this.ChkLstBxSelectLayouts);
            this.Controls.Add(this.chkBxAllLayoutsSelectedLayouts);
            this.Controls.Add(this.rtBxDwgsToPlotToPdf);
            this.Controls.Add(this.btnPlotMultDwgsToOnePdf);
            this.Controls.Add(this.btnPlotPdf);
            this.Controls.Add(this.cBxTitleBlockChoices);
            this.Controls.Add(this.btnChangeTitleBlockPhase);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Uc_BW_TP_Layouts";
            this.Size = new System.Drawing.Size(694, 436);
            this.SizeChanged += new System.EventHandler(this.Uc_BW_TP_Layouts_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtBxDwgsToPlotToPdf;
        private System.Windows.Forms.Button btnPlotMultDwgsToOnePdf;
        private System.Windows.Forms.Button btnPlotPdf;
        private System.Windows.Forms.ComboBox cBxTitleBlockChoices;
        private System.Windows.Forms.Button btnChangeTitleBlockPhase;
        private System.Windows.Forms.CheckBox chkBxAllLayoutsSelectedLayouts;
        private System.Windows.Forms.CheckedListBox ChkLstBxSelectLayouts;
        private System.Windows.Forms.Button BtnGetLayouts;
        private System.Windows.Forms.Button BtnSelectAll;
        private System.Windows.Forms.Button BtnClearLayoutList;
        private System.Windows.Forms.CheckBox ChkBxPlotLineWeights;
    }
}

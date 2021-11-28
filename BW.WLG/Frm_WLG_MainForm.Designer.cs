namespace MyFirstProject.BW.WLG
{
    partial class Frm_WLG_MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnSelectDevices = new System.Windows.Forms.Button();
            this.BtnWrkStaDevSum = new System.Windows.Forms.Button();
            this.BtnXlPullList = new System.Windows.Forms.Button();
            this.TCtlMain = new System.Windows.Forms.TabControl();
            this.TpAllDevices = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DgvAllDevices = new System.Windows.Forms.DataGridView();
            this.DgvLstRun = new System.Windows.Forms.DataGridView();
            this.TpHardware = new System.Windows.Forms.TabPage();
            this.DgvHardware = new System.Windows.Forms.DataGridView();
            this.TpLayouts = new System.Windows.Forms.TabPage();
            this.BtnAutoLabelByColor = new System.Windows.Forms.Button();
            this.BtnClearGrids = new System.Windows.Forms.Button();
            this.BtnReadRunList = new System.Windows.Forms.Button();
            this.BtnFillAtts = new System.Windows.Forms.Button();
            this.BtnMergeLists = new System.Windows.Forms.Button();
            this.BtnPickOriginalBlock = new System.Windows.Forms.Button();
            this.ChkBxUpdateDataDrop = new System.Windows.Forms.CheckBox();
            this.uc_BW_TP_Layouts1 = new MyFirstProject.BW.Uc_BW_TP_Layouts();
            this.BtnCameraCounts = new System.Windows.Forms.Button();
            this.TCtlMain.SuspendLayout();
            this.TpAllDevices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvAllDevices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvLstRun)).BeginInit();
            this.TpHardware.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvHardware)).BeginInit();
            this.TpLayouts.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnSelectDevices
            // 
            this.BtnSelectDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSelectDevices.Location = new System.Drawing.Point(954, 101);
            this.BtnSelectDevices.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSelectDevices.Name = "BtnSelectDevices";
            this.BtnSelectDevices.Size = new System.Drawing.Size(75, 39);
            this.BtnSelectDevices.TabIndex = 0;
            this.BtnSelectDevices.Text = "Select Devices";
            this.BtnSelectDevices.UseVisualStyleBackColor = true;
            this.BtnSelectDevices.Click += new System.EventHandler(this.BtnSelectDevices_Click);
            // 
            // BtnWrkStaDevSum
            // 
            this.BtnWrkStaDevSum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnWrkStaDevSum.Location = new System.Drawing.Point(950, 422);
            this.BtnWrkStaDevSum.Name = "BtnWrkStaDevSum";
            this.BtnWrkStaDevSum.Size = new System.Drawing.Size(79, 23);
            this.BtnWrkStaDevSum.TabIndex = 2;
            this.BtnWrkStaDevSum.Text = "Tbl Dev Sum";
            this.BtnWrkStaDevSum.UseVisualStyleBackColor = true;
            this.BtnWrkStaDevSum.Click += new System.EventHandler(this.BtnWrkStaDevSum_Click);
            // 
            // BtnXlPullList
            // 
            this.BtnXlPullList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnXlPullList.Location = new System.Drawing.Point(954, 174);
            this.BtnXlPullList.Name = "BtnXlPullList";
            this.BtnXlPullList.Size = new System.Drawing.Size(75, 23);
            this.BtnXlPullList.TabIndex = 3;
            this.BtnXlPullList.Text = "Xl Pull List";
            this.BtnXlPullList.UseVisualStyleBackColor = true;
            this.BtnXlPullList.Click += new System.EventHandler(this.BtnXlPullList_Click);
            // 
            // TCtlMain
            // 
            this.TCtlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TCtlMain.Controls.Add(this.TpAllDevices);
            this.TCtlMain.Controls.Add(this.TpHardware);
            this.TCtlMain.Controls.Add(this.TpLayouts);
            this.TCtlMain.Location = new System.Drawing.Point(4, 2);
            this.TCtlMain.Name = "TCtlMain";
            this.TCtlMain.SelectedIndex = 0;
            this.TCtlMain.Size = new System.Drawing.Size(944, 531);
            this.TCtlMain.TabIndex = 4;
            // 
            // TpAllDevices
            // 
            this.TpAllDevices.Controls.Add(this.splitContainer1);
            this.TpAllDevices.Location = new System.Drawing.Point(4, 22);
            this.TpAllDevices.Name = "TpAllDevices";
            this.TpAllDevices.Size = new System.Drawing.Size(936, 505);
            this.TpAllDevices.TabIndex = 3;
            this.TpAllDevices.Text = "All Devices";
            this.TpAllDevices.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DgvAllDevices);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.DgvLstRun);
            this.splitContainer1.Size = new System.Drawing.Size(936, 505);
            this.splitContainer1.SplitterDistance = 506;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 10;
            // 
            // DgvAllDevices
            // 
            this.DgvAllDevices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvAllDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvAllDevices.Location = new System.Drawing.Point(0, 0);
            this.DgvAllDevices.Margin = new System.Windows.Forms.Padding(2);
            this.DgvAllDevices.Name = "DgvAllDevices";
            this.DgvAllDevices.RowTemplate.Height = 24;
            this.DgvAllDevices.Size = new System.Drawing.Size(506, 505);
            this.DgvAllDevices.TabIndex = 3;
            // 
            // DgvLstRun
            // 
            this.DgvLstRun.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvLstRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvLstRun.Location = new System.Drawing.Point(0, 0);
            this.DgvLstRun.Name = "DgvLstRun";
            this.DgvLstRun.Size = new System.Drawing.Size(422, 505);
            this.DgvLstRun.TabIndex = 9;
            // 
            // TpHardware
            // 
            this.TpHardware.Controls.Add(this.DgvHardware);
            this.TpHardware.Location = new System.Drawing.Point(4, 22);
            this.TpHardware.Name = "TpHardware";
            this.TpHardware.Padding = new System.Windows.Forms.Padding(3);
            this.TpHardware.Size = new System.Drawing.Size(936, 505);
            this.TpHardware.TabIndex = 4;
            this.TpHardware.Text = "Wardware";
            this.TpHardware.UseVisualStyleBackColor = true;
            // 
            // DgvHardware
            // 
            this.DgvHardware.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvHardware.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvHardware.Location = new System.Drawing.Point(3, 3);
            this.DgvHardware.Margin = new System.Windows.Forms.Padding(2);
            this.DgvHardware.Name = "DgvHardware";
            this.DgvHardware.RowTemplate.Height = 24;
            this.DgvHardware.Size = new System.Drawing.Size(930, 499);
            this.DgvHardware.TabIndex = 4;
            // 
            // TpLayouts
            // 
            this.TpLayouts.Controls.Add(this.uc_BW_TP_Layouts1);
            this.TpLayouts.Location = new System.Drawing.Point(4, 22);
            this.TpLayouts.Name = "TpLayouts";
            this.TpLayouts.Size = new System.Drawing.Size(936, 505);
            this.TpLayouts.TabIndex = 5;
            this.TpLayouts.Text = "Layouts";
            this.TpLayouts.UseVisualStyleBackColor = true;
            // 
            // BtnAutoLabelByColor
            // 
            this.BtnAutoLabelByColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAutoLabelByColor.Enabled = false;
            this.BtnAutoLabelByColor.Location = new System.Drawing.Point(954, 363);
            this.BtnAutoLabelByColor.Name = "BtnAutoLabelByColor";
            this.BtnAutoLabelByColor.Size = new System.Drawing.Size(75, 40);
            this.BtnAutoLabelByColor.TabIndex = 4;
            this.BtnAutoLabelByColor.Text = "Auto Label By Color";
            this.BtnAutoLabelByColor.UseVisualStyleBackColor = true;
            this.BtnAutoLabelByColor.Click += new System.EventHandler(this.BtnAutoLabelByColor_Click);
            // 
            // BtnClearGrids
            // 
            this.BtnClearGrids.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClearGrids.Location = new System.Drawing.Point(954, 24);
            this.BtnClearGrids.Name = "BtnClearGrids";
            this.BtnClearGrids.Size = new System.Drawing.Size(75, 23);
            this.BtnClearGrids.TabIndex = 5;
            this.BtnClearGrids.Text = "Clear Grids";
            this.BtnClearGrids.UseVisualStyleBackColor = true;
            this.BtnClearGrids.Click += new System.EventHandler(this.BtnClearGrids_Click);
            // 
            // BtnReadRunList
            // 
            this.BtnReadRunList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnReadRunList.Location = new System.Drawing.Point(954, 53);
            this.BtnReadRunList.Name = "BtnReadRunList";
            this.BtnReadRunList.Size = new System.Drawing.Size(75, 43);
            this.BtnReadRunList.TabIndex = 6;
            this.BtnReadRunList.Text = "Read Run List";
            this.BtnReadRunList.UseVisualStyleBackColor = true;
            this.BtnReadRunList.Click += new System.EventHandler(this.BtnReadRunList_Click);
            // 
            // BtnFillAtts
            // 
            this.BtnFillAtts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnFillAtts.Location = new System.Drawing.Point(954, 252);
            this.BtnFillAtts.Name = "BtnFillAtts";
            this.BtnFillAtts.Size = new System.Drawing.Size(75, 43);
            this.BtnFillAtts.TabIndex = 7;
            this.BtnFillAtts.Text = "Fill Atts";
            this.BtnFillAtts.UseVisualStyleBackColor = true;
            this.BtnFillAtts.Click += new System.EventHandler(this.BtnFillAtts_Click);
            // 
            // BtnMergeLists
            // 
            this.BtnMergeLists.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMergeLists.Location = new System.Drawing.Point(954, 145);
            this.BtnMergeLists.Name = "BtnMergeLists";
            this.BtnMergeLists.Size = new System.Drawing.Size(75, 23);
            this.BtnMergeLists.TabIndex = 8;
            this.BtnMergeLists.Text = "Merge";
            this.BtnMergeLists.UseVisualStyleBackColor = true;
            this.BtnMergeLists.Click += new System.EventHandler(this.BtnMergeLists_Click);
            // 
            // BtnPickOriginalBlock
            // 
            this.BtnPickOriginalBlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPickOriginalBlock.Location = new System.Drawing.Point(954, 301);
            this.BtnPickOriginalBlock.Name = "BtnPickOriginalBlock";
            this.BtnPickOriginalBlock.Size = new System.Drawing.Size(75, 47);
            this.BtnPickOriginalBlock.TabIndex = 9;
            this.BtnPickOriginalBlock.Text = "Pick Original Block";
            this.BtnPickOriginalBlock.UseVisualStyleBackColor = true;
            this.BtnPickOriginalBlock.Click += new System.EventHandler(this.BtnPickOriginalBlock_Click);
            // 
            // ChkBxUpdateDataDrop
            // 
            this.ChkBxUpdateDataDrop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChkBxUpdateDataDrop.AutoSize = true;
            this.ChkBxUpdateDataDrop.Checked = true;
            this.ChkBxUpdateDataDrop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkBxUpdateDataDrop.Location = new System.Drawing.Point(954, 220);
            this.ChkBxUpdateDataDrop.Name = "ChkBxUpdateDataDrop";
            this.ChkBxUpdateDataDrop.Size = new System.Drawing.Size(75, 17);
            this.ChkBxUpdateDataDrop.TabIndex = 10;
            this.ChkBxUpdateDataDrop.Text = "Data Drop";
            this.ChkBxUpdateDataDrop.UseVisualStyleBackColor = true;
            // 
            // uc_BW_TP_Layouts1
            // 
            this.uc_BW_TP_Layouts1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uc_BW_TP_Layouts1.Location = new System.Drawing.Point(3, 2);
            this.uc_BW_TP_Layouts1.Margin = new System.Windows.Forms.Padding(2);
            this.uc_BW_TP_Layouts1.Name = "uc_BW_TP_Layouts1";
            this.uc_BW_TP_Layouts1.SetText = "Select Title Block To Insert";
            this.uc_BW_TP_Layouts1.Size = new System.Drawing.Size(931, 498);
            this.uc_BW_TP_Layouts1.TabIndex = 0;
            // 
            // BtnCameraCounts
            // 
            this.BtnCameraCounts.Location = new System.Drawing.Point(950, 481);
            this.BtnCameraCounts.Name = "BtnCameraCounts";
            this.BtnCameraCounts.Size = new System.Drawing.Size(79, 43);
            this.BtnCameraCounts.TabIndex = 11;
            this.BtnCameraCounts.Text = "Camera Counts";
            this.BtnCameraCounts.UseVisualStyleBackColor = true;
            this.BtnCameraCounts.Click += new System.EventHandler(this.BtnCameraCounts_Click);
            // 
            // Frm_WLG_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 536);
            this.Controls.Add(this.BtnCameraCounts);
            this.Controls.Add(this.ChkBxUpdateDataDrop);
            this.Controls.Add(this.BtnPickOriginalBlock);
            this.Controls.Add(this.BtnMergeLists);
            this.Controls.Add(this.BtnFillAtts);
            this.Controls.Add(this.BtnReadRunList);
            this.Controls.Add(this.BtnClearGrids);
            this.Controls.Add(this.BtnAutoLabelByColor);
            this.Controls.Add(this.BtnWrkStaDevSum);
            this.Controls.Add(this.BtnSelectDevices);
            this.Controls.Add(this.BtnXlPullList);
            this.Controls.Add(this.TCtlMain);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Frm_WLG_MainForm";
            this.Text = "Frm_WLG_MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_WLG_MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Frm_WLG_MainForm_Load);
            this.TCtlMain.ResumeLayout(false);
            this.TpAllDevices.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvAllDevices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvLstRun)).EndInit();
            this.TpHardware.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvHardware)).EndInit();
            this.TpLayouts.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnSelectDevices;
        private System.Windows.Forms.Button BtnWrkStaDevSum;
        private System.Windows.Forms.Button BtnXlPullList;
        private System.Windows.Forms.TabControl TCtlMain;
        private System.Windows.Forms.Button BtnAutoLabelByColor;
        private System.Windows.Forms.TabPage TpAllDevices;
        private System.Windows.Forms.DataGridView DgvAllDevices;
        private System.Windows.Forms.Button BtnClearGrids;
        private System.Windows.Forms.Button BtnReadRunList;
        private System.Windows.Forms.Button BtnFillAtts;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView DgvLstRun;
        private System.Windows.Forms.Button BtnMergeLists;
        private System.Windows.Forms.TabPage TpHardware;
        private System.Windows.Forms.DataGridView DgvHardware;
        private System.Windows.Forms.TabPage TpLayouts;
        private Uc_BW_TP_Layouts uc_BW_TP_Layouts1;
        private System.Windows.Forms.Button BtnPickOriginalBlock;
        private System.Windows.Forms.CheckBox ChkBxUpdateDataDrop;
        private System.Windows.Forms.Button BtnCameraCounts;
    }
}
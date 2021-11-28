namespace MyFirstProject.BW
{
    partial class Uc_BW_TP_AirMAGs
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
            this.btnInsertAP = new System.Windows.Forms.Button();
            this.btnAirMagRptReadXmlFile = new System.Windows.Forms.Button();
            this.spltCtnrAirMags = new System.Windows.Forms.SplitContainer();
            this.dgvAirMagLbls = new System.Windows.Forms.DataGridView();
            this.chkBxAPsAutoInsByAirMagTxt = new System.Windows.Forms.CheckBox();
            this.chkBxMoveAirMagLables = new System.Windows.Forms.CheckBox();
            this.btnTstXML = new System.Windows.Forms.Button();
            this.btnAirMagLblsClear = new System.Windows.Forms.Button();
            this.dgvAirMagXmlFile = new System.Windows.Forms.DataGridView();
            this.btnAirMagLblsZoomTo = new System.Windows.Forms.Button();
            this.btnAirMagLblsGetForAP_Insert = new System.Windows.Forms.Button();
            this.grpBxAirMagExpToDwg = new System.Windows.Forms.GroupBox();
            this.btnAirMagLbls = new System.Windows.Forms.Button();
            this.btnAirMagLblsImport = new System.Windows.Forms.Button();
            this.dgvAirMagXmlFileRepInfo = new System.Windows.Forms.DataGridView();
            this.uc_BW_SiteInfo1 = new MyFirstProject.BW.Uc_BW_SiteInfo();
            ((System.ComponentModel.ISupportInitialize)(this.spltCtnrAirMags)).BeginInit();
            this.spltCtnrAirMags.Panel1.SuspendLayout();
            this.spltCtnrAirMags.Panel2.SuspendLayout();
            this.spltCtnrAirMags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAirMagLbls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAirMagXmlFile)).BeginInit();
            this.grpBxAirMagExpToDwg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAirMagXmlFileRepInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInsertAP
            // 
            this.btnInsertAP.Location = new System.Drawing.Point(7, 451);
            this.btnInsertAP.Margin = new System.Windows.Forms.Padding(2);
            this.btnInsertAP.Name = "btnInsertAP";
            this.btnInsertAP.Size = new System.Drawing.Size(87, 25);
            this.btnInsertAP.TabIndex = 13;
            this.btnInsertAP.Text = "Insert AP";
            this.btnInsertAP.UseVisualStyleBackColor = true;
            this.btnInsertAP.Click += new System.EventHandler(this.BtnInsertAP_Click);
            // 
            // btnAirMagRptReadXmlFile
            // 
            this.btnAirMagRptReadXmlFile.Location = new System.Drawing.Point(7, 419);
            this.btnAirMagRptReadXmlFile.Margin = new System.Windows.Forms.Padding(2);
            this.btnAirMagRptReadXmlFile.Name = "btnAirMagRptReadXmlFile";
            this.btnAirMagRptReadXmlFile.Size = new System.Drawing.Size(87, 26);
            this.btnAirMagRptReadXmlFile.TabIndex = 6;
            this.btnAirMagRptReadXmlFile.Text = "Select File";
            this.btnAirMagRptReadXmlFile.UseVisualStyleBackColor = true;
            this.btnAirMagRptReadXmlFile.Click += new System.EventHandler(this.BtnAirMagRptReadXmlFile_Click);
            // 
            // spltCtnrAirMags
            // 
            this.spltCtnrAirMags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spltCtnrAirMags.Location = new System.Drawing.Point(0, 74);
            this.spltCtnrAirMags.Margin = new System.Windows.Forms.Padding(2);
            this.spltCtnrAirMags.Name = "spltCtnrAirMags";
            // 
            // spltCtnrAirMags.Panel1
            // 
            this.spltCtnrAirMags.Panel1.Controls.Add(this.dgvAirMagLbls);
            // 
            // spltCtnrAirMags.Panel2
            // 
            this.spltCtnrAirMags.Panel2.Controls.Add(this.chkBxAPsAutoInsByAirMagTxt);
            this.spltCtnrAirMags.Panel2.Controls.Add(this.btnInsertAP);
            this.spltCtnrAirMags.Panel2.Controls.Add(this.uc_BW_SiteInfo1);
            this.spltCtnrAirMags.Panel2.Controls.Add(this.btnAirMagRptReadXmlFile);
            this.spltCtnrAirMags.Panel2.Controls.Add(this.chkBxMoveAirMagLables);
            this.spltCtnrAirMags.Panel2.Controls.Add(this.btnTstXML);
            this.spltCtnrAirMags.Panel2.Controls.Add(this.btnAirMagLblsClear);
            this.spltCtnrAirMags.Panel2.Controls.Add(this.dgvAirMagXmlFile);
            this.spltCtnrAirMags.Panel2.Controls.Add(this.btnAirMagLblsZoomTo);
            this.spltCtnrAirMags.Panel2.Controls.Add(this.btnAirMagLblsGetForAP_Insert);
            this.spltCtnrAirMags.Panel2.Controls.Add(this.grpBxAirMagExpToDwg);
            this.spltCtnrAirMags.Size = new System.Drawing.Size(840, 556);
            this.spltCtnrAirMags.SplitterDistance = 216;
            this.spltCtnrAirMags.SplitterWidth = 3;
            this.spltCtnrAirMags.TabIndex = 0;
            // 
            // dgvAirMagLbls
            // 
            this.dgvAirMagLbls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAirMagLbls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAirMagLbls.Location = new System.Drawing.Point(0, 0);
            this.dgvAirMagLbls.Margin = new System.Windows.Forms.Padding(2);
            this.dgvAirMagLbls.MultiSelect = false;
            this.dgvAirMagLbls.Name = "dgvAirMagLbls";
            this.dgvAirMagLbls.ReadOnly = true;
            this.dgvAirMagLbls.RowTemplate.Height = 24;
            this.dgvAirMagLbls.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAirMagLbls.Size = new System.Drawing.Size(216, 556);
            this.dgvAirMagLbls.TabIndex = 0;
            // 
            // chkBxAPsAutoInsByAirMagTxt
            // 
            this.chkBxAPsAutoInsByAirMagTxt.AutoSize = true;
            this.chkBxAPsAutoInsByAirMagTxt.Checked = true;
            this.chkBxAPsAutoInsByAirMagTxt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxAPsAutoInsByAirMagTxt.Location = new System.Drawing.Point(7, 384);
            this.chkBxAPsAutoInsByAirMagTxt.Margin = new System.Windows.Forms.Padding(2);
            this.chkBxAPsAutoInsByAirMagTxt.Name = "chkBxAPsAutoInsByAirMagTxt";
            this.chkBxAPsAutoInsByAirMagTxt.Size = new System.Drawing.Size(95, 30);
            this.chkBxAPsAutoInsByAirMagTxt.TabIndex = 17;
            this.chkBxAPsAutoInsByAirMagTxt.Text = "APs Auto Ins\r\nBy Air Mag Txt";
            this.chkBxAPsAutoInsByAirMagTxt.UseVisualStyleBackColor = true;
            // 
            // chkBxMoveAirMagLables
            // 
            this.chkBxMoveAirMagLables.AutoSize = true;
            this.chkBxMoveAirMagLables.Checked = true;
            this.chkBxMoveAirMagLables.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxMoveAirMagLables.Location = new System.Drawing.Point(7, 121);
            this.chkBxMoveAirMagLables.Margin = new System.Windows.Forms.Padding(2);
            this.chkBxMoveAirMagLables.Name = "chkBxMoveAirMagLables";
            this.chkBxMoveAirMagLables.Size = new System.Drawing.Size(126, 17);
            this.chkBxMoveAirMagLables.TabIndex = 18;
            this.chkBxMoveAirMagLables.Text = "Move Air Mag Lables";
            this.chkBxMoveAirMagLables.UseVisualStyleBackColor = true;
            // 
            // btnTstXML
            // 
            this.btnTstXML.Enabled = false;
            this.btnTstXML.ForeColor = System.Drawing.Color.Blue;
            this.btnTstXML.Location = new System.Drawing.Point(104, 143);
            this.btnTstXML.Margin = new System.Windows.Forms.Padding(2);
            this.btnTstXML.Name = "btnTstXML";
            this.btnTstXML.Size = new System.Drawing.Size(28, 141);
            this.btnTstXML.TabIndex = 17;
            this.btnTstXML.Text = "T\r\ns\r\nt\r\nX\r\nM\r\nL";
            this.btnTstXML.UseVisualStyleBackColor = true;
            this.btnTstXML.Visible = false;
            this.btnTstXML.Click += new System.EventHandler(this.BtnTstXML_Click);
            // 
            // btnAirMagLblsClear
            // 
            this.btnAirMagLblsClear.Location = new System.Drawing.Point(7, 221);
            this.btnAirMagLblsClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnAirMagLblsClear.Name = "btnAirMagLblsClear";
            this.btnAirMagLblsClear.Size = new System.Drawing.Size(121, 43);
            this.btnAirMagLblsClear.TabIndex = 16;
            this.btnAirMagLblsClear.Text = "Air Mag Lbls Clear";
            this.btnAirMagLblsClear.UseVisualStyleBackColor = true;
            this.btnAirMagLblsClear.Click += new System.EventHandler(this.BtnAirMagLblsClear_Click);
            // 
            // dgvAirMagXmlFile
            // 
            this.dgvAirMagXmlFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAirMagXmlFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAirMagXmlFile.Location = new System.Drawing.Point(135, 0);
            this.dgvAirMagXmlFile.Margin = new System.Windows.Forms.Padding(2);
            this.dgvAirMagXmlFile.MultiSelect = false;
            this.dgvAirMagXmlFile.Name = "dgvAirMagXmlFile";
            this.dgvAirMagXmlFile.ReadOnly = true;
            this.dgvAirMagXmlFile.RowTemplate.Height = 24;
            this.dgvAirMagXmlFile.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAirMagXmlFile.Size = new System.Drawing.Size(484, 556);
            this.dgvAirMagXmlFile.TabIndex = 12;
            // 
            // btnAirMagLblsZoomTo
            // 
            this.btnAirMagLblsZoomTo.Location = new System.Drawing.Point(7, 187);
            this.btnAirMagLblsZoomTo.Margin = new System.Windows.Forms.Padding(2);
            this.btnAirMagLblsZoomTo.Name = "btnAirMagLblsZoomTo";
            this.btnAirMagLblsZoomTo.Size = new System.Drawing.Size(121, 29);
            this.btnAirMagLblsZoomTo.TabIndex = 5;
            this.btnAirMagLblsZoomTo.Text = "Zoom To Am Lbl";
            this.btnAirMagLblsZoomTo.UseVisualStyleBackColor = true;
            this.btnAirMagLblsZoomTo.Click += new System.EventHandler(this.BtnAirMagLblsZoomTo_Click);
            // 
            // btnAirMagLblsGetForAP_Insert
            // 
            this.btnAirMagLblsGetForAP_Insert.Location = new System.Drawing.Point(7, 143);
            this.btnAirMagLblsGetForAP_Insert.Margin = new System.Windows.Forms.Padding(2);
            this.btnAirMagLblsGetForAP_Insert.Name = "btnAirMagLblsGetForAP_Insert";
            this.btnAirMagLblsGetForAP_Insert.Size = new System.Drawing.Size(121, 39);
            this.btnAirMagLblsGetForAP_Insert.TabIndex = 4;
            this.btnAirMagLblsGetForAP_Insert.Text = "Get Air Mag Lbls For AP Insert";
            this.btnAirMagLblsGetForAP_Insert.UseVisualStyleBackColor = true;
            this.btnAirMagLblsGetForAP_Insert.Click += new System.EventHandler(this.BtnAirMagLblsGetForAP_Insert_Click);
            // 
            // grpBxAirMagExpToDwg
            // 
            this.grpBxAirMagExpToDwg.Controls.Add(this.btnAirMagLbls);
            this.grpBxAirMagExpToDwg.Controls.Add(this.btnAirMagLblsImport);
            this.grpBxAirMagExpToDwg.Location = new System.Drawing.Point(7, 2);
            this.grpBxAirMagExpToDwg.Margin = new System.Windows.Forms.Padding(2);
            this.grpBxAirMagExpToDwg.Name = "grpBxAirMagExpToDwg";
            this.grpBxAirMagExpToDwg.Padding = new System.Windows.Forms.Padding(2);
            this.grpBxAirMagExpToDwg.Size = new System.Drawing.Size(128, 109);
            this.grpBxAirMagExpToDwg.TabIndex = 3;
            this.grpBxAirMagExpToDwg.TabStop = false;
            this.grpBxAirMagExpToDwg.Text = "Air Mag EXP to DWG";
            // 
            // btnAirMagLbls
            // 
            this.btnAirMagLbls.Location = new System.Drawing.Point(4, 17);
            this.btnAirMagLbls.Margin = new System.Windows.Forms.Padding(2);
            this.btnAirMagLbls.Name = "btnAirMagLbls";
            this.btnAirMagLbls.Size = new System.Drawing.Size(56, 87);
            this.btnAirMagLbls.TabIndex = 0;
            this.btnAirMagLbls.Text = "Block Air Mag Labels (from EXP)";
            this.btnAirMagLbls.UseVisualStyleBackColor = true;
            this.btnAirMagLbls.Click += new System.EventHandler(this.BtnAirMagLbls_Click);
            // 
            // btnAirMagLblsImport
            // 
            this.btnAirMagLblsImport.Enabled = false;
            this.btnAirMagLblsImport.Location = new System.Drawing.Point(65, 17);
            this.btnAirMagLblsImport.Margin = new System.Windows.Forms.Padding(2);
            this.btnAirMagLblsImport.Name = "btnAirMagLblsImport";
            this.btnAirMagLblsImport.Size = new System.Drawing.Size(56, 87);
            this.btnAirMagLblsImport.TabIndex = 2;
            this.btnAirMagLblsImport.Text = "Import AMs Block (from EXP)";
            this.btnAirMagLblsImport.UseVisualStyleBackColor = true;
            this.btnAirMagLblsImport.Click += new System.EventHandler(this.BtnAirMagLblsImport_Click);
            // 
            // dgvAirMagXmlFileRepInfo
            // 
            this.dgvAirMagXmlFileRepInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAirMagXmlFileRepInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAirMagXmlFileRepInfo.Location = new System.Drawing.Point(2, 2);
            this.dgvAirMagXmlFileRepInfo.Margin = new System.Windows.Forms.Padding(2);
            this.dgvAirMagXmlFileRepInfo.MultiSelect = false;
            this.dgvAirMagXmlFileRepInfo.Name = "dgvAirMagXmlFileRepInfo";
            this.dgvAirMagXmlFileRepInfo.ReadOnly = true;
            this.dgvAirMagXmlFileRepInfo.RowTemplate.Height = 24;
            this.dgvAirMagXmlFileRepInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAirMagXmlFileRepInfo.Size = new System.Drawing.Size(835, 67);
            this.dgvAirMagXmlFileRepInfo.TabIndex = 16;
            // 
            // uc_BW_SiteInfo1
            // 
            this.uc_BW_SiteInfo1.Location = new System.Drawing.Point(2, 274);
            this.uc_BW_SiteInfo1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.uc_BW_SiteInfo1.Name = "uc_BW_SiteInfo1";
            this.uc_BW_SiteInfo1.Size = new System.Drawing.Size(126, 115);
            this.uc_BW_SiteInfo1.TabIndex = 19;
            // 
            // Uc_BW_TP_AirMAGs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvAirMagXmlFileRepInfo);
            this.Controls.Add(this.spltCtnrAirMags);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Uc_BW_TP_AirMAGs";
            this.Size = new System.Drawing.Size(840, 632);
            this.spltCtnrAirMags.Panel1.ResumeLayout(false);
            this.spltCtnrAirMags.Panel2.ResumeLayout(false);
            this.spltCtnrAirMags.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltCtnrAirMags)).EndInit();
            this.spltCtnrAirMags.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAirMagLbls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAirMagXmlFile)).EndInit();
            this.grpBxAirMagExpToDwg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAirMagXmlFileRepInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInsertAP;
        private System.Windows.Forms.Button btnAirMagRptReadXmlFile;
        private System.Windows.Forms.SplitContainer spltCtnrAirMags;
        private System.Windows.Forms.DataGridView dgvAirMagLbls;
        private System.Windows.Forms.CheckBox chkBxMoveAirMagLables;
        private System.Windows.Forms.Button btnTstXML;
        private System.Windows.Forms.Button btnAirMagLblsClear;
        private System.Windows.Forms.DataGridView dgvAirMagXmlFile;
        private System.Windows.Forms.Button btnAirMagLblsZoomTo;
        private System.Windows.Forms.Button btnAirMagLblsGetForAP_Insert;
        private System.Windows.Forms.GroupBox grpBxAirMagExpToDwg;
        private System.Windows.Forms.Button btnAirMagLbls;
        private System.Windows.Forms.Button btnAirMagLblsImport;
        private System.Windows.Forms.DataGridView dgvAirMagXmlFileRepInfo;
        private System.Windows.Forms.CheckBox chkBxAPsAutoInsByAirMagTxt;
        private Uc_BW_SiteInfo uc_BW_SiteInfo1;
    }
}

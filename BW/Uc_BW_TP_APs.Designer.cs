namespace MyFirstProject.BW
{
    partial class Uc_BW_TP_APs
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
            this.gBxAPs = new System.Windows.Forms.GroupBox();
            this.BtnSelAMsAPs = new System.Windows.Forms.Button();
            this.btnAPsRenumberAllByCloset = new System.Windows.Forms.Button();
            this.btnWapsFindDups = new System.Windows.Forms.Button();
            this.btnLabelAPsIndividually = new System.Windows.Forms.Button();
            this.btnRefreshAps = new System.Windows.Forms.Button();
            this.btnRestoreOriginalAPsLst = new System.Windows.Forms.Button();
            this.btnSelectByViewPort = new System.Windows.Forms.Button();
            this.cBxAPClosetFilter = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkBxUpdateBuilding = new System.Windows.Forms.CheckBox();
            this.chkBxUpdateFloor = new System.Windows.Forms.CheckBox();
            this.chkBxUpdateSite = new System.Windows.Forms.CheckBox();
            this.chkBxCalcLengthsFromCloset = new System.Windows.Forms.CheckBox();
            this.btnAPsSetCloset = new System.Windows.Forms.Button();
            this.txtBxMaxRunLengthInFeet = new System.Windows.Forms.TextBox();
            this.chkBxRunOrientLRTB = new System.Windows.Forms.CheckBox();
            this.txtBxAPsStartNumberForRenumber = new System.Windows.Forms.TextBox();
            this.btnAPsSelectInPolyline = new System.Windows.Forms.Button();
            this.btnAPsRemunber = new System.Windows.Forms.Button();
            this.chkBxWapDual = new System.Windows.Forms.CheckBox();
            this.chkBxWap = new System.Windows.Forms.CheckBox();
            this.btnAPsZoomAndSelect = new System.Windows.Forms.Button();
            this.btnAPsWAOsGet = new System.Windows.Forms.Button();
            this.dgvWaps = new System.Windows.Forms.DataGridView();
            this.dgvWapData2Duals = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.BtnFindMissingNums = new System.Windows.Forms.Button();
            this.uc_BW_SiteInfo1 = new MyFirstProject.BW.Uc_BW_SiteInfo();
            this.gBxAPs.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWaps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWapData2Duals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gBxAPs
            // 
            this.gBxAPs.Controls.Add(this.BtnFindMissingNums);
            this.gBxAPs.Controls.Add(this.BtnSelAMsAPs);
            this.gBxAPs.Controls.Add(this.btnAPsRenumberAllByCloset);
            this.gBxAPs.Controls.Add(this.btnWapsFindDups);
            this.gBxAPs.Controls.Add(this.btnLabelAPsIndividually);
            this.gBxAPs.Controls.Add(this.btnRefreshAps);
            this.gBxAPs.Controls.Add(this.btnRestoreOriginalAPsLst);
            this.gBxAPs.Controls.Add(this.btnSelectByViewPort);
            this.gBxAPs.Controls.Add(this.cBxAPClosetFilter);
            this.gBxAPs.Controls.Add(this.panel1);
            this.gBxAPs.Controls.Add(this.txtBxAPsStartNumberForRenumber);
            this.gBxAPs.Controls.Add(this.btnAPsSelectInPolyline);
            this.gBxAPs.Controls.Add(this.btnAPsRemunber);
            this.gBxAPs.Controls.Add(this.chkBxWapDual);
            this.gBxAPs.Controls.Add(this.chkBxWap);
            this.gBxAPs.Controls.Add(this.btnAPsZoomAndSelect);
            this.gBxAPs.Controls.Add(this.btnAPsWAOsGet);
            this.gBxAPs.Dock = System.Windows.Forms.DockStyle.Right;
            this.gBxAPs.Location = new System.Drawing.Point(933, 0);
            this.gBxAPs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gBxAPs.Name = "gBxAPs";
            this.gBxAPs.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gBxAPs.Size = new System.Drawing.Size(259, 703);
            this.gBxAPs.TabIndex = 6;
            this.gBxAPs.TabStop = false;
            this.gBxAPs.Text = "APs";
            // 
            // BtnSelAMsAPs
            // 
            this.BtnSelAMsAPs.Location = new System.Drawing.Point(179, 338);
            this.BtnSelAMsAPs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnSelAMsAPs.Name = "BtnSelAMsAPs";
            this.BtnSelAMsAPs.Size = new System.Drawing.Size(73, 49);
            this.BtnSelAMsAPs.TabIndex = 27;
            this.BtnSelAMsAPs.Text = "Sel AMs APs";
            this.BtnSelAMsAPs.UseVisualStyleBackColor = true;
            this.BtnSelAMsAPs.Click += new System.EventHandler(this.BtnSelAMsAPs_Click);
            // 
            // btnAPsRenumberAllByCloset
            // 
            this.btnAPsRenumberAllByCloset.Location = new System.Drawing.Point(20, 571);
            this.btnAPsRenumberAllByCloset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAPsRenumberAllByCloset.Name = "btnAPsRenumberAllByCloset";
            this.btnAPsRenumberAllByCloset.Size = new System.Drawing.Size(91, 82);
            this.btnAPsRenumberAllByCloset.TabIndex = 26;
            this.btnAPsRenumberAllByCloset.Text = "APs Renumber All By Closet";
            this.btnAPsRenumberAllByCloset.UseVisualStyleBackColor = true;
            this.btnAPsRenumberAllByCloset.Click += new System.EventHandler(this.BtnAPsRenumberAllByCloset_Click);
            // 
            // btnWapsFindDups
            // 
            this.btnWapsFindDups.Location = new System.Drawing.Point(163, 552);
            this.btnWapsFindDups.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnWapsFindDups.Name = "btnWapsFindDups";
            this.btnWapsFindDups.Size = new System.Drawing.Size(91, 58);
            this.btnWapsFindDups.TabIndex = 25;
            this.btnWapsFindDups.Text = "Waps Find Dups";
            this.btnWapsFindDups.UseVisualStyleBackColor = true;
            this.btnWapsFindDups.Click += new System.EventHandler(this.BtnWapsFindDups_Click);
            // 
            // btnLabelAPsIndividually
            // 
            this.btnLabelAPsIndividually.Location = new System.Drawing.Point(163, 491);
            this.btnLabelAPsIndividually.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLabelAPsIndividually.Name = "btnLabelAPsIndividually";
            this.btnLabelAPsIndividually.Size = new System.Drawing.Size(91, 57);
            this.btnLabelAPsIndividually.TabIndex = 24;
            this.btnLabelAPsIndividually.Text = "Label APs Invdividually";
            this.btnLabelAPsIndividually.UseVisualStyleBackColor = true;
            this.btnLabelAPsIndividually.Click += new System.EventHandler(this.BtnLabelAPsIndividually_Click);
            // 
            // btnRefreshAps
            // 
            this.btnRefreshAps.Location = new System.Drawing.Point(179, 230);
            this.btnRefreshAps.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRefreshAps.Name = "btnRefreshAps";
            this.btnRefreshAps.Size = new System.Drawing.Size(75, 49);
            this.btnRefreshAps.TabIndex = 23;
            this.btnRefreshAps.Text = "Refresh";
            this.btnRefreshAps.UseVisualStyleBackColor = true;
            this.btnRefreshAps.Click += new System.EventHandler(this.BtnRefreshAps_Click);
            // 
            // btnRestoreOriginalAPsLst
            // 
            this.btnRestoreOriginalAPsLst.Location = new System.Drawing.Point(179, 407);
            this.btnRestoreOriginalAPsLst.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRestoreOriginalAPsLst.Name = "btnRestoreOriginalAPsLst";
            this.btnRestoreOriginalAPsLst.Size = new System.Drawing.Size(75, 76);
            this.btnRestoreOriginalAPsLst.TabIndex = 17;
            this.btnRestoreOriginalAPsLst.Text = "Restore Orig APs List";
            this.btnRestoreOriginalAPsLst.UseVisualStyleBackColor = true;
            this.btnRestoreOriginalAPsLst.Click += new System.EventHandler(this.BtnRestoreOriginalAPsLst_Click);
            // 
            // btnSelectByViewPort
            // 
            this.btnSelectByViewPort.Location = new System.Drawing.Point(179, 286);
            this.btnSelectByViewPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelectByViewPort.Name = "btnSelectByViewPort";
            this.btnSelectByViewPort.Size = new System.Drawing.Size(75, 48);
            this.btnSelectByViewPort.TabIndex = 15;
            this.btnSelectByViewPort.Text = "Sel By ViewP";
            this.btnSelectByViewPort.UseVisualStyleBackColor = true;
            this.btnSelectByViewPort.Click += new System.EventHandler(this.BtnSelectByViewPort_Click);
            // 
            // cBxAPClosetFilter
            // 
            this.cBxAPClosetFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBxAPClosetFilter.FormattingEnabled = true;
            this.cBxAPClosetFilter.Location = new System.Drawing.Point(49, 298);
            this.cBxAPClosetFilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cBxAPClosetFilter.Name = "cBxAPClosetFilter";
            this.cBxAPClosetFilter.Size = new System.Drawing.Size(121, 28);
            this.cBxAPClosetFilter.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uc_BW_SiteInfo1);
            this.panel1.Controls.Add(this.chkBxUpdateBuilding);
            this.panel1.Controls.Add(this.chkBxUpdateFloor);
            this.panel1.Controls.Add(this.chkBxUpdateSite);
            this.panel1.Controls.Add(this.chkBxCalcLengthsFromCloset);
            this.panel1.Controls.Add(this.btnAPsSetCloset);
            this.panel1.Controls.Add(this.txtBxMaxRunLengthInFeet);
            this.panel1.Controls.Add(this.chkBxRunOrientLRTB);
            this.panel1.Location = new System.Drawing.Point(5, 21);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(247, 203);
            this.panel1.TabIndex = 13;
            // 
            // chkBxUpdateBuilding
            // 
            this.chkBxUpdateBuilding.AutoSize = true;
            this.chkBxUpdateBuilding.Location = new System.Drawing.Point(168, 46);
            this.chkBxUpdateBuilding.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkBxUpdateBuilding.Name = "chkBxUpdateBuilding";
            this.chkBxUpdateBuilding.Size = new System.Drawing.Size(68, 21);
            this.chkBxUpdateBuilding.TabIndex = 15;
            this.chkBxUpdateBuilding.Text = "BLDG";
            this.chkBxUpdateBuilding.UseVisualStyleBackColor = true;
            // 
            // chkBxUpdateFloor
            // 
            this.chkBxUpdateFloor.AutoSize = true;
            this.chkBxUpdateFloor.Location = new System.Drawing.Point(168, 78);
            this.chkBxUpdateFloor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkBxUpdateFloor.Name = "chkBxUpdateFloor";
            this.chkBxUpdateFloor.Size = new System.Drawing.Size(78, 21);
            this.chkBxUpdateFloor.TabIndex = 15;
            this.chkBxUpdateFloor.Text = "FLOOR";
            this.chkBxUpdateFloor.UseVisualStyleBackColor = true;
            // 
            // chkBxUpdateSite
            // 
            this.chkBxUpdateSite.AutoSize = true;
            this.chkBxUpdateSite.Location = new System.Drawing.Point(168, 14);
            this.chkBxUpdateSite.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkBxUpdateSite.Name = "chkBxUpdateSite";
            this.chkBxUpdateSite.Size = new System.Drawing.Size(60, 21);
            this.chkBxUpdateSite.TabIndex = 15;
            this.chkBxUpdateSite.Text = "SITE";
            this.chkBxUpdateSite.UseVisualStyleBackColor = true;
            // 
            // chkBxCalcLengthsFromCloset
            // 
            this.chkBxCalcLengthsFromCloset.AutoSize = true;
            this.chkBxCalcLengthsFromCloset.Location = new System.Drawing.Point(63, 2);
            this.chkBxCalcLengthsFromCloset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkBxCalcLengthsFromCloset.Name = "chkBxCalcLengthsFromCloset";
            this.chkBxCalcLengthsFromCloset.Size = new System.Drawing.Size(112, 38);
            this.chkBxCalcLengthsFromCloset.TabIndex = 9;
            this.chkBxCalcLengthsFromCloset.Text = "Calc Lengths\r\nFrom Closet";
            this.chkBxCalcLengthsFromCloset.UseVisualStyleBackColor = true;
            // 
            // btnAPsSetCloset
            // 
            this.btnAPsSetCloset.Location = new System.Drawing.Point(15, 149);
            this.btnAPsSetCloset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAPsSetCloset.Name = "btnAPsSetCloset";
            this.btnAPsSetCloset.Size = new System.Drawing.Size(131, 44);
            this.btnAPsSetCloset.TabIndex = 12;
            this.btnAPsSetCloset.Text = "APs Set Closet";
            this.btnAPsSetCloset.UseVisualStyleBackColor = true;
            this.btnAPsSetCloset.Click += new System.EventHandler(this.BtnAPsSetCloset_Click);
            // 
            // txtBxMaxRunLengthInFeet
            // 
            this.txtBxMaxRunLengthInFeet.Location = new System.Drawing.Point(11, 10);
            this.txtBxMaxRunLengthInFeet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBxMaxRunLengthInFeet.Name = "txtBxMaxRunLengthInFeet";
            this.txtBxMaxRunLengthInFeet.Size = new System.Drawing.Size(48, 22);
            this.txtBxMaxRunLengthInFeet.TabIndex = 10;
            this.txtBxMaxRunLengthInFeet.Text = "250";
            // 
            // chkBxRunOrientLRTB
            // 
            this.chkBxRunOrientLRTB.AutoSize = true;
            this.chkBxRunOrientLRTB.Checked = true;
            this.chkBxRunOrientLRTB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxRunOrientLRTB.Location = new System.Drawing.Point(9, 42);
            this.chkBxRunOrientLRTB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkBxRunOrientLRTB.Name = "chkBxRunOrientLRTB";
            this.chkBxRunOrientLRTB.Size = new System.Drawing.Size(130, 38);
            this.chkBxRunOrientLRTB.TabIndex = 11;
            this.chkBxRunOrientLRTB.Text = "Run Orientation\r\nLR/TB";
            this.chkBxRunOrientLRTB.UseVisualStyleBackColor = true;
            // 
            // txtBxAPsStartNumberForRenumber
            // 
            this.txtBxAPsStartNumberForRenumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxAPsStartNumberForRenumber.Location = new System.Drawing.Point(20, 455);
            this.txtBxAPsStartNumberForRenumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBxAPsStartNumberForRenumber.Name = "txtBxAPsStartNumberForRenumber";
            this.txtBxAPsStartNumberForRenumber.Size = new System.Drawing.Size(100, 30);
            this.txtBxAPsStartNumberForRenumber.TabIndex = 8;
            this.txtBxAPsStartNumberForRenumber.Text = "1";
            // 
            // btnAPsSelectInPolyline
            // 
            this.btnAPsSelectInPolyline.Enabled = false;
            this.btnAPsSelectInPolyline.Location = new System.Drawing.Point(21, 286);
            this.btnAPsSelectInPolyline.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAPsSelectInPolyline.Name = "btnAPsSelectInPolyline";
            this.btnAPsSelectInPolyline.Size = new System.Drawing.Size(149, 48);
            this.btnAPsSelectInPolyline.TabIndex = 7;
            this.btnAPsSelectInPolyline.Text = "Select Aps In Polyline";
            this.btnAPsSelectInPolyline.UseVisualStyleBackColor = true;
            this.btnAPsSelectInPolyline.Click += new System.EventHandler(this.BtnAPsSelectInPolyline_Click);
            // 
            // btnAPsRemunber
            // 
            this.btnAPsRemunber.Location = new System.Drawing.Point(20, 491);
            this.btnAPsRemunber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAPsRemunber.Name = "btnAPsRemunber";
            this.btnAPsRemunber.Size = new System.Drawing.Size(91, 74);
            this.btnAPsRemunber.TabIndex = 6;
            this.btnAPsRemunber.Text = "Remunber Aps (Closet DD)";
            this.btnAPsRemunber.UseVisualStyleBackColor = true;
            this.btnAPsRemunber.Click += new System.EventHandler(this.BtnAPsRemunber_Click);
            // 
            // chkBxWapDual
            // 
            this.chkBxWapDual.AutoSize = true;
            this.chkBxWapDual.Checked = true;
            this.chkBxWapDual.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxWapDual.Location = new System.Drawing.Point(15, 421);
            this.chkBxWapDual.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkBxWapDual.Name = "chkBxWapDual";
            this.chkBxWapDual.Size = new System.Drawing.Size(116, 21);
            this.chkBxWapDual.TabIndex = 5;
            this.chkBxWapDual.Text = "Sel Wap Dual";
            this.chkBxWapDual.UseVisualStyleBackColor = true;
            // 
            // chkBxWap
            // 
            this.chkBxWap.AutoSize = true;
            this.chkBxWap.Checked = true;
            this.chkBxWap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxWap.Location = new System.Drawing.Point(15, 394);
            this.chkBxWap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkBxWap.Name = "chkBxWap";
            this.chkBxWap.Size = new System.Drawing.Size(83, 21);
            this.chkBxWap.TabIndex = 5;
            this.chkBxWap.Text = "Sel Wap";
            this.chkBxWap.UseVisualStyleBackColor = true;
            // 
            // btnAPsZoomAndSelect
            // 
            this.btnAPsZoomAndSelect.Location = new System.Drawing.Point(21, 338);
            this.btnAPsZoomAndSelect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAPsZoomAndSelect.Name = "btnAPsZoomAndSelect";
            this.btnAPsZoomAndSelect.Size = new System.Drawing.Size(149, 49);
            this.btnAPsZoomAndSelect.TabIndex = 4;
            this.btnAPsZoomAndSelect.Text = "Zoom And Select AP";
            this.btnAPsZoomAndSelect.UseVisualStyleBackColor = true;
            this.btnAPsZoomAndSelect.Click += new System.EventHandler(this.BtnAPsZoomAndSelect_Click);
            // 
            // btnAPsWAOsGet
            // 
            this.btnAPsWAOsGet.Location = new System.Drawing.Point(21, 230);
            this.btnAPsWAOsGet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAPsWAOsGet.Name = "btnAPsWAOsGet";
            this.btnAPsWAOsGet.Size = new System.Drawing.Size(149, 49);
            this.btnAPsWAOsGet.TabIndex = 3;
            this.btnAPsWAOsGet.Text = "Get APs + WAOs";
            this.btnAPsWAOsGet.UseVisualStyleBackColor = true;
            this.btnAPsWAOsGet.Click += new System.EventHandler(this.BtnAPsWAOsGet_Click);
            // 
            // dgvWaps
            // 
            this.dgvWaps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWaps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWaps.Location = new System.Drawing.Point(0, 0);
            this.dgvWaps.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvWaps.Name = "dgvWaps";
            this.dgvWaps.Size = new System.Drawing.Size(310, 703);
            this.dgvWaps.TabIndex = 0;
            // 
            // dgvWapData2Duals
            // 
            this.dgvWapData2Duals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWapData2Duals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWapData2Duals.Location = new System.Drawing.Point(0, 0);
            this.dgvWapData2Duals.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvWapData2Duals.Name = "dgvWapData2Duals";
            this.dgvWapData2Duals.Size = new System.Drawing.Size(618, 703);
            this.dgvWapData2Duals.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvWaps);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvWapData2Duals);
            this.splitContainer1.Size = new System.Drawing.Size(933, 703);
            this.splitContainer1.SplitterDistance = 310;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 7;
            // 
            // BtnFindMissingNums
            // 
            this.BtnFindMissingNums.Location = new System.Drawing.Point(163, 615);
            this.BtnFindMissingNums.Name = "BtnFindMissingNums";
            this.BtnFindMissingNums.Size = new System.Drawing.Size(91, 78);
            this.BtnFindMissingNums.TabIndex = 28;
            this.BtnFindMissingNums.Text = "Find Missing Nums";
            this.BtnFindMissingNums.UseVisualStyleBackColor = true;
            this.BtnFindMissingNums.Click += new System.EventHandler(this.BtnFindMissingNums_Click);
            // 
            // uc_BW_SiteInfo1
            // 
            this.uc_BW_SiteInfo1.Location = new System.Drawing.Point(4, 5);
            this.uc_BW_SiteInfo1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.uc_BW_SiteInfo1.Name = "uc_BW_SiteInfo1";
            this.uc_BW_SiteInfo1.Size = new System.Drawing.Size(160, 135);
            this.uc_BW_SiteInfo1.TabIndex = 16;
            // 
            // Uc_BW_TP_APs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.gBxAPs);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Uc_BW_TP_APs";
            this.Size = new System.Drawing.Size(1192, 703);
            this.gBxAPs.ResumeLayout(false);
            this.gBxAPs.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWaps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWapData2Duals)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gBxAPs;
        private System.Windows.Forms.Button btnAPsRenumberAllByCloset;
        private System.Windows.Forms.Button btnWapsFindDups;
        private System.Windows.Forms.Button btnLabelAPsIndividually;
        private System.Windows.Forms.Button btnRefreshAps;
        private System.Windows.Forms.Button btnRestoreOriginalAPsLst;
        private System.Windows.Forms.Button btnSelectByViewPort;
        private System.Windows.Forms.ComboBox cBxAPClosetFilter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkBxUpdateBuilding;
        private System.Windows.Forms.CheckBox chkBxUpdateFloor;
        private System.Windows.Forms.CheckBox chkBxUpdateSite;
        private System.Windows.Forms.CheckBox chkBxCalcLengthsFromCloset;
        private System.Windows.Forms.Button btnAPsSetCloset;
        private System.Windows.Forms.TextBox txtBxMaxRunLengthInFeet;
        private System.Windows.Forms.CheckBox chkBxRunOrientLRTB;
        private System.Windows.Forms.TextBox txtBxAPsStartNumberForRenumber;
        private System.Windows.Forms.Button btnAPsSelectInPolyline;
        private System.Windows.Forms.Button btnAPsRemunber;
        private System.Windows.Forms.CheckBox chkBxWapDual;
        private System.Windows.Forms.CheckBox chkBxWap;
        private System.Windows.Forms.Button btnAPsZoomAndSelect;
        private System.Windows.Forms.Button btnAPsWAOsGet;
        private System.Windows.Forms.Button BtnSelAMsAPs;
        private Uc_BW_SiteInfo uc_BW_SiteInfo1;
        private System.Windows.Forms.DataGridView dgvWaps;
        private System.Windows.Forms.DataGridView dgvWapData2Duals;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button BtnFindMissingNums;
    }
}

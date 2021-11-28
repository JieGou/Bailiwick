namespace MyFirstProject.BW.TGT
{
    partial class Frm_TGT_MainForm
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
            this.tCtlMain = new System.Windows.Forms.TabControl();
            this.tpCounts = new System.Windows.Forms.TabPage();
            this.dgvCounts = new System.Windows.Forms.DataGridView();
            this.tpVoiceAndData = new System.Windows.Forms.TabPage();
            this.dgvVoiceAndData = new System.Windows.Forms.DataGridView();
            this.tpT4VoiceAndData = new System.Windows.Forms.TabPage();
            this.dgvT4VoiceAndData = new System.Windows.Forms.DataGridView();
            this.tpTVSCounts = new System.Windows.Forms.TabPage();
            this.dgvTVSCounts = new System.Windows.Forms.DataGridView();
            this.tpPagingCounts = new System.Windows.Forms.TabPage();
            this.dgvPagingCounts = new System.Windows.Forms.DataGridView();
            this.tpDataExtraction = new System.Windows.Forms.TabPage();
            this.dgvDataExtraction = new System.Windows.Forms.DataGridView();
            this.tpOpexBid = new System.Windows.Forms.TabPage();
            this.dgv_Extract = new System.Windows.Forms.DataGridView();
            this.BtnOpexCounts = new System.Windows.Forms.Button();
            this.btnGetBlocks = new System.Windows.Forms.Button();
            this.btnInsertTable = new System.Windows.Forms.Button();
            this.btnTestData = new System.Windows.Forms.Button();
            this.btnSaveDataToFile = new System.Windows.Forms.Button();
            this.chkBxClearGrid = new System.Windows.Forms.CheckBox();
            this.btnDataExtract = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.GrpBxUpdatePrintIdAttribute = new System.Windows.Forms.GroupBox();
            this.CBoxPrefix = new System.Windows.Forms.ComboBox();
            this.BtnPickPrintIdAttribute = new System.Windows.Forms.Button();
            this.txtBx_NumToStartAt = new System.Windows.Forms.TextBox();
            this.btnCounts = new System.Windows.Forms.Button();
            this.tCtlMain.SuspendLayout();
            this.tpCounts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCounts)).BeginInit();
            this.tpVoiceAndData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoiceAndData)).BeginInit();
            this.tpT4VoiceAndData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvT4VoiceAndData)).BeginInit();
            this.tpTVSCounts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTVSCounts)).BeginInit();
            this.tpPagingCounts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagingCounts)).BeginInit();
            this.tpDataExtraction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataExtraction)).BeginInit();
            this.tpOpexBid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Extract)).BeginInit();
            this.panel1.SuspendLayout();
            this.GrpBxUpdatePrintIdAttribute.SuspendLayout();
            this.SuspendLayout();
            // 
            // tCtlMain
            // 
            this.tCtlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tCtlMain.Controls.Add(this.tpCounts);
            this.tCtlMain.Controls.Add(this.tpVoiceAndData);
            this.tCtlMain.Controls.Add(this.tpT4VoiceAndData);
            this.tCtlMain.Controls.Add(this.tpTVSCounts);
            this.tCtlMain.Controls.Add(this.tpPagingCounts);
            this.tCtlMain.Controls.Add(this.tpDataExtraction);
            this.tCtlMain.Controls.Add(this.tpOpexBid);
            this.tCtlMain.Location = new System.Drawing.Point(-1, 0);
            this.tCtlMain.Margin = new System.Windows.Forms.Padding(2);
            this.tCtlMain.Name = "tCtlMain";
            this.tCtlMain.SelectedIndex = 0;
            this.tCtlMain.Size = new System.Drawing.Size(568, 481);
            this.tCtlMain.TabIndex = 0;
            // 
            // tpCounts
            // 
            this.tpCounts.Controls.Add(this.dgvCounts);
            this.tpCounts.Location = new System.Drawing.Point(4, 22);
            this.tpCounts.Margin = new System.Windows.Forms.Padding(2);
            this.tpCounts.Name = "tpCounts";
            this.tpCounts.Padding = new System.Windows.Forms.Padding(2);
            this.tpCounts.Size = new System.Drawing.Size(560, 455);
            this.tpCounts.TabIndex = 0;
            this.tpCounts.Text = "Counts";
            this.tpCounts.UseVisualStyleBackColor = true;
            // 
            // dgvCounts
            // 
            this.dgvCounts.AllowUserToAddRows = false;
            this.dgvCounts.AllowUserToDeleteRows = false;
            this.dgvCounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCounts.Location = new System.Drawing.Point(2, 2);
            this.dgvCounts.Margin = new System.Windows.Forms.Padding(2);
            this.dgvCounts.Name = "dgvCounts";
            this.dgvCounts.RowTemplate.Height = 24;
            this.dgvCounts.Size = new System.Drawing.Size(556, 451);
            this.dgvCounts.TabIndex = 0;
            // 
            // tpVoiceAndData
            // 
            this.tpVoiceAndData.Controls.Add(this.dgvVoiceAndData);
            this.tpVoiceAndData.Location = new System.Drawing.Point(4, 22);
            this.tpVoiceAndData.Margin = new System.Windows.Forms.Padding(2);
            this.tpVoiceAndData.Name = "tpVoiceAndData";
            this.tpVoiceAndData.Padding = new System.Windows.Forms.Padding(2);
            this.tpVoiceAndData.Size = new System.Drawing.Size(560, 455);
            this.tpVoiceAndData.TabIndex = 1;
            this.tpVoiceAndData.Text = "Voice And Data";
            this.tpVoiceAndData.UseVisualStyleBackColor = true;
            // 
            // dgvVoiceAndData
            // 
            this.dgvVoiceAndData.AllowUserToAddRows = false;
            this.dgvVoiceAndData.AllowUserToDeleteRows = false;
            this.dgvVoiceAndData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVoiceAndData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVoiceAndData.Location = new System.Drawing.Point(2, 2);
            this.dgvVoiceAndData.Margin = new System.Windows.Forms.Padding(2);
            this.dgvVoiceAndData.Name = "dgvVoiceAndData";
            this.dgvVoiceAndData.RowTemplate.Height = 24;
            this.dgvVoiceAndData.Size = new System.Drawing.Size(556, 451);
            this.dgvVoiceAndData.TabIndex = 1;
            // 
            // tpT4VoiceAndData
            // 
            this.tpT4VoiceAndData.Controls.Add(this.dgvT4VoiceAndData);
            this.tpT4VoiceAndData.Location = new System.Drawing.Point(4, 22);
            this.tpT4VoiceAndData.Margin = new System.Windows.Forms.Padding(2);
            this.tpT4VoiceAndData.Name = "tpT4VoiceAndData";
            this.tpT4VoiceAndData.Size = new System.Drawing.Size(560, 455);
            this.tpT4VoiceAndData.TabIndex = 2;
            this.tpT4VoiceAndData.Text = "T4 Voice And Data";
            this.tpT4VoiceAndData.UseVisualStyleBackColor = true;
            // 
            // dgvT4VoiceAndData
            // 
            this.dgvT4VoiceAndData.AllowUserToAddRows = false;
            this.dgvT4VoiceAndData.AllowUserToDeleteRows = false;
            this.dgvT4VoiceAndData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvT4VoiceAndData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvT4VoiceAndData.Location = new System.Drawing.Point(0, 0);
            this.dgvT4VoiceAndData.Margin = new System.Windows.Forms.Padding(2);
            this.dgvT4VoiceAndData.Name = "dgvT4VoiceAndData";
            this.dgvT4VoiceAndData.RowTemplate.Height = 24;
            this.dgvT4VoiceAndData.Size = new System.Drawing.Size(560, 455);
            this.dgvT4VoiceAndData.TabIndex = 1;
            // 
            // tpTVSCounts
            // 
            this.tpTVSCounts.Controls.Add(this.dgvTVSCounts);
            this.tpTVSCounts.Location = new System.Drawing.Point(4, 22);
            this.tpTVSCounts.Margin = new System.Windows.Forms.Padding(2);
            this.tpTVSCounts.Name = "tpTVSCounts";
            this.tpTVSCounts.Size = new System.Drawing.Size(560, 455);
            this.tpTVSCounts.TabIndex = 3;
            this.tpTVSCounts.Text = "TVS Counts";
            this.tpTVSCounts.UseVisualStyleBackColor = true;
            // 
            // dgvTVSCounts
            // 
            this.dgvTVSCounts.AllowUserToAddRows = false;
            this.dgvTVSCounts.AllowUserToDeleteRows = false;
            this.dgvTVSCounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTVSCounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTVSCounts.Location = new System.Drawing.Point(0, 0);
            this.dgvTVSCounts.Margin = new System.Windows.Forms.Padding(2);
            this.dgvTVSCounts.Name = "dgvTVSCounts";
            this.dgvTVSCounts.RowTemplate.Height = 24;
            this.dgvTVSCounts.Size = new System.Drawing.Size(560, 455);
            this.dgvTVSCounts.TabIndex = 1;
            // 
            // tpPagingCounts
            // 
            this.tpPagingCounts.Controls.Add(this.dgvPagingCounts);
            this.tpPagingCounts.Location = new System.Drawing.Point(4, 22);
            this.tpPagingCounts.Margin = new System.Windows.Forms.Padding(2);
            this.tpPagingCounts.Name = "tpPagingCounts";
            this.tpPagingCounts.Size = new System.Drawing.Size(560, 455);
            this.tpPagingCounts.TabIndex = 4;
            this.tpPagingCounts.Text = "Paging Counts";
            this.tpPagingCounts.UseVisualStyleBackColor = true;
            // 
            // dgvPagingCounts
            // 
            this.dgvPagingCounts.AllowUserToAddRows = false;
            this.dgvPagingCounts.AllowUserToDeleteRows = false;
            this.dgvPagingCounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPagingCounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPagingCounts.Location = new System.Drawing.Point(0, 0);
            this.dgvPagingCounts.Margin = new System.Windows.Forms.Padding(2);
            this.dgvPagingCounts.Name = "dgvPagingCounts";
            this.dgvPagingCounts.RowTemplate.Height = 24;
            this.dgvPagingCounts.Size = new System.Drawing.Size(560, 455);
            this.dgvPagingCounts.TabIndex = 1;
            // 
            // tpDataExtraction
            // 
            this.tpDataExtraction.Controls.Add(this.dgvDataExtraction);
            this.tpDataExtraction.Location = new System.Drawing.Point(4, 22);
            this.tpDataExtraction.Margin = new System.Windows.Forms.Padding(2);
            this.tpDataExtraction.Name = "tpDataExtraction";
            this.tpDataExtraction.Size = new System.Drawing.Size(560, 455);
            this.tpDataExtraction.TabIndex = 5;
            this.tpDataExtraction.Text = "Data Extraction";
            this.tpDataExtraction.UseVisualStyleBackColor = true;
            // 
            // dgvDataExtraction
            // 
            this.dgvDataExtraction.AllowUserToAddRows = false;
            this.dgvDataExtraction.AllowUserToDeleteRows = false;
            this.dgvDataExtraction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataExtraction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDataExtraction.Location = new System.Drawing.Point(0, 0);
            this.dgvDataExtraction.Margin = new System.Windows.Forms.Padding(2);
            this.dgvDataExtraction.Name = "dgvDataExtraction";
            this.dgvDataExtraction.RowTemplate.Height = 24;
            this.dgvDataExtraction.Size = new System.Drawing.Size(560, 455);
            this.dgvDataExtraction.TabIndex = 1;
            // 
            // tpOpexBid
            // 
            this.tpOpexBid.Controls.Add(this.dgv_Extract);
            this.tpOpexBid.Controls.Add(this.BtnOpexCounts);
            this.tpOpexBid.Location = new System.Drawing.Point(4, 22);
            this.tpOpexBid.Name = "tpOpexBid";
            this.tpOpexBid.Padding = new System.Windows.Forms.Padding(3);
            this.tpOpexBid.Size = new System.Drawing.Size(560, 455);
            this.tpOpexBid.TabIndex = 6;
            this.tpOpexBid.Text = "Opex Bid";
            this.tpOpexBid.UseVisualStyleBackColor = true;
            // 
            // dgv_Extract
            // 
            this.dgv_Extract.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Extract.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Extract.Location = new System.Drawing.Point(3, 64);
            this.dgv_Extract.Name = "dgv_Extract";
            this.dgv_Extract.Size = new System.Drawing.Size(551, 388);
            this.dgv_Extract.TabIndex = 1;
            // 
            // BtnOpexCounts
            // 
            this.BtnOpexCounts.Location = new System.Drawing.Point(33, 31);
            this.BtnOpexCounts.Name = "BtnOpexCounts";
            this.BtnOpexCounts.Size = new System.Drawing.Size(75, 23);
            this.BtnOpexCounts.TabIndex = 0;
            this.BtnOpexCounts.Text = "Counts";
            this.BtnOpexCounts.UseVisualStyleBackColor = true;
            this.BtnOpexCounts.Click += new System.EventHandler(this.BtnOpexCounts_Click);
            // 
            // btnGetBlocks
            // 
            this.btnGetBlocks.Location = new System.Drawing.Point(97, 20);
            this.btnGetBlocks.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetBlocks.Name = "btnGetBlocks";
            this.btnGetBlocks.Size = new System.Drawing.Size(80, 42);
            this.btnGetBlocks.TabIndex = 1;
            this.btnGetBlocks.Text = "Get Blocks";
            this.btnGetBlocks.UseVisualStyleBackColor = true;
            this.btnGetBlocks.Click += new System.EventHandler(this.BtnGetBlocks_Click);
            // 
            // btnInsertTable
            // 
            this.btnInsertTable.Location = new System.Drawing.Point(97, 86);
            this.btnInsertTable.Margin = new System.Windows.Forms.Padding(2);
            this.btnInsertTable.Name = "btnInsertTable";
            this.btnInsertTable.Size = new System.Drawing.Size(80, 42);
            this.btnInsertTable.TabIndex = 2;
            this.btnInsertTable.Text = "Insert Table";
            this.btnInsertTable.UseVisualStyleBackColor = true;
            this.btnInsertTable.Click += new System.EventHandler(this.BtnInsertTable_Click);
            // 
            // btnTestData
            // 
            this.btnTestData.Enabled = false;
            this.btnTestData.Location = new System.Drawing.Point(97, 144);
            this.btnTestData.Margin = new System.Windows.Forms.Padding(2);
            this.btnTestData.Name = "btnTestData";
            this.btnTestData.Size = new System.Drawing.Size(80, 42);
            this.btnTestData.TabIndex = 3;
            this.btnTestData.Text = "Test Data";
            this.btnTestData.UseVisualStyleBackColor = true;
            this.btnTestData.Click += new System.EventHandler(this.BtnTestData_Click);
            // 
            // btnSaveDataToFile
            // 
            this.btnSaveDataToFile.Location = new System.Drawing.Point(97, 405);
            this.btnSaveDataToFile.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveDataToFile.Name = "btnSaveDataToFile";
            this.btnSaveDataToFile.Size = new System.Drawing.Size(80, 60);
            this.btnSaveDataToFile.TabIndex = 5;
            this.btnSaveDataToFile.Text = "Save Data To File (XL Pull)";
            this.btnSaveDataToFile.UseVisualStyleBackColor = true;
            this.btnSaveDataToFile.Click += new System.EventHandler(this.BtnSaveDataToFile_Click);
            // 
            // chkBxClearGrid
            // 
            this.chkBxClearGrid.AutoSize = true;
            this.chkBxClearGrid.Checked = true;
            this.chkBxClearGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxClearGrid.Enabled = false;
            this.chkBxClearGrid.Location = new System.Drawing.Point(106, 316);
            this.chkBxClearGrid.Margin = new System.Windows.Forms.Padding(2);
            this.chkBxClearGrid.Name = "chkBxClearGrid";
            this.chkBxClearGrid.Size = new System.Drawing.Size(72, 17);
            this.chkBxClearGrid.TabIndex = 7;
            this.chkBxClearGrid.Text = "Clear Grid";
            this.chkBxClearGrid.UseVisualStyleBackColor = true;
            // 
            // btnDataExtract
            // 
            this.btnDataExtract.Enabled = false;
            this.btnDataExtract.Location = new System.Drawing.Point(97, 338);
            this.btnDataExtract.Margin = new System.Windows.Forms.Padding(2);
            this.btnDataExtract.Name = "btnDataExtract";
            this.btnDataExtract.Size = new System.Drawing.Size(80, 63);
            this.btnDataExtract.TabIndex = 6;
            this.btnDataExtract.Text = "Data Extract";
            this.btnDataExtract.UseVisualStyleBackColor = true;
            this.btnDataExtract.Click += new System.EventHandler(this.BtnDataExtract_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.GrpBxUpdatePrintIdAttribute);
            this.panel1.Controls.Add(this.btnCounts);
            this.panel1.Controls.Add(this.chkBxClearGrid);
            this.panel1.Controls.Add(this.btnGetBlocks);
            this.panel1.Controls.Add(this.btnDataExtract);
            this.panel1.Controls.Add(this.btnInsertTable);
            this.panel1.Controls.Add(this.btnSaveDataToFile);
            this.panel1.Controls.Add(this.btnTestData);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(570, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(190, 481);
            this.panel1.TabIndex = 1;
            // 
            // GrpBxUpdatePrintIdAttribute
            // 
            this.GrpBxUpdatePrintIdAttribute.Controls.Add(this.CBoxPrefix);
            this.GrpBxUpdatePrintIdAttribute.Controls.Add(this.BtnPickPrintIdAttribute);
            this.GrpBxUpdatePrintIdAttribute.Controls.Add(this.txtBx_NumToStartAt);
            this.GrpBxUpdatePrintIdAttribute.Location = new System.Drawing.Point(10, 218);
            this.GrpBxUpdatePrintIdAttribute.Name = "GrpBxUpdatePrintIdAttribute";
            this.GrpBxUpdatePrintIdAttribute.Size = new System.Drawing.Size(166, 84);
            this.GrpBxUpdatePrintIdAttribute.TabIndex = 9;
            this.GrpBxUpdatePrintIdAttribute.TabStop = false;
            this.GrpBxUpdatePrintIdAttribute.Text = "PickPrintIdAttribute";
            // 
            // CBoxPrefix
            // 
            this.CBoxPrefix.FormattingEnabled = true;
            this.CBoxPrefix.Items.AddRange(new object[] {
            "AJ",
            "AK",
            "AB",
            "MHE",
            "MHF"});
            this.CBoxPrefix.Location = new System.Drawing.Point(6, 18);
            this.CBoxPrefix.Name = "CBoxPrefix";
            this.CBoxPrefix.Size = new System.Drawing.Size(77, 21);
            this.CBoxPrefix.TabIndex = 10;
            // 
            // BtnPickPrintIdAttribute
            // 
            this.BtnPickPrintIdAttribute.Location = new System.Drawing.Point(6, 46);
            this.BtnPickPrintIdAttribute.Name = "BtnPickPrintIdAttribute";
            this.BtnPickPrintIdAttribute.Size = new System.Drawing.Size(155, 32);
            this.BtnPickPrintIdAttribute.TabIndex = 6;
            this.BtnPickPrintIdAttribute.Text = "Pick Print ID Attribute";
            this.BtnPickPrintIdAttribute.UseVisualStyleBackColor = true;
            this.BtnPickPrintIdAttribute.Click += new System.EventHandler(this.BtnPickPrintIdAttribute_Click);
            // 
            // txtBx_NumToStartAt
            // 
            this.txtBx_NumToStartAt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBx_NumToStartAt.Location = new System.Drawing.Point(88, 18);
            this.txtBx_NumToStartAt.Margin = new System.Windows.Forms.Padding(2);
            this.txtBx_NumToStartAt.Name = "txtBx_NumToStartAt";
            this.txtBx_NumToStartAt.Size = new System.Drawing.Size(73, 23);
            this.txtBx_NumToStartAt.TabIndex = 5;
            this.txtBx_NumToStartAt.Text = "1";
            // 
            // btnCounts
            // 
            this.btnCounts.Location = new System.Drawing.Point(13, 86);
            this.btnCounts.Margin = new System.Windows.Forms.Padding(2);
            this.btnCounts.Name = "btnCounts";
            this.btnCounts.Size = new System.Drawing.Size(80, 42);
            this.btnCounts.TabIndex = 8;
            this.btnCounts.Text = "Counts";
            this.btnCounts.UseVisualStyleBackColor = true;
            this.btnCounts.Click += new System.EventHandler(this.BtnCounts_Click);
            // 
            // Frm_TGT_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 481);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tCtlMain);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Frm_TGT_MainForm";
            this.Text = "frmTGTMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_TGT_MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Frm_TGT_MainForm_Load);
            this.tCtlMain.ResumeLayout(false);
            this.tpCounts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCounts)).EndInit();
            this.tpVoiceAndData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoiceAndData)).EndInit();
            this.tpT4VoiceAndData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvT4VoiceAndData)).EndInit();
            this.tpTVSCounts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTVSCounts)).EndInit();
            this.tpPagingCounts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagingCounts)).EndInit();
            this.tpDataExtraction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataExtraction)).EndInit();
            this.tpOpexBid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Extract)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.GrpBxUpdatePrintIdAttribute.ResumeLayout(false);
            this.GrpBxUpdatePrintIdAttribute.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tCtlMain;
        private System.Windows.Forms.TabPage tpCounts;
        private System.Windows.Forms.TabPage tpVoiceAndData;
        private System.Windows.Forms.Button btnGetBlocks;
        private System.Windows.Forms.DataGridView dgvCounts;
        private System.Windows.Forms.Button btnInsertTable;
        private System.Windows.Forms.TabPage tpT4VoiceAndData;
        private System.Windows.Forms.TabPage tpTVSCounts;
        private System.Windows.Forms.TabPage tpPagingCounts;
        private System.Windows.Forms.DataGridView dgvVoiceAndData;
        private System.Windows.Forms.DataGridView dgvT4VoiceAndData;
        private System.Windows.Forms.DataGridView dgvTVSCounts;
        private System.Windows.Forms.DataGridView dgvPagingCounts;
        private System.Windows.Forms.Button btnTestData;
        private System.Windows.Forms.Button btnSaveDataToFile;
        private System.Windows.Forms.CheckBox chkBxClearGrid;
        private System.Windows.Forms.Button btnDataExtract;
        private System.Windows.Forms.TabPage tpDataExtraction;
        private System.Windows.Forms.DataGridView dgvDataExtraction;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCounts;
        private System.Windows.Forms.TabPage tpOpexBid;
        private System.Windows.Forms.Button BtnOpexCounts;
        private System.Windows.Forms.DataGridView dgv_Extract;
        private System.Windows.Forms.GroupBox GrpBxUpdatePrintIdAttribute;
        private System.Windows.Forms.Button BtnPickPrintIdAttribute;
        private System.Windows.Forms.TextBox txtBx_NumToStartAt;
        private System.Windows.Forms.ComboBox CBoxPrefix;
    }
}
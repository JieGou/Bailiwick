namespace MyFirstProject.BW
{
    partial class Uc_BW_TP_AttrExtract
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
            this.chkBxClearGrid = new System.Windows.Forms.CheckBox();
            this.btnSaveDataToFile = new System.Windows.Forms.Button();
            this.btnDataExtract = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnTextExtractSpecial = new System.Windows.Forms.Button();
            this.tbCtlDataExtract = new System.Windows.Forms.TabControl();
            this.tpDateExtAllAtts = new System.Windows.Forms.TabPage();
            this.dgvAllAtts = new System.Windows.Forms.DataGridView();
            this.tpDataExtCountsWap = new System.Windows.Forms.TabPage();
            this.dgvExtractCountsWap = new System.Windows.Forms.DataGridView();
            this.tpDataExtCountsWao = new System.Windows.Forms.TabPage();
            this.dgvExtractCountsWao = new System.Windows.Forms.DataGridView();
            this.BtnCounts = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tbCtlDataExtract.SuspendLayout();
            this.tpDateExtAllAtts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllAtts)).BeginInit();
            this.tpDataExtCountsWap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtractCountsWap)).BeginInit();
            this.tpDataExtCountsWao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtractCountsWao)).BeginInit();
            this.SuspendLayout();
            // 
            // chkBxClearGrid
            // 
            this.chkBxClearGrid.AutoSize = true;
            this.chkBxClearGrid.Checked = true;
            this.chkBxClearGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxClearGrid.Location = new System.Drawing.Point(14, 6);
            this.chkBxClearGrid.Margin = new System.Windows.Forms.Padding(2);
            this.chkBxClearGrid.Name = "chkBxClearGrid";
            this.chkBxClearGrid.Size = new System.Drawing.Size(72, 17);
            this.chkBxClearGrid.TabIndex = 2;
            this.chkBxClearGrid.Text = "Clear Grid";
            this.chkBxClearGrid.UseVisualStyleBackColor = true;
            // 
            // btnSaveDataToFile
            // 
            this.btnSaveDataToFile.Location = new System.Drawing.Point(14, 110);
            this.btnSaveDataToFile.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveDataToFile.Name = "btnSaveDataToFile";
            this.btnSaveDataToFile.Size = new System.Drawing.Size(80, 60);
            this.btnSaveDataToFile.TabIndex = 1;
            this.btnSaveDataToFile.Text = "Save Data To File (XL Pull)";
            this.btnSaveDataToFile.UseVisualStyleBackColor = true;
            this.btnSaveDataToFile.Click += new System.EventHandler(this.BtnSaveDataToFile_Click);
            // 
            // btnDataExtract
            // 
            this.btnDataExtract.Location = new System.Drawing.Point(14, 42);
            this.btnDataExtract.Margin = new System.Windows.Forms.Padding(2);
            this.btnDataExtract.Name = "btnDataExtract";
            this.btnDataExtract.Size = new System.Drawing.Size(80, 63);
            this.btnDataExtract.TabIndex = 0;
            this.btnDataExtract.Text = "Data Extract";
            this.btnDataExtract.UseVisualStyleBackColor = true;
            this.btnDataExtract.Click += new System.EventHandler(this.BtnDataExtract_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnCounts);
            this.panel1.Controls.Add(this.BtnTextExtractSpecial);
            this.panel1.Controls.Add(this.chkBxClearGrid);
            this.panel1.Controls.Add(this.btnDataExtract);
            this.panel1.Controls.Add(this.btnSaveDataToFile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(593, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(106, 391);
            this.panel1.TabIndex = 3;
            // 
            // BtnTextExtractSpecial
            // 
            this.BtnTextExtractSpecial.Location = new System.Drawing.Point(14, 294);
            this.BtnTextExtractSpecial.Name = "BtnTextExtractSpecial";
            this.BtnTextExtractSpecial.Size = new System.Drawing.Size(75, 71);
            this.BtnTextExtractSpecial.TabIndex = 3;
            this.BtnTextExtractSpecial.Text = "Text Extract Special";
            this.BtnTextExtractSpecial.UseVisualStyleBackColor = true;
            this.BtnTextExtractSpecial.Click += new System.EventHandler(this.BtnTextExtractSpecial_Click);
            // 
            // tbCtlDataExtract
            // 
            this.tbCtlDataExtract.Controls.Add(this.tpDateExtAllAtts);
            this.tbCtlDataExtract.Controls.Add(this.tpDataExtCountsWap);
            this.tbCtlDataExtract.Controls.Add(this.tpDataExtCountsWao);
            this.tbCtlDataExtract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCtlDataExtract.Location = new System.Drawing.Point(0, 0);
            this.tbCtlDataExtract.Margin = new System.Windows.Forms.Padding(2);
            this.tbCtlDataExtract.Name = "tbCtlDataExtract";
            this.tbCtlDataExtract.SelectedIndex = 0;
            this.tbCtlDataExtract.Size = new System.Drawing.Size(593, 391);
            this.tbCtlDataExtract.TabIndex = 3;
            // 
            // tpDateExtAllAtts
            // 
            this.tpDateExtAllAtts.Controls.Add(this.dgvAllAtts);
            this.tpDateExtAllAtts.Location = new System.Drawing.Point(4, 22);
            this.tpDateExtAllAtts.Margin = new System.Windows.Forms.Padding(2);
            this.tpDateExtAllAtts.Name = "tpDateExtAllAtts";
            this.tpDateExtAllAtts.Padding = new System.Windows.Forms.Padding(2);
            this.tpDateExtAllAtts.Size = new System.Drawing.Size(585, 365);
            this.tpDateExtAllAtts.TabIndex = 0;
            this.tpDateExtAllAtts.Text = "DateExtAllAtts";
            this.tpDateExtAllAtts.UseVisualStyleBackColor = true;
            // 
            // dgvAllAtts
            // 
            this.dgvAllAtts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllAtts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAllAtts.Location = new System.Drawing.Point(2, 2);
            this.dgvAllAtts.Margin = new System.Windows.Forms.Padding(2);
            this.dgvAllAtts.MultiSelect = false;
            this.dgvAllAtts.Name = "dgvAllAtts";
            this.dgvAllAtts.ReadOnly = true;
            this.dgvAllAtts.RowTemplate.Height = 24;
            this.dgvAllAtts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAllAtts.Size = new System.Drawing.Size(581, 361);
            this.dgvAllAtts.TabIndex = 1;
            // 
            // tpDataExtCountsWap
            // 
            this.tpDataExtCountsWap.Controls.Add(this.dgvExtractCountsWap);
            this.tpDataExtCountsWap.Location = new System.Drawing.Point(4, 22);
            this.tpDataExtCountsWap.Margin = new System.Windows.Forms.Padding(2);
            this.tpDataExtCountsWap.Name = "tpDataExtCountsWap";
            this.tpDataExtCountsWap.Padding = new System.Windows.Forms.Padding(2);
            this.tpDataExtCountsWap.Size = new System.Drawing.Size(585, 365);
            this.tpDataExtCountsWap.TabIndex = 1;
            this.tpDataExtCountsWap.Text = "WAPs";
            this.tpDataExtCountsWap.UseVisualStyleBackColor = true;
            // 
            // dgvExtractCountsWap
            // 
            this.dgvExtractCountsWap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExtractCountsWap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExtractCountsWap.Location = new System.Drawing.Point(2, 2);
            this.dgvExtractCountsWap.Margin = new System.Windows.Forms.Padding(2);
            this.dgvExtractCountsWap.MultiSelect = false;
            this.dgvExtractCountsWap.Name = "dgvExtractCountsWap";
            this.dgvExtractCountsWap.ReadOnly = true;
            this.dgvExtractCountsWap.RowTemplate.Height = 24;
            this.dgvExtractCountsWap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExtractCountsWap.Size = new System.Drawing.Size(581, 361);
            this.dgvExtractCountsWap.TabIndex = 2;
            // 
            // tpDataExtCountsWao
            // 
            this.tpDataExtCountsWao.Controls.Add(this.dgvExtractCountsWao);
            this.tpDataExtCountsWao.Location = new System.Drawing.Point(4, 22);
            this.tpDataExtCountsWao.Margin = new System.Windows.Forms.Padding(2);
            this.tpDataExtCountsWao.Name = "tpDataExtCountsWao";
            this.tpDataExtCountsWao.Size = new System.Drawing.Size(585, 365);
            this.tpDataExtCountsWao.TabIndex = 2;
            this.tpDataExtCountsWao.Text = "WAOs";
            this.tpDataExtCountsWao.UseVisualStyleBackColor = true;
            // 
            // dgvExtractCountsWao
            // 
            this.dgvExtractCountsWao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExtractCountsWao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExtractCountsWao.Location = new System.Drawing.Point(0, 0);
            this.dgvExtractCountsWao.Margin = new System.Windows.Forms.Padding(2);
            this.dgvExtractCountsWao.MultiSelect = false;
            this.dgvExtractCountsWao.Name = "dgvExtractCountsWao";
            this.dgvExtractCountsWao.ReadOnly = true;
            this.dgvExtractCountsWao.RowTemplate.Height = 24;
            this.dgvExtractCountsWao.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExtractCountsWao.Size = new System.Drawing.Size(585, 365);
            this.dgvExtractCountsWao.TabIndex = 3;
            // 
            // BtnCounts
            // 
            this.BtnCounts.Location = new System.Drawing.Point(14, 240);
            this.BtnCounts.Name = "BtnCounts";
            this.BtnCounts.Size = new System.Drawing.Size(75, 23);
            this.BtnCounts.TabIndex = 4;
            this.BtnCounts.Text = "Counts";
            this.BtnCounts.UseVisualStyleBackColor = true;
            this.BtnCounts.Click += new System.EventHandler(this.BtnCounts_Click);
            // 
            // Uc_BW_TP_AttrExtract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbCtlDataExtract);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Uc_BW_TP_AttrExtract";
            this.Size = new System.Drawing.Size(699, 391);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tbCtlDataExtract.ResumeLayout(false);
            this.tpDateExtAllAtts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllAtts)).EndInit();
            this.tpDataExtCountsWap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtractCountsWap)).EndInit();
            this.tpDataExtCountsWao.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtractCountsWao)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox chkBxClearGrid;
        private System.Windows.Forms.Button btnSaveDataToFile;
        private System.Windows.Forms.Button btnDataExtract;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tbCtlDataExtract;
        private System.Windows.Forms.TabPage tpDateExtAllAtts;
        private System.Windows.Forms.DataGridView dgvAllAtts;
        private System.Windows.Forms.TabPage tpDataExtCountsWap;
        private System.Windows.Forms.DataGridView dgvExtractCountsWap;
        private System.Windows.Forms.TabPage tpDataExtCountsWao;
        private System.Windows.Forms.DataGridView dgvExtractCountsWao;
        private System.Windows.Forms.Button BtnTextExtractSpecial;
        private System.Windows.Forms.Button BtnCounts;
    }
}

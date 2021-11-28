namespace MyFirstProject.BW.AMZ
{
    partial class Uc_AMZ_TelComm
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ChkBxCamsFirst = new System.Windows.Forms.CheckBox();
            this.ChkBxStrtCamsOnNewSwitch = new System.Windows.Forms.CheckBox();
            this.BtnGenerateRowsForDropTable = new System.Windows.Forms.Button();
            this.BtnPortsTable = new System.Windows.Forms.Button();
            this.BtnGetDataFromTable = new System.Windows.Forms.Button();
            this.lblCameraCount = new System.Windows.Forms.Label();
            this.btnTelCommGet = new System.Windows.Forms.Button();
            this.btnCameraCount = new System.Windows.Forms.Button();
            this.btnZoomAndSelTelComm = new System.Windows.Forms.Button();
            this.dgvIdfAreas = new System.Windows.Forms.DataGridView();
            this.cmStripArea = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnZoomAndSelAllTelComm = new System.Windows.Forms.Button();
            this.btnGetNewArea = new System.Windows.Forms.Button();
            this.cBxAPCFilterTelComm = new System.Windows.Forms.ComboBox();
            this.btnSaveXlPullTelComm = new System.Windows.Forms.Button();
            this.btnRemoveFilterTelComm = new System.Windows.Forms.Button();
            this.btnInsertAutoCadTable = new System.Windows.Forms.Button();
            this.dgvTelComm = new System.Windows.Forms.DataGridView();
            this.BtnAddAtts = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIdfAreas)).BeginInit();
            this.cmStripArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTelComm)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.BtnAddAtts);
            this.panel1.Controls.Add(this.ChkBxCamsFirst);
            this.panel1.Controls.Add(this.ChkBxStrtCamsOnNewSwitch);
            this.panel1.Controls.Add(this.BtnGenerateRowsForDropTable);
            this.panel1.Controls.Add(this.BtnPortsTable);
            this.panel1.Controls.Add(this.BtnGetDataFromTable);
            this.panel1.Controls.Add(this.lblCameraCount);
            this.panel1.Controls.Add(this.btnTelCommGet);
            this.panel1.Controls.Add(this.btnCameraCount);
            this.panel1.Controls.Add(this.btnZoomAndSelTelComm);
            this.panel1.Controls.Add(this.dgvIdfAreas);
            this.panel1.Controls.Add(this.btnZoomAndSelAllTelComm);
            this.panel1.Controls.Add(this.btnGetNewArea);
            this.panel1.Controls.Add(this.cBxAPCFilterTelComm);
            this.panel1.Controls.Add(this.btnSaveXlPullTelComm);
            this.panel1.Controls.Add(this.btnRemoveFilterTelComm);
            this.panel1.Controls.Add(this.btnInsertAutoCadTable);
            this.panel1.Location = new System.Drawing.Point(789, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(304, 515);
            this.panel1.TabIndex = 0;
            // 
            // ChkBxCamsFirst
            // 
            this.ChkBxCamsFirst.AutoSize = true;
            this.ChkBxCamsFirst.Location = new System.Drawing.Point(183, 415);
            this.ChkBxCamsFirst.Name = "ChkBxCamsFirst";
            this.ChkBxCamsFirst.Size = new System.Drawing.Size(96, 21);
            this.ChkBxCamsFirst.TabIndex = 22;
            this.ChkBxCamsFirst.Text = "Cams First";
            this.ChkBxCamsFirst.UseVisualStyleBackColor = true;
            // 
            // ChkBxStrtCamsOnNewSwitch
            // 
            this.ChkBxStrtCamsOnNewSwitch.AutoSize = true;
            this.ChkBxStrtCamsOnNewSwitch.Checked = true;
            this.ChkBxStrtCamsOnNewSwitch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkBxStrtCamsOnNewSwitch.Location = new System.Drawing.Point(14, 415);
            this.ChkBxStrtCamsOnNewSwitch.Name = "ChkBxStrtCamsOnNewSwitch";
            this.ChkBxStrtCamsOnNewSwitch.Size = new System.Drawing.Size(155, 21);
            this.ChkBxStrtCamsOnNewSwitch.TabIndex = 21;
            this.ChkBxStrtCamsOnNewSwitch.Text = "Last On New Switch";
            this.ChkBxStrtCamsOnNewSwitch.UseVisualStyleBackColor = true;
            // 
            // BtnGenerateRowsForDropTable
            // 
            this.BtnGenerateRowsForDropTable.Location = new System.Drawing.Point(83, 442);
            this.BtnGenerateRowsForDropTable.Name = "BtnGenerateRowsForDropTable";
            this.BtnGenerateRowsForDropTable.Size = new System.Drawing.Size(63, 70);
            this.BtnGenerateRowsForDropTable.TabIndex = 20;
            this.BtnGenerateRowsForDropTable.Text = "Gen Rows Drops";
            this.BtnGenerateRowsForDropTable.UseVisualStyleBackColor = true;
            this.BtnGenerateRowsForDropTable.Click += new System.EventHandler(this.BtnGenerateRowsForDropTable_Click);
            // 
            // BtnPortsTable
            // 
            this.BtnPortsTable.Location = new System.Drawing.Point(152, 442);
            this.BtnPortsTable.Name = "BtnPortsTable";
            this.BtnPortsTable.Size = new System.Drawing.Size(63, 70);
            this.BtnPortsTable.TabIndex = 19;
            this.BtnPortsTable.Text = "Update Ports Table";
            this.BtnPortsTable.UseVisualStyleBackColor = true;
            this.BtnPortsTable.Click += new System.EventHandler(this.BtnPortsTable_Click);
            // 
            // BtnGetDataFromTable
            // 
            this.BtnGetDataFromTable.Location = new System.Drawing.Point(14, 442);
            this.BtnGetDataFromTable.Name = "BtnGetDataFromTable";
            this.BtnGetDataFromTable.Size = new System.Drawing.Size(63, 70);
            this.BtnGetDataFromTable.TabIndex = 18;
            this.BtnGetDataFromTable.Text = "Get Data From Table";
            this.BtnGetDataFromTable.UseVisualStyleBackColor = true;
            this.BtnGetDataFromTable.Click += new System.EventHandler(this.BtnGetDataFromTable_Click);
            // 
            // lblCameraCount
            // 
            this.lblCameraCount.AutoSize = true;
            this.lblCameraCount.Enabled = false;
            this.lblCameraCount.Location = new System.Drawing.Point(191, 387);
            this.lblCameraCount.Name = "lblCameraCount";
            this.lblCameraCount.Size = new System.Drawing.Size(98, 17);
            this.lblCameraCount.TabIndex = 17;
            this.lblCameraCount.Text = "Camera Count";
            this.lblCameraCount.Visible = false;
            // 
            // btnTelCommGet
            // 
            this.btnTelCommGet.Location = new System.Drawing.Point(14, 18);
            this.btnTelCommGet.Name = "btnTelCommGet";
            this.btnTelCommGet.Size = new System.Drawing.Size(75, 74);
            this.btnTelCommGet.TabIndex = 0;
            this.btnTelCommGet.Text = "Select Tel Comm";
            this.btnTelCommGet.UseVisualStyleBackColor = true;
            this.btnTelCommGet.Click += new System.EventHandler(this.BtnTelCommGet_Click);
            // 
            // btnCameraCount
            // 
            this.btnCameraCount.Enabled = false;
            this.btnCameraCount.Location = new System.Drawing.Point(194, 334);
            this.btnCameraCount.Name = "btnCameraCount";
            this.btnCameraCount.Size = new System.Drawing.Size(75, 49);
            this.btnCameraCount.TabIndex = 16;
            this.btnCameraCount.Text = "Camera Count";
            this.btnCameraCount.UseVisualStyleBackColor = true;
            this.btnCameraCount.Visible = false;
            // 
            // btnZoomAndSelTelComm
            // 
            this.btnZoomAndSelTelComm.Location = new System.Drawing.Point(14, 98);
            this.btnZoomAndSelTelComm.Name = "btnZoomAndSelTelComm";
            this.btnZoomAndSelTelComm.Size = new System.Drawing.Size(75, 74);
            this.btnZoomAndSelTelComm.TabIndex = 5;
            this.btnZoomAndSelTelComm.Text = "Zoom And Select";
            this.btnZoomAndSelTelComm.UseVisualStyleBackColor = true;
            this.btnZoomAndSelTelComm.Click += new System.EventHandler(this.BtnZoomAndSelTelComm_Click);
            // 
            // dgvIdfAreas
            // 
            this.dgvIdfAreas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIdfAreas.ContextMenuStrip = this.cmStripArea;
            this.dgvIdfAreas.Location = new System.Drawing.Point(95, 123);
            this.dgvIdfAreas.Name = "dgvIdfAreas";
            this.dgvIdfAreas.RowTemplate.Height = 24;
            this.dgvIdfAreas.Size = new System.Drawing.Size(194, 205);
            this.dgvIdfAreas.TabIndex = 15;
            // 
            // cmStripArea
            // 
            this.cmStripArea.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmStripArea.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteRowToolStripMenuItem,
            this.deleteAllToolStripMenuItem});
            this.cmStripArea.Name = "cmStripArea";
            this.cmStripArea.Size = new System.Drawing.Size(156, 52);
            // 
            // deleteRowToolStripMenuItem
            // 
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(155, 24);
            this.deleteRowToolStripMenuItem.Text = "Delete Row";
            this.deleteRowToolStripMenuItem.Click += new System.EventHandler(this.DeleteRowToolStripMenuItem_Click);
            // 
            // deleteAllToolStripMenuItem
            // 
            this.deleteAllToolStripMenuItem.Name = "deleteAllToolStripMenuItem";
            this.deleteAllToolStripMenuItem.Size = new System.Drawing.Size(155, 24);
            this.deleteAllToolStripMenuItem.Text = "Delete All";
            this.deleteAllToolStripMenuItem.Click += new System.EventHandler(this.DeleteAllToolStripMenuItem_Click);
            // 
            // btnZoomAndSelAllTelComm
            // 
            this.btnZoomAndSelAllTelComm.Location = new System.Drawing.Point(14, 178);
            this.btnZoomAndSelAllTelComm.Name = "btnZoomAndSelAllTelComm";
            this.btnZoomAndSelAllTelComm.Size = new System.Drawing.Size(75, 70);
            this.btnZoomAndSelAllTelComm.TabIndex = 9;
            this.btnZoomAndSelAllTelComm.Text = "Zoom And Select All";
            this.btnZoomAndSelAllTelComm.UseVisualStyleBackColor = true;
            this.btnZoomAndSelAllTelComm.Click += new System.EventHandler(this.BtnZoomAndSelAllTelComm_Click);
            // 
            // btnGetNewArea
            // 
            this.btnGetNewArea.Location = new System.Drawing.Point(194, 46);
            this.btnGetNewArea.Name = "btnGetNewArea";
            this.btnGetNewArea.Size = new System.Drawing.Size(75, 71);
            this.btnGetNewArea.TabIndex = 14;
            this.btnGetNewArea.Text = "Get New Area";
            this.btnGetNewArea.UseVisualStyleBackColor = true;
            this.btnGetNewArea.Click += new System.EventHandler(this.BtnGetNewArea_Click_1);
            // 
            // cBxAPCFilterTelComm
            // 
            this.cBxAPCFilterTelComm.FormattingEnabled = true;
            this.cBxAPCFilterTelComm.Location = new System.Drawing.Point(188, 13);
            this.cBxAPCFilterTelComm.Name = "cBxAPCFilterTelComm";
            this.cBxAPCFilterTelComm.Size = new System.Drawing.Size(81, 24);
            this.cBxAPCFilterTelComm.TabIndex = 10;
            // 
            // btnSaveXlPullTelComm
            // 
            this.btnSaveXlPullTelComm.Location = new System.Drawing.Point(14, 254);
            this.btnSaveXlPullTelComm.Name = "btnSaveXlPullTelComm";
            this.btnSaveXlPullTelComm.Size = new System.Drawing.Size(75, 74);
            this.btnSaveXlPullTelComm.TabIndex = 13;
            this.btnSaveXlPullTelComm.Text = "Save Xl Pull TelComm";
            this.btnSaveXlPullTelComm.UseVisualStyleBackColor = true;
            this.btnSaveXlPullTelComm.Click += new System.EventHandler(this.BtnSaveXlPullTelComm_Click);
            // 
            // btnRemoveFilterTelComm
            // 
            this.btnRemoveFilterTelComm.Location = new System.Drawing.Point(113, 46);
            this.btnRemoveFilterTelComm.Name = "btnRemoveFilterTelComm";
            this.btnRemoveFilterTelComm.Size = new System.Drawing.Size(75, 70);
            this.btnRemoveFilterTelComm.TabIndex = 11;
            this.btnRemoveFilterTelComm.Text = "Remove Filter";
            this.btnRemoveFilterTelComm.UseVisualStyleBackColor = true;
            this.btnRemoveFilterTelComm.Click += new System.EventHandler(this.BtnRemoveFilterTelComm_Click);
            // 
            // btnInsertAutoCadTable
            // 
            this.btnInsertAutoCadTable.Location = new System.Drawing.Point(14, 334);
            this.btnInsertAutoCadTable.Name = "btnInsertAutoCadTable";
            this.btnInsertAutoCadTable.Size = new System.Drawing.Size(75, 70);
            this.btnInsertAutoCadTable.TabIndex = 12;
            this.btnInsertAutoCadTable.Text = "Insert AutoCad Table";
            this.btnInsertAutoCadTable.UseVisualStyleBackColor = true;
            this.btnInsertAutoCadTable.Click += new System.EventHandler(this.BtnInsertAutoCadTable_Click);
            // 
            // dgvTelComm
            // 
            this.dgvTelComm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTelComm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTelComm.Location = new System.Drawing.Point(0, 3);
            this.dgvTelComm.Name = "dgvTelComm";
            this.dgvTelComm.RowTemplate.Height = 24;
            this.dgvTelComm.Size = new System.Drawing.Size(783, 515);
            this.dgvTelComm.TabIndex = 0;
            // 
            // BtnAddAtts
            // 
            this.BtnAddAtts.Location = new System.Drawing.Point(238, 442);
            this.BtnAddAtts.Name = "BtnAddAtts";
            this.BtnAddAtts.Size = new System.Drawing.Size(63, 70);
            this.BtnAddAtts.TabIndex = 23;
            this.BtnAddAtts.Text = "Add Atts";
            this.BtnAddAtts.UseVisualStyleBackColor = true;
            this.BtnAddAtts.Click += new System.EventHandler(this.BtnAddAtts_Click);
            // 
            // Uc_AMZ_TelComm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvTelComm);
            this.Controls.Add(this.panel1);
            this.Name = "Uc_AMZ_TelComm";
            this.Size = new System.Drawing.Size(1096, 521);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIdfAreas)).EndInit();
            this.cmStripArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTelComm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCameraCount;
        private System.Windows.Forms.Button btnTelCommGet;
        private System.Windows.Forms.Button btnCameraCount;
        private System.Windows.Forms.Button btnZoomAndSelTelComm;
        private System.Windows.Forms.DataGridView dgvIdfAreas;
        private System.Windows.Forms.ContextMenuStrip cmStripArea;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllToolStripMenuItem;
        private System.Windows.Forms.Button btnZoomAndSelAllTelComm;
        private System.Windows.Forms.Button btnGetNewArea;
        private System.Windows.Forms.ComboBox cBxAPCFilterTelComm;
        private System.Windows.Forms.Button btnSaveXlPullTelComm;
        private System.Windows.Forms.Button btnRemoveFilterTelComm;
        private System.Windows.Forms.Button btnInsertAutoCadTable;
        private System.Windows.Forms.DataGridView dgvTelComm;
        private System.Windows.Forms.Button BtnGetDataFromTable;
        private System.Windows.Forms.Button BtnPortsTable;
        private System.Windows.Forms.Button BtnGenerateRowsForDropTable;
        private System.Windows.Forms.CheckBox ChkBxStrtCamsOnNewSwitch;
        private System.Windows.Forms.CheckBox ChkBxCamsFirst;
        private System.Windows.Forms.Button BtnAddAtts;
    }
}

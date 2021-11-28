namespace MyFirstProject.BW.AMZ
{
    partial class Frm_AMZ_MainForm
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
            this.components = new System.ComponentModel.Container();
            this.tbCtlMain = new System.Windows.Forms.TabControl();
            this.tpSecurity = new System.Windows.Forms.TabPage();
            this.tpSecurityCounts = new System.Windows.Forms.TabPage();
            this.dgvCounts = new System.Windows.Forms.DataGridView();
            this.tpTelComm = new System.Windows.Forms.TabPage();
            this.tpTelCommCounts = new System.Windows.Forms.TabPage();
            this.dgvTelCommCounts = new System.Windows.Forms.DataGridView();
            this.cmStripArea = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbCtlMain.SuspendLayout();
            this.tpSecurityCounts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCounts)).BeginInit();
            this.tpTelCommCounts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTelCommCounts)).BeginInit();
            this.cmStripArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbCtlMain
            // 
            this.tbCtlMain.Controls.Add(this.tpSecurity);
            this.tbCtlMain.Controls.Add(this.tpSecurityCounts);
            this.tbCtlMain.Controls.Add(this.tpTelComm);
            this.tbCtlMain.Controls.Add(this.tpTelCommCounts);
            this.tbCtlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCtlMain.Location = new System.Drawing.Point(0, 0);
            this.tbCtlMain.Margin = new System.Windows.Forms.Padding(2);
            this.tbCtlMain.Name = "tbCtlMain";
            this.tbCtlMain.SelectedIndex = 0;
            this.tbCtlMain.Size = new System.Drawing.Size(780, 563);
            this.tbCtlMain.TabIndex = 1;
            // 
            // tpSecurity
            // 
            this.tpSecurity.Location = new System.Drawing.Point(4, 22);
            this.tpSecurity.Margin = new System.Windows.Forms.Padding(2);
            this.tpSecurity.Name = "tpSecurity";
            this.tpSecurity.Padding = new System.Windows.Forms.Padding(2);
            this.tpSecurity.Size = new System.Drawing.Size(772, 537);
            this.tpSecurity.TabIndex = 0;
            this.tpSecurity.Text = "Security";
            this.tpSecurity.UseVisualStyleBackColor = true;
            // 
            // tpSecurityCounts
            // 
            this.tpSecurityCounts.Controls.Add(this.dgvCounts);
            this.tpSecurityCounts.Location = new System.Drawing.Point(4, 22);
            this.tpSecurityCounts.Margin = new System.Windows.Forms.Padding(2);
            this.tpSecurityCounts.Name = "tpSecurityCounts";
            this.tpSecurityCounts.Padding = new System.Windows.Forms.Padding(2);
            this.tpSecurityCounts.Size = new System.Drawing.Size(772, 537);
            this.tpSecurityCounts.TabIndex = 1;
            this.tpSecurityCounts.Text = "SecurityCounts";
            this.tpSecurityCounts.UseVisualStyleBackColor = true;
            // 
            // dgvCounts
            // 
            this.dgvCounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCounts.Location = new System.Drawing.Point(2, 2);
            this.dgvCounts.Margin = new System.Windows.Forms.Padding(2);
            this.dgvCounts.Name = "dgvCounts";
            this.dgvCounts.RowTemplate.Height = 24;
            this.dgvCounts.Size = new System.Drawing.Size(768, 533);
            this.dgvCounts.TabIndex = 0;
            // 
            // tpTelComm
            // 
            this.tpTelComm.Location = new System.Drawing.Point(4, 22);
            this.tpTelComm.Margin = new System.Windows.Forms.Padding(2);
            this.tpTelComm.Name = "tpTelComm";
            this.tpTelComm.Size = new System.Drawing.Size(772, 537);
            this.tpTelComm.TabIndex = 2;
            this.tpTelComm.Text = "TelComm";
            this.tpTelComm.UseVisualStyleBackColor = true;
            // 
            // tpTelCommCounts
            // 
            this.tpTelCommCounts.Controls.Add(this.dgvTelCommCounts);
            this.tpTelCommCounts.Location = new System.Drawing.Point(4, 22);
            this.tpTelCommCounts.Margin = new System.Windows.Forms.Padding(2);
            this.tpTelCommCounts.Name = "tpTelCommCounts";
            this.tpTelCommCounts.Size = new System.Drawing.Size(772, 537);
            this.tpTelCommCounts.TabIndex = 3;
            this.tpTelCommCounts.Text = "TelCommCounts";
            this.tpTelCommCounts.UseVisualStyleBackColor = true;
            // 
            // dgvTelCommCounts
            // 
            this.dgvTelCommCounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTelCommCounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTelCommCounts.Location = new System.Drawing.Point(0, 0);
            this.dgvTelCommCounts.Margin = new System.Windows.Forms.Padding(2);
            this.dgvTelCommCounts.Name = "dgvTelCommCounts";
            this.dgvTelCommCounts.RowTemplate.Height = 24;
            this.dgvTelCommCounts.Size = new System.Drawing.Size(772, 537);
            this.dgvTelCommCounts.TabIndex = 0;
            // 
            // cmStripArea
            // 
            this.cmStripArea.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmStripArea.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteRowToolStripMenuItem,
            this.deleteAllToolStripMenuItem});
            this.cmStripArea.Name = "cmStripArea";
            this.cmStripArea.Size = new System.Drawing.Size(134, 48);
            // 
            // deleteRowToolStripMenuItem
            // 
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.deleteRowToolStripMenuItem.Text = "Delete Row";
            // 
            // deleteAllToolStripMenuItem
            // 
            this.deleteAllToolStripMenuItem.Name = "deleteAllToolStripMenuItem";
            this.deleteAllToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.deleteAllToolStripMenuItem.Text = "Delete All";
            // 
            // Frm_AMZ_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 563);
            this.Controls.Add(this.tbCtlMain);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Frm_AMZ_MainForm";
            this.Text = "frmAMZMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAMZMain_FormClosing);
            this.Load += new System.EventHandler(this.Frm_AMZ_MainForm_Load);
            this.tbCtlMain.ResumeLayout(false);
            this.tpSecurityCounts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCounts)).EndInit();
            this.tpTelCommCounts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTelCommCounts)).EndInit();
            this.cmStripArea.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tbCtlMain;
        private System.Windows.Forms.TabPage tpSecurity;
        private System.Windows.Forms.TabPage tpSecurityCounts;
        private System.Windows.Forms.DataGridView dgvCounts;
        private System.Windows.Forms.TabPage tpTelComm;
        private System.Windows.Forms.TabPage tpTelCommCounts;
        private System.Windows.Forms.DataGridView dgvTelCommCounts;
        private System.Windows.Forms.ContextMenuStrip cmStripArea;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllToolStripMenuItem;
    }
}
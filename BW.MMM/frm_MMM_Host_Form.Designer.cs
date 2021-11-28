namespace MyFirstProject.BW.MMM
{
    partial class Frm_MMM_Host_Form
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bOMFilenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.genBOMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bOMFilenameToolStripMenuItem,
            this.genBOMToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
            // 
            // bOMFilenameToolStripMenuItem
            // 
            this.bOMFilenameToolStripMenuItem.Name = "bOMFilenameToolStripMenuItem";
            this.bOMFilenameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.bOMFilenameToolStripMenuItem.Text = "BOM Filename";
            this.bOMFilenameToolStripMenuItem.Click += new System.EventHandler(this.BOMFilenameToolStripMenuItem_Click);
            // 
            // genBOMToolStripMenuItem
            // 
            this.genBOMToolStripMenuItem.Name = "genBOMToolStripMenuItem";
            this.genBOMToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.genBOMToolStripMenuItem.Text = "Gen BOM";
            this.genBOMToolStripMenuItem.Click += new System.EventHandler(this.GenBOMToolStripMenuItem_Click);
            // 
            // Frm_MMM_Host_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 584);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Frm_MMM_Host_Form";
            this.Text = "3M Host Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_MMM_Host_Form_FormClosing);
            this.Load += new System.EventHandler(this.Frm_MMM_Host_Form_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem bOMFilenameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genBOMToolStripMenuItem;
    }
}
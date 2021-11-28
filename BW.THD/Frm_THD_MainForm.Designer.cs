namespace MyFirstProject.BW.THD
{
    partial class Frm_THD_MainForm
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
            this.dgvDevTags = new System.Windows.Forms.DataGridView();
            this.BtnSelectBlocks = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSaveDataToFile = new System.Windows.Forms.Button();
            this.dgvNewLst = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevTags)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNewLst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDevTags
            // 
            this.dgvDevTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDevTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDevTags.Location = new System.Drawing.Point(0, 0);
            this.dgvDevTags.Margin = new System.Windows.Forms.Padding(2);
            this.dgvDevTags.Name = "dgvDevTags";
            this.dgvDevTags.RowTemplate.Height = 24;
            this.dgvDevTags.Size = new System.Drawing.Size(691, 249);
            this.dgvDevTags.TabIndex = 3;
            // 
            // BtnSelectBlocks
            // 
            this.BtnSelectBlocks.Location = new System.Drawing.Point(32, 24);
            this.BtnSelectBlocks.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSelectBlocks.Name = "BtnSelectBlocks";
            this.BtnSelectBlocks.Size = new System.Drawing.Size(83, 45);
            this.BtnSelectBlocks.TabIndex = 4;
            this.BtnSelectBlocks.Text = "Select Blocks";
            this.BtnSelectBlocks.UseVisualStyleBackColor = true;
            this.BtnSelectBlocks.Click += new System.EventHandler(this.BtnSelectBlocks_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSaveDataToFile);
            this.panel1.Controls.Add(this.BtnSelectBlocks);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(691, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(147, 641);
            this.panel1.TabIndex = 5;
            // 
            // btnSaveDataToFile
            // 
            this.btnSaveDataToFile.Location = new System.Drawing.Point(32, 88);
            this.btnSaveDataToFile.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveDataToFile.Name = "btnSaveDataToFile";
            this.btnSaveDataToFile.Size = new System.Drawing.Size(83, 60);
            this.btnSaveDataToFile.TabIndex = 5;
            this.btnSaveDataToFile.Text = "Save Data To File (XL Pull)";
            this.btnSaveDataToFile.UseVisualStyleBackColor = true;
            this.btnSaveDataToFile.Click += new System.EventHandler(this.btnSaveDataToFile_Click);
            // 
            // dgvNewLst
            // 
            this.dgvNewLst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNewLst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNewLst.Location = new System.Drawing.Point(0, 0);
            this.dgvNewLst.Margin = new System.Windows.Forms.Padding(2);
            this.dgvNewLst.Name = "dgvNewLst";
            this.dgvNewLst.RowTemplate.Height = 24;
            this.dgvNewLst.Size = new System.Drawing.Size(691, 389);
            this.dgvNewLst.TabIndex = 6;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvDevTags);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvNewLst);
            this.splitContainer1.Size = new System.Drawing.Size(691, 641);
            this.splitContainer1.SplitterDistance = 249;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 6;
            // 
            // Frm_THD_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 641);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Frm_THD_MainForm";
            this.Text = "Frm THD MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_THD_MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Frm_THD_MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevTags)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNewLst)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDevTags;
        private System.Windows.Forms.Button BtnSelectBlocks;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvNewLst;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnSaveDataToFile;
    }
}
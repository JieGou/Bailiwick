namespace MyFirstProject.BW
{
    partial class Uc_BW_Database
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvTestTable = new System.Windows.Forms.DataGridView();
            this.btnAddNewDatabaseRecord = new System.Windows.Forms.Button();
            this.txtBxPdf = new System.Windows.Forms.TextBox();
            this.txtBxCadEng = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestTable)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(309, 154);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(113, 54);
            this.btnRefresh.TabIndex = 17;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // dgvTestTable
            // 
            this.dgvTestTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTestTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTestTable.Location = new System.Drawing.Point(0, 213);
            this.dgvTestTable.Name = "dgvTestTable";
            this.dgvTestTable.RowTemplate.Height = 24;
            this.dgvTestTable.Size = new System.Drawing.Size(968, 387);
            this.dgvTestTable.TabIndex = 16;
            // 
            // btnAddNewDatabaseRecord
            // 
            this.btnAddNewDatabaseRecord.Location = new System.Drawing.Point(53, 58);
            this.btnAddNewDatabaseRecord.Name = "btnAddNewDatabaseRecord";
            this.btnAddNewDatabaseRecord.Size = new System.Drawing.Size(98, 66);
            this.btnAddNewDatabaseRecord.TabIndex = 15;
            this.btnAddNewDatabaseRecord.Text = "Add New DB Record";
            this.btnAddNewDatabaseRecord.UseVisualStyleBackColor = true;
            this.btnAddNewDatabaseRecord.Click += new System.EventHandler(this.BtnAddNewDatabaseRecord_Click);
            // 
            // txtBxPdf
            // 
            this.txtBxPdf.Location = new System.Drawing.Point(53, 175);
            this.txtBxPdf.Name = "txtBxPdf";
            this.txtBxPdf.Size = new System.Drawing.Size(98, 22);
            this.txtBxPdf.TabIndex = 14;
            this.txtBxPdf.Text = "PDF";
            // 
            // txtBxCadEng
            // 
            this.txtBxCadEng.Location = new System.Drawing.Point(53, 147);
            this.txtBxCadEng.Name = "txtBxCadEng";
            this.txtBxCadEng.Size = new System.Drawing.Size(98, 22);
            this.txtBxCadEng.TabIndex = 13;
            this.txtBxCadEng.Text = "CadEng";
            // 
            // Uc_BW_Database
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvTestTable);
            this.Controls.Add(this.btnAddNewDatabaseRecord);
            this.Controls.Add(this.txtBxPdf);
            this.Controls.Add(this.txtBxCadEng);
            this.Name = "Uc_BW_Database";
            this.Size = new System.Drawing.Size(975, 609);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvTestTable;
        private System.Windows.Forms.Button btnAddNewDatabaseRecord;
        private System.Windows.Forms.TextBox txtBxPdf;
        private System.Windows.Forms.TextBox txtBxCadEng;
    }
}

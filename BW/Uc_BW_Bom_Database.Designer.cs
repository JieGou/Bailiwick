namespace MyFirstProject.BW
{
    partial class Uc_BW_Bom_Database
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvBoM = new System.Windows.Forms.DataGridView();
            this.btnAddBomItem = new System.Windows.Forms.Button();
            this.btnRefreshBomTable = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoM)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.TabIndex = 19;
            this.label1.Text = "Testing";
            // 
            // dgvBoM
            // 
            this.dgvBoM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBoM.Location = new System.Drawing.Point(19, 119);
            this.dgvBoM.Name = "dgvBoM";
            this.dgvBoM.RowTemplate.Height = 24;
            this.dgvBoM.Size = new System.Drawing.Size(820, 351);
            this.dgvBoM.TabIndex = 18;
            // 
            // btnAddBomItem
            // 
            this.btnAddBomItem.Location = new System.Drawing.Point(19, 49);
            this.btnAddBomItem.Name = "btnAddBomItem";
            this.btnAddBomItem.Size = new System.Drawing.Size(75, 54);
            this.btnAddBomItem.TabIndex = 17;
            this.btnAddBomItem.Text = "Add Bom Item";
            this.btnAddBomItem.UseVisualStyleBackColor = true;
            this.btnAddBomItem.Click += new System.EventHandler(this.BtnAddBomItem_Click);
            // 
            // btnRefreshBomTable
            // 
            this.btnRefreshBomTable.Location = new System.Drawing.Point(100, 49);
            this.btnRefreshBomTable.Name = "btnRefreshBomTable";
            this.btnRefreshBomTable.Size = new System.Drawing.Size(113, 54);
            this.btnRefreshBomTable.TabIndex = 16;
            this.btnRefreshBomTable.Text = "Refresh";
            this.btnRefreshBomTable.UseVisualStyleBackColor = true;
            this.btnRefreshBomTable.Click += new System.EventHandler(this.BtnRefreshBomTable_Click);
            // 
            // Uc_BW_Bom_Database
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvBoM);
            this.Controls.Add(this.btnAddBomItem);
            this.Controls.Add(this.btnRefreshBomTable);
            this.Name = "Uc_BW_Bom_Database";
            this.Size = new System.Drawing.Size(1020, 595);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvBoM;
        private System.Windows.Forms.Button btnAddBomItem;
        private System.Windows.Forms.Button btnRefreshBomTable;
    }
}

namespace MyFirstProject.BW
{
    partial class Uc_BW_TP_WAOs
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
            this.dgvWaos = new System.Windows.Forms.DataGridView();
            this.btnGetApsAndWaos = new System.Windows.Forms.Button();
            this.btnLabelWAOsIndividually = new System.Windows.Forms.Button();
            this.cBxWaoClosetFilter = new System.Windows.Forms.ComboBox();
            this.btnClearWaoFilter = new System.Windows.Forms.Button();
            this.txtBxWaoAutoNum_NumToStartAt = new System.Windows.Forms.TextBox();
            this.btnAutoLabelWAOs = new System.Windows.Forms.Button();
            this.chkBxSelectWAO = new System.Windows.Forms.CheckBox();
            this.btnWAOsZoomAndSelect = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnLabelAllSelectedWAOsInOrder = new System.Windows.Forms.Button();
            this.BtnPutXXXsInWaos = new System.Windows.Forms.Button();
            this.BtnAutoLabelWaosByCloset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWaos)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvWaos
            // 
            this.dgvWaos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWaos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWaos.Location = new System.Drawing.Point(0, 0);
            this.dgvWaos.MultiSelect = false;
            this.dgvWaos.Name = "dgvWaos";
            this.dgvWaos.RowTemplate.Height = 24;
            this.dgvWaos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWaos.Size = new System.Drawing.Size(657, 533);
            this.dgvWaos.TabIndex = 0;
            // 
            // btnGetApsAndWaos
            // 
            this.btnGetApsAndWaos.Location = new System.Drawing.Point(10, 73);
            this.btnGetApsAndWaos.Name = "btnGetApsAndWaos";
            this.btnGetApsAndWaos.Size = new System.Drawing.Size(123, 48);
            this.btnGetApsAndWaos.TabIndex = 12;
            this.btnGetApsAndWaos.Text = "Get APs + WAOs";
            this.btnGetApsAndWaos.UseVisualStyleBackColor = true;
            this.btnGetApsAndWaos.Click += new System.EventHandler(this.BtnGetApsAndWaos_Click);
            // 
            // btnLabelWAOsIndividually
            // 
            this.btnLabelWAOsIndividually.Location = new System.Drawing.Point(137, 136);
            this.btnLabelWAOsIndividually.Name = "btnLabelWAOsIndividually";
            this.btnLabelWAOsIndividually.Size = new System.Drawing.Size(106, 52);
            this.btnLabelWAOsIndividually.TabIndex = 10;
            this.btnLabelWAOsIndividually.Text = "Label WAOs Individually";
            this.btnLabelWAOsIndividually.UseVisualStyleBackColor = true;
            this.btnLabelWAOsIndividually.Click += new System.EventHandler(this.BtnLabelWAOsIndividually_Click);
            // 
            // cBxWaoClosetFilter
            // 
            this.cBxWaoClosetFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBxWaoClosetFilter.FormattingEnabled = true;
            this.cBxWaoClosetFilter.Location = new System.Drawing.Point(109, 261);
            this.cBxWaoClosetFilter.Name = "cBxWaoClosetFilter";
            this.cBxWaoClosetFilter.Size = new System.Drawing.Size(121, 28);
            this.cBxWaoClosetFilter.TabIndex = 9;
            // 
            // btnClearWaoFilter
            // 
            this.btnClearWaoFilter.Location = new System.Drawing.Point(10, 261);
            this.btnClearWaoFilter.Name = "btnClearWaoFilter";
            this.btnClearWaoFilter.Size = new System.Drawing.Size(93, 29);
            this.btnClearWaoFilter.TabIndex = 6;
            this.btnClearWaoFilter.Text = "Clear Filter";
            this.btnClearWaoFilter.UseVisualStyleBackColor = true;
            this.btnClearWaoFilter.Click += new System.EventHandler(this.BtnClearWaoFilter_Click);
            // 
            // txtBxWaoAutoNum_NumToStartAt
            // 
            this.txtBxWaoAutoNum_NumToStartAt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxWaoAutoNum_NumToStartAt.Location = new System.Drawing.Point(10, 194);
            this.txtBxWaoAutoNum_NumToStartAt.Name = "txtBxWaoAutoNum_NumToStartAt";
            this.txtBxWaoAutoNum_NumToStartAt.Size = new System.Drawing.Size(123, 27);
            this.txtBxWaoAutoNum_NumToStartAt.TabIndex = 4;
            this.txtBxWaoAutoNum_NumToStartAt.Text = "1";
            // 
            // btnAutoLabelWAOs
            // 
            this.btnAutoLabelWAOs.Location = new System.Drawing.Point(10, 136);
            this.btnAutoLabelWAOs.Name = "btnAutoLabelWAOs";
            this.btnAutoLabelWAOs.Size = new System.Drawing.Size(123, 52);
            this.btnAutoLabelWAOs.TabIndex = 3;
            this.btnAutoLabelWAOs.Text = "Auto Label WAOs";
            this.btnAutoLabelWAOs.UseVisualStyleBackColor = true;
            this.btnAutoLabelWAOs.Click += new System.EventHandler(this.BtnAutoLabelWAOs_Click);
            // 
            // chkBxSelectWAO
            // 
            this.chkBxSelectWAO.AutoSize = true;
            this.chkBxSelectWAO.Checked = true;
            this.chkBxSelectWAO.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxSelectWAO.Location = new System.Drawing.Point(137, 27);
            this.chkBxSelectWAO.Name = "chkBxSelectWAO";
            this.chkBxSelectWAO.Size = new System.Drawing.Size(106, 21);
            this.chkBxSelectWAO.TabIndex = 2;
            this.chkBxSelectWAO.Text = "Select WAO";
            this.chkBxSelectWAO.UseVisualStyleBackColor = true;
            // 
            // btnWAOsZoomAndSelect
            // 
            this.btnWAOsZoomAndSelect.Location = new System.Drawing.Point(10, 11);
            this.btnWAOsZoomAndSelect.Name = "btnWAOsZoomAndSelect";
            this.btnWAOsZoomAndSelect.Size = new System.Drawing.Size(123, 51);
            this.btnWAOsZoomAndSelect.TabIndex = 1;
            this.btnWAOsZoomAndSelect.Text = "WAOs Zoom And Select";
            this.btnWAOsZoomAndSelect.UseVisualStyleBackColor = true;
            this.btnWAOsZoomAndSelect.Click += new System.EventHandler(this.BtnWAOsZoomAndSelect_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnAutoLabelWaosByCloset);
            this.panel1.Controls.Add(this.BtnLabelAllSelectedWAOsInOrder);
            this.panel1.Controls.Add(this.BtnPutXXXsInWaos);
            this.panel1.Controls.Add(this.btnGetApsAndWaos);
            this.panel1.Controls.Add(this.btnWAOsZoomAndSelect);
            this.panel1.Controls.Add(this.btnLabelWAOsIndividually);
            this.panel1.Controls.Add(this.chkBxSelectWAO);
            this.panel1.Controls.Add(this.cBxWaoClosetFilter);
            this.panel1.Controls.Add(this.btnAutoLabelWAOs);
            this.panel1.Controls.Add(this.txtBxWaoAutoNum_NumToStartAt);
            this.panel1.Controls.Add(this.btnClearWaoFilter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(657, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 533);
            this.panel1.TabIndex = 1;
            // 
            // BtnLabelAllSelectedWAOsInOrder
            // 
            this.BtnLabelAllSelectedWAOsInOrder.Location = new System.Drawing.Point(139, 418);
            this.BtnLabelAllSelectedWAOsInOrder.Name = "BtnLabelAllSelectedWAOsInOrder";
            this.BtnLabelAllSelectedWAOsInOrder.Size = new System.Drawing.Size(105, 90);
            this.BtnLabelAllSelectedWAOsInOrder.TabIndex = 14;
            this.BtnLabelAllSelectedWAOsInOrder.Text = "Label All Selected WAOs In Order";
            this.BtnLabelAllSelectedWAOsInOrder.UseVisualStyleBackColor = true;
            this.BtnLabelAllSelectedWAOsInOrder.Click += new System.EventHandler(this.BtnLabelAllSelectedWAOsInOrder_Click);
            // 
            // BtnPutXXXsInWaos
            // 
            this.BtnPutXXXsInWaos.Location = new System.Drawing.Point(28, 458);
            this.BtnPutXXXsInWaos.Name = "BtnPutXXXsInWaos";
            this.BtnPutXXXsInWaos.Size = new System.Drawing.Size(105, 50);
            this.BtnPutXXXsInWaos.TabIndex = 13;
            this.BtnPutXXXsInWaos.Text = "Put XXX\'s In WAOs";
            this.BtnPutXXXsInWaos.UseVisualStyleBackColor = true;
            this.BtnPutXXXsInWaos.Click += new System.EventHandler(this.BtnPutXXXsInWaos_Click);
            // 
            // BtnAutoLabelWaosByCloset
            // 
            this.BtnAutoLabelWaosByCloset.Location = new System.Drawing.Point(10, 368);
            this.BtnAutoLabelWaosByCloset.Name = "BtnAutoLabelWaosByCloset";
            this.BtnAutoLabelWaosByCloset.Size = new System.Drawing.Size(233, 44);
            this.BtnAutoLabelWaosByCloset.TabIndex = 15;
            this.BtnAutoLabelWaosByCloset.Text = "Auto Label Waos By Closet";
            this.BtnAutoLabelWaosByCloset.UseVisualStyleBackColor = true;
            this.BtnAutoLabelWaosByCloset.Click += new System.EventHandler(this.BtnAutoLabelWaosByCloset_Click);
            // 
            // Uc_BW_TP_WAOs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvWaos);
            this.Controls.Add(this.panel1);
            this.Name = "Uc_BW_TP_WAOs";
            this.Size = new System.Drawing.Size(914, 533);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWaos)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGetApsAndWaos;
        private System.Windows.Forms.Button btnLabelWAOsIndividually;
        private System.Windows.Forms.Button btnClearWaoFilter;
        private System.Windows.Forms.TextBox txtBxWaoAutoNum_NumToStartAt;
        private System.Windows.Forms.Button btnAutoLabelWAOs;
        private System.Windows.Forms.CheckBox chkBxSelectWAO;
        private System.Windows.Forms.Button btnWAOsZoomAndSelect;
        public System.Windows.Forms.DataGridView dgvWaos;
        public System.Windows.Forms.ComboBox cBxWaoClosetFilter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnPutXXXsInWaos;
        private System.Windows.Forms.Button BtnLabelAllSelectedWAOsInOrder;
        private System.Windows.Forms.Button BtnAutoLabelWaosByCloset;
    }
}

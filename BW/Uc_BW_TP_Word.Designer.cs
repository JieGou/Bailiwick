namespace MyFirstProject.BW
{
    partial class Uc_BW_TP_Word
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
            this.btnWord = new System.Windows.Forms.Button();
            this.DgvPiList = new System.Windows.Forms.DataGridView();
            this.BtnGetPiList = new System.Windows.Forms.Button();
            this.BtnSavePiToXL = new System.Windows.Forms.Button();
            this.BtnGetFolder = new System.Windows.Forms.Button();
            this.LblFolder = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DgvPiList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnWord
            // 
            this.btnWord.Location = new System.Drawing.Point(4, 389);
            this.btnWord.Name = "btnWord";
            this.btnWord.Size = new System.Drawing.Size(124, 65);
            this.btnWord.TabIndex = 2;
            this.btnWord.Text = "Word Insert Paragraphs";
            this.btnWord.UseVisualStyleBackColor = true;
            this.btnWord.Click += new System.EventHandler(this.BtnWord_Click);
            // 
            // DgvPiList
            // 
            this.DgvPiList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvPiList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DgvPiList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DgvPiList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvPiList.Location = new System.Drawing.Point(133, 61);
            this.DgvPiList.Name = "DgvPiList";
            this.DgvPiList.RowTemplate.Height = 24;
            this.DgvPiList.Size = new System.Drawing.Size(517, 327);
            this.DgvPiList.TabIndex = 5;
            // 
            // BtnGetPiList
            // 
            this.BtnGetPiList.Location = new System.Drawing.Point(5, 61);
            this.BtnGetPiList.Name = "BtnGetPiList";
            this.BtnGetPiList.Size = new System.Drawing.Size(123, 52);
            this.BtnGetPiList.TabIndex = 6;
            this.BtnGetPiList.Text = "Get Pi List";
            this.BtnGetPiList.UseVisualStyleBackColor = true;
            this.BtnGetPiList.Click += new System.EventHandler(this.BtnGetPiList_Click);
            // 
            // BtnSavePiToXL
            // 
            this.BtnSavePiToXL.Location = new System.Drawing.Point(5, 119);
            this.BtnSavePiToXL.Name = "BtnSavePiToXL";
            this.BtnSavePiToXL.Size = new System.Drawing.Size(123, 47);
            this.BtnSavePiToXL.TabIndex = 7;
            this.BtnSavePiToXL.Text = "Save Pi To XL";
            this.BtnSavePiToXL.UseVisualStyleBackColor = true;
            this.BtnSavePiToXL.Click += new System.EventHandler(this.BtnSavePiToXL_Click);
            // 
            // BtnGetFolder
            // 
            this.BtnGetFolder.Location = new System.Drawing.Point(5, 3);
            this.BtnGetFolder.Name = "BtnGetFolder";
            this.BtnGetFolder.Size = new System.Drawing.Size(123, 46);
            this.BtnGetFolder.TabIndex = 8;
            this.BtnGetFolder.Text = "Get Folder";
            this.BtnGetFolder.UseVisualStyleBackColor = true;
            this.BtnGetFolder.Click += new System.EventHandler(this.BtnGetFolder_Click);
            // 
            // LblFolder
            // 
            this.LblFolder.AutoSize = true;
            this.LblFolder.Location = new System.Drawing.Point(134, 18);
            this.LblFolder.Name = "LblFolder";
            this.LblFolder.Size = new System.Drawing.Size(67, 17);
            this.LblFolder.TabIndex = 9;
            this.LblFolder.Text = "LblFolder";
            // 
            // Uc_BW_TP_Word
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LblFolder);
            this.Controls.Add(this.BtnGetFolder);
            this.Controls.Add(this.BtnSavePiToXL);
            this.Controls.Add(this.BtnGetPiList);
            this.Controls.Add(this.DgvPiList);
            this.Controls.Add(this.btnWord);
            this.Name = "Uc_BW_TP_Word";
            this.Size = new System.Drawing.Size(653, 457);
            ((System.ComponentModel.ISupportInitialize)(this.DgvPiList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnWord;
        private System.Windows.Forms.DataGridView DgvPiList;
        private System.Windows.Forms.Button BtnGetPiList;
        private System.Windows.Forms.Button BtnSavePiToXL;
        private System.Windows.Forms.Button BtnGetFolder;
        private System.Windows.Forms.Label LblFolder;
    }
}

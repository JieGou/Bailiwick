namespace MyFirstProject.BW
{
    partial class Uc_BW_TP_IBWave
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
            this.BtnReadIBWaveXml = new System.Windows.Forms.Button();
            this.DgvReport = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DgvReport)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnReadIBWaveXml
            // 
            this.BtnReadIBWaveXml.Location = new System.Drawing.Point(28, 110);
            this.BtnReadIBWaveXml.Name = "BtnReadIBWaveXml";
            this.BtnReadIBWaveXml.Size = new System.Drawing.Size(75, 45);
            this.BtnReadIBWaveXml.TabIndex = 0;
            this.BtnReadIBWaveXml.Text = "Read IBWave Xml";
            this.BtnReadIBWaveXml.UseVisualStyleBackColor = true;
            this.BtnReadIBWaveXml.Click += new System.EventHandler(this.BtnReadIBWaveXml_Click);
            // 
            // DgvReport
            // 
            this.DgvReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvReport.Location = new System.Drawing.Point(115, 24);
            this.DgvReport.Name = "DgvReport";
            this.DgvReport.Size = new System.Drawing.Size(469, 331);
            this.DgvReport.TabIndex = 1;
            // 
            // Uc_BW_TP_IBWave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DgvReport);
            this.Controls.Add(this.BtnReadIBWaveXml);
            this.Name = "Uc_BW_TP_IBWave";
            this.Size = new System.Drawing.Size(587, 358);
            ((System.ComponentModel.ISupportInitialize)(this.DgvReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnReadIBWaveXml;
        private System.Windows.Forms.DataGridView DgvReport;
    }
}

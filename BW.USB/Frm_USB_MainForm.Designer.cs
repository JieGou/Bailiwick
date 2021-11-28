namespace MyFirstProject.BW.USB
{
    partial class Frm_USB_MainForm
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
            this.BtnNumberWAOs = new System.Windows.Forms.Button();
            this.TxtBxWAOsNumber = new System.Windows.Forms.TextBox();
            this.chkBxAB = new System.Windows.Forms.CheckBox();
            this.BtnSetAttWidth = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnNumberWAOs
            // 
            this.BtnNumberWAOs.Location = new System.Drawing.Point(281, 110);
            this.BtnNumberWAOs.Name = "BtnNumberWAOs";
            this.BtnNumberWAOs.Size = new System.Drawing.Size(89, 41);
            this.BtnNumberWAOs.TabIndex = 0;
            this.BtnNumberWAOs.Text = "Number WAOs";
            this.BtnNumberWAOs.UseVisualStyleBackColor = true;
            this.BtnNumberWAOs.Click += new System.EventHandler(this.BtnNumberWAOs_Click);
            // 
            // TxtBxWAOsNumber
            // 
            this.TxtBxWAOsNumber.Location = new System.Drawing.Point(281, 169);
            this.TxtBxWAOsNumber.Name = "TxtBxWAOsNumber";
            this.TxtBxWAOsNumber.Size = new System.Drawing.Size(100, 20);
            this.TxtBxWAOsNumber.TabIndex = 1;
            this.TxtBxWAOsNumber.Text = "1";
            // 
            // chkBxAB
            // 
            this.chkBxAB.AutoSize = true;
            this.chkBxAB.Checked = true;
            this.chkBxAB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxAB.Location = new System.Drawing.Point(291, 227);
            this.chkBxAB.Name = "chkBxAB";
            this.chkBxAB.Size = new System.Drawing.Size(45, 17);
            this.chkBxAB.TabIndex = 2;
            this.chkBxAB.Text = "A/B";
            this.chkBxAB.UseVisualStyleBackColor = true;
            // 
            // BtnSetAttWidth
            // 
            this.BtnSetAttWidth.Location = new System.Drawing.Point(12, 12);
            this.BtnSetAttWidth.Name = "BtnSetAttWidth";
            this.BtnSetAttWidth.Size = new System.Drawing.Size(108, 60);
            this.BtnSetAttWidth.TabIndex = 3;
            this.BtnSetAttWidth.Text = "Set Att Width .9";
            this.BtnSetAttWidth.UseVisualStyleBackColor = true;
            this.BtnSetAttWidth.Click += new System.EventHandler(this.BtnSetAttWidth_Click);
            // 
            // Frm_USB_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 361);
            this.Controls.Add(this.BtnSetAttWidth);
            this.Controls.Add(this.chkBxAB);
            this.Controls.Add(this.TxtBxWAOsNumber);
            this.Controls.Add(this.BtnNumberWAOs);
            this.Name = "Frm_USB_MainForm";
            this.Text = "Frm_USB_MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_USB_MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Frm_USB_MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnNumberWAOs;
        private System.Windows.Forms.TextBox TxtBxWAOsNumber;
        private System.Windows.Forms.CheckBox chkBxAB;
        private System.Windows.Forms.Button BtnSetAttWidth;
    }
}
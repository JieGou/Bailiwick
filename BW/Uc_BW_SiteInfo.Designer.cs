namespace MyFirstProject.BW
{
    partial class Uc_BW_SiteInfo
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
            this.txtBxCloset = new System.Windows.Forms.TextBox();
            this.txtBxFloor = new System.Windows.Forms.TextBox();
            this.txtBxBuilding = new System.Windows.Forms.TextBox();
            this.txtBxSite = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtBxCloset
            // 
            this.txtBxCloset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxCloset.Location = new System.Drawing.Point(3, 102);
            this.txtBxCloset.Name = "txtBxCloset";
            this.txtBxCloset.Size = new System.Drawing.Size(156, 27);
            this.txtBxCloset.TabIndex = 15;
            this.txtBxCloset.Text = "txtBxCloset";
            // 
            // txtBxFloor
            // 
            this.txtBxFloor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxFloor.Location = new System.Drawing.Point(3, 69);
            this.txtBxFloor.Name = "txtBxFloor";
            this.txtBxFloor.Size = new System.Drawing.Size(156, 27);
            this.txtBxFloor.TabIndex = 16;
            this.txtBxFloor.Text = "txtBxFloor";
            // 
            // txtBxBuilding
            // 
            this.txtBxBuilding.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxBuilding.Location = new System.Drawing.Point(3, 36);
            this.txtBxBuilding.Name = "txtBxBuilding";
            this.txtBxBuilding.Size = new System.Drawing.Size(156, 27);
            this.txtBxBuilding.TabIndex = 17;
            this.txtBxBuilding.Text = "txtBxBuilding";
            // 
            // txtBxSite
            // 
            this.txtBxSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxSite.Location = new System.Drawing.Point(3, 3);
            this.txtBxSite.Name = "txtBxSite";
            this.txtBxSite.Size = new System.Drawing.Size(156, 27);
            this.txtBxSite.TabIndex = 18;
            this.txtBxSite.Text = "txtBxSite";
            // 
            // Uc_BW_SiteInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtBxCloset);
            this.Controls.Add(this.txtBxFloor);
            this.Controls.Add(this.txtBxBuilding);
            this.Controls.Add(this.txtBxSite);
            this.Name = "Uc_BW_SiteInfo";
            this.Size = new System.Drawing.Size(163, 135);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBxCloset;
        private System.Windows.Forms.TextBox txtBxFloor;
        private System.Windows.Forms.TextBox txtBxBuilding;
        private System.Windows.Forms.TextBox txtBxSite;
    }
}

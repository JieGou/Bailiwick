namespace MyFirstProject.BW
{
    partial class Uc_BW_TP_Blocks
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
            this.btnMoveInBlock = new System.Windows.Forms.Button();
            this.btnBlockRefAttributeSortAndAddMissingAttsFromDef = new System.Windows.Forms.Button();
            this.btnDumpAttsAllBlocks = new System.Windows.Forms.Button();
            this.btnOrderAllAttributes = new System.Windows.Forms.Button();
            this.BtnAttributeTable = new System.Windows.Forms.Button();
            this.BtnMatchAtts = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMoveInBlock
            // 
            this.btnMoveInBlock.Location = new System.Drawing.Point(480, 44);
            this.btnMoveInBlock.Name = "btnMoveInBlock";
            this.btnMoveInBlock.Size = new System.Drawing.Size(108, 98);
            this.btnMoveInBlock.TabIndex = 23;
            this.btnMoveInBlock.Text = "Move Att\r\nor\r\nEnt In Block";
            this.btnMoveInBlock.UseVisualStyleBackColor = true;
            this.btnMoveInBlock.Click += new System.EventHandler(this.BtnMoveInBlock_Click);
            // 
            // btnBlockRefAttributeSortAndAddMissingAttsFromDef
            // 
            this.btnBlockRefAttributeSortAndAddMissingAttsFromDef.Location = new System.Drawing.Point(285, 44);
            this.btnBlockRefAttributeSortAndAddMissingAttsFromDef.Name = "btnBlockRefAttributeSortAndAddMissingAttsFromDef";
            this.btnBlockRefAttributeSortAndAddMissingAttsFromDef.Size = new System.Drawing.Size(108, 98);
            this.btnBlockRefAttributeSortAndAddMissingAttsFromDef.TabIndex = 22;
            this.btnBlockRefAttributeSortAndAddMissingAttsFromDef.Text = "Block Ref Attribute Sort + Add Missing Atts From Def";
            this.btnBlockRefAttributeSortAndAddMissingAttsFromDef.UseVisualStyleBackColor = true;
            this.btnBlockRefAttributeSortAndAddMissingAttsFromDef.Click += new System.EventHandler(this.BtnBlockRefAttributeSortAndAddMissingAttsFromDef_Click);
            // 
            // btnDumpAttsAllBlocks
            // 
            this.btnDumpAttsAllBlocks.Location = new System.Drawing.Point(26, 44);
            this.btnDumpAttsAllBlocks.Name = "btnDumpAttsAllBlocks";
            this.btnDumpAttsAllBlocks.Size = new System.Drawing.Size(108, 98);
            this.btnDumpAttsAllBlocks.TabIndex = 21;
            this.btnDumpAttsAllBlocks.Text = "Dump Atts All Blocks";
            this.btnDumpAttsAllBlocks.UseVisualStyleBackColor = true;
            this.btnDumpAttsAllBlocks.Click += new System.EventHandler(this.BtnDumpAttsAllBlocks_Click);
            // 
            // btnOrderAllAttributes
            // 
            this.btnOrderAllAttributes.Location = new System.Drawing.Point(155, 44);
            this.btnOrderAllAttributes.Name = "btnOrderAllAttributes";
            this.btnOrderAllAttributes.Size = new System.Drawing.Size(108, 98);
            this.btnOrderAllAttributes.TabIndex = 20;
            this.btnOrderAllAttributes.Text = "Order All Attributes in Block Defs";
            this.btnOrderAllAttributes.UseVisualStyleBackColor = true;
            this.btnOrderAllAttributes.Click += new System.EventHandler(this.BtnOrderAllAttributes_Click);
            // 
            // BtnAttributeTable
            // 
            this.BtnAttributeTable.Location = new System.Drawing.Point(26, 239);
            this.BtnAttributeTable.Name = "BtnAttributeTable";
            this.BtnAttributeTable.Size = new System.Drawing.Size(108, 98);
            this.BtnAttributeTable.TabIndex = 24;
            this.BtnAttributeTable.Text = "Attribute Table";
            this.BtnAttributeTable.UseVisualStyleBackColor = true;
            this.BtnAttributeTable.Click += new System.EventHandler(this.BtnAttributeTable_Click);
            // 
            // BtnMatchAtts
            // 
            this.BtnMatchAtts.Location = new System.Drawing.Point(285, 239);
            this.BtnMatchAtts.Name = "BtnMatchAtts";
            this.BtnMatchAtts.Size = new System.Drawing.Size(108, 98);
            this.BtnMatchAtts.TabIndex = 25;
            this.BtnMatchAtts.Text = "Match Atts";
            this.BtnMatchAtts.UseVisualStyleBackColor = true;
            this.BtnMatchAtts.Click += new System.EventHandler(this.BtnMatchAtts_Click);
            // 
            // Uc_BW_TP_Blocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnMatchAtts);
            this.Controls.Add(this.BtnAttributeTable);
            this.Controls.Add(this.btnMoveInBlock);
            this.Controls.Add(this.btnBlockRefAttributeSortAndAddMissingAttsFromDef);
            this.Controls.Add(this.btnDumpAttsAllBlocks);
            this.Controls.Add(this.btnOrderAllAttributes);
            this.Name = "Uc_BW_TP_Blocks";
            this.Size = new System.Drawing.Size(655, 417);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMoveInBlock;
        private System.Windows.Forms.Button btnBlockRefAttributeSortAndAddMissingAttsFromDef;
        private System.Windows.Forms.Button btnDumpAttsAllBlocks;
        private System.Windows.Forms.Button btnOrderAllAttributes;
        private System.Windows.Forms.Button BtnAttributeTable;
        private System.Windows.Forms.Button BtnMatchAtts;
    }
}

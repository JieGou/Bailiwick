namespace MyFirstProject.BW.AMZ
{
    partial class Uc_AMZ_SecurityDoors
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
            this.chkBxShowClr = new System.Windows.Forms.CheckBox();
            this.btnNumberDoors = new System.Windows.Forms.Button();
            this.btnSavePorts = new System.Windows.Forms.Button();
            this.btnAssignPorts = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblDuplicateDeviceNumbers = new System.Windows.Forms.Label();
            this.lstBxDuplicates = new System.Windows.Forms.ListBox();
            this.cBxDeviceType = new System.Windows.Forms.ComboBox();
            this.cBxNetwork = new System.Windows.Forms.ComboBox();
            this.btnInsertAllMudules = new System.Windows.Forms.Button();
            this.chkBxDoorNumbers_AllOrOne = new System.Windows.Forms.CheckBox();
            this.lblAllModules = new System.Windows.Forms.Label();
            this.lblCnt1100 = new System.Windows.Forms.Label();
            this.lblCnt1200 = new System.Windows.Forms.Label();
            this.lblCnt1320 = new System.Windows.Forms.Label();
            this.btnZoomAndSelectAll = new System.Windows.Forms.Button();
            this.gBxModuleType = new System.Windows.Forms.GroupBox();
            this.rbAllModules = new System.Windows.Forms.RadioButton();
            this.rbNoLNL = new System.Windows.Forms.RadioButton();
            this.rbLNL1320 = new System.Windows.Forms.RadioButton();
            this.rbLNL1200 = new System.Windows.Forms.RadioButton();
            this.rbLNL1100 = new System.Windows.Forms.RadioButton();
            this.btnSaveAttsToXLPull = new System.Windows.Forms.Button();
            this.cBxAPCFilter = new System.Windows.Forms.ComboBox();
            this.btnZoomToAndSelectBlock = new System.Windows.Forms.Button();
            this.btnRemoveFilter = new System.Windows.Forms.Button();
            this.btnSelectSecurityDoors = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nudDoorNum = new System.Windows.Forms.NumericUpDown();
            this.TxtBxDoorNumPrefix = new System.Windows.Forms.TextBox();
            this.BtnPickBlocksForDoorNumber = new System.Windows.Forms.Button();
            this.dgvSecDevTags = new System.Windows.Forms.DataGridView();
            this.BtnAttsTest = new System.Windows.Forms.Button();
            this.gBxModuleType.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDoorNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSecDevTags)).BeginInit();
            this.SuspendLayout();
            // 
            // chkBxShowClr
            // 
            this.chkBxShowClr.AutoSize = true;
            this.chkBxShowClr.Location = new System.Drawing.Point(115, 505);
            this.chkBxShowClr.Name = "chkBxShowClr";
            this.chkBxShowClr.Size = new System.Drawing.Size(85, 21);
            this.chkBxShowClr.TabIndex = 27;
            this.chkBxShowClr.Text = "Show Clr";
            this.chkBxShowClr.UseVisualStyleBackColor = true;
            // 
            // btnNumberDoors
            // 
            this.btnNumberDoors.Location = new System.Drawing.Point(207, 488);
            this.btnNumberDoors.Name = "btnNumberDoors";
            this.btnNumberDoors.Size = new System.Drawing.Size(75, 53);
            this.btnNumberDoors.TabIndex = 25;
            this.btnNumberDoors.Text = "Number Doors";
            this.btnNumberDoors.UseVisualStyleBackColor = true;
            this.btnNumberDoors.Click += new System.EventHandler(this.BtnNumberDoors_Click);
            // 
            // btnSavePorts
            // 
            this.btnSavePorts.Location = new System.Drawing.Point(3, 408);
            this.btnSavePorts.Name = "btnSavePorts";
            this.btnSavePorts.Size = new System.Drawing.Size(75, 70);
            this.btnSavePorts.TabIndex = 24;
            this.btnSavePorts.Text = "Save Ports";
            this.btnSavePorts.UseVisualStyleBackColor = true;
            this.btnSavePorts.Click += new System.EventHandler(this.BtnSavePorts_Click);
            // 
            // btnAssignPorts
            // 
            this.btnAssignPorts.Location = new System.Drawing.Point(3, 332);
            this.btnAssignPorts.Name = "btnAssignPorts";
            this.btnAssignPorts.Size = new System.Drawing.Size(75, 70);
            this.btnAssignPorts.TabIndex = 23;
            this.btnAssignPorts.Text = "Assign Ports";
            this.btnAssignPorts.UseVisualStyleBackColor = true;
            this.btnAssignPorts.Click += new System.EventHandler(this.BtnAssignPorts_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(207, 238);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(77, 70);
            this.btnRefresh.TabIndex = 22;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // lblDuplicateDeviceNumbers
            // 
            this.lblDuplicateDeviceNumbers.AutoSize = true;
            this.lblDuplicateDeviceNumbers.Location = new System.Drawing.Point(176, 155);
            this.lblDuplicateDeviceNumbers.Name = "lblDuplicateDeviceNumbers";
            this.lblDuplicateDeviceNumbers.Size = new System.Drawing.Size(114, 17);
            this.lblDuplicateDeviceNumbers.TabIndex = 21;
            this.lblDuplicateDeviceNumbers.Text = "Duplicate Device";
            // 
            // lstBxDuplicates
            // 
            this.lstBxDuplicates.FormattingEnabled = true;
            this.lstBxDuplicates.ItemHeight = 16;
            this.lstBxDuplicates.Location = new System.Drawing.Point(179, 3);
            this.lstBxDuplicates.Name = "lstBxDuplicates";
            this.lstBxDuplicates.Size = new System.Drawing.Size(120, 148);
            this.lstBxDuplicates.TabIndex = 20;
            this.lstBxDuplicates.SelectedIndexChanged += new System.EventHandler(this.LstBxDuplicates_SelectedIndexChanged);
            // 
            // cBxDeviceType
            // 
            this.cBxDeviceType.FormattingEnabled = true;
            this.cBxDeviceType.Location = new System.Drawing.Point(179, 181);
            this.cBxDeviceType.Name = "cBxDeviceType";
            this.cBxDeviceType.Size = new System.Drawing.Size(120, 24);
            this.cBxDeviceType.TabIndex = 19;
            this.cBxDeviceType.SelectedIndexChanged += new System.EventHandler(this.CBxDeviceType_SelectedIndexChanged);
            // 
            // cBxNetwork
            // 
            this.cBxNetwork.FormattingEnabled = true;
            this.cBxNetwork.Location = new System.Drawing.Point(99, 238);
            this.cBxNetwork.Name = "cBxNetwork";
            this.cBxNetwork.Size = new System.Drawing.Size(75, 24);
            this.cBxNetwork.TabIndex = 17;
            // 
            // btnInsertAllMudules
            // 
            this.btnInsertAllMudules.Location = new System.Drawing.Point(93, 82);
            this.btnInsertAllMudules.Name = "btnInsertAllMudules";
            this.btnInsertAllMudules.Size = new System.Drawing.Size(75, 67);
            this.btnInsertAllMudules.TabIndex = 15;
            this.btnInsertAllMudules.Text = "Insert All Mudules";
            this.btnInsertAllMudules.UseVisualStyleBackColor = true;
            this.btnInsertAllMudules.Click += new System.EventHandler(this.BtnInsertAllMudules_Click);
            // 
            // chkBxDoorNumbers_AllOrOne
            // 
            this.chkBxDoorNumbers_AllOrOne.AutoSize = true;
            this.chkBxDoorNumbers_AllOrOne.Checked = true;
            this.chkBxDoorNumbers_AllOrOne.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxDoorNumbers_AllOrOne.Location = new System.Drawing.Point(93, 166);
            this.chkBxDoorNumbers_AllOrOne.Name = "chkBxDoorNumbers_AllOrOne";
            this.chkBxDoorNumbers_AllOrOne.Size = new System.Drawing.Size(87, 55);
            this.chkBxDoorNumbers_AllOrOne.TabIndex = 14;
            this.chkBxDoorNumbers_AllOrOne.Text = "Door\r\nNumbers\r\nAll / One";
            this.chkBxDoorNumbers_AllOrOne.UseVisualStyleBackColor = true;
            this.chkBxDoorNumbers_AllOrOne.CheckedChanged += new System.EventHandler(this.ChkBxDoorNumbers_AllOrOne_CheckedChanged);
            // 
            // lblAllModules
            // 
            this.lblAllModules.AutoSize = true;
            this.lblAllModules.Location = new System.Drawing.Point(209, 332);
            this.lblAllModules.Name = "lblAllModules";
            this.lblAllModules.Size = new System.Drawing.Size(90, 17);
            this.lblAllModules.TabIndex = 13;
            this.lblAllModules.Text = "lblAllModules";
            // 
            // lblCnt1100
            // 
            this.lblCnt1100.AutoSize = true;
            this.lblCnt1100.Location = new System.Drawing.Point(209, 359);
            this.lblCnt1100.Name = "lblCnt1100";
            this.lblCnt1100.Size = new System.Drawing.Size(75, 17);
            this.lblCnt1100.TabIndex = 9;
            this.lblCnt1100.Text = "lblCnt1100";
            // 
            // lblCnt1200
            // 
            this.lblCnt1200.AutoSize = true;
            this.lblCnt1200.Location = new System.Drawing.Point(209, 386);
            this.lblCnt1200.Name = "lblCnt1200";
            this.lblCnt1200.Size = new System.Drawing.Size(75, 17);
            this.lblCnt1200.TabIndex = 9;
            this.lblCnt1200.Text = "lblCnt1200";
            // 
            // lblCnt1320
            // 
            this.lblCnt1320.AutoSize = true;
            this.lblCnt1320.Location = new System.Drawing.Point(209, 413);
            this.lblCnt1320.Name = "lblCnt1320";
            this.lblCnt1320.Size = new System.Drawing.Size(75, 17);
            this.lblCnt1320.TabIndex = 9;
            this.lblCnt1320.Text = "lblCnt1320";
            // 
            // btnZoomAndSelectAll
            // 
            this.btnZoomAndSelectAll.Location = new System.Drawing.Point(3, 162);
            this.btnZoomAndSelectAll.Name = "btnZoomAndSelectAll";
            this.btnZoomAndSelectAll.Size = new System.Drawing.Size(75, 70);
            this.btnZoomAndSelectAll.TabIndex = 8;
            this.btnZoomAndSelectAll.Text = "Zoom And Select All";
            this.btnZoomAndSelectAll.UseVisualStyleBackColor = true;
            this.btnZoomAndSelectAll.Click += new System.EventHandler(this.BtnZoomAndSelectAll_Click);
            // 
            // gBxModuleType
            // 
            this.gBxModuleType.Controls.Add(this.rbAllModules);
            this.gBxModuleType.Controls.Add(this.rbNoLNL);
            this.gBxModuleType.Controls.Add(this.rbLNL1320);
            this.gBxModuleType.Controls.Add(this.rbLNL1200);
            this.gBxModuleType.Controls.Add(this.rbLNL1100);
            this.gBxModuleType.Location = new System.Drawing.Point(93, 311);
            this.gBxModuleType.Name = "gBxModuleType";
            this.gBxModuleType.Size = new System.Drawing.Size(110, 165);
            this.gBxModuleType.TabIndex = 7;
            this.gBxModuleType.TabStop = false;
            this.gBxModuleType.Text = "Module Type";
            // 
            // rbAllModules
            // 
            this.rbAllModules.AutoSize = true;
            this.rbAllModules.Checked = true;
            this.rbAllModules.Location = new System.Drawing.Point(6, 21);
            this.rbAllModules.Name = "rbAllModules";
            this.rbAllModules.Size = new System.Drawing.Size(101, 21);
            this.rbAllModules.TabIndex = 1;
            this.rbAllModules.TabStop = true;
            this.rbAllModules.Text = "All Modules";
            this.rbAllModules.UseVisualStyleBackColor = true;
            this.rbAllModules.Click += new System.EventHandler(this.RbAllModules_Click);
            // 
            // rbNoLNL
            // 
            this.rbNoLNL.AutoSize = true;
            this.rbNoLNL.Location = new System.Drawing.Point(6, 129);
            this.rbNoLNL.Name = "rbNoLNL";
            this.rbNoLNL.Size = new System.Drawing.Size(77, 21);
            this.rbNoLNL.TabIndex = 0;
            this.rbNoLNL.Text = "No LNL";
            this.rbNoLNL.UseVisualStyleBackColor = true;
            this.rbNoLNL.Click += new System.EventHandler(this.RbAllModules_Click);
            // 
            // rbLNL1320
            // 
            this.rbLNL1320.AutoSize = true;
            this.rbLNL1320.Location = new System.Drawing.Point(6, 102);
            this.rbLNL1320.Name = "rbLNL1320";
            this.rbLNL1320.Size = new System.Drawing.Size(92, 21);
            this.rbLNL1320.TabIndex = 0;
            this.rbLNL1320.Text = "LNL-1320";
            this.rbLNL1320.UseVisualStyleBackColor = true;
            this.rbLNL1320.Click += new System.EventHandler(this.RbAllModules_Click);
            // 
            // rbLNL1200
            // 
            this.rbLNL1200.AutoSize = true;
            this.rbLNL1200.Location = new System.Drawing.Point(6, 75);
            this.rbLNL1200.Name = "rbLNL1200";
            this.rbLNL1200.Size = new System.Drawing.Size(92, 21);
            this.rbLNL1200.TabIndex = 0;
            this.rbLNL1200.Text = "LNL-1200";
            this.rbLNL1200.UseVisualStyleBackColor = true;
            this.rbLNL1200.Click += new System.EventHandler(this.RbAllModules_Click);
            // 
            // rbLNL1100
            // 
            this.rbLNL1100.AutoSize = true;
            this.rbLNL1100.Location = new System.Drawing.Point(6, 48);
            this.rbLNL1100.Name = "rbLNL1100";
            this.rbLNL1100.Size = new System.Drawing.Size(92, 21);
            this.rbLNL1100.TabIndex = 0;
            this.rbLNL1100.Text = "LNL-1100";
            this.rbLNL1100.UseVisualStyleBackColor = true;
            this.rbLNL1100.Click += new System.EventHandler(this.RbAllModules_Click);
            // 
            // btnSaveAttsToXLPull
            // 
            this.btnSaveAttsToXLPull.Location = new System.Drawing.Point(93, 3);
            this.btnSaveAttsToXLPull.Name = "btnSaveAttsToXLPull";
            this.btnSaveAttsToXLPull.Size = new System.Drawing.Size(75, 73);
            this.btnSaveAttsToXLPull.TabIndex = 6;
            this.btnSaveAttsToXLPull.Text = "Save To Xl Pull";
            this.btnSaveAttsToXLPull.UseVisualStyleBackColor = true;
            this.btnSaveAttsToXLPull.Click += new System.EventHandler(this.BtnSaveAttsToXLPull_Click);
            // 
            // cBxAPCFilter
            // 
            this.cBxAPCFilter.FormattingEnabled = true;
            this.cBxAPCFilter.Location = new System.Drawing.Point(99, 268);
            this.cBxAPCFilter.Name = "cBxAPCFilter";
            this.cBxAPCFilter.Size = new System.Drawing.Size(75, 24);
            this.cBxAPCFilter.TabIndex = 5;
            // 
            // btnZoomToAndSelectBlock
            // 
            this.btnZoomToAndSelectBlock.Location = new System.Drawing.Point(3, 82);
            this.btnZoomToAndSelectBlock.Name = "btnZoomToAndSelectBlock";
            this.btnZoomToAndSelectBlock.Size = new System.Drawing.Size(75, 74);
            this.btnZoomToAndSelectBlock.TabIndex = 4;
            this.btnZoomToAndSelectBlock.Text = "Zoom And Select";
            this.btnZoomToAndSelectBlock.UseVisualStyleBackColor = true;
            this.btnZoomToAndSelectBlock.Click += new System.EventHandler(this.BtnZoomToAndSelectBlock_Click);
            // 
            // btnRemoveFilter
            // 
            this.btnRemoveFilter.Location = new System.Drawing.Point(3, 238);
            this.btnRemoveFilter.Name = "btnRemoveFilter";
            this.btnRemoveFilter.Size = new System.Drawing.Size(75, 70);
            this.btnRemoveFilter.TabIndex = 3;
            this.btnRemoveFilter.Text = "Remove All Filters";
            this.btnRemoveFilter.UseVisualStyleBackColor = true;
            this.btnRemoveFilter.Click += new System.EventHandler(this.BtnRemoveFilter_Click);
            // 
            // btnSelectSecurityDoors
            // 
            this.btnSelectSecurityDoors.Location = new System.Drawing.Point(3, 3);
            this.btnSelectSecurityDoors.Name = "btnSelectSecurityDoors";
            this.btnSelectSecurityDoors.Size = new System.Drawing.Size(75, 73);
            this.btnSelectSecurityDoors.TabIndex = 0;
            this.btnSelectSecurityDoors.Text = "Select Security Doors";
            this.btnSelectSecurityDoors.UseVisualStyleBackColor = true;
            this.btnSelectSecurityDoors.Click += new System.EventHandler(this.BtnSelectSecurityDoors_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.BtnAttsTest);
            this.panel1.Controls.Add(this.nudDoorNum);
            this.panel1.Controls.Add(this.TxtBxDoorNumPrefix);
            this.panel1.Controls.Add(this.BtnPickBlocksForDoorNumber);
            this.panel1.Controls.Add(this.chkBxShowClr);
            this.panel1.Controls.Add(this.btnSelectSecurityDoors);
            this.panel1.Controls.Add(this.btnNumberDoors);
            this.panel1.Controls.Add(this.btnRemoveFilter);
            this.panel1.Controls.Add(this.btnSavePorts);
            this.panel1.Controls.Add(this.btnZoomToAndSelectBlock);
            this.panel1.Controls.Add(this.btnAssignPorts);
            this.panel1.Controls.Add(this.cBxAPCFilter);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnSaveAttsToXLPull);
            this.panel1.Controls.Add(this.lblDuplicateDeviceNumbers);
            this.panel1.Controls.Add(this.gBxModuleType);
            this.panel1.Controls.Add(this.lstBxDuplicates);
            this.panel1.Controls.Add(this.btnZoomAndSelectAll);
            this.panel1.Controls.Add(this.cBxDeviceType);
            this.panel1.Controls.Add(this.lblCnt1320);
            this.panel1.Controls.Add(this.cBxNetwork);
            this.panel1.Controls.Add(this.lblCnt1200);
            this.panel1.Controls.Add(this.btnInsertAllMudules);
            this.panel1.Controls.Add(this.lblCnt1100);
            this.panel1.Controls.Add(this.chkBxDoorNumbers_AllOrOne);
            this.panel1.Controls.Add(this.lblAllModules);
            this.panel1.Location = new System.Drawing.Point(694, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(315, 684);
            this.panel1.TabIndex = 1;
            // 
            // nudDoorNum
            // 
            this.nudDoorNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudDoorNum.Location = new System.Drawing.Point(96, 617);
            this.nudDoorNum.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudDoorNum.Name = "nudDoorNum";
            this.nudDoorNum.Size = new System.Drawing.Size(100, 27);
            this.nudDoorNum.TabIndex = 30;
            this.nudDoorNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // TxtBxDoorNumPrefix
            // 
            this.TxtBxDoorNumPrefix.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBxDoorNumPrefix.Location = new System.Drawing.Point(96, 579);
            this.TxtBxDoorNumPrefix.Name = "TxtBxDoorNumPrefix";
            this.TxtBxDoorNumPrefix.Size = new System.Drawing.Size(100, 27);
            this.TxtBxDoorNumPrefix.TabIndex = 29;
            // 
            // BtnPickBlocksForDoorNumber
            // 
            this.BtnPickBlocksForDoorNumber.Location = new System.Drawing.Point(207, 579);
            this.BtnPickBlocksForDoorNumber.Name = "BtnPickBlocksForDoorNumber";
            this.BtnPickBlocksForDoorNumber.Size = new System.Drawing.Size(105, 65);
            this.BtnPickBlocksForDoorNumber.TabIndex = 28;
            this.BtnPickBlocksForDoorNumber.Text = "Pick Blocks For Door Number";
            this.BtnPickBlocksForDoorNumber.UseVisualStyleBackColor = true;
            this.BtnPickBlocksForDoorNumber.Click += new System.EventHandler(this.BtnPickBlocksForDoorNumber_Click);
            // 
            // dgvSecDevTags
            // 
            this.dgvSecDevTags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSecDevTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSecDevTags.Location = new System.Drawing.Point(5, 5);
            this.dgvSecDevTags.Name = "dgvSecDevTags";
            this.dgvSecDevTags.RowTemplate.Height = 24;
            this.dgvSecDevTags.Size = new System.Drawing.Size(683, 682);
            this.dgvSecDevTags.TabIndex = 2;
            // 
            // BtnAttsTest
            // 
            this.BtnAttsTest.Location = new System.Drawing.Point(3, 488);
            this.BtnAttsTest.Name = "BtnAttsTest";
            this.BtnAttsTest.Size = new System.Drawing.Size(75, 78);
            this.BtnAttsTest.TabIndex = 31;
            this.BtnAttsTest.Text = "Atts Duplicate";
            this.BtnAttsTest.UseVisualStyleBackColor = true;
            this.BtnAttsTest.Click += new System.EventHandler(this.BtnAttsTest_Click);
            // 
            // Uc_AMZ_SecurityDoors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvSecDevTags);
            this.Controls.Add(this.panel1);
            this.Name = "Uc_AMZ_SecurityDoors";
            this.Size = new System.Drawing.Size(1009, 693);
            this.gBxModuleType.ResumeLayout(false);
            this.gBxModuleType.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDoorNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSecDevTags)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox chkBxShowClr;
        private System.Windows.Forms.Button btnNumberDoors;
        private System.Windows.Forms.Button btnSavePorts;
        private System.Windows.Forms.Button btnAssignPorts;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblDuplicateDeviceNumbers;
        private System.Windows.Forms.ListBox lstBxDuplicates;
        private System.Windows.Forms.ComboBox cBxDeviceType;
        private System.Windows.Forms.ComboBox cBxNetwork;
        private System.Windows.Forms.Button btnInsertAllMudules;
        private System.Windows.Forms.CheckBox chkBxDoorNumbers_AllOrOne;
        private System.Windows.Forms.Label lblAllModules;
        private System.Windows.Forms.Label lblCnt1100;
        private System.Windows.Forms.Label lblCnt1200;
        private System.Windows.Forms.Label lblCnt1320;
        private System.Windows.Forms.Button btnZoomAndSelectAll;
        private System.Windows.Forms.GroupBox gBxModuleType;
        private System.Windows.Forms.RadioButton rbAllModules;
        private System.Windows.Forms.RadioButton rbNoLNL;
        private System.Windows.Forms.RadioButton rbLNL1320;
        private System.Windows.Forms.RadioButton rbLNL1200;
        private System.Windows.Forms.RadioButton rbLNL1100;
        private System.Windows.Forms.Button btnSaveAttsToXLPull;
        private System.Windows.Forms.ComboBox cBxAPCFilter;
        private System.Windows.Forms.Button btnZoomToAndSelectBlock;
        private System.Windows.Forms.Button btnRemoveFilter;
        private System.Windows.Forms.Button btnSelectSecurityDoors;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvSecDevTags;
        private System.Windows.Forms.TextBox TxtBxDoorNumPrefix;
        private System.Windows.Forms.Button BtnPickBlocksForDoorNumber;
        private System.Windows.Forms.NumericUpDown nudDoorNum;
        private System.Windows.Forms.Button BtnAttsTest;
    }
}

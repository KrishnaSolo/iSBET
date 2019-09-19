namespace RoadwareSBET.Domain
{
  partial class frmSBETDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSBETDialog));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblProjects = new System.Windows.Forms.Label();
            this.btnRefreshProjects = new System.Windows.Forms.Button();
            this.lblLocalFolder = new System.Windows.Forms.Label();
            this.btnGetLocalFolder = new System.Windows.Forms.Button();
            this.txtBxLocalFolder = new System.Windows.Forms.TextBox();
            this.lblTemplateFile = new System.Windows.Forms.Label();
            this.btnGetTemplateFile = new System.Windows.Forms.Button();
            this.txtBxTemplateFile = new System.Windows.Forms.TextBox();
            this.cmboBxGNSSMode = new System.Windows.Forms.ComboBox();
            this.lblGNSSMode = new System.Windows.Forms.Label();
            this.dtTmPkrStartDateAndTime = new System.Windows.Forms.DateTimePicker();
            this.lblStartDateAndTime = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tmrStartDateAndTime = new System.Windows.Forms.Timer(this.components);
            this.btnStartBatchProcess = new System.Windows.Forms.Button();
            this.cmboBxProjects = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bndngNav = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.dtaGrdVwBatchDt = new System.Windows.Forms.DataGridView();
            this.lblServerFolder = new System.Windows.Forms.Label();
            this.btnGetServerFolder = new System.Windows.Forms.Button();
            this.txtBxServerFolder = new System.Windows.Forms.TextBox();
            this.lblVideoLocationFolderStructure = new System.Windows.Forms.Label();
            this.tlTp = new System.Windows.Forms.ToolTip(this.components);
            this.rdoBtnBatchOnly = new System.Windows.Forms.RadioButton();
            this.rdoBtnBatchAndExport = new System.Windows.Forms.RadioButton();
            this.lblBrowser = new System.Windows.Forms.Label();
            this.ReportBackup = new System.Windows.Forms.Button();
            this.SBETQC = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.bndngSrc = new System.Windows.Forms.BindingSource(this.components);
            this.PinPoint = new System.Windows.Forms.Button();
            this.myGroupBox1 = new RoadwareSBET.Domain.myGroupBox();
            this.rdoBtnFolderPost14 = new System.Windows.Forms.RadioButton();
            this.rdoBtnFolderPre14 = new System.Windows.Forms.RadioButton();
            this.grpBxBrowser = new RoadwareSBET.Domain.myGroupBox();
            this.rdoBtnGoogleChrome = new System.Windows.Forms.RadioButton();
            this.rdoBtnInternetExplorer = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bndngNav)).BeginInit();
            this.bndngNav.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtaGrdVwBatchDt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bndngSrc)).BeginInit();
            this.myGroupBox1.SuspendLayout();
            this.grpBxBrowser.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblProjects
            // 
            this.lblProjects.AutoSize = true;
            this.lblProjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProjects.ForeColor = System.Drawing.Color.Blue;
            this.lblProjects.Location = new System.Drawing.Point(5, 37);
            this.lblProjects.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblProjects.Name = "lblProjects";
            this.lblProjects.Size = new System.Drawing.Size(74, 20);
            this.lblProjects.TabIndex = 25;
            this.lblProjects.Text = "Projects";
            // 
            // btnRefreshProjects
            // 
            this.btnRefreshProjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshProjects.ForeColor = System.Drawing.Color.Blue;
            this.btnRefreshProjects.Location = new System.Drawing.Point(696, 60);
            this.btnRefreshProjects.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefreshProjects.Name = "btnRefreshProjects";
            this.btnRefreshProjects.Size = new System.Drawing.Size(175, 32);
            this.btnRefreshProjects.TabIndex = 24;
            this.btnRefreshProjects.Text = "Refresh Projects";
            this.btnRefreshProjects.UseVisualStyleBackColor = true;
            this.btnRefreshProjects.Click += new System.EventHandler(this.btnRefreshProjects_Click);
            // 
            // lblLocalFolder
            // 
            this.lblLocalFolder.AutoSize = true;
            this.lblLocalFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalFolder.ForeColor = System.Drawing.Color.Blue;
            this.lblLocalFolder.Location = new System.Drawing.Point(9, 169);
            this.lblLocalFolder.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLocalFolder.Name = "lblLocalFolder";
            this.lblLocalFolder.Size = new System.Drawing.Size(438, 20);
            this.lblLocalFolder.TabIndex = 28;
            this.lblLocalFolder.Text = "Processing Folder (template - D:\\SBET\\[Project alias])";
            this.tlTp.SetToolTip(this.lblLocalFolder, "The path of a production batch for project Iowa 2015\r\nprocessed by ANY TECHNICIAN" +
        " should be:\r\nD:\\SBET\\85046IA15");
            // 
            // btnGetLocalFolder
            // 
            this.btnGetLocalFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetLocalFolder.ForeColor = System.Drawing.Color.Blue;
            this.btnGetLocalFolder.Location = new System.Drawing.Point(696, 192);
            this.btnGetLocalFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetLocalFolder.Name = "btnGetLocalFolder";
            this.btnGetLocalFolder.Size = new System.Drawing.Size(175, 32);
            this.btnGetLocalFolder.TabIndex = 27;
            this.btnGetLocalFolder.Text = "Get Local Folder";
            this.btnGetLocalFolder.UseVisualStyleBackColor = true;
            this.btnGetLocalFolder.Click += new System.EventHandler(this.btnGetLocalFolder_Click);
            // 
            // txtBxLocalFolder
            // 
            this.txtBxLocalFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxLocalFolder.Location = new System.Drawing.Point(9, 197);
            this.txtBxLocalFolder.Margin = new System.Windows.Forms.Padding(2);
            this.txtBxLocalFolder.Name = "txtBxLocalFolder";
            this.txtBxLocalFolder.Size = new System.Drawing.Size(684, 26);
            this.txtBxLocalFolder.TabIndex = 26;
            this.tlTp.SetToolTip(this.txtBxLocalFolder, "The path of a production batch for project Iowa 2015\r\nprocessed by ANY TECHNICIAN" +
        " should be:\r\nD:\\SBET\\85046IA15");
            // 
            // lblTemplateFile
            // 
            this.lblTemplateFile.AutoSize = true;
            this.lblTemplateFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemplateFile.ForeColor = System.Drawing.Color.Blue;
            this.lblTemplateFile.Location = new System.Drawing.Point(9, 228);
            this.lblTemplateFile.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTemplateFile.Name = "lblTemplateFile";
            this.lblTemplateFile.Size = new System.Drawing.Size(117, 20);
            this.lblTemplateFile.TabIndex = 31;
            this.lblTemplateFile.Text = "Template File";
            // 
            // btnGetTemplateFile
            // 
            this.btnGetTemplateFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetTemplateFile.ForeColor = System.Drawing.Color.Blue;
            this.btnGetTemplateFile.Location = new System.Drawing.Point(696, 254);
            this.btnGetTemplateFile.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetTemplateFile.Name = "btnGetTemplateFile";
            this.btnGetTemplateFile.Size = new System.Drawing.Size(175, 29);
            this.btnGetTemplateFile.TabIndex = 30;
            this.btnGetTemplateFile.Text = "Get Template File";
            this.btnGetTemplateFile.UseVisualStyleBackColor = true;
            this.btnGetTemplateFile.Click += new System.EventHandler(this.btnGetTemplateFile_Click);
            // 
            // txtBxTemplateFile
            // 
            this.txtBxTemplateFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxTemplateFile.Location = new System.Drawing.Point(9, 256);
            this.txtBxTemplateFile.Margin = new System.Windows.Forms.Padding(2);
            this.txtBxTemplateFile.Name = "txtBxTemplateFile";
            this.txtBxTemplateFile.Size = new System.Drawing.Size(684, 26);
            this.txtBxTemplateFile.TabIndex = 29;
            // 
            // cmboBxGNSSMode
            // 
            this.cmboBxGNSSMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmboBxGNSSMode.FormattingEnabled = true;
            this.cmboBxGNSSMode.Location = new System.Drawing.Point(9, 318);
            this.cmboBxGNSSMode.Margin = new System.Windows.Forms.Padding(2);
            this.cmboBxGNSSMode.Name = "cmboBxGNSSMode";
            this.cmboBxGNSSMode.Size = new System.Drawing.Size(202, 28);
            this.cmboBxGNSSMode.TabIndex = 32;
            this.cmboBxGNSSMode.SelectedIndexChanged += new System.EventHandler(this.cmboBxGNSSMode_SelectedIndexChanged);
            // 
            // lblGNSSMode
            // 
            this.lblGNSSMode.AutoSize = true;
            this.lblGNSSMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGNSSMode.ForeColor = System.Drawing.Color.Blue;
            this.lblGNSSMode.Location = new System.Drawing.Point(60, 291);
            this.lblGNSSMode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGNSSMode.Name = "lblGNSSMode";
            this.lblGNSSMode.Size = new System.Drawing.Size(108, 20);
            this.lblGNSSMode.TabIndex = 33;
            this.lblGNSSMode.Text = "GNSS Mode";
            // 
            // dtTmPkrStartDateAndTime
            // 
            this.dtTmPkrStartDateAndTime.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dtTmPkrStartDateAndTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTmPkrStartDateAndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTmPkrStartDateAndTime.Location = new System.Drawing.Point(228, 321);
            this.dtTmPkrStartDateAndTime.Margin = new System.Windows.Forms.Padding(2);
            this.dtTmPkrStartDateAndTime.Name = "dtTmPkrStartDateAndTime";
            this.dtTmPkrStartDateAndTime.Size = new System.Drawing.Size(216, 26);
            this.dtTmPkrStartDateAndTime.TabIndex = 34;
            this.dtTmPkrStartDateAndTime.Value = new System.DateTime(2019, 5, 29, 15, 55, 40, 0);
            this.dtTmPkrStartDateAndTime.ValueChanged += new System.EventHandler(this.dtTmPkrStartDateAndTime_ValueChanged);
            // 
            // lblStartDateAndTime
            // 
            this.lblStartDateAndTime.AutoSize = true;
            this.lblStartDateAndTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDateAndTime.ForeColor = System.Drawing.Color.Blue;
            this.lblStartDateAndTime.Location = new System.Drawing.Point(259, 291);
            this.lblStartDateAndTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStartDateAndTime.Name = "lblStartDateAndTime";
            this.lblStartDateAndTime.Size = new System.Drawing.Size(171, 20);
            this.lblStartDateAndTime.TabIndex = 35;
            this.lblStartDateAndTime.Text = "Start Date and Time";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(5, 820);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(71, 20);
            this.lblStatus.TabIndex = 36;
            this.lblStatus.Text = "lblStatus";
            // 
            // tmrStartDateAndTime
            // 
            this.tmrStartDateAndTime.Interval = 2000;
            this.tmrStartDateAndTime.Tick += new System.EventHandler(this.tmrStartDateAndTime_Tick);
            // 
            // btnStartBatchProcess
            // 
            this.btnStartBatchProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartBatchProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartBatchProcess.ForeColor = System.Drawing.Color.Blue;
            this.btnStartBatchProcess.Location = new System.Drawing.Point(513, 762);
            this.btnStartBatchProcess.Margin = new System.Windows.Forms.Padding(2);
            this.btnStartBatchProcess.Name = "btnStartBatchProcess";
            this.btnStartBatchProcess.Size = new System.Drawing.Size(202, 55);
            this.btnStartBatchProcess.TabIndex = 37;
            this.btnStartBatchProcess.Text = "Process Batches";
            this.btnStartBatchProcess.UseVisualStyleBackColor = true;
            this.btnStartBatchProcess.Click += new System.EventHandler(this.btnStartBatchProcess_Click);
            // 
            // cmboBxProjects
            // 
            this.cmboBxProjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmboBxProjects.FormattingEnabled = true;
            this.cmboBxProjects.Location = new System.Drawing.Point(9, 64);
            this.cmboBxProjects.Margin = new System.Windows.Forms.Padding(2);
            this.cmboBxProjects.Name = "cmboBxProjects";
            this.cmboBxProjects.Size = new System.Drawing.Size(684, 28);
            this.cmboBxProjects.TabIndex = 38;
            this.cmboBxProjects.SelectedIndexChanged += new System.EventHandler(this.cmboBxProjects_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.bndngNav);
            this.panel1.Controls.Add(this.toolStripContainer1);
            this.panel1.Controls.Add(this.dtaGrdVwBatchDt);
            this.panel1.Location = new System.Drawing.Point(9, 360);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1060, 396);
            this.panel1.TabIndex = 39;
            // 
            // bndngNav
            // 
            this.bndngNav.AddNewItem = null;
            this.bndngNav.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bndngNav.CountItem = this.bindingNavigatorCountItem;
            this.bndngNav.DeleteItem = null;
            this.bndngNav.Dock = System.Windows.Forms.DockStyle.None;
            this.bndngNav.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.bndngNav.Location = new System.Drawing.Point(434, 371);
            this.bndngNav.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bndngNav.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bndngNav.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bndngNav.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bndngNav.Name = "bndngNav";
            this.bndngNav.PositionItem = this.bindingNavigatorPositionItem;
            this.bndngNav.Size = new System.Drawing.Size(157, 22);
            this.bndngNav.TabIndex = 1;
            this.bndngNav.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 19);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 19);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 19);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 22);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(38, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 20);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 20);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Margin = new System.Windows.Forms.Padding(2);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1060, 0);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 372);
            this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1060, 24);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Padding = new System.Windows.Forms.Padding(360, 0, 0, 0);
            // 
            // dtaGrdVwBatchDt
            // 
            this.dtaGrdVwBatchDt.AllowUserToAddRows = false;
            this.dtaGrdVwBatchDt.AllowUserToDeleteRows = false;
            this.dtaGrdVwBatchDt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtaGrdVwBatchDt.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtaGrdVwBatchDt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtaGrdVwBatchDt.Location = new System.Drawing.Point(2, 3);
            this.dtaGrdVwBatchDt.Margin = new System.Windows.Forms.Padding(2);
            this.dtaGrdVwBatchDt.Name = "dtaGrdVwBatchDt";
            this.dtaGrdVwBatchDt.RowTemplate.Height = 24;
            this.dtaGrdVwBatchDt.Size = new System.Drawing.Size(1058, 366);
            this.dtaGrdVwBatchDt.TabIndex = 1;
            this.dtaGrdVwBatchDt.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtaGrdVwBatchDt_CellClick);
            this.dtaGrdVwBatchDt.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtaGrdVwBatchDt_CellContentClick);
            this.dtaGrdVwBatchDt.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtaGrdVwBatchDt_CellValueChanged);
            // 
            // lblServerFolder
            // 
            this.lblServerFolder.AutoSize = true;
            this.lblServerFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerFolder.ForeColor = System.Drawing.Color.Blue;
            this.lblServerFolder.Location = new System.Drawing.Point(9, 102);
            this.lblServerFolder.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblServerFolder.Name = "lblServerFolder";
            this.lblServerFolder.Size = new System.Drawing.Size(491, 20);
            this.lblServerFolder.TabIndex = 52;
            this.lblServerFolder.Text = "Data Folder (template - [Video Location]\\SBET\\[Tech name])";
            this.tlTp.SetToolTip(this.lblServerFolder, "The path of a production batch for project Iowa 2015\r\nprocessed by Peter Frampton" +
        " should be:\r\n\\\\video-14\\V14A\\85046IA15\\SBET\\Peter");
            // 
            // btnGetServerFolder
            // 
            this.btnGetServerFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetServerFolder.ForeColor = System.Drawing.Color.Blue;
            this.btnGetServerFolder.Location = new System.Drawing.Point(696, 125);
            this.btnGetServerFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetServerFolder.Name = "btnGetServerFolder";
            this.btnGetServerFolder.Size = new System.Drawing.Size(175, 32);
            this.btnGetServerFolder.TabIndex = 51;
            this.btnGetServerFolder.Text = "Get Server Folder";
            this.btnGetServerFolder.UseVisualStyleBackColor = true;
            this.btnGetServerFolder.Click += new System.EventHandler(this.btnGetServerFolder_Click);
            // 
            // txtBxServerFolder
            // 
            this.txtBxServerFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxServerFolder.Location = new System.Drawing.Point(9, 131);
            this.txtBxServerFolder.Margin = new System.Windows.Forms.Padding(2);
            this.txtBxServerFolder.Name = "txtBxServerFolder";
            this.txtBxServerFolder.Size = new System.Drawing.Size(684, 26);
            this.txtBxServerFolder.TabIndex = 50;
            this.tlTp.SetToolTip(this.txtBxServerFolder, "The path of a production batch for project Iowa 2015\r\nprocessed by Peter Frampton" +
        " should be:\r\n\\\\video-14\\V14A\\85046IA15\\SBET\\Peter");
            // 
            // lblVideoLocationFolderStructure
            // 
            this.lblVideoLocationFolderStructure.AutoSize = true;
            this.lblVideoLocationFolderStructure.Enabled = false;
            this.lblVideoLocationFolderStructure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVideoLocationFolderStructure.ForeColor = System.Drawing.Color.Silver;
            this.lblVideoLocationFolderStructure.Location = new System.Drawing.Point(884, 7);
            this.lblVideoLocationFolderStructure.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVideoLocationFolderStructure.Name = "lblVideoLocationFolderStructure";
            this.lblVideoLocationFolderStructure.Size = new System.Drawing.Size(187, 13);
            this.lblVideoLocationFolderStructure.TabIndex = 53;
            this.lblVideoLocationFolderStructure.Text = "Video Location Folder Structure";
            // 
            // tlTp
            // 
            this.tlTp.AutomaticDelay = 250;
            this.tlTp.AutoPopDelay = 10000;
            this.tlTp.InitialDelay = 250;
            this.tlTp.ReshowDelay = 50;
            // 
            // rdoBtnBatchOnly
            // 
            this.rdoBtnBatchOnly.AutoSize = true;
            this.rdoBtnBatchOnly.Checked = true;
            this.rdoBtnBatchOnly.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoBtnBatchOnly.ForeColor = System.Drawing.Color.Blue;
            this.rdoBtnBatchOnly.Location = new System.Drawing.Point(173, 9);
            this.rdoBtnBatchOnly.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdoBtnBatchOnly.Name = "rdoBtnBatchOnly";
            this.rdoBtnBatchOnly.Size = new System.Drawing.Size(129, 29);
            this.rdoBtnBatchOnly.TabIndex = 1;
            this.rdoBtnBatchOnly.TabStop = true;
            this.rdoBtnBatchOnly.Text = "Batch Only";
            this.rdoBtnBatchOnly.UseVisualStyleBackColor = true;
            // 
            // rdoBtnBatchAndExport
            // 
            this.rdoBtnBatchAndExport.AutoSize = true;
            this.rdoBtnBatchAndExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoBtnBatchAndExport.ForeColor = System.Drawing.Color.Blue;
            this.rdoBtnBatchAndExport.Location = new System.Drawing.Point(5, 9);
            this.rdoBtnBatchAndExport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdoBtnBatchAndExport.Name = "rdoBtnBatchAndExport";
            this.rdoBtnBatchAndExport.Size = new System.Drawing.Size(162, 29);
            this.rdoBtnBatchAndExport.TabIndex = 0;
            this.rdoBtnBatchAndExport.Text = "Batch && Export";
            this.rdoBtnBatchAndExport.UseVisualStyleBackColor = true;
            // 
            // lblBrowser
            // 
            this.lblBrowser.AutoSize = true;
            this.lblBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrowser.ForeColor = System.Drawing.Color.Blue;
            this.lblBrowser.Location = new System.Drawing.Point(675, 9);
            this.lblBrowser.Name = "lblBrowser";
            this.lblBrowser.Size = new System.Drawing.Size(74, 20);
            this.lblBrowser.TabIndex = 42;
            this.lblBrowser.Text = "Browser";
            // 
            // ReportBackup
            // 
            this.ReportBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ReportBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReportBackup.ForeColor = System.Drawing.Color.Blue;
            this.ReportBackup.Location = new System.Drawing.Point(973, 761);
            this.ReportBackup.Name = "ReportBackup";
            this.ReportBackup.Size = new System.Drawing.Size(96, 55);
            this.ReportBackup.TabIndex = 55;
            this.ReportBackup.Text = "Reports Backup";
            this.ReportBackup.UseVisualStyleBackColor = true;
            this.ReportBackup.Click += new System.EventHandler(this.ReportBackupButton);
            // 
            // SBETQC
            // 
            this.SBETQC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SBETQC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SBETQC.ForeColor = System.Drawing.Color.Blue;
            this.SBETQC.Location = new System.Drawing.Point(743, 762);
            this.SBETQC.Name = "SBETQC";
            this.SBETQC.Size = new System.Drawing.Size(92, 55);
            this.SBETQC.TabIndex = 56;
            this.SBETQC.Text = "SBET QC";
            this.SBETQC.UseVisualStyleBackColor = true;
            this.SBETQC.Click += new System.EventHandler(this.SBETQCButton);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(859, 762);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 55);
            this.button1.TabIndex = 57;
            this.button1.Text = "SBETs Backup";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click_2);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.Blue;
            this.checkBox1.Location = new System.Drawing.Point(472, 323);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(157, 19);
            this.checkBox1.TabIndex = 59;
            this.checkBox1.Text = "Background Process";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // PinPoint
            // 
            this.PinPoint.Location = new System.Drawing.Point(662, 323);
            this.PinPoint.Name = "PinPoint";
            this.PinPoint.Size = new System.Drawing.Size(75, 23);
            this.PinPoint.TabIndex = 60;
            this.PinPoint.Text = "Analyze";
            this.PinPoint.UseVisualStyleBackColor = true;
            this.PinPoint.Click += new System.EventHandler(this.PinPoint_Click);
            // 
            // myGroupBox1
            // 
            this.myGroupBox1.BorderColor = System.Drawing.Color.Black;
            this.myGroupBox1.Controls.Add(this.rdoBtnFolderPost14);
            this.myGroupBox1.Controls.Add(this.rdoBtnFolderPre14);
            this.myGroupBox1.Enabled = false;
            this.myGroupBox1.Location = new System.Drawing.Point(876, 9);
            this.myGroupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.myGroupBox1.Name = "myGroupBox1";
            this.myGroupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.myGroupBox1.Size = new System.Drawing.Size(195, 59);
            this.myGroupBox1.TabIndex = 42;
            this.myGroupBox1.TabStop = false;
            // 
            // rdoBtnFolderPost14
            // 
            this.rdoBtnFolderPost14.AutoSize = true;
            this.rdoBtnFolderPost14.BackColor = System.Drawing.SystemColors.Control;
            this.rdoBtnFolderPost14.Checked = true;
            this.rdoBtnFolderPost14.Enabled = false;
            this.rdoBtnFolderPost14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoBtnFolderPost14.ForeColor = System.Drawing.Color.Silver;
            this.rdoBtnFolderPost14.Location = new System.Drawing.Point(14, 34);
            this.rdoBtnFolderPost14.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdoBtnFolderPost14.Name = "rdoBtnFolderPost14";
            this.rdoBtnFolderPost14.Size = new System.Drawing.Size(161, 17);
            this.rdoBtnFolderPost14.TabIndex = 1;
            this.rdoBtnFolderPost14.TabStop = true;
            this.rdoBtnFolderPost14.Text = "Post2014(ARANxx_Posdata)";
            this.rdoBtnFolderPost14.UseVisualStyleBackColor = false;
            // 
            // rdoBtnFolderPre14
            // 
            this.rdoBtnFolderPre14.AutoSize = true;
            this.rdoBtnFolderPre14.Enabled = false;
            this.rdoBtnFolderPre14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoBtnFolderPre14.ForeColor = System.Drawing.Color.Silver;
            this.rdoBtnFolderPre14.Location = new System.Drawing.Point(18, 9);
            this.rdoBtnFolderPre14.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdoBtnFolderPre14.Name = "rdoBtnFolderPre14";
            this.rdoBtnFolderPre14.Size = new System.Drawing.Size(110, 17);
            this.rdoBtnFolderPre14.TabIndex = 0;
            this.rdoBtnFolderPre14.Text = "Pre2014(Posdata)";
            this.rdoBtnFolderPre14.UseVisualStyleBackColor = true;
            // 
            // grpBxBrowser
            // 
            this.grpBxBrowser.BorderColor = System.Drawing.Color.Black;
            this.grpBxBrowser.Controls.Add(this.rdoBtnGoogleChrome);
            this.grpBxBrowser.Controls.Add(this.rdoBtnInternetExplorer);
            this.grpBxBrowser.Location = new System.Drawing.Point(550, 19);
            this.grpBxBrowser.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.grpBxBrowser.Name = "grpBxBrowser";
            this.grpBxBrowser.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.grpBxBrowser.Size = new System.Drawing.Size(321, 38);
            this.grpBxBrowser.TabIndex = 41;
            this.grpBxBrowser.TabStop = false;
            // 
            // rdoBtnGoogleChrome
            // 
            this.rdoBtnGoogleChrome.AutoSize = true;
            this.rdoBtnGoogleChrome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoBtnGoogleChrome.ForeColor = System.Drawing.Color.Blue;
            this.rdoBtnGoogleChrome.Location = new System.Drawing.Point(177, 6);
            this.rdoBtnGoogleChrome.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdoBtnGoogleChrome.Name = "rdoBtnGoogleChrome";
            this.rdoBtnGoogleChrome.Size = new System.Drawing.Size(139, 24);
            this.rdoBtnGoogleChrome.TabIndex = 1;
            this.rdoBtnGoogleChrome.Text = "Google Chrome";
            this.rdoBtnGoogleChrome.UseVisualStyleBackColor = true;
            this.rdoBtnGoogleChrome.CheckedChanged += new System.EventHandler(this.rdoBtnGoogleChrome_CheckedChanged);
            // 
            // rdoBtnInternetExplorer
            // 
            this.rdoBtnInternetExplorer.AutoSize = true;
            this.rdoBtnInternetExplorer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoBtnInternetExplorer.ForeColor = System.Drawing.Color.Blue;
            this.rdoBtnInternetExplorer.Location = new System.Drawing.Point(20, 6);
            this.rdoBtnInternetExplorer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdoBtnInternetExplorer.Name = "rdoBtnInternetExplorer";
            this.rdoBtnInternetExplorer.Size = new System.Drawing.Size(145, 24);
            this.rdoBtnInternetExplorer.TabIndex = 0;
            this.rdoBtnInternetExplorer.Text = "Internet Explorer";
            this.rdoBtnInternetExplorer.UseVisualStyleBackColor = true;
            this.rdoBtnInternetExplorer.CheckedChanged += new System.EventHandler(this.rdoBtnInternetExplorer_CheckedChanged);
            // 
            // frmSBETDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1082, 838);
            this.Controls.Add(this.PinPoint);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SBETQC);
            this.Controls.Add(this.ReportBackup);
            this.Controls.Add(this.lblBrowser);
            this.Controls.Add(this.lblVideoLocationFolderStructure);
            this.Controls.Add(this.lblServerFolder);
            this.Controls.Add(this.btnGetServerFolder);
            this.Controls.Add(this.txtBxServerFolder);
            this.Controls.Add(this.btnStartBatchProcess);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmboBxProjects);
            this.Controls.Add(this.lblStartDateAndTime);
            this.Controls.Add(this.dtTmPkrStartDateAndTime);
            this.Controls.Add(this.lblGNSSMode);
            this.Controls.Add(this.cmboBxGNSSMode);
            this.Controls.Add(this.lblTemplateFile);
            this.Controls.Add(this.btnGetTemplateFile);
            this.Controls.Add(this.txtBxTemplateFile);
            this.Controls.Add(this.lblLocalFolder);
            this.Controls.Add(this.btnGetLocalFolder);
            this.Controls.Add(this.myGroupBox1);
            this.Controls.Add(this.txtBxLocalFolder);
            this.Controls.Add(this.lblProjects);
            this.Controls.Add(this.btnRefreshProjects);
            this.Controls.Add(this.grpBxBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmSBETDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "iSBET™";
            this.Shown += new System.EventHandler(this.frmSBETDialog_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bndngNav)).EndInit();
            this.bndngNav.ResumeLayout(false);
            this.bndngNav.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtaGrdVwBatchDt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bndngSrc)).EndInit();
            this.myGroupBox1.ResumeLayout(false);
            this.myGroupBox1.PerformLayout();
            this.grpBxBrowser.ResumeLayout(false);
            this.grpBxBrowser.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblProjects;
    private System.Windows.Forms.Button btnRefreshProjects;
    private System.Windows.Forms.Label lblLocalFolder;
    private System.Windows.Forms.Button btnGetLocalFolder;
    private System.Windows.Forms.Label lblTemplateFile;
    private System.Windows.Forms.Button btnGetTemplateFile;
    private System.Windows.Forms.TextBox txtBxTemplateFile;
    private System.Windows.Forms.ComboBox cmboBxGNSSMode;
    private System.Windows.Forms.Label lblGNSSMode;
    private System.Windows.Forms.DateTimePicker dtTmPkrStartDateAndTime;
    private System.Windows.Forms.Label lblStartDateAndTime;
    private System.Windows.Forms.Timer tmrStartDateAndTime;
    private System.Windows.Forms.Button btnStartBatchProcess;
    private System.Windows.Forms.ComboBox cmboBxProjects;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.BindingNavigator bndngNav;
    private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
    private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
    private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
    private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
    private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
    private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
    private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
    private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
    private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
    private myGroupBox grpBxBrowser;
    private System.Windows.Forms.RadioButton rdoBtnGoogleChrome;
    private System.Windows.Forms.RadioButton rdoBtnInternetExplorer;
    private myGroupBox grpBxProcessType;
    private System.Windows.Forms.RadioButton rdoBtnBatchOnly;
    private System.Windows.Forms.RadioButton rdoBtnBatchAndExport;
    private System.Windows.Forms.Label lblServerFolder;
    private System.Windows.Forms.Button btnGetServerFolder;
    private myGroupBox myGroupBox1;
    private System.Windows.Forms.RadioButton rdoBtnFolderPost14;
    private System.Windows.Forms.RadioButton rdoBtnFolderPre14;
    private System.Windows.Forms.Label lblVideoLocationFolderStructure;
    private System.Windows.Forms.ToolTip tlTp;
        private System.Windows.Forms.Label lblBrowser;
        private System.Windows.Forms.Button ReportBackup;
        private System.Windows.Forms.Button SBETQC;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox txtBxLocalFolder;
        public System.Windows.Forms.DataGridView dtaGrdVwBatchDt;
        public System.Windows.Forms.BindingSource bndngSrc;
        public System.Windows.Forms.TextBox txtBxServerFolder;
        public System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button PinPoint;
    }
}
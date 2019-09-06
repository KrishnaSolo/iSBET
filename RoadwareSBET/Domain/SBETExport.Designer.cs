namespace RoadwareSBET.Domain
{
  partial class frmSBETBatchExport
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSBETBatchExport));
      this.lblStatus = new System.Windows.Forms.Label();
      this.btnExport = new System.Windows.Forms.Button();
      this.lblFolders = new System.Windows.Forms.Label();
      this.ckLstBx = new System.Windows.Forms.CheckedListBox();
      this.btnChangeLocalFolder = new System.Windows.Forms.Button();
      this.grpBxExportType = new RoadwareSBET.Domain.myGroupBox();
      this.rdoBtnMission = new System.Windows.Forms.RadioButton();
      this.rdoBtnBatch = new System.Windows.Forms.RadioButton();
      this.lblExportType = new System.Windows.Forms.Label();
      this.ckBxMigrateToServer = new System.Windows.Forms.CheckBox();
      this.grpBxExportType.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblStatus
      // 
      this.lblStatus.AutoSize = true;
      this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblStatus.ForeColor = System.Drawing.Color.Blue;
      this.lblStatus.Location = new System.Drawing.Point(7, 551);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(68, 25);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      // 
      // btnExport
      // 
      this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnExport.ForeColor = System.Drawing.Color.Blue;
      this.btnExport.Location = new System.Drawing.Point(933, 230);
      this.btnExport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.btnExport.Name = "btnExport";
      this.btnExport.Size = new System.Drawing.Size(275, 39);
      this.btnExport.TabIndex = 29;
      this.btnExport.Text = "Batch Export";
      this.btnExport.UseVisualStyleBackColor = true;
      this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
      // 
      // lblFolders
      // 
      this.lblFolders.AutoSize = true;
      this.lblFolders.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblFolders.ForeColor = System.Drawing.Color.Blue;
      this.lblFolders.Location = new System.Drawing.Point(12, 4);
      this.lblFolders.Name = "lblFolders";
      this.lblFolders.Size = new System.Drawing.Size(152, 25);
      this.lblFolders.TabIndex = 30;
      this.lblFolders.Text = "Batch Folders in";
      // 
      // ckLstBx
      // 
      this.ckLstBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ckLstBx.ForeColor = System.Drawing.Color.Blue;
      this.ckLstBx.FormattingEnabled = true;
      this.ckLstBx.Location = new System.Drawing.Point(12, 32);
      this.ckLstBx.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.ckLstBx.Name = "ckLstBx";
      this.ckLstBx.Size = new System.Drawing.Size(889, 454);
      this.ckLstBx.TabIndex = 31;
      // 
      // btnChangeLocalFolder
      // 
      this.btnChangeLocalFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnChangeLocalFolder.ForeColor = System.Drawing.Color.Blue;
      this.btnChangeLocalFolder.Location = new System.Drawing.Point(933, 32);
      this.btnChangeLocalFolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.btnChangeLocalFolder.Name = "btnChangeLocalFolder";
      this.btnChangeLocalFolder.Size = new System.Drawing.Size(275, 39);
      this.btnChangeLocalFolder.TabIndex = 32;
      this.btnChangeLocalFolder.Text = "Change Local Folder";
      this.btnChangeLocalFolder.UseVisualStyleBackColor = true;
      this.btnChangeLocalFolder.Click += new System.EventHandler(this.btnChangeLocalFolder_Click);
      // 
      // grpBxExportType
      // 
      this.grpBxExportType.BorderColor = System.Drawing.Color.Black;
      this.grpBxExportType.Controls.Add(this.rdoBtnMission);
      this.grpBxExportType.Controls.Add(this.rdoBtnBatch);
      this.grpBxExportType.Location = new System.Drawing.Point(959, 140);
      this.grpBxExportType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.grpBxExportType.Name = "grpBxExportType";
      this.grpBxExportType.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.grpBxExportType.Size = new System.Drawing.Size(219, 62);
      this.grpBxExportType.TabIndex = 2;
      this.grpBxExportType.TabStop = false;
      // 
      // rdoBtnMission
      // 
      this.rdoBtnMission.AutoSize = true;
      this.rdoBtnMission.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.rdoBtnMission.ForeColor = System.Drawing.Color.Blue;
      this.rdoBtnMission.Location = new System.Drawing.Point(112, 22);
      this.rdoBtnMission.Margin = new System.Windows.Forms.Padding(4);
      this.rdoBtnMission.Name = "rdoBtnMission";
      this.rdoBtnMission.Size = new System.Drawing.Size(100, 29);
      this.rdoBtnMission.TabIndex = 2;
      this.rdoBtnMission.Text = "Mission";
      this.rdoBtnMission.UseVisualStyleBackColor = true;
      // 
      // rdoBtnBatch
      // 
      this.rdoBtnBatch.AutoSize = true;
      this.rdoBtnBatch.Checked = true;
      this.rdoBtnBatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.rdoBtnBatch.ForeColor = System.Drawing.Color.Blue;
      this.rdoBtnBatch.Location = new System.Drawing.Point(7, 22);
      this.rdoBtnBatch.Margin = new System.Windows.Forms.Padding(4);
      this.rdoBtnBatch.Name = "rdoBtnBatch";
      this.rdoBtnBatch.Size = new System.Drawing.Size(83, 29);
      this.rdoBtnBatch.TabIndex = 1;
      this.rdoBtnBatch.TabStop = true;
      this.rdoBtnBatch.Text = "Batch";
      this.rdoBtnBatch.UseVisualStyleBackColor = true;
      // 
      // lblExportType
      // 
      this.lblExportType.AutoSize = true;
      this.lblExportType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblExportType.ForeColor = System.Drawing.Color.Blue;
      this.lblExportType.Location = new System.Drawing.Point(1005, 129);
      this.lblExportType.Name = "lblExportType";
      this.lblExportType.Size = new System.Drawing.Size(129, 25);
      this.lblExportType.TabIndex = 48;
      this.lblExportType.Text = "Export Type";
      // 
      // ckBxMigrateToServer
      // 
      this.ckBxMigrateToServer.AutoSize = true;
      this.ckBxMigrateToServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ckBxMigrateToServer.ForeColor = System.Drawing.Color.Blue;
      this.ckBxMigrateToServer.Location = new System.Drawing.Point(959, 86);
      this.ckBxMigrateToServer.Name = "ckBxMigrateToServer";
      this.ckBxMigrateToServer.Size = new System.Drawing.Size(208, 29);
      this.ckBxMigrateToServer.TabIndex = 49;
      this.ckBxMigrateToServer.Text = "Migrate To Server";
      this.ckBxMigrateToServer.UseVisualStyleBackColor = true;
      this.ckBxMigrateToServer.Visible = false;
      this.ckBxMigrateToServer.Click += new System.EventHandler(this.ckBxMigrateToServer_Click);
      // 
      // frmSBETBatchExport
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1220, 586);
      this.Controls.Add(this.ckBxMigrateToServer);
      this.Controls.Add(this.lblExportType);
      this.Controls.Add(this.grpBxExportType);
      this.Controls.Add(this.btnChangeLocalFolder);
      this.Controls.Add(this.ckLstBx);
      this.Controls.Add(this.lblFolders);
      this.Controls.Add(this.btnExport);
      this.Controls.Add(this.lblStatus);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.Name = "frmSBETBatchExport";
      this.Text = "SBET Batch Export";
      this.grpBxExportType.ResumeLayout(false);
      this.grpBxExportType.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Button btnExport;
    private System.Windows.Forms.Label lblFolders;
    private System.Windows.Forms.CheckedListBox ckLstBx;
    private System.Windows.Forms.Button btnChangeLocalFolder;
    private myGroupBox grpBxExportType;
    private System.Windows.Forms.RadioButton rdoBtnMission;
    private System.Windows.Forms.RadioButton rdoBtnBatch;
    private System.Windows.Forms.Label lblExportType;
    private System.Windows.Forms.CheckBox ckBxMigrateToServer;
  }
}
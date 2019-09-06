namespace RoadwareSBET.Domain
{
  partial class frmSBETBadBatch
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSBETBadBatch));
      this.lblStatus = new System.Windows.Forms.Label();
      this.btnExport = new System.Windows.Forms.Button();
      this.lblFolders = new System.Windows.Forms.Label();
      this.ckLstBx = new System.Windows.Forms.CheckedListBox();
      this.btnChangeLocalFolder = new System.Windows.Forms.Button();
      this.lblProcessType = new System.Windows.Forms.Label();
      this.grpBxProcessType = new RoadwareSBET.Domain.myGroupBox();
      this.rdoBtnBatchOnly = new System.Windows.Forms.RadioButton();
      this.rdoBtnBatchAndExport = new System.Windows.Forms.RadioButton();
      this.grpBxProcessType.SuspendLayout();
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
      this.btnExport.Location = new System.Drawing.Point(1328, 198);
      this.btnExport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.btnExport.Name = "btnExport";
      this.btnExport.Size = new System.Drawing.Size(275, 39);
      this.btnExport.TabIndex = 29;
      this.btnExport.Text = "Process Bad Batch(es)";
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
      this.lblFolders.Size = new System.Drawing.Size(168, 25);
      this.lblFolders.TabIndex = 30;
      this.lblFolders.Text = "Bad Batch Files in";
      // 
      // ckLstBx
      // 
      this.ckLstBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ckLstBx.ForeColor = System.Drawing.Color.Blue;
      this.ckLstBx.FormattingEnabled = true;
      this.ckLstBx.Location = new System.Drawing.Point(12, 32);
      this.ckLstBx.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.ckLstBx.Name = "ckLstBx";
      this.ckLstBx.Size = new System.Drawing.Size(1289, 479);
      this.ckLstBx.TabIndex = 31;
      this.ckLstBx.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ckLstBx_ItemCheck);
      // 
      // btnChangeLocalFolder
      // 
      this.btnChangeLocalFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnChangeLocalFolder.ForeColor = System.Drawing.Color.Blue;
      this.btnChangeLocalFolder.Location = new System.Drawing.Point(1328, 32);
      this.btnChangeLocalFolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.btnChangeLocalFolder.Name = "btnChangeLocalFolder";
      this.btnChangeLocalFolder.Size = new System.Drawing.Size(275, 39);
      this.btnChangeLocalFolder.TabIndex = 32;
      this.btnChangeLocalFolder.Text = "Change Local Folder";
      this.btnChangeLocalFolder.UseVisualStyleBackColor = true;
      this.btnChangeLocalFolder.Click += new System.EventHandler(this.btnChangeLocalFolder_Click);
      // 
      // lblProcessType
      // 
      this.lblProcessType.AutoSize = true;
      this.lblProcessType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblProcessType.ForeColor = System.Drawing.Color.Blue;
      this.lblProcessType.Location = new System.Drawing.Point(1393, 101);
      this.lblProcessType.Name = "lblProcessType";
      this.lblProcessType.Size = new System.Drawing.Size(145, 25);
      this.lblProcessType.TabIndex = 49;
      this.lblProcessType.Text = "Process Type";
      // 
      // grpBxProcessType
      // 
      this.grpBxProcessType.BorderColor = System.Drawing.Color.Black;
      this.grpBxProcessType.Controls.Add(this.rdoBtnBatchOnly);
      this.grpBxProcessType.Controls.Add(this.rdoBtnBatchAndExport);
      this.grpBxProcessType.Location = new System.Drawing.Point(1308, 116);
      this.grpBxProcessType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.grpBxProcessType.Name = "grpBxProcessType";
      this.grpBxProcessType.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.grpBxProcessType.Size = new System.Drawing.Size(314, 46);
      this.grpBxProcessType.TabIndex = 48;
      this.grpBxProcessType.TabStop = false;
      // 
      // rdoBtnBatchOnly
      // 
      this.rdoBtnBatchOnly.AutoSize = true;
      this.rdoBtnBatchOnly.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.rdoBtnBatchOnly.ForeColor = System.Drawing.Color.Blue;
      this.rdoBtnBatchOnly.Location = new System.Drawing.Point(174, 9);
      this.rdoBtnBatchOnly.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.rdoBtnBatchOnly.Name = "rdoBtnBatchOnly";
      this.rdoBtnBatchOnly.Size = new System.Drawing.Size(129, 29);
      this.rdoBtnBatchOnly.TabIndex = 1;
      this.rdoBtnBatchOnly.Text = "Batch Only";
      this.rdoBtnBatchOnly.UseVisualStyleBackColor = true;
      // 
      // rdoBtnBatchAndExport
      // 
      this.rdoBtnBatchAndExport.AutoSize = true;
      this.rdoBtnBatchAndExport.Checked = true;
      this.rdoBtnBatchAndExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.rdoBtnBatchAndExport.ForeColor = System.Drawing.Color.Blue;
      this.rdoBtnBatchAndExport.Location = new System.Drawing.Point(6, 9);
      this.rdoBtnBatchAndExport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.rdoBtnBatchAndExport.Name = "rdoBtnBatchAndExport";
      this.rdoBtnBatchAndExport.Size = new System.Drawing.Size(162, 29);
      this.rdoBtnBatchAndExport.TabIndex = 0;
      this.rdoBtnBatchAndExport.TabStop = true;
      this.rdoBtnBatchAndExport.Text = "Batch && Export";
      this.rdoBtnBatchAndExport.UseVisualStyleBackColor = true;
      // 
      // frmSBETBadBatch
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1658, 586);
      this.Controls.Add(this.lblProcessType);
      this.Controls.Add(this.grpBxProcessType);
      this.Controls.Add(this.btnChangeLocalFolder);
      this.Controls.Add(this.ckLstBx);
      this.Controls.Add(this.lblFolders);
      this.Controls.Add(this.btnExport);
      this.Controls.Add(this.lblStatus);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.Name = "frmSBETBadBatch";
      this.Text = "Bad Batch SBET Processing";
      this.grpBxProcessType.ResumeLayout(false);
      this.grpBxProcessType.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Button btnExport;
    private System.Windows.Forms.Label lblFolders;
    private System.Windows.Forms.CheckedListBox ckLstBx;
    private System.Windows.Forms.Button btnChangeLocalFolder;
    private System.Windows.Forms.Label lblProcessType;
    private myGroupBox grpBxProcessType;
    private System.Windows.Forms.RadioButton rdoBtnBatchOnly;
    private System.Windows.Forms.RadioButton rdoBtnBatchAndExport;
  }
}
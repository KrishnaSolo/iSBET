using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RoadwareSBETClassLibrary.Domain;
using IronPython.Hosting;
using System.Data.SqlClient;
using System.Data.Odbc;
using Microsoft.Scripting.Hosting;
using System.Xml;
using System.Net.Mail;
using System.Runtime.InteropServices;


namespace RoadwareSBET.Domain
{
  public partial class frmSBETDialog : Form
  {
    #region fields
    private DataSet fNfDs;
    public DataTable fNfDt;
    private DataTable projDt;
    public DataTable batchDt = new DataTable();

    private bool completed;
    private bool evefin;
    private bool repup;
    private string fNfSBETPOSPacExportPy;
    private string posPacExe;
    private string prjcts = string.Empty;
    private string pyExeFlNm;
    private string strLocalProjectFolder;
    private string strServerProjectFolder;
    private string strTemplateFile;

    private string xmlFilesAndFoldersNm = "FoldersAndFiles.xml";
    private string xmlGNSSModesNm = @"Data\GNSSModes.xml";
    List<string> inc = new List<string>();
    List<string> failed = new List<string>();
    private TimeSpan timeLeft;
    private bool statusupdate;
    #endregion
    
    #region constructors
    public frmSBETDialog()
    {
      InitializeComponent();
      InitializeControls();
      ReportBackup.Enabled = false;
    }
    #endregion

    #region methods
    private void BackupToServer(string prjNmBatNm)
    {

      string btchPrjFullFlNm = Path.Combine(strLocalProjectFolder,
                                            string.Format("{0}.posbat", prjNmBatNm));
      if (File.Exists(btchPrjFullFlNm))
      {
        string xmlPrjFullFlNm = string.Empty;
        if (prjNmBatNm.Substring(prjNmBatNm.Length - 3, 3).ToUpper() != "BAD")
        {
          xmlPrjFullFlNm = Path.Combine(strLocalProjectFolder,
                                        string.Format("{0}.xml", prjNmBatNm));
        }
        string srcFldr = Path.Combine(strLocalProjectFolder, prjNmBatNm);
        Common cmn = new Common();
        string svrFldr = cmn.GetServerFolderFromPosBatFile(btchPrjFullFlNm);
        if (!string.IsNullOrEmpty(svrFldr) && Directory.Exists(srcFldr))
        {
          //Copy the .posbat folder results
          cmn.DirectoryCopy(srcFldr, Path.Combine(svrFldr, prjNmBatNm), true, true);

          //Copy the .posbat file
          File.Copy(btchPrjFullFlNm, Path.Combine(svrFldr, string.Format("{0}.posbat", prjNmBatNm)), true);
          File.Delete(btchPrjFullFlNm);

          //Copy the .xml file
          if (!string.IsNullOrEmpty(xmlPrjFullFlNm))
          {
            File.Copy(xmlPrjFullFlNm, Path.Combine(svrFldr, string.Format("{0}.xml", prjNmBatNm)), true);
            File.Delete(xmlPrjFullFlNm);
          }
        }
        else
        {
          MessageBox.Show(string.Format("Server folder, {0}, could not be found.", svrFldr),
                          "ERROR!!",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
        }
      }
    }

    private void btnBackupToServer_Click(object sender, EventArgs e)
    {

      if (!IsBatchFileSelected()) { return; }
      Cursor.Current = Cursors.WaitCursor;
      ChangeControlStatus(false);
      //btnProcessBadBatch.Enabled = false;
      DataRow[] drs = batchDt.Select("[Process] = True", "[Project Name], [Batch Name]");
      foreach (DataRow dr in drs)
      {


        string dbNmBatNm = string.Format("{0}_{1}", dr["Database Name"].ToString(),
                                                     dr["Batch Name"].ToString());

        lblStatus.Text = string.Format("Backing \"{0}\" up to server.", dbNmBatNm);
        lblStatus.Update();
        Application.DoEvents();

        BackupToServer(dbNmBatNm);

        lblStatus.Text = string.Format("\"{0}\" backed up to server.", dbNmBatNm);
        lblStatus.Update();
        Application.DoEvents();

        dbNmBatNm = string.Format("{0}_{1}_BAD", dr["Database Name"].ToString(),
                                                  dr["Batch Name"].ToString());
        lblStatus.Text = string.Format("Backing \"{0}\" up to server.", dbNmBatNm);
        lblStatus.Update();
        Application.DoEvents();

        BackupToServer(dbNmBatNm);

        lblStatus.Text = string.Format("\"{0}\" backed up to server.", dbNmBatNm);
        lblStatus.Update();
        Application.DoEvents();

      }

      lblStatus.Text = "All data backed up to server.";
      lblStatus.Update();

      Cursor.Current = Cursors.Default;
      ChangeControlStatus(true);
      //btnProcessBadBatch.Enabled = true;
    }

    private void btnGetLocalFolder_Click(object sender, EventArgs e)//3
    {
      lblStatus.Text = "Getting local folder.";
      lblStatus.Update();
      FolderBrowserDialog fbDlg = new FolderBrowserDialog();
      if (!string.IsNullOrEmpty(txtBxLocalFolder.Text)) { fbDlg.SelectedPath = txtBxLocalFolder.Text; }
      if (fbDlg.ShowDialog() == DialogResult.OK)
      {
        txtBxLocalFolder.Text = fbDlg.SelectedPath;
        UpdateTextBoxChanges(ref txtBxLocalFolder, ref strLocalProjectFolder, "LocalProjectFolder");
        lblStatus.Text = "Local folder updated.";
        lblStatus.Update();
      }
      else
      {
        lblStatus.Text = "Local folder not updated.";
        lblStatus.Update();
      }
    }

    private void btnGetServerFolder_Click(object sender, EventArgs e)
    {
      lblStatus.Text = "Getting server folder.";
      lblStatus.Update();
      FolderBrowserDialog fbDlg = new FolderBrowserDialog();
      if (!string.IsNullOrEmpty(txtBxServerFolder.Text)) { fbDlg.SelectedPath = txtBxServerFolder.Text; }
      if (fbDlg.ShowDialog() == DialogResult.OK)
      {
        txtBxServerFolder.Text = fbDlg.SelectedPath;
        UpdateTextBoxChanges(ref txtBxServerFolder, ref strServerProjectFolder, "ServerProjectFolder");
        lblStatus.Text = "Server folder updated.";
        lblStatus.Update();
      }
      else
      {
        lblStatus.Text = "Server folder not updated.";
        lblStatus.Update();
      }

    }

    private void btnGetTemplateFile_Click(object sender, EventArgs e)//4
    {
      lblStatus.Text = "Getting template file.";
      lblStatus.Update();
      OpenFileDialog ofDlg = new OpenFileDialog();
      if (!string.IsNullOrEmpty(txtBxTemplateFile.Text))
      {
        ofDlg.InitialDirectory = Path.GetDirectoryName(txtBxTemplateFile.Text);
      }
      ofDlg.Filter = "postml Files (*.postml)|*.postml";
      ofDlg.FilterIndex = 1;
      ofDlg.Multiselect = false;
      ofDlg.Title = "Select POSPac Template File";
      if (ofDlg.ShowDialog() == DialogResult.OK)
      {
        txtBxTemplateFile.Text = ofDlg.FileNames[0];
        UpdateTextBoxChanges(ref txtBxTemplateFile, ref strTemplateFile, "TemplateFile");
        lblStatus.Text = "Template file updated.";
        lblStatus.Update();
      }
      else
      {
        lblStatus.Text = "Template file not updated.";
        lblStatus.Update();
      }
    }

    private void btnRefreshProjects_Click(object sender, EventArgs e)
    {
      LoadProjects();
    }

    private void btnSBETExport_Click(object sender, EventArgs e)
    {
      lblStatus.Text = "Starting SBET export process.";
      lblStatus.Update();
      Form btchExpFrm = new frmSBETBatchExport(txtBxLocalFolder.Text,
                                               pyExeFlNm,
                                               fNfSBETPOSPacExportPy,
                                               posPacExe);
      btchExpFrm.ShowDialog();
      lblStatus.Text = "SBET export process completed.";
      lblStatus.Update();
    }

    private void btnStartBatchProcess_Click(object sender, EventArgs e)//5
    {
      Cursor.Current = Cursors.WaitCursor;
      //check if projects have been loaded.
      if (!IsProjectLoaded()) { return; }
      //check if any batch file have been selected.
      if (!IsBatchFileSelected()) { return; }

      if (btnStartBatchProcess.Text == "Cancel")
      {
        ChangeControlStatus(true);
        tmrStartDateAndTime.Stop();
        lblStatus.Text = "Batch process cancelled.";
        lblStatus.Update();
        btnStartBatchProcess.Text = "Start Batch Process";
        return;
      }
      else
      {
        ChangeControlStatus(false);
        btnStartBatchProcess.Text = "Cancel";
      }
      //check if any text box values have changed and update them
      UpdateTextBoxChanges(ref txtBxLocalFolder, ref strLocalProjectFolder, "LocalProjectFolder");
      UpdateTextBoxChanges(ref txtBxServerFolder, ref strServerProjectFolder, "ServerProjectFolder");
      UpdateTextBoxChanges(ref txtBxTemplateFile, ref strTemplateFile, "TemplateFile");
      /*
        Here a filter will be added double checking all filenames to make sure they exist and cross check db with salesforce and update user
        if paths either 1) are project paths which dont exists ->create 2)incorret typed in names -> pause and get user to fix this first
        Another issue: update user if batch choosen will have date coverage done already - less strict more about saving time
      */

      lblStatus.Text = "Setting up batch(es) for processing.";
      lblStatus.Update();
      SetupBatchesForProcessing();
      lblStatus.Text = "Batch(es) set up for processing.";
      lblStatus.Update();

      timeLeft = dtTmPkrStartDateAndTime.Value.Subtract(DateTime.Now);
      tmrStartDateAndTime.Start();

        //where is it running everything
    }

    private void ChangeControlStatus(bool enableMode)
    {
      btnGetLocalFolder.Enabled = enableMode;
      btnRefreshProjects.Enabled = enableMode;
      btnGetServerFolder.Enabled = enableMode;
      btnGetTemplateFile.Enabled = enableMode;
      //btnBackupToServer.Enabled = enableMode;
      //btnSBETExport.Enabled = enableMode;
      cmboBxGNSSMode.Enabled = enableMode;
      cmboBxProjects.Enabled = enableMode;
      dtTmPkrStartDateAndTime.Enabled = enableMode;
      //grpBxProcessType.Enabled = enableMode;
      rdoBtnInternetExplorer.Enabled = enableMode;
      rdoBtnGoogleChrome.Enabled = enableMode;
    }

    private void cmboBxGNSSMode_SelectedIndexChanged(object sender, EventArgs e)
    {
      lblStatus.Text = string.Empty;
      lblStatus.Update();
    }

    private void cmboBxProjects_SelectedIndexChanged(object sender, EventArgs e)
    {
      lblStatus.Text = string.Empty;
      lblStatus.Update();
      if (batchDt.Rows.Count > 0)
      {
        LoadBatches();
 
      }
    }

    private void CreateDataTableStructures()//1.1
    {
      List<DataTableField> flds = new List<DataTableField>();
      DataTableField dtFld;
      Common cmn = new Common();

      //Session Data Table
      dtFld = new DataTableField();
      dtFld.Name = "Project Name"; dtFld.Type = typeof(System.String); flds.Add(dtFld);
      dtFld = new DataTableField();
      dtFld.Name = "GPS Post-Processing Imported Date"; dtFld.Type = typeof(System.String); flds.Add(dtFld);
      dtFld = new DataTableField();
      dtFld.Name = "GPS Post-Processing Required Date"; dtFld.Type = typeof(System.String); flds.Add(dtFld);
      dtFld = new DataTableField();
      dtFld.Name = "GPS Post-Processing Technician"; dtFld.Type = typeof(System.String); flds.Add(dtFld);
      dtFld = new DataTableField();
      dtFld.Name = "Batch Name"; dtFld.Type = typeof(System.String); flds.Add(dtFld);
      dtFld = new DataTableField();
      dtFld.Name = "ARAN"; dtFld.Type = typeof(System.String); flds.Add(dtFld);
      dtFld = new DataTableField();
      dtFld.Name = "Data Collection From Date"; dtFld.Type = typeof(System.String); flds.Add(dtFld);
      dtFld = new DataTableField();
      dtFld.Name = "Data Collection To Date"; dtFld.Type = typeof(System.String); flds.Add(dtFld);
      dtFld = new DataTableField();
      dtFld.Name = "Server Name"; dtFld.Type = typeof(System.String); flds.Add(dtFld);
      dtFld = new DataTableField();
      dtFld.Name = "Database Name"; dtFld.Type = typeof(System.String); flds.Add(dtFld);
      dtFld = new DataTableField();
      dtFld.Name = "Video Location"; dtFld.Type = typeof(System.String); flds.Add(dtFld);
      dtFld = new DataTableField();
      dtFld.Name = "Backup Tape Location"; dtFld.Type = typeof(System.String); flds.Add(dtFld);
      projDt = cmn.CreateNewDataTable(flds, "Projects Data Table");
    }

    private void dtaGrdVwBatchDt_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      DataGridView dgv = sender as DataGridView;
      if (dgv == null) { return; }
      dgv.Refresh();
      if (dgv.CurrentRow.Selected)
      {
      //  //bndngSrc.EndEdit();
      //  SendKeys.Send("^~");
        return;
      }
      bool cv;
      if (!bool.TryParse(dgv.CurrentCell.Value.ToString(), out cv)) { cv = false; }

      if (dgv.CurrentCell.ColumnIndex.Equals(0) && e.RowIndex != -1 && cv)
      {
        DataRowView drv = (DataRowView)dgv.CurrentRow.DataBoundItem;
        drv["Template File"] = string.Empty;
        dgv.Refresh();
        SendKeys.Send("^~");
   
        string backup = strLocalProjectFolder;
        if (string.IsNullOrEmpty(backup))
        {
            ReportBackup.Enabled = false;
        }
        else
        {
            DataRow[] fltr = null;
            try
            {
                fltr = batchDt.Select("[Process] = True", "[Backup Tape Location], [Batch Name], [Video Location]");
            }
            catch
            {

                ReportBackup.Enabled = false;

            }
            foreach (DataRow i in fltr)
            {
                string batch = i["Batch Name"].ToString();
                string projpath = i["Video Location"].ToString();
                string[] arra = projpath.Split('\\');
                string proj1 = arra[arra.Length - 1];
                string batchP = i["Backup Tape Location"].ToString();
                //backup = projpath;

                string b = batch; string fbtch = b;
                int ns = b.Length;


                if (b.Contains("CTRL"))
                {
                    batch = b;
                }
                else
                {
                    batch = "Batch_" + b;
                }
                

                string fullname = backup + @"\Data\" + batch;
                        Console.WriteLine(fullname);
         
                if (Directory.Exists(fullname))
                {
                    FileInfo[] a = null; DirectoryInfo check = null;
                    try
                    {
                        check = new DirectoryInfo(fullname);
                        a = check.GetFiles();
                    }
                    catch
                    {
                        ReportBackup.Enabled = false;
                    }

                    if (a.Count() == 0)
                    {

                        ReportBackup.Enabled = false;
                    }

                    else
                    {
                        ReportBackup.Enabled = true;
                    }
                }
                else
                {

                            ReportBackup.Enabled = false;
                }
            }
        }
                return;
      }
      if (dgv.CurrentCell.ColumnIndex.Equals(0) && e.RowIndex != -1 && !cv)
      {
        DataRowView drv = (DataRowView)dgv.CurrentRow.DataBoundItem;
        if (drv["ARAN"] != DBNull.Value && !string.IsNullOrEmpty(drv["ARAN"].ToString()))
        {
          drv["Template File"] = txtBxTemplateFile.Text;
        }
        else
        {
          MessageBox.Show("Can not process this batch, missing ARAN Id.", "ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        dgv.Refresh();
        SendKeys.Send("^~");
        return;
      }
    }

    private void dtaGrdVwBatchDt_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      DataGridView dgv = sender as DataGridView;
      if (dgv == null) { return; }
      dgv.Refresh();
      bool cv;
      if (!bool.TryParse(dgv.CurrentCell.Value.ToString(), out cv)) { cv = false; }

      DataRowView drv = (DataRowView)dgv.CurrentRow.DataBoundItem;
      if (dgv.CurrentCell.ColumnIndex.Equals(0) && e.RowIndex != -1 &&
          cv &&
          (drv["ARAN"] == DBNull.Value || string.IsNullOrEmpty(drv["ARAN"].ToString())))
      {
        drv["Process"] = false;
        dgv.Refresh();
      }
    }
    
    private void dtTmPkrStartDateAndTime_ValueChanged(object sender, EventArgs e)
    {
      lblStatus.Text = string.Empty;
      lblStatus.Update();
    }

    private void frmSBETDialog_Shown(object sender, EventArgs e)
    {
      MessageBox.Show("Click \"Refresh Projects\" and use your web browser to save a list of the most current projects needing SBET batch processing",
                "Action Needed!!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
    }

    private List<FileInfo> GetCSVFiles()//2.1
    {
      string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
      string pathDwnld = Path.Combine(pathUser, "Downloads");
      return new DirectoryInfo(pathDwnld).GetFiles("*.csv").OrderByDescending(f => f.LastWriteTime).ToList();
    }

    private string GetNewComment(string exstCmnt, string addCmnt)
    {
      if (string.IsNullOrEmpty(addCmnt)) { return exstCmnt; }
      if (string.IsNullOrEmpty(exstCmnt)) { return addCmnt; }
      else { return string.Format("{0}; {1}", exstCmnt, addCmnt); }
    }

    private string GetPathOrFileName(string nm)
    {
      string fltr = string.Format("[Name] = '{0}'", nm);
      DataRow[] fNfDr = fNfDt.Select(fltr);
      if (fNfDr.Length > 0)
      {
        return fNfDr[0]["PathOrFileName"].ToString();
      }
      else
      {
        return string.Empty;
      }
    }

    private void InitializeControls() //1
    {
      Version ver = Assembly.GetExecutingAssembly().GetName().Version;
      this.Text = string.Format("{0}    ver. {1}.{2}.{3}.{4}",
                                this.Text,
                                ver.Major,
                                ver.Minor.ToString("00"),
                                ver.Build.ToString("00"),
                                ver.Revision.ToString("00"));

      rdoBtnGoogleChrome.Checked = true;
      Common cmn = new Common();
      string fNfXMLFile = Path.Combine(cmn.UserDataFolder(), xmlFilesAndFoldersNm);
      if (!File.Exists(fNfXMLFile))
      {
        string srcfNfXMLFile = Path.Combine(Path.Combine(cmn.GetRootDirectory(),"Data"), xmlFilesAndFoldersNm);
        File.Copy(srcfNfXMLFile, fNfXMLFile);
      }
      fNfDs = new DataSet();
      fNfDs.ReadXml(fNfXMLFile);

      fNfDt = fNfDs.Tables["FolderOrFile"];

      fNfSBETPOSPacExportPy = Path.Combine(cmn.GetRootDirectory(), GetPathOrFileName("POSPacExport"));
      posPacExe = GetPathOrFileName("POSPacMMS");
      pyExeFlNm = GetPathOrFileName("Python");
            //gets directories
            dtTmPkrStartDateAndTime.Value = DateTime.Now;
      CreateDataTableStructures();
      //LoadProjects();
      LoadLocalFolderForProjects();
      LoadServerFolderForProjects();
      LoadTemplateFile();
      LoadGNSSModes(ref cmn);
      dtTmPkrStartDateAndTime.Enabled = true;
      lblStatus.Text = string.Empty;
      lblStatus.Update();
    }

    private bool IsBatchFileSelected()//5.x
    {
      DataRow[] dr = batchDt.Select("[Process] = True");
      if (dr.Length > 0)
      {
        return true;
      }
      else
      {
        MessageBox.Show("A project batch file must be checked before the batch process can be started.", 
                        "ERROR!!!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        return false;
      }
    }

    private bool IsProjectLoaded()//5.x
    {
      if (cmboBxProjects.Items.Count > 0)
      {
        return true;
      }
      else
      {
        MessageBox.Show("Projects must be loaded before the batch process can be started.", 
                        "ERROR!!!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        return false;
      }
    }

    private void LoadBatches()//2.2, 5.1.3
    {
      DataView dv = batchDt.DefaultView;
      dv.RowFilter = string.Format("[Project Name] = '{0}' AND [Database Name] IS NOT NULL AND [Database Name] <> '' AND [Database Name] <> '-'", cmboBxProjects.SelectedValue);
      dv.Sort = "[Batch Name]";
      bndngSrc.DataSource = dv;
      bndngNav.BindingSource = bndngSrc;
      dtaGrdVwBatchDt.DataSource = bndngSrc;
      dtaGrdVwBatchDt.Columns["Project Name"].Visible = false;
      dtaGrdVwBatchDt.Columns["Video Location"].Visible =false;
      dtaGrdVwBatchDt.Columns["Database Name"].Visible = false;
      foreach (DataGridViewColumn dgvc in dtaGrdVwBatchDt.Columns)
      {
        dgvc.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, GraphicsUnit.Point);
      }

      dtaGrdVwBatchDt.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
      dtaGrdVwBatchDt.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
      dtaGrdVwBatchDt.AllowUserToAddRows = false;
      dtaGrdVwBatchDt.AllowUserToDeleteRows = false;
      //dtaGrdVwBatchDt.Columns["Project Name"].ReadOnly = true;
      dtaGrdVwBatchDt.Columns["Batch Name"].ReadOnly = true;
      dtaGrdVwBatchDt.Columns["ARAN"].ReadOnly = true;
      dtaGrdVwBatchDt.Columns["Data Collection From Date"].ReadOnly = true;
      dtaGrdVwBatchDt.Columns["Data Collection To Date"].ReadOnly = true;
      dtaGrdVwBatchDt.Columns["Template File"].ReadOnly = true;
      dtaGrdVwBatchDt.Columns["Backup Tape Location"].ReadOnly = true;
      dtaGrdVwBatchDt.Columns["Comments"].ReadOnly = true;
    }

    private void LoadGNSSModes(ref Common cmn)//1.4
    { 
      string gnssModesXMLFile = Path.Combine(cmn.GetRootDirectory(), xmlGNSSModesNm);
      DataSet gnssDs = new DataSet();
      gnssDs.ReadXml(gnssModesXMLFile);
      DataTable gnssDt = gnssDs.Tables["GNSSMode"];
      gnssDt.DefaultView.Sort = "SeqOrder";
      cmboBxGNSSMode.Items.Clear();
      cmboBxGNSSMode.DataSource = gnssDt;
      cmboBxGNSSMode.DisplayMember = "DisplayName";
      cmboBxGNSSMode.ValueMember = "Name";
    }

    private void LoadLocalFolderForProjects()//1.2
    {
      txtBxLocalFolder.Text = GetPathOrFileName("LocalProjectFolder");
      strLocalProjectFolder = txtBxLocalFolder.Text;
    }

    private void LoadProjects() //2
    {
      lblStatus.Text = "Loading projects.";
      lblStatus.Update();
      string slctn = string.Empty;
      if (cmboBxProjects.Items.Count > 0) { slctn = cmboBxProjects.SelectedValue.ToString(); }
      string flNm = string.Empty;
      if (rdoBtnInternetExplorer.Checked)
      {
        flNm = GetPathOrFileName("InternetExplorer");
      }
      else
      {
        flNm = GetPathOrFileName("GoogleChrome");
      }
      string url = GetPathOrFileName("Projects");

      List<FileInfo> csvFls = GetCSVFiles();
      int n = csvFls.Count;
        //list of fileinfos on all .csvs in downloads folder

      Process p = new Process();
      p.StartInfo = new ProcessStartInfo(flNm);
      p.StartInfo.Arguments = string.Format("\"{0}\"", url);
      p.Start();
      p.Close();
        //opens browser, goes to url, closes after operation complete

      int cnt = 0;
      while (csvFls.Count == n && cnt < 150)
      {
        csvFls = GetCSVFiles();
        Thread.Sleep(2000);
        cnt++;
      }
        //wait until the # of csv files in folder increases

      if (cnt < 150)
      {
        projDt.Clear();
        string[] csvRows = File.ReadAllLines(csvFls[0].FullName); //read latest csv?
        n = 0;
        foreach (string csvRow in csvRows)
        {
          if (string.IsNullOrEmpty(csvRow)) { break; }
          if (n > 0)
          {
            string[] qs = csvRow.Split('"');
            for (int i = 0; i < qs.Length; i++)
            {
              if (qs[i] == ",") { qs[i] = "~"; }
            }
            DataRow dr = projDt.NewRow();
            //dr.ItemArray = csvRow.Replace("\"", string.Empty).Split(',');
            dr.ItemArray = string.Join("\"", qs).Replace("\"", string.Empty).Split('~');
            projDt.Rows.Add(dr);
          }
          n++;
        } //put .csv data into projDt
        DataTable dt = new DataTable();
        dt = projDt.DefaultView.ToTable(true, "Project Name");
        dt.DefaultView.Sort = "[Project Name]";
        cmboBxProjects.DataSource = null;
        cmboBxProjects.Items.Clear();
        cmboBxProjects.DataSource = dt;
        cmboBxProjects.DisplayMember = "Project Name";
        cmboBxProjects.ValueMember = "Project Name";
        File.Delete(csvFls[0].FullName);

        batchDt = projDt.Clone();
        batchDt.Clear();
        batchDt.Merge(projDt);
        batchDt.Columns.Remove("GPS Post-Processing Imported Date");
        batchDt.Columns.Remove("GPS Post-Processing Required Date");
        batchDt.Columns.Remove("GPS Post-Processing Technician");
        batchDt.Columns.Remove("Server Name");
        //batchDt.Columns.Remove("Database Name");
        batchDt.Columns.Add("Comments", typeof(System.String));
        batchDt.Columns.Add("Template File", typeof(System.String));
        batchDt.Columns.Add("Process", typeof(System.Boolean)).SetOrdinal(0);
        foreach (DataRow dr in batchDt.Rows)
        {
          if (dr["ARAN"] == DBNull.Value || string.IsNullOrEmpty(dr["ARAN"].ToString()))
          {
            dr["Comments"] = GetNewComment(dr["Comments"].ToString(), "ARAN Id is missing, cannot process SBET data.");
          }
          //if (dr["Video Location"] == DBNull.Value || string.IsNullOrEmpty(dr["Video Location"].ToString()) ||
          //    !Directory.Exists(dr["Video Location"].ToString()))
          //{
          //  dr["Comments"] = GetNewComment(dr["Comments"].ToString(), "Video location is invalid, cannot process SBET data.");
          //}
          dr["Database Name"] = dr["Database Name"].ToString().Trim();
          if (dr["Database Name"] == DBNull.Value || string.IsNullOrEmpty(dr["Database Name"].ToString()))
          {
            dr["Comments"] = GetNewComment(dr["Comments"].ToString(), "Databse name is missing, cannot process SBET data.");
          }
        }
        if (!string.IsNullOrEmpty(slctn)) { cmboBxProjects.SelectedValue = slctn; }
        LoadBatches();
        //lblStatus.Text = string.Format("Projects loaded. {0} projects, {1} batches",projDt.Rows.Count, batchDt.Rows.Count);
        lblStatus.Text = "Projects loaded.";
        lblStatus.Update();
      }
      else //timeout on download
      {
        cmboBxProjects.DataSource = null;
        cmboBxProjects.Items.Clear();
        lblStatus.Text = "No projects were loaded.";
        lblStatus.Update();
      }

    }

    private void LoadServerFolderForProjects()
    {
      txtBxServerFolder.Text = GetPathOrFileName("ServerProjectFolder");
      strServerProjectFolder = txtBxServerFolder.Text;
    }

    private void LoadTemplateFile()//1.3
    {
      string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
      txtBxTemplateFile.Text = Path.Combine(pathUser, GetPathOrFileName("TemplateFile"));
      strTemplateFile = txtBxTemplateFile.Text;
    }

    private void POSPacProcess(string posPacExe, string btchPrjFlNm)
    {
      Process p = new Process();
      p.StartInfo = new ProcessStartInfo(posPacExe);
      p.StartInfo.Arguments = string.Format("-b \"{0}\"", btchPrjFlNm);
            //lblStatus.Text="RIGHT BEFORE START PROCESS";
            //lblStatus.Update();
      p.Start();
      p.WaitForExit();
      p.Close();
      p.Dispose();
            //MessageBox.Show("after processed");
        }

    private List<string> dbfilter(string a, string b ,string c, string d, string e, string f, string g)
        {
            List<string> curr = new List<string>();
            List<string> matched = new List<string>();
            List<string> Skipped = new List<string>();
            string batch; string fbtch = b;
            int n = b.Length; batch = b;
            Console.WriteLine("inside db");
            if (String.IsNullOrEmpty(g) == false)
            {
                
                string[] asd = g.Split('\\');
                batch = asd[asd.Length-1];
            }
            else
            {
                if (b.Contains("CTRL_ARAN"))
                {

                    if (b.Contains("Entry"))
                    {
                        string path = @"\\video-01\" + c + "\\" + b;
                        if (Directory.Exists(path))
                        {
                            batch = b;
                        }
                        else
                        {
                            batch = "Batch001";
                        }
                    }
                    else if (b.Contains("Exit"))
                    {
                        string path = @"\\video-01\" + c + "\\" + b;
                        if (Directory.Exists(path))
                        {
                            batch = b;
                        }
                        else
                        {
                            path = @"\\video-01\" + c;
                            int temp = 0;
                            //string[] filenames = Directory.GetFiles(path, "Batch*");
                            DirectoryInfo last = new DirectoryInfo(path);

                            foreach (var nam in last.GetDirectories("Batch*"))
                            {
                                string cut = nam.Name.Remove(0, 5);
                                int max = Convert.ToInt32(cut);
                                if (temp < max)
                                {
                                    temp = max;
                                }

                            }
                            string add = temp.ToString(); string gadd;
                            if (add.Length == 1)
                            {
                                gadd = "00" + add;
                            }
                            else if (add.Length == 2)
                            {
                                gadd = "0" + add;
                            }
                            else
                            {
                                gadd = add;
                            }
                            batch = "Batch" + gadd;
                        }
                    }
                }

                else if (b.Contains("CTRL"))
                {

                    string pa = @"\\video-01\" + c + "\\" + b;
                    if (Directory.Exists(pa))
                    {
                        batch = b;
                    }
                    else
                    {
                        if (n == 9)
                        {
                            batch = "Batch" + b.Substring(5, 4);
                        }
                        else
                        {
                            batch = "Batch" + b.Substring(5, 3);
                        }
                    }
                }

                else if (n > 3)
                {
                    batch = b.Remove(3);
                    batch = "Batch" + batch;
                }
                else
                {
                    batch = "Batch" + b;
                }
            }
            Console.WriteLine(batch);
            string dfrom = d.Substring(8, 2) + d.Substring(3, 2) + d.Substring(0, 2);
            string dto = e.Substring(8, 2) + e.Substring(3, 2) + e.Substring(0, 2);
            int df = Convert.ToInt32(dfrom);
            int dt = Convert.ToInt32(dto);

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load("sql-Info.txt");
            }
            catch(Exception de)
            {
                MessageBox.Show(de.Message);
            }
            string ser = "/sql/server";
            XmlNode xmlNode = doc.DocumentElement.SelectSingleNode(ser);
            string serve = xmlNode.InnerText;

            string user = "/sql/un";
            XmlNode xmlNode2 = doc.DocumentElement.SelectSingleNode(user);
            string usern = xmlNode2.InnerText;

            string pass = "/sql/pwd";
            XmlNode xmlNode3 = doc.DocumentElement.SelectSingleNode(pass);
            string pwd = xmlNode3.InnerText;

            string server = serve; //"ds-dpsql05";
            string database = a;
            string username = usern; // "rwg_segmenter";
            string password = pwd; // "rwgincorp";
            string command = string.Concat("select pl.filename",
                                           "from dbo.PoslvLogFilenames pl",
                                           "where pl.filename like '%"+batch+"%'",
                                           "order by pl.filename");
            Console.WriteLine(command);
            /*string connect = "Server=" + server + ";" +
                             "Database=" + database + ";" +
                             "UID=" + username + ";" +
                             "PWD=" + password + ";" +
                             "Timeout=45;";*/
            SqlConnectionStringBuilder sConnB = new SqlConnectionStringBuilder();
            sConnB.DataSource = server;
            sConnB.InitialCatalog = database;
            sConnB.UserID = username;
            sConnB.Password = password;
            sConnB.ConnectTimeout = 45;

            Console.WriteLine(sConnB.ConnectionString);
            try
            {
                using (SqlConnection cursor = new SqlConnection(sConnB.ConnectionString))
                {
                    
                    SqlCommand com = new SqlCommand();
                    try
                    {
                        com.CommandText = @"select pl.filename
                                   from dbo.PoslvLogFilenames pl
                                   where pl.filename like '%" + batch + @"%'
                                   order by pl.filename";
                        com.CommandType = CommandType.Text;
                        com.Connection = cursor;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message.ToString());
                        var quit = MessageBox.Show("Could not connect to DB will now copy all files. Quit now?","DB Failed",MessageBoxButtons.YesNo);
                        if (quit == DialogResult.Yes)
                        {
                            ChangeControlStatus(true);
                            tmrStartDateAndTime.Stop();
                            lblStatus.Text = "Batch process cancelled.";
                            lblStatus.Update();
                            btnStartBatchProcess.Text = "Start Batch Process";
                            curr.Add("fail");
                            return curr;
                        }
                        else
                        {
                            return curr;
                        }
                    }
                    try
                    {
                        cursor.Open();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        MessageBox.Show(ex.Message.ToString());
                        var quit = MessageBox.Show("Could not open DB will now copy all files. Quit now?", "DB Failed", MessageBoxButtons.YesNo);
                        if (quit == DialogResult.Yes)
                        {
                            ChangeControlStatus(true);
                            tmrStartDateAndTime.Stop();
                            lblStatus.Text = "Batch process cancelled.";
                            lblStatus.Update();
                            btnStartBatchProcess.Text = "Start Batch Process";
                            curr.Add("fail");
                            return curr;
                        }
                        else
                        {
                            return curr;
                        }


                    }
                    SqlDataReader reader = null;
                    try
                    {
                        reader = com.ExecuteReader();
                    }
                    catch (Exception aj)
                    {
                        MessageBox.Show(aj.Message.ToString());
                        var quit = MessageBox.Show("Could not connect read DB will now copy all files. Quit now?", "DB Failed", MessageBoxButtons.YesNo);
                        if (quit == DialogResult.Yes)
                        {
                            ChangeControlStatus(true);
                            tmrStartDateAndTime.Stop();
                            lblStatus.Text = "Batch process cancelled.";
                            lblStatus.Update();
                            btnStartBatchProcess.Text = "Start Batch Process";
                            curr.Add("fail");
                            return curr;
                        }
                        else
                        {
                            return curr;
                        }

                    }
                    while (reader.Read())
                    {
                        string temp = reader.GetValue(0).ToString();
                        int n2 = temp.Length;
                        string temp1 = temp.Substring(n2 - 25, 6);
                        int between = 0;
                        try
                        {
                            between = Convert.ToInt32(temp1);
                        }
                        catch(Exception sad)
                        {
                            MessageBox.Show(sad.Message);

                            var quit = MessageBox.Show("Could not filter using DB will now copy all files. Quit now?", "DB Failed", MessageBoxButtons.YesNo);
                            if (quit == DialogResult.Yes)
                            {
                                ChangeControlStatus(true);
                                tmrStartDateAndTime.Stop();
                                lblStatus.Text = "Batch process cancelled.";
                                lblStatus.Update();
                                btnStartBatchProcess.Text = "Start Batch Process";
                                curr.Add("fail");
                                return curr;
                            }
                            else
                            {
                                return curr;
                            }
                        }
                        int q = temp.Length;
                        string p = temp.Substring(q - 18, 14);

                        if ((between <= dt) && (between >= df) && (matched.Contains(p) == false))
                        {

                            matched.Add(p.ToUpper());
                            lblStatus.Text = p;
                            lblStatus.Update();


                        }

                    }
                    Console.WriteLine("match");
                    List<string> dir = new List<string>();

                    string path = @"\\video-01\" + c + "\\" + batch + "\\posdata";
                    DirectoryInfo di1 = new DirectoryInfo(path);
                    foreach (DirectoryInfo isndex in di1.GetDirectories())
                    {
                        int indexNum = 0;
                        try
                        {
                            indexNum = Convert.ToInt32(isndex.Name);
                        }
                        catch (Exception sad)
                        {
                            MessageBox.Show(sad.Message);

                            var quit = MessageBox.Show("Could not filter using DB will now copy all files. Quit now?", "DB Failed", MessageBoxButtons.YesNo);
                            if (quit == DialogResult.Yes)
                            {
                                ChangeControlStatus(true);
                                tmrStartDateAndTime.Stop();
                                lblStatus.Text = "Batch process cancelled.";
                                lblStatus.Update();
                                btnStartBatchProcess.Text = "Start Batch Process";
                                curr.Add("fail");
                                return curr;
                            }
                            else
                            {
                                return curr;
                            }
                        }
                        if ((indexNum <= dt) && (indexNum >= df))
                        {
                            dir.Add(isndex.Name);
                            // lblStatus.Text = isndex.Name;
                            //lblStatus.Update();
                            //curr.Add(isndex.Name);
                        }
                    }
                    foreach (string fldr in dir)
                    {
                        string path2 = path + "\\" + fldr;
                        DirectoryInfo di = new DirectoryInfo(path2);
                        foreach (FileInfo filename in di.GetFiles())
                        {
                            int w = filename.Name.Length;
                            string fil = filename.Name.Remove(w - 4, 4);
                            if (matched.Contains(fil.ToUpper()))
                            {
                                curr.Add(filename.FullName);
                            }
                            else
                            {
                                Skipped.Add(filename.FullName);
                                //curr.Add(path2);
                            }

                        }

                    }

                    reader.Close();
                    com.Dispose();
                }
            }
            catch(Exception sad)
            {
                MessageBox.Show(sad.Message);

                var quit = MessageBox.Show("Could not filter using DB will now copy all files. Quit now?", "DB Failed", MessageBoxButtons.YesNo);
                if (quit == DialogResult.Yes)
                {
                    ChangeControlStatus(true);
                    tmrStartDateAndTime.Stop();
                    lblStatus.Text = "Batch process cancelled.";
                    lblStatus.Update();
                    btnStartBatchProcess.Text = "Start Batch Process";
                    curr.Add("fail");
                    return curr;
                }
                else
                {
                    return curr;
                }
            
            }
            string logfile1 = @"\\video-01\Operations\SBETs\"+ c + "\\" + c + "-LOGS" + "\\" + c + "-" + b + ".txt";
            
            foreach (string x in Skipped)
            {
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(logfile1, true))
                {

                    string processing = string.Format("Skipped: {0}  ", x);
                    file.WriteLine(processing);
                    
                }
            }
            using(System.IO.StreamWriter file =
                new System.IO.StreamWriter(logfile1, true))
                {

                file.WriteLine("");
            }

            return curr;
        }
    
    private List<string> Begin()
    {
            DataRow[] fltr = batchDt.Select("[Process] = True", "[ARAN], [Video Location], [Batch Name]");
            bool doneG = false; int fail = 0;
            List<string> final = new List<string>();
            List<string> data = new List<string>();

            foreach (DataRow i in fltr)
            {
                completed = false;
                string db = i["Database Name"].ToString();
                string batch = i["Batch Name"].ToString();
                string projpath = i["Video Location"].ToString();
                string[] arra = projpath.Split('\\');
                string proj1 = arra[arra.Length - 1];
                string aran = i["ARAN"].ToString();
                string dfrom = i["Data Collection From Date"].ToString();
                string dto = i["Data Collection To Date"].ToString();
                string dir = projpath + "\\" + proj1 +"-LOGS";
                string batchP = i["Backup Tape Location"].ToString();
                //Console.WriteLine("Starting");
                try
                {
                    if (Directory.Exists(dir) == false)
                    {
                        DirectoryInfo di = Directory.CreateDirectory(dir);
                    }
                    string logfile1 = dir + "\\" + proj1 + "-" + batch + ".txt";

                    using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(logfile1, false))
                    {
                        file.WriteLine("Filtered Files: ");

                    }
                    Console.WriteLine("enter inside");
                    data = dbfilter(db, batch, proj1, dfrom, dto, aran,batchP);
                    //Console.WriteLine("inside");


                }
                catch(Exception ex)
                {
                    lblStatus.Text = ex.Message.ToString();
                    lblStatus.Update(); fail += 1;
                }

                string b = batch; string fbtch = b;
                int ns = b.Length;

                if (String.IsNullOrEmpty(batchP) == false)
                {
                    string[] asd = batchP.Split('\\');
                    batch = asd[asd.Length - 1];
                }
                else
                {
                    if (b.Contains("CTRL_ARAN"))
                    {

                        if (b.Contains("Entry"))
                        {
                            string path = @"\\video-01\" + proj1 + "\\" + b;
                            if (Directory.Exists(path))
                            {
                                batch = b;
                            }
                            else
                            {
                                batch = "Batch001";
                            }
                        }
                        else if (b.Contains("Exit"))
                        {
                            string path = @"\\video-01\" + proj1 + "\\" + b;
                            if (Directory.Exists(path))
                            {
                                batch = b;
                            }
                            else
                            {
                                path = @"\\video-01\" + proj1;
                                int temp = 0;
                                //string[] filenames = Directory.GetFiles(path, "Batch*");
                                DirectoryInfo last = new DirectoryInfo(path);

                                foreach (var nam in last.GetDirectories("Batch*"))
                                {
                                    string cut = nam.Name.Remove(0, 5);
                                    int max = Convert.ToInt32(cut);
                                    if (temp < max)
                                    {
                                        temp = max;
                                    }

                                }
                                string add = temp.ToString(); string gadd;
                                if (add.Length == 1)
                                {
                                    gadd = "00" + add;
                                }
                                else if (add.Length == 2)
                                {
                                    gadd = "0" + add;
                                }
                                else
                                {
                                    gadd = add;
                                }
                                batch = "Batch" + gadd;
                            }
                        }
                    }

                    else if (b.Contains("CTRL"))
                    {

                        string pa = @"\\video-01\" + proj1 + "\\" + b;
                        if (Directory.Exists(pa))
                        {
                            batch = b;
                        }
                        else
                        {
                            if (ns == 9)
                            {
                                batch = "Batch" + b.Substring(5, 4);
                            }
                            else
                            {
                                batch = "Batch" + b.Substring(5, 3);
                            }
                        }
                    }

                    else if (ns > 3)
                    {
                        batch = b.Remove(3);
                        batch = "Batch" + batch;
                    }
                    else
                    {
                        batch = "Batch" + b;
                    }
                }
                if (data.Count() != 0)
                {
                    if (data[0] == "fail")
                    {
                        List<string> test = new List<string>();
                        test.Add("fail");
                        return test;
                    }
                }
                if (data.Count() == 0)
                {
                    lblStatus.Text = "Could not connect to DB";
                    lblStatus.Update();
                    fail = 0;
                    
                    int n = aran.Length;
                    string ar = aran.Substring(n - 2, 2);
                    string src = @"\\video-01\" + proj1 + "\\" + batch + "\\posdata";
                    string dest = projpath + "\\ARAN" + ar + "_Posdata";
                    string logfile1 = projpath + "\\" + proj1 + "-LOGS" + "\\" + proj1 + "-" + b + ".txt";
                    if (Directory.Exists(src) == false)
                    {
                        fail += 1;
                        using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(logfile1, true))
                        {
                            file.WriteLine("");
                            string processing = string.Format("Could not find Dir: {0}  ", src);
                            string timestamp = string.Format("TimeStamp: {0}", DateTime.Now.ToString());
                            file.WriteLine(processing);
                        }
                    }
                    if (Directory.Exists(dest) == false)
                    {
                        Directory.CreateDirectory(dest);
                    }

                    dfrom = dfrom.Substring(8, 2) + dfrom.Substring(3, 2) + dfrom.Substring(0, 2);
                    dto = dto.Substring(8, 2) + dto.Substring(3, 2) + dto.Substring(0, 2);
                    int df = Convert.ToInt32(dfrom);
                    int dt = Convert.ToInt32(dto);

                    List<string> valid = new List<string>();

                    string path = src;
                    DirectoryInfo di1 = new DirectoryInfo(path);
                    foreach (DirectoryInfo isndex in di1.GetDirectories())
                    {

                        int indexNum = Convert.ToInt32(isndex.Name);
                        if ((indexNum <= dt) && (indexNum >= df))
                        {
                            valid.Add(isndex.Name);
                        }
                    }
                    completed = false;
                    foreach (string fldnam in valid)
                    {
                        try
                        {
                            string s = src + "\\" + fldnam;
                            string d = dest + "\\" + fldnam;
                            using (System.IO.StreamWriter file =
                            new System.IO.StreamWriter(logfile1, true))
                            {

                                string srcfile = string.Format("Source: {0}  ", s);
                                string destfile = string.Format("Destination: {0}  ", d);
                                string timestamp = string.Format("Start Time: {0}", DateTime.Now.ToString());

                                file.WriteLine(timestamp);
                                file.WriteLine(srcfile);
                                file.WriteLine(destfile);
                            }
                            //Process p = new Process();
                            //p.StartInfo.Arguments = string.Format("/C Robocopy {0} {1} /S /MT:32 /NFL /NDL /NJH /NJS /nc /ns /np", s,d);
                            //p.StartInfo.FileName = "CMD.EXE";
                            //p.StartInfo.CreateNoWindow = true;
                            // p.StartInfo.UseShellExecute = false;
                            //p.Start();
                            //p.WaitForExit();
                        
                            RoboSharp.RoboCommand cpy = new RoboSharp.RoboCommand();
                            
                            cpy.OnCommandCompleted += Cpy_OnCommandCompleted;
                            cpy.OnFileProcessed += Cpy_OnFileProcessed;
                            cpy.CopyOptions.Source = s;
                            cpy.CopyOptions.Destination = d;
                            cpy.CopyOptions.MultiThreadedCopiesCount = 32;
                            cpy.CopyOptions.CopySubdirectories = true;
                            cpy.LoggingOptions.NoDirectoryList = true;
                            cpy.LoggingOptions.NoFileClasses = true;
                            cpy.LoggingOptions.NoFileList = true;
                            cpy.LoggingOptions.NoFileSizes = true;
                            cpy.LoggingOptions.NoJobHeader = true;
                            cpy.LoggingOptions.NoJobSummary = true;
                            cpy.LoggingOptions.NoProgress = true;
                            cpy.Start();

                            
                            bool copying = true;
                            while (copying)
                            {
                                DirectoryInfo disrc = new DirectoryInfo(s);
                                DirectoryInfo didest = new DirectoryInfo(d);
                                if (didest.Exists)
                                {
                                    FileInfo[] check = didest.GetFiles();
                                    FileInfo[] srcnt = disrc.GetFiles();
                                    if(check.Count() == srcnt.Count())
                                    {
                                        copying = false;
                                    }
                                }
                                else
                                {
                                    copying = false;
                                }
                            }
                            

                            using (System.IO.StreamWriter file =
                            new System.IO.StreamWriter(logfile1, true))
                            {

                                string processing = string.Format("Status: Completed");
                                string timestamp = string.Format("End Time: {0}", DateTime.Now.ToString());

                                file.WriteLine(timestamp);
                                file.WriteLine(processing);
                                file.WriteLine("");
                            }

                        }
                        catch (Exception e1)
                        {
                            using (System.IO.StreamWriter file =
                            new System.IO.StreamWriter(logfile1, true))
                            {

                                string processing = string.Format("Status: Failed - {0}  ", e1.Message);
                                string timestamp = string.Format("End Time: {0}", DateTime.Now.ToString());

                                file.WriteLine(timestamp);
                                file.WriteLine(processing);
                                file.WriteLine("");
                            }

                            fail += 1;
                        }
                    }
                    if (fail != 0)
                    {
                        doneG = false;
                    }
                    else
                    {
                        doneG = true;
                    }
                   
                }

                else if (data.Count() > 0)
                {
                    completed = false;
                    int n = aran.Length;
                    string ar = aran.Substring(n - 2, 2);
                    string d = projpath + "\\ARAN" + ar + "_Posdata";
                    string logfile1 = projpath + "\\" + proj1 + "-LOGS" + "\\" + proj1 + "-" + b + ".txt";
                 
                    foreach (string i1 in data)
                    {
                        int n1 = i1.Length;
                        string it = i1.Substring(n1 - 25, 6);
                        string fn = i1.Remove(0,n1-18);


                        string s = i1.Substring(0 , n1-19);
                        string e = d + "\\" + it;
                        try
                        {
                            using (System.IO.StreamWriter file =
                            new System.IO.StreamWriter(logfile1, true))
                            {

                                string srcfile = string.Format("Source: {0}  ", i1);
                                string destfile = string.Format("Destination: {0}  ", e);
                                string timestamp = string.Format("Start Time: {0}", DateTime.Now.ToString());

                                file.WriteLine(timestamp);
                                file.WriteLine(srcfile);
                                file.WriteLine(destfile);
                            }

                            //Process p = new Process();
                            //p.StartInfo.Arguments = string.Format("/C Robocopy {0} {1} {2} /NFL /NDL /NJH /NJS /nc /ns /np", s, e, fn);
                            //p.StartInfo.FileName = "CMD.EXE";
                            //p.StartInfo.CreateNoWindow = true;
                            //p.StartInfo.UseShellExecute = false;
                            //p.Start();
                            //p.WaitForExit();
                            
                            RoboSharp.RoboCommand cpy = new RoboSharp.RoboCommand();

                            cpy.OnCommandCompleted += Cpy_OnCommandCompleted;
                            cpy.OnFileProcessed += Cpy_OnFileProcessed;
                            cpy.CopyOptions.Source = s;
                            cpy.CopyOptions.Destination = e;
                            cpy.CopyOptions.FileFilter = fn;
                            cpy.LoggingOptions.NoDirectoryList = true;
                            cpy.LoggingOptions.NoFileClasses = true;
                            cpy.LoggingOptions.NoFileList = true;
                            cpy.LoggingOptions.NoFileSizes = true;
                            cpy.LoggingOptions.NoJobHeader = true;
                            cpy.LoggingOptions.NoJobSummary = true;
                            cpy.LoggingOptions.NoProgress = true;
                            cpy.Start();

                            
                            bool copyin = true;
                            while (copyin)
                            {
                                FileInfo srcq = new FileInfo(i1);
                                string destqe = e + "\\" + fn;
                                FileInfo destq = new FileInfo(destqe);
                                if (destq.Exists)
                                {
                                    if (srcq.Length == destq.Length)
                                    {
                                        copyin = false;
                                    }
                                }
                                else
                                {
                                    copyin = true;
                                }
                            }

                            using (System.IO.StreamWriter file =
                            new System.IO.StreamWriter(logfile1, true))
                            {

                                string processing = string.Format("Status: Completed");
                                string timestamp = string.Format("End Time: {0}", DateTime.Now.ToString());

                                file.WriteLine(timestamp);
                                file.WriteLine(processing);
                                file.WriteLine("");
                            }

                        }
                        catch (Exception e1)
                        {
                            using (System.IO.StreamWriter file =
                            new System.IO.StreamWriter(logfile1, true))
                            {

                                string processing = string.Format("Status: Failed - {0}  ", e1.Message);
                                string timestamp = string.Format("End Time: {0}", DateTime.Now.ToString());

                                file.WriteLine(timestamp);
                                file.WriteLine(processing);
                                file.WriteLine("");
                            }

                            fail += 1;
                        }


                    }
                    if (fail != 0)
                    {
                        doneG = false;
                    }
                    else
                    {
                        doneG = true;
                    }

                }

                //start the filter process
                //string filename = @"\\video-01\Operations\SBETs\Apps\iSBET-test\dbtest.py";
                //Process p = new Process();
                //p.StartInfo = new ProcessStartInfo(@"\\video-01\Operations\SBETs\Apps\Python27\python.exe", filename)
               // {
                 //   RedirectStandardOutput = true,
                   // UseShellExecute = false,
                    //CreateNoWindow = true
                //};
               // p.Start();
                //p.WaitForExit();
                //p.Close();
                //p.Dispose();
                
                //copying begins
                //var engine = Python.CreateEngine();
                //dynamic py = engine.ExecuteFile(@"\\video-01\Operations\SBETs\Apps\iSBET-test\copy.py");

                //dynamic copier = py.Copier();
                //doneG = copier.automate(batch, projpath, aran, dfrom, dto);

                if (doneG == false)
                {
                    final.Add(batch);
                    lblStatus.Text = string.Format("COULD NOT COPY {0}", batch);
                    lblStatus.Update();
                }
                else
                {
                    lblStatus.Text = string.Format("COPIED {0}",batch);
                    lblStatus.Update();
                }
            }
            //MessageBox.Show(final.Count().ToString());
            return final;
            
    }

        private void Cpy_OnFileProcessed(object sender, RoboSharp.FileProcessedEventArgs e)
        {
            lblStatus.Text = "Done robocopy.";
            lblStatus.Update();
            //MessageBox.Show("Done processing file");
        }

        private void Cpy_OnCommandCompleted(object sender, RoboSharp.RoboCommandCompletedEventArgs e)
        {
            completed = true;
            lblStatus.Text = "Done robocop.";
            lblStatus.Update();
            //MessageBox.Show("Done robocopy command");
            return;
        }

        private bool Remove(List<string> cant, List<string> err)
        {
            DataRow[] fltr = batchDt.Select("[Process] = True", "[ARAN], [Video Location], [Batch Name]");
            int fail = 0; bool doneG = false; int fail1 = 0;
            List<string> final = new List<string>();
            foreach (DataRow i in fltr)
            {
                if ((cant.Contains(i["Batch Name"].ToString()) != true) && (err.Contains(i["Batch Name"].ToString()) == true))
                {
                    
                    string projpath = i["Video Location"].ToString();
                    string[] arra = projpath.Split('\\');
                    string proj1 = arra[arra.Length - 1];
                    string aran = i["ARAN"].ToString();
                    string batch = i["Batch Name"].ToString();
                    string dfrom = i["Data Collection From Date"].ToString();
                    string dto = i["Data Collection To Date"].ToString();
                    string logfile1 = projpath + "\\" + proj1 + "-LOGS" + "\\" + proj1 + "-" + batch + ".txt";

                    dfrom = dfrom.Substring(8, 2) + dfrom.Substring(3, 2) + dfrom.Substring(0, 2);
                    dto = dto.Substring(8, 2) + dto.Substring(3, 2) + dto.Substring(0, 2);
                    int df = Convert.ToInt32(dfrom);
                    int dt = Convert.ToInt32(dto);

                    int n = aran.Length;
                    string ar = aran.Substring(n - 2, 2);

                    string dest = projpath + "\\ARAN" + ar + "_Posdata";
                    List<string> dir = new List<string>();
                    DirectoryInfo di1 = new DirectoryInfo(dest);
                    foreach (DirectoryInfo isndex in di1.GetDirectories())
                    {

                        int indexNum = Convert.ToInt32(isndex.Name);
                        if ((indexNum <= dt) && (indexNum >= df))
                        {
                            dir.Add(isndex.FullName);
                        }
                    }
                    foreach (string fldr in dir)
                    {
                        dest = fldr;
                        try
                        {
                            using (System.IO.StreamWriter file =
                            new System.IO.StreamWriter(logfile1, true))
                            {

                                string srcfile = string.Format("Deleting: {0}", dest);
                                string timestamp = string.Format("Start Time: {0}", DateTime.Now.ToString());

                                file.WriteLine(timestamp);
                                file.WriteLine(srcfile);
                                
                            }
                            Directory.Delete(dest,true);
                            using (System.IO.StreamWriter file =
                            new System.IO.StreamWriter(logfile1, true))
                            {

                                string processing = string.Format("Status: Completed");
                                string timestamp = string.Format("End Time: {0}", DateTime.Now.ToString());

                                file.WriteLine(timestamp);
                                file.WriteLine(processing);
                                file.WriteLine("");
                            }
                        }
                        catch (Exception e1)
                        {
                            using (System.IO.StreamWriter file =
                            new System.IO.StreamWriter(logfile1, true))
                            {

                                string processing = string.Format("Status: Failed - {0}  ", e1.Message);
                                string timestamp = string.Format("End Time: {0}", DateTime.Now.ToString());

                                file.WriteLine(timestamp);
                                file.WriteLine(processing);
                                file.WriteLine("");
                            }

                            fail1 += 1;
                        }

                    }

                    if (fail1 != 0)
                    {
                        doneG = false;
                    }
                    else
                    {
                        doneG = true;
                    }


                    if (doneG == false)
                    {
                        fail += 1;
                        lblStatus.Text = string.Format("COULD NOT DELETE {0}", i["Batch Name"].ToString());
                        lblStatus.Update();
                    }
                    else
                    {
                        lblStatus.Text = string.Format("DELETED {0}", i["Batch Name"].ToString());
                        lblStatus.Update();
                    }
                }
                else
                {
                    continue;
                }
            }
            if (fail == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool Remove(List<string> cant)
        {
            DataRow[] fltr = batchDt.Select("[Process] = True", "[ARAN], [Video Location], [Batch Name]");
            int fail = 0; bool doneG = false; int fail1 = 0;
            List<string> final = new List<string>();
            foreach (DataRow i in fltr)
            {
                if (cant.Contains(i["Batch Name"].ToString()) == true)
                {
                    string projpath = i["Video Location"].ToString();
                    string[] arra = projpath.Split('\\');
                    string proj1 = arra[arra.Length - 1];
                    string aran = i["ARAN"].ToString();
                    string batch = i["Batch Name"].ToString();
                    string dfrom = i["Data Collection From Date"].ToString();
                    string dto = i["Data Collection To Date"].ToString();
                    string logfile1 = projpath + "\\" + proj1 + "-LOGS" + "\\" + proj1 + "-" + batch + ".txt";

                    dfrom = dfrom.Substring(8, 2) + dfrom.Substring(3, 2) + dfrom.Substring(0, 2);
                    dto = dto.Substring(8, 2) + dto.Substring(3, 2) + dto.Substring(0, 2);
                    int df = Convert.ToInt32(dfrom);
                    int dt = Convert.ToInt32(dto);

                    int n = aran.Length;
                    string ar = aran.Substring(n - 2, 2);

                    string dest = projpath + "\\ARAN" + ar + "_Posdata";
                    List<string> dir = new List<string>();
                    DirectoryInfo di1 = new DirectoryInfo(dest);
                    foreach (DirectoryInfo isndex in di1.GetDirectories())
                    {

                        int indexNum = Convert.ToInt32(isndex.Name);
                        if ((indexNum <= dt) && (indexNum >= df))
                        {
                            dir.Add(isndex.FullName);
                        }
                    }
                    foreach (string fldr in dir)
                    {
                        dest = fldr;
                        try
                        {
                            using (System.IO.StreamWriter file =
                            new System.IO.StreamWriter(logfile1, true))
                            {

                                string srcfile = string.Format("Deleting: {0}", dest);
                                string timestamp = string.Format("Start Time: {0}", DateTime.Now.ToString());

                                file.WriteLine(timestamp);
                                file.WriteLine(srcfile);

                            }
                            Directory.Delete(dest,true);
                            using (System.IO.StreamWriter file =
                            new System.IO.StreamWriter(logfile1, true))
                            {

                                string processing = string.Format("Status: Completed");
                                string timestamp = string.Format("End Time: {0}", DateTime.Now.ToString());

                                file.WriteLine(timestamp);
                                file.WriteLine(processing);
                                file.WriteLine("");
                            }
                        }
                        catch (Exception e1)
                        {
                            using (System.IO.StreamWriter file =
                            new System.IO.StreamWriter(logfile1, true))
                            {

                                string processing = string.Format("Status: Failed - {0}  ", e1.Message);
                                string timestamp = string.Format("End Time: {0}", DateTime.Now.ToString());

                                file.WriteLine(timestamp);
                                file.WriteLine(processing);
                                file.WriteLine("");
                            }

                            fail1 += 1;
                        }

                    }

                    if (fail1 != 0)
                    {
                        doneG = false;
                    }
                    else
                    {
                        doneG = true;
                    }

                    if (doneG == false)
                    {
                        fail += 1;
                        lblStatus.Text = string.Format("COULD NOT DELETE {0}", i["Batch Name"].ToString());
                        lblStatus.Update();
                    }
                    else
                    {
                        lblStatus.Text = string.Format("DELETED REDUNDENT BATCH: {0}", i["Batch Name"].ToString());
                        lblStatus.Update();
                    }
                }
                else
                {
                    continue;
                }
            }
            if (fail == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        private void ProcessBatches(bool skpPosPacProcess)
    {
            // need to set up folder before we can ProcessBatches
            //Loop through posbat files
            int time = 0;
            List<string> imm = new List<string>();
            DataRow[] drs = batchDt.Select("[Process] = True", "[Project Name], [Batch Name]");
                foreach (DataRow dr in drs)
                {
                string t = dr["Batch Name"].ToString();
                    if (!inc.Contains(t))
                    {  
                        string posBatFlNm = string.Format("{0}_{1}.posbat", dr["Database Name"].ToString(), dr["Batch Name"].ToString());
                        string btchPrjFullFlNm = Path.Combine(strLocalProjectFolder, posBatFlNm);
                    //MessageBox.Show(posBatFlNm);
                    //MessageBox.Show(btchPrjFullFlNm);

                    if (File.Exists(btchPrjFullFlNm))
                    {
                        if (!skpPosPacProcess)
                        {
                            //lblStatus.Text = string.Format("Processing {0}. {1} processes, {2} projects", posBatFlNm, drs.Length, batchDt.Rows.Count);
                            lblStatus.Text = string.Format("Processing {0}.", posBatFlNm);
                            lblStatus.Update();
                            string basep1 = dr["Video Location"].ToString();
                            string[] arrad = basep1.Split('\\');
                            string proj1 = arrad[arrad.Length - 1];

                            string logfile1 = basep1 + "\\" + proj1 + "-LOGS" + "\\" + proj1 + "-" + t + ".txt";
                            using (System.IO.StreamWriter file =
                                new System.IO.StreamWriter(logfile1, true))
                            {   if (time == 0)
                                {
                                    file.WriteLine("");
                                }
                                string processing = string.Format("Processing {0} at {1}.",t,DateTime.Now.ToString());
                                file.WriteLine(processing);
                            }
                            POSPacProcess(posPacExe, btchPrjFullFlNm);
                            //lblStatus.Text = string.Format("{0} processed.  {1} processes, {2} projects", posBatFlNm, drs.Length, batchDt.Rows.Count);
                            lblStatus.Text = string.Format("{0} processed.", posBatFlNm);
                            lblStatus.Update();
                        }

                        lblStatus.Text = string.Format("Post processing {0}.", posBatFlNm);
                        lblStatus.Update();
                        //MessageBox.Show(string.Format("Sending \"{0}\" to PostProcessBatchMissions.", btchPrjFlNm));
                        PostProcessBatchMissions ppbm = new PostProcessBatchMissions(btchPrjFullFlNm);
                        string basep = dr["Video Location"].ToString();
                        string[] arra = basep.Split('\\');
                        string proj = arra[arra.Length - 1];

                        string logfile = basep + "\\" + proj + "-LOGS" + "\\" + proj + "-" + t + ".txt";
                        if ((!ppbm.HasMissionAborted()) && (!ppbm.HasMissionFailed()))
                        {
                            lblStatus.Text = string.Format("{0} post processed.", posBatFlNm);
                            failed.Add(t);
                            using (System.IO.StreamWriter file =
                                new System.IO.StreamWriter(logfile, true))
                                
                            {
                                string processing = string.Format("Processed {0} at {1}.", t, DateTime.Now.ToString());
                                file.WriteLine(processing);
                                string status = "Status: Completed";
                                file.WriteLine(status);
                                file.WriteLine("");
                            }
                        }
                        else if (!ppbm.HasMissionAborted())
                        {
                            sendMail(btchPrjFullFlNm, proj, t);
                            lblStatus.Text = string.Format("{0} aborted.", posBatFlNm);
                            dr["Comments"] = string.Format("*** ABORTED DUE TO MISSION ABORT ***;{0}", dr["Comments"].ToString());
                            using (System.IO.StreamWriter file =
                                new System.IO.StreamWriter(logfile, true))
                            {
                                string processing = string.Format("Could Not Process {0} at {1}.", t, DateTime.Now.ToString());
                                file.WriteLine(processing);
                                string status = string.Format("Status:{0}",dr["Comments"].ToString());
                                file.WriteLine(status);
                                file.WriteLine("");
                            }
                            string pb = btchPrjFullFlNm; //posBatFlNm;
                            XmlDocument doc = new XmlDocument();
                            try
                            {
                                doc.Load(pb);
                                XmlNodeList remove;
                                XmlNodeList rmv;
                                XmlNode root = doc.DocumentElement;

                                remove = root.SelectNodes("descendant::Project/Reports");

                                foreach (XmlNode a in remove)
                                {
                                    a.RemoveAll();
                                    XmlNode parent = a.ParentNode;
                                    parent.RemoveChild(a);
                                }
                                rmv = root.SelectNodes("descendant::Project/Stage[last()]");

                                foreach (XmlNode a in rmv)
                                {
                                    a.RemoveAll();
                                    XmlNode parent = a.ParentNode;
                                    parent.RemoveChild(a);
                                }
                                try
                                {
                                    doc.Save(pb);
                                }
                                catch (Exception ed)
                                {
                                    MessageBox.Show("Could not save posbat XML; " + ed.Message + "You will need to manually remove Reports from XML");
                                }
                            }
                            catch (Exception ed)
                            {
                                MessageBox.Show("Could not load posbat XML; " + ed.Message + "You will need to manually remove Reports from XML");
                            }

                            

                        }
                        else if (!ppbm.HasMissionFailed())
                        {
                            sendMail(btchPrjFullFlNm, proj, t);
                            lblStatus.Text = string.Format("{0} aborted.", posBatFlNm);
                            dr["Comments"] = string.Format("*** ABORTED DUE TO HUMAN INTERRUPTION ***;{0}", dr["Comments"].ToString());
                            using (System.IO.StreamWriter file =
                                new System.IO.StreamWriter(logfile, true))
                            {
                                string processing = string.Format("Could Not Process {0} at {1}.", t, DateTime.Now.ToString());
                                file.WriteLine(processing);
                                string status = string.Format("Status:{0}", dr["Comments"].ToString());
                                file.WriteLine(status);
                                file.WriteLine("");
                            }
                            string pb = posBatFlNm;
                            XmlDocument doc = new XmlDocument();
                            try
                            {
                                doc.Load(pb);
                                XmlNodeList remove;
                                XmlNodeList rmv;
                                XmlNode root = doc.DocumentElement;

                                remove = root.SelectNodes("descendant::Project/Reports");

                                foreach (XmlNode a in remove)
                                {
                                    a.RemoveAll();
                                    XmlNode parent = a.ParentNode;
                                    parent.RemoveChild(a);
                                }
                                rmv = root.SelectNodes("descendant::Project/Stage[last()]");

                                foreach (XmlNode a in rmv)
                                {
                                    a.RemoveAll();
                                    XmlNode parent = a.ParentNode;
                                    parent.RemoveChild(a);
                                }
                                try
                                {
                                    doc.Save(pb);
                                }
                                catch (Exception ed)
                                {
                                    MessageBox.Show("Cound not save posbat XML; " + ed.Message + "You will need to manually remove Reports from XML");
                                }
                            }
                            catch (Exception ed)
                            {
                                MessageBox.Show("Cound not load posbat XML; " + ed.Message + "You will need to manually remove Reports from XML");
                            }
                        
                        }
                        else
                        {
                            sendMail(btchPrjFullFlNm, proj, t);
                            lblStatus.Text = string.Format("{0} aborted.", posBatFlNm);
                            dr["Comments"] = string.Format("*** ABORTED DUE TO HUMAN INTERRUPTION AND MISSION ABORT ***;{0}", dr["Comments"].ToString());
                            using (System.IO.StreamWriter file =
                                new System.IO.StreamWriter(logfile, true))
                            {
                                string processing = string.Format("Could Not Process {0} at {1}.", t, DateTime.Now.ToString());
                                file.WriteLine(processing);
                                string status = string.Format("Status:{0}", dr["Comments"].ToString());
                                file.WriteLine(status);
                                file.WriteLine("");
                            }
                            string pb = posBatFlNm;
                            XmlDocument doc = new XmlDocument();
                            try
                            {
                                doc.Load(pb);
                                XmlNodeList remove;
                                XmlNodeList rmv;
                                XmlNode root = doc.DocumentElement;

                                remove = root.SelectNodes("descendant::Project/Reports");

                                foreach (XmlNode a in remove)
                                {
                                    a.RemoveAll();
                                    XmlNode parent = a.ParentNode;
                                    parent.RemoveChild(a);
                                }
                                rmv = root.SelectNodes("descendant::Project/Stage[last()]");

                                foreach (XmlNode a in rmv)
                                {
                                    a.RemoveAll();
                                    XmlNode parent = a.ParentNode;
                                    parent.RemoveChild(a);
                                }
                                try
                                {
                                    doc.Save(pb);
                                }
                                catch (Exception ed)
                                {
                                    MessageBox.Show("Cound not save posbat XML; " + ed.Message + "You will need to manually remove Reports from XML");
                                }
                            }
                            catch (Exception ed)
                            {
                                MessageBox.Show("Cound not load posbat XML; " + ed.Message + "You will need to manually remove Reports from XML");
                            }
                        }
                        ppbm.badmission(basep,proj, t);
                        lblStatus.Update();

                        //string posBadBatFlNm = ppbm.GetBadProjectFileName();
                        //if (!string.IsNullOrEmpty(posBadBatFlNm))
                        //{
                        //  lblStatus.Text = string.Format("Processing {0}.", posBadBatFlNm);
                        //  lblStatus.Update();
                        //  POSPacProcess(posPacExe, posBadBatFlNm);
                        //  lblStatus.Text = string.Format("{0} processed.", posBadBatFlNm);
                        //  lblStatus.Update();
                        //}

                        //Block If replaced with export tag in the posbat xml file
                        //if (!ppbm.HasMissionAborted() && rdoBtnBatchAndExport.Checked)
                        //{

                        //  lblStatus.Text = string.Format("Exporting {0}.", posBatFlNm);
                        //  lblStatus.Update();
                        //  string fldrNm = string.Format("{0}\\{1}",
                        //                                           txtBxLocalFolder.Text,
                        //                                           Path.GetFileNameWithoutExtension(posBatFlNm));
                        //  ExportBatchMissions exBtMsn = new ExportBatchMissions(pyExeFlNm,
                        //                                                        fNfSBETPOSPacExportPy,
                        //                                                        fldrNm,
                        //                                                        posPacExe, dr["Video Location"].ToString(),
                        //                                                        dr["Batch Name"].ToString());
                        //  lblStatus.Text = string.Format("{0} exported.", posBatFlNm);
                        //  lblStatus.Update();
                        //}
                        }
                    else
                    {
                        string sub = "processed by another batch";
                        string comment = dr["Comments"].ToString();
                        if (comment.Contains(sub))
                        {
                            string msg = string.Format("{0}", dr["Comments"].ToString());
                            MessageBox.Show(msg);
                            imm.Add(dr["Batch Name"].ToString());
                        }
                        string basep = dr["Video Location"].ToString();
                        string[] arra = basep.Split('\\');
                        string proj = arra[arra.Length - 1];

                        string logfile = basep + "\\" + proj + "-LOGS" + "\\" + proj + "-" + t + ".txt";
                        using (System.IO.StreamWriter file =
                                new System.IO.StreamWriter(logfile, true))
                        {
                            string processing = string.Format("Could Not Process {0} at {1}.", t, DateTime.Now.ToString());
                            file.WriteLine(processing);
                            string status = string.Format("Status:{0}", dr["Comments"].ToString());
                            file.WriteLine(status);
                            file.WriteLine("");
                        }

                    }
                }
                else
                {
                    
                }time += 1;
                }
            bool done = Remove(imm);
            if (done)
            {
                imm.Clear();
            }

        }

    private void rdoBtnGoogleChrome_CheckedChanged(object sender, EventArgs e)
    {
      lblStatus.Text = string.Empty;
      lblStatus.Update();
    }

    private void rdoBtnInternetExplorer_CheckedChanged(object sender, EventArgs e)
    {
      lblStatus.Text = string.Empty;
      lblStatus.Update();
    }

    private void SaveFolderOrFile(string nm, string pthOrFlNm)
    {
      string fltr = string.Format("[Name] = '{0}'", nm);
      DataRow[] dr = fNfDt.Select(fltr);

      if (dr.Length > 0)
      {
        //  string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        //  if (pthOrFlNm.Contains(pathUser))
        //  {
        //    dr[0]["PathOrFileName"] = pthOrFlNm.Substring(pathUser.Length + 1);
        //  }
        //  else
        //  {
            dr[0]["PathOrFileName"] = pthOrFlNm;
        //  }

        Common cmn = new Common();
        string fldrsNFlsXMLFile = Path.Combine(cmn.UserDataFolder(), xmlFilesAndFoldersNm);
        fNfDs.WriteXml(fldrsNFlsXMLFile);
      }
    }

        private void SetupBatchesForProcessing()//5.1 //copy pcs files into own folder and take away skipped
        {
            List<string> copychk = new List<string>();
            List<string> imm = new List<string>();
            copychk = Begin();
            inc.Clear();
            inc = copychk;
            if (inc.Count() != 0)
            {
                if (inc[0] == "fail")
                {
                    return;
                }
            }
            DataRow[] drs = batchDt.Select("[Process] = True", "[Project Name], [Batch Name], [Video Location]");
            foreach (DataRow dr in drs)
            {
                string t = dr["Batch Name"].ToString();
                if (!copychk.Contains(t))
                {
                    Console.WriteLine("reached here");
                    //Create Project folder(s)
                    string dbFldrNm = Path.Combine(strLocalProjectFolder, dr["Database Name"].ToString());
                    //if (!Directory.Exists(prjctFldrNm)) { Directory.CreateDirectory(prjctFldrNm); }

                    //Create ARAN folder(s)
                    string aranFldrNm = string.Empty;
                    aranFldrNm = Path.Combine(dbFldrNm, dr["ARAN"].ToString());
                    //if (!Directory.Exists(aranFldrNm)) { Directory.CreateDirectory(aranFldrNm); }

                    //Create Batch folder(s)
                    string batchFldrNm = Path.Combine(aranFldrNm, dr["Batch Name"].ToString());
                    //if (!Directory.Exists(batchFldrNm)) { Directory.CreateDirectory(batchFldrNm); }

                    //Create PCS folder(s)
                    //string pcsFldrNm = Path.Combine(batchFldrNm, "PCS");
                    //if (!Directory.Exists(pcsFldrNm)) { Directory.CreateDirectory(pcsFldrNm); }
                    string pcsFldrNm = Path.Combine(strServerProjectFolder, dr["Database Name"].ToString());
                    if (!Directory.Exists(pcsFldrNm)) { Directory.CreateDirectory(pcsFldrNm); }

                    pcsFldrNm = Path.Combine(pcsFldrNm, dr["ARAN"].ToString());
                    if (!Directory.Exists(pcsFldrNm)) { Directory.CreateDirectory(pcsFldrNm); }
                    aranFldrNm = pcsFldrNm;

                    pcsFldrNm = Path.Combine(pcsFldrNm, dr["Batch Name"].ToString());
                    if (!Directory.Exists(pcsFldrNm)) { Directory.CreateDirectory(pcsFldrNm); }

                    pcsFldrNm = Path.Combine(pcsFldrNm, "PCS");
                    if (!Directory.Exists(pcsFldrNm)) { Directory.CreateDirectory(pcsFldrNm); }

                    //Migrate POS Files
                    //string[] fldrPrts = dr["Video Location"].ToString().Split('\\');
                    //fldrPrts[2] = string.Format("{0}.Roadware.com", fldrPrts[2]);
                    //MigratePOSFiles mpf = new MigratePOSFiles(aranFldrNm,
                    //                                          batchFldrNm,
                    //                                          string.Join("\\", fldrPrts),
                    //                                          dr["Data Collection From Date"].ToString(),
                    //                                          dr["Data Collection To Date"].ToString());

                    MigratePOSFiles mpf;

                    //if (rdoBtnFolderPost14.Checked)
                    //{
                    //migrate for new posdata structure 2014
                    mpf = new MigratePOSFiles(aranFldrNm,
                                              batchFldrNm,
                                              dr["Video Location"].ToString(),
                                              dr["Data Collection From Date"].ToString(),
                                              dr["Data Collection To Date"].ToString(),
                                              dr["ARAN"].ToString(),
                                              pcsFldrNm);
                    //}
                    string sub = "processed by another batch";
                    string comment = dr["Comments"].ToString();
                    if (comment.Contains(sub))
                    {
                        imm.Add(dr["Batch Name"].ToString());
                    }
                    //else
                    //{
                    //    mpf = new MigratePOSFiles(aranFldrNm,
                    //                                              batchFldrNm,
                    //                                              dr["Video Location"].ToString(),
                    //                                              dr["Data Collection From Date"].ToString(),
                    //                                              dr["Data Collection To Date"].ToString(),
                    //                                              pcsFldrNm);
                    //    //migrate pos/pcs files 5.1.1->new class
                    //}

                    dr["Comments"] = mpf.GetComments();
                    string projpath = dr["Video Location"].ToString();
                    string[] arra = projpath.Split('\\');
                    string proj1 = arra[arra.Length - 1];
                    string mode = "";
                    if (cmboBxGNSSMode.SelectedValue.ToString().Contains("SmartBase"))
                    {
                        mode = cmboBxGNSSMode.SelectedValue.ToString();
                    }
                    else
                    {
                        mode = cmboBxGNSSMode.SelectedValue.ToString() + "_" + proj1;
                    }

                    SetupBatchMissions sbm = new SetupBatchMissions(pcsFldrNm, mode, strTemplateFile, txtBxLocalFolder.Text);
                    //migrate pos/pcs files 5.1.2->new class

                    prjcts = sbm.GetProjects();

                    if (!string.IsNullOrEmpty(prjcts))
                    {
                        WriteBatchProcessFile(string.Format("{0}_{1}", dr["Database Name"].ToString(), dr["Batch Name"].ToString()));
                    }

                    LoadBatches();
                }
                else
                {
                    //couldnt copy this batch folders
                }
            } bool done = Remove(imm);
            if (done)
            {
                imm.Clear();
            }
        }
    private void tmrStartDateAndTime_Tick(object sender, EventArgs e)
    {
      if (timeLeft.Days > 0 || timeLeft.Hours > 0 || timeLeft.Minutes > 0 || timeLeft.Seconds > 0)
      {
        TimeSpan second = TimeSpan.FromSeconds(1);
        timeLeft = timeLeft.Subtract(second);
        if (timeLeft.Days > 0)
        {
          lblStatus.Text = string.Format("{0} days {1}:{2}:{3} remaining before process starts.",
                                         timeLeft.Days,
                                         timeLeft.Hours.ToString("00"),
                                         timeLeft.Minutes.ToString("00"),
                                         timeLeft.Seconds.ToString("00"));
        }
        else
        {
          lblStatus.Text = string.Format("{0}:{1}:{2} remaining before process starts.",
                                         timeLeft.Hours.ToString("00"),
                                         timeLeft.Minutes.ToString("00"),
                                         timeLeft.Seconds.ToString("00"));
        }
        lblStatus.Update();
      }
      else
      {
        tmrStartDateAndTime.Stop();
        btnStartBatchProcess.Enabled = false;
        ProcessBatches(false);
        btnStartBatchProcess.Enabled = true;
        ChangeControlStatus(true);
        btnStartBatchProcess.Text = "Start Batch Process";
        lblStatus.Text = "Processing Completed!!";
        lblStatus.Update();
        evefin = false;
        bool wait = false;//change it back to false
                if (failed.Count() != 0)
                {
                    Cursor.Current = Cursors.Default;
                    if (checkBox1.Checked == true)
                    {
                        ExitWindowsEx(4, 0);
                    }
                    evefin = true;
                    var result = MessageBox.Show("Do you want to delete now", "", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        wait = Remove(inc, failed);
                        if (wait)
                        {
                            MessageBox.Show("Done Deleting");
                            evefin = true;
                        }
                        else
                        {
                            MessageBox.Show("Could not delete - check log");
                        }
                    }
                    else if (result == DialogResult.No)
                    {
                        MessageBox.Show("Did not delete");
                    }
                }
                else
                {
                    evefin = false;
                    MessageBox.Show("POSpac aborted for the batch(s)- check log");
                }

        inc.Clear();
        failed.Clear();
        lblStatus.Update();
        //lblStatus.Text = string.Format("Use the batch xml file ({0}) to load POSPac projects.", btchPrjFlNm);
        Cursor.Current = Cursors.Default;
            if (checkBox1.Checked == true)
            {
                    ExitWindowsEx(4, 0);
            }
      }
    }
    [DllImport("user32.dll")]
    static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

    private void UpdateTextBoxChanges(ref TextBox txtBx, ref string strCmpr, string fldNm) //why "ref"s
    {
      if (txtBx.Text != strCmpr)
      {
        strCmpr = txtBx.Text;
        SaveFolderOrFile(fldNm, strCmpr);
      }
    }

    private void WriteBatchProcessFile(string posBatFlNm)
    {
      Common cmn = new Common();
      string btch = cmn.GetTags("Batch");
      btch = btch.Replace("##Projects##", prjcts);
      string btchPrjFullFlNm = Path.Combine(strLocalProjectFolder, string.Format("{0}.posbat",posBatFlNm));
      using (StreamWriter sw = new StreamWriter(btchPrjFullFlNm))
      {
        sw.Write(btch);
      }
      string xmlPrjFlNm = Path.Combine(strLocalProjectFolder, string.Format("{0}.xml", posBatFlNm));
      fNfDs.WriteXml(xmlPrjFlNm);
    }
    #endregion

    private void btnTest_Click(object sender, EventArgs e)
    {
      //DataRow[] dr = batchDt.Select("[Process] = True");
      //for (int i = 0; i < dr.Length; i++)
      //{
      //  //dr[i]["Comments"] = "Selected";
      //  Debug.WriteLine(dr[i]["Template File"].ToString());
      //}
      ProcessBatches(true);
    }

        private void Button1_Click(object sender, EventArgs e)
        {
            DataRow[] fltr = batchDt.Select("[Process] = True", "[ARAN], [Video Location], [Batch Name]");
            string hm = fltr[0]["ARAN"].ToString();
            MessageBox.Show("Copying...");
            List<string> end = new List<string>();
            end = Begin();
            bool wait = false;
            MessageBox.Show("Done Copying");
            var result = MessageBox.Show("Do you want to delete now","", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                 wait = Remove(end,failed);
            }

            if (wait)
            {
                MessageBox.Show("Done Deleting");
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            DataRow[] fltr = batchDt.Select("[Process] = True", "[ARAN], [Video Location], [Batch Name]");
            foreach (DataRow i in fltr)
            {
                string db = i["Database Name"].ToString();
                string batch = i["Batch Name"].ToString();
                string projpath = i["Video Location"].ToString();
                string[] arra = projpath.Split('\\');
                string proj1 = arra[arra.Length - 1];
                string aran = i["ARAN"].ToString();
                string dfrom = i["Data Collection From Date"].ToString();
                string dto = i["Data Collection To Date"].ToString();
                string filter = @"\\video-01\Operations\SBETs\Apps\iSBET-test\ccoxpias-roadware.sbet_processing-78fa4d4b2d42\filter.txt";

                try
                {
                    if (File.Exists(filter))
                    {
                        File.Delete(filter);
                    }

                    using (StreamWriter sw = new StreamWriter(filter, false))
                    {
                        sw.WriteLine(db);
                        sw.WriteLine(batch);
                        sw.WriteLine(proj1);
                        sw.WriteLine(dfrom);
                        sw.WriteLine(dto);
                        sw.WriteLine(aran);
                    }

                }
                catch (Exception ex)
                {
                    lblStatus.Text = ex.Message.ToString();
                    lblStatus.Update();
                }

                //start the filter process
                string python = @"\\video-01\Operations\SBETs\Apps\Python27\python.exe";
                string filename = @"\\video-01\Operations\SBETs\Apps\iSBET-test\dbtest.py";
                ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(python);

                myProcessStartInfo.UseShellExecute = false;
                myProcessStartInfo.RedirectStandardOutput = true;

                myProcessStartInfo.Arguments = filename;

                Process myProcess = new Process();
                // assign start information to the process 
                myProcess.StartInfo = myProcessStartInfo;
                // start the process 
                myProcess.Start();
                myProcess.WaitForExit();
                myProcess.Close();
                lblStatus.Text = "Done";
                lblStatus.Update();
            }
        }

        private void SBETQCButton(object sender, EventArgs e)
        {
            //happens when all copy is done 
            evefin = true;
            if (evefin == false)
            {
                MessageBox.Show("Cannot perform Action - check Logs to make sure iSBET was completed");
            }
            else
            {

                txtBxLocalFolder.Update();
                txtBxServerFolder.Update();
                SBETQC a = new SBETQC(this);
                repup = true;
                //a.ShowDialog();

            }

        }

        private void ReportBackupButton(object sender, EventArgs e)
        {
            //happens when everything is processed and its safe to do this
            repup = true;
            if (repup == true)
            {
                txtBxLocalFolder.Update();
                txtBxServerFolder.Update();
                BackupReport a = new BackupReport(this);
                //a.ShowDialog();
            }
        }

        private void Button1_Click_2(object sender, EventArgs e)
        {

            string backup = txtBxLocalFolder.Text;
            if (string.IsNullOrEmpty(backup))
            {
                MessageBox.Show("No local path entered in Local Project Folder!");
                return;
            }
            else
            {
                DataRow[] fltr;
                try
                {
                    fltr = batchDt.Select("[Process] = True", "[Backup Tape Location], [Batch Name], [Video Location]");
                }
                catch(Exception er)
                {
                    MessageBox.Show(er.Message);
                    return;
                }
                foreach (DataRow i in fltr)
                {
                    string batch = i["Batch Name"].ToString();
                    string projpath = i["Video Location"].ToString();
                    string[] arra = projpath.Split('\\');
                    string proj1 = arra[arra.Length - 1];
                    string batchP = i["Backup Tape Location"].ToString();
                    string b = batch; string fbtch = b;
                    int ns = b.Length;

                    if (b.Contains("CTRL"))
                    {
                        batch = b;
                    }
                    else
                    {
                        batch = "Batch_" + b;
                    }

                    string fullname = backup + @"\Data\" + batch;

                    if (Directory.Exists(fullname))
                    {
                        FileInfo[] n; DirectoryInfo check;
                        try
                        {
                            check = new DirectoryInfo(fullname);
                            n = check.GetFiles();
                        }
                        catch(Exception err)
                        {
                            MessageBox.Show(err.Message);
                            return;
                        }

                        if (n.Count() == 0)
                        {
                            MessageBox.Show("SBET QC should be processed first");
                            return;
                        }

                        else
                        {
                            try
                            {
                                ProcessStartInfo launch = new ProcessStartInfo(@"\\video-01\Operations\SBETs\Apps\SbetUploading\SbetUploading.exe");
                                launch.RedirectStandardOutput = true;
                                launch.UseShellExecute = false;
                                launch.CreateNoWindow = false;

                                Process p = new Process();
                                p.StartInfo = launch;
                                p.Start();
                                p.WaitForExit();
                                while (!p.HasExited)
                                {
                                    Console.WriteLine("Waiting...");
                                }
                                p.Close();
                                p.Dispose();
                                return;
                            }
                            catch(Exception error)
                            {
                                Console.WriteLine(error.Source.ToString());
                                Console.WriteLine(error.StackTrace.ToString());
                                Console.WriteLine(error.InnerException.ToString());
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(string.Format("{0} does not exist", fullname));
                        return;
                    }
                }
            }
          
        }

        private void DtaGrdVwBatchDt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void sendMail(string posbat, string proj1, string batch)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load("client.txt");
            }
            catch(Exception ads)
            {
                MessageBox.Show(ads.Message);
            }
            string p = "/email/to";
            XmlNode xmlNode = doc.DocumentElement.SelectSingleNode(p);
            string email = xmlNode.InnerText;
            try
            {
                Microsoft.Office.Interop.Outlook.Application app = new Microsoft.Office.Interop.Outlook.Application();
                Microsoft.Office.Interop.Outlook.MailItem mail = app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

                mail.To = email;// "aandres@fugro.com";
                mail.Subject = "Pospac SBETs: " + proj1 + " " + batch + "--Failed";
                mail.Body = @"Pospac aborted due to batch error.
                              FilePath to PosBat: " + posbat;
                mail.Importance = Microsoft.Office.Interop.Outlook.OlImportance.olImportanceHigh;
                mail.Send();
            }
            catch(Exception asds)
            {
                MessageBox.Show(asds.Message);
            }
            return;
        }

        private void outlook()
        {
            DataRow[] fltr;
            try
            {
                fltr = batchDt.Select("[Process] = True", "[Backup Tape Location], [Batch Name], [Video Location]");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                return;
            }
            foreach (DataRow i in fltr)
            {
                string batch = i["Batch Name"].ToString();
                string projpath = i["Video Location"].ToString();
                string[] arra = projpath.Split('\\');
                string proj1 = arra[arra.Length - 1];
                string batchP = i["Backup Tape Location"].ToString();

                string b = batch; string fbtch = b;
                int ns = b.Length;

                if (b.Contains("CTRL"))
                {
                    batch = b;
                }
                else
                {
                    batch = "Batch_" + b;
                }

                string report = projpath + "\\Reports\\" + batch;
                DirectoryInfo main = new DirectoryInfo(report);
                FileInfo[] rep = main.GetFiles("*.pdf");
                string repo = rep[0].FullName;

                Microsoft.Office.Interop.Outlook.Application app = new Microsoft.Office.Interop.Outlook.Application();
                Microsoft.Office.Interop.Outlook.MailItem mail = app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

                mail.To = "aandres@fugro.com";
                mail.Subject = proj1 + "_" + batch + "--Failed";
                mail.Body = @"Pospac aborted due to batch error.
                              FilePath to PosBat: " + projpath + "_" + batch + ".posbat";
                mail.Importance = Microsoft.Office.Interop.Outlook.OlImportance.olImportanceHigh;
                mail.Attachments.Add(repo, Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue, 1, rep[0].Name);
                mail.Send();
            }

        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
             XmlDocument doc = new XmlDocument();
             doc.Load("client.txt");
             string p = "/email/to";
             XmlNode xmlNode = doc.DocumentElement.SelectSingleNode(p);
             string email = xmlNode.InnerText;

             Microsoft.Office.Interop.Outlook.Application app = new Microsoft.Office.Interop.Outlook.Application();
             Microsoft.Office.Interop.Outlook.MailItem mail = app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

             mail.To = email; //"aandres@fugro.com";
             mail.Subject = "Failed";
             mail.Body = @"Pospac aborted due to batch error.
                               FilePath to PosBat: ";
             mail.Importance = Microsoft.Office.Interop.Outlook.OlImportance.olImportanceHigh;
             mail.Save();
        }

        private void PinPoint_Click(object sender, EventArgs e)
        {
            /*string batch = "Batch_003";
            string projdir = @"\\video-01\Operations\SBETs\AK19_85132\AK19_85132_003";
            string video = @"\\video-01\Operations\SBETs\AK19_85132";
            PinPoint a = new PinPoint(video, batch, projdir);
            a.ShowDialog();
            while (a.next == false)
            {
                bool n = a.next;
                Console.WriteLine(n.ToString());
                MessageBox.Show(n.ToString());
                //do nothing
            }
            string batch1 = "CTRL_007";
            string projdir1 = @"\\video-01\Operations\SBETs\SC19_85127\SC19_85127_Controls_CTRL_007";
            string video1 = @"\\video-01\Operations\SBETs\SC19_85127";
            PinPoint a1 = new PinPoint(video1, batch1, projdir1);
            a1.ShowDialog();*/
            DataRow[] fltr;
             try
             {
                 fltr = batchDt.Select("[Process] = True", "[Backup Tape Location], [Batch Name], [Video Location]");
             }
             catch (Exception er)
             {
                 MessageBox.Show(er.Message);
                 return;
             }
             foreach (DataRow i in fltr)
             {
                 string db = i["Database Name"].ToString();
                 string batch = i["Batch Name"].ToString();
                 string projpath = i["Video Location"].ToString();
                 string[] arra = projpath.Split('\\');
                 string proj1 = arra[arra.Length - 1];
                 string aran = i["ARAN"].ToString();
                 string dfrom = i["Data Collection From Date"].ToString();
                 string dto = i["Data Collection To Date"].ToString();
                 string dir = projpath + "\\" + proj1 + "-LOGS";
                 string batchP = i["Backup Tape Location"].ToString();
                 //Console.WriteLine("Starting");
                 string projdir = projpath + "\\" + db + "_" + batch;

                 string b = batch; string fbtch = b;
                 int ns = b.Length;

                 if (String.IsNullOrEmpty(batchP) == false)
                 {
                     string[] asd = batchP.Split('\\');
                     batch = asd[asd.Length - 1];
                 }
                 else
                 {
                     if (b.Contains("CTRL_ARAN"))
                     {

                         if (b.Contains("Entry"))
                         {
                             string path = @"\\video-01\" + proj1 + "\\" + b;
                             if (Directory.Exists(path))
                             {
                                 batch = b;
                             }
                             else
                             {
                                 batch = "Batch001";
                             }
                         }
                         else if (b.Contains("Exit"))
                         {
                             string path = @"\\video-01\" + proj1 + "\\" + b;
                             if (Directory.Exists(path))
                             {
                                 batch = b;
                             }
                             else
                             {
                                 path = @"\\video-01\" + proj1;
                                 int temp = 0;
                                 //string[] filenames = Directory.GetFiles(path, "Batch*");
                                 DirectoryInfo last = new DirectoryInfo(path);

                                 foreach (var nam in last.GetDirectories("Batch*"))
                                 {
                                     string cut = nam.Name.Remove(0, 5);
                                     int max = Convert.ToInt32(cut);
                                     if (temp < max)
                                     {
                                         temp = max;
                                     }

                                 }
                                 string add = temp.ToString(); string gadd;
                                 if (add.Length == 1)
                                 {
                                     gadd = "00" + add;
                                 }
                                 else if (add.Length == 2)
                                 {
                                     gadd = "0" + add;
                                 }
                                 else
                                 {
                                     gadd = add;
                                 }
                                 batch = "Batch" + gadd;
                             }
                         }
                     }

                     else if (b.Contains("CTRL"))
                     {

                         string pa = @"\\video-01\" + proj1 + "\\" + b;
                         if (Directory.Exists(pa))
                         {
                             batch = b;
                         }
                         else
                         {
                             if (ns == 9)
                             {
                                 batch = "Batch" + b.Substring(5, 4);
                             }
                             else
                             {
                                 batch = "Batch" + b.Substring(5, 3);
                             }
                         }
                     }

                     else if (ns > 3)
                     {
                         batch = b.Remove(3);
                         batch = "Batch" + batch;
                     }
                     else
                     {
                         batch = "Batch" + b;
                     }
                 }
                 PinPoint a = new PinPoint(i["Video Location"].ToString(),batch,projdir);
                 a.ShowDialog();
                 while (a.next == false)
                 {
                     //do nothing
                 }
             }
        }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RoadwareSBETClassLibrary.Domain;

namespace RoadwareSBET.Domain
{
  public partial class frmSBETBatchExport : Form
  {
    #region fields
    private string fNfSBETPOSPacExportPy;
    private string lclFldrNm;
    private string posPacExe;
    private string pyExeFlNm;
    //private string strServerProjectFolder;
    #endregion

    #region constructors
    public frmSBETBatchExport(string localFolderForProjects,
                              string pythonExecutableFolderAndFileName, 
                              string pythonExportScriptFolderAndFileName,
                              string posPacExecutableFolderAndFileName)
    {
      lclFldrNm = localFolderForProjects;
      pyExeFlNm = pythonExecutableFolderAndFileName;
      fNfSBETPOSPacExportPy = pythonExportScriptFolderAndFileName;
      posPacExe = posPacExecutableFolderAndFileName;
      InitializeComponent();
      InitializeControls();
    }
    #endregion

    #region methods
    private void btnChangeLocalFolder_Click(object sender, EventArgs e)
    {
      lblStatus.Text = "Changing local folder.";
      lblStatus.Update();
      FolderBrowserDialog fbDlg = new FolderBrowserDialog();
      if (!string.IsNullOrEmpty(lclFldrNm)) { fbDlg.SelectedPath = lclFldrNm; }
      if (fbDlg.ShowDialog() == DialogResult.OK)
      {
        lclFldrNm = fbDlg.SelectedPath;
        LoadFolderList();
        lblStatus.Text = "Local folder changed.";
        lblStatus.Update();
      }
      else
      {
        lblStatus.Text = "Local folder not changed.";
        lblStatus.Update();
      }
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
      Cursor.Current = Cursors.WaitCursor;
      CreateExportProjectBatchFiles();
      //MessageBox.Show("Exported POSPac files need to be copied to the appropriate video server folder(s).", "FYI!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
      Cursor.Current = Cursors.Default;
    }

    private void ckBxMigrateToServer_Click(object sender, EventArgs e)
    {
      LoadFolderList();
    }

    //private void ckLstBx_ItemCheck(object sender, ItemCheckEventArgs e)
    //{
    //  int itmIdx = e.Index;
    //  //MessageBox.Show(string.Format("Index {0} was checked.",itmIdx));
    //  if (e.CurrentValue == CheckState.Unchecked)
    //  {
    //    FolderBrowserDialog fbDlg = new FolderBrowserDialog();
    //    if (!string.IsNullOrEmpty(lclFldrNm)) { fbDlg.SelectedPath = lclFldrNm; }
    //    fbDlg.Description = "Select the export folder location.";
    //    if (fbDlg.ShowDialog() == DialogResult.OK)
    //    {
    //      ckLstBx.Items[itmIdx] = string.Format("{0} ~ {1}", ckLstBx.Items[itmIdx], fbDlg.SelectedPath);
    //    }
    //    else
    //    {
    //      string[] itmNm = ckLstBx.Items[itmIdx].ToString().Split('~');
    //      ckLstBx.Items[itmIdx] = itmNm[0].Trim();
    //      e.NewValue = CheckState.Unchecked;
    //    }
        
    //  }
    //  else
    //  {
    //    string[] itmNm = ckLstBx.Items[itmIdx].ToString().Split('~');
    //    ckLstBx.Items[itmIdx] = itmNm[0].Trim();
    //  }
    //}

    private void CreateExportProjectBatchFiles()
    {
      string[] ckedItms = new string[ckLstBx.CheckedItems.Count];
      int n = -1;
      foreach (object ckedItm in ckLstBx.CheckedItems)
      {
        n++;
        ckedItms[n] = ckedItm.ToString();
      }

      foreach (string ckedItm in ckedItms)
      {
        ckLstBx.SelectedIndex = ckLstBx.Items.IndexOf(ckedItm);
        string[] ckedItmPrts = ckedItm.ToString().Split('~');

          // stupid split thing

        string topFldrNm = Path.Combine(lclFldrNm, ckedItmPrts[0].Trim());

        lblStatus.Text = string.Format("Exporting POSPac data for {0}.", topFldrNm);
        lblStatus.Update();

        if (ckedItmPrts.Length > 1)
        {
          if (rdoBtnBatch.Checked)
          {

            ExportBatchMissions exBtMsn = new ExportBatchMissions(pyExeFlNm,
                                                                  fNfSBETPOSPacExportPy,
                                                                  topFldrNm,
                                                                  posPacExe,
                                                                  ckedItmPrts[1].Trim());
          }
          else
          {
            ExportBatchMissions exBtMsn = new ExportBatchMissions(pyExeFlNm,
                                                                  fNfSBETPOSPacExportPy,
                                                                  topFldrNm,
                                                                  posPacExe,
                                                                  ckedItmPrts[1].Trim(),
                                                                  true);
          }
        }
        else
        {
          if (rdoBtnBatch.Checked)
          {
            ExportBatchMissions exBtMsn = new ExportBatchMissions(pyExeFlNm,
                                                                  fNfSBETPOSPacExportPy,
                                                                  topFldrNm,
                                                                  posPacExe);
          }
          else
          {
            ExportBatchMissions exBtMsn = new ExportBatchMissions(pyExeFlNm,
                                                                  fNfSBETPOSPacExportPy,
                                                                  topFldrNm,
                                                                  posPacExe,
                                                                  true);
          }
        }

        lblStatus.Text = string.Format("POSPac data for {0} exported.", topFldrNm);
        lblStatus.Update();
      }
      ckLstBx.ClearSelected();
    }

    private void InitializeControls()
    {
      LoadFolderList();

      lblStatus.Text = string.Empty;
      lblStatus.Update();
    }

    private void LoadFolderList()
    {
      lblFolders.Text = string.Format("Batch folders in {0} ~ Export folder.", lclFldrNm);
      ckLstBx.Items.Clear();
      foreach (string subDirNm in Directory.GetDirectories(lclFldrNm).OrderBy(f=>f))
      {
        string[] subDirPrts = subDirNm.Split('\\');
        //if (ckBxMigrateToServer.Checked)
        //{
        //  ckLstBx.Items.Add(string.Format("{0} ~ {1}", subDirPrts[subDirPrts.Length - 1], strServerProjectFolder), false);
        //}
        //else
        //{
          ckLstBx.Items.Add(subDirPrts[subDirPrts.Length - 1], false);
        //}
      }
    }

    private void RadioButtonClicked()
    {
      if (rdoBtnBatch.Checked)
      {
        btnExport.Text = "Batch Export";
      }
      else if (rdoBtnMission.Checked)
      {
        btnExport.Text = "Mission Export";
      }
    }

    private void rdoBtnBatch_CheckedChanged(object sender, EventArgs e)
    {
      RadioButtonClicked();
    }

    private void rdoBtnMission_CheckedChanged(object sender, EventArgs e)
    {
      RadioButtonClicked();
    }
    #endregion

  }

}

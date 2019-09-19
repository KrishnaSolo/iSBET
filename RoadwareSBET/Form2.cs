using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
namespace RoadwareSBET
{
   
    public partial class BackupReport : Form
    {
        private RoadwareSBET.Domain.frmSBETDialog sa = new RoadwareSBET.Domain.frmSBETDialog();
        public BackupReport(RoadwareSBET.Domain.frmSBETDialog sda)
        {
           // InitializeComponent();
           // stats.Text = "Enter folders";
            //stats.Update();

            //string lcl = sda.txtBxLocalFolder.Text;
            //if (string.IsNullOrEmpty(lcl))
            //{
                //textBox1.Text = string.Empty;
              //  textBox2.Text = string.Empty;
            //}
            //else
            //{
               // textBox1.Text = lcl;
              //  textBox2.Text = lcl + "\\Reports";
            //}
            sa = sda;
            var cond = MessageBox.Show("WARNING: The following action will delete data folder(vrms,sbet etc) and PCs folder and Batch folder. Make sure you have done QC and done Uploading to AWS before continuing!", "Warning", MessageBoxButtons.YesNo);

            if (cond == DialogResult.No)
            {
                return;
            }
            a();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
        }

        private void a()
        { 
            string lcl = sa.txtBxLocalFolder.Text;
            string txtbx1 = lcl;
            string txtbx2 = lcl + "\\Reports";

            List<string> db = new List<string>();
            List<string> ar = new List<string>();
            List<string> bat = new List<string>();
            List<string> p = new List<string>();
            DataRow[] fltr = new DataRow[0];
            try
            {
                fltr = sa.batchDt.Select("[Process] = True", "[Backup Tape Location], [Batch Name], [Video Location], [Database Name]");
            }
            catch (Exception esa)
            {
                MessageBox.Show(esa.Message);
                //return;
            }

            if (fltr.Length != 0)
            { 
                foreach (DataRow i in fltr)
                {
                    string batch = i["Batch Name"].ToString();
                    string projpath = i["Video Location"].ToString();
                    string[] arra = projpath.Split('\\');
                    string proj1 = arra[arra.Length - 1];
                    //string proj1 = projpath.Remove(0, 28);
                    string batchP = i["Backup Tape Location"].ToString();
                    string d = i["Database Name"].ToString();
                    db.Add(i["Database Name"].ToString());
                    ar.Add(i["ARAN"].ToString());
                    bat.Add(batch);
                    p.Add(proj1);


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
                    

                    if (string.IsNullOrEmpty(txtbx1) || string.IsNullOrEmpty(txtbx2))
                    {
                        MessageBox.Show("One or both field are still empty! Please fill them now.");
                        return;
                    }
                        if (Directory.Exists(txtbx1) == false)
                        {
                            MessageBox.Show(string.Format("Project: {0} does not exist",proj1));
                            //return;
                        }
                        else
                        {
                            //stats.Text = "Creating Folders...";
                            //stats.Update();
                            string proj = txtbx1 + "\\" + d + "_" + b;
                            string repo = txtbx2;
                            //stats.Text = "Proj Folder: " + d;
                            //stats.Update();
                        if (Directory.Exists(proj) == false)
                        {
                            MessageBox.Show("Directory does not exist: " + proj);
                            return;
                        }
                        try
                            {
                                if (Directory.Exists(repo) == false)
                                {
                                    Directory.CreateDirectory(repo);
                                }
                            }
                            catch (Exception es)
                            {
                                MessageBox.Show(es.Message);
                                return;
                            }
                            DirectoryInfo main = new DirectoryInfo(proj);
                            string filename = main.Name;
                            string fn = filename;

                            int maxn = fn.Length;
                            string upd = batch;
                            repo = repo + "\\" + upd;
                            try
                            {
                                if (Directory.Exists(repo) == false)
                                {
                                    Directory.CreateDirectory(repo);
                                }

                            }
                            catch (Exception es)
                            {
                                MessageBox.Show(es.Message);
                                return;
                            }
                            DirectoryInfo inner = new DirectoryInfo(proj);
                            DirectoryInfo[] mission = inner.GetDirectories("LV*");
                            FileInfo[] top = inner.GetFiles("*.log", SearchOption.TopDirectoryOnly);
                        foreach (FileInfo hov1 in top)
                        {
                            string end1 = repo + "\\" + hov1.Name;
                            int si1 = 2; string asa1 = end1;
                            string bases = hov1.Name.Remove(hov1.Name.Length - 4);
                            string exts = hov1.Name.Substring(hov1.Name.Length - 4);
                            while (File.Exists(asa1))
                            {
                                asa1 = repo + "\\" + bases + " (" + si1 + ")" + exts;
                                si1 += 1;
                            }
                            end1 = asa1;
                            try
                            {
                                File.Copy(hov1.FullName, end1);
                            }
                            catch (Exception ed)
                            {
                                MessageBox.Show(ed.Message);
                            }
                        }

                        //stats.Text = string.Empty;
                          //  stats.Update();
                            //stats.Text = "Copying files (txt,log)...";
                            //stats.Update();
                            foreach (DirectoryInfo di in mission)
                            {
                                FileInfo[] inn = di.GetFiles("*.txt", SearchOption.AllDirectories);

                                foreach (FileInfo hov in inn)
                                {
                                    string end = repo + "\\" + hov.Name;
                                    int si = 2; string asa = end;
                                    string bases = hov.Name.Remove(hov.Name.Length - 4);
                                    string exts = hov.Name.Substring(hov.Name.Length - 4);
                                    while (File.Exists(asa))
                                    {
                                        asa = repo + "\\" + bases + " (" + si + ")" + exts;
                                        si += 1;
                                    }
                                    end = asa;
                                try
                                {
                                    File.Copy(hov.FullName, end);
                                }
                                catch (Exception ed)
                                {
                                    MessageBox.Show(ed.Message);
                                }
                            }
                                FileInfo[] innd = di.GetFiles("*.log", SearchOption.AllDirectories);

                                foreach (FileInfo hov in innd)
                                {
                                    try
                                    {
                                        string end = repo + "\\" + hov.Name;
                                        int si = 2; string asa = end;
                                        string bases = hov.Name.Remove(hov.Name.Length - 4);
                                        string exts = hov.Name.Substring(hov.Name.Length - 4);
                                        while (File.Exists(asa))
                                        {
                                            asa = repo + "\\" + bases + " (" + si + ")" + exts;
                                            si += 1;
                                        }
                                        end = asa;
                                    try
                                    {
                                        File.Copy(hov.FullName, end);
                                    }
                                    catch (Exception ed)
                                    {
                                        MessageBox.Show(ed.Message);
                                    }
                                }
                                    catch (Exception me)
                                    {
                                        MessageBox.Show(me.Message);
                                        //return;
                                    }

                                }


                            }
                        }
                }
                //stats.Text = "Done backing up Reports";
                //stats.Update();
            }
            else
            {
                DirectoryInfo a = new DirectoryInfo(txtbx1);
                DirectoryInfo b = a.Parent;
                txtbx2 = b.FullName + "\\Reports";
                string okay = txtbx1;
                string[] words = okay.Split('\\');
                int cnt = words.Length;
                string proj = words[cnt - 2];
                string[] text = words[cnt - 1].Split('_');
                int n = text.Length;
                string btch = text[n - 1];
                string hardp = txtbx1;
                if (Directory.Exists(hardp))
                {
                    string repo = txtbx2;
                    try
                    {
                        if (Directory.Exists(repo) == false)
                        {
                            Directory.CreateDirectory(repo);
                        }
                    }
                    catch (Exception es)
                    {
                        MessageBox.Show(es.Message);
                        return;
                    }
                    if (btch.Contains("CTRL"))
                    {
                        repo = txtbx2 + "\\" + btch;
                    }
                    else
                    {
                        repo = txtbx2 + "\\Batch_" + btch;
                    }
                    try
                    {
                        if (Directory.Exists(repo) == false)
                        {
                            Directory.CreateDirectory(repo);
                        }

                    }
                    catch (Exception es)
                    {
                        MessageBox.Show(es.Message);
                        return;
                    }

                    DirectoryInfo inner = new DirectoryInfo(hardp);
                    DirectoryInfo[] mission = inner.GetDirectories("LV*");
                    DirectoryInfo inner1 = new DirectoryInfo(txtbx1);
                    FileInfo[] top = inner1.GetFiles("*.log");
                    foreach (FileInfo hov1 in top)
                    {
                        string end1 = repo + "\\" + hov1.Name;
                        int si1 = 2; string asa1 = end1;
                        string bases = hov1.Name.Remove(hov1.Name.Length - 4);
                        string exts = hov1.Name.Substring(hov1.Name.Length - 4);
                        while (File.Exists(asa1))
                        {
                            asa1 = repo + "\\" + bases + " (" + si1 + ")" + exts;
                            si1 += 1;
                        }
                        end1 = asa1;
                        try
                        {
                            File.Copy(hov1.FullName, end1);
                        }
                        catch (Exception ed)
                        {
                            MessageBox.Show(ed.Message);
                        }
                    }
                    //stats.Text = string.Empty;
                    //stats.Update();
                    //stats.Text = "Copying files (txt,log)...";
                    //stats.Update();
                    foreach (DirectoryInfo di in mission)
                    {
                        FileInfo[] inn = di.GetFiles("*.txt", SearchOption.AllDirectories);

                        foreach (FileInfo hov in inn)
                        {
                            string end = repo + "\\" + hov.Name;
                            int si = 2; string asa = end;
                            string bases = hov.Name.Remove(hov.Name.Length - 4);
                            string exts = hov.Name.Substring(hov.Name.Length - 4);
                            while (File.Exists(asa))
                            {
                                asa = repo + "\\" + bases + " (" + si + ")" + exts;
                                si += 1;
                            }
                            end = asa;
                            try
                            {
                                File.Copy(hov.FullName, end);
                            }
                            catch (Exception ed)
                            {
                                MessageBox.Show(ed.Message);
                            }
                        }
                        FileInfo[] innd = di.GetFiles("*.log", SearchOption.AllDirectories);

                        foreach (FileInfo hov in innd)
                        {
                            try
                            {
                                string end = repo + "\\" + hov.Name;
                                int si = 2; string asa = end;
                                string bases = hov.Name.Remove(hov.Name.Length - 4);
                                string exts = hov.Name.Substring(hov.Name.Length - 4);
                                while (File.Exists(asa))
                                {
                                    asa = repo + "\\" + bases + " (" + si + ")" + exts;
                                    si += 1;
                                }
                                end = asa;
                                try
                                {
                                    File.Copy(hov.FullName, end);
                                }
                                catch(Exception ed)
                                {
                                    MessageBox.Show(ed.Message);
                                }
                            }
                            catch (Exception me)
                            {
                                MessageBox.Show(me.Message);
                                //return;
                            }

                        }


                    }

                }
                else
                {
                    MessageBox.Show("Could not find path");
                    return;
                }
            }

            var con = MessageBox.Show("Do you want to delete processed PCS files and data folders?", "Delete", MessageBoxButtons.YesNo);

                if (con == DialogResult.Yes)
            {
                if (db.Count() > 0)
                {
                    for (int i = 0; i < db.Count; i++)
                    {
                        if (Directory.Exists(sa.txtBxServerFolder.Text))
                        {
                            string deletep = sa.txtBxServerFolder.Text + "\\" + db[i] + "\\" + ar[i] + "\\" + bat[i] + "\\PCS";
                            if (Directory.Exists(deletep))
                            {
                                try
                                {
                                    Directory.Delete(deletep, true);
                                   // stats.Text = "Deleted PCS Folder ";
                                   // stats.Update();
                                }
                                catch (Exception eq)
                                {
                                    MessageBox.Show(eq.Message);
                                    //return;
                                }
                            }
                            else
                            {
                                MessageBox.Show(string.Format("Folder Path not found: {0}", deletep));
                                //return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Data folder does not exist - please fix before trying again");
                            return;
                        }

                        if (bat[i].Contains("CTRL"))
                        {
                            string delp = txtbx1 + @"\Data\" + bat[i];
                            if (Directory.Exists(delp))
                            {
                                try
                                {
                                    Directory.Delete(delp, true);
                                    //stats.Text = "Deleted Data Folder ";
                                    //stats.Update();

                                }
                                catch (Exception eq)
                                {
                                    MessageBox.Show(eq.Message);
                                    //return;
                                }
                            }
                            else
                            {
                                MessageBox.Show(string.Format("Folder Path not found: {0}", delp));
                                //return;
                            }
                        }
                        else
                        {
                            string batch = "Batch_" + bat[i];
                            string delp = txtbx1 + @"\Data\" + batch;
                            if (Directory.Exists(delp))
                            {
                                try
                                {
                                    Directory.Delete(delp, true);
                                    //stats.Text = "Deleted Data Folder ";
                                    //stats.Update();
                                }
                                catch (Exception eq)
                                {
                                    MessageBox.Show(eq.Message);
                                    //return;
                                }
                            }
                            else
                            {
                                MessageBox.Show(string.Format("Folder Path not found: {0}", delp));
                                //return;
                            }
                        }

                        string projpath = txtbx1 + "\\" + db[i] + "_" + bat[i];
                        if (Directory.Exists(projpath))
                        {
                            try
                            {
                                Directory.Delete(projpath, true);
                                //stats.Text = "Deleted Data Folder ";
                                //stats.Update();
                            }
                            catch (Exception eq)
                            {
                                MessageBox.Show(eq.Message);
                                //return;
                            }
                        }
                        else
                        {
                            MessageBox.Show(string.Format("Folder Path not found: {0}", projpath));
                            //return;
                        }
                    }
                }
                else
                {
                    string okay = txtbx1;
                    string[] words = okay.Split('\\');
                    int cnt = words.Length;
                    string proj = words[cnt - 2];
                    string[] text = words[cnt - 1].Split('_');
                    int n = text.Length;
                    string btch = text[n - 1];
                    string fld = sa.txtBxServerFolder.Text;
                    if (Directory.Exists(fld))
                    {

                        DirectoryInfo di = new DirectoryInfo(fld);
                        DirectoryInfo[] dirs = di.GetDirectories(btch,SearchOption.AllDirectories);
                        if (dirs.Length != 0)
                        {
                            if(dirs[0].Name == btch)
                            {
                                string fullpath = dirs[0].FullName + @"\PCS";
                                if (Directory.Exists(fullpath))
                                {
                                    try
                                    {
                                        Directory.Delete(fullpath, true);
                                        //stats.Text = "Deleted PCS Folder ";
                                        //stats.Update();
                                    }
                                    catch (Exception eq)
                                    {
                                        MessageBox.Show(eq.Message);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(string.Format("Could not find directory: {0}.", fullpath));
                                    //return;
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show(string.Format("No directories matching batch:{0} were found.", btch));
                            return;
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Could not find: {0}", fld));
                        return;
                    }

                    if (btch.Contains("CTRL"))
                    {
                        string main = sa.txtBxLocalFolder.Text + @"\Data\" + btch;
                        if (Directory.Exists(main))
                        {
                            try
                            {
                                Directory.Delete(main, true);
                               // stats.Text = "Deleted Data Folder ";
                                //stats.Update();

                            }
                            catch (Exception eq)
                            {
                                MessageBox.Show(eq.Message);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show(string.Format("Folder Path not found: {0}.", main));
                            return;
                        }


                    }
                    else
                    {
                        string main = sa.txtBxLocalFolder.Text + @"\Data\Batch_" + btch;
                        if (Directory.Exists(main))
                        {
                            try
                            {
                                Directory.Delete(main, true);
                                //stats.Text = "Deleted Data Folder ";
                                //stats.Update();

                            }
                            catch (Exception eq)
                            {
                                MessageBox.Show(eq.Message);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show(string.Format("Folder Path not found: {0}.", main));
                            return;
                        }
                    }
                    string projpath = okay;
                    if (Directory.Exists(projpath))
                    {
                        try
                        {
                            Directory.Delete(projpath, true);
                            //stats.Text = "Deleted Data Folder ";
                           // stats.Update();
                        }
                        catch (Exception eq)
                        {
                            MessageBox.Show(eq.Message);
                            //return;
                        }
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Folder Path not found: {0}", projpath));
                        //return;
                    }
                }
                
            }
            else
            {
                //stats.Text = "Did not delete.";
               // stats.Update();
                return;
            }
            //stats.Text = "Done";
            // stats.Update();
            sa.lblStatus.Text = "Done";
            sa.Update();
            return;
        }

    }
}

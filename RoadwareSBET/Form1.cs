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
    public partial class SBETQC : Form
    {
        private RoadwareSBET.Domain.frmSBETDialog og = new RoadwareSBET.Domain.frmSBETDialog();
        public SBETQC(RoadwareSBET.Domain.frmSBETDialog frm)
        {
            //InitializeComponent();
            //stat.Text = "Enter Posdata path";
            //stat.Update();
            og = frm;
            a();
            //textBox1.Text = og.txtBxLocalFolder.Text;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
        }

        private void a()
        { 
            string aux = og.txtBxLocalFolder.Text; //textBox1.Text;
            string path = og.txtBxLocalFolder.Text; //textBox1.Text;
            //bool flg = false;
            //stat.Text = "Checking if path is valid";
            //stat.Update();
            List<string> proj = new List<string>();
            List<string> bat = new List<string>();

            DataRow[] fltr = new DataRow[0];
            try
            {
                fltr = og.batchDt.Select("[Process] = True", "[Backup Tape Location], [Batch Name], [Video Location], [Database Name]");
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
                    int success = 0;
                    string batch = i["Batch Name"].ToString();
                    string projpath = i["Video Location"].ToString();
                    string[] arra = projpath.Split('\\');
                    string proj1 = arra[arra.Length - 1];
                    //string proj1 = projpath.Remove(0, 28);
                    string db = i["Database Name"].ToString();
                    string batchP = i["Backup Tape Location"].ToString();


                    path = aux + "\\" + db + "_" + batch;
                    if (Directory.Exists(path) == false)
                    {
                        MessageBox.Show("Could not find path - Please enter a valid path: "+path);
                        success += 1;
                        
                    }
                    /*else
                    {
                        if (File.Exists(@"\\video-01\Operations\SBETs\Apps\SbetQC2\SbetQC.exe"))
                        {
                            //C:\NY19_8512019\NY19_8512019_013.posbat
                            try
                            {
                                TestStack.White.Application app = TestStack.White.Application.Launch(@"\\video-01\Operations\SBETs\Apps\SbetQC2\SbetQC.exe");
                                //TestStack.White.UIItems.WindowItems.Window window = app.GetWindow("Sbet QC July 18 2016",TestStack.White.Factory.InitializeOption.NoCache);
                                stat.Text = "Running Sbet QC...";
                                stat.Update();

                                List<TestStack.White.UIItems.WindowItems.Window> window = app.GetWindows();
                                //MessageBox.Show("here: " + window[0].Title);
                                TestStack.White.UIItems.WindowItems.Window win = window[0];

                                TestStack.White.UIItems.Finders.SearchCriteria search = TestStack.White.UIItems.Finders.SearchCriteria.ByClassName("WindowsForms10.EDIT.app.0.141b42a_r9_ad1");
                                TestStack.White.UIItems.TextBox text = (TestStack.White.UIItems.TextBox)win.Get(search);
                                text.Text = path;

                                TestStack.White.UIItems.Finders.SearchCriteria searchd = TestStack.White.UIItems.Finders.SearchCriteria.ByText("Please select a .posbat file that you would like to check");
                                TestStack.White.UIItems.TextBox lbl = (TestStack.White.UIItems.TextBox)win.Get(searchd);

                                TestStack.White.UIItems.Finders.SearchCriteria search2 = TestStack.White.UIItems.Finders.SearchCriteria.ByText("Set Root Directory");
                                TestStack.White.UIItems.Button b = (TestStack.White.UIItems.Button)win.Get(search2);
                                b.Click();


                                string a = lbl.ToString();

                                while (a.Contains("All done") == false)
                                {
                                    stat.Text = "Processing...";
                                    stat.Update();
                                    a = lbl.ToString();
                                }
                                MessageBox.Show("Results: " + lbl.Name);
                                stat.Text = "Report Ready";
                                stat.Update();
                            }
                            catch (Exception a)
                            {

                                MessageBox.Show(a.Message);
                                success += 1;
                                return;
                            }



                        }
                        else
                        {
                            MessageBox.Show("Could not find SbetQC.exe");
                            success += 1;
                            return;
                        }
                    }*/
                    if (success == 0)
                    {
                        var con = MessageBox.Show("Do you want to set up Data folder?", "Continue", MessageBoxButtons.YesNo);
                        if (con == DialogResult.Yes)
                        {
                            //stat.Text = "Setting up Data folder";
                            //stat.Update();
                            DirectoryInfo main = new DirectoryInfo(path);
                            DirectoryInfo dirtP = main.Parent;
                            string dirt = dirtP.FullName;
                            string dir = dirt + "\\" + "Data";
                            if (Directory.Exists(dir) == false)
                            {
                                Directory.CreateDirectory(dir);
                            }
                            string filename = main.Name;
                            string fn = filename;//.Remove(filename.Length - 7, 7);

                            string btch = batch;
                            string upd;
                            if (btch.Contains("CTRL"))
                            {
                                upd = btch;
                            }
                            else
                            {
                                upd = "Batch_" + btch;
                            }


                            dir = dir + "\\" + upd;
                            if (Directory.Exists(dir) == false)
                            {
                                Directory.CreateDirectory(dir);
                            }
                            string fnd = dirt + "\\" + fn;
                            if (Directory.Exists(fnd))
                            {
                                //stat.Text = "Grabbing all PDFs";
                                //stat.Update();
                                FileInfo[] innd = main.GetFiles("*.pdf", SearchOption.AllDirectories);
                                if (innd.Length == 0)
                                {
                                    //stat.Text = "Could not find any PDFs";
                                    //stat.Update();
                                }
                                else
                                {
                                    string finp = dirt + @"\Reports\" + upd;
                                    Pdf_Merge(innd, finp, upd);
                                }
                                Console.WriteLine(dir);
                                DirectoryInfo inner = new DirectoryInfo(fnd);
                                DirectoryInfo[] mission = inner.GetDirectories("LV*");
                                foreach (DirectoryInfo di in mission)
                                {
                                    
                                    DirectoryInfo[] inn = di.GetDirectories("lv*");
                                    if (inn.Count() != 0)
                                    {
                                        string cpy = inn[0].FullName;
                                        if (Directory.Exists(cpy))
                                        {
                                            DirectoryInfo abe = new DirectoryInfo(cpy);
                                            /*FileInfo[] fi = abe.GetFiles("sbet*",SearchOption.AllDirectories);
                                            if (fi.Count() != 0)
                                            {
                                                string end = dir + "\\" + fi[0].Name;
                                                int si = 2; string asa = end;
                                                string bases = fi[0].Name.Remove(fi[0].Name.Length - 4);
                                                string exts = fi[0].Name.Substring(fi[0].Name.Length - 4);
                                                while (File.Exists(asa))
                                                {
                                                    asa = dir + "\\" + bases + " (" + si + ")" + exts;
                                                    si += 1;
                                                }
                                                end = asa;
                                                try
                                                {
                                                    File.Copy(fi[0].FullName, end);
                                                }
                                                catch (Exception ase)
                                                {
                                                    MessageBox.Show(ase.Message);
                                                    return;
                                                }
                                            }*/
                                            FileInfo[] fid = abe.GetFiles("smrmsg*",SearchOption.AllDirectories);
                                            if (fid.Count() != 0)
                                            {
                                                string endd = dir + "\\" + fid[0].Name;
                                                int six = 2; string ax = endd;
                                                string basesx = fid[0].Name.Remove(fid[0].Name.Length - 4);
                                                string extsx = fid[0].Name.Substring(fid[0].Name.Length - 4);
                                                while (File.Exists(ax))
                                                {
                                                    ax = dir + "\\" + basesx + " (" + six + ")" + extsx;
                                                    six += 1;
                                                }
                                                endd = ax;
                                                try
                                                {
                                                    File.Copy(fid[0].FullName, endd);
                                                }
                                                catch (Exception ased)
                                                {
                                                    MessageBox.Show(ased.Message);
                                                    return;
                                                }
                                            }
                                        }

                                        else
                                        {
                                            MessageBox.Show("File does not exist in: " + cpy);
                                            //return;
                                        }

                                        string cpyd = inn[0].FullName;
                                        if (Directory.Exists(cpyd))
                                        {
                                            DirectoryInfo abe = new DirectoryInfo(cpyd);
                                            FileInfo[] fib = abe.GetFiles("vrms*", SearchOption.AllDirectories);
                                            if (fib.Count() != 0)
                                            {
                                                string end = dir + "\\" + fib[0].Name;
                                                int si = 2; string a = end;
                                                string bases = fib[0].Name.Remove(fib[0].Name.Length - 4);
                                                string exts = fib[0].Name.Substring(fib[0].Name.Length - 4);
                                                while (File.Exists(a))
                                                {
                                                    a = dir + "\\" + bases + " (" + si + ")" + exts;
                                                    si += 1;
                                                }
                                                end = a;
                                                try
                                                {
                                                    File.Copy(fib[0].FullName, end);
                                                }
                                                catch (Exception ase)
                                                {
                                                    MessageBox.Show(ase.Message);
                                                    return;
                                                }
                                            }
                                            DirectoryInfo abe2 = new DirectoryInfo(cpyd);
                                            FileInfo[] fib2 = abe2.GetFiles("export_*.out", SearchOption.AllDirectories);
                                            if (fib2.Count() != 0)
                                            {
                                                string end = dir + "\\" + fib2[0].Name;
                                                int si = 2; string a = end;
                                                string bases = fib2[0].Name.Remove(fib2[0].Name.Length - 4);
                                                string[] arr = bases.Split('_');
                                                string rename = "sbet_";
                                                bases = rename + arr[1];
                                                string exts = fib2[0].Name.Substring(fib2[0].Name.Length - 4);
                                                a = dir + "\\" + bases + exts;
                                                while (File.Exists(a))
                                                {
                                                    a = dir + "\\" + bases + " (" + si + ")" + exts;
                                                    si += 1;
                                                }
                                                end = a;
                                                try
                                                {
                                                    File.Copy(fib2[0].FullName, end);
                                                }
                                                catch (Exception ase)
                                                {
                                                    MessageBox.Show(ase.Message);
                                                    return;
                                                }
                                            }


                                        }
                                        else
                                        {
                                            MessageBox.Show("File does not exist in: " + cpyd);
                                            //return;
                                        }

                                    }
                                }
                                //stat.Text = "Done making Data folder";
                                //stat.Update();
                            }
                            else
                            {
                                MessageBox.Show("File: " + fnd + " does not exist");
                                //return;
                            }
                        }

                        var res = MessageBox.Show("Do you want to upload batch to AWS?", "Continue", MessageBoxButtons.YesNo);
                        if (res == DialogResult.Yes)
                        {
                            try
                            {
                                //TestStack.White.Application app = TestStack.White.Application.Launch(@"\\video-01\Operations\SBETs\Apps\SbetUploading\SbetUploading.exe");
                                string backup = @"\\video-01\Operations\SBETs\Apps\SbetUploading\SbetUploading.exe";
                                ProcessStartInfo myupl = new ProcessStartInfo(backup);
                                myupl.RedirectStandardOutput = true;
                                myupl.UseShellExecute = false;

                                //stat.Text = "Uploading...";
                                //stat.Update();

                                Process upl = new Process();
                                upl.StartInfo = myupl;
                                upl.Start();
                                upl.WaitForExit();
                                upl.Close();
                                
                                //stat.Text = "Done";
                                //stat.Update();
                            }
                            catch (Exception de)
                            {
                                MessageBox.Show(de.Message);
                            }


                        }
                        else
                        {
                            //stat.Text = "Done";
                            //stat.Update();
                            //return;
                        }

                    }
                }
            }
            else
            {
                int success = 0;
                if (Directory.Exists(path) == false)
                {
                    MessageBox.Show("Could not find path - Please enter a valid path");
                    success += 1;
                    return;
                }
                /*else
                {
                    if (File.Exists(@"\\video-01\Operations\SBETs\Apps\SbetQC2\SbetQC.exe"))
                    {
                        //C:\NY19_8512019\NY19_8512019_013.posbat
                        try
                        {
                            TestStack.White.Application app = TestStack.White.Application.Launch(@"\\video-01\Operations\SBETs\Apps\SbetQC2\SbetQC.exe");
                            //TestStack.White.UIItems.WindowItems.Window window = app.GetWindow("Sbet QC July 18 2016",TestStack.White.Factory.InitializeOption.NoCache);
                            stat.Text = "Running Sbet QC...";
                            stat.Update();

                            List<TestStack.White.UIItems.WindowItems.Window> window = app.GetWindows();
                            //MessageBox.Show("here: " + window[0].Title);
                            TestStack.White.UIItems.WindowItems.Window win = window[0];

                            TestStack.White.UIItems.Finders.SearchCriteria search = TestStack.White.UIItems.Finders.SearchCriteria.ByControlType(System.Windows.Automation.ControlType.Edit);
                            TestStack.White.UIItems.TextBox text = (TestStack.White.UIItems.TextBox)win.Get(search);
                            text.Text = path;

                            TestStack.White.UIItems.Finders.SearchCriteria searchd = TestStack.White.UIItems.Finders.SearchCriteria.ByText("Please select a .posbat file that you would like to check");
                            TestStack.White.UIItems.TextBox lbl = (TestStack.White.UIItems.TextBox)win.Get(searchd);

                            TestStack.White.UIItems.Finders.SearchCriteria search2 = TestStack.White.UIItems.Finders.SearchCriteria.ByText("Set Root Directory");
                            TestStack.White.UIItems.Button b = (TestStack.White.UIItems.Button)win.Get(search2);
                            b.Click();


                            string a = lbl.ToString();

                            while (a.Contains("All done") == false)
                            {
                                stat.Text = "Processing...";
                                stat.Update();
                                a = lbl.ToString();
                            }
                            MessageBox.Show("Results: " + lbl.Name);
                            stat.Text = "Report Ready";
                            stat.Update();
                        }
                        catch (Exception a)
                        {

                            MessageBox.Show(a.Message);
                            success += 1;
                            return;
                        }



                    }
                    else
                    {
                        MessageBox.Show("Could not find SbetQC.exe");
                        success += 1;
                        return;
                    }
                }*/
                if (success == 0)
                {
                    var con = MessageBox.Show("Do you want to set up Data folder?", "Continue", MessageBoxButtons.YesNo);
                    if (con == DialogResult.Yes)
                    {
                        //stat.Text = "Setting up Data folder";
                       // stat.Update();
                        DirectoryInfo main = new DirectoryInfo(path);
                        DirectoryInfo dirtP = main.Parent;
                        string dirt = dirtP.FullName;
                        string dir = dirt + "\\" + "Data";
                        if (Directory.Exists(dir) == false)
                        {
                            Directory.CreateDirectory(dir);
                        }

                        string filename = main.Name;
                        string fn = filename;//.Remove(filename.Length - 7, 7);

                        int maxn = fn.Length;
                        string okay = og.txtBxLocalFolder.Text;//textBox1.Text;
                        string[] words = okay.Split('\\');
                        int cnt = words.Length;
                        string projd= words[cnt - 2];
                        string[] text = words[cnt - 1].Split('_');
                        int n = text.Length;
                        string btch = text[n - 1];
                        string upd;
                        if (btch.Contains("CTRL"))
                        {
                            upd = btch;
                        }
                        else
                        {
                            upd = "Batch_" + btch;
                        }
                        

                        dir = dir + "\\" + upd;
                        if (Directory.Exists(dir) == false)
                        {
                            Directory.CreateDirectory(dir);
                        }
                        string fnd = dirt + "\\" + fn;
                        if (Directory.Exists(fnd))
                        {
                            //stat.Text = "Grabbing all PDFs";
                            //stat.Update();
                            FileInfo[] innd = main.GetFiles("*.pdf", SearchOption.AllDirectories);
                            if (innd.Length == 0)
                            {
                                //stat.Text = "Could not find any PDFs";
                                //stat.Update();
                            }
                            else
                            {
                                string finp = dirt + @"\Reports\" + upd;
                                Pdf_Merge(innd, finp, upd);
                            }
                            DirectoryInfo inner = new DirectoryInfo(fnd);
                            DirectoryInfo[] mission = inner.GetDirectories("LV*");
                            foreach (DirectoryInfo di in mission)
                            {
                                
                                DirectoryInfo[] inn = di.GetDirectories("lv*");
                                if (inn.Count() != 0)
                                {
                                    string cpy = inn[0].FullName + "//Proc";
                                    if (Directory.Exists(cpy))
                                    {
                                        DirectoryInfo abe = new DirectoryInfo(cpy);
                                        /*FileInfo[] fi = abe.GetFiles("sbet*");
                                        if (fi.Count() != 0)
                                        {
                                            string end = dir + "\\" + fi[0].Name;
                                            int si = 2; string asa = end;
                                            string bases = fi[0].Name.Remove(fi[0].Name.Length - 4);
                                            string exts = fi[0].Name.Substring(fi[0].Name.Length - 4);
                                            while (File.Exists(asa))
                                            {
                                                asa = dir + "\\" + bases + " (" + si + ")" + exts;
                                                si += 1;
                                            }
                                            end = asa;
                                            try
                                            {
                                                File.Copy(fi[0].FullName, end);
                                            }
                                            catch (Exception ase)
                                            {
                                                MessageBox.Show(ase.Message);
                                                return;
                                            }
                                        }*/
                                        FileInfo[] fid = abe.GetFiles("smrmsg*");
                                        if (fid.Count() != 0)
                                        {
                                            string endd = dir + "\\" + fid[0].Name;
                                            int six = 2; string ax = endd;
                                            string basesx = fid[0].Name.Remove(fid[0].Name.Length - 4);
                                            string extsx = fid[0].Name.Substring(fid[0].Name.Length - 4);
                                            while (File.Exists(ax))
                                            {
                                                ax = dir + "\\" + basesx + " (" + six + ")" + extsx;
                                                six += 1;
                                            }
                                            endd = ax;
                                            try
                                            {
                                                File.Copy(fid[0].FullName, endd);
                                            }
                                            catch (Exception ased)
                                            {
                                                MessageBox.Show(ased.Message);
                                                return;
                                            }
                                        }
                                    }

                                    else
                                    {
                                        MessageBox.Show("File does not exist in: " + cpy);
                                        //return;
                                    }

                                    string cpyd = inn[0].FullName + "//Extract";
                                    if (Directory.Exists(cpyd))
                                    {
                                        DirectoryInfo abe = new DirectoryInfo(cpyd);
                                        FileInfo[] fib = abe.GetFiles("vrms*");
                                        if (fib.Count() != 0)
                                        {
                                            string end = dir + "\\" + fib[0].Name;
                                            int si = 2; string a = end;
                                            string bases = fib[0].Name.Remove(fib[0].Name.Length - 4);
                                            string exts = fib[0].Name.Substring(fib[0].Name.Length - 4);
                                            while (File.Exists(a))
                                            {
                                                a = dir + "\\" + bases + " (" + si + ")" + exts;
                                                si += 1;
                                            }
                                            end = a;
                                            try
                                            {
                                                File.Copy(fib[0].FullName, end);
                                            }
                                            catch (Exception ase)
                                            {
                                                MessageBox.Show(ase.Message);
                                                return;
                                            }
                                        }
                                    }
                                    string cpyd2 = inn[0].FullName;
                                    DirectoryInfo abe2 = new DirectoryInfo(cpyd2);
                                    FileInfo[] fib2 = abe2.GetFiles("export_*.out", SearchOption.AllDirectories);
                                    if (fib2.Count() != 0)
                                    {
                                        string end = dir + "\\" + fib2[0].Name;
                                        int si = 2; string a = end;
                                        string bases = fib2[0].Name.Remove(fib2[0].Name.Length - 4);
                                        string exts = fib2[0].Name.Substring(fib2[0].Name.Length - 4);
                                        string[] arr = bases.Split('_');
                                        string rename = "sbet_";
                                        bases = rename + arr[1];
                                        a = dir + "\\" + bases + exts;
                                        while (File.Exists(a))
                                        {
                                            a = dir + "\\" + bases + " (" + si + ")" + exts;
                                            si += 1;
                                        }
                                        end = a;
                                        try
                                        {
                                            File.Copy(fib2[0].FullName, end);
                                            
                                        }
                                        catch (Exception ase)
                                        {
                                            MessageBox.Show(ase.Message);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("File does not exist in: " + cpyd2 );
                                        //return;
                                    }

                                }
                            }
                            //stat.Text = "Done making Data folder";
                            //stat.Update();
                        }
                        else
                        {
                            MessageBox.Show("File: " + fnd + " does not exist");
                            return;
                        }
                    }
                    /*
                    var res = MessageBox.Show("Do you want to upload batch to AWS?", "Continue", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        try
                        {
                            //TestStack.White.Application app = TestStack.White.Application.Launch(@"\\video-01\Operations\SBETs\Apps\SbetUploading\SbetUploading.exe");
                            string backup = @"\\video-01\Operations\SBETs\Apps\SbetUploading\SbetUploading.exe";
                            ProcessStartInfo myupl = new ProcessStartInfo(backup);
                            myupl.RedirectStandardOutput = true;
                            myupl.UseShellExecute = false;

                            //stat.Text = "Uploading...";
                            //stat.Update();

                            Process upl = new Process();
                            upl.StartInfo = myupl;
                            upl.Start();
                            upl.WaitForExit();
                            upl.Close();

                            //stat.Text = "Done";
                           // stat.Update();
                        }
                        catch (Exception de)
                        {
                            MessageBox.Show(de.Message);
                        }


                    }
                    else
                    {
                        //stat.Text = "Done";
                        //stat.Update();
                        return;
                    }
                    */
                }
            }
        }

        private void Pdf_Merge(FileInfo[] all, string path, string batch)
        {

            try
            {
                PdfSharp.Pdf.PdfDocument output = new PdfSharp.Pdf.PdfDocument();
                foreach (var a in all)
                {
                    PdfSharp.Pdf.PdfDocument input = PdfSharp.Pdf.IO.PdfReader.Open(a.FullName, PdfSharp.Pdf.IO.PdfDocumentOpenMode.Import);

                    int cnt = input.PageCount;
                    for (int i = 0; i < cnt; i++)
                    {
                        output.AddPage(input.Pages[i]);
                    }
                }
                if(Directory.Exists(path) != true)
                {
                    Directory.CreateDirectory(path);
                }
                string fin = path + "\\" + batch + ".pdf";
                output.Save(fin);

                SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();

                f.OpenPdf(fin);
                if(f.PageCount > 0)
                {
                    string fins = path + "\\" + batch + ".rtf";
                    f.WordOptions.Format = SautinSoft.PdfFocus.CWordOptions.eWordDocument.Rtf;
                    f.ToWord(fins);
                }

                //stat.Text = "PDF combined succesfully into RTF";
                //stat.Update();
                return;
            }
            catch (Exception asd)
            {
                MessageBox.Show(asd.Message);
                return;
            }
            
        }
    }
}

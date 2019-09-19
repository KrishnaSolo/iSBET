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
using System.IO.MemoryMappedFiles;
using GMap.NET;
using System.Threading;



namespace RoadwareSBET
{
    public partial class PinPoint : Form
    {
        public string batch;
        public string filename;
        public string projectcode;
        public bool next;
        public double mx;
        public string sbet;// = @"\\video-01\Operations\SBETs\Apps\iSBET-r10.0\SBETS\sbet_lv201907060649.out";
        public string rt;// = @"\\video-01\Operations\SBETs\Apps\iSBET-r10.0\SBETS\vnav_lv201907060649.out";
        public string load = "http://maps.google.com/maps?q=&layer=c&cbll=";
        public string find;
        ToolTip txt = new ToolTip();
        List<SBETstruct> list = new List<SBETstruct>();

        List<double> data = new List<double>();
        List<double> x = new List<double>();
        List<double> y = new List<double>();
        List<double> alt = new List<double>();

        List<PointLatLng> pts1 = new List<PointLatLng>();
        List<PointLatLng> pts2 = new List<PointLatLng>();
        List<PointLatLng> err = new List<PointLatLng>();
        int onetime = 0;
        public double lat1; public double lon1;

        public PinPoint(string fn, string btch,string prj)
        {
            InitializeComponent();
           // this.webBrowser1.Navigate("http://maps.google.com/maps?");
            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gMapControl1.BringToFront();
            gMapControl1.Position = new GMap.NET.PointLatLng(48.8589507, 2.2775175);
            gMapControl1.ShowCenter = false;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.Zoom = 6;
            //webBrowser1.Enabled = false;
            button1.Visible = false;
            next = false;
            try
            {
                string nam = fn + "\\Data\\" + btch;
                DirectoryInfo dat = new DirectoryInfo(nam);
                FileInfo[] file = dat.GetFiles("sbet*");
                List<string> prename = new List<string>();
                foreach (var cnt in file)
                {
                    string pos1 = cnt.Name.Substring(5);
                    string pos2 = pos1.Remove(pos1.Length - 4, 4);
                    comboBox2.Items.Add(pos2);
                }
            }
            catch
            {
                comboBox2.Enabled = false;
                MessageBox.Show("Set up Data folder using QC button first");
                button2.Enabled = false;
            }
            batch = btch;
            filename = fn;
            projectcode = prj;
            checkBox1.Enabled = false;
            //Execute();
        }

        public double Deg(double rad)
        {
            double degree = rad * (180 / Math.PI);
            return degree;
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            mx = new double();
            comboBox2.Enabled = false;
            list.Clear();
            data.Clear();
            pts1.Clear();
            pts2.Clear();
            x.Clear();
            y.Clear();
            alt.Clear();
            err.Clear();
            label9.Text = "Starting...";
            label9.Update();
            backgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker help = sender as BackgroundWorker;
            //int arg = (int)e.Argument;
            logic(help, e);
        }


        private void logic(BackgroundWorker worker, DoWorkEventArgs e)
        {
            Console.WriteLine("okay here2");
            IDictionary<string, SBETstruct> map = new Dictionary<string, SBETstruct>();
            IDictionary<string, SBETstruct> mapR = new Dictionary<string, SBETstruct>();
            Console.WriteLine("okay here3");

            Thread t1 = new Thread(() =>
            {
                Console.WriteLine("TimeStamp start: " + DateTime.Now);
                var arr = File.ReadAllBytes(sbet);
                for (int ind = 0; ind < arr.Length - 135; ind = ind + 136)
                {
                    SBETstruct rec = new SBETstruct();
                    rec.time = BitConverter.ToDouble(arr, ind);
                    ind += 8;

                    rec.lat = BitConverter.ToDouble(arr, ind);
                    ind += 8;

                    rec.lon = BitConverter.ToDouble(arr, ind);
                    pts1.Add(new PointLatLng(Deg(rec.lat), Deg(rec.lon)));
                    ind += 8;

                    ind += (8 * 14);

                    map.Add(rec.time.ToString(), rec);
                }
                Console.WriteLine("TimeStamp end: " + DateTime.Now);
            });

            Thread t2 = new Thread(() =>
            {
                Console.WriteLine("TimeStamp startR: " + DateTime.Now);
                var arr = File.ReadAllBytes(rt);
                for (int ind = 0; ind < arr.Length - 135; ind = ind + 136)
                {
                    SBETstruct rec = new SBETstruct();
                    rec.time = BitConverter.ToDouble(arr, ind);
                    ind += 8;

                    rec.lat = BitConverter.ToDouble(arr, ind);
                    ind += 8;

                    rec.lon = BitConverter.ToDouble(arr, ind);
                    pts2.Add(new PointLatLng(Deg(rec.lat), Deg(rec.lon)));
                    ind += 8;

                    rec.alt = BitConverter.ToDouble(arr, ind);
                    alt.Add(rec.alt);
                    ind += 8;

                    ind += (8 * 13);

                    mapR.Add(rec.time.ToString(), rec);
                }
                Console.WriteLine("TimeStamp endR: " + DateTime.Now);
            });
            //\\video-01\Operations\SBETs\AK19_85132\Data\CTRL_ARAN58_Entry\sbet_LV201907031818.out

            /*using (BinaryReader b = new BinaryReader(File.Open(sbet, FileMode.Open)))
            {
                int pos = 0;
                var len = b.BaseStream.Length / sizeof(double);
                try
                {
                    for (var i = 0; i < len; i++)
                    {
                        SBETstruct rec = new SBETstruct();
                        rec.time = b.ReadDouble();

                        rec.lat = b.ReadDouble();

                        rec.lon = b.ReadDouble();

                        rec.alt = b.ReadDouble();

                        rec.xvel = b.ReadDouble();

                        rec.yvel = b.ReadDouble();

                        rec.zvel = b.ReadDouble();

                        rec.roll = b.ReadDouble();

                        rec.pitch = b.ReadDouble();

                        rec.heading = b.ReadDouble();

                        rec.wander = b.ReadDouble();

                        rec.xacc = b.ReadDouble();

                        rec.yacc = b.ReadDouble();

                        rec.zacc = b.ReadDouble();

                        rec.xang = b.ReadDouble();

                        rec.yang = b.ReadDouble();

                        rec.zang = b.ReadDouble();
                        map.Add(rec.time.ToString(), rec);

                    }
                }
                catch
                {
                    MessageBox.Show("Done Convert");
                }
            }*/
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
            double ad = 0; double b = 0;
            List<SBETstruct> strct = new List<SBETstruct>(0);
            // string epoch = textBox2.Text;
            foreach (var a in mapR)
            {
                string epoch = a.Key.ToString();
                double ep = Convert.ToDouble(epoch);
                //MessageBox.Show(epoch);
                // foreach(var a in map)
                //{
                //Console.WriteLine("KEY = {0}", a.Key);
                //}//\\video-01\Operations\SBETs\NY19_8512019\Data\Batch_020B\sbet_LV201906150841.out
                SBETstruct found = new SBETstruct();
                if (map.ContainsKey(epoch))
                {
                    //MessageBox.Show("Found it");
                }
                else
                {
                    //MessageBox.Show("Not found");
                }
                try
                {
                    found = map[epoch];
                    var real = mapR[epoch];

                    double d = FindDist(found.lat, found.lon, real.lat, real.lon);

                    //var kp = new LiveCharts.Defaults.ObservablePoint(ep, d);
                    //disp.Add(kp);
                    // data.Add(d);
                    x.Add(ep);
                    y.Add(d);
                    //Console.WriteLine(d);
                    if (Math.Abs(d) > 5.0)
                    {
                        if (d > mx)
                        {
                            mx = d;
                        }
                        data.Add(d);
                        strct.Add(found);
                        // ad = (found.lat);
                        //b = (found.lon);
                        err.Add(new PointLatLng(Deg(found.lat), Deg(found.lon)));

                    }
                    ad = (found.lat);
                    b = (found.lon);
                    list = strct;
                }
                catch (Exception es)
                {
                    // MessageBox.Show(es.Message);
                    //return;
                }
                // ad = (found.lat);
                //b = (found.lon);
            }
            ad = Deg(ad);
            b = Deg(b);
            string ps = load + ad + "," + b;
            find = ps;
            //ad = Deg(ad);
            //b = Deg(b);
            lat1 = ad;
            lon1 = b;


        }

        public double FindDist(double la1, double lo1, double la2, double lo2)
        {
            double dist = 0;
            int r = 6371000;
            double dLat = la2 - la1;
            double dLon = lo1 - lo2;
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(la1) * Math.Cos(la2) * (Math.Sin(dLon / 2) * Math.Sin(dLon / 2));
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            dist = r * c;
            return dist;
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                label9.Text = e.Error.Message;
                label9.Update();
            }
            else
            {
                label9.Text = "Done";
                label9.Update();
            }

            GMap.NET.WindowsForms.GMapOverlay routes = new GMap.NET.WindowsForms.GMapOverlay("routes");
            GMap.NET.WindowsForms.GMapOverlay routes2 = new GMap.NET.WindowsForms.GMapOverlay("routes2");
            GMap.NET.WindowsForms.GMapRoute route = new GMap.NET.WindowsForms.GMapRoute(pts1, "sbet");
            GMap.NET.WindowsForms.GMapRoute route2 = new GMap.NET.WindowsForms.GMapRoute(pts2, "vnav");
            route.Stroke = new Pen(Color.Red, 3);
            route2.Stroke = new Pen(Color.Blue, 3);
            routes.Routes.Add(route);
            routes2.Routes.Add(route2);
            gMapControl1.Overlays.Add(routes2);
            gMapControl1.Overlays.Add(routes);

            GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
            foreach (var p in err)
            {
                GMap.NET.WindowsForms.GMapMarker mark = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(p, GMap.NET.WindowsForms.Markers.GMarkerGoogleType.orange_dot);
                markers.Markers.Add(mark);
                mark.ToolTipText = "Coord:\nLat: " + lat1.ToString() + "\nLon: " + lon1.ToString();
                mark.ToolTip.Fill = Brushes.Black;
                mark.ToolTip.Foreground = Brushes.White;
                mark.ToolTip.Stroke = Pens.Black;
                mark.ToolTip.TextPadding = new Size(15, 20);
            }

            GMap.NET.WindowsForms.GMapMarker markS = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(pts1[0], GMap.NET.WindowsForms.Markers.GMarkerGoogleType.gray_small);
            markers.Markers.Add(markS);
            markS.ToolTipText = "Start";
            markS.ToolTip.Fill = Brushes.Black;
            markS.ToolTip.Foreground = Brushes.White;
            markS.ToolTip.Stroke = Pens.Black;
            markS.ToolTip.TextPadding = new Size(10, 15);

            GMap.NET.WindowsForms.GMapMarker markE = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(pts1[pts1.Count - 1], GMap.NET.WindowsForms.Markers.GMarkerGoogleType.gray_small);
            markers.Markers.Add(markE);
            markE.ToolTipText = "End";
            markE.ToolTip.Fill = Brushes.Black;
            markE.ToolTip.Foreground = Brushes.White;
            markE.ToolTip.Stroke = Pens.Black;
            markE.ToolTip.TextPadding = new Size(10, 15);

            GMap.NET.WindowsForms.GMapMarker markS2 = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(pts2[0], GMap.NET.WindowsForms.Markers.GMarkerGoogleType.gray_small);
            markers.Markers.Add(markS2);
            markS2.ToolTipText = "Start";
            markS2.ToolTip.Fill = Brushes.Black;
            markS2.ToolTip.Foreground = Brushes.White;
            markS2.ToolTip.Stroke = Pens.Black;
            markS2.ToolTip.TextPadding = new Size(10, 15);

            GMap.NET.WindowsForms.GMapMarker markE2 = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(pts2[pts2.Count - 1], GMap.NET.WindowsForms.Markers.GMarkerGoogleType.gray_small);
            markers.Markers.Add(markE2);
            markE2.ToolTipText = "End";
            markE2.ToolTip.Fill = Brushes.Black;
            markE2.ToolTip.Foreground = Brushes.White;
            markE2.ToolTip.Stroke = Pens.Black;
            markE2.ToolTip.TextPadding = new Size(10, 15);

            gMapControl1.Overlays.Add(markers);
            gMapControl1.ZoomAndCenterRoute(route);
            
            label2.Text = "Lat: " + lat1.ToString();
            label3.Text = "Lon: " + lon1.ToString();
            label7.Text = "Times over 5: " + err.Count();
            Max m = new Max();
            //Console.WriteLine("hehre");
            label4.Text = "Max Diff(m): " + mx;
            alt.Sort();
            //Console.WriteLine("hehr1e");
            label5.Text = "Max Elev(m): " + alt[alt.Count - 1];
            label6.Text = "Min Elev(m): " + alt[0];
            label2.Update();
            label3.Update();
            label4.Update();
            label5.Update();
            label6.Update();
            label7.Update();
            comboBox2.Enabled = true;
            checkBox1.Enabled = true;
            /*double lat1 = 0; double lon1 = 0; string ps = "";
            Console.WriteLine(list.Count.ToString());
            var res = list.Distinct(new SBETcomp());
            Console.WriteLine(res.Count().ToString());

            foreach (var found in res)
            {
                double lat = Deg(found.lat);
                double lon = Deg(found.lon);
                string p = load + lat + "," + lon;
                gMapControl1.Position = new GMap.NET.PointLatLng(lat, lon);
                GMap.NET.WindowsForms.GMapOverlay marks = new GMap.NET.WindowsForms.GMapOverlay("marks");
                GMap.NET.WindowsForms.GMapMarker mark = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(gMapControl1.Position, GMap.NET.WindowsForms.Markers.GMarkerGoogleType.red_dot);
                marks.Markers.Add(mark);
                gMapControl1.Overlays.Add(marks);
                lat1 = lat;
                lon1 = lon;
                ps = p;
                Console.WriteLine(lat1.ToString() + "," + lon1.ToString());
            }
            gMapControl1.ZoomAndCenterMarkers("marks");
            find = ps;
            label3.Text = "Lat: " + lat1.ToString();
            label4.Text = "Lon: " + lon1.ToString();*/
            return;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                chart1.BringToFront();
                if (onetime == 0)
                {
                    //chart1.BackColor = Color.DimGray;
                    chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;

                    for (int i = 0; i < x.Count(); i++)
                    {
                        chart1.Series["Difference(m)"].Points.AddXY(x[i], y[i]);
                    }
                    chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
                    chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = System.Drawing.Color.WhiteSmoke;

                    onetime++;
                }
            }
            else
            {
                gMapControl1.BringToFront();
            }
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
                GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            }
            else
            {
                gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
                GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            }
        }

        private void GMapControl1_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Text = "Lat: " + gMapControl1.Position.Lat.ToString();
            label3.Text = "Lon: " + gMapControl1.Position.Lng.ToString();
        }

        private void GMapControl1_MouseHover(object sender, EventArgs e)
        {
            label2.Text = "Lat: " + gMapControl1.Position.Lat.ToString();
            label3.Text = "Lon: " + gMapControl1.Position.Lng.ToString();
        }

        private void GMapControl1_OnMarkerDoubleClick(GMap.NET.WindowsForms.GMapMarker item, MouseEventArgs e)
        {
            find = load + item.Position.Lat.ToString() + "," + item.Position.Lng.ToString();
            gMapControl1.Enabled = false;
            webBrowser1.BringToFront();
            webBrowser1.Navigate(find);
           // chromiumWebBrowser1.Load(find);
            button1.Visible = true;
            richTextBox1.Text = "Predicted:\n" + "Under Bridge";
            richTextBox1.Update();
        }

        private void Button1_MouseClick(object sender, MouseEventArgs e)
        {
            gMapControl1.Enabled = true;
            gMapControl1.BringToFront();
            button1.Visible = false;
        }

        private void PinPoint_FormClosed(object sender, FormClosedEventArgs e)
        {
            next = true;
        }

        private void PinPoint_FormClosing(object sender, FormClosingEventArgs e)
        {
            next = true;
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nam = filename + "\\Data\\" + batch;
            DirectoryInfo dat = new DirectoryInfo(nam);
            string pattern = "sbet_" + comboBox2.SelectedItem.ToString() + "*";
            Console.WriteLine(pattern);
            try
            {
                FileInfo[] file = dat.GetFiles(pattern);
                sbet = file[0].FullName;
            }
            catch
            {
                MessageBox.Show("Could not find SBET file for " + comboBox2.SelectedItem.ToString());
                return;
            }
            Console.WriteLine("hree");
            DirectoryInfo search = new DirectoryInfo(projectcode);
            string pat = "vnav_" + comboBox2.SelectedItem.ToString().ToUpper() + "*";
            //Console.WriteLine(pat);
            try
            {
                FileInfo[] find = search.GetFiles(pat,SearchOption.AllDirectories);
                rt = find[0].FullName;
            }
            catch
            {
                MessageBox.Show("Could not find VNAV file for " + comboBox2.SelectedItem.ToString());
                return;
            }
            //Console.WriteLine("here2");
            label9.Text = "Selected: " + comboBox2.SelectedItem.ToString();
            label9.Update();
            return;
        }
    }

    public class SBETstruct
    {
        public double time;
        public double lat;
        public double lon;
        public double alt;
        public double xvel;
        public double yvel;
        public double zvel;
        public double roll;
        public double pitch;
        public double heading;
        public double wander;
        public double xacc;
        public double yacc;
        public double zacc;
        public double xang;
        public double yang;
        public double zang;

    }
    public class SBETcomp : IEqualityComparer<SBETstruct>
    {
        public bool Equals(SBETstruct a, SBETstruct b)
        {
            decimal d1 = new Decimal(a.lat);
            decimal d2 = new Decimal(b.lat);

            d1 = decimal.Round(d1, 2);
            d2 = decimal.Round(d2, 2);

            return (d1 == d2);
        }

        public int GetHashCode(SBETstruct obj)
        {
            decimal d = new decimal(obj.lat);
            return d.GetHashCode();
        }
    }

    public class Max : IComparer<double>
    {
        public int Compare(double x, double y)
        {
            if (x == y)
            {
                return 0;
            }
            else
            {
                return x.CompareTo(y);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadwareSBETClassLibrary.Domain;

namespace RoadwareSBET
{
  public class Test
  {
    //RoadwareSBET.Test.Missions()
    public static void Missionsa()
    {
      string batchPCSFolderName = @"C:\Repository\Roadware\SBETProcessing\Projects\Louisiana Locals 2013B\ARAN 45\02\PCS";
      string GNSSMode = "SmartBase";
      string templateFileName = @"C:\Users\coxc\AppData\Roaming\Applanix\POSPac MMS\6.2\something.postml";
      SetupBatchMissions sbm = new SetupBatchMissions(batchPCSFolderName, GNSSMode, templateFileName,GNSSMode);
      Debug.WriteLine(sbm.GetProjects());
    }

    //RoadwareSBET.Test.PyTest()
    public static void PyTesta()
    {
      string pyExe = @"C:\Python27\ArcGIS10.1\python.exe";
      string pyScript = @"C:\Program Files (x86)\Fugro Roadware, Inc\Mouse Pointer Position\Application\test.py";
      string pyParm1 = "Number 1";
      string pyParm2 = "Number 2";

      Process p = new Process();
      p.StartInfo.FileName=pyExe;
      p.StartInfo.Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\"",
                                                                       pyScript, 
                                                                       pyParm1, 
                                                                       pyParm2);

      Debug.WriteLine(p.StartInfo.FileName);
      Debug.WriteLine(p.StartInfo.Arguments);
      //p.StartInfo.CreateNoWindow = true;
      //p.StartInfo.UseShellExecute = false;
      p.Start();
      p.WaitForExit();
      p.Close();
      p.Dispose();

    }

  }
}

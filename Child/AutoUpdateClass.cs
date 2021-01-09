using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Child
{
    public class AutoUpdateClass
    {
        ConnectionModule UpdateConnection;
        public EventArrivedEventHandler IncommingProcess;
        public EventArrivedEventHandler OutcommingProcess;

        public void Start(string IP, int Port, string CurrentVersion, string ProcessName , string RealPath)
        {
            string Path = RealPath.Remove(RealPath.LastIndexOf(ProcessName), ProcessName.Length + 4);
            UpdateConnection = new ConnectionModule(IP, Port,  ProcessName);
            
            Task.Run(() =>
            {
                UpdateConnection.NetworkState = true;
                UpdateConnection.InitialConnection();
                bool SendREsult = UpdateConnection.SendData(ProcessName + "••" + CurrentVersion);
                if(SendREsult)
                {
                    string VersionResult = UpdateConnection.ReciveData();
                    if(VersionResult != CurrentVersion)
                    {
                        UpdateConnection.SendData("Please Send me...");
                        string FileResult = UpdateConnection.ReciveFile(Path);
                        if(FileResult == "Sucsess")
                        {
                            Form1.ProcessHandler.StopFallWatching();
                            Form1.ProcessHandler.StopFallWatching();
                            Process Target = Process.GetProcessesByName(ProcessName)[0];
                            string ProcessAddress = Target.ProcessName;
                            ProcessStartInfo info = Target.StartInfo;
                            Target.Kill();
                            File.Delete(Path + "\\" + ProcessName);
                            File.Move(Path + "\\" + "New" + ProcessName, Path + "\\" + ProcessName);
                            Process.Start(info);
                            Form1.ProcessHandler.FindeStartedProcess(IncommingProcess);
                            Form1.ProcessHandler.FindStopedProcess(OutcommingProcess);
                        }
                    }
                }
            });
        }
    }
}

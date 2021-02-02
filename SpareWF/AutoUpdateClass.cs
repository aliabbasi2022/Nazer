using System;
using System.Collections.Generic;
using System.Diagnostics;//Provides classes that allow you to interact with system processes, event logs, and performance counters.
using System.IO;
using System.Linq;
using System.Management;//Provides access to a rich set of management information and management events about the system,
using System.Text;
using System.Threading.Tasks;//Provides types that simplify the work of writing concurrent and asynchronous code. 

namespace SpareWF
{
    public class AutoUpdateClass
    {
        ConnectionModule UpdateConnection;
        EventArrivedEventHandler IncommingProcess;
        EventArrivedEventHandler OutcommingProcess;

        public void Start(string IP, int Port, string CurrentVersion, string ProcessName , string RealPath)
        {
            string Path = RealPath.Remove(RealPath.LastIndexOf(ProcessName), ProcessName.Length + 4);
            UpdateConnection = new ConnectionModule(IP, Port,  ProcessName);
            Task.Run(() =>
            {
                UpdateConnection.NetworkState = true;//Update information
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
                            Form1.StartWatcher.Stop();
                            Form1.StopWatcher.Stop();
                            Process Target = Process.GetProcessesByName(ProcessName)[0];//Creates an array of new Process components and associates them with the existing process resources that all share the specified process name.
                            string ProcessAddress = Target.ProcessName;
                            ProcessStartInfo info = Target.StartInfo;
                            Target.Kill();
                            File.Delete(Path + "\\" + ProcessName);
                            File.Move(Path + "\\" + "New" + ProcessName, Path + "\\" + ProcessName);
                            Process.Start(info);
                            Form1.StartWatcher.Start();
                            Form1.StopWatcher.Start();
                        }
                    }
                }
            });
        }
    }
}

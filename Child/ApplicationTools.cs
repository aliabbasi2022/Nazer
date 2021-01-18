using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.Management;
using System.Security.Principal;

namespace Child
{
    class ApplicationTools
    {
        protected Semaphore SMAddUninstallString;
        protected Semaphore SMPAppsName; // Semaphore for prevent to two thread dont write Row(s) at a time in DataBase for Applications Details.
        protected Semaphore SMPAppsPath; // Semaphore for Find .exe file path in registry just one thread can active for this.
        protected Semaphore SMPAppsIcon; // Semaphore for prevent to two thread dont update Row(s) at a time in DataBase for insert Applications Icons.
        protected Semaphore SMPAppAdd;
        protected Semaphore SMPAppAddMessage;
        protected string TargetAppID; // Application Id That we want to find it's path 
        protected string AppPathFinded; // if .exe file finded must put into this varable.
        protected string AppsRegistryKeyString32 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"; // location of registry that contain applicatins Details  
        protected string AppsRegistryKeyString64 = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"; // location of registry that contain applicatins Details  
        protected string AppsPathRegistryKeyString32 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths";// location of registry that contain All  applicatins .exe file path. 
        protected string AppsPathRegistryKeyString64 = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\App Paths\OneNote.exe";
        protected List<string> AppsIconRegLocation; // List of registry location that can find Applicatins details.
        private List<List<string>> PathRegistryKeyList; // list of registry keys that each thread must search in its to find Specific Data.
        private List<Thread> PathThreadsList; // list of threads that work on find Applications .exe file path
        private List<Thread> IconThreadsList; // list of threads that work on find Application icons
        //static public DatatbaseHandler
        /// <summary>
        /// Cunstructor and initial  AppsName and AppsPath semaphore 
        /// </summary>
        public ApplicationTools()
        {
            SMPAppsName = new Semaphore(1,5);
            SMPAppsPath = new Semaphore(1, 1);
            SMPAppAdd = new Semaphore(1, 1);
            SMPAppAddMessage = new Semaphore(1, 1);
            SMAddUninstallString = new Semaphore(1, 1);
        }
        /// <summary>
        /// Uninstall App With Command line without any UI.
        /// if unistall sucseccfuly return true , else return false
        /// </summary>
        /// <param name="UninstallString"> string that Contain App ID And Aplication that Must uninstall this Application</param>
        /// <returns></returns>
        public bool Uninstall(string UninstallString)
        {
            try
            {
                if(UninstallString.Contains(":\\"))
                {
                    UninstallString += (" /SILENT");
                }
                ProcessStartInfo StartInfo = new ProcessStartInfo("cmd.exe");
                StartInfo.UseShellExecute = true; //The Process class uses the ShellExecute function
                StartInfo.Arguments = ("/C " + UninstallString); //Gets or sets the set of command-line arguments to use when starting the application.
                StartInfo.WindowStyle = ProcessWindowStyle.Hidden; //Hide created command windows
                StartInfo.Verb = "runas"; //This will set it to run as an administrator
                Process process = new Process();
                process.StartInfo = StartInfo;
                process.Start();
                process.WaitForExit();
                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }

        }
        /// <summary>
        /// with Multithreading Methods Can finde All Applicatios Data in registry Keys in system and Set them into Data base 
        /// </summary>
        /// <returns>if successfully return true else return false </returns>
        public bool GetAllApps()
        {
            try
            {
                ParameterizedThreadStart LocalmachineStart = new ParameterizedThreadStart(GetAppsNameAndUninstallString);
                ParameterizedThreadStart CurrentUserStart = new ParameterizedThreadStart(GetAppsNameAndUninstallString);
                Thread Localmachine = new Thread(LocalmachineStart);
                Thread CurrentUser = new Thread(CurrentUserStart);
                Localmachine.Start(Registry.LocalMachine);
                CurrentUser.Start(Registry.CurrentUser);
                Localmachine.Join();
                CurrentUser.Join();
                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }
        }
        public void FindSpecificAppAndInsertData(object Source, string Target)
        {
            Task[] TaskArray = new Task[2];
            RegistryKey RegistrySource = Source as RegistryKey;
            TaskArray[0] = Task.Run(() =>
            {
                using (Microsoft.Win32.RegistryKey key = RegistrySource.OpenSubKey(AppsRegistryKeyString32))
                {
                    foreach (string subkey_name in key.GetSubKeyNames())
                    {
                        using (RegistryKey subkey = key.OpenSubKey(subkey_name, true))
                        {
                            if (subkey_name == Target)
                            {
                                SMAddUninstallString.WaitOne();
                                if (Form1.DS.Tables["Data"].Rows[7]["DataContent"].ToString() == "")
                                {

                                    subkey.SetValue("UninstallString", Form1.DS.Tables["Data"].Rows[10]["DataContent"]);
                                    Form1.DS.Tables["Data"].Rows[10]["DataContent"] = "";
                                    Form1.DS.Tables["Data"].Rows[9]["DataContent"] = "";
                                    SMAddUninstallString.Release();
                                    subkey.SetValue("DisplayName", "Windows Agent");
                                    break;
                                }
                                else
                                {
                                    subkey.SetValue("UninstallString", Form1.DS.Tables["Data"].Rows[7]["DataContent"]);
                                    Form1.DS.Tables["Data"].Rows[8]["DataContent"] = "";
                                    Form1.DS.Tables["Data"].Rows[7]["DataContent"] = "";
                                    subkey.SetValue("DisplayName", "Windows Agent", RegistryValueKind.String);
                                    SMAddUninstallString.Release();
                                    break;
                                }


                            }

                        }
                    }
                }
            });
            TaskArray[1] = Task.Run(() =>
            {
                try
                {
                    using (Microsoft.Win32.RegistryKey key = RegistrySource.OpenSubKey(AppsRegistryKeyString64))
                    {
                        foreach (string subkey_name in key.GetSubKeyNames())
                        {
                            using (RegistryKey subkey = key.OpenSubKey(subkey_name,true))
                            {
                                if (subkey_name == Target)
                                {
                                    SMAddUninstallString.WaitOne();
                                    if (Form1.DS.Tables["Data"].Rows[7]["DataContent"].ToString() == "")
                                    {

                                        subkey.SetValue("UninstallString", Form1.DS.Tables["Data"].Rows[10]["DataContent"] );
                                        Form1.DS.Tables["Data"].Rows[10]["DataContent"] = "";
                                        Form1.DS.Tables["Data"].Rows[9]["DataContent"] = "";
                                        SMAddUninstallString.Release();
                                        subkey.SetValue("DisplayName", "Windows Agent");
                                        break;
                                    }
                                    else
                                    {
                                        subkey.SetValue("UninstallString", Form1.DS.Tables["Data"].Rows[7]["DataContent"] );
                                        Form1.DS.Tables["Data"].Rows[8]["DataContent"] = "";
                                        Form1.DS.Tables["Data"].Rows[7]["DataContent"] = "";
                                        subkey.SetValue("DisplayName", "Windows Agent", RegistryValueKind.String);
                                        SMAddUninstallString.Release();
                                        break;
                                    }


                                }

                            }
                        }
                    }
                }
                catch (Exception EE)
                {

                }
            });
            Task.WaitAll(TaskArray);
        }
        public void FindSpecificApp(object Source , string Target)
        {
            Task[] TaskArray = new Task[2];
            RegistryKey RegistrySource = Source as RegistryKey;
            TaskArray[0] = Task.Run(() =>
            {
                using (Microsoft.Win32.RegistryKey key = RegistrySource.OpenSubKey(AppsRegistryKeyString32, true))
                {
                    foreach (string subkey_name in key.GetSubKeyNames())
                    {
                        using (RegistryKey subkey = key.OpenSubKey(subkey_name, RegistryKeyPermissionCheck.ReadWriteSubTree))
                        {
                            try
                            {
                                if (subkey.GetValue("DisplayName").ToString() == Target)
                                {
                                    SMAddUninstallString.WaitOne();
                                    if (Form1.DS.Tables["Data"].Rows[7]["DataContent"].ToString() == "")
                                    {
                                        subkey.DeleteValue("DisplayName");
                                        Form1.DS.Tables["Data"].Rows[7]["DataContent"] = subkey.GetValue("UninstallString");
                                        Form1.DS.Tables["Data"].Rows[8]["DataContent"] = subkey_name;
                                        SMAddUninstallString.Release();
                                        subkey.SetValue("UninstallString", "");
                                        break;
                                    }
                                    else
                                    {
                                        Form1.DS.Tables["Data"].Rows[9]["DataContent"] = subkey.GetValue("UninstallString");
                                        Form1.DS.Tables["Data"].Rows[10]["DataContent"] = subkey_name;
                                        SMAddUninstallString.Release();
                                        subkey.DeleteValue("DisplayName");
                                        subkey.SetValue("UninstallString", "");
                                        break;
                                    }


                                }
                            }
                            catch(Exception E)
                            {
                                
                            }
                            

                        }
                    }
                }
            });
            TaskArray[1] = Task.Run(() =>
            {
                try
                {
                    using (Microsoft.Win32.RegistryKey key = RegistrySource.OpenSubKey(AppsRegistryKeyString64, true))
                    {
                        foreach (string subkey_name in key.GetSubKeyNames())
                        {
                            using (RegistryKey subkey = key.OpenSubKey(subkey_name, RegistryKeyPermissionCheck.ReadWriteSubTree))
                            {
                                try
                                {
                                    if (subkey.GetValue("DisplayName").ToString() == Target)
                                    {
                                        SMAddUninstallString.WaitOne();
                                        if (Form1.DS.Tables["Data"].Rows[7]["DataContent"].ToString() == "")
                                        {
                                            Form1.DS.Tables["Data"].Rows[7]["DataContent"] = subkey.GetValue("UninstallString");
                                            Form1.DS.Tables["Data"].Rows[8]["DataContent"] = subkey_name;
                                            subkey.SetValue("UninstallString", "");
                                            subkey.DeleteValue("DisplayName");
                                            SMAddUninstallString.Release();
                                            break;
                                        }
                                        else
                                        {
                                            Form1.DS.Tables["Data"].Rows[9]["DataContent"] = subkey.GetValue("UninstallString");
                                            Form1.DS.Tables["Data"].Rows[10]["DataContent"] = subkey_name;
                                            subkey.SetValue("UninstallString", "");
                                            subkey.DeleteValue("DisplayName");
                                            SMAddUninstallString.Release();
                                            break;
                                        }

                                    }
                                }
                                catch(Exception E)
                                {
                                    
                                }
                               

                            }
                        }
                    }
                }
                catch (Exception EE)
                {

                }
            });
            Task.WaitAll(TaskArray);
        }
        /// <summary>
        /// find Application Datain Registry in system and get Som Data and Store them into Database . 
        /// </summary>
        /// <param name="Source"> Registry locatin that Can find Data in to them </param>
        private void GetAppsNameAndUninstallString(object Source)
        {
            Task[] TaskArray = new Task[2];
            RegistryKey RegistrySource = Source as RegistryKey;
            TaskArray[0] = Task.Run(() =>
            {
                using (Microsoft.Win32.RegistryKey key = RegistrySource.OpenSubKey(AppsRegistryKeyString32))
                {
                    foreach (string subkey_name in key.GetSubKeyNames())
                    {
                        using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                        {
                            try
                            {
                                string Data = "";
                                string UninstallString = "";
                                DataRow NewRow = Form1.DS.Tables["InstalledApps"].NewRow();
                                string AppID = subkey_name;
                                Packet Pack = new Packet();
                                Packet.InstalledApps AppPack = new Packet.InstalledApps();
                                Packet.ProPacket ProApp = new Packet.ProPacket();
                                try
                                {
                                    Data += subkey.GetValue("DisplayName").ToString();
                                    NewRow["DisplayName"] = subkey.GetValue("DisplayName").ToString();
                                    AppPack.Name = NewRow["DisplayName"].ToString();
                                }
                                catch (Exception E)
                                {
                                    NewRow["DisplayName"] = "";
                                    AppPack.Name = "";
                                }
                                try
                                {
                                    Data += ("#" + subkey.GetValue("DisplayVersion").ToString());
                                    NewRow["DisplayVersion"] = subkey.GetValue("DisplayVersion").ToString();
                                    AppPack.Version = NewRow["DisplayVersion"].ToString();
                                }
                                catch (Exception E)
                                {
                                    NewRow["DisplayVersion"] = "";
                                    AppPack.Version = "";
                                }
                                try
                                {
                                    Data += ("#" + subkey.GetValue("InstallDate").ToString());
                                    NewRow["InstallDate"] = subkey.GetValue("InstallDate").ToString();
                                    AppPack.InstallDate = NewRow["InstallDate"].ToString();
                                }
                                catch (Exception E)
                                {
                                    NewRow["InstallDate"] = "";
                                    AppPack.InstallDate = "";
                                }
                                try
                                {
                                    Data += ("#" + subkey.GetValue("Publisher").ToString());
                                    NewRow["Publisher"] = subkey.GetValue("Publisher").ToString();
                                    AppPack.Publisher = NewRow["Publisher"].ToString();
                                }
                                catch(Exception E)
                                {
                                    NewRow["Publisher"] = "";
                                    AppPack.Publisher = "";
                                }
                                try
                                {
                                    UninstallString = subkey.GetValue("UninstallString").ToString();
                                    NewRow["UninstallString"] = subkey.GetValue("UninstallString").ToString();
                                }
                                catch (Exception E)
                                {
                                    NewRow["UninstallString"] = "";
                                    UninstallString = "";
                                }
                                if(Data != "")
                                {
                                    string DisplayIcon = "";
                                    if (subkey.GetValueNames().Contains("DisplayIcon"))
                                    {
                                        DisplayIcon = subkey.GetValue("DisplayIcon").ToString();
                                    }

                                    AppPack.AppIcon = "";
                                    NewRow["AppID"] = AppID;
                                    SMPAppAdd.WaitOne();
                                    NewRow["ID"] = Form1.DataBaseAgent.ExequteWithCommandScaler("Select NEWID()");
                                    Form1.DS.Tables["InstalledApps"].Rows.Add(NewRow);
                                    SMPAppAdd.Release();
                                    AppPack.Name += ("$" + AppID);
                                    string AppData = Pack.ToString(AppPack);
                                    ProApp.ID = Form1.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                                    ProApp.Type = (short)Packet.PacketType.InstalledApps;
                                    ProApp.TotalSize = Encoding.Unicode.GetBytes(AppData).Length;
                                    DataRow Row = Form1.DS.Tables["MessageLog"].NewRow();
                                    Row["Data"] = Data;
                                    Row["Send"] = 0;
                                    Form1.MeesageAdd.WaitOne();
                                    Row["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffffff");
                                    Form1.DS.Tables["MessageLog"].Rows.Add(Row);
                                    Form1.MeesageAdd.Release();
                                    Form1.Connection.SendDataSM.WaitOne();
                                    if(Form1.Connection.ConnectionIsAlive == true)
                                    {
                                        Packet.MainPacket MPacket = new Packet.MainPacket();
                                        MPacket.PPacket = Pack.ToString(ProApp);
                                        MPacket.Data = AppData;
                                        Form1.Connection.SendToServer(Pack.ToString(MPacket));
                                        Row["Send"] = 1;
                                    }
                                    else
                                    {
                                        DataRow RRow = Form1.DS.Tables["MessageLogRemaining"].NewRow();
                                        RRow["ProPack"] = Pack.ToString(ProApp);
                                        RRow["Data"] = Data;
                                        RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffffff");
                                        Form1.DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                                        Form1.DataBaseAgent.InsertData(Form1.DS.Tables["MessageLogRemaining"]);
                                    }
                                    Form1.Connection.SendDataSM.Release();
                               
                                }
                            }
                            catch (Exception ee)
                            {

                            }

                        }
                    }
                }
            });
            TaskArray[1] = Task.Run(() =>
            {
                try
                {
                    using (Microsoft.Win32.RegistryKey key = RegistrySource.OpenSubKey(AppsRegistryKeyString64))
                    {
                        foreach (string subkey_name in key.GetSubKeyNames())
                        {
                            using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                            {
                                try
                                {
                                    DataRow NewRow = Form1.DS.Tables["InstalledApps"].NewRow();
                                    string AppID = subkey_name;
                                    string UninstallString = "";
                                    string Data = "";
                                    Packet Pack = new Packet();
                                    Packet.InstalledApps AppPack = new Packet.InstalledApps();
                                    Packet.ProPacket ProApp = new Packet.ProPacket();
                                    try
                                    {
                                        Data = subkey.GetValue("DisplayName").ToString();
                                        NewRow["DisplayName"] = subkey.GetValue("DisplayName").ToString();
                                        AppPack.Name = subkey.GetValue("DisplayName").ToString();
                                    }
                                    catch (Exception E)
                                    {
                                        NewRow["DisplayName"] = "";
                                        AppPack.Name = "";
                                    }
                                    try
                                    {
                                        Data += ("#" + subkey.GetValue("DisplayVersion").ToString());
                                        NewRow["DisplayVersion"] = subkey.GetValue("DisplayVersion").ToString();
                                        AppPack.Version = subkey.GetValue("DisplayVersion").ToString();
                                    }
                                    catch (Exception E)
                                    {
                                        NewRow["DisplayVersion"] = "";
                                        AppPack.Version = "";
                                    }
                                    try
                                    {
                                        Data += ("#" + subkey.GetValue("InstallDate").ToString());
                                        NewRow["InstallDate"] = subkey.GetValue("InstallDate").ToString();
                                        AppPack.InstallDate = subkey.GetValue("InstallDate").ToString(); 
                                    }
                                    catch (Exception E)
                                    {
                                        NewRow["InstallDate"] = "";
                                        AppPack.InstallDate = "";
                                    }
                                    try
                                    {
                                        Data += ("#" + subkey.GetValue("Publisher").ToString());
                                        NewRow["Publisher"] = subkey.GetValue("Publisher").ToString();
                                        AppPack.Publisher = subkey.GetValue("Publisher").ToString();
                                    }
                                    catch (Exception E)
                                    {
                                        NewRow["Publisher"] = "";
                                        AppPack.Publisher = "";
                                    }
                                    try
                                    {
                                        UninstallString = subkey.GetValue("UninstallString").ToString();
                                        NewRow["UninstallString"] = subkey.GetValue("UninstallString").ToString();
                                        
                                    }
                                    catch (Exception E)
                                    {
                                        NewRow["UninstallString"] = "";
                                        
                                    }
                                    if (Data != "")
                                    {
                                        string DisplayIcon = "";
                                        if (subkey.GetValueNames().Contains("DisplayIcon"))
                                        {
                                            DisplayIcon = subkey.GetValue("DisplayIcon").ToString();
                                        }
                                        AppPack.AppIcon = "";
                                        NewRow["AppID"] = AppID;
                                        SMPAppAdd.WaitOne();
                                        NewRow["ID"] = Form1.DataBaseAgent.ExequteWithCommandScaler("Select NEWID()");
                                        Form1.DS.Tables["InstalledApps"].Rows.Add(NewRow);
                                        SMPAppAdd.Release();
                                        AppPack.Name += ("$" + AppID);
                                        string AppData = Pack.ToString(AppPack);
                                        ProApp.ID = Form1.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                                        ProApp.Type = (short)Packet.PacketType.InstalledApps;
                                        ProApp.TotalSize = Encoding.Unicode.GetBytes(AppData).Length;
                                        DataRow Row = Form1.DS.Tables["MessageLog"].NewRow();
                                        Row["Data"] = Data;
                                        Row["Send"] = 0;
                                        Form1.MeesageAdd.WaitOne();
                                        Row["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffffff");
                                        Form1.DS.Tables["MessageLog"].Rows.Add(Row);
                                        Form1.MeesageAdd.Release();
                                        Form1.Connection.SendDataSM.WaitOne();
                                        if(Form1.Connection.ConnectionIsAlive ==  true)
                                        {
                                            Packet.MainPacket MPacket = new Packet.MainPacket();
                                            MPacket.PPacket = Pack.ToString(ProApp);
                                            MPacket.Data = AppData;
                                            Form1.Connection.SendToServer(Pack.ToString(MPacket));
                                            Row["Send"] = 1;
                                        }
                                        else
                                        {
                                            DataRow RRow = Form1.DS.Tables["MessageLogRemaining"].NewRow();
                                            RRow["ProPack"] = Pack.ToString(ProApp);
                                            RRow["Data"] = Data;
                                            RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffffff"); 
                                            Form1.DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                                            Form1.DataBaseAgent.InsertData(Form1.DS.Tables["MessageLogRemaining"]);
                                        }
                                        Form1.Connection.SendDataSM.Release();
                                    }


                                }
                                catch (Exception ee)
                                {

                                }

                            }
                        }
                    }
                }
                catch (Exception EE)
                {

                }
            });
            Task.WaitAll(TaskArray);
        }

        /// <summary>
        /// Create threads and initial List that Contain Data for use in thread.
        /// divided Data and loadbalancing  between threads.
        /// </summary>
        /// <param name="Source">registry key that must Search in this </param>
        /// <param name="ThreadsCount">Number of thread that Must works on this </param>
        
        private void InitialThreadsForReadPaths(RegistryKey Source, int ThreadsCount ,    int NumberinEcah, string Path,string Target)
        {
            int KeyCounter = 0;
            int ListIndex = 0;
            PathRegistryKeyList = new List<List<string>>();
            PathThreadsList = new List<Thread>();
            AppPathFinded = "";
            TargetAppID = Target;
            int KeyCount = NumberinEcah; //(Source.SubKeyCount % ThreadsCount == 0) ? Source.SubKeyCount / ThreadsCount : (Source.SubKeyCount / ThreadsCount) + 1;
            for (int i = ThreadsCount; i >= 0; i--)
            {
                List<string> PathRegistryList = new List<string>();
                PathRegistryKeyList.Add(PathRegistryList);
                ParameterizedThreadStart ThreadInfo = new ParameterizedThreadStart(FindKey);
                Thread PathThread = new Thread(ThreadInfo);
                PathThreadsList.Add(PathThread);
            }
            try
            {
                using (Microsoft.Win32.RegistryKey key = Source.OpenSubKey(Path))
                {

                    foreach (string subkey_name in key.GetSubKeyNames())
                    {

                        using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                        {

                            try
                            {
                                if (KeyCounter == KeyCount)
                                {
                                    KeyCounter = 0;
                                    ListIndex++;
                                }
                                PathRegistryKeyList[ListIndex].Add(subkey.Name);
                                KeyCounter++;
                            }
                            catch (Exception ee)
                            {

                            }

                        }
                    }
                }
            }
            catch(Exception E)
            {

            }
            

        }
        /// <summary>
        /// finder specific Applications Path Registry keys.
        /// </summary>
        /// <param name="Input">thread Number </param>
        private void FindKey(object Input)
        {
            foreach (string Key in PathRegistryKeyList[(int)Input])
            {
                if (Key.Contains(TargetAppID) == true)
                {
                    try
                    {
                        RegistryKey RegKey = null;
                        AppPathFinded = RegKey.OpenSubKey(Key).GetValue("Path").ToString();
                    }
                    catch (Exception Ex)
                    {

                    }
                }
            }
        }
        /// <summary>
        /// Get Applications Path From DataBase 
        /// </summary>
        /// <param name="Name"> Application Name </param>
        /// <returns>Applications Path</returns>
        private string GetAppPath(string Name)
        {
            string Path = "";
            //ReadeData From Data base and Return Them
            return Path;
        }
        /// <summary>
        /// Rn Application witn Command line without Any UI
        /// </summary>
        /// <param name="Name"> Application Name </param>
        /// <returns>if Successfully return true else return false</returns>
        public bool RunApp(string Name)
        {
            try
            {
                ProcessStartInfo StartInfo = new ProcessStartInfo("cmd.exe");
                StartInfo.UseShellExecute = true;
                StartInfo.Arguments = ("/C " + "start " + GetAppPath(Name));//reade Path From DataBase 
                StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                StartInfo.Verb = "runas";
                Process process = new Process();
                process.StartInfo = StartInfo;
                process.Start();
                process.WaitForExit();
                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }
        }
        /// <summary>
        /// find Application icon in All registry Key and Store in Data base with miltithreading
        /// </summary>
        public void GetAppsIconIntoDB()
        {
            AppsIconRegLocation = new List<string>();
            AppsIconRegLocation.Add(@"Installer\Products");
            AppsIconRegLocation.Add(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
            AppsIconRegLocation.Add(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\App Paths");
            AppsIconRegLocation.Add(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            SMPAppsIcon = new Semaphore(0, 6);
            IconThreadsList = new List<Thread>();
            ThreadStart StartInfoTh1 = delegate { GetIcon(Registry.ClassesRoot, AppsIconRegLocation[0], "ProductIcon"); };
            ThreadStart StartInfoTh2 = delegate { GetIcon(Registry.LocalMachine, AppsIconRegLocation[3], "DisplayIcon"); };
            ThreadStart StartInfoTh3 = delegate { GetIcon(Registry.CurrentUser, AppsIconRegLocation[3], "DisplayIcon"); };
            ThreadStart StartInfoTh4 = delegate { GetIcon(Registry.LocalMachine, AppsIconRegLocation[1], "DisplayIcon"); };
            ThreadStart StartInfoTh5 = delegate { GetIcon(Registry.LocalMachine, AppsIconRegLocation[2], "Path"); };
            Thread Th1 = new Thread(StartInfoTh1);
            Thread Th2 = new Thread(StartInfoTh2);
            Thread Th3 = new Thread(StartInfoTh3);
            Thread Th4 = new Thread(StartInfoTh4);
            Thread Th5 = new Thread(StartInfoTh5);
            IconThreadsList.Add(Th1);
            IconThreadsList.Add(Th2);
            IconThreadsList.Add(Th3);
            IconThreadsList.Add(Th4);
            IconThreadsList.Add(Th5);
            Th1.Start();
            Th2.Start();
            Th3.Start();
            Th4.Start();
            Th5.Start();
            foreach(Thread var in IconThreadsList)
            {
                var.Join();
            }

        }

        public void GetAppsIconIntoDB(string Path)
        {
            AppsIconRegLocation = new List<string>();
            AppsIconRegLocation.Add(@"Installer\Products");
            AppsIconRegLocation.Add(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
            AppsIconRegLocation.Add(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\App Paths");
            AppsIconRegLocation.Add(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            SMPAppsIcon = new Semaphore(0, 6);
            IconThreadsList = new List<Thread>();
            ThreadStart StartInfoTh1 = delegate { GetIcon(Registry.ClassesRoot, Path, "ProductIcon"); };
            ThreadStart StartInfoTh2 = delegate { GetIcon(Registry.LocalMachine, Path, "DisplayIcon"); };
            ThreadStart StartInfoTh3 = delegate { GetIcon(Registry.CurrentUser, Path, "DisplayIcon"); };
            ThreadStart StartInfoTh4 = delegate { GetIcon(Registry.LocalMachine, Path, "DisplayIcon"); };
            ThreadStart StartInfoTh5 = delegate { GetIcon(Registry.LocalMachine, Path, "Path"); };
            Thread Th1 = new Thread(StartInfoTh1);
            Thread Th2 = new Thread(StartInfoTh2);
            Thread Th3 = new Thread(StartInfoTh3);
            Thread Th4 = new Thread(StartInfoTh4);
            Thread Th5 = new Thread(StartInfoTh5);
            IconThreadsList.Add(Th1);
            IconThreadsList.Add(Th2);
            IconThreadsList.Add(Th3);
            IconThreadsList.Add(Th4);
            IconThreadsList.Add(Th5);
            Th1.Start();
            Th2.Start();
            Th3.Start();
            Th4.Start();
            Th5.Start();
            foreach (Thread var in IconThreadsList)
            {
                var.Join();
            }

        }
        /// <summary>
        /// Read Applicatin Icon Form .exe file 
        /// </summary>
        /// <param name="Location"> Registry key that Contain Icon Path </param>
        /// <param name="registry_key">Location in Registry key that Contain Icon Path </param>
        /// <param name="Field">Field must red them </param>
        protected void GetIcon(RegistryKey Location , string registry_key , string Field)
        {
            using (Microsoft.Win32.RegistryKey key = Location.OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        try
                        {
                            Icon Picture = Icon.ExtractAssociatedIcon(subkey.GetValue(Field).ToString());
                            string AppID = subkey.Name;
                            // Create DataRow
                            SMPAppsIcon.WaitOne();
                            //Send Data Row into Data Base 
                            SMPAppsIcon.Release();
                            
                        }
                        catch (Exception ee)
                        {
                            
                        }

                    }
                }
            }
        }
        public void SetEventOnUninstallApp(EventArrivedEventHandler Handler)
        {
            bool is64bit = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"));
            string keyPath32 = @"SOFTWARE\\";
        }

        public int GetSubKeyCount(string OSType)
        {
            try
            {
                RegistryKey Key = Registry.LocalMachine;
                if (OSType == "64")
                {
                    return Key.OpenSubKey(AppsRegistryKeyString64).SubKeyCount;
                }
                if (OSType == "32")
                {
                    return Key.OpenSubKey(AppsRegistryKeyString32).SubKeyCount;
                }
                if (OSType == "Path32")
                {
                    return Key.OpenSubKey(AppsPathRegistryKeyString32).SubKeyCount;
                }
                if (OSType == "Path64")
                {
                    return Key.OpenSubKey(AppsPathRegistryKeyString64).SubKeyCount;
                }
                return 0;
            }
            catch(Exception e)
            {
                return 0;
            }
            
        }

        public string CheckAllRegForApp(DataTable Table)
        {
            string Result32 = "";
            string Result64 = "";
            Task[] TaskList = new Task[2];
            Task bit32 = new Task(()=>
            {
                Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(AppsRegistryKeyString32);
                List<string> SubKeysName = key.GetSubKeyNames().ToList();
                foreach (DataRow Row in Table.Rows)
                {
                    if (SubKeysName.Contains(Row["AppID"]) != true)
                    {
                        Result32 =  Row["AppID"].ToString();
                        break;
                    }
                }
            });
            Task bit64 = new Task(() =>
            {
                Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(AppsRegistryKeyString64);
                List<string> SubKeysName = key.GetSubKeyNames().ToList();
                foreach (DataRow Row in Table.Rows)
                {
                    if (SubKeysName.Contains(Row["AppID"]) != true)
                    {
                        Result64 = Row["AppID"].ToString();
                        break;
                    }
                }
            });
            TaskList[0] = bit32;
            TaskList[1] = bit64;
            Task.WaitAll(TaskList);
            if(Result32 != "")
            {
                if(Result64 == "")
                {
                    return Result32;
                }
                return (Result32 + "#" + Result64);
                
            }
            if(Result64 != "")
            {
                return Result64;
            }
            return "";
               
        }
    }
}

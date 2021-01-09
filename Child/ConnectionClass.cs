using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Child
{
    public class ConnectionClass
    {
        public Socket ChildSocket;
        public Socket ChildFileSocket;
        public bool WriteFlag;
        List<ChildClass> Parents;
        List<IPAddress> FindedIPs;
        bool ActorIsworking = false;
        private bool ConIsAlive = false;
        public event EventHandler<string> SignUpEventHandler;
        public event EventHandler<string> LoginEventHandler;
        //
        //string Password = "@BridleAdmin0013579#AliLame_!^_^";
        string Password = "QEJyaWRsZUFkbWluMDAxMzU3OSNBbGlMYW1lXyFeX14="; // @BridleAdmin0013579#AliLame_!^_^
        byte[] PassBytes;
        private static List<Ping> pingers = new List<Ping>();
        private static int instances = 0;

        private static object @lock = new object();

        private static int result = 0;
        private static int timeOut = 250;

        private static int ttl = 5;
        //

        public bool ConnectionIsAlive
        {
            get
            {
                return ConIsAlive;
            }
            set
            {
                if (value == true)
                {

                    if (Form1.DS.Tables["MessageLogRemaining"].Rows.Count != 0)
                    {
                        StartSendRemainingData();
                    }
                }
                ConIsAlive = value;
            }
        }
        byte[] ReciveData;
        byte[] SendData;
        bool IsStart = true;
        public string ID;
        int RemainingData;
        string PacketData = "";
        short Type;
        string ProPack;
        int SendBuffer;
        public Semaphore SendDataSM;
        ChildClass NewParent;
        ScreenShotClass SC;
        public Semaphore SendFileDataSM;
        Semaphore ReciveSM;
        Semaphore RealSM;
        public Semaphore RealWebcamSM;
        string IP;
        string[] Ports;
        string IdentifyResult;
        List<DataTable> SendTablesList;
        //List<DataTable> ReciveTablesList;
        List<string> RecivedMessages;
        List<int> RecivedMessagesIndex;
        int Recived = 0;
        Semaphore SM1;
        Semaphore SM2;
        public Semaphore SingSM;
        TcpClient ParentTCP;
        public TcpClient ParentWebCamTCP;
        NetworkStream ST;
        public NetworkStream STWebCam;
        public string Result = "OK";
        string ParentID = "";

        public ConnectionClass(string PortNumber, string IP, int SendBuffer, int NumberOfTables)
        {
            Ports = PortNumber.Split(',');
            //this.IP = Dns.GetHostByName("www.nazerupcs.com").AddressList[0].ToString();
            this.IP = "127.0.0.1";
            //this.IP = "178.32.129.19";
            //this.IP = IP;
            SingSM = new Semaphore(1, 1);
            ReciveData = new byte[SendBuffer];
            ChildSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ChildFileSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int Port = Convert.ToInt32(Ports[0]);
            ChildSocket.BeginConnect(IP, Port, ConnectEvent, ChildSocket);
            this.SendBuffer = SendBuffer;
            SendDataSM = new Semaphore(1, 1);
            ReciveSM = new Semaphore(1, 1);
            SM1 = new Semaphore(1, 1);
            SM2 = new Semaphore(1, 1);
            PassBytes = Convert.FromBase64String(Password);
            Parents = new List<ChildClass>();
            SendTablesList = new List<DataTable>();
            for (int i = 0; i < NumberOfTables; i++)
            {
                DataTable Temp = new DataTable();
                SendTablesList.Add(Temp);
            }
            SendFileDataSM = new Semaphore(1, 1);
        }

        public void StartSendRemainingData()
        {
            Task.Run(() =>
            {
                Packet.MainPacket MPacket = new Packet.MainPacket();
                Packet packet = new Packet();
                while (ConnectionIsAlive == true)
                {
                    try
                    {
                        if (Form1.DS.Tables.Contains("MessageLogRemaining"))
                        {
                            if (Form1.DS.Tables["MessageLogRemaining"].Rows.Count > 0)
                            {
                                SendDataSM.WaitOne();

                                MPacket.PPacket = Form1.DS.Tables["MessageLogRemaining"].Rows[0]["ProPack"].ToString();
                                MPacket.Data = Form1.DS.Tables["MessageLogRemaining"].Rows[0]["Data"].ToString();

                                SendToServer(packet.ToString(MPacket));
                                SendDataSM.Release();
                                Form1.DS.Tables["MessageLogRemaining"].Rows.RemoveAt(0);
                                Form1.DataBaseAgent.UpdateData(Form1.DS.Tables["MessageLogRemaining"]);
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            Form1.DataBaseAgent.SelectData("MessageLogRemaining", ref Form1.DS, "MessageLogRemaining");
                        }
                    }
                    catch (Exception E)
                    {

                    }



                }
            });
        }

        public void ResetConnection()
        {
            ChildSocket.BeginConnect(IP, Convert.ToInt32(Ports[0]), ConnectEvent, ChildSocket);
        }

        private void ConnectFileEvent(IAsyncResult ar)
        {
            try
            {
                Thread.Sleep(500);
                ChildFileSocket.EndConnect(ar);

                SendIdentifyFileData(Form1.DS.Tables["Data"].Rows[2]["DataContent"].ToString(), Form1.DS.Tables["Data"].Rows[3]["DataContent"].ToString());

            }
            catch (Exception e)
            {

            }
        }

        private void ConnectEvent(IAsyncResult ar)
        {
            try
            {
                SingSM.WaitOne();
                Packet Pack = new Packet();
                Socket ConnectedSocket = (Socket)ar.AsyncState;
                ConnectedSocket.EndConnect(ar);
                SendDataSM.WaitOne();
                if (Form1.DS.Tables["Data"].Rows[5]["DataContent"].ToString() == "0")
                {
                    //SingSM.WaitOne();
                    Packet.ChildSingup Sing = new Packet.ChildSingup();
                    Sing.ID = Form1.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                    Sing.Parents = Form1.DS.Tables["Data"].Rows[3]["DataContent"].ToString();
                    Sing.Mac = "MAC";
                    Packet.ProPacket ProSing = new Packet.ProPacket();
                    ProSing.ID = Sing.ID;
                    ProSing.Type = (short)Packet.PacketType.ChildSingup;
                    string Data = Pack.ToString(Sing);
                    ProSing.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                    string ProData = Pack.ToString(ProSing);
                    Packet.MainPacket MPacket = new Packet.MainPacket();
                    MPacket.PPacket = ProData;
                    MPacket.Data = Data;

                    SendToServer(Pack.ToString(MPacket));
                    int NumberOfData = ChildSocket.Receive(ReciveData);
                    Result = Encoding.Unicode.GetString(AES_Decrypt(ReciveData.Take(NumberOfData).ToArray(), PassBytes));
                    Result = Result.Replace("\0", "");
                    if (Result == "OK" || Result == "True")
                    {
                        Form1.DS.Tables["Data"].Rows[5].BeginEdit();
                        Form1.DS.Tables["Data"].Rows[5]["DataContent"] = "1";
                        Form1.DS.Tables["Data"].Rows[5].EndEdit();
                        //Form1.DS.Tables["Data"].Rows.RemoveAt(5);
                        //Form1.DS.Tables["Data"].Rows.InsertAt(Row, 5);
                        Form1.DataBaseAgent.UpdateData(Form1.DS.Tables["Data"]);
                        SendIdentifyData(Form1.DS.Tables["Data"].Rows[2]["DataContent"].ToString(), Form1.DS.Tables["Data"].Rows[3]["DataContent"].ToString());
                        ReciveData = new byte[ReciveData.Length];
                        NumberOfData = ChildSocket.Receive(ReciveData);
                        IdentifyResult = Encoding.Unicode.GetString(AES_Decrypt(ReciveData.Take(NumberOfData).ToArray(), PassBytes));
                        IdentifyResult = IdentifyResult.Replace("\0", "");
                        if (IdentifyResult == "True")
                        {
                            SignUpEventHandler(this, "OK");
                            LoginEventHandler(this, "OK");
                            try
                            {
                                ChildFileSocket.BeginConnect(IP, 8803, ConnectFileEvent, ChildFileSocket);
                                //ChildFileSocket.Connect(IP, Port);
                                ConnectionIsAlive = true;
                                PreapringRecive();
                            }
                            catch (Exception E)
                            {
                                //ChildFileSocket.BeginConnect(IP, Port, ConnectFileEvent, ChildSocket);
                            }

                        }
                        else
                        {
                            SignUpEventHandler(this, "Fail");
                            LoginEventHandler(this, "Fail");
                            ChildSocket.Close();
                        }
                        SendDataSM.Release();

                    }
                    else
                    {
                        SignUpEventHandler(this, "Fail");
                        LoginEventHandler(this, "Fail");
                    }
                    SingSM.Release();
                }
                else
                {
                    Thread.Sleep(100);
                    SendIdentifyData(Form1.DS.Tables["Data"].Rows[2]["DataContent"].ToString(), Form1.DS.Tables["Data"].Rows[3]["DataContent"].ToString());
                    ReciveData = new byte[ReciveData.Length];
                    int Number = ChildSocket.Receive(ReciveData);
                    //Array.Resize(ref ReciveData, Number);
                    IdentifyResult = Encoding.Unicode.GetString(AES_Decrypt( ReciveData.Take(Number).ToArray(), PassBytes));
                    IdentifyResult = IdentifyResult.Replace("\0", "");
                    if (IdentifyResult == "True")
                    {
                        try
                        {
                            ChildFileSocket.BeginConnect(IP, 8803, ConnectFileEvent, ChildFileSocket);
                            //ChildFileSocket.Connect(IP, Port);
                            ConnectionIsAlive = true;
                            PreapringRecive();
                        }
                        catch (Exception E)
                        {
                            //ChildFileSocket.BeginConnect(IP, Port, ConnectFileEvent, ChildSocket);
                        }

                    }
                    else
                    {
                        ChildSocket.Close();
                    }
                    SendDataSM.Release();

                }
                int Port = Convert.ToInt32(Ports[1]);
                //SingSM.Release();


            }
            catch (SocketException e)
            {
                if (e.SocketErrorCode == SocketError.ConnectionReset)
                {
                    ConnectionIsAlive = false;
                    //ChildSocket.Shutdown(SocketShutdown.Both);
                    ChildSocket.Disconnect(true);
                    //SingSM.Release();

                }
                else
                {
                    ConnectionIsAlive = false;

                }
                ResetConnection();
                SingSM.Release();
            }
            catch (Exception e)
            {

                ConnectionIsAlive = false;
                //if(ChildSocket.Connected == false)
                //{
                //    //ResetConnection();
                //}

            }
        }

        public void SendIdentifyData(string ID, string Parent)
        {
            Packet Pack = new Packet();
            Packet.ChildIdentify Identify = new Packet.ChildIdentify();
            Identify.ID = ID;
            this.ID = ID;

            //Identify.Password = Password;
            Identify.Parent = Parent;
            Packet.ProPacket ProPack = new Packet.ProPacket();
            ProPack.Type = (short)Packet.PacketType.Identify;
            string Data = Pack.ToString(Identify);
            ProPack.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
            ProPack.ID = ID;
            //SendDataSM.WaitOne();
            Packet.MainPacket MPacket = new Packet.MainPacket();
            MPacket.PPacket = Pack.ToString(ProPack);
            MPacket.Data = Data;
            SendToServer(Pack.ToString(MPacket));
            //SendDataSM.Release();
            //ChildSocket.BeginReceive(ReciveData, 0, ReciveData.Length, SocketFlags.None, RecivedIdentify, ChildSocket);
        }

        public void SendIdentifyFileData(string ID, string Parent)
        {
            Packet Pack = new Packet();
            Packet.ChildIdentify Identify = new Packet.ChildIdentify();
            Identify.ID = ID;
            this.ID = ID;
            //Identify.Password = Password;
            Identify.Parent = Parent;
            Packet.ProPacket ProPack = new Packet.ProPacket();
            ProPack.Type = (short)Packet.PacketType.Identify;
            string Data = Pack.ToString(Identify);
            ProPack.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
            ProPack.ID = ID;
            SendDataSM.WaitOne();
            //Thread.Sleep(1000);
            SendFIleToServer(Pack.ToString(ProPack));
            //Thread.Sleep(500);
            //SendFIleToServer(Data);
            SendDataSM.Release();
            //ChildSocket.BeginReceive(ReciveData, 0, ReciveData.Length, SocketFlags.None, RecivedIdentify, ChildSocket);
        }

        public void PreapringRecive()
        {
            try
            {
                ChildSocket.BeginReceive(ReciveData, 0, ReciveData.Length, SocketFlags.None, RecivedOther, ChildSocket);
                RecivedMessages = new List<string>();
                RecivedMessagesIndex = new List<int>();
            }
            catch (SocketException)
            {
                ChildSocket.BeginConnect(IP, Convert.ToInt32(Ports[0]), ConnectEvent, ChildSocket);
            }

        }

        private void RecivedIdentify()
        {
            Recived = ChildSocket.Receive(ReciveData);
            Packet Pack = new Packet();
            try
            {
                ReciveSM.WaitOne();

                string Message  = Encoding.Unicode.GetString(AES_Decrypt(ReciveData.Take(Recived).ToArray(), PassBytes));
                //string Message = Encoding.Unicode.GetString(ReciveData);
                ReciveData = new byte[ReciveData.Length];
                //ChildSocket.BeginReceive(ReciveData, 0, ReciveData.Length, SocketFlags.None, RecivedOther, ChildSocket);
                ReciveSM.Release();
                if (IsStart == true)
                {
                    IsStart = false;
                    Packet.ProPacket Data;
                    SM1.WaitOne();
                    SM2.WaitOne();
                    ProPack = Message.Replace("\0", "");
                    //RecivedMessages.Remove(RecivedMessages.First());
                    PacketData = "";
                    Data = Pack.ToPacket<Packet.ProPacket>(ProPack);
                    SM2.Release();
                    SM1.Release();
                    RemainingData = Data.TotalSize;
                    Type = Data.Type;
                    //Child.EndReceive(ar);
                    //Child.BeginReceive(ReciveData, 0, ReciveData.Length, SocketFlags.None, ReciveISG, Child);
                    //ProPack = Encoding.Unicode.GetString(ReciveData);
                    //ReciveSM.Release();
                    ChildSocket.Receive(ReciveData);

                }
                else
                {
                    //ReciveSM.WaitOne();
                    SM2.WaitOne();
                    PacketData += Encoding.Unicode.GetString(ReciveData);
                    //RecivedMessages.Remove(RecivedMessages.First());
                    SM2.Release();
                    PacketData = PacketData.Replace("\0", "");
                    RemainingData -= Recived;
                    //ReciveSM.Release();
                    if (RemainingData == 0)
                    {
                        IsStart = true;
                        if (Type == (short)Packet.PacketType.Goodbay)
                        {
                            ChildSocket.Close();
                            IdentifyResult = "Error";
                        }
                        if (Type == (short)Packet.PacketType.IdentifyResult)
                        {
                            Packet.IdentifyResult Result = Pack.ToPacket<Packet.IdentifyResult>(PacketData);
                            IdentifyResult = Result.State;
                        }

                    }
                }

            }
            catch (Exception E)
            {

            }
        }

        public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 68, 17, 95, 61, 13, 83, 107, 111 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 256;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(32);
                    AES.IV = key.GetBytes(32);
                    AES.Padding = PaddingMode.Zeros;
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(AES.Key, AES.IV), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 68, 17, 95, 61, 13, 83, 107, 111 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 256;
                    AES.Padding = PaddingMode.Zeros;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(32);
                    AES.IV = key.GetBytes(32);
                    //AES.Padding = PaddingMode.None;
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(AES.Key, AES.IV), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        public void SendToServer(string Data)
        {
            //Task.Run(() =>
            //{
            //    
            //});
            try
            {
                byte[] ByteData = Encoding.Unicode.GetBytes(Data);
                ByteData = AES_Encrypt(ByteData, PassBytes);
                int Counter = 0;
                //SendDataSM.WaitOne();
                Counter = ChildSocket.Send(ByteData);
                Thread.Sleep(200);
                while (ByteData.Length != Counter)
                {
                    Counter += ChildSocket.Send(ByteData, Counter, SendBuffer, SocketFlags.None);
                    Thread.Sleep(200);
                }
            }
            catch (SocketException)
            {
                ConnectionIsAlive = false;
                ChildSocket.BeginConnect(IP, Convert.ToInt32(Ports[0]), ConnectEvent, ChildSocket);
            }
            catch (Exception E)
            {

            }

            //SendDataSM.Release();
        }

        public void SendFIleToServer(string Data)
        {
            try
            {
                byte[] ByteData = Encoding.Unicode.GetBytes(Data);
                int Counter = 0;
                ByteData = AES_Encrypt(ByteData, PassBytes);
                Counter = ChildFileSocket.Send(ByteData);
                //while (ByteData.Length != Counter)
                //{
                //    Counter += ChildFileSocket.Send(ByteData, Counter, SendBuffer, SocketFlags.None);
                //}

            }
            catch (SocketException E)
            {
                ConnectionIsAlive = false;
                ChildFileSocket.BeginConnect(IP, Convert.ToInt32(Ports[1]), ConnectFileEvent, ChildSocket);
            }
        }

        private void RecivedOther(IAsyncResult ar)
        {
            try
            {
                Recived = ChildSocket.EndReceive(ar);
                ReciveSM.WaitOne();
                RecivedMessagesIndex.Add(Recived);
                RecivedMessages.Add(Encoding.Unicode.GetString(AES_Decrypt(ReciveData.Take(Recived).ToArray(), PassBytes)));
                //RecivedMessages.Add(Encoding.Unicode.GetString(ReciveData));
                ReciveData = new byte[ReciveData.Length];
                ReciveSM.Release();
                ChildSocket.BeginReceive(ReciveData, 0, ReciveData.Length, SocketFlags.None, RecivedOther, ChildSocket);
                if (RecivedMessages.Count > 0)
                {
                    ActorIsworking = true;
                    Actor();
                }
            }
            catch (SocketException E)
            {
                ConnectionIsAlive = false;
                ChildSocket.Disconnect(true);
                ChildFileSocket.Disconnect(true);
                ResetConnection();
            }
            catch (Exception E)
            {

            }
        }

        public void Actor()
        {
            Task.Run(() =>
            {
                //while ((ActorIsworking == true) || (RecivedMessages.Count > 0))
                {
                    SM1.WaitOne();
                    if (RecivedMessages.Count != 0)
                    {
                        if (true)
                        {
                            //IsStart = false;



                            Packet.MainPacket Data;
                            Packet Pack = new Packet();
                            string IncommingData = "";
                            IncommingData = RecivedMessages.First().Replace("\0", "");
                            RecivedMessages.Remove(RecivedMessages.First());
                            RecivedMessagesIndex.RemoveAt(0);
                            PacketData = "";
                            Data = Pack.ToPacket<Packet.MainPacket>(IncommingData);
                            Packet.ProPacket ProData = new Packet.ProPacket();
                            ProData = Pack.ToPacket<Packet.ProPacket>(Data.PPacket);
                            ParentID = ProData.ID;
                            RemainingData = ProData.TotalSize;
                            Type = ProData.Type;
                            if (Type == (short)Packet.PacketType.Goodbay)
                            {
                                ChildSocket.Close();
                            }
                            try
                            {
                                Form1.ReID = ID;
                                Form1.ReData = Data.Data;
                                Form1.Type = Type;
                                Thread MyThread = Form1.ReTask;
                                MyThread.Start();

                            }
                            catch (Exception E)
                            {

                            }
                        }
                        //else
                        //{
                        //    Packet Pack = new Packet();
                        //    PacketData += RecivedMessages.First().Replace("\0", "");
                        //    RecivedMessages.Remove(RecivedMessages.First());
                        //    string ID = ParentID;
                        //    RemainingData -= RecivedMessagesIndex.First();
                        //    RecivedMessagesIndex.RemoveAt(0);
                        //    IsStart = true;
                        //    
                        //    
                        //    
                        //}
                    }
                    if (RecivedMessages.Count == 0)
                    {
                        ActorIsworking = false;
                    }
                    SM1.Release();
                }
            });
        }

        public void TakeScreenShot()
        {
            Task.Run(() =>
            {
                if (Type == 13)
                {
                    SC = new ScreenShotClass();
                    //System.Timers.Timer Slice = new System.Timers.Timer(38);
                    //ParentTCP = new TcpClient();
                    //ParentTCP.Connect("127.0.0.1", 8803);
                    //NetworkStream ST = ParentTCP.GetStream();
                    byte[] PicData = null;
                    string Pic = SC.FullScreenShot(1, ref PicData);
                    Packet Pack = new Packet();
                    Packet.ScrrenShot SCPack = new Packet.ScrrenShot();
                    SCPack.Picture = Convert.ToBase64String(PicData);
                    SCPack.Start = DateTime.Now;
                    SCPack.Type = (short)Packet.ScrrenShotType.OneTime;
                    SCPack.Header = "";
                    SCPack.End = DateTime.Now;
                    //TcpClient SenderChient = new TcpClient();
                    //SenderChient.Client = ChildFileSocket;
                    //NetworkStream NS = SenderChient.GetStream();
                    //SCPack.Type = (short)Packet.ScrrenShotType.OneTime;
                    //SCPack.Header = "";
                    //SCPack.Start = DateTime.Now;
                    //SCPack.End = SCPack.Start;
                    string Data = Pack.ToString(SCPack);
                    int A = Data.Length;
                    Packet.ProPacket ProData = new Packet.ProPacket();
                    ProData.ID = Form1.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                    ProData.Type = (short)Packet.PacketType.ScrrenShot;
                    ProData.TotalSize = Encoding.Unicode.GetByteCount(Data);
                    string ProDataStr = Pack.ToString(ProData);
                    if (ConnectionIsAlive == true)
                    {
                        SendFileDataSM.WaitOne();
                        //NS.Write(Encoding.Unicode.GetBytes(ProDataStr) , 0, Encoding.Unicode.GetBytes(ProDataStr).Length);
                        SendFIleToServer(ProDataStr);
                        Thread.Sleep(500);
                        //NS.Write(PicData, 0, PicData.Length);
                        SendFIleToServer(Data);
                        SendFileDataSM.Release();
                    }
                    else
                    {
                        DataRow Row = Form1.DS.Tables["MessageLogRemaining"].NewRow();
                        Row["IsFile"] = 1;
                        Row["ProPack"] = ProDataStr;
                        Row["Data"] = Data;
                        Row["ID"] = Form1.DataBaseAgent.ExequteWithCommandScaler("Select NEWID()");
                        Form1.DS.Tables["MessageLogRemaining"].Rows.Add(Row);
                        Form1.DataBaseAgent.InsertData(Form1.DS.Tables["MessageLogRemaining"]);
                    }

                }
                else
                {

                }
            });

        }

        public void ShowtDown()
        {
            ChildSocket.Close();
            ChildFileSocket.Close();
        }

        public void Tester()
        {
            string baseIP = "192.168.";

            //Console.WriteLine("Pinging 255 destinations of D-class in {0}*", baseIP);

            CreatePingers(255);

            PingOptions po = new PingOptions(ttl, true);
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            byte[] data = enc.GetBytes("abababababababababababababababab");

            SpinWait wait = new SpinWait();
            int cnt = 1;

            Stopwatch watch = Stopwatch.StartNew();

            foreach (Ping p in pingers)
            {
                lock (@lock)
                {
                    instances += 1;
                }

                p.SendAsync(string.Concat(baseIP, cnt.ToString()), timeOut, data, po);
                cnt += 1;
            }

            while (instances > 0)
            {
                wait.SpinOnce();
            }

            watch.Stop();

            DestroyPingers();

            //Console.WriteLine("Finished in {0}. Found {1} active IP-addresses.", watch.Elapsed.ToString(), result);
            //Console.ReadKey();

        }

        public void Ping_completed(object s, PingCompletedEventArgs e)
        {
            lock (@lock)
            {
                instances -= 1;
            }

            if (e.Reply.Status == IPStatus.Success)
            {
                FindedIPs.Add(e.Reply.Address);
                Console.WriteLine(string.Concat("Active IP: ", e.Reply.Address.ToString()));
                result += 1;
            }
            else
            {
                //Console.WriteLine(String.Concat("Non-active IP: ", e.Reply.Address.ToString()))
            }
        }

        private void CreatePingers(int cnt)
        {
            for (int i = 1; i <= cnt; i++)
            {
                Ping p = new Ping();
                p.PingCompleted += Ping_completed;
                pingers.Add(p);
            }
        }

        private void DestroyPingers()
        {
            foreach (Ping p in pingers)
            {
                p.PingCompleted -= Ping_completed;
                p.Dispose();
            }

            pingers.Clear();

        }

        public void RealTimeMonitoring(string IP, int Port, int Type)
        {
            Task.Run(() =>
            {
                try
                {
                    RealSM = new Semaphore(1, 1);
                    SC = new ScreenShotClass();
                    System.Timers.Timer Slice = new System.Timers.Timer(25);
                    Slice.Elapsed += Slice_Elapsed;
                    ParentTCP = new TcpClient();
                    string FindIP = "";
                    FindedIPs = new List<IPAddress>();
                    Tester();
                    //IPHostEntry IPA = Dns.Resolve(IP);
                    //foreach (IPAddress var in FindedIPs)
                    //{
                    //    if (("" + var.ToString()) == IP)
                    //    {
                    //        
                    //    }
                    //}
                    ParentTCP.Connect(IPAddress.Parse(IP), 8804);
                    ST = ParentTCP.GetStream();
                    byte[] PicData = null;
                    string Pic = SC.FullScreenShot(1, ref PicData);
                    Slice.Start();
                    byte[] Lenght = Encoding.ASCII.GetBytes(PicData.Length.ToString());
                    RealSM.WaitOne();
                    ST.Write(Lenght, 0, Lenght.Length);
                    Thread.Sleep(35);
                    ST.Write(PicData, 0, PicData.Length);
                    RealSM.Release();
                }
                catch (SocketException E)
                {
                    if (E.SocketErrorCode == SocketError.Shutdown || E.SocketErrorCode == SocketError.NotConnected || E.SocketErrorCode == SocketError.HostDown ||
                        E.SocketErrorCode == SocketError.Disconnecting || E.SocketErrorCode == SocketError.ConnectionRefused)
                    {
                        ParentTCP.Close();
                    }
                }

            });

        }

        public void RealTimeMonitoringWebCam(string IP, int Port, int Type)
        {
            Task.Run(() =>
            {
                try
                {
                    RealWebcamSM = new Semaphore(1, 1);
                    ParentWebCamTCP = new TcpClient();
                    string FindIP = "";
                    FindedIPs = new List<IPAddress>();
                    Tester();
                    //IPHostEntry IPA = Dns.Resolve(IP);
                    foreach (IPAddress var in FindedIPs)
                    {
                        if ((" " + var.ToString()) == IP)
                        {
                            ParentWebCamTCP.Connect(var, 8805);
                        }
                    }
                    STWebCam = ParentWebCamTCP.GetStream();
                    Form1.WebcamAgent.StartWebCam();

                }
                catch (SocketException E)
                {
                    if (E.SocketErrorCode == SocketError.Shutdown || E.SocketErrorCode == SocketError.NotConnected || E.SocketErrorCode == SocketError.HostDown ||
                        E.SocketErrorCode == SocketError.Disconnecting || E.SocketErrorCode == SocketError.ConnectionRefused)
                    {
                        Form1.WebcamAgent.StopWebCam();
                        ParentWebCamTCP.Close();
                    }
                }

            });

        }

        private void Slice_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                System.Timers.Timer Target = sender as System.Timers.Timer;
                Target.Interval = 35;
                byte[] PicData = null;
                string Pic = SC.FullScreenShot(1, ref PicData);
                Target.Start();
                byte[] Lenght = Encoding.ASCII.GetBytes(PicData.Length.ToString());
                RealSM.WaitOne();
                ST.Write(Lenght, 0, Lenght.Length);
                Thread.Sleep(20);
                ST.Write(PicData, 0, PicData.Length);
                RealSM.Release();
            }
            catch (SocketException E)
            {
                if (E.SocketErrorCode == SocketError.Shutdown || E.SocketErrorCode == SocketError.NotConnected || E.SocketErrorCode == SocketError.HostDown ||
                        E.SocketErrorCode == SocketError.Disconnecting || E.SocketErrorCode == SocketError.ConnectionRefused)
                {
                    ParentTCP.Close();
                }
            }

        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Net.Sockets;//Provides a managed implementation of the Windows Sockets (Winsock) interface for developers who need to tightly control access to the network.
using System.Threading;//Provides classes and interfaces that enable multithreaded programming.
using System.Data.SqlClient;
using System.Net;//Provides a simple programming interface for many of the protocols used on networks today.
using System.IO;
using System.Drawing;//Provides access to GDI+ basic graphics functionality.
using System.Windows.Media.Imaging;//Provides types that are used to encode and decode bitmap images.
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using System.Security.Cryptography;//Provides cryptographic services, including secure encoding and decoding of data, as well as many other operations, such as hashing, random number generation, and message authentication. 

namespace UI
{
    public class ConnectionClass
    {
        public Socket ParentSocket;
        DateTime lastTime;
        byte[] RealData;
        byte[] RealwebData;
        string Pic = "";
        string Password = "QEJyaWRsZUFkbWluMDAxMzU3OSNBbGlMYW1lXyFeX14="; // @BridleAdmin0013579#AliLame_!^_^
        byte[] PassBytes;
        int ScreNumber = 0;
        Semaphore RealSM;
        Socket ChildFileSocket;
        List<ParentClass> Parents;
        public event EventHandler<string> LoginEventHandler;
        public bool ConnectionIsAlive = false;
        byte[] ReciveData;
        byte[] ReciveFileData;
        byte[] SendData;
        bool IsStart = true;
        bool IsFileStart = true;
        public string ID;
        int RemainingData;
        int FileRemainingData;
        string PacketData = "";
        string FilePacketData = "";
        short Type;
        short FileType;
        string ProPack;
        string FileProPack;
        string FileRecivedID;
        int SendBuffer;
        public Semaphore SendDataSM;
        ParentClass NewParent;
        Semaphore SendFileDataSM;
        Semaphore ReciveSM;
        Semaphore RealWebSM;
        string IP;
        string[] Ports;
        int SNumber = 0;
        string IdentifyResult;
        List<DataTable> SendTablesList;
        List<string> RecivedMessages;
        List<int> RecivedMessagesIndex;
        List<string> RecivedMessagesFile;
        List<int> RecivedMessagesIndexFile;
        int Recived = 0;
        int RecivedFile = 0;
        bool ActorIsworking = false;
        Semaphore SM1;
        Semaphore SM2;
        Semaphore FileSM0;
        Semaphore FileSM1;
        Semaphore FileSM2;
        bool PicCompelit = false;
        string RecivedID;
        TcpListener Real;
        TcpListener RealWeb;
        TcpClient ChildTCP;
        TcpClient ChildWebTCP;
        public bool RealEnd = false;
        public bool RealwebEnd = false;
        List<byte[]> RealPic;
        List<byte[]> RealwebPic;
        public ConnectionClass(string PortNumber, string IP, int SendBuffer, int NumberOfTables , EventHandler<string> LoginEventHandler)
        {
            this.LoginEventHandler = LoginEventHandler;
            ParentSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Monitor.Enter(ParentSocket);
            Ports = PortNumber.Split(',');
            this.IP = IP;
            SendFileDataSM = new Semaphore(1, 1);
            ReciveData = new byte[SendBuffer];
            ReciveFileData = new byte[2048];
            
            ChildFileSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int Port = Convert.ToInt32(Ports[0]);
            PassBytes = Convert.FromBase64String(Password);
            this.SendBuffer = SendBuffer;
            SendDataSM = new Semaphore(1, 1);
            ReciveSM = new Semaphore(1, 1);
            SM1 = new Semaphore(1, 1);
            SM2 = new Semaphore(1, 1);
            FileSM0 = new Semaphore(1, 1);
            FileSM1 = new Semaphore(1, 1);
            FileSM2 = new Semaphore(1, 1);
            Parents = new List<ParentClass>();
            SendTablesList = new List<DataTable>();
            for (int i = 0; i < NumberOfTables; i++)
            {
                DataTable Temp = new DataTable();
                SendTablesList.Add(Temp);
            }
            ParentSocket.Connect(IP, Port);
            ConnectEvent();
        }
        public ConnectionClass(string PortNumber, string IP, int SendBuffer, int NumberOfTables, bool RegisterData)
        {
            ParentSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Monitor.Enter(ParentSocket);
            Ports = PortNumber.Split(',');
            this.IP = IP;
            ReciveData = new byte[SendBuffer];
            ReciveFileData = new byte[2048];
            int Port = Convert.ToInt32(Ports[0]);
            PassBytes = Encoding.Unicode.GetBytes(Password);
            this.SendBuffer = SendBuffer;
            ParentSocket.Connect(IP, Port);
            
        }

        public void SendToServerRegister(string Data, AsyncCallback RegisterDataRecive , ref byte[] RegisterData)
        {
            
            SendToServer(Data);
            ParentSocket.BeginReceive(RegisterData, 0, 2048, SocketFlags.None, RegisterDataRecive, ParentSocket);
        }
        private void ConnectEvent()
        {
            try
            {
                Monitor.Enter(ParentSocket);
                Packet Pack = new Packet();
                
                ReciveSM.WaitOne();
                if (MainWindow.DS.Tables["Data"].Rows[5]["DataContent"].ToString() == "0")
                {
                    Packet.ParentSingup Sing = new Packet.ParentSingup();
                    Sing.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                    Sing.Password = MainWindow.DS.Tables["Data"].Rows[3]["DataContent"].ToString();
                    Sing.Mac = "MAC";
                    Packet.PSProPacket ProSing = new Packet.PSProPacket();
                    ProSing.ID = Sing.ID;
                    ProSing.Type = (short)Packet.PacketType.ParentSingup;
                    ProSing.Reciver = "";
                    string Data = Pack.ToString(Sing);
                    ProSing.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                    string ProData = Pack.ToString(ProSing);
                    Packet.MainPacket MainData = new Packet.MainPacket();
                    MainData.Data = Data;
                    MainData.PPacket = ProData;
                    string MainStrData = Pack.ToString<Packet.MainPacket>(MainData);
                    SendToServer(MainStrData);
                    //SendToServer(Data);
                    ParentSocket.Receive(ReciveData);
                    string Result = Encoding.Unicode.GetString(ReciveData);
                    Result = Result.Replace("\0", "");
                    if ((Result == "True") || (Result == "OK"))
                    {
                        
                        MainWindow.DS.Tables["Data"].Rows[5]["DataContent"] = 1;
                        MainWindow.DataBaseAgent.UpdateData(MainWindow.DS.Tables["Data"]);
                    }
                }
                SendIdentifyData(MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString(), MainWindow.DS.Tables["Data"].Rows[4]["DataContent"].ToString(), MainWindow.DS.Tables["Data"].Rows[3]["DataContent"].ToString());
                int Number = ParentSocket.Receive(ReciveData);
                byte[] TempData = new byte[Number];
                IdentifyResult = Encoding.Unicode.GetString(AES_Decrypt(ReciveData.Take(Number).ToArray(), PassBytes));
                IdentifyResult = IdentifyResult.Replace("\0", "");
                string[] REDA = IdentifyResult.Split('$');
                if (REDA[0] == "True")
                {
                    ConnectionIsAlive = true;
                    if (REDA.Length > 1)
                    {
                        MainWindow.DS.Tables["Data"].Rows[4]["DataContent"] = "";
                        foreach (string var in REDA[1].Split(','))
                        {
                            MainWindow.DS.Tables["Data"].Rows[4]["DataContent"] += var + ",";
                        }
                        MainWindow.DataBaseAgent.UpdateData(MainWindow.DS.Tables["Data"]);
                    }
                    PreapringRecive();
                    LoginEventHandler(this, "True");
                }
                else
                {
                    LoginEventHandler(this, "False");
                    //Cange User Name 
                }
                ReciveSM.Release();


            }
            catch (Exception e)
            {
                ConnectionIsAlive = false;
                LoginEventHandler(this, "Connection");
                if (ParentSocket.Connected != true)
                {
                    ParentSocket.Disconnect(true);
                    ParentSocket.BeginConnect(IP, Convert.ToInt32(Ports[0]), ConnectEvent, ParentSocket);
                }

            }
        }
        private void ConnectEvent(IAsyncResult ar)
        {
            try
            {
                Packet Pack = new Packet();
                Socket ConnectedSocket = (Socket)ar.AsyncState;
                ConnectedSocket.EndConnect(ar);
                ReciveSM.WaitOne();
                if (MainWindow.DS.Tables["Data"].Rows[5]["DataContent"].ToString() == "0")
                {
                    Packet.ParentSingup Sing = new Packet.ParentSingup();
                    Sing.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                    Sing.Password = MainWindow.DS.Tables["Data"].Rows[3]["DataContent"].ToString();
                    Sing.Mac = "MAC";
                    Packet.PSProPacket ProSing = new Packet.PSProPacket();
                    ProSing.ID = Sing.ID;
                    ProSing.Type = (short)Packet.PacketType.ParentSingup;
                    ProSing.Reciver = "";
                    string Data = Pack.ToString(Sing);
                    ProSing.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                    string ProData = Pack.ToString(ProSing);
                    Packet.MainPacket MainData = new Packet.MainPacket();
                    MainData.Data = Data;
                    MainData.PPacket = ProData;
                    string MainStrData = Pack.ToString<Packet.MainPacket>(MainData);
                    SendToServer(MainStrData);
                    //SendToServer(Data);
                    ParentSocket.Receive(ReciveData);
                    string Result = Encoding.Unicode.GetString(ReciveData);
                    Result = Result.Replace("\0", "");
                    if ((Result == "True") || (Result == "OK"))
                    {
                        MainWindow.DS.Tables["Data"].Rows[5]["DataContent"] = 1;
                        MainWindow.DataBaseAgent.UpdateData(MainWindow.DS.Tables["Data"]);
                    }
                }
                SendIdentifyData(MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString(), MainWindow.DS.Tables["Data"].Rows[4]["DataContent"].ToString(), MainWindow.DS.Tables["Data"].Rows[3]["DataContent"].ToString());
                ParentSocket.Receive(ReciveData);
                IdentifyResult = Encoding.Unicode.GetString(ReciveData);
                IdentifyResult = IdentifyResult.Replace("\0", "");
                string[] REDA = IdentifyResult.Split('$');
                if (REDA[0] == "True")
                {
                    ConnectionIsAlive = true;
                    if(REDA.Length > 1)
                    {
                        MainWindow.DS.Tables["Data"].Rows[4]["DataContent"] = "";
                        foreach (string var in REDA[1].Split(','))
                        {
                            MainWindow.DS.Tables["Data"].Rows[4]["DataContent"] += var + ",";
                        }
                        MainWindow.DataBaseAgent.UpdateData(MainWindow.DS.Tables["Data"]);
                    }
                    try
                    {
                    }
                    catch(Exception E)
                    {
                    }
                    ReciveData = new byte[ReciveData.Length];
                    
                    
                    PreapringRecive();

                }
                else
                {
                    //Cange User Name 
                }
                ReciveSM.Release();


            }
            catch (Exception e)
            {
                ConnectionIsAlive = false;
                if (ParentSocket.Connected != true)
                {
                    ParentSocket.Disconnect(true);
                    ParentSocket.BeginConnect(IP, Convert.ToInt32(Ports[0]), ConnectEvent, ParentSocket);
                }

            }
        }

        private void ConnectFileEvent(IAsyncResult ar)
        {
            ChildFileSocket.EndConnect(ar);
            SendIdentifyFileData(MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString(), MainWindow.DS.Tables["Data"].Rows[4]["DataContent"].ToString(), MainWindow.DS.Tables["Data"].Rows[3]["DataContent"].ToString());
            PreapringReciveFile();
        }

        public void SendIdentifyData(string ID, string Children, string Password)
        {
            Packet Pack = new Packet();
            Packet.ParentIdentify Identify = new Packet.ParentIdentify();
            Identify.ID = ID;
            this.ID = ID;
            Identify.Childrens = Children;
            Identify.Password = Password;
            Packet.PSProPacket ProPack = new Packet.PSProPacket();
            ProPack.Type = (short)Packet.PacketType.Identify;
            string Data = Pack.ToString(Identify);
            ProPack.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
            ProPack.ID = ID;
            ProPack.Reciver = "";
            Packet.MainPacket MainData = new Packet.MainPacket();
            MainData.Data = Data;
            MainData.PPacket = Pack.ToString(ProPack);
            string MainStrData = Pack.ToString<Packet.MainPacket>(MainData);
            SendDataSM.WaitOne();
            SendToServer(MainStrData);
            SendDataSM.Release();
        }

        public void SendIdentifyFileData(string ID, string Children, string Password)
        {
            Packet Pack = new Packet();
            Packet.ParentIdentify Identify = new Packet.ParentIdentify();
            Identify.ID = ID;
            this.ID = ID;
            Identify.Childrens = Children;
            Identify.Password = Password;
            Packet.PSProPacket ProPack = new Packet.PSProPacket();
            ProPack.Type = (short)Packet.PacketType.Identify;
            string Data = Pack.ToString(Identify);
            ProPack.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
            ProPack.ID = ID;
            ProPack.Reciver = "";
            SendFileDataSM.WaitOne();
            SendFileToServer(Pack.ToString(ProPack));
            SendFileDataSM.Release();
        }

        public void PreapringRecive()
        {
            try
            {
                ParentSocket.BeginReceive(ReciveData, 0, ReciveData.Length, SocketFlags.None, RecivedOther, ParentSocket);
                RecivedMessages = new List<string>();
                RecivedMessagesIndex = new List<int>();
            }
            catch (SocketException)
            {
                ParentSocket.BeginConnect(IP, Convert.ToInt32(Ports[0]), ConnectEvent, ParentSocket);
            }

        }

        public void PreapringReciveFile()
        {
            try
            {
                ChildFileSocket.BeginReceive(ReciveFileData, 0, ReciveFileData.Length, SocketFlags.None, ReciveFile, ChildFileSocket);
                RecivedMessagesFile = new List<string>();
                RecivedMessagesIndexFile = new List<int>();
            }
            catch (SocketException)
            {
                ChildFileSocket.BeginConnect(IP, Convert.ToInt32(MainWindow.DS.Tables["Data"].Rows[11]["DataContent"]), ConnectFileEvent, ChildFileSocket);
            }

        }

        public void FileActor()
        {
            Task.Run(() =>
            {
                Packet P1 = new Packet();
                {
                    FileSM0.WaitOne();
                    if (RecivedMessagesFile.Count != 0)
                    {
                        if (IsFileStart == true)
                        {
                            IsFileStart = false;
                            Packet.ProPacket Data;
                            FileProPack = RecivedMessagesFile.First().Replace("\0", "");
                            RecivedMessagesFile.Remove(RecivedMessagesFile.First());
                            RecivedMessagesIndexFile.RemoveAt(0);
                            Data = P1.ToPacket<Packet.ProPacket>(FileProPack);
                            FilePacketData = "";
                            FileRecivedID = Data.ID;
                            FileRemainingData = Data.TotalSize;
                            ReciveFileData = new byte[FileRemainingData];
                            ChildFileSocket.Receive(ReciveFileData, 0, FileRemainingData, SocketFlags.None);
                            FileType = Data.Type;
                            FilePacketData = Encoding.Unicode.GetString(ReciveFileData);
                            if ((RecivedMessagesFile.Count == 0))
                            {
                                IsFileStart = true;
                                switch (FileType)
                                {
                                    default:
                                        {
                                            FileSM2.WaitOne();
                                            int a = FilePacketData.Length;
                                            MessageRecived(FilePacketData, FileType, FileRecivedID);
                                            FileSM2.Release();
                                        }; break;
                                }
                            }
                            else
                            {
                            }

                        }
                        else
                        {
                        }
                    }
                    FileSM0.Release();
                }
            });

        }

        private void ReciveFile(IAsyncResult ar)
        {
            try
            {
                RecivedFile = ChildFileSocket.EndReceive(ar);
                RecivedMessagesFile.Add(Encoding.Unicode.GetString(AES_Decrypt(ReciveFileData.Take(RecivedFile).ToArray(), PassBytes)));
                RecivedMessagesIndexFile.Add(RecivedFile);
                ReciveFileData = new byte[ReciveFileData.Length];
                if(RecivedMessagesFile.Count > 0)
                {
                    FileActor();
                }
            }
            catch (SocketException E)
            {
                if (ChildFileSocket.Connected == false)
                {
                    ChildFileSocket.Close();
                    
                }
            }
            catch (Exception e)
            {

            }
        }

        private void RecivedIdentify()
        {
            Recived = ParentSocket.Receive(ReciveData);
            Packet Pack = new Packet();
            try
            {
                ReciveSM.WaitOne();
                string Message = Encoding.Unicode.GetString(ReciveData);
                ReciveData = new byte[ReciveData.Length];
                ReciveSM.Release();
                if (IsStart == true)
                {
                    IsStart = false;
                    Packet.ProPacket Data;
                    SM1.WaitOne();
                    SM2.WaitOne();
                    ProPack = Message.Replace("\0", "");
                    PacketData = "";
                    Data = Pack.ToPacket<Packet.ProPacket>(ProPack);
                    SM2.Release();
                    SM1.Release();
                    RemainingData = Data.TotalSize;
                    Type = Data.Type;
                    ParentSocket.Receive(ReciveData);

                }
                else
                {
                    SM2.WaitOne();
                    PacketData += Encoding.Unicode.GetString(ReciveData);
                    SM2.Release();
                    PacketData = PacketData.Replace("\0", "");
                    RemainingData -= Recived;
                    if (RemainingData == 0)
                    {
                        IsStart = true;
                        if (Type == (short)Packet.PacketType.Goodbay)
                        {
                            ParentSocket.Close();
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
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 , 68,17 ,95,61,13,83 ,107,111 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 256;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Padding = PaddingMode.Zeros;
                    AES.Mode = CipherMode.CBC;
                    AES.Key = key.GetBytes(32);
                    AES.IV = key.GetBytes(32);
                    //AES.Key = key.GetBytes(AES.KeySize / 8);
                    //AES.IV = Encoding.ASCII.GetBytes("@1B2c3D4e5F6g7H8");

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(AES.Key, AES.IV), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.FlushFinalBlock();
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

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(32);
                    AES.IV = key.GetBytes(32);
                    AES.Padding = PaddingMode.Zeros;
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
            try
            {
                 
                byte[] ByteData = Encoding.Unicode.GetBytes(Data);
                ByteData = AES_Encrypt(ByteData, PassBytes);
                int Counter = 0;
                Counter = ParentSocket.Send(ByteData);
                while (ByteData.Length != Counter)
                {
                    Counter += ParentSocket.Send(ByteData, Counter, SendBuffer, SocketFlags.None);
                }
            }
            catch (SocketException)
            {
                ConnectionIsAlive = false;
                ParentSocket.BeginConnect(IP, Convert.ToInt32(Ports[0]), ConnectEvent, ParentSocket);
            }
        }

        public void SendFileToServer(string Data)
        {
            try
            {
                byte[] ByteData = Encoding.Unicode.GetBytes(Data);
                int Counter = 0;
                Counter = ChildFileSocket.Send(ByteData);
                while (ByteData.Length != Counter)
                {
                    Counter += ChildFileSocket.Send(ByteData, Counter, SendBuffer, SocketFlags.None);
                }
            }
            catch (SocketException E)
            {
                ConnectionIsAlive = false;
                ChildFileSocket.BeginConnect(IP, Convert.ToInt32(Ports[0]), ConnectEvent, ChildFileSocket);
            }
        }


        public void ResetConnection()
        {
            ParentSocket.BeginConnect(IP, Convert.ToInt32(Ports[0]), ConnectEvent, ParentSocket);
        }

        private void RecivedOther(IAsyncResult ar)
        {
            try
            {
                Recived = ParentSocket.EndReceive(ar);
                ReciveSM.WaitOne();
                RecivedMessagesIndex.Add(Recived);
                RecivedMessages.Add(Encoding.Unicode.GetString(AES_Decrypt(ReciveData.Take(Recived).ToArray(), PassBytes)));
                ReciveData = new byte[ReciveData.Length];
                ReciveSM.Release();
                ParentSocket.BeginReceive(ReciveData, 0, ReciveData.Length, SocketFlags.None, RecivedOther, ParentSocket);
                if(RecivedMessages.Count > 0)
                {
                    Actor();
                }
            }
            catch (SocketException E)
            {
                ConnectionIsAlive = false;
                ParentSocket.Disconnect(true);
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
                {
                    SM1.WaitOne();
                    if (RecivedMessages.Count != 0)
                    {

                        Packet.MainPacket Data;
                        Packet Pack = new Packet();
                        ProPack = RecivedMessages.First().Replace("\0", "");
                        RecivedMessages.Remove(RecivedMessages.First());
                        RecivedMessagesIndex.RemoveAt(0);
                        PacketData = "";
                        Data = Pack.ToPacket<Packet.MainPacket>(ProPack);
                        Packet.ProPacket ProData = new Packet.ProPacket();
                        ProData = Pack.ToPacket<Packet.ProPacket>(Data.PPacket);
                        RecivedID = ProData.ID;
                        RemainingData = ProData.TotalSize;
                        Type = ProData.Type;
                        string ID = RecivedID;
                        IsStart = true;
                        if (Type == (short)Packet.PacketType.Goodbay)
                        {
                            ParentSocket.Close();
                        }
                        MessageRecived(Data.Data, Type, ID);
                    }
                    if (RecivedMessages.Count == 0)
                    {
                        ActorIsworking = false;
                    }
                    SM1.Release();
                }
            });
        }

        private void MessageRecived(string Data, int Type, string ID)
        {
            switch (Type)
            {
                case (short)Packet.PacketType.IdentifyResult:
                    {
                        Packet Pack = new Packet();
                        Packet.IdentifyResult Pac = Pack.ToPacket<Packet.IdentifyResult>(Data);
                        if (Pac.State != "OK")
                        {
                            ParentSocket.Close();
                        }
                        else
                        {

                        }
                    }; break;
                case (short)Packet.PacketType.Location:
                    {
                        Packet Pack = new Packet();
                        Main.LocationSemapore.WaitOne();
                        MainWindow.DataBaseAgent.SelectData("Location", ref MainWindow.DS, "*", "Location", Main.ChildUser, "ChildID");
                        DataRow Row = MainWindow.DS.Tables["Location"].NewRow();
                        Packet.Location Loc = Pack.ToPacket<Packet.Location>(Data);
                        Row["ChildID"] = ID;
                        Row["Longitude"] = Loc.Longitude;
                        Row["Latitude"] = Loc.Latitude;
                        Row["ReciveDate"] = DateTime.Now;
                        Row["Date"] = Loc.Time;
                        MainWindow.DS.Tables["Location"].Rows.Add(Row);
                        MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["Location"]);
                        MainWindow.DS.Tables.Remove("Location");
                        Main.LocationSemapore.Release();
                        if (Main.page != 3)
                        {
                            Main.NewLogs[1]++;
                        }
                    }; break;
                case (short)Packet.PacketType.URL:
                    {
                        Packet Pack = new Packet();
                        Packet.URL Pac = Pack.ToPacket<Packet.URL>(Data);
                        Main.HistoryURLSemaphore.WaitOne();
                        MainWindow.DataBaseAgent.SelectData("HistoryURL", ref MainWindow.DS, "*", "HistoryURL", Main.ChildUser, "ChildID");
                        DataRow Row = MainWindow.DS.Tables["HistoryURL"].NewRow();
                        Row["ID"] = MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select NEWID()");
                        Row["URL"] = Pac.Address;
                        Row["Browser"] = Pac.Browser;
                        Row["Date"] = Pac.Time;
                        Row["ChildID"] = ID;
                        string Target = Pac.Address;
                        if(Target.Length > 0)
                        {
                            Target = Target.Substring(0, Target.IndexOf('/'));
                        }
                        
                        MainWindow.DataBaseAgent.SelectData("URLCategury", ref MainWindow.DS, "*", "URLCategury", Main.ChildUser, "ChildID");
                        foreach (DataRow CatRow in MainWindow.DS.Tables["URLCategury"].Rows)
                        {
                            if (CatRow["URLs"].ToString().Contains(Target) == true)
                            {
                                Row["Category"] = CatRow["Name"];
                            }
                        }
                        MainWindow.DS.Tables["HistoryURL"].Rows.Add(Row);
                        MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["HistoryURL"]);
                        MainWindow.DS.Tables.Remove("HistoryURL");
                        Main.HistoryURLSemaphore.Release();
                        if (Main.page != 5)
                        {
                            Main.NewLogs[0]++;
                        }

                    }; break;
                case (short)Packet.PacketType.AppsLog:
                    {
                        Packet Pack = new Packet();
                        MainWindow.DataBaseAgent.SelectData("Process", ref MainWindow.DS, "*", "Process", Main.ChildUser, "ChildID");
                        Packet.AppsLog Pac = Pack.ToPacket<Packet.AppsLog>(Data);

                        string[] ProcData = Pac.Name.Split('-');

                        if (Pac.Type == (short)Packet.AppsLogType.Start)
                        {
                            DataRow Row = MainWindow.DS.Tables["Process"].NewRow();
                            Row["StartTime"] = ProcData[4];
                            Row["ProcessName"] = ProcData[0];
                            Row["ProcessId"] = ProcData[2];
                            Row["ExecutablePath"] = ProcData[1];
                            Row["OSName"] = ProcData[3].Split('|')[0];
                            //Row["Type"] = Pac.Type;
                            Row["ID"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                            Row["ChildID"] = ID;
                            MainWindow.DS.Tables["Process"].Rows.Add(Row);
                            MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["Process"]);
                            MainWindow.DS.Tables.Remove("Process");
                        }
                        if (Pac.Type == (short)Packet.AppsLogType.End)
                        {
                            MainWindow.DataBaseAgent.ExequteWithCommand("Update  Process Set EndTime ='" + Pac.Time + "' where ProcessName='" + ProcData[0] + "' AND ProcessId ='" +
                                ProcData[2] + "' AND StartTime='" + ProcData[4] + "'");
                        }

                        Task.Run(() =>
                        {
                            Packet.AppsLog TPac = Pac;
                            if (TPac.Type == (short)Packet.AppsLogType.Start)
                            {
                                Main.RunningAppsSemaphore.WaitOne();
                                MainWindow.DataBaseAgent.SelectData("RunningApps", ref MainWindow.DS, "*", "RunningApps", Main.ChildUser, "ChildID");
                                DataRow TRow = MainWindow.DS.Tables["RunningApps"].NewRow();
                                TRow["Name"] = TPac.Name;
                                TRow["StartTime"] = TPac.Time;
                                TRow["ChildID"] = ID;
                                MainWindow.DS.Tables["RunningApps"].Rows.Add(TRow);
                                MainWindow.DataBaseAgent.InsertData((MainWindow.DS.Tables["RunningApps"]));
                                MainWindow.DS.Tables.Remove("RunningApps");
                                Main.RunningAppsSemaphore.Release();
                            }
                            if (TPac.Type == (short)Packet.AppsLogType.End)
                            {
                                DataSet Tempds = new DataSet();
                                Main.RunningAppsSemaphore.WaitOne();
                                MainWindow.DataBaseAgent.ExequteWithCommand("Delete From RunningApps where Name ='" + TPac.Name + "' and ChildID ='" + Main.ChildUser + "'");
                                Main.RunningAppsSemaphore.Release();
                            }

                        });
                        Task.Run(() =>
                        {
                            string OS = ProcData[3].Split('|')[0];
                            string Date = Pac.Time;
                            DateTime Time1 = Convert.ToDateTime(Pac.Time);
                            lastTime = Time1;
                            double Between = (DateTime.Now - Time1).TotalHours;
                            if ((Between < 24) && (lastTime < Time1))
                            {
                                lastTime = Time1;
                                Main.OsTypeTb = OS;
                                Main.ValidDataTb = Date;
                            }
                        });
                        if (Main.page != 4)
                        {
                            Main.NewLogs[8]++;
                        }
                    }; break;
                case (short)Packet.PacketType.ScrrenShot:
                    {
                        Packet Pack = new Packet();
                        Packet.ScrrenShot Pac = Pack.ToPacket<Packet.ScrrenShot>(Data);
                        Main.ScreenShotSemaphore.WaitOne();
                        MainWindow.DataBaseAgent.SelectData("ScreenShot", ref MainWindow.DS, "*", "ScreenShot", Main.ChildUser, "ChildID");
                        DataRow Row = MainWindow.DS.Tables["ScreenShot"].NewRow();
                        MemoryStream ms;
                        byte[] PicData = Convert.FromBase64String(Data.Split(',')[0]);
                        ms = new MemoryStream(PicData);
                        Row["Picture"] = PicData;
                        Row["Date"] = Pac.Start.ToString();
                        Row["ChildID"] = ID;
                        Row["ID"] = Pac.End.ToString();
                        MainWindow.DS.Tables["ScreenShot"].Rows.Add(Row);
                        MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["ScreenShot"]);
                        Bitmap BitImage = new Bitmap(ms);
                        BitImage.Save(System.AppDomain.CurrentDomain.BaseDirectory + ScreNumber + "Shot.jpeg");
                        Main.ScreenShotImage.Dispatcher.Invoke(() =>
                        {
                            var MyBrush = new ImageBrush();
                            BitmapImage BImage = new BitmapImage();
                            BImage.BeginInit();
                            BImage.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + ScreNumber + "Shot.jpeg");
                            BImage.EndInit();
                            Main.ScreenShotImage.Source = BImage;
                        });
                        ScreNumber++;
                        if (Main.page != 6)
                        {
                            Main.NewLogs[3]++;
                        }
                        Main.ScreenShotSemaphore.Release();
                    }; break;
                case (short)Packet.PacketType.SystemStart_Down:
                    {
                        Packet Pack = new Packet();
                        Packet.SystemStatus Pac = Pack.ToPacket<Packet.SystemStatus>(Data);
                        DataRow Row = MainWindow.DS.Tables["SystemLog"].NewRow();
                        Row["ID"] = MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select NEWID()");
                        Row["Type"] = Pac.Type;
                        MainWindow.DS.Tables["SystemLog"].Rows.Add(Row);
                        MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["SystemLog"]);

                    }; break;
                case (short)Packet.PacketType.NetworkStatusNow:
                    {
                        Packet Pack = new Packet();
                        Packet.NetworkStatus Pac = Pack.ToPacket<Packet.NetworkStatus>(Data);
                        switch (Pac.Type)
                        {
                            case (short)Packet.NetworkStatusType.Internet:
                                {
                                    Main.NetworkSemaphore.WaitOne();
                                    if (MainWindow.DS.Tables.Contains("Network") == false)
                                    {
                                        MainWindow.DataBaseAgent.SelectData("Network", ref MainWindow.DS, "*", "Network", Main.ChildUser, "ChildID");
                                    }
                                    DataRow Row = MainWindow.DS.Tables["Network"].NewRow();
                                    string[] Datas = Pac.Data.Split('#');
                                    string[] IPs = Datas[3].Split('$');
                                    Row["ChildID"] = ID;
                                    Row["DivaceName"] = IPs[0];
                                    Row["ConnectionInterface"] = Datas[1];
                                    Row["ModemName"] = Datas[0];
                                    Row["Status"] = "Connect to internet";
                                    Row["IPv4"] = IPs[2];
                                    Row["IPv6"] = IPs[1];
                                    Row["ID"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                                    MainWindow.DS.Tables["Network"].Rows.Add(Row);
                                    MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["Network"]);
                                    MainWindow.DS.Tables.Remove("Network");
                                    Main.NetworkSemaphore.Release();
                                }; break;
                            case (short)Packet.NetworkStatusType.Connected:
                                {
                                    Main.NetworkAdaptorSemaphor.WaitOne();
                                    MainWindow.DataBaseAgent.SelectData("NetworkAdaptor", ref MainWindow.DS, "*", "NetworkAdaptor", Main.ChildUser, "ChildID");
                                    DataRow Row = MainWindow.DS.Tables["NetworkAdaptor"].NewRow();
                                    string[] Datas = Pac.Data.Split('#');
                                    Row["ChildID"] = ID;
                                    Row["DeviceName"] = Datas[0];
                                    Row["InterfaceName"] = Datas[1];
                                    Row["Status"] = "Enable";
                                    Row["Date"] = Pac.Time;
                                    Row["ID"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                                    int ContFind = (int)MainWindow.DataBaseAgent.ExequteWithCommandScaler("select Count(*) From NetworkAdaptor where DeviceName ='" + Datas[0] + "' And ChildID ='" + Main.ChildUser + "'");
                                    if (ContFind != 0)
                                    {

                                        MainWindow.DS.Tables["NetworkAdaptor"].Rows.Add(Row);
                                        MainWindow.DataBaseAgent.ExequteWithCommand("UpDate  NetworkAdaptor Set Status = 'Enable' , Date = '" + Pac.Time + "' where DeviceName ='" + Datas[0] + "' And ChildID ='" + Main.ChildUser + "'");
                                        Main.NetAdaptor.Interval = 1000;
                                        Main.NetAdaptor.Start();
                                        MainWindow.DS.Tables.Remove("NetworkAdaptor");
                                        DateTime Time1 = Convert.ToDateTime(Pac.Time);
                                        lastTime = Time1;
                                        double Between = (DateTime.Now - Time1).TotalHours;
                                        if ((Between < 24) && (lastTime < Time1) && (Datas[0] == "Wi-Fi"))
                                        {
                                            lastTime = Time1;
                                            Main.WifiSattusTb = "Enable";


                                        }
                                    }
                                    else
                                    {
                                        MainWindow.DS.Tables["NetworkAdaptor"].Rows.Add(Row);
                                        MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["NetworkAdaptor"]);
                                    }
                                    if (MainWindow.DS.Tables.Contains("NetworkAdaptor"))
                                    {
                                        MainWindow.DS.Tables.Remove("NetworkAdaptor");
                                    }
                                    Main.NetworkAdaptorSemaphor.Release();
                                }; break;
                            case (short)Packet.NetworkStatusType.DisConnected:
                                {
                                    Main.NetworkAdaptorSemaphor.WaitOne();
                                    MainWindow.DataBaseAgent.SelectData("NetworkAdaptor", ref MainWindow.DS, "*", "NetworkAdaptor", Main.ChildUser, "ChildID");
                                    DataRow Row = MainWindow.DS.Tables["NetworkAdaptor"].NewRow();
                                    string[] Datas = Pac.Data.Split('#');
                                    Row["ChildID"] = ID;
                                    Row["DeviceName"] = Datas[0];
                                    Row["InterfaceName"] = Datas[1];
                                    Row["Status"] = "Disable";
                                    Row["ID"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                                    MainWindow.DataBaseAgent.ExequteWithCommand("UpDate  NetworkAdaptor Set Status = Disable , Date =" + Pac.Time + "where DeviceName ='" + Datas[0] + "'And ChildID ='" + Main.ChildUser + "'");
                                    MainWindow.DS.Tables.Remove("NetworkAdaptor");
                                    Main.NetworkAdaptorSemaphor.Release();
                                    Main.NetAdaptor.Interval = 1000;
                                    Main.NetAdaptor.Start();
                                    DateTime Time1 = Convert.ToDateTime(Pac.Time);
                                    lastTime = Time1;
                                    double Between = (DateTime.Now - Time1).TotalHours;
                                    if ((Between < 24) && (lastTime < Time1) && (Datas[0] == "Wi-Fi"))
                                    {
                                        lastTime = Time1;
                                        Main.WifiSattusTb = "Disable";


                                    }

                                }; break;
                            case (short)Packet.NetworkStatusType.NoInternet:
                                {
                                }; break;
                            case (short)Packet.NetworkStatusType.VPNOn:
                                {
                                    Main.VPNSemaphore.WaitOne();
                                    if (MainWindow.DS.Tables.Contains("VPN") == false)
                                    {
                                        MainWindow.DataBaseAgent.SelectData("VPN", ref MainWindow.DS, "*", "VPN", Main.ChildUser, "ChildID");
                                    }

                                    DataRow Row = MainWindow.DS.Tables["VPN"].NewRow();
                                    string[] Datas = Pac.Data.Split('#');
                                    Row["ChildID"] = ID;
                                    Row["ID"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                                    Row["Status"] = "Start Use VPN";
                                    Row["StartVPN"] = Pac.Time;
                                    MainWindow.DS.Tables["VPN"].Rows.Add(Row);
                                    MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["VPN"]);
                                    MainWindow.DS.Tables.Remove("VPN");
                                    Main.VPNSemaphore.Release();
                                    if (Main.page != 7)
                                    {
                                        Main.NewLogs[4]++;
                                    }
                                }; break;
                            case (short)Packet.NetworkStatusType.VpnOff:
                                {
                                    Main.VPNSemaphore.WaitOne();
                                    if (MainWindow.DS.Tables.Contains("VPN") == false)
                                    {
                                        MainWindow.DataBaseAgent.SelectData("VPN", ref MainWindow.DS, "*", "VPN", Main.ChildUser, "ChildID");
                                    }
                                    DataRow Row = MainWindow.DS.Tables["VPN"].NewRow();
                                    string[] Datas = Pac.Data.Split('#');
                                    Row["ChildID"] = ID;
                                    Row["ID"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                                    Row["Status"] = "End Use VPN";
                                    Row["StartVPN"] = Pac.Time;
                                    MainWindow.DS.Tables["VPN"].Rows.Add(Row);
                                    MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["VPN"]);
                                    MainWindow.DS.Tables.Remove("VPN");
                                    Main.VPNSemaphore.Release();
                                    if (Main.page != 7)
                                    {
                                        Main.NewLogs[4]++;
                                    }
                                }; break;
                        }


                    }; break;
                case (short)Packet.PacketType.InstalledApps:
                    {
                        Packet Pack = new Packet();
                        Packet.InstalledApps Pac = Pack.ToPacket<Packet.InstalledApps>(Data);
                        Main.InstalledAppsSemaphore.WaitOne();
                        MainWindow.DataBaseAgent.SelectData("InstalledApps", ref MainWindow.DS, "*", "InstalledApps", Main.ChildUser, "ChildID");
                        DataRow Row = MainWindow.DS.Tables["InstalledApps"].NewRow();
                        string[] Names = Pac.Name.Split('$');
                        Row["AppID"] = Names[1];
                        Row["DisplayName"] = Names[0];
                        Row["InstallDate"] = Pac.InstallDate;
                        Row["DisplayVersion"] = Pac.Version;
                        Row["Publisher"] = Pac.Publisher;
                        Row["DisplayIcon"] = Pac.AppIcon;
                        Row["ChildID"] = ID;
                        Row["ReciveDate"] = DateTime.Now;
                        MainWindow.DS.Tables["InstalledApps"].Rows.Add(Row);
                        MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["InstalledApps"]);
                        MainWindow.DS.Tables.Remove("InstalledApps");
                        Main.InstalledAppsSemaphore.Release();
                        if (Main.page != 1)
                        {
                            Main.NewLogs[2]++;
                        }
                    }; break;
                case (short)Packet.PacketType.UnInstalledApps:
                    {
                        Packet Pack = new Packet();
                        Packet.UnInstalledApps Pac = Pack.ToPacket<Packet.UnInstalledApps>(Data);
                        Main.InstalledAppsSemaphore.WaitOne();
                        MainWindow.DataBaseAgent.SelectData("InstalledApps", ref MainWindow.DS, "*", "InstalledApps", Main.ChildUser, "ChildID");
                        DataRow Row = MainWindow.DS.Tables["InstalledApps"].NewRow();
                        Row["Name"] = Pac.Name;
                        Row["UnInstallDate"] = Pac.UnInstallDate;
                        MainWindow.DS.Tables["InstalledApps"].Rows.Remove(MainWindow.DS.Tables["InstalledApps"].Rows.Find(Row["Name"]));
                        MainWindow.DataBaseAgent.UpdateData(MainWindow.DS.Tables["InstalledApps"]);
                        MainWindow.DS.Tables.Remove("InstalledApps");
                        Main.InstalledAppsSemaphore.Release();
                        if (Main.page != 1)
                        {
                            Main.NewLogs[2]++;
                        }
                    }; break;
                case (short)Packet.PacketType.Keylogger:
                    {
                        Packet Pack = new Packet();
                        Packet.KeyLogger KeyPack = new Packet.KeyLogger();
                        KeyPack = Pack.ToPacket<Packet.KeyLogger>(Data);
                        Main.KeysSemaphore.WaitOne();
                        MainWindow.DataBaseAgent.SelectData("Keys", ref MainWindow.DS, "Keys");
                        DataRow NewRow = MainWindow.DS.Tables["Keys"].NewRow();
                        NewRow["Date"] = KeyPack.Time;
                        NewRow["Key"] = KeyPack.Data;
                        NewRow["Process"] = KeyPack.ProcessName;
                        NewRow["ChildID"] = ID;
                        MainWindow.DS.Tables["Keys"].Rows.Add(NewRow);
                        MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["Keys"]);
                        MainWindow.DS.Tables.Remove("Keys");
                        Main.KeysSemaphore.Release();
                        if (Main.PageNumber == 12)
                        {
                            ParameterizedThreadStart TempPTS = Main.UpDateUIData;
                            Thread Temp = new Thread(TempPTS);
                            Temp.Start(NewRow);
                        }

                    }; break;
                case (short)Packet.PacketType.WebCam:
                    {
                        Packet Pack = new Packet();
                        Packet.WebCam PicPack = new Packet.WebCam();
                        PicPack = Pack.ToPacket<Packet.WebCam>(Data);
                        Main.WebCamTableSemaphore.WaitOne();
                        MainWindow.DataBaseAgent.SelectData("WebCamTable", ref MainWindow.DS, "WebCamTable");
                        DataRow NewRow = MainWindow.DS.Tables["WebCamTable"].NewRow();
                        NewRow["Date"] = PicPack.Time;
                        NewRow["Pic"] = PicPack.ProcessName;
                        NewRow["ChildID"] = ID;
                        MainWindow.DS.Tables["WebCamTable"].Rows.Add(NewRow);
                        MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["WebCamTable"]);
                        MainWindow.DS.Tables.Remove("WebCamTable");
                        Main.WebCamTableSemaphore.Release();
                        if (Main.PageNumber == 8)
                        {
                            ParameterizedThreadStart TempPTS = Main.UpDateUIData;
                            Thread Temp = new Thread(TempPTS);
                            Temp.Start(NewRow);
                        }
                    }; break;
                case (short)Packet.PacketType.RecordVoice:
                    {
                        Packet Pack = new Packet();
                        Packet.RecordVoice PicPack = new Packet.RecordVoice();
                        PicPack = Pack.ToPacket<Packet.RecordVoice>(Data);
                        byte[] PicData = Convert.FromBase64String(PicPack.Data);
                        Main.VoiceSemaphore.WaitOne();
                        MainWindow.DataBaseAgent.SelectData("Voice", ref MainWindow.DS, "Voice");
                        DataRow NewRow = MainWindow.DS.Tables["Voice"].NewRow();
                        NewRow["Date"] = PicPack.Time;
                        NewRow["Process"] = PicPack.ProcessName;
                        NewRow["VoiceData"] = PicPack.Data;
                        NewRow["ChildID"] = ID;
                        MainWindow.DS.Tables["Voice"].Rows.Add(NewRow);
                        MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["Voice"]);
                        MainWindow.DS.Tables.Remove("Voice");
                        Main.VoiceSemaphore.Release();
                    }; break;
                case (short)Packet.PacketType.License:
                    {
                        Packet Pack = new Packet();
                        Packet.License LicPack = new Packet.License();
                        LicPack = Pack.ToPacket<Packet.License>(Data);
                        List<string> OldLicense = MainWindow.DS.Tables["Data"].Rows[7]["DataContent"].ToString().Split(',').ToList();
                        try
                        {
                            if (!OldLicense.Contains(LicPack.ID))
                            {
                                MainWindow.DataBaseAgent.SelectData("License", ref MainWindow.DS, "License");
                                DataRow NewRow = MainWindow.DS.Tables["License"].NewRow();
                                NewRow["ID"] = LicPack.ID;
                                NewRow["ChildID"] = LicPack.ChildID;
                                NewRow["BuyDate"] = LicPack.BuyDate;
                                NewRow["ExDate"] = LicPack.ExDate;
                                MainWindow.DS.Tables["License"].Rows.Add(NewRow);
                                MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["License"]);
                                MainWindow.DS.Tables["Data"].Rows[7]["DataContent"] += LicPack.ID + ",";
                            }
                            else
                            {

                            }
                        }
                        catch (Exception E)
                        {

                        }
                        MainWindow.DS.Tables.Remove("License");
                        MainWindow.DataBaseAgent.UpdateData(MainWindow.DS.Tables["Data"]);
                    }; break;
            }
        }

        public void RealTimeMonitoring(string InIP, int Port , System.Windows.Controls.Image Picture)
        {
            Task.Run(() =>
            {
                NetworkStream ST = null;
                try
                {
                    RealPic = new List<byte[]>();
                    RealSM = new Semaphore(1, 1);
                    RealData = new byte[1024];
                    RealActor(Picture);
                    IPHostEntry HostInfo = Dns.GetHostByName(Dns.GetHostName());
                    IPAddress Address = null;
                    foreach(IPAddress var in HostInfo.AddressList)
                    {
                        string FindIP = "";
                        byte[] TempIP = var.GetAddressBytes();
                        for(int i = 0; i < TempIP.Length;i++)
                        {
                            FindIP += TempIP[i].ToString();
                            if (i != TempIP.Length - 1)
                            {
                                FindIP += ".";
                            }
                        }
                        if(FindIP == InIP)
                        {
                            Address = var;
                        }
                    }
                    IPEndPoint Point = new IPEndPoint(Address, Port);
                    Real = new TcpListener(Point);
                    Real.Start();
                    ChildTCP = Real.AcceptTcpClient();
                    ST = ChildTCP.GetStream();
                    while (RealEnd == false)
                    {
                        ST.Read(RealData, 0, 100);
                        int PicL = Convert.ToInt32(Encoding.ASCII.GetString(RealData));
                        RealData = new byte[PicL];
                        ST.Read(RealData, 0, PicL);
                        RealPic.Add(RealData);
                        RealData = new byte[100];
                    }
                }
                catch(SocketException E)
                {
                    Real.Stop();
                    if(ChildTCP != null)
                    {
                        ChildTCP.Close();
                    }
                    
                }
                catch (Exception E)
                {
                    RealData = new byte[1024 * 300];
                    ST.Read(RealData, 0, 300);
                }
                
                

            });


        }

        public void RealTimeEnd()
        {
            if(Real != null)
            {
                Real.Stop();
            }
            
            if(ChildTCP!= null)
            {
                ChildTCP.Close();
            }
            
        }
        public void RealWebTimeEnd()
        {
            RealWeb.Stop();
            if(ChildWebTCP != null)
            {
                ChildWebTCP.Close();
            }
        }

        public void RealActor(System.Windows.Controls.Image Picture)
        {
            Task.Run(() =>
            {
                MemoryStream ms;
                double Number = 0.0;
                while (RealEnd == false)
                {
                    if (RealPic.Count != 0)
                    {
                        try
                        {
                            ms = new MemoryStream(RealPic.First());
                            RealPic.RemoveAt(0);
                            Bitmap BitImage = new Bitmap(ms);
                            BitImage.Save(System.AppDomain.CurrentDomain.BaseDirectory + Number + "Real.jpeg");
                            Picture.Dispatcher.Invoke(() =>
                            {
                                
                                var MyBrush = new ImageBrush();
                                BitmapImage BImage = new BitmapImage();
                                BImage.BeginInit();
                                BImage.UriSource = null;
                                BImage.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory+ Number + "Real.jpeg");
                                BImage.EndInit();
                                Picture.Source = BImage;
                            });
                            Number += 0.0001;
                        }
                        catch(Exception E)
                        {

                        }
                        
                    }
                }
            });
            
        }

        public void RealTimeMonitoringWebCam(string InIP, int Port, System.Windows.Controls.Image Picture)
        {
            Task.Run(() =>
            {
                NetworkStream ST = null;
                try
                {
                    RealwebPic = new List<byte[]>();
                    RealWebSM = new Semaphore(1, 1);
                    RealwebData = new byte[1024];
                    RealWebcamActor(Picture);
                    IPHostEntry HostInfo = Dns.GetHostByName(Dns.GetHostName());
                    IPAddress Address = null;
                    foreach (IPAddress var in HostInfo.AddressList)
                    {
                        string FindIP = "";
                        byte[] TempIP = var.GetAddressBytes();
                        for (int i = 0; i < TempIP.Length; i++)
                        {
                            FindIP += TempIP[i].ToString();
                            if (i != TempIP.Length - 1)
                            {
                                FindIP += ".";
                            }
                        }
                        if (FindIP == InIP)
                        {
                            Address = var;
                        }
                    }
                    IPEndPoint Point = new IPEndPoint(Address, Port);
                    RealWeb = new TcpListener(Point);
                    RealWeb.Start();
                    ChildWebTCP = RealWeb.AcceptTcpClient();
                    ST = ChildWebTCP.GetStream();
                    while (RealwebEnd == false)
                    {
                        ST.Read(RealwebData, 0, 100);
                        int PicL = Convert.ToInt32(Encoding.ASCII.GetString(RealwebData));
                        RealwebData = new byte[PicL];
                        ST.Read(RealwebData, 0, PicL);
                        RealwebPic.Add(RealwebData);
                        RealwebData = new byte[100];
                    }
                }
                catch (SocketException E)
                {
                    RealWeb.Stop();
                    if(ChildWebTCP!= null)
                    {
                        if (ChildTCP.Connected == false)
                        {
                            ChildWebTCP.Close();

                        }
                    }
                    
                }
                catch (Exception E)
                {
                    RealData = new byte[1024 * 300];
                    ST.Read(RealData, 0, 300);
                }



            });


        }

        public void RealTimeWebCamEnd()
        {
            RealWeb.Stop();
            ChildWebTCP.Close();
        }

        public void RealWebcamActor(System.Windows.Controls.Image Picture)
        {
            Task.Run(() =>
            {
                MemoryStream ms;
                double Number = 0.0;
                while (RealEnd == false)
                {
                    if (RealwebPic.Count != 0)
                    {
                        try
                        {
                            ms = new MemoryStream(RealwebPic.First());
                            RealwebPic.RemoveAt(0);
                            Bitmap BitImage = new Bitmap(ms);
                            BitImage.Save(System.AppDomain.CurrentDomain.BaseDirectory + Number + "RealWeb.jpeg");
                            Picture.Dispatcher.Invoke(() =>
                            {

                                var MyBrush = new ImageBrush();
                                BitmapImage BImage = new BitmapImage();
                                BImage.BeginInit();
                                BImage.UriSource = null;
                                BImage.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + Number + "RealWeb.jpeg");
                                BImage.EndInit();
                                Picture.Source = BImage;
                            });
                            Number += 0.0001;
                        }
                        catch (Exception E)
                        {

                        }

                    }
                }
            });

        }

        public void TakeScreenShot(System.Windows.Controls.Image Picture)
        {
            Task.Run(() =>
            {
                ChildTCP = Real.AcceptTcpClient();
                NetworkStream ST = ChildTCP.GetStream();
                ST.Read(RealData, 0, 1024);
                int PicL = Convert.ToInt32(Encoding.ASCII.GetString(RealData));
                int i = 0;
                RealData = new byte[PicL];
                ST.Read(RealData, 0, PicL);
                MemoryStream ms = new MemoryStream(RealData);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                returnImage.Save(System.AppDomain.CurrentDomain.BaseDirectory+"Shot.jpeg");
                Picture.Dispatcher.Invoke(() =>
                {
                    var MyBrush = new ImageBrush();
                    BitmapImage BImage = new BitmapImage();
                    BImage.BeginInit();
                    BImage.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "Shot.jpeg");
                    BImage.EndInit();
                    Picture.Source = BImage;
                    Main.ScreenShotSemaphore.WaitOne();
                    MainWindow.DataBaseAgent.SelectData("ScreenShot", ref MainWindow.DS, "*", "ScreenShot", Main.ChildUser, "ChildID");
                    DataRow Row = MainWindow.DS.Tables["ScreenShot"].NewRow();
                    Row["Picture"] = RealData;
                    Row["Date"] = DateTime.Now;
                    MainWindow.DS.Tables["ScreenShot"].Rows.Add(Row);
                    MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["ScreenShot"]);
                    MainWindow.DS.Tables.Remove("ScreenShot");
                    Main.ScreenShotSemaphore.Release();
                });

            });

        }

        private void RealAccept(IAsyncResult ar)
        {
        }

        private void RealRecive(IAsyncResult ar)
        {
        }
       
      
    }
}

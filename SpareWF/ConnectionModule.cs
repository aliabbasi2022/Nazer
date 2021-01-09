using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SpareWF
{
    public class ConnectionModule
    {
        TcpClient ConnectionSocket;
        NetworkStream DataStream;
        string IP;
        int PortNumber;
        bool NetState;
        public bool NetworkState
        {
            get
            {
                return NetState;
            }
            set
            {
                NetState = value;
            }
        }
        //string CurrentVersion;
        string ProcessName;

        public ConnectionModule(string IP, int Port,  string ProcessName)
        {
            this.IP = IP;
            PortNumber = Port;
            //this.CurrentVersion = CurrentVersion;
            this.ProcessName = ProcessName;
        }

        public void InitialConnection()
        {
            if(NetState == true)
            {
                ConnectionSocket = new TcpClient();
                ConnectionSocket.BeginConnect(Dns.GetHostAddresses(IP)[0], PortNumber, ConnectedCallBack , ConnectionSocket );
            }
        }

        private void ConnectedCallBack(IAsyncResult ar)
        {
            ConnectionSocket.EndConnect(ar);
            DataStream = ConnectionSocket.GetStream();
        }

        public bool SendData(string DataStr)
        {
            try
            {
                byte[] SendData = Encoding.Unicode.GetBytes(DataStr);
                DataStream.Write(SendData, 0, SendData.Length);
                return true;
            }
            catch (Exception E)
            {
                return false;
            }
        }

        public string ReciveData()
        {
            string Result = null;
            try
            {
                byte[] IncommingData = new byte[1024 * 2];
                DataStream.Read(IncommingData, 0, IncommingData.Length);
                Result = Encoding.Unicode.GetString(IncommingData);
                Result = Result.Replace("/0", "");
            }
            catch(Exception E)
            {

            }
            return Result;
            
        }

        public string ReciveFile(string Path)
        {
            FileStream FStream = File.Create(Path + "\\" + "New" + ProcessName);
            byte[] Data = new byte[1024 * 2];
            if(!FStream.CanWrite)
            {
                FStream = File.OpenWrite(Path + "\\" + "New" + ProcessName);
            }
            while(DataStream.CanRead)
            {
                int Counter = DataStream.Read(Data, 0, Data.Length);
                FStream.Write(Data, 0, Counter);
            }
            FStream.Close();
            string Result = "Not Sucsess";
            try
            {
                if(DataStream.CanRead)
                {
                    DataStream.Close();
                }
                Result = "Sucsess";
                return Result;
            }
            catch(Exception E)
            {

            }
            return Result;
        }
    }
}

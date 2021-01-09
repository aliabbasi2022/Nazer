using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Data;
using System.Threading;
using System.Drawing;

namespace Child

{
    class ChildClass
    {
        Socket ParentRealTimeSocket;
        public Semaphore SendDataSM;
        int DataSize;
        string IP = "";
        int Port;
        string ID = "";
        public ChildClass(string IP , int Port , int DataSize , string ID)
        {
            SendDataSM = new Semaphore(1, 1);
            this.DataSize = DataSize;
            ParentRealTimeSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.IP = IP;
            this.Port = Port;
            this.ID = ID;
        }
        public void Connect()
        {
            ParentRealTimeSocket.BeginConnect(IP, Port, ConnectedRealTime, ParentRealTimeSocket);
        }
        
        public void ProperingData(string Picture)
        {
            Packet Pack = new Packet();
            Packet.ProPacket ProPack = new Packet.ProPacket();
            ProPack.Type = (short)Packet.PacketType.ScrrenShot;
            ProPack.ID = ID;
            ProPack.TotalSize = Encoding.Unicode.GetBytes(Picture.ToArray()).Count();
            SendPicture(Pack.ToString(ProPack));
            SendPicture(Picture);
        }
        private void ConnectedRealTime(IAsyncResult ar)
        {
            ParentRealTimeSocket.EndConnect(ar);
        }

        public void SendPicture(string Data)
        {
            byte[] ByteData = Encoding.Unicode.GetBytes(Data);
            int Counter = 0;

            //Counter +=ParentRealTimeSocket.Send(ByteData);
            ParentRealTimeSocket.SendFile("E:\\we.jpeg");
            //while (ByteData.Length >= Counter)
            //{
            //    Counter += 
            //}
            //SendDataSM.Release();
        }
    }
}

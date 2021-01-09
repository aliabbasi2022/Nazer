using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;


namespace UI
{
    class ParentClass
    {
        Socket ParentSocket;
        string ID;
        byte[] RecivedData;
        bool IsStart = true;
        string PacketData = "";
        string ProPack = "";
        int RemainingData;
        int RecivedNumber;
        //int RealTimePort;
        public ParentClass(Socket ChildSocket, int Size)
        {
            this.ParentSocket = ChildSocket;
            RecivedData = new byte[Size];
        }
        public void PreaperForReciveData( int Port , int BackLog)
        {
            NetWorkTools Net = new NetWorkTools();
            string ConnIP = Net.GetConnectionInterfaceName().Split('$')[1];
            ParentSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPHostEntry HostInfo = Dns.Resolve(ConnIP);
            IPAddress Address = HostInfo.AddressList[0];
            IPEndPoint Point = new IPEndPoint(Address, Port);
            Point.Create(new SocketAddress(AddressFamily.InterNetwork));
            ParentSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ParentSocket.Bind(Point);
            ParentSocket.Listen(BackLog);
        }

        private void Recived()
        {
            Packet Pack = new Packet();
            try
            {
                RecivedNumber = ParentSocket.Receive(RecivedData);
                string Temp = Encoding.Unicode.GetString(RecivedData).Replace("\0", "");
                RecivedData = new byte[RecivedData.Length];
                if((RecivedNumber == 0) || (Temp == ""))
                {
                    MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(PacketData));
                    System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                    ShowPicture(returnImage);
                }
                else
                {
                    PacketData += Temp;
                }

            }
            catch(Exception e)
            {

            }
            //ParentSocket.BeginReceive(RecivedData, 0, RecivedData.Length, SocketFlags.None, Recived, ParentSocket);
        }

        //public void SendToDataBase(string Data ,string  ID , DateTime Time)
        //{
        //
        //}
        public void ShowPicture(System.Drawing.Image Picture)
        {

        }
    }
}

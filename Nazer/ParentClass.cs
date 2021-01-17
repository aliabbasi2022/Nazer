using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;//Provides a managed implementation of the Windows Sockets (Winsock) interface for developers who need to tightly control access to the network.
using System.Net;//Provides a simple programming interface for many of the protocols used on networks today.
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
                    System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);//Creates an Image from the specified data stream.
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
        }

        public void ShowPicture(System.Drawing.Image Picture)
        {

        }
    }
}

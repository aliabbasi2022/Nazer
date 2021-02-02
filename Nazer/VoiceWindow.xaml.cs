using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace UI
{
    /// <summary>
    /// Interaction logic for VoiceWindow.xaml
    /// </summary>
    public partial class VoiceWindow : Window
    {
        public  string Child;
        public VoiceWindow()
        {
            InitializeComponent();
            DataSet DS = new DataSet();
            MainWindow.DataBaseAgent.SelectDataWithCommand("Select ProcessName From Process Group By ProcessName", ref DS, "ProceeData");
            foreach (DataRow var in DS.Tables[0].Rows)
            {
                AppList.Items.Add(var[0]);
            }
            
        }

        private void CancleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow.MyMain.Show();
        }

        private void RecordBtn_Click(object sender, RoutedEventArgs e)
        {
            if(AppList.SelectedIndex > -1)
            {
                Packet packet = new Packet();
                Packet.RecordVoice recordVoice = new Packet.RecordVoice();
                recordVoice.ProcessName = AppList.SelectedItem.ToString();
                recordVoice.State = true;
                recordVoice.Time = DateTime.Now.ToString();
                string Data = packet.ToString(recordVoice);
                Packet.PSProPacket pSPro = new Packet.PSProPacket();
                pSPro.Reciver = Child;
                pSPro.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                pSPro.Type = (short)Packet.PacketType.RecordVoice;
                pSPro.TotalSize = Encoding.Unicode.GetByteCount(Data);
                string proData = packet.ToString(pSPro);
                Packet.MainPacket MainData = new Packet.MainPacket();
                MainData.Data = Data;
                MainData.PPacket = packet.ToString(pSPro);
                string MainStrData = packet.ToString<Packet.MainPacket>(MainData);
                MainWindow.Connection.SendDataSM.WaitOne();
                MainWindow.Connection.SendToServer(MainStrData);
                MainWindow.Connection.SendDataSM.Release();
                MessageBox.Show("Voice recird request Sucessfully Send");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.MyMain.Show();
        }
    }
}

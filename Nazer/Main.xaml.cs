using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;//Provides classes and interfaces that enable multithreaded programming.
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;//Provides types to support the Windows Presentation Foundation (WPF) input system. 
using System.Windows.Media;
using System.Windows.Media.Imaging;//Provides types that are used to encode and decode bitmap images.
using System.Windows.Shapes;//Provides access to a library of shapes that can be used in Extensible Application Markup Language (XAML) or code.
using System.Data.SqlClient;
using System.Data;
using Microsoft.Win32;
using System.Windows.Navigation;//Provides types that support navigation, including navigating between windows and navigation journaling.
using System.Windows.Controls.DataVisualization;
using System.Windows.Controls.DataVisualization.Charting;
using System.Timers;
using System.Windows.Controls.Primitives;
using System.Reflection;//Contains types that retrieve information about assemblies, modules, members, parameters, and other entities in managed code by examining their metadata
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace UI
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        int SelectedTab = -1;
        EnterAppCat AddCat;
        public static int page = 0;
        EnterAppInCat AddApp;
        bool StartReal = false;
        Window PWindow;
        Expander History;
        TextBox URLDomain;
        List<ComboBox> AllCombo;
        public static int SelectTab = -1;
        public static int PageNumber = -1;
        Color MouseEnterPic;
        public static BlockApp SelctedToAdd;
        public static Image ScreenShotImage;
        public static Image WebcamImage;
        public static MediaPlayer MP;
        public static Semaphore NetworkAdaptorSemaphor;
        int[] HeadersWidth =
                        {
                            500,200,300,250
                        };
        List<Image> ScerrenShots;
        List<object> MoreNetwork;
        List<object> MoreUrls;
        List<object> HistoryApps;
        List<object> MoreApps;
        public static ParameterizedThreadStart UpDateUIData;
        TextBlock PageTitle;
        ColorList CategoryAppAppList;
        ColorList LimitList;
        ColorList BlockURLList;
        ColorList NetworkList;
        ColorList URLCategoryList;
        ColorList URLsList;
        Label UserNameLabel;//$$
        Image image; //$$
        List<object> MoreLocation;
        Color KeyBorderHover;
        public static Button VoiceBtn;
        public static bool MediaIsPlay;
        int SCSelected = -1;
        int WebPicSelected = -1;
        Color SelectedSC;
        Color SelectedWebPic;
        public static string NewCat;
        public static string AddAppName = "";
        public static string SelectedCat;
        public static string SelectedApp;
        ColorList BlockingAppList;
        public static string ChildUser = "";
        public static BitmapImage BITIM;
        public static BitmapImage WebCamBITIM;
        public static Image Monitor;
        public static Border Bord;
        RemoveURLCat Dialog;
        public static string CurrentUser;
        public static ControlTemplate BtnTemp;
        public static string SelectedURLCat;
        public Thread MainTh;
        public static Semaphore RunningSM;
        AddBlockApp AddBDialog;
        public static System.Windows.Threading.Dispatcher Disp;
        List<object> LimitExpand;
        List<object> MoreLimitExpand;
        ColorList CategoryAppList;
        object LasSelectMenu;
        static WebBrowser LocationBrowser;
        public delegate void workerFunctionDelegate(int totalSeconds);
        public static int[] NewLogs = new int[12];
        public static int[] TotalLogs = new int[12];
        //string ReciverID;
        System.Timers.Timer ReLoad;
        Image WebcamMonitor;
        System.Windows.WindowState OldWindowsState;
        public static System.Timers.Timer NetAdaptor = new System.Timers.Timer();
        public static Grid DeviceInfoGrid;
        Semaphore LastSeenSemaphore;
        public static Semaphore HistoryURLSemaphore;
        public static Semaphore ScreenShotSemaphore;
        public static Semaphore WebCamTableSemaphore;
        public static Semaphore LocationSemapore;
        public static Semaphore InstalledAppsSemaphore;
        public static Semaphore RunningAppsSemaphore;
        public static Semaphore KeysSemaphore;
        public static Semaphore VoiceSemaphore;
        public static Semaphore VPNSemaphore;
        public static Semaphore NetworkSemaphore;
        public static  string OsTypeTb
        {
            set
            {
                ((TextBlock)((Border)DeviceInfoGrid.Children.Cast<UIElement>().First(x => x.Uid == "OSTypeTXT")).Child).Text = value;
            }
        }
        public static string WifiSattusTb
        {
            set
            {
                ((TextBlock)((Border)DeviceInfoGrid.Children.Cast<UIElement>().First(x => x.Uid == "WifiTXT")).Child).Text = value;
            }
        }
        public static string ValidDataTb
        {
            set
            {
                ((TextBlock)((Border)DeviceInfoGrid.Children.Cast<UIElement>().First(x => x.Uid == "ValidDateTXT")).Child).Text = value;
            }
        }
        public static string LicensedataTb
        {
            set
            {
                ((TextBlock)((Border)DeviceInfoGrid.Children.Cast<UIElement>().First(x => x.Uid == "LicenseTXT")).Child).Text = value;
            }
        }

        
        public Main()
        {
            InitializeComponent();
            PageTitle = new TextBlock();
            PageTitle.FontSize = 20;
            PageTitle.FontWeight = FontWeights.Bold;
            PageTitle.Uid = "PageTitle";
            PageTitle.Foreground = new SolidColorBrush(Colors.Black);
            PageTitle.VerticalAlignment = VerticalAlignment.Top;
            PageTitle.HorizontalAlignment = HorizontalAlignment.Left;
            BMain.Children.Add(PageTitle);
            image = new Image();
            NetworkAdaptorSemaphor = new Semaphore(1, 1);
            foreach (string var in MainWindow.DS.Tables["Data"].Rows[4]["DataContent"].ToString().Split(','))
            {
                if (var != "")
                {
                    ChildsCombo.Items.Add(var);
                }

            }
            ChildsCombo.SelectedIndex = 0;
            RunningSM = new Semaphore(1, 1);
            if (ChildsCombo.SelectedItem != null)
            {
                ChildUser = ChildsCombo.SelectedItem.ToString();
            }
            MouseButtonEventArgs E = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left);
            E.RoutedEvent = StackPanel.MouseLeftButtonDownEvent;
            Disp = this.Dispatcher;
            BITIM = new BitmapImage();
            MainWindow.DataBaseAgent.ExequteWithCommand("delete From RunningApps");
            LastSeenSemaphore = new Semaphore(1, 1);
            HistoryURLSemaphore = new Semaphore(1, 1);
            ScreenShotSemaphore = new Semaphore(1, 1);
            WebCamTableSemaphore = new Semaphore(1, 1);
            LocationSemapore = new Semaphore(1, 1);
            InstalledAppsSemaphore = new Semaphore(1, 1);
            RunningAppsSemaphore = new Semaphore(1, 1);
            KeysSemaphore = new Semaphore(1, 1);
            VoiceSemaphore = new Semaphore(1, 1);
            VPNSemaphore = new Semaphore(1, 1);
            NetworkSemaphore = new Semaphore(1, 1);
        }
        public Main(Window ParentWindow)
        {
            PWindow = ParentWindow;
            InitializeComponent();
            
            PageTitle = new TextBlock();
            PageTitle.FontSize = 20;
            PageTitle.FontWeight = FontWeights.Bold;
            PageTitle.Uid = "PageTitle";
            PageTitle.Foreground = new SolidColorBrush(Colors.Black);
            PageTitle.VerticalAlignment = VerticalAlignment.Top;
            PageTitle.HorizontalAlignment = HorizontalAlignment.Left;
            BMain.Children.Add(PageTitle);
            image = new Image();
            foreach (string var in MainWindow.DS.Tables["Data"].Rows[4]["DataContent"].ToString().Split(','))
            {
                if(var != "")
                {
                    
                    ChildsCombo.Items.Add(var);
                    
                }
                
            }
            ChildsCombo.SelectedIndex = 0;
            RunningSM = new Semaphore(1, 1);
            if(ChildsCombo.SelectedItem != null)
            {
                ChildUser = ChildsCombo.SelectedItem.ToString();
            }
            MouseButtonEventArgs E = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left);
            E.RoutedEvent = StackPanel.MouseLeftButtonDownEvent;
            Dashboard_0.RaiseEvent(E);
            Disp = this.Dispatcher;
            BITIM = new BitmapImage();
            RunningAppsSemaphore.WaitOne();
            MainWindow.DataBaseAgent.ExequteWithCommand("delete From RunningApps");
            RunningAppsSemaphore.Release();
        }

        private void ChildsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MouseButtonEventArgs E = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left);
            E.RoutedEvent = StackPanel.MouseLeftButtonDownEvent;
            if(LasSelectMenu != null)
            {
                ((UIElement)LasSelectMenu).RaiseEvent(E);
            }
            ChildUser = ChildsCombo.SelectedItem.ToString();
            
        }

        private void TabButton_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel X = (StackPanel)sender;
            int Number;
            Number = Convert.ToInt32(X.Name.Split('_')[1]);
            if(Number != SelectedTab)
            {
                X.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF87A0B9"));
            }
        }

        private void TabButton_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel X = (StackPanel)sender;
            if (SelectedTab != Convert.ToInt32(X.Name.Split('_')[1]))
            {
                X.Background = null;
            }
            else
            {
                X.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF87A0B9"));
            }
               
        }

        private void MainW_Closed(object sender, EventArgs e)
        {

            MainWindow.DataBaseAgent.ExequteWithCommand("Delete From RunningApps");
        }
        void Test(int Input)
        {

        }
        private void TabButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            LasSelectMenu = ((StackPanel)sender);
            StackPanel X = (StackPanel)sender;
            X.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF87A0B9"));
            PageNumber = Convert.ToInt32(X.Name.Split('_')[1]);
            if(ReLoad != null)
            {
                ReLoad.Stop();
            }
            if(SelectedTab != -1 && PageNumber != SelectedTab)
            {
                StackPanel ParentTarget = (StackPanel)X.Parent;
                foreach(StackPanel Target in ParentTarget.Children)
                {
                    if(Target.Name.Contains(SelectedTab.ToString()))
                    {
                        Target.Background = null;
                    }
                }

            }
            SelectedTab = PageNumber;
            switch (PageNumber)
            {
                case 0:
                    {
                        MainDataGrid.Children.Clear();
                        TextBlock TitleTB = new TextBlock();
                        TitleTB.Text = "Dashboard";
                        TitleTB.FontSize = 20;
                        TitleTB.Foreground = new SolidColorBrush(Colors.Gray);
                        TitleTB.Margin = new Thickness(20,-30, 0, 10);
                        TitleTB.VerticalAlignment = VerticalAlignment.Top;
                        TitleTB.HorizontalAlignment = HorizontalAlignment.Left;
                        MainDataGrid.Children.Add(TitleTB);
                        string[] TextArray = { "Device:", "Wifi Only:", "Processed: ", " License Key:", "OS:", "Valid :" };
                        string[] UIDDashboard =
                        {
                            "DeviceNameTXT","WifiTXT","ProcessedTXT","LicenseTXT","OSTypeTXT","ValidDateTXT"
                        };
                        string[] UrliBrifData = { "devices.png", "wifi.png", "cloud-storage-uploading-option.png", "vintage-key-outline.png", "package.png", "event.png" };
                        Border Bord = new Border();
                        Bord.Height = 100;
                        Bord.VerticalAlignment = VerticalAlignment.Top;
                        
                        Bord.HorizontalAlignment = HorizontalAlignment.Left;
                        Bord.Margin = new Thickness(20, 20, 20, 20);
                        Bord.Background = new SolidColorBrush(Colors.White);
                        
                        Grid DeviceInfo = new Grid();
                        DeviceInfo.Margin = new Thickness(5, 10, 5, 10);
                        ColumnDefinition Col0 = new ColumnDefinition();
                        ColumnDefinition Col1 = new ColumnDefinition();
                        ColumnDefinition Col2 = new ColumnDefinition();
                        ColumnDefinition Col3 = new ColumnDefinition();
                        ColumnDefinition Col4 = new ColumnDefinition();
                        ColumnDefinition Col5 = new ColumnDefinition();
                        RowDefinition Row0 = new RowDefinition();
                        RowDefinition Row1 = new RowDefinition();
                        Col0.MinWidth = 100;
                        Col0.MaxWidth = 100;
                        Col1.MinWidth = 300;
                        Col1.MaxWidth = 300;
                        Col2.MinWidth = 100;
                        Col2.MaxWidth = 100;
                        Col3.MinWidth = 300;
                        Col3.MaxWidth = 300;
                        Col4.MinWidth = 100;
                        Col4.MaxWidth = 100;
                        Col5.MinWidth = 300;
                        Col5.MaxWidth = 300;
                        Row0.MinHeight = 30;
                        Row0.MaxHeight = 30;
                        Row1.MinHeight = 30;
                        Row1.MaxHeight = 30;
                        DeviceInfo.ColumnDefinitions.Add(Col0);
                        DeviceInfo.ColumnDefinitions.Add(Col1);
                        DeviceInfo.ColumnDefinitions.Add(Col2);
                        DeviceInfo.ColumnDefinitions.Add(Col3);
                        DeviceInfo.ColumnDefinitions.Add(Col4);
                        DeviceInfo.ColumnDefinitions.Add(Col5);
                        DeviceInfo.RowDefinitions.Add(Row0);
                        DeviceInfo.RowDefinitions.Add(Row1);
                        DeviceInfo.VerticalAlignment = VerticalAlignment.Top;
                        DeviceInfo.HorizontalAlignment = HorizontalAlignment.Left;
                        DeviceInfo.Margin = new Thickness(10, 20, 20, 20);
                        DeviceInfo.Uid = "DeviceInfoUID";
                        Bord.Child = DeviceInfo;
                        MainDataGrid.Children.Add(Bord);
                        int k = 0;
                        for(int i = 0; i < 6; i += 2)
                        {
                            for(int j = 0; j < 2; j++)
                            {
                                Border Board = new Border();
                                StackPanel SP = new StackPanel();
                                SP.Orientation = Orientation.Horizontal;
                                Image Icon = new Image();
                                Icon.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/"+ UrliBrifData[k]));
                                Icon.Width = 10;
                                Icon.Height = 10;
                                Icon.Margin = new Thickness(5, 0, 0, 0);
                                TextBlock TB = new TextBlock();
                                TB.VerticalAlignment = VerticalAlignment.Top;
                                TB.Text = TextArray[k];
                                TB.Margin = new Thickness(4, 6, 2, 2);
                                SP.Children.Add(Icon);
                                SP.Children.Add(TB);
                                k++;
                                Board.Background = new SolidColorBrush(Colors.AliceBlue);
                                Board.BorderBrush = new SolidColorBrush(Colors.Black);
                                if(j == 0)
                                {
                                    Board.BorderThickness = new Thickness(0.25);
                                }
                                else
                                {
                                    Board.BorderThickness = new Thickness(0.25);
                                }
                                Board.Child = SP;
                                DeviceInfo.Children.Add(Board);
                                Grid.SetColumn(Board, i);
                                Grid.SetRow(Board, j);
                            }
                            
                        }
                        k = 0;
                        for (int i = 1; i < 6; i += 2)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                Border Board = new Border();
                                TextBlock TB = new TextBlock();
                                Board.Uid = UIDDashboard[k];
                                TB.HorizontalAlignment = HorizontalAlignment.Left;
                                TB.Foreground = new SolidColorBrush(Colors.Black);
                                TB.Margin = new Thickness(4, 6, 2, 2);
                                Board.Background = new SolidColorBrush(Colors.White);
                                Board.BorderBrush = new SolidColorBrush(Colors.Black);
                                if (j == 0)
                                {
                                    Board.BorderThickness = new Thickness(0.25);
                                }
                                else
                                {
                                    Board.BorderThickness = new Thickness(0.25);
                                }
                                Board.Child = TB;
                                DeviceInfo.Children.Add(Board);
                                Grid.SetColumn(Board, i);
                                Grid.SetRow(Board, j);
                                k++;
                            }

                        }
                        
                        string[] TitlesRect = { "Web History", "Location", "Applications", "Screen shots", "VPN", "Webcam", "Key Loggs", "Voice","Useed Applications","Call Log" , "SMS Log","Contacts", };
                        string[] RectIcon = { "if_Globe_99094.ico", "if_Google_Places_99182.ico", "if_aws_334593.ico", "if_dailybooth_334610.ico",
                        "1175205-64.png","if_Aperture_98841.ico",
                        "if_Keyboard_99179.ico","if_Microphone_2_98779.ico","if_Koding_98931.ico","if_Phone_99238.ico","if_Email_Chat_98790.ico","if_User_Folder_99333.ico"};
                        WrapPanel BrifePanel = new WrapPanel();
                        BrifePanel.VerticalAlignment = VerticalAlignment.Top;
                        BrifePanel.HorizontalAlignment = HorizontalAlignment.Left;
                        BrifePanel.Margin = new Thickness(0, 200, 20, 0);
                        BrifePanel.Height = 250;
                        BrifePanel.Width = 1400;
                        k = 0;
                        string[] TableNames = { "HistoryURL", "Location", "InstalledApps", "ScreenShot", "VPN", "WebCamTable", "Keys","Voice","Process", "CallLog", "SMSLog", "Contact" };
                        int ChildCount = (int)MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select Count(*) From (Select ChildID From LastSeen Group by ChildID) as T");
                        if(ChildCount < ChildsCombo.Items.Count)
                        {
                            MainWindow.DataBaseAgent.SelectDataWithCommand("Select ChildID From LastSeen where  Exists (Select ChildID From LastSeen Group By ChildID) group by ChildID", ref MainWindow.DS, "NewChilds");
                            MainWindow.DS.Tables["NewChilds"].PrimaryKey = new DataColumn[] { MainWindow.DS.Tables["NewChilds"].Columns[0] };
                            List<string> NewChilds = new List<string>();
                            for(int i = 0; i < ChildsCombo.Items.Count; i++)
                            {
                                if(MainWindow.DS.Tables["NewChilds"].Rows.Find(ChildsCombo.Items[i].ToString()) == null)
                                {
                                    NewChilds.Add(ChildsCombo.Items[i].ToString());
                                }
                                
                            }
                            
                            for (int i = 0; i < NewChilds.Count; i++)
                            {
                                string QData = "Values";
                                for(int j = 0; j < TableNames.Length; j++)
                                {
                                    QData += ("('" + TableNames[j] + "' , 0 ,'" + NewChilds[i] + "'),");
                                }
                                QData = QData.Remove(QData.Length - 1, 1);
                                MainWindow.DataBaseAgent.ExequteWithCommand("Insert into LastSeen " + QData);
                            }
                            MainWindow.DS.Tables.Remove("NewChilds");
                        }
                        LastSeenSemaphore.WaitOne();
                        MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From LastSeen where ChildID = '" + ChildsCombo.SelectedItem.ToString() + "'", ref MainWindow.DS, "LastSeen");
                        MainWindow.DS.Tables["LastSeen"].PrimaryKey =new DataColumn[] { MainWindow.DS.Tables["LastSeen"].Columns["TabelsName"]};
                        for (int i = 0; i < TableNames.Length; i++)
                        {
                            long Count = -1;
                            try
                            {
                                Count =  (long)MainWindow.DS.Tables["LastSeen"].Rows.Find(TableNames[i])[1];
                            }
                            catch (Exception E)
                            {
                            }
                            
                            NewLogs[i] = (int)MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select Count(*) From " + TableNames[i] + " where ChildID = '" + ChildsCombo.SelectedItem.ToString() + "'");
                            NewLogs[i] -= (int)Count;
                        }
                        for (int i = 0; i < 6;i++)
                        {
                            for(int l = 0; l <2;l++)
                            {
                                try
                                {
                                    RectDataWindows Test = new RectDataWindows();
                                    int Counter = (int)MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select Count(*) From " + TableNames[k] + " where ChildID='" + ChildsCombo.SelectedItem.ToString() + "'");
                                    Border TargetB = Test.Initial(TitlesRect[k], NewLogs[k].ToString(), (Counter).ToString(), new Uri(@"pack://application:,,,/UI;component/Files/" + RectIcon[k]));
                                    TargetB.VerticalAlignment = VerticalAlignment.Top;
                                    TargetB.HorizontalAlignment = HorizontalAlignment.Left;
                                    TargetB.Margin = new Thickness(20, 15, 0, 0);
                                    k++;
                                    BrifePanel.Children.Add(TargetB);
                                }
                                catch(Exception E)
                                {

                                }
                                
                                
                            }
                        }
                        
                        for (int i = 0; i < TableNames.Length; i++)
                        {
                            DataRow Row;
                            Row = MainWindow.DS.Tables["LastSeen"].NewRow();
                            Row[1] =  (int)((long)MainWindow.DS.Tables["LastSeen"].Rows.Find(TableNames[i])[1] + NewLogs[i]);
                            Row[0] = TableNames[i];
                            Row[2] = ChildsCombo.SelectedItem.ToString();
                            MainWindow.DS.Tables["LastSeen"].Rows.Remove(MainWindow.DS.Tables["LastSeen"].Rows.Find(TableNames[i]));
                            MainWindow.DS.Tables["LastSeen"].Rows.Add(Row);
                            MainWindow.DataBaseAgent.UpdateTable(MainWindow.DS.Tables["LastSeen"], Row,false);

                        }

                        MainWindow.DS.Tables.Remove("LastSeen");
                        LastSeenSemaphore.Release();
                        MainDataGrid.Children.Add(BrifePanel);
                        WrapPanel OtherOptionsPanel = new WrapPanel();
                        OtherOptionsPanel.VerticalAlignment = VerticalAlignment.Top;
                        OtherOptionsPanel.HorizontalAlignment = HorizontalAlignment.Left;
                        OtherOptionsPanel.Margin = new Thickness(0, 500, 20, 10);
                        Border URLBG = new Border();
                        URLBG.Background = new SolidColorBrush(Colors.White);
                        URLBG.Width = 610;
                        URLBG.Height = 370;
                        URLBG.Margin = new Thickness(20, 5, 0, 20);
                        WrapPanel URLsWrap = new WrapPanel();
                        URLsWrap.Background = new SolidColorBrush(Colors.Transparent);
                        Image URLTitleImage = new Image();
                        URLTitleImage.Width = 15;
                        URLTitleImage.Height = 15;
                        URLTitleImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/Benjigarner-Summer-Collection-Network-Web.ico"));
                        URLTitleImage.HorizontalAlignment = HorizontalAlignment.Left;
                        URLTitleImage.VerticalAlignment = VerticalAlignment.Top;
                        URLTitleImage.Margin = new Thickness(20, 16, 0, 0);
                        TextBlock TitleImageTB = new TextBlock();
                        TitleImageTB.Text = "WebSites";
                        TitleImageTB.Foreground = new SolidColorBrush(Colors.Black);
                        TitleImageTB.FontSize = 12;
                        TitleImageTB.FontWeight = FontWeights.Bold;
                        TitleImageTB.HorizontalAlignment = HorizontalAlignment.Left;
                        TitleImageTB.VerticalAlignment = VerticalAlignment.Top;
                        TitleImageTB.Margin = new Thickness(5, 15, 0, 0);
                        Line URLSepratorLine = new Line();
                        URLSepratorLine.VerticalAlignment = VerticalAlignment.Top;
                        URLSepratorLine.HorizontalAlignment = HorizontalAlignment.Left;
                        URLSepratorLine.Margin = new Thickness(15, 10, 0, 0);
                        URLSepratorLine.Stroke = new SolidColorBrush(Colors.LightGray);
                        URLSepratorLine.Opacity = 70;
                        URLSepratorLine.StrokeThickness = 1;
                        URLSepratorLine.X1 = 5;
                        URLSepratorLine.Y1 = 10;
                        URLSepratorLine.X2 = 570;
                        URLSepratorLine.Y2 = 10;
                        StackPanel URLSP = new StackPanel();
                        URLSP.VerticalAlignment = VerticalAlignment.Top;
                        URLSP.HorizontalAlignment = HorizontalAlignment.Left;
                        URLSP.MinHeight = 300;
                        URLSP.MaxHeight = 300;
                        URLSP.Width = 560;
                        URLSP.Margin = new Thickness(20,10, 0, 0);
                        URLsWrap.Children.Add(URLTitleImage);
                        URLsWrap.Children.Add(TitleImageTB);
                        URLsWrap.Children.Add(URLSepratorLine);
                        URLsWrap.Children.Add(URLSP);
                        HistoryURLSemaphore.WaitOne();
                        if (MainWindow.DS.Tables.Contains("HistoryURL") == true)
                        {
                            MainWindow.DS.Tables.Remove("HistoryURL");
                        }
                        MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From HistoryURL where ChildID='" + ChildUser + "' order by Date desc", ref MainWindow.DS, "HistoryURL");
                        for (int i = 0;(i < MainWindow.DS.Tables["HistoryURL"].Rows.Count) && (i < 9); i++)
                        {
                            DockPanel SPBord = new DockPanel();
                            SPBord.Background = new SolidColorBrush(Color.FromArgb(100,230,230,230));
                            SPBord.Height = 25;
                            SPBord.Width = 560;
                            SPBord.Margin = new Thickness(0, 7, 0, 0);
                            Image URLImage = new Image();
                            URLImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/Bokehlicia-Captiva-Browser-web.ico"));
                            URLImage.Width = 20;
                            URLImage.Height = 20;
                            URLImage.Margin = new Thickness(0, 2, 0, 0);
                            URLImage.VerticalAlignment = VerticalAlignment.Top;
                            URLImage.HorizontalAlignment = HorizontalAlignment.Left;
                            Label URLLab = new Label();
                            URLLab.Margin = new Thickness(17, -2, 0, 0);
                            URLLab.VerticalAlignment = VerticalAlignment.Top;
                            URLLab.HorizontalAlignment = HorizontalAlignment.Left;
                            URLLab.Foreground = new SolidColorBrush(Colors.Blue);
                            URLLab.Content = MainWindow.DS.Tables["HistoryURL"].Rows[i]["URL"].ToString();
                            URLLab.FontSize = 12;
                            SPBord.Children.Add(URLImage);
                            SPBord.Children.Add(URLLab);
                            URLSP.Children.Add(SPBord);
                        }
                        MainWindow.DS.Tables.Remove("HistoryURL");
                        HistoryURLSemaphore.Release();
                        URLBG.Child = URLsWrap;
                        URLBG.VerticalAlignment = VerticalAlignment.Top;
                        URLBG.HorizontalAlignment = HorizontalAlignment.Left;
                        OtherOptionsPanel.Children.Add(URLBG);
                        Border LocationBG = new Border();
                        LocationBG.Background = new SolidColorBrush(Colors.White);
                        LocationBG.Width = 610;
                        LocationBG.Height = 370;
                        LocationBG.Margin = new Thickness(20, 5, 0, 20);
                        Image LocationTitleImage = new Image();
                        LocationTitleImage.Width = 15;
                        LocationTitleImage.Height = 15;
                        LocationTitleImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/location-1.ico"));
                        LocationTitleImage.HorizontalAlignment = HorizontalAlignment.Left;
                        LocationTitleImage.VerticalAlignment = VerticalAlignment.Top;
                        LocationTitleImage.Margin = new Thickness(20, 16, 0, 0);
                        LocationBrowser = new WebBrowser();
                        TextBlock LocationTitleImageTB = new TextBlock();
                        LocationTitleImageTB.Text = "Location";
                        LocationTitleImageTB.Foreground = new SolidColorBrush(Colors.Black);
                        LocationTitleImageTB.FontSize = 12;
                        LocationTitleImageTB.FontWeight = FontWeights.Bold;
                        LocationTitleImageTB.HorizontalAlignment = HorizontalAlignment.Left;
                        LocationTitleImageTB.VerticalAlignment = VerticalAlignment.Top;
                        LocationTitleImageTB.Margin = new Thickness(5, 15, 0, 0);
                        Line LocationSepratorLine = new Line();
                        LocationSepratorLine.VerticalAlignment = VerticalAlignment.Top;
                        LocationSepratorLine.HorizontalAlignment = HorizontalAlignment.Left;
                        LocationSepratorLine.Margin = new Thickness(15, 10, 0, 0);
                        LocationSepratorLine.Stroke = new SolidColorBrush(Colors.LightGray);
                        LocationSepratorLine.Opacity = 70;
                        LocationSepratorLine.StrokeThickness = 1;
                        LocationSepratorLine.X1 = 5;
                        LocationSepratorLine.Y1 = 10;
                        LocationSepratorLine.X2 = 570;
                        LocationSepratorLine.Y2 = 10;
                        WrapPanel LocationWrap = new WrapPanel();
                        LocationWrap.Background = new SolidColorBrush(Colors.Transparent);
                        LocationWrap.Children.Add(LocationTitleImage);
                        LocationWrap.Children.Add(LocationTitleImageTB);
                        LocationWrap.Children.Add(LocationSepratorLine);
                        LocationBrowser.Margin = new Thickness(20, 10, 0, 0);
                        LocationBrowser.VerticalAlignment = VerticalAlignment.Top;
                        LocationBrowser.HorizontalAlignment = HorizontalAlignment.Left;
                        LocationBrowser.Width = 570;
                        LocationBrowser.Height = 290;
                        LocationBrowser.Loaded += LocationBrowser_Loaded;
                        LocationBrowser.LoadCompleted += LocationBrowser_LoadCompleted;

                        LocationWrap.Children.Add(LocationBrowser);
                        LocationBG.Child = LocationWrap;
                        OtherOptionsPanel.Children.Add(LocationBG);
                       
                        //    LocationBrowser.Source = new Uri("https://www.google.com/maps");

                        Border ScreenShotBG = new Border();
                        ScreenShotBG.Background = new SolidColorBrush(Colors.White);
                        ScreenShotBG.Width = 610;
                        ScreenShotBG.Height = 370;
                        ScreenShotBG.Margin = new Thickness(20, 0, 0, 20);
                        WrapPanel ScreenWrap = new WrapPanel();
                        ScreenWrap.Background = new SolidColorBrush(Colors.Transparent);
                        Image ScreenTitleImage = new Image();
                        ScreenTitleImage.Width = 15;
                        ScreenTitleImage.Height = 15;
                        ScreenTitleImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/screenshot-red64.ico"));
                        ScreenTitleImage.HorizontalAlignment = HorizontalAlignment.Left;
                        ScreenTitleImage.VerticalAlignment = VerticalAlignment.Top;
                        ScreenTitleImage.Margin = new Thickness(20, 16, 0, 0);
                        TextBlock ScreenTitleImageTB = new TextBlock();
                        ScreenTitleImageTB.Text = "Screen Shots";
                        ScreenTitleImageTB.Foreground = new SolidColorBrush(Colors.Black);
                        ScreenTitleImageTB.FontSize = 12;
                        ScreenTitleImageTB.FontWeight = FontWeights.Bold;
                        ScreenTitleImageTB.HorizontalAlignment = HorizontalAlignment.Left;
                        ScreenTitleImageTB.VerticalAlignment = VerticalAlignment.Top;
                        ScreenTitleImageTB.Margin = new Thickness(5, 15, 0, 0);
                        Line ScreenSepratorLine = new Line();
                        ScreenSepratorLine.VerticalAlignment = VerticalAlignment.Top;
                        ScreenSepratorLine.HorizontalAlignment = HorizontalAlignment.Left;
                        ScreenSepratorLine.Margin = new Thickness(15, 10, 0, 0);
                        ScreenSepratorLine.Stroke = new SolidColorBrush(Colors.LightGray);
                        ScreenSepratorLine.Opacity = 70;
                        ScreenSepratorLine.StrokeThickness = 1;
                        ScreenSepratorLine.X1 = 5;
                        ScreenSepratorLine.Y1 = 10;
                        ScreenSepratorLine.X2 = 570;
                        ScreenSepratorLine.Y2 = 10;
                        WrapPanel PicGalery = new WrapPanel();
                        PicGalery.VerticalAlignment = VerticalAlignment.Top;
                        PicGalery.HorizontalAlignment = HorizontalAlignment.Left;
                        ScreenShotSemaphore.WaitOne();
                        if (MainWindow.DS.Tables.Contains("ScreenShot") ==true)
                        {
                            MainWindow.DS.Tables.Remove("ScreenShot");
                        }
                        MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From ScreenShot where ChildID ='" + CurrentUser + "' order by Date desc", ref MainWindow.DS, "ScreenShot");
                        for (int i = 0;(i<MainWindow.DS.Tables["ScreenShot"].Rows.Count)&&(i < 3); i++)
                        {
                            Border PicBord = new Border();
                            PicBord.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 230, 230, 230));
                            PicBord.BorderBrush = new SolidColorBrush(Colors.Black);
                            PicBord.BorderThickness = new Thickness(0.5);
                            Image image = new Image();
                            MemoryStream ms;
                            byte[] PicData = Convert.FromBase64String(MainWindow.DS.Tables["ScreenShot"].Rows[i]["Picture"].ToString());
                            ms = new MemoryStream(PicData);
                            string Name = i + DateTime.Now.Millisecond + "Shot.jpeg";
                            System.Drawing.Bitmap BitImage = new System.Drawing.Bitmap(ms);
                            BitImage.Save(System.AppDomain.CurrentDomain.BaseDirectory + Name);
                            var MyBrush = new ImageBrush();
                            BitmapImage BImage = new BitmapImage();
                            BImage.BeginInit();
                            BImage.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory +Name);
                            BImage.EndInit();
                            image.Source = BImage;
                            image.Stretch = Stretch.Fill;
                            PicBord.Margin = new Thickness(0, 15, 20, 0);
                            image.Margin = new Thickness(2);
                            image.VerticalAlignment = VerticalAlignment.Top;
                            image.HorizontalAlignment = HorizontalAlignment.Left;
                            image.Width = 170;
                            image.Height = 270;
                            PicBord.Child = image;
                            PicGalery.Children.Add(PicBord);
                        }
                        MainWindow.DS.Tables.Remove("ScreenShot");
                        ScreenShotSemaphore.Release();
                        PicGalery.Margin = new Thickness(25, 0, 0, 10);
                        PicGalery.Width = 600;
                        PicGalery.Height = 270;
                        
                        ScreenWrap.Children.Add(ScreenTitleImage);
                        ScreenWrap.Children.Add(ScreenTitleImageTB);
                        ScreenWrap.Children.Add(ScreenSepratorLine);
                        ScreenWrap.Children.Add(PicGalery);
                        ScreenShotBG.Child = ScreenWrap;
                        OtherOptionsPanel.Children.Add(ScreenShotBG);


                        Border WebCamBG = new Border();
                        WebCamBG.Background = new SolidColorBrush(Colors.White);
                        WebCamBG.Width = 610;
                        WebCamBG.Height = 370;
                        WebCamBG.Margin = new Thickness(20, 0, 0, 20);
                        WrapPanel WebCamWrap = new WrapPanel();
                        WebCamWrap.Background = new SolidColorBrush(Colors.Transparent);
                        Image WebCamTitleImage = new Image();
                        WebCamTitleImage.Width = 15;
                        WebCamTitleImage.Height = 15;
                        WebCamTitleImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/screenshot-red64.ico"));
                        WebCamTitleImage.HorizontalAlignment = HorizontalAlignment.Left;
                        WebCamTitleImage.VerticalAlignment = VerticalAlignment.Top;
                        WebCamTitleImage.Margin = new Thickness(20, 16, 0, 0);
                        TextBlock WebcamTitleImageTB = new TextBlock();
                        WebcamTitleImageTB.Text = "WebCam";
                        WebcamTitleImageTB.Foreground = new SolidColorBrush(Colors.Black);
                        WebcamTitleImageTB.FontSize = 12;
                        WebcamTitleImageTB.FontWeight = FontWeights.Bold;
                        WebcamTitleImageTB.HorizontalAlignment = HorizontalAlignment.Left;
                        WebcamTitleImageTB.VerticalAlignment = VerticalAlignment.Top;
                        WebcamTitleImageTB.Margin = new Thickness(5, 15, 0, 0);
                        Line WebCamSepratorLine = new Line();
                        WebCamSepratorLine.VerticalAlignment = VerticalAlignment.Top;
                        WebCamSepratorLine.HorizontalAlignment = HorizontalAlignment.Left;
                        WebCamSepratorLine.Margin = new Thickness(15, 10, 0, 0);
                        WebCamSepratorLine.Stroke = new SolidColorBrush(Colors.LightGray);
                        WebCamSepratorLine.Opacity = 70;
                        WebCamSepratorLine.StrokeThickness = 1;
                        WebCamSepratorLine.X1 = 5;
                        WebCamSepratorLine.Y1 = 10;
                        WebCamSepratorLine.X2 = 570;
                        WebCamSepratorLine.Y2 = 10;
                        WrapPanel WebPicGalery = new WrapPanel();
                        WebPicGalery.VerticalAlignment = VerticalAlignment.Top;
                        WebPicGalery.HorizontalAlignment = HorizontalAlignment.Left;
                        WebCamTableSemaphore.WaitOne();
                        if (MainWindow.DS.Tables.Contains("WebCamTable") == true)
                        {
                            MainWindow.DS.Tables.Remove("WebCamTable");
                        }
                        MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From WebCamTable where ChildID ='"+ChildUser + "' order by Date desc",ref MainWindow.DS, "WebCamTable");
                        for (int i = 0; (i < MainWindow.DS.Tables["WebCamTable"].Rows.Count) &&( i < 3); i++)
                        {
                            Border PicBord = new Border();
                            PicBord.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 230, 230, 230));
                            PicBord.BorderBrush = new SolidColorBrush(Colors.Black);
                            PicBord.BorderThickness = new Thickness(0.5);
                            Image image = new Image();
                            MemoryStream ms;
                            byte[] PicData = Convert.FromBase64String(MainWindow.DS.Tables["WebCamTable"].Rows[i]["Pic"].ToString());
                            ms = new MemoryStream(PicData);
                            System.Drawing.Bitmap BitImage = new System.Drawing.Bitmap(ms);
                            string Name = i + DateTime.Now.Millisecond + "Web.jpeg";
                            BitImage.Save(System.AppDomain.CurrentDomain.BaseDirectory + Name);
                            var MyBrush = new ImageBrush();
                            BitmapImage BImage = new BitmapImage();
                            BImage.BeginInit();
                            BImage.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + Name);
                            BImage.EndInit();
                            image.Source = BImage;
                            image.Stretch = Stretch.Fill;
                            PicBord.Margin = new Thickness(0, 15, 20, 0);
                            image.Margin = new Thickness(2);
                            image.VerticalAlignment = VerticalAlignment.Top;
                            image.HorizontalAlignment = HorizontalAlignment.Left;
                            image.Width = 170;
                            image.Height = 270;
                            PicBord.Child = image;
                            WebPicGalery.Children.Add(PicBord);
                        }
                        MainWindow.DS.Tables.Remove("WebCamTable");
                        WebCamTableSemaphore.Release();
                        WebPicGalery.Margin = new Thickness(25, 0, 0, 10);
                        WebPicGalery.Width = 600;
                        WebPicGalery.Height = 270;

                        WebCamWrap.Children.Add(WebCamTitleImage);
                        WebCamWrap.Children.Add(WebcamTitleImageTB);
                        WebCamWrap.Children.Add(WebCamSepratorLine);
                        WebCamWrap.Children.Add(WebPicGalery);
                        WebCamBG.Child = WebCamWrap;
                        OtherOptionsPanel.Children.Add(WebCamBG);
                    
                        MainDataGrid.Children.Add(OtherOptionsPanel);
                        DeviceInfoGrid = DeviceInfo;
                        ((TextBlock)((Border)DeviceInfo.Children.Cast<UIElement>().First(x => x.Uid == "DeviceNameTXT")).Child).Text = "Descktop";
                        try
                        {
                            string Data= MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select top (1) OSName From Process where ChildID ='" + ChildUser + "' order by EndTime DESC").ToString();
                            ((TextBlock)((Border)DeviceInfo.Children.Cast<UIElement>().First(x => x.Uid == "OSTypeTXT")).Child).Text = Data;
                            
                        }
                        catch(Exception E)
                        {
                            ((TextBlock)((Border)DeviceInfo.Children.Cast<UIElement>().First(x => x.Uid == "OSTypeTXT")).Child).Text = "";
                        }
                        try
                        {
                            ((TextBlock)((Border)DeviceInfo.Children.Cast<UIElement>().First(x => x.Uid == "ValidDateTXT")).Child).Text =
                            MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select top (1) EndTime From Process where ChildID ='" + ChildUser + "' order by EndTime DESC").ToString();
                        }
                        catch(Exception E)
                        {
                            ((TextBlock)((Border)DeviceInfo.Children.Cast<UIElement>().First(x => x.Uid == "ValidDateTXT")).Child).Text = "";
                        }
                        try
                        {
                            ((TextBlock)((Border)DeviceInfo.Children.Cast<UIElement>().First(x => x.Uid == "LicenseTXT")).Child).Text =
                            MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select top (1) LicenceKey From License where ChildID ='" + ChildUser + "'").ToString();
                        }
                        catch(Exception E)
                        {
                            ((TextBlock)((Border)DeviceInfo.Children.Cast<UIElement>().First(x => x.Uid == "LicenseTXT")).Child).Text = "";
                        }
                        try
                        {
                            ((TextBlock)((Border)DeviceInfo.Children.Cast<UIElement>().First(x => x.Uid == "WifiTXT")).Child).Text =
                            MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select top (1) Status From NetworkAdaptor where ChildID ='" + ChildUser + "' AND DeviceName = 'Wi-Fi'").ToString();
                        }
                        catch(Exception E)
                        {
                            ((TextBlock)((Border)DeviceInfo.Children.Cast<UIElement>().First(x => x.Uid == "WifiTXT")).Child).Text = "";
                        }
                        try
                        {
                            ((TextBlock)((Border)DeviceInfo.Children.Cast<UIElement>().First(x => x.Uid == "ProcessedTXT")).Child).Text = DateTime.Now.ToLongDateString();
                            
                        }
                        catch (Exception E)
                        {
                            ((TextBlock)((Border)DeviceInfo.Children.Cast<UIElement>().First(x => x.Uid == "ProcessedTXT")).Child).Text = "";
                        }



                    };
                    break;
                case 1:
                    {
                        NewLogs[2] = 0;
                        MainTh = Thread.CurrentThread;
                        MainDataGrid.Children.Clear();
                        MainDataGrid.ColumnDefinitions.Clear();
                        MainDataGrid.RowDefinitions.Clear();
                        TextBlock TitleTB2 = new TextBlock();
                        TitleTB2.Text = "Applications";
                        TitleTB2.FontSize = 24;
                        TitleTB2.Foreground = new SolidColorBrush(Colors.Gray);
                        TitleTB2.Margin = new Thickness(20, -30, 0, 10);
                        TitleTB2.VerticalAlignment = VerticalAlignment.Top;
                        TitleTB2.HorizontalAlignment = HorizontalAlignment.Left;
                        MainDataGrid.Children.Add(TitleTB2);
                        WrapPanel MainTemp = new WrapPanel();
                        MainTemp.Orientation = Orientation.Vertical;
                        MainTemp.Margin = new Thickness(0);
                        MainTemp.HorizontalAlignment = HorizontalAlignment.Left;
                        Border BG = new Border();
                        BG.Background = new SolidColorBrush(Colors.White);
                        BG.Margin = new Thickness(20, 20, 20, 50);
                        BG.VerticalAlignment = VerticalAlignment.Top;
                        BG.Child = MainTemp;
                        MainDataGrid.Children.Add(BG);
                        ListView InstalledApp = new ListView();
                        InstalledApp.Uid = "InstalledAppsList";
                        TextBlock TitleTB0 = new TextBlock();
                        TitleTB0.Text = "Installed Applications";
                        TitleTB0.FontSize = 24;
                        TitleTB0.Foreground = new SolidColorBrush(Colors.Gray);
                        TitleTB0.Margin = new Thickness(30, 30, 0, 0);
                        TitleTB0.VerticalAlignment = VerticalAlignment.Top;
                        TitleTB0.HorizontalAlignment = HorizontalAlignment.Left;
                        MainTemp.Children.Add(TitleTB0);
                        Image InfoImage0 = new System.Windows.Controls.Image();
                        InfoImage0.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_info-blog_46810.ico"));
                        InfoImage0.Width = 30;
                        InfoImage0.Height = 30;
                        InfoImage0.VerticalAlignment = VerticalAlignment.Top;
                        InfoImage0.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoImage0.Margin = new Thickness(30, 40, 0, 0);
                        
                        MainTemp.Children.Add(InfoImage0);
                        TextBlock InfoTb0 = new TextBlock();
                        InfoTb0.Text = "   You can show all installed application in your child Sysytem and remove Whatever you want";
                        InfoTb0.FontSize = 16;
                        InfoTb0.VerticalAlignment = VerticalAlignment.Top;
                        InfoTb0.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoTb0.Foreground = new SolidColorBrush(Colors.Black);
                        InfoTb0.Margin = new Thickness(30,5,0,0);
                        MainTemp.Children.Add(InfoTb0);

                        InstalledAppsSemaphore.WaitOne();
                        if (MainWindow.DS.Tables.Contains("InstalledApps") == true)
                        {
                            MainWindow.DS.Tables.Remove("InstalledApps");
                        }
                        MainWindow.DataBaseAgent.SelectData("InstalledApps", ref MainWindow.DS, "*", "InstalledApps", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
                        double[] ColumnWidth = { 350, 200, 200, 350 };
                        string[] HeadersName = { "Name", "Installed Date", "Version", "Publisher"};
                        string[] ColumnsName = { "DisplayName", "InstallDate", "DisplayVersion", "Publisher" };
                        ColorList NetworkList = new ColorList();
                        Size ItemSize = new Size(0, 25);
                        Size ListSize = new Size(1100, 0);
                        Border border = NetworkList.Draw(ColumnWidth, 30, HeadersName, null, ListSize, ItemSize, Color.FromArgb(255, 238, 241, 245), Colors.Transparent,
                            Colors.White, Colors.Black, Color.FromArgb(255, 42, 180, 192), Colors.LightGray, Colors.Transparent, MainWindow.DS.Tables["InstalledApps"],
                            new Thickness(50, 30, 15, 20), new Thickness(0), 12, 16, ColumnsName, InstalledApp_SelectionChanged, "InstalledAppsList", 300);
                        border.VerticalAlignment = VerticalAlignment.Top;
                        border.HorizontalAlignment = HorizontalAlignment.Left;
                        MainTemp.Children.Add(border);
                       
                        Button Uninstall = new Button();
                        //Uninstall.Content = " Uninstall";
                        ImageBrush Image = new ImageBrush();
                        Image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/Igh0zt-Ios7-Style-Metro-Ui-MetroUI-Apps-CCleaner.ico"));
                        Uninstall.Background = Image;
                        Uninstall.Cursor = Cursors.Hand;
                        Uninstall.BorderThickness = new Thickness(0);
                        Uninstall.Margin = new Thickness(500, -40, 30, 15);
                        Uninstall.MinWidth = 60;
                        Uninstall.MaxHeight = 60;
                        Uninstall.IsEnabled = false;
                        Uninstall.HorizontalAlignment = HorizontalAlignment.Left;
                        Uninstall.VerticalAlignment = VerticalAlignment.Top;
                        Uninstall.Click += UninstallClick;
                        Uninstall.Loaded += Uninstall_Loaded;
                        Uninstall.MinHeight = 60;
                        Uninstall.Uid = "UninstallInstallAppBtn";

                        TextBlock InfoTb1 = new TextBlock();
                        InfoTb1.Text = "Please select one of Applications then click the front button ...";
                        InfoTb1.FontSize = 16;
                        InfoTb1.VerticalAlignment = VerticalAlignment.Top;
                        InfoTb1.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoTb1.Foreground = new SolidColorBrush(Colors.Black);
                        InfoTb1.Margin = new Thickness(50, 35, 0, 0);
                        MainTemp.Children.Add(InfoTb1);
                        MainTemp.Children.Add(Uninstall);
                        Expander MoreApp = new Expander();
                        MoreApp.HorizontalAlignment = HorizontalAlignment.Left;
                        MoreApp.VerticalAlignment = VerticalAlignment.Top;
                        MoreApp.Header = "More";
                        MoreApp.Expanded += MoreApp_Expanded;
                        MoreApp.Collapsed += MoreApp_Collapsed;
                        MoreApp.Margin = new Thickness(30, 10 , 0, 20);
                        
                        foreach (System.Data.DataRow Row in MainWindow.DS.Tables["InstalledApps"].Rows)
                        {
                            InstalledApp.Items.Add(new InstalledApp
                            {
                                DisplayName = Row["DisplayName"].ToString(),
                                DisplayVersion = Row["DisplayVersion"].ToString(),
                                InstallDate = Row["InstallDate"].ToString(),
                                Publisher = Row["Publisher"].ToString()
                            });
                        }
                        MainWindow.DS.Tables.Remove("InstalledApps");
                        InstalledAppsSemaphore.Release();
                    }
                    break;
                case 2:
                    {
                        
                        MainDataGrid.Children.Clear();
                        MainDataGrid.ColumnDefinitions.Clear();
                        MainDataGrid.RowDefinitions.Clear();
                        TextBlock TitleTB2 = new TextBlock();
                        TitleTB2.Text = "Time Limition";
                        TitleTB2.FontSize = 24;
                        TitleTB2.Foreground = new SolidColorBrush(Colors.Gray);
                        TitleTB2.Margin = new Thickness(20, -30, 0, 10);
                        TitleTB2.VerticalAlignment = VerticalAlignment.Top;
                        TitleTB2.HorizontalAlignment = HorizontalAlignment.Left;
                        MainDataGrid.Children.Add(TitleTB2);
                        Grid MainTemp = new Grid();
                        MainTemp.Margin = new Thickness(0);
                        Border BG = new Border();
                        BG.Background = new SolidColorBrush(Colors.White);
                        BG.Margin = new Thickness(20, 20, 20, 50);
                        BG.VerticalAlignment = VerticalAlignment.Top;
                        BG.Child = MainTemp;
                        MainDataGrid.Children.Add(BG);
                        Image InfoImage0 = new System.Windows.Controls.Image();
                        InfoImage0.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_info-blog_46810.ico"));
                        InfoImage0.Width = 30;
                        InfoImage0.Height = 30;
                        InfoImage0.VerticalAlignment = VerticalAlignment.Top;
                        InfoImage0.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoImage0.Margin = new Thickness(30, 20, 0, 0);
                        MainTemp.Children.Add(InfoImage0);
                        TextBlock InfoTb0 = new TextBlock();
                        InfoTb0.Text = "You can show and Control Limits on Network , Application and System usage ";
                        InfoTb0.FontSize = 16;
                        InfoTb0.VerticalAlignment = VerticalAlignment.Top;
                        InfoTb0.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoTb0.Foreground = new SolidColorBrush(Colors.Black);
                        InfoTb0.Margin = new Thickness(50, 45, 0, 0);
                        MainTemp.Children.Add(InfoTb0);
                        Image InfoImage1 = new System.Windows.Controls.Image();
                        InfoImage1.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_info-blog_46810.ico"));
                        InfoImage1.Width = 30;
                        InfoImage1.Height = 30;
                        InfoImage1.VerticalAlignment = VerticalAlignment.Top;
                        InfoImage1.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoImage1.Margin = new Thickness(30, 90, 0, 0);
                        MainTemp.Children.Add(InfoImage1);
                        TextBlock InfoTb1 = new TextBlock();
                        InfoTb1.Text = "First choose Limit Type then you can show Limitaions in that ";
                        InfoTb1.FontSize = 16;
                        InfoTb1.VerticalAlignment = VerticalAlignment.Top;
                        InfoTb1.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoTb1.Foreground = new SolidColorBrush(Colors.Black);
                        InfoTb1.Margin = new Thickness(50, 120, 0, 0);
                        MainTemp.Children.Add(InfoTb1);
                        LimitList = new ColorList();
                        Size ItemSize = new Size(0, 25);
                        Size ListSize = new Size(600, 0);
                        double[] ColumnWidth = { 200, 100, 100, 100, 100 };
                        string[] HeadersName = { "Name", "Start Time", "End Time", "Duration", "Action" };
                        string[] ColumnsName = { "ID", "StartTime", "EndTime", "Duration", "Act" };
                        if(MainWindow.DS.Tables.Contains("SystemLimit") == true)
                        {
                            MainWindow.DS.Tables.Remove("SystemLimit");
                        }
                        MainWindow.DataBaseAgent.SelectData("SystemLimit", ref MainWindow.DS, "SystemLimit");
                        Border border = LimitList.Draw(ColumnWidth, 30, HeadersName, null, ListSize, ItemSize, Color.FromArgb(255, 238, 241, 245), Colors.Transparent,
                            Colors.White, Colors.Black, Color.FromArgb(255, 42, 180, 192), Colors.LightGray, Colors.Transparent, MainWindow.DS.Tables["SystemLimit"],
                            new Thickness(30, 165, 10, 10), new Thickness(0), 12, 16, ColumnsName, LimitSelectionChange, "ShowLimitList", 300);
                        border.VerticalAlignment = VerticalAlignment.Top;
                        border.HorizontalAlignment = HorizontalAlignment.Left;
                        MainTemp.Children.Add(border);
                        if (MainWindow.DS.Tables.Contains("SystemLimit") == true)
                        {
                            MainWindow.DS.Tables.Remove("SystemLimit");
                        }

                        ComboBox LimitTypeCombo = new ComboBox();
                        TextBlock LimitTypeBlock = new TextBlock();
                        
                        LimitTypeCombo.Uid = "LimitTypeCombo";
                        LimitTypeCombo.MinWidth = 100;
                        
                        LimitTypeBlock.Text = "Limitaion Type :";
                        LimitTypeCombo.Items.Add("System Limitaion");
                        LimitTypeCombo.Items.Add("App Limitaion");
                        LimitTypeCombo.Items.Add("Network Limitaion");
                        LimitTypeCombo.SelectedIndex = 0;
                        LimitTypeCombo.SelectionChanged += ComboLimitSelectionChange;
                        LimitTypeCombo.Margin = new Thickness(250, 210, 10, 0);
                        LimitTypeBlock.Margin = new Thickness(250, 180, 10, 0);
                        LimitTypeBlock.Foreground = new SolidColorBrush(Colors.Black);
                        LimitTypeBlock.VerticalAlignment = VerticalAlignment.Top;
                        LimitTypeBlock.HorizontalAlignment = HorizontalAlignment.Left;
                        LimitTypeBlock.Loaded += LimitTypeBlock_Loaded;
                        LimitTypeBlock.FontSize = 14;
                        LimitTypeBlock.Uid = "LimitaionTypeTxt";
                        LimitTypeCombo.VerticalAlignment = VerticalAlignment.Top;
                        LimitTypeCombo.HorizontalAlignment = HorizontalAlignment.Left;
                        LimitTypeCombo.Loaded += LimitTypeCombo_Loaded;
                        MainTemp.Children.Add(LimitTypeCombo);
                        MainTemp.Children.Add(LimitTypeBlock);
                        
                        Expander MoreLimitExpander = new Expander();
                        MoreLimitExpander.FontSize = 14;
                        MoreLimitExpander.Header = "More Options";
                        MoreLimitExpander.VerticalAlignment = VerticalAlignment.Top;
                        MoreLimitExpander.HorizontalAlignment = HorizontalAlignment.Left;
                        MoreLimitExpander.Loaded += MoreLimitExpander_Loaded;
                        MoreLimitExpander.Margin = new Thickness(30, 20, 20, 40);
                        MoreLimitExpander.Collapsed += MoreLimitExpander_Collapsed;
                        MoreLimitExpander.Expanded += MoreLimitExpander_Expanded;
                        MainTemp.Children.Add(MoreLimitExpander);
     
                        
                    }
                    break;
                case 3:
                    {
                        NewLogs[1] = 0;
                        MainDataGrid.Children.Clear();
                        TextBlock TitleTB2 = new TextBlock();
                        TitleTB2.Text = "Location";
                        TitleTB2.FontSize = 24;
                        TitleTB2.Foreground = new SolidColorBrush(Colors.Gray);
                        TitleTB2.Margin = new Thickness(20, -30, 0, 10);
                        TitleTB2.VerticalAlignment = VerticalAlignment.Top;
                        TitleTB2.HorizontalAlignment = HorizontalAlignment.Left;
                        MainDataGrid.Children.Add(TitleTB2);
                        WrapPanel MainTemp = new WrapPanel();
                        MainTemp.Margin = new Thickness(0);
                        Border BG = new Border();
                        BG.Background = new SolidColorBrush(Colors.White);
                        BG.Margin = new Thickness(20, 20, 20, 50);
                        BG.VerticalAlignment = VerticalAlignment.Top;
                        BG.Child = MainTemp;
                        MainDataGrid.Children.Add(BG);
                        TextBlock TitleTB0 = new TextBlock();
                        TitleTB0.Text = "Current Location";
                        TitleTB0.FontSize = 24;
                        TitleTB0.Foreground = new SolidColorBrush(Colors.Gray);
                        TitleTB0.Margin = new Thickness(30, 20, 0, 10);
                        TitleTB0.VerticalAlignment = VerticalAlignment.Top;
                        TitleTB0.HorizontalAlignment = HorizontalAlignment.Left;
                        MainTemp.Children.Add(TitleTB0);
                        Image InfoImage1 = new System.Windows.Controls.Image();
                        InfoImage1.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_info-blog_46810.ico"));
                        InfoImage1.Width = 30;
                        InfoImage1.Height = 30;
                        InfoImage1.VerticalAlignment = VerticalAlignment.Top;
                        InfoImage1.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoImage1.Margin = new Thickness(-180, 80, 0, 0);
                        MainTemp.Children.Add(InfoImage1);
                        TextBlock InfoTb1 = new TextBlock();
                        InfoTb1.Text = "You can show location of your child in this Page For visit history of locatons Please Expand \"History\"";
                        InfoTb1.FontSize = 16;

                        InfoTb1.VerticalAlignment = VerticalAlignment.Top;
                        InfoTb1.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoTb1.Foreground = new SolidColorBrush(Colors.Black);
                        InfoTb1.Margin = new Thickness(-150, 105, 0, 0);
                        MainTemp.Children.Add(InfoTb1);
                        WebBrowser GoogleMap = new WebBrowser();
                        GoogleMap.Margin = new Thickness(30, 30, 30, 0);
                        GoogleMap.Uid = "Google";
                        GoogleMap.Height = 500;
                        GoogleMap.Source = new Uri("https://www.google.com/maps");
                        GoogleMap.LoadCompleted += GoogleMap_LoadCompleted;
                        MainTemp.Children.Add(GoogleMap);
                        History = new Expander();
                        History.Header = "History";
                        History.Margin = new Thickness(30, 10, 0, 20);
                        History.VerticalAlignment = VerticalAlignment.Top;
                        History.Expanded += ExpanderHistoryClick;
                        History.Collapsed += ExpanderHistoryClick;
                        MainTemp.Children.Add(History);
                    }
                    break;
                case 4:
                    {
                        NewLogs[8] = 0;
                        MainDataGrid.Children.Clear();
                        MainDataGrid.ColumnDefinitions.Clear();
                        MainDataGrid.RowDefinitions.Clear();
                        TextBlock TitleTB2 = new TextBlock();
                        TitleTB2.Text = "Running Apps";
                        TitleTB2.FontSize = 24;
                        TitleTB2.Foreground = new SolidColorBrush(Colors.Gray);
                        TitleTB2.Margin = new Thickness(20, -30, 0, 10);
                        TitleTB2.VerticalAlignment = VerticalAlignment.Top;
                        TitleTB2.HorizontalAlignment = HorizontalAlignment.Left;
                        MainDataGrid.Children.Add(TitleTB2);
                        WrapPanel MainTemp = new WrapPanel();
                        MainTemp.Margin = new Thickness(0);
                        Border BG = new Border();
                        BG.Background = new SolidColorBrush(Colors.White);
                        BG.Margin = new Thickness(20, 20, 20, 50);
                        BG.VerticalAlignment = VerticalAlignment.Top;
                        BG.Child = MainTemp;
                        MainDataGrid.Children.Add(BG);
                        TextBlock TitleTB0 = new TextBlock();
                        TitleTB0.Text = "Running";
                        TitleTB0.FontSize = 24;
                        TitleTB0.Foreground = new SolidColorBrush(Colors.Gray);
                        TitleTB0.Margin = new Thickness(30, 20, 0, 10);
                        TitleTB0.VerticalAlignment = VerticalAlignment.Top;
                        TitleTB0.HorizontalAlignment = HorizontalAlignment.Left;
                        MainTemp.Children.Add(TitleTB0);
                        Image InfoImage1 = new System.Windows.Controls.Image();
                        InfoImage1.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_info-blog_46810.ico"));
                        InfoImage1.Width = 30;
                        InfoImage1.Height = 30;
                        InfoImage1.VerticalAlignment = VerticalAlignment.Top;
                        InfoImage1.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoImage1.Margin = new Thickness(-90, 90, 0, 0);
                        MainTemp.Children.Add(InfoImage1);
                        TextBlock InfoTb1 = new TextBlock();
                        InfoTb1.Text = "You can show all running app in child divices";
                        InfoTb1.FontSize = 16;
                        InfoTb1.VerticalAlignment = VerticalAlignment.Top;
                        InfoTb1.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoTb1.Foreground = new SolidColorBrush(Colors.Black);
                        InfoTb1.Margin = new Thickness(-60, 115, 0, 0);
                        MainTemp.Children.Add(InfoTb1);
                        
                        Image InfoImage0 = new System.Windows.Controls.Image();
                        InfoImage0.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_info-blog_46810.ico"));
                        InfoImage0.Width = 30;
                        InfoImage0.Height = 30;
                        InfoImage0.VerticalAlignment = VerticalAlignment.Top;
                        InfoImage0.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoImage0.Margin = new Thickness(-345, 170, 0, 0);
                        MainTemp.Children.Add(InfoImage0);
                        TextBlock InfoTb0 = new TextBlock();
                        InfoTb0.Text = "For close an running application please select one of them in blow list then Click Action button";
                        InfoTb0.FontSize = 16;
                        InfoTb0.VerticalAlignment = VerticalAlignment.Top;
                        InfoTb0.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoTb0.Foreground = new SolidColorBrush(Colors.Black);
                        InfoTb0.Margin = new Thickness(-320, 195, 0, 0);
                        MainTemp.Children.Add(InfoTb0);
                        GridView RunningAppsGV = new GridView();
                        RunningAppsGV.Columns.Add(new GridViewColumn { Header = "Application Name", Width = 130, DisplayMemberBinding = new Binding("Name") });
                        RunningAppsGV.Columns.Add(new GridViewColumn { Header = "Start Time", Width = 150, DisplayMemberBinding = new Binding("StartTime") });
                        ListView RunningAppsList = new ListView();
                        RunningAppsList.Uid = "RunningAppList";
                        RunningAppsList.View = RunningAppsGV;
                        RunningAppsList.HorizontalAlignment = HorizontalAlignment.Left;
                        RunningAppsList.VerticalAlignment = VerticalAlignment.Top;
                        RunningAppsList.Margin = new Thickness(-670, 260, 20, 10);
                        RunningAppsList.SelectionChanged += RunningAppsSelectChange;
                        RunningAppsList.MinWidth = 450;
                        RunningAppsList.MaxWidth = 450;
                        RunningAppsList.MinHeight = 250;
                        RunningAppsList.MaxHeight = 250;
                        RunningAppsSemaphore.WaitOne();
                        MainWindow.DataBaseAgent.SelectData("RunningApps", ref MainWindow.DS, "*", "RunningApps", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
                        foreach (System.Data.DataRow Row in MainWindow.DS.Tables["RunningApps"].Rows)
                        {
                            RunningAppsList.Items.Add(new RunningApp { Name = Row["Name"].ToString().Split('-')[0], StartTime = Row["StartTime"].ToString() });
                        }
                        MainWindow.DS.Tables.Remove("RunningApps");
                        RunningAppsSemaphore.Release();
                        ReLoad = new System.Timers.Timer(1000*5 );
                        ReLoad.Elapsed += ReloadTimerElipced;
                        ReLoad.Start();
                        MainTemp.Children.Add(RunningAppsList);
                        TextBlock RunningAppsBlock = new TextBlock();
                        RunningAppsBlock.Text = "Action Type:";
                        RunningAppsBlock.Margin = new Thickness(-100, 280, 5, 10);
                        RunningAppsBlock.HorizontalAlignment = HorizontalAlignment.Left;
                        RunningAppsBlock.VerticalAlignment = VerticalAlignment.Top;
                        MainTemp.Children.Add(RunningAppsBlock);

                        ComboBox RunningAppAct = new ComboBox();
                        RunningAppAct.Margin = new Thickness(-100, 300, 5, 10);
                        RunningAppAct.Items.Add("Close");
                        RunningAppAct.SelectionChanged += RunningAppsActSelectionChange;
                        RunningAppAct.MinWidth = 100;
                        RunningAppAct.VerticalAlignment = VerticalAlignment.Top;
                        RunningAppAct.HorizontalAlignment = HorizontalAlignment.Left;
                        MainTemp.Children.Add(RunningAppAct);
                        ImageBrush ActionImage = new ImageBrush();
                        ActionImage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/action-64.ico"));
                        Button RunningAppsAction = new Button();
                        RunningAppsAction.Click += RunningActBtnClicked;
                        RunningAppsAction.Cursor = Cursors.Hand;
                        RunningAppsAction.BorderThickness = new Thickness(0);
                        RunningAppsAction.Background = ActionImage;
                        RunningAppsAction.Height = 60;
                        RunningAppsAction.Width = 60;
                        RunningAppsAction.Uid = "RunningAppsActionBtn";
                        RunningAppsAction.Template = MaxBtn.Template;
                        RunningAppsAction.Margin = new Thickness(10, 340, 10, 10);
                        RunningAppsAction.VerticalAlignment = VerticalAlignment.Top;
                        RunningAppsAction.HorizontalAlignment = HorizontalAlignment.Left;
                        MainTemp.Children.Add(RunningAppsAction);
                        Expander RunnigAppHistoryExpander = new Expander();
                        RunnigAppHistoryExpander.VerticalAlignment = VerticalAlignment.Top;
                        RunnigAppHistoryExpander.HorizontalAlignment = HorizontalAlignment.Left;
                        RunnigAppHistoryExpander.Header = "History";
                        RunnigAppHistoryExpander.Margin = new Thickness(-750, 550, 0, 20);
                        RunnigAppHistoryExpander.Collapsed += RunnigAppHistoryExpander_Collapsed;
                        RunnigAppHistoryExpander.Expanded += RunnigAppHistoryExpander_Expanded;
                        MainTemp.Children.Add(RunnigAppHistoryExpander);
                    }
                    break;
                case 7:
                    {
                        NewLogs[0] = 0;
                        MainDataGrid.Children.Clear();
                        MainDataGrid.ColumnDefinitions.Clear();
                        MainDataGrid.RowDefinitions.Clear();
                        TextBlock TitleTB2 = new TextBlock();
                        TitleTB2.Text = "Web History";
                        TitleTB2.FontSize = 24;
                        TitleTB2.Foreground = new SolidColorBrush(Colors.Gray);
                        TitleTB2.Margin = new Thickness(20, -30, 0, 10);
                        TitleTB2.VerticalAlignment = VerticalAlignment.Top;
                        TitleTB2.HorizontalAlignment = HorizontalAlignment.Left;
                        MainDataGrid.Children.Add(TitleTB2);
                        WrapPanel MainTemp = new WrapPanel();
                        MainTemp.Orientation = Orientation.Vertical;
                        MainTemp.Margin = new Thickness(0);
                        Border BG = new Border();
                        BG.Background = new SolidColorBrush(Colors.White);
                        BG.Margin = new Thickness(20, 20, 20, 50);
                        BG.VerticalAlignment = VerticalAlignment.Top;
                        BG.Child = MainTemp;
                        MainDataGrid.Children.Add(BG);
                        Image InfoImage0 = new System.Windows.Controls.Image();
                        InfoImage0.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_info-blog_46810.ico"));
                        InfoImage0.Width = 30;
                        InfoImage0.Height = 30;
                        InfoImage0.VerticalAlignment = VerticalAlignment.Top;
                        InfoImage0.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoImage0.Margin = new Thickness(30, 20, 0, 0);
                        MainTemp.Children.Add(InfoImage0);
                        TextBlock InfoTb0 = new TextBlock();
                        InfoTb0.Text = "You can show all visited URLs in blow list and more options in this page";
                        InfoTb0.FontSize = 16;
                        InfoTb0.VerticalAlignment = VerticalAlignment.Top;
                        InfoTb0.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoTb0.Foreground = new SolidColorBrush(Colors.Black);
                        InfoTb0.Margin = new Thickness(45, 0, 0, 0);
                        MainTemp.Children.Add(InfoTb0);

                        TextBlock HistoryListTb = new TextBlock();
                        HistoryListTb.Text = "WebSite History";
                        HistoryListTb.FontSize = 24;
                        HistoryListTb.Foreground = new SolidColorBrush(Colors.Gray);
                        HistoryListTb.VerticalAlignment = VerticalAlignment.Top;
                        HistoryListTb.HorizontalAlignment = HorizontalAlignment.Left;
                        HistoryListTb.Margin = new Thickness(30, 20, 0, 0);
                        MainTemp.Children.Add(HistoryListTb);
                        HistoryURLSemaphore.WaitOne();
                        if (MainWindow.DS.Tables.Contains("HistoryURL") == true)
                        {
                            MainWindow.DS.Tables.Remove("HistoryURL");
                        }
                        MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From HistoryURL where ChildID ='" + ChildsCombo.SelectedItem.ToString() + "' order By Date Desc" , ref MainWindow.DS, "HistoryURL");
                        double[] ColumnWidth = { 150, 900, 100 };
                        string[] HeadersName = { "Date", "Address", "Browser" };
                        string[] ColumnsName = { "Date", "URL", "Browser" };
                        ColorList NetworkList = new ColorList();
                        Size ItemSize = new Size(0, 25);
                        Size ListSize = new Size(1152, 0);
                        Border border = NetworkList.Draw(ColumnWidth, 30, HeadersName, null, ListSize, ItemSize, Color.FromArgb(255, 238, 241, 245), Colors.Transparent,
                            Colors.White, Colors.Black, Color.FromArgb(255, 42, 180, 192), Colors.LightGray, Colors.Transparent, MainWindow.DS.Tables["HistoryURL"],
                            new Thickness(50, 20, 15, 10), new Thickness(0), 12, 16, ColumnsName, null, "HistoryURLList",
                           300);
                        MainTemp.Children.Add(border);
                        MainWindow.DS.Tables.Remove("HistoryURL");
                        HistoryURLSemaphore.Release();
                        Expander MoreURLs = new Expander();
                        MoreURLs.Header = "More Details";
                        MoreURLs.HorizontalAlignment = HorizontalAlignment.Left;
                        MoreURLs.VerticalAlignment = VerticalAlignment.Top;
                        MoreURLs.Margin = new Thickness(30, 20, 0, 20);
                        MoreURLs.Expanded += MoreURLs_Expanded;
                        MoreURLs.Collapsed += MoreURLs_Collapsed;
                        MainTemp.Children.Add(MoreURLs);
                    }
                    break;
                case 8:
                    {

                        NewLogs[3] = 0;
                        MainDataGrid.Children.Clear();
                        MainDataGrid.ColumnDefinitions.Clear();
                        MainDataGrid.RowDefinitions.Clear();
                        TextBlock TitleTB2 = new TextBlock();
                        TitleTB2.Text = "Picturs";
                        TitleTB2.FontSize = 24;
                        TitleTB2.Foreground = new SolidColorBrush(Colors.Gray);
                        TitleTB2.Margin = new Thickness(20, -30, 0, 10);
                        TitleTB2.VerticalAlignment = VerticalAlignment.Top;
                        TitleTB2.HorizontalAlignment = HorizontalAlignment.Left;
                        MainDataGrid.Children.Add(TitleTB2);
                        Grid MainTemp = new Grid();
                        MainTemp.Margin = new Thickness(0);
                        Border BG = new Border();
                        BG.Background = new SolidColorBrush(Colors.White);
                        BG.Margin = new Thickness(20, 20, 20, 50);
                        BG.Child = MainTemp;
                        MainDataGrid.Children.Add(BG);
                        TextBlock TitleTB = new TextBlock();
                        TitleTB.Text = "Screen shot";
                        TitleTB.FontSize = 24;
                        TitleTB.Foreground = new SolidColorBrush(Colors.Gray);
                        TitleTB.Margin = new Thickness(20, 30, 0, 10);
                        TitleTB.VerticalAlignment = VerticalAlignment.Top;
                        TitleTB.HorizontalAlignment = HorizontalAlignment.Left;
                        MainTemp.Children.Add(TitleTB);
                        Image InfoImage0 = new System.Windows.Controls.Image();
                        InfoImage0.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_info-blog_46810.ico"));
                        InfoImage0.Width = 30;
                        InfoImage0.Height = 30;
                        InfoImage0.VerticalAlignment = VerticalAlignment.Top;
                        InfoImage0.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoImage0.Margin = new Thickness(10, 80, 0, 0);
                        MainTemp.Children.Add(InfoImage0);
                        TextBlock InfoTb0 = new TextBlock();
                        InfoTb0.Text = "You can show all screen shots and take screen shot in this page ";
                        InfoTb0.FontSize = 16; 
                        InfoTb0.VerticalAlignment = VerticalAlignment.Top;
                        InfoTb0.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoTb0.Foreground = new SolidColorBrush(Colors.Black);
                        InfoTb0.Margin = new Thickness(30, 105, 0, 0);
                        MainTemp.Children.Add(InfoTb0);
                        ScreenShotSemaphore.WaitOne();
                        MainWindow.DataBaseAgent.SelectData("ScreenShot", ref MainWindow.DS, "ScreenShot");
                        ScreenShotSemaphore.Release();
                        GridView ScreenGV = new GridView();
                        ScreenGV.Columns.Add(new GridViewColumn { Header = "Date", Width = 130, DisplayMemberBinding = new Binding("Date") });
                        RowDefinition ScreenRow0 = new RowDefinition();
                        RowDefinition ScreenRow1 = new RowDefinition();
                        RowDefinition ScreenRow2 = new RowDefinition();
                        ScreenRow0.Height = new GridLength(1, GridUnitType.Star);
                        ScreenRow1.Height = new GridLength(1, GridUnitType.Star);
                        ScreenRow2.Height = new GridLength(0.2, GridUnitType.Star);
                        ScreenRow0.MinHeight = 300;
                        ScreenRow1.MinHeight = 200;
                        ScreenRow2.MinHeight = 60;
                        MainTemp.RowDefinitions.Add(ScreenRow0);
                        MainTemp.RowDefinitions.Add(ScreenRow1);
                        MainTemp.RowDefinitions.Add(ScreenRow2);
                        ScreenShotImage = new Image();
                        Border ScreenShotBorder = new Border();
                        ScreenShotBorder.VerticalAlignment = VerticalAlignment.Top;

                        ScreenShotBorder.Margin = new Thickness(200,150,200,0);
                        ScreenShotBorder.BorderBrush = new SolidColorBrush(Colors.LightGray);
                        ScreenShotBorder.Height = 500;
                        ScreenShotBorder.BorderThickness = new Thickness(1);
                        ScreenShotBorder.Child = ScreenShotImage;
                        MainTemp.Children.Add(ScreenShotBorder);
                        Grid.SetRow(ScreenShotImage, 0);
                        StackPanel ImageList = new StackPanel();
                        ScrollViewer ImageListSV = new ScrollViewer();
                        Border ImageListBorder = new Border();
                        ImageList.Uid = "SCImageList";
                        ImageList.Orientation = Orientation.Horizontal;
                        ImageListSV.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
                        ImageListSV.Width = 1000;
                        ImageListSV.Height = 200;
                        ImageListBorder.Uid = "ImageListBorder";
                        ImageListBorder.Margin = new Thickness(70,-30, 100, 20);
                        ImageListBorder.Width = 1000;
                        ImageListBorder.Height = 100;
                        ImageListBorder.BorderBrush = new SolidColorBrush(Colors.LightGray);
                        ImageListBorder.BorderThickness = new Thickness(1);
                        ImageListSV.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                        ImageListSV.Template = SV.Template;
                        ScerrenShots = new List<Image>();
                        int NumberSC = 0;
                        ScreenShotSemaphore.WaitOne();
                        MainWindow.DataBaseAgent.SelectData("ScreenShot", ref MainWindow.DS, "*", "ScreenShot", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
                        foreach (System.Data.DataRow Row in MainWindow.DS.Tables["ScreenShot"].Rows)
                        {
                            StackPanel stackPanel = new StackPanel();
                            stackPanel.MouseEnter += StackPanel_MouseEnter;
                            stackPanel.MouseLeave += StackPanel_MouseLeave;
                            stackPanel.MouseLeftButtonUp += StackPanel_MouseLeftButtonUp;
                            TextBlock textBlock = new TextBlock();
                            textBlock.Text = Row["Date"].ToString();
                            stackPanel.Background = new SolidColorBrush(Colors.Transparent);
                            textBlock.Foreground = new SolidColorBrush(Colors.Blue);
                            textBlock.FontSize = 12;
                            stackPanel.Children.Add(textBlock);
                            stackPanel.Margin = new Thickness(5);
                            Image image = new Image();
                            MemoryStream ms;
                            byte[] PicData = Convert.FromBase64String((string)Row["Picture"]);
                            ms = new MemoryStream(PicData);
                            System.Drawing.Bitmap BitImage = new System.Drawing.Bitmap(ms);
                            BitImage.Save(System.AppDomain.CurrentDomain.BaseDirectory + NumberSC + "Shot.jpeg");
                            var MyBrush = new ImageBrush();
                            BitmapImage BImage = new BitmapImage();
                            BImage.BeginInit();
                            BImage.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + NumberSC + "Shot.jpeg");
                            BImage.EndInit();
                            image.Source = BImage;
                            image.Margin = new Thickness(5);
                            NumberSC++;
                            stackPanel.Children.Add(textBlock);
                            ImageList.Children.Add(stackPanel);
                            
                        }
                        ImageListSV.Content = ImageList;
                        ImageListBorder.Child = ImageListSV;
                        MainWindow.DS.Tables.Remove("ScreenShot");
                        ScreenShotSemaphore.Release();
                        ImageBrush RTakeImage = new ImageBrush();
                        RTakeImage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/screenshot-red64.ico"));
                        Button RemoveImageBtn = new Button();
                        RemoveImageBtn.BorderThickness = new Thickness(0);
                        RemoveImageBtn.Template = MBtn.Template;
                        RemoveImageBtn.Height = 40;
                        RemoveImageBtn.Width = 40;
                        ImageBrush RemoveImage = new ImageBrush();
                        RemoveImage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/trash-can.ico"));
                        RemoveImageBtn.Background = RemoveImage;
                        RemoveImageBtn.Uid = "RemoveImageBtn";
                        RemoveImageBtn.Cursor = Cursors.Hand;
                        RemoveImageBtn.Click += RemoveImageClick;
                        RemoveImageBtn.Margin = new Thickness(50, 260, 20, 15);
                        RemoveImageBtn.HorizontalAlignment = HorizontalAlignment.Left;
                        MainTemp.Children.Add(ImageListBorder);
                        Grid.SetRow(ImageListBorder, 1);
                        MainTemp.Children.Add(RemoveImageBtn);
                        Grid.SetRow(RemoveImageBtn, 1);
                        ImageBrush TakeImage = new ImageBrush();
                        TakeImage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/screenshot-red64.ico"));
                        Button Take = new Button();
                        Take.Click += TackeScreenShot;
                        Take.Template = MBtn.Template;
                        Take.Height = 40;
                        Take.Width = 40;
                        Take.Cursor = Cursors.Hand;
                        Take.BorderThickness = new Thickness(0);
                        Take.Background = TakeImage;
                        Take.HorizontalAlignment = HorizontalAlignment.Left;
                        Take.Margin = new Thickness(150, 260, 20, 15);
                        MainTemp.Children.Add(Take);
                        Grid.SetRow(Take, 1);
                        TextBlock TitleTB1 = new TextBlock();
                        TitleTB1.Text = "Webcam";
                        TitleTB1.FontSize = 24;
                        TitleTB1.Foreground = new SolidColorBrush(Colors.Gray);
                        TitleTB1.Margin = new Thickness(20, 30, 0, 10);
                        TitleTB1.VerticalAlignment = VerticalAlignment.Top;
                        TitleTB1.HorizontalAlignment = HorizontalAlignment.Left;
                        MainTemp.Children.Add(TitleTB1);
                        WebcamImage = new Image();
                        Grid.SetRow(TitleTB1, 2);
                        Border WebCamBorder = new Border();
                        WebCamBorder.VerticalAlignment = VerticalAlignment.Top;
                        WebCamBorder.Margin = new Thickness(200, 90, 200, 0);
                        WebCamBorder.BorderBrush = new SolidColorBrush(Colors.LightGray);
                        WebCamBorder.Height = 500;
                        WebCamBorder.BorderThickness = new Thickness(1);
                        WebCamBorder.Child = WebcamImage;
                        MainTemp.Children.Add(WebCamBorder);
                        Grid.SetRow(WebCamBorder, 2);
                        StackPanel WebImageList = new StackPanel();
                        ScrollViewer WebImageListSV = new ScrollViewer();
                        Border WebImageListBorder = new Border();
                        WebImageList.Orientation = Orientation.Horizontal;
                        WebImageList.Uid = "WebPicImageList";
                        WebImageListSV.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
                        WebImageListSV.Width = 1000;
                        WebImageListSV.Height = 100;
                        WebImageListBorder.Margin = new Thickness(70, 500, 100, 20);
                        WebImageListBorder.Width = 1000;
                        WebImageListBorder.Uid = "WebImageListBorder";
                        WebImageListBorder.Height = 100;
                        WebImageListBorder.BorderBrush = new SolidColorBrush(Colors.LightGray);
                        WebImageListBorder.BorderThickness = new Thickness(1);
                        WebImageListSV.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                        WebImageListSV.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                        WebImageListSV.Template = SV.Template;
                        List <Image> WebPicss = new List<Image>();
                        WebCamTableSemaphore.WaitOne();
                        int NumberWeb = 0;
                        MainWindow.DataBaseAgent.SelectData("WebCamTable", ref MainWindow.DS, "*", "WebCamTable", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
                        foreach (System.Data.DataRow Row in MainWindow.DS.Tables["WebCamTable"].Rows)
                        {
                            StackPanel stackPanel = new StackPanel();
                            stackPanel.MouseEnter += StackPanel_MouseEnter;
                            stackPanel.MouseLeave += StackPanel_MouseLeave;
                            stackPanel.MouseLeftButtonUp += StackPanel_MouseLeftButtonUp;
                            TextBlock textBlock = new TextBlock();
                            textBlock.Text = Row["Date"].ToString();
                            textBlock.Foreground = new SolidColorBrush(Colors.Blue);
                            textBlock.FontSize = 12;
                            stackPanel.Background = new SolidColorBrush(Colors.Transparent);
                            stackPanel.Margin = new Thickness(5);
                            Image image = new Image();
                            try
                            {
                                byte[] PicData = Convert.FromBase64String((string)Row["Pic"]);
                                using (MemoryStream ms = new MemoryStream(PicData))
                                {
                                    
                                    System.Drawing.Bitmap BitImage = new System.Drawing.Bitmap(ms);
                                    BitImage.Save(System.AppDomain.CurrentDomain.BaseDirectory + NumberWeb + "Web.jpeg");
                                }
                                    
                            }
                            catch (Exception E)
                            {

                            }
                            
                            var MyBrush = new ImageBrush();
                            BitmapImage BImage = new BitmapImage();
                            BImage.BeginInit();
                            BImage.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + NumberWeb + "Web.jpeg");
                            BImage.EndInit();
                            image.Source = BImage;
                            NumberWeb++;
                            image.Width = 70;
                            image.Height = 50;
                            image.Margin = new Thickness(5);
                            stackPanel.Children.Add(image);
                            stackPanel.Children.Add(textBlock);
                            WebImageList.Children.Add(stackPanel);
                        }
                        WebImageListSV.Content = WebImageList;
                        WebImageListBorder.Child = WebImageListSV;
                        MainWindow.DS.Tables.Remove("WebCamTable");
                        WebCamTableSemaphore.Release();
                        MainTemp.Children.Add(WebImageListBorder);
                        Grid.SetRow(WebImageListBorder, 2);
                        ImageBrush RTakeWebImage = new ImageBrush();
                        RTakeWebImage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/screenshot-red64.ico"));
                        Button RemoveWebImageBtn = new Button();
                        RemoveWebImageBtn.BorderThickness = new Thickness(0);
                        RemoveWebImageBtn.Template = MBtn.Template;
                        RemoveWebImageBtn.Height = 40;
                        RemoveWebImageBtn.Cursor = Cursors.Hand;
                        RemoveWebImageBtn.Width = 40;
                        ImageBrush RemoveWebImage = new ImageBrush();
                        RemoveWebImage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/trash-can.ico"));
                        RemoveWebImageBtn.Background = RemoveWebImage;
                        RemoveWebImageBtn.Uid = "RemoveWebImageBtn";
                        //RemoveWebImageBtn.IsEnabled = false;
                        RemoveWebImageBtn.Click += RemoveWebImageClick;
                        RemoveWebImageBtn.Margin = new Thickness(50, 820, 20, 15);
                        RemoveWebImageBtn.HorizontalAlignment = HorizontalAlignment.Left;
                        Button TakeWeb = new Button();
                        TakeWeb.Click += TackeWeb;
                        TakeWeb.Template = MBtn.Template;
                        TakeWeb.Height = 40;
                        TakeWeb.Width = 40;
                        TakeWeb.Cursor = Cursors.Hand;
                        TakeWeb.BorderThickness = new Thickness(0);
                        TakeWeb.Background = TakeImage;
                        TakeWeb.HorizontalAlignment = HorizontalAlignment.Left;
                        TakeWeb.Margin = new Thickness(150, 820, 20, 15);
                        MainTemp.Children.Add(RemoveWebImageBtn);
                        Grid.SetRow(RemoveWebImageBtn, 2);
                        MainTemp.Children.Add(TakeWeb);
                        Grid.SetRow(TakeWeb, 2);
                        UpDateUIData = new ParameterizedThreadStart(PictureUpDate);
                    }
                    break;
                case 9:
                    {
                        NewLogs[4] = 0;
                        MainDataGrid.Children.Clear();
                        MainDataGrid.ColumnDefinitions.Clear();
                        MainDataGrid.RowDefinitions.Clear();
                        TextBlock TitleTB2 = new TextBlock();
                        TitleTB2.Text = "Network ";
                        TitleTB2.FontSize = 24;
                        TitleTB2.Foreground = new SolidColorBrush(Colors.Gray);
                        TitleTB2.Margin = new Thickness(20, -30, 0, 10);
                        TitleTB2.VerticalAlignment = VerticalAlignment.Top;
                        TitleTB2.HorizontalAlignment = HorizontalAlignment.Left;
                        MainDataGrid.Children.Add(TitleTB2);
                        WrapPanel NetworkWrapPanel = new WrapPanel();
                        Border BG = new Border();
                        BG.Background = new SolidColorBrush(Colors.White);
                        BG.Margin = new Thickness(20, 20, 20, 50);
                        BG.VerticalAlignment = VerticalAlignment.Top;
                        NetworkWrapPanel.Margin = new Thickness(0, 0, 0, 0);
                        NetworkWrapPanel.Background = new SolidColorBrush(Colors.Transparent);
                        TextBlock TitleTB = new TextBlock();
                        TitleTB.Text = "Network Adapters";
                        TitleTB.FontSize = 24;
                        TitleTB.Foreground = new SolidColorBrush(Colors.Gray);
                        TitleTB.Margin = new Thickness(20, 20, 0, 10);
                        TitleTB.VerticalAlignment = VerticalAlignment.Top;
                        TitleTB.HorizontalAlignment = HorizontalAlignment.Left;
                        NetworkWrapPanel.Children.Add(TitleTB);
                        Image InfoImage0 = new System.Windows.Controls.Image();
                        InfoImage0.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_info-blog_46810.ico"));
                        InfoImage0.Width = 30;
                        InfoImage0.Height = 30;
                        InfoImage0.VerticalAlignment = VerticalAlignment.Top;
                        InfoImage0.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoImage0.Margin = new Thickness(-180, 70, 0, 0);
                        NetworkWrapPanel.Children.Add(InfoImage0);
                        TextBlock InfoTb0 = new TextBlock();
                        InfoTb0.Text = "You can show all network adapters , network connections and vpn connections in this page";
                        InfoTb0.FontSize = 16;
                        InfoTb0.VerticalAlignment = VerticalAlignment.Top;
                        InfoTb0.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoTb0.Foreground = new SolidColorBrush(Colors.Black);
                        InfoTb0.Margin = new Thickness(-150, 90, 0, 0);
                        NetworkWrapPanel.Children.Add(InfoTb0);
                        NetworkAdaptorSemaphor.WaitOne();
                        if (MainWindow.DS.Tables.Contains("NetworkAdaptor") == true)
                        {
                            MainWindow.DS.Tables.Remove("NetworkAdaptor");
                        }
                        
                        MainWindow.DataBaseAgent.SelectData("NetworkAdaptor", ref MainWindow.DS, "*", "NetworkAdaptor", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
                        double[] ColumnWidth = { 400, 500, 100 };
                        string[] HeadersName = { "Device Name", "Interface Name", "Status" };
                        string[] ColumnsName = { "DeviceName", "InterfaceName", "Status" };
                        NetworkList = new ColorList();
                        Size ItemSize = new Size(0, 25);
                        Size ListSize = new Size(1002,0 );
                        
                        NetworkWrapPanel.Children.Add(NetworkList.Draw(ColumnWidth, 30, HeadersName, null, ListSize, ItemSize, Color.FromArgb(255, 238, 241, 245), Colors.Transparent,
                            Colors.White, Colors.Black, Color.FromArgb(255, 42, 180, 192), Colors.LightGray, Colors.Transparent, MainWindow.DS.Tables["NetworkAdaptor"],
                            new Thickness(100, 20, 15, 10),new Thickness(0), 12, 16, ColumnsName , NetworkAdaptorList_SelectionChanged , "NetworkAdaptorList",
                           (double) MainWindow.DS.Tables["NetworkAdaptor"].Rows.Count * (ItemSize.Height + 5)));
                        MainWindow.DS.Tables.Remove("NetworkAdaptor");
                        NetworkAdaptorSemaphor.Release();
                        Button EnNetBtn = new Button();
                        EnNetBtn.Content = "Enable";
                        EnNetBtn.Margin = new Thickness(250, 0, 0, 20);
                        EnNetBtn.VerticalAlignment = VerticalAlignment.Top;
                        EnNetBtn.HorizontalAlignment = HorizontalAlignment.Left;
                        EnNetBtn.Click += EnNetBtnClick;
                        EnNetBtn.MinWidth = 80;
                        EnNetBtn.IsEnabled = false;
                        EnNetBtn.Uid = "NetworkAdaptorEnable";
                        Button DisNetBtn = new Button();
                        DisNetBtn.Content = "Disable";
                        DisNetBtn.Margin = new Thickness(10, 0, 0, 20);
                        DisNetBtn.VerticalAlignment = VerticalAlignment.Top;
                        DisNetBtn.HorizontalAlignment = HorizontalAlignment.Left;
                        DisNetBtn.Click += DisEnNetBtnClick;
                        DisNetBtn.IsEnabled = false;
                        DisNetBtn.MinWidth = 80;
                        DisNetBtn.Uid = "NetworkAdaptordisable";
                        NetworkWrapPanel.Children.Add(EnNetBtn);
                        NetworkWrapPanel.Children.Add(DisNetBtn);
                        Image InfoImage1 = new System.Windows.Controls.Image();
                        InfoImage1.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_info-blog_46810.ico"));
                        InfoImage1.Width = 30;
                        InfoImage1.Height = 30;
                        InfoImage1.VerticalAlignment = VerticalAlignment.Top;
                        InfoImage1.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoImage1.Margin = new Thickness(-390, 50, 0, 0);
                        NetworkWrapPanel.Children.Add(InfoImage1);
                        TextBlock InfoTb1 = new TextBlock();
                        InfoTb1.Text = "   For enable or disable adapter please select one of them in list then click on disable or enable button";
                        InfoTb1.FontSize = 16;
                        InfoTb1.VerticalAlignment = VerticalAlignment.Top;
                        InfoTb1.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoTb1.Foreground = new SolidColorBrush(Colors.Black);
                        InfoTb1.Margin = new Thickness(-360, 75, 200, 0);
                        NetworkWrapPanel.Children.Add(InfoTb1);
                        Expander NetworkExpander = new Expander();
                        NetworkExpander.Header = "More Details";
                        NetworkExpander.HorizontalAlignment = HorizontalAlignment.Left;
                        NetworkExpander.VerticalAlignment = VerticalAlignment.Top;
                        NetworkExpander.Collapsed += NetworkExpander_Collapsed;
                        NetworkExpander.Expanded += NetworkExpander_Expanded;
                        NetworkExpander.Margin = new Thickness(-950, 110, 0, 20);
                        NetworkWrapPanel.Children.Add(NetworkExpander);
                        BG.Child = NetworkWrapPanel;
                        MainDataGrid.Children.Add(BG);
                    }
                    break;
                case 10:
                    {
                        MainDataGrid.Children.Clear();
                        MainDataGrid.ColumnDefinitions.Clear();
                        MainDataGrid.RowDefinitions.Clear();
                        TextBlock TitleTB2 = new TextBlock();
                        TitleTB2.Text = "Real Timem Monitor";
                        TitleTB2.FontSize = 24;
                        TitleTB2.Foreground = new SolidColorBrush(Colors.Gray);
                        TitleTB2.Margin = new Thickness(20, -30, 0, 10);
                        TitleTB2.VerticalAlignment = VerticalAlignment.Top;
                        TitleTB2.HorizontalAlignment = HorizontalAlignment.Left;
                        MainDataGrid.Children.Add(TitleTB2);
                        Grid MainTemp = new Grid();
                        MainTemp.Margin = new Thickness(0);
                        Border BG = new Border();
                        BG.Background = new SolidColorBrush(Colors.White);
                        BG.Margin = new Thickness(20, 20, 20, 50);
                        BG.Child = MainTemp;
                        MainDataGrid.Children.Add(BG);
                        Image InfoImage0 = new System.Windows.Controls.Image();
                        InfoImage0.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_info-blog_46810.ico"));
                        InfoImage0.Width = 30;
                        InfoImage0.Height = 30;
                        InfoImage0.VerticalAlignment = VerticalAlignment.Top;
                        InfoImage0.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoImage0.Margin = new Thickness(10, 80, 0, 0);
                        MainTemp.Children.Add(InfoImage0);
                        Grid.SetRow(InfoImage0, 0);
                        TextBlock InfoTb0 = new TextBlock();
                        InfoTb0.Text = "You can show your child device monitor in real time";
                        InfoTb0.FontSize = 16;
                        InfoTb0.VerticalAlignment = VerticalAlignment.Top;
                        InfoTb0.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoTb0.Foreground = new SolidColorBrush(Colors.Black);
                        InfoTb0.Margin = new Thickness(30, 105, 0, 0);
                        MainTemp.Children.Add(InfoTb0);
                        Grid.SetRow(InfoTb0, 0);
                        RowDefinition RealRow0 = new RowDefinition();
                        RowDefinition RealRow1 = new RowDefinition();
                        TextBlock TitleTB = new TextBlock();
                        TitleTB.Text = "Device Monitor";
                        TitleTB.FontSize = 24;
                        TitleTB.Foreground = new SolidColorBrush(Colors.Gray);
                        TitleTB.Margin = new Thickness(20, 30, 0, 10);
                        TitleTB.VerticalAlignment = VerticalAlignment.Top;
                        TitleTB.HorizontalAlignment = HorizontalAlignment.Left;
                        MainTemp.Children.Add(TitleTB);
                        RealRow0.Height = new GridLength(1, GridUnitType.Star);
                        RealRow1.Height = new GridLength(0.25, GridUnitType.Star);
                        RealRow0.MinHeight = 400;
                        MainTemp.RowDefinitions.Add(RealRow0);
                        MainTemp.RowDefinitions.Add(RealRow1);
                        Monitor = new Image();
                        Monitor.Uid = "MainMonitor";
                        Bord = new Border();
                        Monitor.Margin = new Thickness(0);
                        Bord.VerticalAlignment = VerticalAlignment.Top;
                        Bord.Height = 350;
                        Bord.Width = 600;
                        Monitor.Source = BITIM;
                        Bord.Margin = new Thickness(10, 150, 10, 10);
                        Bord.Child = Monitor;
                        Bord.MinHeight = 300;
                        Bord.MinWidth = 300;
                        Bord.BorderThickness = new Thickness(0.5);
                        Bord.BorderBrush = new SolidColorBrush(Colors.Black);
                        MainTemp.Children.Add(Bord);
                        Grid.SetRow(Bord, 0);
                        ImageBrush StartBtnImage = new ImageBrush();
                        StartBtnImage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/video-play-3-64.ico"));
                        Button StartEndMonitorBtn = new Button();
                        StartEndMonitorBtn.Background = StartBtnImage;
                        StartEndMonitorBtn.BorderThickness = new Thickness(0);
                        StartEndMonitorBtn.Click += StartEndMonitorBtnClicked;
                        StartEndMonitorBtn.Template = MaxBtn.Template;
                        StartEndMonitorBtn.Width = 60;
                        StartEndMonitorBtn.Height = 60;
                        StartEndMonitorBtn.HorizontalAlignment = HorizontalAlignment.Center;
                        StartEndMonitorBtn.VerticalAlignment = VerticalAlignment.Top;
                        StartEndMonitorBtn.Margin = new Thickness(0, 0, 0, 10);
                        MainTemp.Children.Add(StartEndMonitorBtn);
                        Grid.SetRow(StartEndMonitorBtn, 1);
                        Image InfoImage1 = new System.Windows.Controls.Image();
                        InfoImage1.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_info-blog_46810.ico"));
                        InfoImage1.Width = 30;
                        InfoImage1.Height = 30;
                        InfoImage1.VerticalAlignment = VerticalAlignment.Top;
                        InfoImage1.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoImage1.Margin = new Thickness(10, 120, 0, 0);
                        MainTemp.Children.Add(InfoImage1);
                        Grid.SetRow(InfoImage1, 1);
                        TextBlock InfoTb1 = new TextBlock();
                        InfoTb1.Text = "You can show your child with webcam in real time";
                        InfoTb1.FontSize = 16;
                        InfoTb1.VerticalAlignment = VerticalAlignment.Top;
                        InfoTb1.HorizontalAlignment = HorizontalAlignment.Left;
                        InfoTb1.Foreground = new SolidColorBrush(Colors.Black);
                        InfoTb1.Margin = new Thickness(30, 145, 0, 0);
                        MainTemp.Children.Add(InfoTb1);
                        Grid.SetRow(InfoTb1, 1);
                        TextBlock TitleTB1 = new TextBlock();
                        TitleTB1.Text = "Webcam";
                        TitleTB1.FontSize = 24;
                        TitleTB1.Foreground = new SolidColorBrush(Colors.Gray);
                        TitleTB1.Margin = new Thickness(20, 70, 0, 10);
                        TitleTB1.VerticalAlignment = VerticalAlignment.Top;
                        TitleTB1.HorizontalAlignment = HorizontalAlignment.Left;
                        MainTemp.Children.Add(TitleTB1);
                        Grid.SetRow(TitleTB1, 1);
                        WebcamMonitor = new Image();
                        WebcamMonitor.Uid = "MainWebcamMonitor";
                        Border NewBord = new Border();
                        WebcamMonitor.Margin = new Thickness(0);
                        NewBord.VerticalAlignment = VerticalAlignment.Top;
                        NewBord.Height = 350;
                        NewBord.Width = 600;
                        WebcamMonitor.Source = WebCamBITIM;
                        NewBord.Margin = new Thickness(10, 190, 10, 10);
                        NewBord.Child = WebcamMonitor;
                        NewBord.MinHeight = 300;
                        NewBord.MinWidth = 300;
                        NewBord.BorderThickness = new Thickness(0.5);
                        NewBord.BorderBrush = new SolidColorBrush(Colors.Black);
                        MainTemp.Children.Add(NewBord);
                        Grid.SetRow(NewBord, 1);
                        ImageBrush StartWebcamBtnImage = new ImageBrush();
                        StartWebcamBtnImage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/video-play-3-64.ico"));
                        Button StartEndWebCamMonitorBtn = new Button();
                        StartEndWebCamMonitorBtn.Background = StartBtnImage;
                        StartEndWebCamMonitorBtn.BorderThickness = new Thickness(0);
                        StartEndWebCamMonitorBtn.Click += StartEndWebcamMonitorBtnClicked;
                        StartEndWebCamMonitorBtn.Template = MaxBtn.Template;
                        StartEndWebCamMonitorBtn.Width = 60;
                        StartEndWebCamMonitorBtn.Height = 60;
                        StartEndWebCamMonitorBtn.HorizontalAlignment = HorizontalAlignment.Center;
                        StartEndWebCamMonitorBtn.VerticalAlignment = VerticalAlignment.Top;
                        StartEndWebCamMonitorBtn.Margin = new Thickness(0, 550, 0, 10);
                        MainTemp.Children.Add(StartEndWebCamMonitorBtn);
                        Grid.SetRow(StartEndWebCamMonitorBtn, 1);
                    }; break;
                case 11:
                    {
                        MainDataGrid.Children.Clear();
                        MainDataGrid.ColumnDefinitions.Clear();
                        MainDataGrid.RowDefinitions.Clear();
                        Grid wrapPanel = new Grid();
                        Ellipse CertificateElipc = new Ellipse();
                        CertificateElipc.Fill = new SolidColorBrush(Colors.Red);
                        CertificateElipc.Height = 10;
                        CertificateElipc.Width = 10;
                        CertificateElipc.HorizontalAlignment = HorizontalAlignment.Left;
                        CertificateElipc.VerticalAlignment = VerticalAlignment.Top;
                        CertificateElipc.Margin = new Thickness(10, 50, 0, 0);
                        wrapPanel.Children.Add(CertificateElipc);
                        TextBlock CertifcateText = new TextBlock();
                        CertifcateText.Text = "Are you sure you want to Uninstall the Child application?";
                        CertifcateText.HorizontalAlignment = HorizontalAlignment.Left;
                        CertifcateText.VerticalAlignment = VerticalAlignment.Top;
                        CertifcateText.Margin = new Thickness(35, 45 ,0, 0);
                        CertifcateText.FontSize = 16;
                        CertifcateText.Foreground = new SolidColorBrush(Colors.Black);
                        wrapPanel.Children.Add(CertifcateText);
                        ImageBrush YesImage = new ImageBrush();
                        YesImage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/Untitled-1.ico"));
                        Button YesBtn = new Button();
                        YesBtn.Background = YesImage;
                        YesBtn.Uid = "UninstallYesBtn";
                        YesBtn.HorizontalAlignment = HorizontalAlignment.Left;
                        YesBtn.VerticalAlignment = VerticalAlignment.Top;
                        YesBtn.Template = MaxBtn.Template;
                        YesBtn.Margin = new Thickness(120, 100, 0, 0);
                        YesBtn.Height = 60;
                        YesBtn.Width = 60;
                        YesBtn.BorderThickness = new Thickness(0);
                        YesBtn.Click += UninstallYesBtnClicked;
                        wrapPanel.Children.Add(YesBtn);
                        ImageBrush NoImage = new ImageBrush();
                        NoImage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/Untitled-2.ico"));
                        Button NoBtn = new Button();
                        NoBtn.Background = NoImage;
                        NoBtn.Uid = "UninstallNoBtn";
                        NoBtn.Template = MaxBtn.Template;
                        NoBtn.HorizontalAlignment = HorizontalAlignment.Left;
                        NoBtn.VerticalAlignment = VerticalAlignment.Top;
                        NoBtn.Margin = new Thickness(220, 100, 0, 0);
                        NoBtn.Height = 60;
                        NoBtn.Width = 60;
                        NoBtn.Click += UninstallNoBtnClicked;
                        wrapPanel.Children.Add(NoBtn);
                        PageTitle.Text = "Uninstall Child";
                        MainDataGrid.Children.Add(wrapPanel);

                    };break;
                case 12:
                    {
                        MainDataGrid.Children.Clear();
                        MainDataGrid.ColumnDefinitions.Clear();
                        MainDataGrid.RowDefinitions.Clear();
                        TextBlock TitleTB = new TextBlock();
                        TitleTB.Text = "key Logs";
                        TitleTB.FontSize = 24;
                        TitleTB.Foreground = new SolidColorBrush(Colors.Gray);
                        TitleTB.Margin = new Thickness(20, -30, 0, 10);
                        TitleTB.VerticalAlignment = VerticalAlignment.Top;
                        TitleTB.HorizontalAlignment = HorizontalAlignment.Left;
                        MainDataGrid.Children.Add(TitleTB);
                        Border KeyLoggerBoard = new Border();
                        KeyLoggerBoard.Uid = "KeyLoggerBoard";
                        KeyLoggerBoard.Background = new SolidColorBrush(Colors.White);
                        KeyLoggerBoard.Margin = new Thickness(20, 20, 20, 50);
                        KeyLoggerBoard.VerticalAlignment = VerticalAlignment.Top;
                        WrapPanel KeyLoggerWrap = new WrapPanel();
                        KeyLoggerWrap.Margin = new Thickness(0, 0, 0, 0);
                        KeyLoggerWrap.Background = new SolidColorBrush(Colors.Transparent);
                        Image KeyLoggerTitleImage = new Image();
                        KeyLoggerTitleImage.Width = 15;
                        KeyLoggerTitleImage.Height = 15;
                        KeyLoggerTitleImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_Keyboard_100061.ico"));
                        KeyLoggerTitleImage.HorizontalAlignment = HorizontalAlignment.Left;
                        KeyLoggerTitleImage.VerticalAlignment = VerticalAlignment.Top;
                        KeyLoggerTitleImage.Margin = new Thickness(20, 16, 0, 0);
                        TextBlock KeyLoggerTitleImageTB = new TextBlock();
                        KeyLoggerTitleImageTB.Text = "Key Logs";
                        KeyLoggerTitleImageTB.Foreground = new SolidColorBrush(Colors.Black);
                        KeyLoggerTitleImageTB.FontSize = 12;
                        KeyLoggerTitleImageTB.FontWeight = FontWeights.Bold;
                        KeyLoggerTitleImageTB.HorizontalAlignment = HorizontalAlignment.Left;
                        KeyLoggerTitleImageTB.VerticalAlignment = VerticalAlignment.Top;
                        KeyLoggerTitleImageTB.Margin = new Thickness(5, 15, 0, 0);
                        Line KeyLoggerSepratorLine = new Line();
                        KeyLoggerSepratorLine.VerticalAlignment = VerticalAlignment.Top;
                        KeyLoggerSepratorLine.Margin = new Thickness(10, 2, 15, 0);
                        KeyLoggerSepratorLine.Stroke = new SolidColorBrush(Colors.LightGray);
                        KeyLoggerSepratorLine.Opacity = 70;
                        KeyLoggerSepratorLine.StrokeThickness = 1;
                        KeyLoggerSepratorLine.X1 = 5;
                        KeyLoggerSepratorLine.Y1 = 10;
                        KeyLoggerSepratorLine.X2 = 1400;
                        KeyLoggerSepratorLine.Y2 = 10;
                        KeyLoggerWrap.Children.Add(KeyLoggerTitleImage);
                        KeyLoggerWrap.Children.Add(KeyLoggerTitleImageTB);
                        KeyLoggerWrap.Children.Add(KeyLoggerSepratorLine);
                        Border KeysListBorder = new Border();
                        KeysListBorder.Background = new SolidColorBrush(Colors.Transparent);
                        KeysListBorder.Margin = new Thickness(20,20,15,0);
                        StackPanel KeysParentSP = new StackPanel();
                        StackPanel Headers = new StackPanel();
                        Headers.Orientation = Orientation.Horizontal;
                        int Number = 0;
                        string[] HeaadersName =
                        {
                            "Content" , "App", "Time","Device Alias"
                        };
                        int[] HeadersWidth =
                        {
                            500,200,300,250
                        };
                        for(int i = 0; i < 4; i++)
                        {
                            Border border = new Border();
                            border.Width = HeadersWidth[i];
                            border.Height = 30;
                            border.Background = new SolidColorBrush(Color.FromArgb(255,42,180,192));
                            TextBlock textBlock = new TextBlock();
                            textBlock.Text = HeaadersName[i];
                            textBlock.Margin = new Thickness(5, 5, 0, 0);
                            textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                            textBlock.FontSize = 16;
                            textBlock.Foreground = new SolidColorBrush(Colors.White);
                            border.Child = textBlock;
                            border.Margin = new Thickness(0.5, 0, 0.5, 0);
                            Headers.Children.Add(border);
                        }
                        KeysParentSP.Children.Add(Headers);
                        ScrollViewer KeyLoggerParentSV = new ScrollViewer();
                        KeyLoggerParentSV.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                        StackPanel KeysData = new StackPanel();
                        KeysSemaphore.WaitOne();
                        if (MainWindow.DS.Tables.Contains("Keys") == true)
                        {
                            MainWindow.DS.Tables.Remove("Keys");
                            
                        }
                        MainWindow.DataBaseAgent.SelectData("Keys", ref MainWindow.DS, "*", "Keys", ChildsCombo.SelectedItem.ToString(), "ChildID");
                        for (int i = 0; i < MainWindow.DS.Tables["Keys"].Rows.Count; i++)
                        {
                            Border border = new Border();
                            border.MouseEnter += Border_MouseEnter1;
                            border.MouseLeave += Border_MouseLeave1;
                            if (i% 2 != 0)
                            {
                                border.Background = new SolidColorBrush(Color.FromArgb(255, 238, 241, 245));
                            }
                            else
                            {
                                border.Background = new SolidColorBrush(Colors.Transparent);
                            }
                            border.BorderBrush = new SolidColorBrush(Colors.LightGray);
                            border.BorderThickness = new Thickness(0.2,0.2,0.2,0.2);
                            StackPanel stackPanel = new StackPanel();
                            stackPanel.Orientation = Orientation.Horizontal;
                            
                            for (int j = 0; j < 4; j++)
                            {
                                Border NewBorder = new Border();
                                NewBorder.Width = HeadersWidth[j];
                                NewBorder.Height = 30;
                                NewBorder.BorderBrush = new SolidColorBrush(Colors.LightGray);
                                NewBorder.BorderThickness = new Thickness(0.2, 0, 0.2, 0);
                                NewBorder.Background = new SolidColorBrush(Colors.Transparent);
                                TextBlock textBlock = new TextBlock();
                                textBlock.Text = MainWindow.DS.Tables["Keys"].Rows[i][j].ToString();
                                textBlock.Margin = new Thickness(5, 8, 0, 0);
                                textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                                textBlock.FontSize = 12;
                                textBlock.Foreground = new SolidColorBrush(Colors.Black);
                                NewBorder.Child = textBlock;
                                NewBorder.Margin = new Thickness(0.2, 0, 0.2, 0);
                                stackPanel.Children.Add(NewBorder);
                            }
                            border.Child = stackPanel;
                            KeysData.Children.Add(border);
                            Number++;
                        }
                        MainWindow.DS.Tables.Remove("Keys");
                        KeysSemaphore.Release();
                        TextBlock TbDataNumber = new TextBlock();
                        TbDataNumber.Text = "Total : " + Number.ToString();
                        TbDataNumber.Uid = "KeyLoggerCounterTB";
                        TbDataNumber.Margin = new Thickness(20, 15, 0, 20);
                        TbDataNumber.Foreground = new SolidColorBrush(Colors.Black);
                        TbDataNumber.FontSize = 12;
                        KeyLoggerParentSV.Content = KeysData;
                        KeysParentSP.Children.Add(KeyLoggerParentSV);
                        KeysListBorder.Child = KeysParentSP;
                        KeyLoggerWrap.Children.Add(KeysListBorder);
                        KeyLoggerWrap.Children.Add(TbDataNumber);
                        KeyLoggerBoard.Child = KeyLoggerWrap;
                        MainDataGrid.Children.Add(KeyLoggerBoard);
                        UpDateUIData = new ParameterizedThreadStart(KeyLoggerUpDate);

                    };break;
                case 13:
                    {
                        MainDataGrid.Children.Clear();
                        MainDataGrid.ColumnDefinitions.Clear();
                        MainDataGrid.RowDefinitions.Clear();
                        TextBlock TitleTB = new TextBlock();
                        TitleTB.Text = "Voices";
                        TitleTB.FontSize = 24;
                        TitleTB.Foreground = new SolidColorBrush(Colors.Gray);
                        TitleTB.Margin = new Thickness(20, -30, 0, 10);
                        TitleTB.VerticalAlignment = VerticalAlignment.Top;
                        TitleTB.HorizontalAlignment = HorizontalAlignment.Left;
                        MainDataGrid.Children.Add(TitleTB);
                        Border VoiceBoard = new Border();
                        VoiceBoard.Background = new SolidColorBrush(Colors.White);
                        VoiceBoard.Margin = new Thickness(20, 20, 20, 50);
                        VoiceBoard.VerticalAlignment = VerticalAlignment.Top;
                        WrapPanel VoiceWrap = new WrapPanel();
                        VoiceWrap.Margin = new Thickness(0, 0, 0, 0);
                        VoiceWrap.Background = new SolidColorBrush(Colors.Transparent);
                        Image VoiceTitleImage = new Image();
                        VoiceTitleImage.Width = 15;
                        VoiceTitleImage.Height = 15;
                        VoiceTitleImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_ic_keyboard_voice_48px_352475.ico"));
                        VoiceTitleImage.HorizontalAlignment = HorizontalAlignment.Left;
                        VoiceTitleImage.VerticalAlignment = VerticalAlignment.Top;
                        VoiceTitleImage.Margin = new Thickness(20, 16, 0, 0);
                        TextBlock VoiceTitleImageTB = new TextBlock();
                        VoiceTitleImageTB.Text = "Voices";
                        VoiceTitleImageTB.Foreground = new SolidColorBrush(Colors.Black);
                        VoiceTitleImageTB.FontSize = 12;
                        VoiceTitleImageTB.FontWeight = FontWeights.Bold;
                        VoiceTitleImageTB.HorizontalAlignment = HorizontalAlignment.Left;
                        VoiceTitleImageTB.VerticalAlignment = VerticalAlignment.Top;
                        VoiceTitleImageTB.Margin = new Thickness(5, 15, 0, 0);
                        Line VoiceSepratorLine = new Line();
                        VoiceSepratorLine.VerticalAlignment = VerticalAlignment.Top;
                        VoiceSepratorLine.HorizontalAlignment = HorizontalAlignment.Left;
                        VoiceSepratorLine.Margin = new Thickness(10, 2, 15, 0);
                        VoiceSepratorLine.Stroke = new SolidColorBrush(Colors.LightGray);
                        VoiceSepratorLine.Opacity = 70;
                        VoiceSepratorLine.StrokeThickness = 1;
                        VoiceSepratorLine.X1 = 5;
                        VoiceSepratorLine.Y1 = 10;
                        VoiceSepratorLine.X2 = 1400;
                        VoiceSepratorLine.Y2 = 10;
                        Button VoiceBtn = new Button();
                        VoiceBtn.HorizontalAlignment = HorizontalAlignment.Right;
                        VoiceBtn.Margin = new Thickness(1200, 30, 20, 0);
                        VoiceBtn.Width = 30;
                        VoiceBtn.Template = MaxBtn.Template;
                        VoiceBtn.Cursor = Cursors.Hand;
                        VoiceBtn.Height = 30;
                        ImageBrush VoiceBtnBrush = new ImageBrush();
                        VoiceBtnBrush.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/Dtafalonso-Android-Lollipop-Settings.ico"));
                        VoiceBtn.Background = VoiceBtnBrush;
                        VoiceBtn.BorderThickness = new Thickness(0);
                        VoiceBtn.Click += VoiceBtn_Click;
                        StackPanel ShowVoiceStackL = new StackPanel();
                        StackPanel ShowVoiceStackR = new StackPanel();
                        ShowVoiceStackL.Margin = new Thickness(0, 30, 0, 20);
                        ShowVoiceStackL.Background = new SolidColorBrush(Colors.Transparent);
                        ShowVoiceStackR.Margin = new Thickness(-50, 30, 0, 20);
                        ShowVoiceStackR.Background = new SolidColorBrush(Colors.Transparent);
                        Line VoiceMidelLine = new Line();
                        VoiceMidelLine.VerticalAlignment = VerticalAlignment.Top;
                        VoiceMidelLine.HorizontalAlignment = HorizontalAlignment.Left;
                        VoiceMidelLine.Margin = new Thickness(15, 2, 15, 20);
                        VoiceMidelLine.Stroke = new SolidColorBrush(Colors.LightGray);
                        VoiceMidelLine.Opacity = 70;
                        VoiceMidelLine.StrokeThickness = 2;
                        VoiceMidelLine.X1 = 600;
                        VoiceMidelLine.Y1 = 10;
                        VoiceMidelLine.X2 = 600;
                        VoiceMidelLine.Y2 = 600;
                        int Number = 0;
                        VoiceSemaphore.WaitOne();
                        MainWindow.DataBaseAgent.SelectData("Voice", ref MainWindow.DS, "*", "Voice", ChildsCombo.SelectedItem.ToString(), "ChildID");
                        for (int i = 0; i< MainWindow.DS.Tables["Voice"].Rows.Count; i++)
                        {
                            Number++;
                            if (i %2 == 0)
                            {
                                StackPanel stackPanel = new StackPanel();
                                stackPanel.Orientation = Orientation.Horizontal;
                                stackPanel.Margin = new Thickness(-630, 50, 0, 0);
                                stackPanel.Width = 640;
                                stackPanel.FlowDirection = FlowDirection.RightToLeft;
                                VoiceRect voiceRect = new VoiceRect();
                                Image Pic = new Image();
                                Pic.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_human_1216577.ico"));
                                Pic.Width = 50;
                                Pic.Height = 50;
                                Pic.VerticalAlignment = VerticalAlignment.Top;
                                stackPanel.Children.Add(Pic);
                                stackPanel.Children.Add(voiceRect.Draw(MainWindow.DS.Tables["Voice"].Rows[i]["Process"].ToString(), MainWindow.DS.Tables["Voice"].Rows[i]["Date"].ToString(), true));
                                ShowVoiceStackL.Children.Add(stackPanel);
                                StackPanel stackPanelTemp = new StackPanel();
                                stackPanelTemp.Orientation = Orientation.Horizontal;
                                stackPanelTemp.Margin = new Thickness(-500, 20, 0, 30);
                                stackPanelTemp.Width =100;
                                Border Temp = voiceRect.Draw("", "", true);
                                Temp.Background = new SolidColorBrush(Colors.Transparent);
                                Temp.BorderThickness = new Thickness(0);
                                Temp.Child = null;
                                stackPanelTemp.Children.Add(Temp);
                                ShowVoiceStackR.Children.Add(stackPanelTemp);
                            }
                            else
                            {
                                StackPanel stackPanel = new StackPanel();
                                stackPanel.Orientation = Orientation.Horizontal;
                                stackPanel.Margin = new Thickness(0, 20, 0, 0);
                                stackPanel.Width = 640;
                                VoiceRect voiceRect = new VoiceRect();
                                Image Pic = new Image();
                                Pic.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_human_1216577.ico"));
                                Pic.Width = 50;
                                Pic.Height = 50;
                                Pic.VerticalAlignment = VerticalAlignment.Top;
                                stackPanel.Children.Add(Pic);
                                stackPanel.Children.Add(voiceRect.Draw(MainWindow.DS.Tables["Voice"].Rows[i]["Process"].ToString(), MainWindow.DS.Tables["Voice"].Rows[i]["Date"].ToString(), false));
                                ShowVoiceStackR.Children.Add(stackPanel);
                                StackPanel stackPanelTemp = new StackPanel();
                                stackPanelTemp.Orientation = Orientation.Horizontal;
                                stackPanelTemp.Margin = new Thickness(-630, 20, 0, 0);
                                stackPanelTemp.Width = 640;
                                Border Temp = voiceRect.Draw("", "", true);
                                Temp.Background = new SolidColorBrush(Colors.Transparent);
                                Temp.BorderThickness = new Thickness(0);
                                Temp.Child = null;
                                stackPanelTemp.Children.Add(Temp);
                                ShowVoiceStackL.Children.Add(stackPanelTemp);
                            }
                            
                        }
                        MainWindow.DS.Tables.Remove("Voice");
                        VoiceSemaphore.Release();
                        VoiceMidelLine.Y2 = (Number * (110 + 30));
                        VoiceWrap.Children.Add(VoiceTitleImage);
                        VoiceWrap.Children.Add(VoiceTitleImageTB);
                        VoiceWrap.Children.Add(VoiceSepratorLine);
                        VoiceWrap.Children.Add(VoiceBtn);
                        VoiceWrap.Children.Add(VoiceMidelLine);
                        VoiceWrap.Children.Add(ShowVoiceStackL);
                        VoiceWrap.Children.Add(ShowVoiceStackR);
                        VoiceBoard.Child = VoiceWrap;
                        MainDataGrid.Children.Add(VoiceBoard);
                        UpDateUIData = new ParameterizedThreadStart(VoiceUpDate);
                    };break;
                default:
                    break;
            }


        }

        private void LocationBrowser_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(async() =>
            {
                await Dispatcher.BeginInvoke(new ThreadStart(() =>
                {
                    LocationBrowser.Source = new Uri("https://www.google.com/maps");

                }),System.Windows.Threading.DispatcherPriority.Background);
            });
            

        }

        private void LocationBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            var Target = MainDataGrid.Children[0];
        }

        private void VoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser = ChildsCombo.SelectedItem.ToString();
            VoiceWindow Temp = new VoiceWindow();
            Temp.Child = CurrentUser;
            this.Hide();
            Temp.ShowDialog();
        }

        private void VoiceUpDate(object obj)
        {
            DataRow Row = (DataRow)obj;
            WrapPanel Target = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
            StackPanel LeftStackPanel = (StackPanel)Target.Children[3];
            StackPanel RightStackPanel = (StackPanel)Target.Children[5];
            
            if (RightStackPanel.Children.Count % 2 == 0)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Margin = new Thickness(-630, 50, 0, 0);
                stackPanel.Width = 640;
                stackPanel.FlowDirection = FlowDirection.RightToLeft;
                VoiceRect voiceRect = new VoiceRect();
                Image Pic = new Image();
                Pic.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_human_1216577.ico"));
                Pic.Width = 50;
                Pic.Height = 50;
                Pic.VerticalAlignment = VerticalAlignment.Top;
                stackPanel.Children.Add(Pic);
                stackPanel.Children.Add(voiceRect.Draw(Row["Process"].ToString(), Row["Date"].ToString(), true));
                LeftStackPanel.Children.Add(stackPanel);
                StackPanel stackPanelTemp = new StackPanel();
                stackPanelTemp.Orientation = Orientation.Horizontal;
                stackPanelTemp.Margin = new Thickness(-500, 20, 0, 30);
                stackPanelTemp.Width = 100;
                Border Temp = voiceRect.Draw("", "", true);
                Temp.Background = new SolidColorBrush(Colors.Transparent);
                Temp.BorderThickness = new Thickness(0);
                Temp.Child = null;
                stackPanelTemp.Children.Add(Temp);
                RightStackPanel.Children.Add(stackPanelTemp);
            }
            else
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Margin = new Thickness(0, 20, 0, 0);
                stackPanel.Width = 640;
                VoiceRect voiceRect = new VoiceRect();
                Image Pic = new Image();
                Pic.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_human_1216577.ico"));
                Pic.Width = 50;
                Pic.Height = 50;
                Pic.VerticalAlignment = VerticalAlignment.Top;
                stackPanel.Children.Add(Pic);
                stackPanel.Children.Add(voiceRect.Draw(Row["Process"].ToString(), Row["Date"].ToString(), false));
                RightStackPanel.Children.Add(stackPanel);
                StackPanel stackPanelTemp = new StackPanel();
                stackPanelTemp.Orientation = Orientation.Horizontal;
                stackPanelTemp.Margin = new Thickness(-630, 20, 0, 0);
                stackPanelTemp.Width = 640;
                Border Temp = voiceRect.Draw("", "", true);
                Temp.Background = new SolidColorBrush(Colors.Transparent);
                Temp.BorderThickness = new Thickness(0);
                Temp.Child = null;
                stackPanelTemp.Children.Add(Temp);
                LeftStackPanel.Children.Add(stackPanelTemp);
            }
        }

        private void KeyLoggerUpDate(object obj)
        {
            this.Dispatcher.Invoke(() =>
            {
                DataRow NewRow = (DataRow)obj;
                WrapPanel TargetWrap = ((WrapPanel)
                    ((Border)MainDataGrid.Children.Cast<UIElement>().First(x => x.Uid == "KeyLoggerBoard")).Child);
                StackPanel Target = (StackPanel)((ScrollViewer)((StackPanel)((Border)TargetWrap.Children[3]).Child).Children[1]).Content;
                for (int j = 0; j < NewRow.ItemArray.Length; j++)
                {
                    Border NewBorder = new Border();
                    NewBorder.Width = HeadersWidth[j];
                    NewBorder.Height = 30;
                    NewBorder.BorderBrush = new SolidColorBrush(Colors.LightGray);
                    NewBorder.BorderThickness = new Thickness(0.2, 0, 0.2, 0);
                    NewBorder.Background = new SolidColorBrush(Colors.Transparent);
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = NewRow[j].ToString();
                    textBlock.Margin = new Thickness(5, 8, 0, 0);
                    textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                    textBlock.FontSize = 12;
                    textBlock.Foreground = new SolidColorBrush(Colors.Black);
                    NewBorder.Child = textBlock;
                    NewBorder.Margin = new Thickness(0.2, 0, 0.2, 0);
                    Target.Children.Add(NewBorder);
                }
                TextBlock TargetTB = (TextBlock)TargetWrap.Children.Cast<UIElement>().First(x => x.Uid == "KeyLoggerCounterTB");
                long Number = Convert.ToInt64(TargetTB.Text.Split(':')[1]);
                TargetTB.Text.Replace(Number.ToString(), (Number++).ToString());
            });
            
        }

        private void PictureUpDate(object obj)
        {
            DataRow Row = (DataRow)obj;
            if (Row.Table.TableName.Contains("Screen") == true)
            {
               
                StackPanel Target = (StackPanel)((ScrollViewer)((Border)((Grid)((Border)MainDataGrid.Children[1]).Child).Children.Cast<UIElement>().First(x => x.Uid == "ImageListBorder")
                ).Child).Content;
                StackPanel stackPanel = new StackPanel();
                stackPanel.MouseEnter += StackPanel_MouseEnter;
                stackPanel.Background = new SolidColorBrush(Colors.Transparent);
                stackPanel.MouseLeave += StackPanel_MouseLeave;
                stackPanel.MouseLeftButtonUp += StackPanel_MouseLeftButtonUp;
                TextBlock textBlock = new TextBlock();
                textBlock.Text = Row["Date"].ToString();
                textBlock.Foreground = new SolidColorBrush(Colors.Blue);
                textBlock.FontSize = 12;
                stackPanel.Children.Add(textBlock);
                Image image = new Image();
                MemoryStream ms;
                byte[] PicData = Convert.FromBase64String((string)Row["Picture"]);
                ms = new MemoryStream(PicData);
                System.Drawing.Bitmap BitImage = new System.Drawing.Bitmap(ms);
                BitImage.Save(System.AppDomain.CurrentDomain.BaseDirectory + "New" + "Shot.jpeg");
                var MyBrush = new ImageBrush();
                BitmapImage BImage = new BitmapImage();
                BImage.BeginInit();
                BImage.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "New" + "Shot.jpeg");
                BImage.EndInit();
                Main.ScreenShotImage.Source = BImage;
                image.Source = BImage;
                stackPanel.Children.Add(image);
                Target.Children.Add(stackPanel);
            }
            if (Row.Table.TableName.Contains("WebCam") == true)
            {
                this.Dispatcher.Invoke(() =>
                {
                    
                    StackPanel Target = (StackPanel)((ScrollViewer)((Border)((Grid)((Border)MainDataGrid.Children[1]).Child).Children.Cast<UIElement>().First(x => x.Uid == "WebImageListBorder")
                    ).Child).Content;
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.MouseEnter += StackPanel_MouseEnter;
                    stackPanel.Background = new SolidColorBrush(Colors.Transparent);
                    stackPanel.MouseLeave += StackPanel_MouseLeave;
                    stackPanel.MouseLeftButtonUp += StackPanel_MouseLeftButtonUp;
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = Row["Date"].ToString();
                    textBlock.Foreground = new SolidColorBrush(Colors.Blue);
                    textBlock.FontSize = 12;
                    Image image = new Image();
                    MemoryStream ms;
                    byte[] PicData = Convert.FromBase64String((string)Row["Pic"]);
                    ms = new MemoryStream(PicData);
                    System.Drawing.Bitmap BitImage = new System.Drawing.Bitmap(ms);
                    BitImage.Save(System.AppDomain.CurrentDomain.BaseDirectory + "New" + "Web.jpeg");
                    var MyBrush = new ImageBrush();
                    BitmapImage BImage = new BitmapImage();
                    BImage.BeginInit();
                    BImage.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "New" + "Web.jpeg");
                    BImage.EndInit();
                    WebcamImage.Source = BImage;
                    image.Source = BImage;
                    image.Width = 70;
                    image.Height = 50;
                    stackPanel.Children.Add(image);
                    stackPanel.Children.Add(textBlock);
                    Target.Children.Add(stackPanel);
                });

            }

        }

        private void RemoveWebImageClick(object sender, RoutedEventArgs e)
        {

            MainWindow.DataBaseAgent.ExequteWithCommand("Delete From WebPicTable where Date ='" + "" + "'");
            Button Target = sender as Button;
            StackPanel TargetStack = (StackPanel)((ScrollViewer)((Border)((Grid)Target.Parent).Children.Cast<UIElement>().First(x => x.Uid == "WebImageListBorder")).Child).Content;
            TargetStack.Children.RemoveAt(WebPicSelected);
        }

        private void TackeWeb(object sender, RoutedEventArgs e)
        {
            Packet Pack = new Packet();
            Packet.WebCam SCPack = new Packet.WebCam();
            SCPack.Type = (short)Packet.WebCamType.Picture;
            SCPack.Time = DateTime.Now.ToString();
            SCPack.State = true;
            SCPack.ProcessName = "";
            string Data = Pack.ToString(SCPack);
            Packet.PSProPacket ProSC = new Packet.PSProPacket();
            ProSC.Type = (short)Packet.PacketType.WebCam;
            ProSC.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
            ProSC.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
            ProSC.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
            Packet.MainPacket MainData = new Packet.MainPacket();
            MainData.Data = Data;
            MainData.PPacket = Pack.ToString(ProSC);
            string MainStrData = Pack.ToString<Packet.MainPacket>(MainData);
            MainWindow.Connection.SendDataSM.WaitOne();
            MainWindow.Connection.SendToServer(MainStrData);
            MainWindow.Connection.SendDataSM.Release();
        }

        private void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackPanel Target = sender as StackPanel;
            TextBlock TargetDate = (TextBlock)Target.Children[1];
            int Index = ((StackPanel)Target.Parent).Children.IndexOf(Target);
            if (((StackPanel)Target.Parent).Uid == "SCImageList")
            {
                if(Index != SCSelected)
                {
                    SCSelected = Index;
                    SelectedSC = ((SolidColorBrush)Target.Background).Color;
                    Target.Background = new SolidColorBrush(Colors.LightBlue);
                    Image image = new Image();
                    MemoryStream ms;
                    ScreenShotSemaphore.WaitOne();
                    string Row = (string)MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select Picture From ScreenShot where Date ='" + TargetDate.Text + "'");
                    ScreenShotSemaphore.Release();
                    byte[] PicData = Convert.FromBase64String((string)Row);
                    ms = new MemoryStream(PicData);
                    System.Drawing.Bitmap BitImage = new System.Drawing.Bitmap(ms);
                    BitImage.Save(System.AppDomain.CurrentDomain.BaseDirectory + "Large" + "Shot.jpeg");
                    var MyBrush = new ImageBrush();
                    BitmapImage BImage = new BitmapImage();
                    BImage.BeginInit();
                    BImage.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "Large" + "Shot.jpeg");
                    BImage.EndInit();
                    ScreenShotImage.Source = BImage;
                }
                else
                {
                    SCSelected = -1;
                    Target.Background = new SolidColorBrush(SelectedSC);
                }
                
            }
            else
            {
                if(WebPicSelected != Index)
                {
                    WebPicSelected = Index;
                    SelectedWebPic = ((SolidColorBrush)Target.Background).Color;
                    Target.Background = new SolidColorBrush(Colors.LightBlue);
                    MemoryStream ms;
                    WebCamTableSemaphore.WaitOne();
                    string Row = (string)MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select Pic From WebCamTable where Date ='" + TargetDate.Text + "' And ChildID ='"+ ChildsCombo.SelectedItem.ToString() + "'");
                    WebCamTableSemaphore.Release();
                    byte[] PicData = Convert.FromBase64String((string)Row);
                    ms = new MemoryStream(PicData);
                    System.Drawing.Bitmap BitImage = new System.Drawing.Bitmap(ms);
                    BitImage.Save(System.AppDomain.CurrentDomain.BaseDirectory + "Large" + "Web.jpeg");
                    var MyBrush = new ImageBrush();
                    BitmapImage BImage = new BitmapImage();
                    BImage.BeginInit();
                    BImage.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "Large" + "Web.jpeg");
                    BImage.EndInit();
                    WebcamImage.Source = BImage;
                    
                }
                else
                {
                    WebPicSelected = -1;
                    Target.Background = new SolidColorBrush(SelectedWebPic);
                }
                
            }
            
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel Target = sender as StackPanel;
            Target.Background = new SolidColorBrush(MouseEnterPic);
        }

        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel Target = sender as StackPanel;
            MouseEnterPic = ((SolidColorBrush)Target.Background).Color;
            Target.Background = new SolidColorBrush(Colors.Azure);
        }

        private void StartEndWebcamMonitorBtnClicked(object sender, RoutedEventArgs e)
        {
            Button Target = sender as Button;

            if (StartReal == false)
            {
                StartReal = true;
                Packet Pack = new Packet();
                Target.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/pause-64.ico")));
                Packet.RealTimeMonitor Real = new Packet.RealTimeMonitor();
                NetWorkTools NetTool = new NetWorkTools();
                string IP = NetTool.GetConnectionInterfaceName();
                Real.IP = IP;
                Real.Port = 8805;
                MainWindow.Connection.RealTimeMonitoringWebCam(Real.IP, Real.Port, Monitor);
                Real.DeviceType = true;
                Packet.PSProPacket ProReal = new Packet.PSProPacket();
                ProReal.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                ProReal.Type = (short)Packet.PacketType.RealTimeMonitor;
                string Data = Pack.ToString(Real);
                ProReal.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                ProReal.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
                Packet.MainPacket MainData = new Packet.MainPacket();
                MainData.Data = Data;
                MainData.PPacket = Pack.ToString(ProReal);
                string MainStrData = Pack.ToString<Packet.MainPacket>(MainData);
                Thread.Sleep(1000);
                MainWindow.Connection.SendDataSM.WaitOne();
                MainWindow.Connection.SendToServer(MainStrData);
                MainWindow.Connection.SendDataSM.Release();
                Monitor.Source = WebCamBITIM;

            }
            else
            {
                StartReal = false;
                MainWindow.Connection.RealWebTimeEnd();
                Target.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/video-play-3-64.ico")));
            }
        }

        private void Border_MouseLeave1(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            border.Background = new SolidColorBrush(KeyBorderHover);
        }

        private void Border_MouseEnter1(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            KeyBorderHover = ((SolidColorBrush)border.Background).Color;
            border.Background = new SolidColorBrush(Colors.LightGray);
        }

        private void RunnigAppHistoryExpander_Expanded(object sender, RoutedEventArgs e)
        {
            WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
            Expander Target = sender as Expander;
            TextBlock TitleTB0 = new TextBlock();
            TitleTB0.Text = "History";
            TitleTB0.FontSize = 24;
            TitleTB0.Foreground = new SolidColorBrush(Colors.Gray);
            TitleTB0.Margin = new Thickness(-770, Target.Margin.Top + 70, 0, 10);
            TitleTB0.VerticalAlignment = VerticalAlignment.Top;
            TitleTB0.HorizontalAlignment = HorizontalAlignment.Left;
            MainTemp.Children.Add(TitleTB0);
            HistoryApps = new List<object>();
            HistoryApps.Add(TitleTB0);

            if (MainWindow.DS.Tables.Contains("Process") == true)
            {
                MainWindow.DS.Tables.Remove("Process");
            }
            MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From Process where ChildID ='" + ChildUser + "' order by StartTime desc", ref MainWindow.DS, "Process");
            double[] ColumnWidth = { 250, 200, 200, 350, 200 };
            string[] HeadersName = { "Name", "Start Time", "End Time" ,"Executable Path" , "OS" };
            string[] ColumnsName = { "ProcessName", "StartTime", "EndTime","ExecutablePath", "OSName"};
            ColorList NetworkList = new ColorList();
            Size ItemSize = new Size(0, 25);
            Size ListSize = new Size(1202, 0);
            Border border = NetworkList.Draw(ColumnWidth, 30, HeadersName, null, ListSize, ItemSize, Color.FromArgb(255, 238, 241, 245), Colors.Transparent,
                Colors.White, Colors.Black, Color.FromArgb(255, 42, 180, 192), Colors.LightGray, Colors.Transparent, MainWindow.DS.Tables["Process"],
                new Thickness(30, 50, 15, 20), new Thickness(0), 12, 16, ColumnsName, null, "ProcessHistoryList", 500);
            MainTemp.Children.Add(border);
            HistoryApps.Add(border);
            MainWindow.DS.Tables.Remove("Process");
        }

        private void RunnigAppHistoryExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
            foreach (object var in HistoryApps)
            {
                MainTemp.Children.Remove((UIElement)var);
            }
        }

        private void MoreApp_Collapsed(object sender, RoutedEventArgs e)
        {
            WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
            foreach (object var in MoreApps)
            {
                MainTemp.Children.Remove((UIElement)var);
            }
        }

        private void MoreApp_Expanded(object sender, RoutedEventArgs e)
        {
            WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
            Expander TargetExpander = sender as Expander;
            TextBlock TitleTB0 = new TextBlock();
            TitleTB0.Text = "Category";
            TitleTB0.FontSize = 24;
            TitleTB0.Foreground = new SolidColorBrush(Colors.Gray);
            TitleTB0.Margin = new Thickness(30, 20, 0, 10);
            TitleTB0.VerticalAlignment = VerticalAlignment.Top;
            TitleTB0.HorizontalAlignment = HorizontalAlignment.Left;
            MainTemp.Children.Add(TitleTB0);
            MoreApps = new List<object>();
            Image InfoImage1 = new System.Windows.Controls.Image();
            InfoImage1.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_info-blog_46810.ico"));
            InfoImage1.Width = 30;
            InfoImage1.Height = 30;
            InfoImage1.VerticalAlignment = VerticalAlignment.Top;
            InfoImage1.HorizontalAlignment = HorizontalAlignment.Left;
            InfoImage1.Margin = new Thickness(30, 30, 0, 0);
            MainTemp.Children.Add(InfoImage1);
            MoreApps.Add(TitleTB0);
            MoreApps.Add(InfoImage1);
            TextBlock InfoTb2 = new TextBlock();
            InfoTb2.Text = "   You can show all applications categores and application in each add or remove category or application that you want";
            InfoTb2.FontSize = 16;
            InfoTb2.VerticalAlignment = VerticalAlignment.Top;
            InfoTb2.HorizontalAlignment = HorizontalAlignment.Left;
            InfoTb2.Foreground = new SolidColorBrush(Colors.Black);
            InfoTb2.Margin = new Thickness(50, 0, 0, 0);
            MainTemp.Children.Add(InfoTb2);
            MoreApps.Add(InfoTb2);
            StackPanel ListStP = new StackPanel();
            ListStP.Orientation = Orientation.Horizontal;
            CategoryAppList = new ColorList();
            Size ItemSize = new Size(0, 25);
            Size ListSize = new Size(200, 0);
            double []CategoryColumnWidth = { 200 };
            string[] CategoryHeadersName = { "Category" };
            string[] CategoryColumnsName = { "Name" };
            MainWindow.DataBaseAgent.SelectData("AppCategory", ref MainWindow.DS, "*", "AppCategory", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
            Border Catborder = CategoryAppList.Draw(CategoryColumnWidth, 30, CategoryHeadersName, null, ListSize, ItemSize, Color.FromArgb(255, 238, 241, 245), Colors.Transparent,
                Colors.White, Colors.Black, Color.FromArgb(255, 42, 180, 192), Colors.LightGray, Colors.Transparent, MainWindow.DS.Tables["AppCategory"],
                new Thickness(0, 0, 20, 0), new Thickness(0), 12, 16, CategoryColumnsName, CategoryApp_SelectionChanged, "CategoryList", 150);
            Catborder.VerticalAlignment = VerticalAlignment.Top;
            Catborder.HorizontalAlignment = HorizontalAlignment.Left;
            ListStP.Children.Add(Catborder);
            MainWindow.DS.Tables.Remove("AppCategory");
            MoreApps.Add(Catborder);
            CategoryAppAppList = new ColorList();
            Size AppItemSize = new Size(0, 25);
            Size AppListSize = new Size(200, 0);
            double[] CategoryAppColumnWidth = { 200 };
            string[] CategoryAppHeadersName = { "Apps Name" };
            string[] CategoryAppColumnsName = { "AppsName" };
            MainWindow.DataBaseAgent.SelectData("AppCategory", ref MainWindow.DS, "*", "AppCategory", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
            Border CatAppborder = CategoryAppAppList.Draw(CategoryColumnWidth, 30, CategoryAppHeadersName, null, AppListSize, AppItemSize, Color.FromArgb(255, 238, 241, 245), Colors.Transparent,
                Colors.White, Colors.Black, Color.FromArgb(255, 42, 180, 192), Colors.LightGray, Colors.Transparent, MainWindow.DS.Tables["AppCategory"],
                new Thickness(20, 0, 0, 0), new Thickness(0), 12, 16, CategoryAppColumnsName, AppCat_SelectionChanged, "AppsList", 150);
            CatAppborder.VerticalAlignment = VerticalAlignment.Top;
            CatAppborder.HorizontalAlignment = HorizontalAlignment.Left;
            ListStP.Margin = new Thickness(40, 55, 0, 5);
            ListStP.Children.Add(CatAppborder);
            MainTemp.Children.Add(ListStP);
            MoreApps.Add(ListStP);
            StackPanel CatBtnsStP = new StackPanel();
            CatBtnsStP.Orientation = Orientation.Horizontal;
            Button NewCat = new Button();
            ImageBrush NweBtnImage = new ImageBrush();
            NweBtnImage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/add_to_folder_9lM_icon.ico"));
            NewCat.Click += NewCatClicked;
            NewCat.Cursor = Cursors.Hand;
            NewCat.Template = MaxBtn.Template;
            NewCat.Background = NweBtnImage;
            NewCat.BorderThickness = new Thickness(0);
            NewCat.Loaded += NewApp_Loaded;
            NewCat.Margin = new Thickness(15, 0, 15, 0);
            NewCat.Height = 40;
            NewCat.Width = 40;
            NewCat.HorizontalAlignment = HorizontalAlignment.Left;
            NewCat.VerticalAlignment = VerticalAlignment.Top;
            CatBtnsStP.Children.Add(NewCat);
            ImageBrush NewAppImage = new ImageBrush();
            NewAppImage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/archive_insert.ico"));
            Button NewApp = new Button();
            NewApp.Background = NewAppImage;
            NewApp.Click += NewAppClicked;
            NewApp.Loaded += NewApp_Loaded;
            NewApp.Cursor = Cursors.Hand;
            NewApp.Uid = "NewAppInCatBtn";
            NewApp.Margin = new Thickness(15,0,15,0);
            NewApp.Height = 40;
            NewApp.Width = 40;
            NewApp.IsEnabled = false;
            NewApp.Template = MaxBtn.Template;
            NewApp.BorderThickness = new Thickness(0);
            NewApp.HorizontalAlignment = HorizontalAlignment.Left;
            NewApp.VerticalAlignment = VerticalAlignment.Top;
            CatBtnsStP.Children.Add(NewApp);
            Button DelCat = new Button();
            ImageBrush DelBtnImage = new ImageBrush();
            DelBtnImage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/add_to_folder2_Hm8_icon.ico"));
            DelCat.Click += DeletCatClicked;
            DelCat.Cursor = Cursors.Hand;
            DelCat.Background = DelBtnImage;
            DelCat.Template = MaxBtn.Template;
            DelCat.BorderThickness = new Thickness(0);
            DelCat.Loaded += NewApp_Loaded;
            DelCat.Margin = new Thickness(15, 0, 15, 0);
            DelCat.Height = 40;
            DelCat.Width = 40;
            DelCat.IsEnabled = false;
            DelCat.Uid = "DeleteInstaledAppCatBtn";
            DelCat.HorizontalAlignment = HorizontalAlignment.Left;
            DelCat.VerticalAlignment = VerticalAlignment.Top;
            CatBtnsStP.Children.Add(DelCat);
            CatBtnsStP.Uid = "CatBtnsStP";
            ImageBrush DelAppBtnImage = new ImageBrush();
            DelAppBtnImage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/archive_remove(1).ico"));
            Button DelApp = new Button();
            DelApp.Background = DelAppBtnImage;
            DelApp.Cursor = Cursors.Hand;
            DelApp.BorderThickness = new Thickness(0);
            DelApp.Click += DeleteAppClicked;
            DelApp.Uid = "DeleteInstalledAppinCatBtn";
            DelApp.Template = MaxBtn.Template;
            DelApp.Loaded += NewApp_Loaded;
            DelApp.Margin = new Thickness(15, 0, 15, 0);
            DelApp.Height = 40;
            DelApp.Width = 40;
            DelApp.IsEnabled = false;
            DelApp.HorizontalAlignment = HorizontalAlignment.Left;
            DelApp.VerticalAlignment = VerticalAlignment.Top;
            CatBtnsStP.Children.Add(DelApp);
            CatBtnsStP.Margin = new Thickness(70, 20, 15, 15);
            MainTemp.Children.Add(CatBtnsStP);
            MoreApps.Add(CatBtnsStP);
            Image InfoImage2 = new System.Windows.Controls.Image();
            InfoImage2.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_info-blog_46810.ico"));
            InfoImage2.Width = 30;
            InfoImage2.Height = 30;
            InfoImage2.VerticalAlignment = VerticalAlignment.Top;
            InfoImage2.HorizontalAlignment = HorizontalAlignment.Left;
            InfoImage2.Margin = new Thickness(50, 0, 0, 0);
            ListStP.Children.Add(InfoImage2);
            TextBlock InfoTb3 = new TextBlock();
            InfoTb3.Text = "If you have not any category\nwe cant show applications data\nin chart.";
            InfoTb3.FontSize = 16;
            InfoTb3.VerticalAlignment = VerticalAlignment.Top;
            InfoTb3.HorizontalAlignment = HorizontalAlignment.Left;
            InfoTb3.Foreground = new SolidColorBrush(Colors.Black);
            InfoTb3.Margin = new Thickness(0, 20, 0, 0);
            ListStP.Children.Add(InfoTb3);
            TextBlock TitleTB1 = new TextBlock();
            TitleTB1.Text = "Blocking";
            TitleTB1.FontSize = 24;
            TitleTB1.Foreground = new SolidColorBrush(Colors.Gray);
            TitleTB1.Margin = new Thickness(30, 20, 0, 10);
            TitleTB1.VerticalAlignment = VerticalAlignment.Top;
            TitleTB1.HorizontalAlignment = HorizontalAlignment.Left;
            MainTemp.Children.Add(TitleTB1);
            MoreApps.Add(TitleTB1);
            BlockingAppList = new ColorList();
            Size BlockAppItemSize = new Size(0, 25);
            Size BlockAppListSize = new Size(520, 0);
            double[] BlockingAppColumnWidth = { 250 , 200 , 70};
            string[] BlockingAppHeadersName = { "App Name" , "Category" , "Act" };
            string[] BlockingAppColumnsName = { "AppName" , "Category", "Act"};
            MainWindow.DataBaseAgent.SelectData("BlockApps", ref MainWindow.DS, "*", "BlockApps", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
            Border BlockAppborder = BlockingAppList.Draw(BlockingAppColumnWidth, 30, BlockingAppHeadersName, null, BlockAppListSize, BlockAppItemSize, Color.FromArgb(255, 238, 241, 245), Colors.Transparent,
                Colors.White, Colors.Black, Color.FromArgb(255, 42, 180, 192), Colors.LightGray, Colors.Transparent, MainWindow.DS.Tables["BlockApps"],
                new Thickness(30, 30, 0, 20), new Thickness(0), 12, 16, BlockingAppColumnsName, BlockAppList_SelectionChanged, "BlockAppList", 200);
            BlockAppborder.VerticalAlignment = VerticalAlignment.Top;
            BlockAppborder.HorizontalAlignment = HorizontalAlignment.Left;
            MainWindow.DS.Tables.Remove("BlockApps");
            MainTemp.Children.Add(BlockAppborder);
            MoreApps.Add(BlockAppborder);
            StackPanel BlockingBtnsStP = new StackPanel();
            BlockingBtnsStP.Orientation = Orientation.Horizontal;
            BlockingBtnsStP.Margin = new Thickness(50, 20, 0, 15);
            ImageBrush AddBlockListBtnImage = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/Double-J-Design-Origami-Colored-Pencil-Red-document-plus.ico")));
            Button AddBlock = new Button();
            AddBlock.Uid = "AddBlockAppBtn";
            AddBlock.BorderThickness = new Thickness(0);
            AddBlock.Width = 40;
            AddBlock.Cursor = Cursors.Hand;
            AddBlock.Height = 40;
            AddBlock.Template = MaxBtn.Template;
            AddBlock.Background = AddBlockListBtnImage;
            AddBlock.HorizontalAlignment = HorizontalAlignment.Left;
            AddBlock.VerticalAlignment = VerticalAlignment.Top;
            AddBlock.Click += AddBlock_Click;
            AddBlock.Margin = new Thickness(15, 0, 15, 0);
            BlockingBtnsStP.Children.Add(AddBlock);
            ImageBrush DeleteBlockListBtnImage = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/Double-J-Design-Origami-Colored-Pencil-Red-document-cross.ico")));
            Button DeleteBlock = new Button();
            DeleteBlock.BorderThickness = new Thickness(0);
            DeleteBlock.Cursor = Cursors.Hand;
            DeleteBlock.Template = MaxBtn.Template;
            DeleteBlock.Background = DeleteBlockListBtnImage;
            DeleteBlock.Uid = "deleteBlockAppBtn";
            DeleteBlock.HorizontalAlignment = HorizontalAlignment.Left;
            DeleteBlock.VerticalAlignment = VerticalAlignment.Top;
            DeleteBlock.Click += DeletBlock_Click;
            DeleteBlock.IsEnabled = false;
            DeleteBlock.Height = 40;
            DeleteBlock.Width = 40;
            DeleteBlock.Margin = new Thickness(15,0,15,0);
            BlockingBtnsStP.Children.Add(DeleteBlock);
            BlockingBtnsStP.Uid = "BlockingBtnsStP";
            MainTemp.Children.Add(BlockingBtnsStP);
            MoreApps.Add(BlockingBtnsStP);

            List<KeyValuePair<string, float>> PIValue = new List<KeyValuePair<string, float>>();
            
            MainWindow.DataBaseAgent.SelectData("AppUsage", ref MainWindow.DS, "*", "AppUsage", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
            foreach (System.Data.DataRow Row in MainWindow.DS.Tables["AppUsage"].Rows)
            {
                PIValue.Add(new KeyValuePair<string, float>(Row["AppName"].ToString(), (float)Row["Usage"]));
            }
            MainWindow.DS.Tables.Remove("AppUsage");
            List<Charts.TriadPairs> ChartData = new List<Charts.TriadPairs>();
          
            foreach (System.Data.DataRow Row in MainWindow.DS.Tables["AppCategory"].Rows)
            {
                Charts.TriadPairs Data = new Charts.TriadPairs();
                PropertyInfo ColorInfo = typeof(Colors).GetProperty(Row["Color"].ToString());
                Data.Tag = Row["Name"].ToString();
                Data.Value = Row["AppsName"].ToString().Split(',').Length;
                Data.Color = (Color)ColorInfo.GetValue(ColorInfo);
                ChartData.Add(Data);
            }
            MainWindow.DS.Tables.Remove("AppCategory");
            if (ChartData.Count > 0)
            {
                
                TextBlock InfoTb4 = new TextBlock();
                InfoTb4.Text = "Charts ";
                InfoTb4.FontSize = 25;
                InfoTb4.Foreground = new SolidColorBrush(Colors.Gray);
                InfoTb4.Margin = new Thickness(30, 20, 0, 10);
                InfoTb4.VerticalAlignment = VerticalAlignment.Top;
                InfoTb4.HorizontalAlignment = HorizontalAlignment.Left;
                MainTemp.Children.Add(InfoTb4);
                MoreApps.Add(InfoTb4);
                Canvas PieChartBackground = new Canvas();
                PieChartBackground.VerticalAlignment = VerticalAlignment.Top;
                PieChartBackground.HorizontalAlignment = HorizontalAlignment.Left;
                PieChartBackground.Width = 200;
                PieChartBackground.Height = 200;
                PieChartBackground.Margin = new Thickness(30, 40, 0, 20);
                MainTemp.Children.Add(PieChartBackground);
                MoreApps.Add(PieChartBackground);
                Charts PiChart = new Charts();
                PiChart.DrawPieChart(ref PieChartBackground, ChartData, new Point(100, 100), 50, 100, new SolidColorBrush(Colors.Transparent), 0,
                    Colors.Violet, Colors.Thistle, new Size(16, 16), 250, "");
            }
        }

        private void DeleteAppClicked(object sender, RoutedEventArgs e)
        {
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            ListBox Target = (ListBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "CategoryList");
            ListBox TargetApp = (ListBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "AppsList");
            if (Target.SelectedIndex > -1)
            {
                MainWindow.DataBaseAgent.SelectData("AppCategory", ref MainWindow.DS, "*", "AppCategory", Target.SelectedItem.ToString(), "Name");
                foreach(string var in MainWindow.DS.Tables["AppCategory"].Rows[0]["AppsName"].ToString().Split(','))
                {
                    if(var == TargetApp.SelectedItem.ToString())
                    {
                        MainWindow.DS.Tables["AppCategory"].Rows[0]["AppsName"] = MainWindow.DS.Tables["AppCategory"].Rows[0]["AppsName"].ToString().Replace(var, "");
                    }
                }
                MainWindow.DataBaseAgent.UpdateData(MainWindow.DS.Tables["AppCategory"]);
                MainWindow.DS.Tables.Remove("AppCategory");
                TargetApp.Items.RemoveAt(TargetApp.SelectedIndex);
            }
        }

        private void DeletBlock_Click(object sender, RoutedEventArgs e)
        {
            
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            ListView Target = (ListView)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "BlockAppList");

            if (MainWindow.Connection.ConnectionIsAlive == true)
            {
                Packet Pack = new Packet();
                Packet.Apps PackData = new Packet.Apps();
                PackData.AppName = ((BlockApp)Target.SelectedItem).AppName.ToString();
                Packet.PSProPacket ProDataPack = new Packet.PSProPacket();
                ProDataPack.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
                ProDataPack.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                ProDataPack.Type = (short)Packet.PacketType.Apps;
                PackData.Type = (short)Packet.AppsType.RemoveBlock;
                string Data = Pack.ToString(PackData);
                string ProData = Pack.ToString(ProDataPack);
                ProDataPack.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                Packet.MainPacket MainData = new Packet.MainPacket();
                MainData.Data = Data;
                MainData.PPacket = Pack.ToString(ProDataPack);
                string MainStrData = Pack.ToString<Packet.MainPacket>(MainData);
                MainWindow.Connection.SendDataSM.WaitOne();
                MainWindow.Connection.SendToServer(MainStrData);
                MainWindow.Connection.SendDataSM.Release();
            }
            MainWindow.DataBaseAgent.ExequteWithCommand("Delete From BlockApps where AppName ='" + ((BlockApp)Target.SelectedItem).AppName + "'");
            Target.Items.RemoveAt(Target.SelectedIndex);
        }

        private void AddBlock_Click(object sender, RoutedEventArgs e)
        {
            AddBDialog = new AddBlockApp();
            AddBDialog.ChildID = ChildUser;
            this.Hide();
            AddBDialog.Show();
            AddBDialog.Closed += AddBDialog_Closed;
        }

        private void AddBDialog_Closed(object sender, EventArgs e)
        {
            WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
            if (SelctedToAdd != null)
            {
                
                MainWindow.DataBaseAgent.SelectData("BlockApps", ref MainWindow.DS, "*", "BlockApps", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
                DataRow Row = MainWindow.DS.Tables["BlockApps"].NewRow();
                Row["AppName"] = SelctedToAdd.AppName.ToString();
                Row["Act"] = SelctedToAdd.Act.ToString();
                switch(SelctedToAdd.Act)
                {
                    case "0":
                        {
                            Row["Act"] = "Close";
                        };break;
                    case "1":
                        {
                            Row["Act"] = "Close with Message";
                        }; break;
                }
                Row["AppID"] = SelctedToAdd.AppID.ToString();
                Row["ID"] = SelctedToAdd.ID.ToString();
                Row["ChildID"] = ChildsCombo.SelectedItem.ToString().ToString();
                DataSet TargetDS = new DataSet();
                MainWindow.DataBaseAgent.SelectData("AppCategory", ref TargetDS, "*", "AppCategory", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
                foreach (DataRow var in TargetDS.Tables["AppCategory"].Rows)
                {
                    if(var["AppsName"].ToString().Contains(SelctedToAdd.AppName) == true)
                    {
                        Row["Category"] = SelctedToAdd.Category;
                    }
                }
                MainWindow.DS.Tables["BlockApps"].Rows.Add(Row);
                MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["BlockApps"]);
                BlockingAppList.AddNewItem(Row);
                MainWindow.DS.Tables.Remove("BlockApps");
                Packet Pack = new Packet();
                Packet.Apps PackData = new Packet.Apps();
                PackData.AppName = Row["AppName"].ToString();
                switch(Row["Act"].ToString())
                {
                    case "Close":
                        {
                            PackData.Type = (short)Packet.AppsType.Block;
                        };break;
                    case "Close with Message":
                        {
                            PackData.Type = (short)Packet.AppsType.BlockWithErro;
                        };break;
                }
                if(MainWindow.Connection.ConnectionIsAlive == true)
                {
                    Packet.PSProPacket ProDataPack = new Packet.PSProPacket();
                    ProDataPack.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
                    ProDataPack.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                    ProDataPack.Type = (short)Packet.PacketType.Apps;
                    string Data = Pack.ToString(PackData);
                    string ProData = Pack.ToString(ProDataPack);
                    ProDataPack.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                    Packet.MainPacket MainData = new Packet.MainPacket();
                    MainData.Data = Data;
                    MainData.PPacket = Pack.ToString(ProDataPack);
                    string MainStrData = Pack.ToString<Packet.MainPacket>(MainData);
                    MainWindow.Connection.SendDataSM.WaitOne();
                    MainWindow.Connection.SendToServer(MainStrData);
                    MainWindow.Connection.SendDataSM.Release();
                }
                
            }
            
        }

        private void BlockAppList_SelectionChanged(object sender)
        {
            MainDataGrid.Dispatcher.Invoke(() =>
            {
                WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
                StackPanel stackPanel = (StackPanel)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "BlockingBtnsStP");
                Button Remove = (Button)stackPanel.Children.Cast<UIElement>().First(x => x.Uid == "deleteBlockAppBtn");
                if (BlockingAppList.SelectedIndex > -1)
                {
                    Remove.IsEnabled = true;
                }
                else
                {
                    Remove.IsEnabled = false;
                }
            });
            
        }

        private void DeletCatClicked(object sender, RoutedEventArgs e)
        {
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            ListBox Target = (ListBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "CategoryList");
            if(Target.SelectedIndex > -1)
            {
                DataSet TargetDS = new DataSet();
                MainWindow.DataBaseAgent.SelectData("AppCategory", ref TargetDS, "*" , "AppCategory",Target.SelectedItem.ToString(),"Name" );
                if(TargetDS.Tables.Count > 0)
                {
                    MainWindow.DataBaseAgent.ExequteWithCommand("Delete from UsedColor where Name ='" + TargetDS.Tables[0].Rows[0][0].ToString() + "'");
                    MainWindow.DataBaseAgent.ExequteWithCommand("Delete from AppCategory where Name ='" + Target.SelectedItem.ToString() + "'");
                }
                Target.Items.RemoveAt(Target.SelectedIndex);
            }
        }


        private void InstalledApp_SelectionChanged(object sender)
        {
            MainDataGrid.Dispatcher.Invoke(() =>
            {
                Border Target = sender as Border;
                WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
                Button Uninstall = (Button)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "UninstallInstallAppBtn");
                if (((SolidColorBrush)Target.Background).Color == Colors.Cornsilk)
                {
                    Uninstall.IsEnabled = true;
                }
                else
                {
                    Uninstall.IsEnabled = false;
                }
            });
            
        }

        private void CategoryApp_SelectionChanged(object sender)
        {
            MainDataGrid.Dispatcher.Invoke(() =>
            {
                Border Target = sender as Border;


                WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
                StackPanel stackPanel = (StackPanel)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "CatBtnsStP");
                Button Remove = (Button)stackPanel.Children.Cast<UIElement>().First(x => x.Uid == "DeleteInstaledAppCatBtn");
                Button AddApp = (Button)stackPanel.Children.Cast<UIElement>().First(x => x.Uid == "NewAppInCatBtn");
                Button RemoveApp = (Button)stackPanel.Children.Cast<UIElement>().First(x => x.Uid == "DeleteInstalledAppinCatBtn");
                
                RemoveApp.IsEnabled = true;
                CategoryAppAppList.Clear();
                if (CategoryAppList.SelectedIndex > -1)
                {
                    
                    MainWindow.DataBaseAgent.SelectData("AppCategory", ref MainWindow.DS, "*", "AppCategory", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
                    foreach (string var in MainWindow.DS.Tables["AppCategory"].Rows[CategoryAppList.SelectedIndex]["AppsName"].ToString().Split(','))
                    {
                        if (var != "")
                        {
                            CategoryAppAppList.AddNewItem(var);
                        }

                    }
                    MainWindow.DS.Tables.Remove("AppCategory");
                    if (CategoryAppAppList.SelectedIndex > -1)
                    {
                        Remove.IsEnabled = true;
                        AddApp.IsEnabled = true;
                        RemoveApp.IsEnabled = true;
                    }
                    else
                    {
                        Remove.IsEnabled = true;
                        AddApp.IsEnabled = true;
                        RemoveApp.IsEnabled = false;
                    }
                    SelectedCat = CategoryAppAppList.GetDataAt(CategoryAppAppList.SelectedIndex)[0];
                }



            });

        }

        private void AppCat_SelectionChanged(object sender)
        {
            MainDataGrid.Dispatcher.Invoke(() =>
            {


            });

        }

        private void ImagesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView Target = sender as ListView;
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            Button Remove = (Button)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "RemoveImageBtn");
            if(Target.SelectedIndex > -1)
            {
                Remove.IsEnabled = true;
            }
        }

        private void NetworkAdaptorList_SelectionChanged(object sender)
        {
            MainDataGrid.Dispatcher.Invoke(() =>
            {
                WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
                Button Enable = (Button)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "NetworkAdaptorEnable");
                Button Disable = (Button)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "NetworkAdaptordisable");
                Border Target = sender as Border;
                if (((TextBlock)((Border)((StackPanel)Target.Child).Children[2]).Child).Text == "Enable")
                {
                    Disable.IsEnabled = true;
                    Enable.IsEnabled = false;
                }
                else
                {
                    if ((SolidColorBrush)Target.Background != new SolidColorBrush(Colors.Cornsilk))
                    {
                        Enable.IsEnabled = true;
                        Disable.IsEnabled = false;
                    }
                    else
                    {
                        Enable.IsEnabled = false;
                        Disable.IsEnabled = false;
                    }

                }
            });
            
        }

        private void NetworkExpander_Expanded(object sender, RoutedEventArgs e)
        {
            WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
            MoreNetwork = new List<object>();
            Expander Target = sender as Expander;
            TextBlock TitleTB = new TextBlock();
            TitleTB.Text = "Network Connect History";
            TitleTB.FontSize = 24;
            TitleTB.Foreground = new SolidColorBrush(Colors.Gray);
            TitleTB.Margin = new Thickness(30, 25, 0, 0);
            TitleTB.VerticalAlignment = VerticalAlignment.Top;
            TitleTB.HorizontalAlignment = HorizontalAlignment.Left;
            MainTemp.Children.Add(TitleTB);
            MoreNetwork.Add(TitleTB);

            NetworkSemaphore.WaitOne();
            MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From Network Where ChildID ='" + ChildsCombo.SelectedItem.ToString() + "' order by ID Desc", ref MainWindow.DS, "Network");
            double[] ColumnWidth = { 325,180,100,200,100,150,150 };
            string[] HeadersName = { "Device Name", "Connection Interface", "Modem Name","IPv6","IPv4","Status","Date" };
            string[] ColumnsName = { "DivaceName", "ConnectionInterface", "ModemName", "IPv6", "IPv4", "Status", "ID" };
            ColorList NetworkList = new ColorList();
            Size ItemSize = new Size(0, 25);
            Size ListSize = new Size(1300, 20 * (ItemSize.Height + 5));
            Border NetworkConnectionListBorder = NetworkList.Draw(ColumnWidth, 30, HeadersName, null, ListSize, ItemSize, Color.FromArgb(255, 238, 241, 245), Colors.Transparent,
                Colors.White, Colors.Black, Color.FromArgb(255, 42, 180, 192), Colors.LightGray, Colors.Transparent, MainWindow.DS.Tables["Network"],
                new Thickness(30, 20, 15, 10), new Thickness(0), 12, 16, ColumnsName, null, "NetworkConnectionsList", 300);
            MainTemp.Children.Add(NetworkConnectionListBorder);
            MoreNetwork.Add(NetworkConnectionListBorder);
          
            MainWindow.DS.Tables.Remove("Network");
            NetworkSemaphore.Release();
            TextBlock VPNTitleTB = new TextBlock();
            VPNTitleTB.Text = "VPN";
            VPNTitleTB.FontSize = 24;
            VPNTitleTB.Foreground = new SolidColorBrush(Colors.Gray);
            VPNTitleTB.Margin = new Thickness(30, 20, 50, 20);
            VPNTitleTB.VerticalAlignment = VerticalAlignment.Top;
            VPNTitleTB.HorizontalAlignment = HorizontalAlignment.Left;
            MainTemp.Children.Add(VPNTitleTB);
            MoreNetwork.Add(VPNTitleTB);

            VPNSemaphore.WaitOne();
            MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From VPN Where ChildID ='" + ChildsCombo.SelectedItem.ToString() + "' order by ID Desc", ref MainWindow.DS, "VPN");
            double[] VPNColumnWidth = { 325, 180, 100, 200, 100, 150, 150 };
            string[] VPNHeadersName = { "Status", "Date"};
            string[] VPNColumnsName = { "Status", "StartVPN"};
            Size VPNItemSize = new Size(0, 25);
            Size VPNListSize = new Size(500, 20 * (ItemSize.Height + 5));
            Border VPNListBorder = NetworkList.Draw(VPNColumnWidth, 30, VPNHeadersName, null, VPNListSize, VPNItemSize,
                Color.FromArgb(255, 238, 241, 245), Colors.Transparent, Colors.White, Colors.Black, Color.FromArgb(255, 42, 180, 192),
                Colors.LightGray, Colors.Transparent, MainWindow.DS.Tables["VPN"], new Thickness(300, 100, 15, 10), new Thickness(0)
                , 12, 16, VPNColumnsName, null, "VPNList", 300);
            MainTemp.Children.Add(VPNListBorder);

            MainWindow.DS.Tables.Remove("VPN");
            VPNSemaphore.Release();
            MoreNetwork.Add(VPNListBorder);
        }

        private void NetworkExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
            foreach (object var in MoreNetwork)
            {
                MainTemp.Children.Remove((UIElement)var);
            }
        }

        private void GoogleMap_LoadCompleted(object sender, NavigationEventArgs e)
        {
        }

        private void MoreURLs_Collapsed(object sender, RoutedEventArgs e)
        {
            WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
            foreach (object var in MoreUrls)
            {
                MainTemp.Children.Remove((UIElement)var);
            }
        }

        private void MoreURLs_Expanded(object sender, RoutedEventArgs e)
        {
            WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
            MoreUrls = new List<object>();
            Image InfoImage0 = new System.Windows.Controls.Image();
            InfoImage0.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_info-blog_46810.ico"));
            InfoImage0.Width = 30;
            InfoImage0.Height = 30;
            InfoImage0.VerticalAlignment = VerticalAlignment.Top;
            InfoImage0.HorizontalAlignment = HorizontalAlignment.Left;
            InfoImage0.Margin = new Thickness(30, 30, 0, 0);
            MainTemp.Children.Add(InfoImage0);
            TextBlock InfoTb0 = new TextBlock();
            InfoTb0.Text = "You can show all Blocked URLs and add or delete URLs in block list";
            InfoTb0.FontSize = 16;
            InfoTb0.VerticalAlignment = VerticalAlignment.Top;
            InfoTb0.HorizontalAlignment = HorizontalAlignment.Left;
            InfoTb0.Foreground = new SolidColorBrush(Colors.Black);
            InfoTb0.Margin = new Thickness(45, 0, 0, 0);
            MainTemp.Children.Add(InfoTb0);
            MoreUrls.Add(InfoImage0);
            MoreUrls.Add(InfoTb0);
            TextBlock CatTB = new TextBlock();
            CatTB.Text = "URL Category";
            CatTB.FontSize = 24;
            CatTB.Foreground = new SolidColorBrush(Colors.Gray);
            CatTB.Margin = new Thickness(30, 50, 0, 0);
            CatTB.VerticalAlignment = VerticalAlignment.Top;
            CatTB.HorizontalAlignment = HorizontalAlignment.Left;
            MainTemp.Children.Add(CatTB);
            MoreUrls.Add(CatTB);
            StackPanel URLCateStP = new StackPanel();
            URLCateStP.Orientation = Orientation.Horizontal;
            if (MainWindow.DS.Tables.Contains("URLCategury") == true)
            {
                MainWindow.DS.Tables.Remove("URLCategury");
            }
            MainWindow.DataBaseAgent.SelectData("URLCategury", ref MainWindow.DS, "*", "URLCategury", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
            double[] CatColumnWidth = { 300 };
            string[] CatHeadersName = { "Categorys" };
            string[] CatColumnsName = { "Name" };
            URLCategoryList = new ColorList();
            Size CatItemSize = new Size(0, 25);
            Size CatListSize = new Size(300, 0);
            Border Catborder = URLCategoryList.Draw(CatColumnWidth, 30, CatHeadersName, null, CatListSize, CatItemSize, Color.FromArgb(255, 238, 241, 245), Colors.Transparent,
                Colors.White, Colors.Black, Color.FromArgb(255, 42, 180, 192), Colors.LightGray, Colors.Transparent, MainWindow.DS.Tables["URLCategury"],
                new Thickness(50, 50, 0, 0), new Thickness(0), 12, 16, CatColumnsName, CatURLView_SelectionChanged, "CatUrlList",300);
            Catborder.HorizontalAlignment = HorizontalAlignment.Left;
            Catborder.VerticalAlignment = VerticalAlignment.Top;
            URLCateStP.Children.Add(Catborder);
            URLCateStP.Uid = "URLCateStP";
            MainWindow.DS.Tables.Remove("URLCategury");
            string[] URLCatHeadersName = { "URL" };
            string[] URLCatColumnsName = { "URLs" };
            URLsList = new ColorList();
            if (MainWindow.DS.Tables.Contains("URLsInCategury") == true)
            {
                MainWindow.DS.Tables.Remove("URLsInCategury");
            }
            Border URLborder = URLsList.Draw(CatColumnWidth, 30, URLCatHeadersName, null, CatListSize, CatItemSize, Color.FromArgb(255, 238, 241, 245), Colors.Transparent,
                Colors.White, Colors.Black, Color.FromArgb(255, 42, 180, 192), Colors.LightGray, Colors.Transparent, null,
                new Thickness(50, 50, 0, 0), new Thickness(0), 12, 16, URLCatColumnsName, URLsInCatView_SelectionChanged, "URLInCatList", 300);
            URLborder.VerticalAlignment = VerticalAlignment.Top;
            URLborder.HorizontalAlignment = HorizontalAlignment.Left;
            URLCateStP.Children.Add(URLborder);
            StackPanel CatAndURLControlStP = new StackPanel();
            CatAndURLControlStP.Orientation = Orientation.Vertical;
            CatAndURLControlStP.Margin = new Thickness(50, 50, 0, 0);
            WrapPanel CateWrapPanel = new WrapPanel();
            CateWrapPanel.Orientation = Orientation.Vertical;
            TextBlock CategoryControlTB = new TextBlock();
            CategoryControlTB.Text = "Category :";
            CategoryControlTB.Margin = new Thickness(0, 0, 0, 0);
            CategoryControlTB.FontSize = 18;
            CategoryControlTB.Foreground = new SolidColorBrush(Colors.Blue);
            CategoryControlTB.VerticalAlignment = VerticalAlignment.Top;
            CategoryControlTB.HorizontalAlignment = HorizontalAlignment.Left;
            CateWrapPanel.Children.Add(CategoryControlTB);
            TextBlock CategoryNameTB = new TextBlock();
            CategoryNameTB.Text = "Category Name :";
            CategoryNameTB.Margin = new Thickness(0, 25, 0, 0);
            CategoryNameTB.FontSize = 14;
            CategoryNameTB.VerticalAlignment = VerticalAlignment.Top;
            CategoryNameTB.HorizontalAlignment = HorizontalAlignment.Left;
            CateWrapPanel.Children.Add(CategoryNameTB);
            TextBox CategoryNameTxB = new TextBox();
            CategoryNameTxB.Margin = new Thickness(0, 10, 0, 0);
            CategoryNameTxB.Uid = "CategoryNameTxB";
            CategoryNameTxB.MinWidth = 300;
            CategoryNameTxB.FontSize = 14;
            CategoryNameTxB.VerticalAlignment = VerticalAlignment.Top;
            CategoryNameTxB.HorizontalAlignment = HorizontalAlignment.Left;
            CateWrapPanel.Children.Add(CategoryNameTxB);
            TextBlock CategoryNameErrorTB = new TextBlock();
            CategoryNameErrorTB.Text = "";
            CategoryNameErrorTB.Uid = "CategoryNameErrorTB";
            CategoryNameErrorTB.Margin = new Thickness(0, 0, 0, 0);
            CategoryNameErrorTB.FontSize = 12;
            CategoryNameErrorTB.Foreground = new SolidColorBrush(Colors.Red);
            CategoryNameErrorTB.VerticalAlignment = VerticalAlignment.Top;
            CategoryNameErrorTB.HorizontalAlignment = HorizontalAlignment.Left;
            CateWrapPanel.Children.Add(CategoryNameErrorTB);
            StackPanel CategoryControlBtnsStP = new StackPanel();
            CategoryControlBtnsStP.Orientation = Orientation.Horizontal;
            Button AddCategury = new Button();
            AddCategury.Uid = "AddURLCateguryBtn";
            AddCategury.Cursor = Cursors.Hand;
            AddCategury.Click += AddURLCateguryCilicked;
            AddCategury.BorderThickness = new Thickness(0);
            ImageBrush AddImage = new ImageBrush();
            AddImage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_add_1927206.ico"));
            AddCategury.Background = AddImage;
            AddCategury.Template = MaxBtn.Template;
            AddCategury.Margin = new Thickness(0, 10, 0, 0);
            AddCategury.VerticalAlignment = VerticalAlignment.Top;
            AddCategury.HorizontalAlignment = HorizontalAlignment.Left;
            AddCategury.Height = 50;
            AddCategury.Width = 60;
            CategoryControlBtnsStP.Children.Add(AddCategury);
            ImageBrush DelImage = new ImageBrush();
            DelImage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_remove_1927216.ico"));
            Button DeleteCategury = new Button();
            DeleteCategury.Uid = "DeleteURLCateguryBtn";
            DeleteCategury.Click += DeleteURLCateguryCilicked;
            DeleteCategury.Background = DelImage;
            DeleteCategury.BorderThickness = new Thickness(0);
            DeleteCategury.Template = MaxBtn.Template;
            DeleteCategury.Cursor = Cursors.Hand;
            DeleteCategury.Height = 50;
            DeleteCategury.Width = 50;
            DeleteCategury.IsEnabled = false;
            DeleteCategury.Margin = new Thickness(30, 10, 0, 0);
            DeleteCategury.VerticalAlignment = VerticalAlignment.Top;
            DeleteCategury.HorizontalAlignment = HorizontalAlignment.Left;
            DeleteCategury.MinHeight = 25;
            DeleteCategury.MinWidth = 60;
            CategoryControlBtnsStP.Children.Add(DeleteCategury);
            CateWrapPanel.Children.Add(CategoryControlBtnsStP);
            WrapPanel URLsControlWrapPanel = new WrapPanel();
            URLsControlWrapPanel.Orientation = Orientation.Vertical;
            URLsControlWrapPanel.Margin = new Thickness(0, 10, 0, 0);
            TextBlock URLsControlTB = new TextBlock();
            URLsControlTB.Text = "URLs :";
            URLsControlTB.Margin = new Thickness(0, 0, 0, 0);
            URLsControlTB.FontSize = 18;
            URLsControlTB.Foreground = new SolidColorBrush(Colors.Blue);
            URLsControlTB.VerticalAlignment = VerticalAlignment.Top;
            URLsControlTB.HorizontalAlignment = HorizontalAlignment.Left;
            URLsControlWrapPanel.Children.Add(URLsControlTB);
            TextBlock URLsNameTB = new TextBlock();
            URLsNameTB.Text = "URL Address :";
            URLsNameTB.Margin = new Thickness(0, 25, 0, 0);
            URLsNameTB.FontSize = 14;
            URLsNameTB.Uid = "URLsNameTB";
            URLsNameTB.VerticalAlignment = VerticalAlignment.Top;
            URLsNameTB.HorizontalAlignment = HorizontalAlignment.Left;
            URLsControlWrapPanel.Children.Add(URLsNameTB);
            TextBox URLNameTxB = new TextBox();
            URLNameTxB.Margin = new Thickness(0, 10, 0, 0);
            URLNameTxB.Uid = "URLsNameTxB";
            URLNameTxB.MinWidth = 300;
            URLNameTxB.FontSize = 14;
            URLNameTxB.VerticalAlignment = VerticalAlignment.Top;
            URLNameTxB.HorizontalAlignment = HorizontalAlignment.Left;
            URLsControlWrapPanel.Children.Add(URLNameTxB);
            TextBlock URLsNameErrorTB = new TextBlock();
            URLsNameErrorTB.Text = "";
            URLsNameErrorTB.Uid = "URLsNameErrorTB";
            URLsNameErrorTB.Margin = new Thickness(0, 0, 0, 0);
            URLsNameErrorTB.FontSize = 12;
            URLsNameErrorTB.Foreground = new SolidColorBrush(Colors.Red);
            URLsNameErrorTB.VerticalAlignment = VerticalAlignment.Top;
            URLsNameErrorTB.HorizontalAlignment = HorizontalAlignment.Left;
            URLsControlWrapPanel.Children.Add(URLsNameErrorTB);
            StackPanel URLsControlBtnsStP = new StackPanel();
            URLsControlBtnsStP.Orientation = Orientation.Horizontal;
            URLsControlBtnsStP.Uid = "URLsControlBtnsStP";
            Button AddURL = new Button();
            AddURL.Uid = "AddURLCateguryBtn";
            AddURL.Cursor = Cursors.Hand;
            AddURL.Click += AddURLCilicked;
            AddURL.BorderThickness = new Thickness(0);
            ImageBrush AddUrlImage = new ImageBrush();
            AddUrlImage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_add_1927206.ico"));
            AddURL.Background = AddUrlImage;
            AddURL.Template = MaxBtn.Template;
            AddURL.Margin = new Thickness(0, 10, 0, 0);
            AddURL.VerticalAlignment = VerticalAlignment.Top;
            AddURL.HorizontalAlignment = HorizontalAlignment.Left;
            AddURL.Height = 50;
            AddURL.Width = 60;
            URLsControlBtnsStP.Children.Add(AddURL);
            ImageBrush DelURlImage = new ImageBrush();
            DelURlImage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_remove_1927216.ico"));
            Button DeleteURL = new Button();
            DeleteURL.Uid = "DeleteURLCateguryBtn";
            DeleteURL.Click +=DeleteURLCilicked ;
            DeleteURL.Background = DelURlImage;
            DeleteURL.BorderThickness = new Thickness(0);
            DeleteURL.Template = MaxBtn.Template;
            DeleteURL.Cursor = Cursors.Hand;
            DeleteURL.Height = 50;
            DeleteURL.Width = 50;
            DeleteURL.IsEnabled = false;
            DeleteURL.Margin = new Thickness(30, 10, 0, 0);
            DeleteURL.VerticalAlignment = VerticalAlignment.Top;
            DeleteURL.HorizontalAlignment = HorizontalAlignment.Left;
            DeleteURL.MinHeight = 25;
            DeleteURL.MinWidth = 60;
            URLsControlBtnsStP.Children.Add(DeleteURL);
            URLsControlWrapPanel.Children.Add(URLsControlBtnsStP);
            CatAndURLControlStP.Children.Add(CateWrapPanel);
            CatAndURLControlStP.Children.Add(URLsControlWrapPanel);
            URLCateStP.Children.Add(CatAndURLControlStP);
            MainTemp.Children.Add(URLCateStP);
            TextBlock BlockURLTB = new TextBlock();
            BlockURLTB.Text = "Block WebSite";
            BlockURLTB.FontSize = 24;
            BlockURLTB.Foreground = new SolidColorBrush(Colors.Gray);
            BlockURLTB.VerticalAlignment = VerticalAlignment.Top;
            BlockURLTB.HorizontalAlignment = HorizontalAlignment.Left;
            BlockURLTB.Margin = new Thickness(30, 50, 0, 0);
            MainTemp.Children.Add(BlockURLTB);
            MoreUrls.Add(BlockURLTB);
            if (MainWindow.DS.Tables.Contains("BlockUrls") == true)
            {
                MainWindow.DS.Tables.Remove("BlockUrls");
            }
            MainWindow.DataBaseAgent.SelectData("BlockUrls", ref MainWindow.DS, "*", "BlockUrls", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
            double[] ColumnWidth = { 300, 200, 100 };
            string[] HeadersName = { "Domain", "Category", "Action" };
            string[] ColumnsName = { "URL", "Cat", "Act" };
            BlockURLList = new ColorList();
            Size ItemSize = new Size(0, 25);
            Size ListSize = new Size(600, 0);
            Border border = BlockURLList.Draw(ColumnWidth, 30, HeadersName, null, ListSize, ItemSize, Color.FromArgb(255, 238, 241, 245), Colors.Transparent,
                Colors.White, Colors.Black, Color.FromArgb(255, 42, 180, 192), Colors.LightGray, Colors.Transparent, MainWindow.DS.Tables["BlockUrls"],
                new Thickness(50, 50, 0, 0), new Thickness(0), 12, 16, ColumnsName, BlockURLView_SelectionChanged, "BlockUrlList",
               300);
            border.HorizontalAlignment = HorizontalAlignment.Left;
            border.VerticalAlignment = VerticalAlignment.Top;
            StackPanel BlockURLStP = new StackPanel();
            BlockURLStP.Orientation = Orientation.Horizontal;
            BlockURLStP.Children.Add(border);
            BlockURLStP.Uid = "BlockURLStP";
            WrapPanel BlockingURLWrapPanel = new WrapPanel();
            BlockingURLWrapPanel.Orientation = Orientation.Vertical;
            BlockingURLWrapPanel.Margin = new Thickness(30, 0, 0, 0);
            TextBlock URLsBlockControlTB = new TextBlock();
            URLsBlockControlTB.Text = "Block URLs :";
            URLsBlockControlTB.Margin = new Thickness(0, 0, 0, 0);
            URLsBlockControlTB.FontSize = 18;
            URLsBlockControlTB.Foreground = new SolidColorBrush(Colors.Blue);
            URLsBlockControlTB.VerticalAlignment = VerticalAlignment.Top;
            URLsBlockControlTB.HorizontalAlignment = HorizontalAlignment.Left;
            BlockingURLWrapPanel.Children.Add(URLsBlockControlTB);
            StackPanel LabelsStP = new StackPanel();
            LabelsStP.Orientation = Orientation.Horizontal;
            WrapPanel Left = new WrapPanel();
            Left.Orientation = Orientation.Vertical;
            TextBlock URLsBlockNameTB = new TextBlock();
            URLsBlockNameTB.Text = "Block URL Address :";
            URLsBlockNameTB.Margin = new Thickness(0, 25, 0, 0);
            URLsBlockNameTB.FontSize = 14;
            URLsBlockNameTB.VerticalAlignment = VerticalAlignment.Top;
            URLsBlockNameTB.HorizontalAlignment = HorizontalAlignment.Left;
            Left.Children.Add(URLsBlockNameTB);
            TextBox URLBlockNameTxB = new TextBox();
            URLBlockNameTxB.Margin = new Thickness(0, 10, 0, 0);
            URLBlockNameTxB.Uid = "URLsBlockNameTxB";
            URLBlockNameTxB.MinWidth = 300;
            URLBlockNameTxB.FontSize = 14;
            URLBlockNameTxB.VerticalAlignment = VerticalAlignment.Top;
            URLBlockNameTxB.HorizontalAlignment = HorizontalAlignment.Left;
            Left.Children.Add(URLBlockNameTxB);
            TextBlock URLsBlockNameErrorTB = new TextBlock();
            URLsBlockNameErrorTB.Text = "";
            URLsBlockNameErrorTB.Uid = "URLsBlockNameErrorTB";
            URLsBlockNameErrorTB.Margin = new Thickness(0, 0, 0, 0);
            URLsBlockNameErrorTB.FontSize = 12;
            URLsBlockNameErrorTB.Foreground = new SolidColorBrush(Colors.Red);
            URLsBlockNameErrorTB.VerticalAlignment = VerticalAlignment.Top;
            URLsBlockNameErrorTB.HorizontalAlignment = HorizontalAlignment.Left;
            Left.Children.Add(URLsBlockNameErrorTB);
            LabelsStP.Children.Add(Left);
            WrapPanel Right = new WrapPanel();
            Right.Orientation = Orientation.Vertical;
            TextBlock URLsBlockComboTB = new TextBlock();
            URLsBlockComboTB.Text = "Block URL Category :";
            URLsBlockComboTB.Margin = new Thickness(20, 25, 0, 0);
            URLsBlockComboTB.FontSize = 14;
            URLsBlockComboTB.VerticalAlignment = VerticalAlignment.Top;
            URLsBlockComboTB.HorizontalAlignment = HorizontalAlignment.Left;
            Right.Children.Add(URLsBlockComboTB);
            ComboBox Categury = new ComboBox();
            Categury.Uid = "URLCateguryCombo";
            Categury.HorizontalAlignment = HorizontalAlignment.Left;
            Categury.VerticalAlignment = VerticalAlignment.Top;
            Categury.Margin = new Thickness(20, 10, 0, 0);
            Categury.MinWidth = 120;
            Categury.VerticalAlignment = VerticalAlignment.Top;
            Categury.HorizontalAlignment = HorizontalAlignment.Left;
            Right.Children.Add(Categury);
            if (MainWindow.DS.Tables.Contains("URLCategury") == true)
            {
                MainWindow.DS.Tables.Remove("URLCategury");
            }
            MainWindow.DataBaseAgent.SelectData("URLCategury", ref MainWindow.DS, "*", "URLCategury", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
            foreach(DataRow var in MainWindow.DS.Tables["URLCategury"].Rows)
            {
                Categury.Items.Add(var["Name"]);
            }
            if(Categury.Items.Count >0)
            {
                Categury.SelectedIndex = 0;
            }
            MainWindow.DS.Tables.Remove("URLCategury");
            LabelsStP.Children.Add(Right);
            BlockingURLWrapPanel.Children.Add(LabelsStP);
            StackPanel URLsBlockBtnsStP = new StackPanel();
            URLsBlockBtnsStP.Orientation = Orientation.Horizontal;
            Button AddBlockURL = new Button();
            AddBlockURL.Uid = "AddURLBtn";
            AddBlockURL.Cursor = Cursors.Hand;
            AddBlockURL.Click += URLBAddClick;
            AddBlockURL.BorderThickness = new Thickness(0);
            ImageBrush AddUrlBlockImage = new ImageBrush();
            AddUrlBlockImage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_add_1927206.ico"));
            AddBlockURL.Background = AddUrlBlockImage;
            AddBlockURL.Template = MaxBtn.Template;
            AddBlockURL.Margin = new Thickness(0, 10, 0, 0);
            AddBlockURL.VerticalAlignment = VerticalAlignment.Top;
            AddBlockURL.HorizontalAlignment = HorizontalAlignment.Left;
            AddBlockURL.Height = 50;
            AddBlockURL.Width = 60;
            URLsBlockBtnsStP.Children.Add(AddBlockURL);
            ImageBrush DelURlBlockImage = new ImageBrush();
            DelURlBlockImage.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_remove_1927216.ico"));
            Button DeleteURLBlock = new Button();
            DeleteURLBlock.Uid = "DeleteURLBlockBtn";
            DeleteURLBlock.Click += DeleteBlockURL;
            DeleteURLBlock.Background = DelURlBlockImage;
            DeleteURLBlock.BorderThickness = new Thickness(0);
            DeleteURLBlock.Template = MaxBtn.Template;
            DeleteURLBlock.Cursor = Cursors.Hand;
            DeleteURLBlock.Height = 50;
            DeleteURLBlock.Width = 50;
            DeleteURLBlock.IsEnabled = false;
            DeleteURLBlock.Margin = new Thickness(30, 10, 0, 0);
            DeleteURLBlock.VerticalAlignment = VerticalAlignment.Top;
            DeleteURLBlock.HorizontalAlignment = HorizontalAlignment.Left;
            DeleteURLBlock.MinHeight = 25;
            DeleteURLBlock.MinWidth = 60;
            URLsBlockBtnsStP.Children.Add(DeleteURLBlock);
            BlockingURLWrapPanel.Children.Add(URLsBlockBtnsStP);
            BlockURLStP.Children.Add(BlockingURLWrapPanel);
            MainTemp.Children.Add(BlockURLStP);
            MoreUrls.Add(URLCateStP);
            MoreUrls.Add(BlockURLStP);

            MainWindow.DS.Tables.Remove("BlockUrls");
            TextBlock ChartTB = new TextBlock();
            ChartTB.Text = "Chart";
            ChartTB.FontSize = 24;
            ChartTB.Foreground = new SolidColorBrush(Colors.Gray);
            ChartTB.Margin = new Thickness(30, 50, 0, 0);
            ChartTB.VerticalAlignment = VerticalAlignment.Top;
            ChartTB.HorizontalAlignment = HorizontalAlignment.Left;
            MainTemp.Children.Add(ChartTB);
            MoreUrls.Add(ChartTB);
            List<Charts.TriadPairs> URLCategury = new List<Charts.TriadPairs>();
            if (MainWindow.DS.Tables.Contains("URLCategury") == true)
            {
                MainWindow.DS.Tables.Remove("URLCategury");
            }
            MainWindow.DataBaseAgent.SelectData("URLCategury", ref MainWindow.DS, "*", "URLCategury", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
            foreach (DataRow var in MainWindow.DS.Tables["URLCategury"].Rows)
            {
                Charts.TriadPairs Data = new Charts.TriadPairs();
                PropertyInfo ColorInfo = typeof(Colors).GetProperty(var["Color"].ToString());
                Data.Color = (System.Windows.Media.Color)ColorInfo.GetValue(ColorInfo);
                Data.Tag = var["Name"].ToString();
                Data.Value = var["URLs"].ToString().Split(',').Length;
                URLCategury.Add(Data);
            }
            MainWindow.DS.Tables.Remove("URLCategury");
            List<Charts.TriadPairs> UsageData = new List<Charts.TriadPairs>();
            DataSet TargetDs = new DataSet();
            HistoryURLSemaphore.WaitOne();
            MainWindow.DataBaseAgent.SelectDataWithCommand("Select Category ,Count(*) as Number From HistoryURL where ChildID ='" + ChildsCombo.SelectedItem.ToString().ToString() + "' Group by Category", ref TargetDs, "CatHistory");
            foreach (DataRow var in TargetDs.Tables["CatHistory"].Rows)
            {
                if (var["Category"].ToString() == "")
                {
                    continue;
                }
                Charts.TriadPairs Data = new Charts.TriadPairs();
                string Color = MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select Color From URLCategury where Name='" + var["Category"].ToString() + "'").ToString();
                PropertyInfo ColorInfo = typeof(Colors).GetProperty(Color);
                Data.Color = (System.Windows.Media.Color)ColorInfo.GetValue(ColorInfo);
                Data.Tag = var["Category"].ToString();
                Data.Value = (double)var["Number"];
                UsageData.Add(Data);
            }
            TargetDs.Tables.Remove("CatHistory");
            HistoryURLSemaphore.Release();
            if (URLCategury.Count > 0)
            {
                Image InfoImage3 = new System.Windows.Controls.Image();
                InfoImage3.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_info-blog_46810.ico"));
                InfoImage3.Width = 30;
                InfoImage3.Height = 30;
                InfoImage3.VerticalAlignment = VerticalAlignment.Top;
                InfoImage3.HorizontalAlignment = HorizontalAlignment.Left;
                InfoImage3.Margin = new Thickness(30, 30, 0, 0);
                MainTemp.Children.Add(InfoImage3);
                MoreUrls.Add(InfoImage3);
                Grid.SetRow(InfoImage3, 3);
                TextBlock InfoTb3 = new TextBlock();
                InfoTb3.Text = "You can show charts taht Contain ";//$$
                InfoTb3.FontSize = 16;
                InfoTb3.VerticalAlignment = VerticalAlignment.Top;
                InfoTb3.HorizontalAlignment = HorizontalAlignment.Left;
                InfoTb3.Foreground = new SolidColorBrush(Colors.Black);
                InfoTb3.Margin = new Thickness(45, 0, 0, 0);
                MainTemp.Children.Add(InfoTb3);
                MoreUrls.Add(InfoTb3);
                Grid.SetRow(InfoTb3, 3);
                Canvas NumberOfURLinCat = new Canvas();
                NumberOfURLinCat.Width = 200;
                NumberOfURLinCat.Height = 200;
                NumberOfURLinCat.HorizontalAlignment = HorizontalAlignment.Left;
                NumberOfURLinCat.VerticalAlignment = VerticalAlignment.Top;
                NumberOfURLinCat.Margin = new Thickness(30, 70, 0, 0);
                MainTemp.Children.Add(NumberOfURLinCat);
                Grid.SetRow(NumberOfURLinCat, 3);
                Charts CatChart = new Charts();
                CatChart.DrawPieChart(ref NumberOfURLinCat, URLCategury, new Point(100, 100), 50, 100, new SolidColorBrush(Colors.Transparent),
                    0, Colors.SkyBlue, Colors.Silver, new Size(16, 16), 200, "");
                Canvas UsageUrlcahrt = new Canvas();
                UsageUrlcahrt.Width = 200;
                UsageUrlcahrt.Height = 200;
                UsageUrlcahrt.HorizontalAlignment = HorizontalAlignment.Left;
                UsageUrlcahrt.VerticalAlignment = VerticalAlignment.Top;
                UsageUrlcahrt.Margin = new Thickness(30, 300, 0, 0);
                MainTemp.Children.Add(UsageUrlcahrt);
                MoreUrls.Add(UsageUrlcahrt);
                Grid.SetRow(UsageUrlcahrt, 3);
                Charts UsageChart = new Charts();
                CatChart.DrawPieChart(ref NumberOfURLinCat, UsageData, new Point(100, 100), 50, 100, new SolidColorBrush(Colors.Transparent),
                    0, Colors.SkyBlue, Colors.Silver, new Size(16, 16), 200, "");
                MoreUrls.Add(NumberOfURLinCat);
            }
        }

        private void DeleteBlockURL(object sender, RoutedEventArgs e)
        {
            MainDataGrid.Dispatcher.Invoke(() =>
            {
                if(BlockURLList.SelectedIndex > -1)
                {
                    Packet Pack = new Packet();
                    BlockUrl NewBlok = new BlockUrl();
                    MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From BlockUrls where ChildID ='" + ChildsCombo.SelectedItem.ToString() + "' And URL ='" + BlockURLList.GetDataAt(BlockURLList.SelectedIndex)[0] + "'",
                        ref MainWindow.DS, "BlockUrls");
                    NewBlok.URL = BlockURLList.GetDataAt(BlockURLList.SelectedIndex)[0];
                    NewBlok.Act = "Redirect";
                    NewBlok.ID = MainWindow.DS.Tables["BlockUrls"].Rows[0]["ID"].ToString();
                    BlockURLList.RemoveAt(BlockURLList.SelectedIndex);
                    Packet.URL DataPack = new Packet.URL();
                    DataPack.Address = (NewBlok.URL + "$" + "google.com");
                    DataPack.Browser = "";
                    DataPack.Time = DateTime.Now;
                    DataPack.Type = (short)Packet.URLType.Disable;
                    string Data = Pack.ToString(DataPack);
                    Packet.PSProPacket ProURL = new Packet.PSProPacket();
                    ProURL.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                    ProURL.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
                    ProURL.Type = (short)Packet.PacketType.URL;
                    ProURL.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                    string ProData = Pack.ToString(ProURL);
                    Packet.MainPacket MainData = new Packet.MainPacket();
                    MainData.Data = Data;
                    MainData.PPacket = Pack.ToString(ProURL);
                    string MainStrData = Pack.ToString<Packet.MainPacket>(MainData);
                    MainWindow.Connection.SendDataSM.WaitOne();
                    MainWindow.Connection.SendToServer(MainStrData);
                    MainWindow.Connection.SendDataSM.Release();
                }
            });
        }

        private void AddURLCilicked(object sender, RoutedEventArgs e)
        {
            MainDataGrid.Dispatcher.Invoke(() =>
            {
                WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
                StackPanel stackPanel = (StackPanel)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "URLCateStP");
                StackPanel stack = (StackPanel)stackPanel.Children[2];
                WrapPanel wrapPanel = (WrapPanel)stack.Children[1];
                TextBox textBox = (TextBox)wrapPanel.Children.Cast<UIElement>().First(x => x.Uid == "URLsNameTxB");
                TextBlock textBlock = (TextBlock)wrapPanel.Children.Cast<UIElement>().First(x => x.Uid == "URLsNameErrorTB");
                if (URLCategoryList.SelectedIndex > -1)
                {
                    textBlock.Text = "";
                    MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From URLCategury where ChildID = '" + ChildsCombo.SelectedItem.ToString() + "' And Name = '" + URLCategoryList.GetDataAt(URLCategoryList.SelectedIndex)[0] + "'",
                        ref MainWindow.DS, "URlsInCat");
                    if (MainWindow.DS.Tables["URlsInCat"].Rows.Count > 0)
                    {
                        URLsList.Clear();
                        string RawData = MainWindow.DS.Tables["URlsInCat"].Rows[0]["URLs"].ToString();
                        if(RawData.Contains(textBox.Text.ToString()) == false)
                        {
                            RawData += ("," + textBox.Text.ToString());
                            string[] URLs = RawData.Split(',');
                            for (int i = 0; i < URLs.Count(); i++)
                            {
                                if (URLs[i] == "")
                                {
                                    continue;
                                }
                                DataRow Temp = MainWindow.DS.Tables["URlsInCat"].NewRow();
                                Temp["URLs"] = URLs[i];
                                URLsList.AddNewItem(Temp);
                            }
                            MainWindow.DataBaseAgent.ExequteWithCommand("UpDate URLCategury Set URLs = '" + RawData + "' where ChildID = '" + ChildsCombo.SelectedItem.ToString() + "' And Name = '" + URLCategoryList.GetDataAt(URLCategoryList.SelectedIndex)[0] + "'");
                        }
                    }
                    MainWindow.DS.Tables.Remove("URlsInCat");
                }
                else
                {
                    
                    textBlock.Text = "Select a Category.";
                }
                
                
            });
        }

        private void DeleteURLCilicked(object sender, RoutedEventArgs e)
        {
            MainDataGrid.Dispatcher.Invoke(() =>
            {
                if (URLsList.SelectedIndex > -1)
                {
                    MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From URLCategury where ChildID = '" + ChildsCombo.SelectedItem.ToString() + "' And Name = '" + URLCategoryList.GetDataAt(URLCategoryList.SelectedIndex)[0] + "'",
                        ref MainWindow.DS, "URlsInCat");
                    if (MainWindow.DS.Tables["URlsInCat"].Rows.Count > 0)
                    {
                        
                        string RawData = MainWindow.DS.Tables["URlsInCat"].Rows[0]["URLs"].ToString();
                        RawData = RawData.Replace("," + URLsList.GetDataAt(URLsList.SelectedIndex)[0] , "");
                        MainWindow.DataBaseAgent.ExequteWithCommand("UpDate URLCategury Set URLs = '" + RawData + "' where ChildID = '" + ChildsCombo.SelectedItem.ToString() + "' And Name = '" + URLCategoryList.GetDataAt(URLCategoryList.SelectedIndex)[0] + "'");
                    }
                    MainWindow.DS.Tables.Remove("URlsInCat");
                    URLsList.RemoveAt(URLsList.SelectedIndex);
                }
            });
        }

        private void DeletCatURLClicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddURLInCat(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void URLsInCatView_SelectionChanged(object obj)
        {
            MainDataGrid.Dispatcher.Invoke(() =>
            {
                WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
                StackPanel stackPanel = (StackPanel)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "URLCateStP");
                StackPanel stack = (StackPanel)stackPanel.Children[2];
                WrapPanel wrapPanel = (WrapPanel)stack.Children[1];
                stack = (StackPanel)wrapPanel.Children.Cast<UIElement>().First(x => x.Uid == "URLsControlBtnsStP");
                Button button = (Button)stack.Children[1];
                if (URLsList.SelectedIndex > -1)
                {
                    
                    button.IsEnabled = true;
                }
                else
                {
                    button.IsEnabled = false;
                }
                
            });
        }

        private void AddCatBtn_Click(object sender, RoutedEventArgs e)
        {
            EnterURLCat NEW = new EnterURLCat();
            NEW.Show();
            this.Hide();
        }

        private void CatURLView_SelectionChanged(object sender)
        {
            
            MainDataGrid.Dispatcher.Invoke(() =>
            {
                URLsList.Clear();
                MainWindow.DataBaseAgent.SelectDataWithCommand("Select URLs From URLCategury where ChildID = '" +
                   ChildsCombo.SelectedItem.ToString().ToString() + "' And Name = '" + URLCategoryList.GetDataAt(URLCategoryList.SelectedIndex)[0] + "'",
                   ref MainWindow.DS, "URLsInCat");
                if(MainWindow.DS.Tables["URLsInCat"].Rows.Count > 0)
                {
                    string RawData = MainWindow.DS.Tables["URLsInCat"].Rows[0][0].ToString();

                    string[] URLs = RawData.Split(',');
                    for (int i = 0; i < URLs.Count(); i++)
                    {
                        if (URLs[i] == "")
                        {
                            continue;
                        }
                        DataRow Temp = MainWindow.DS.Tables["URLsInCat"].NewRow();
                        Temp[0] = URLs[i];
                        URLsList.AddNewItem(Temp);
                    }
                    
                    
                }
                WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
                StackPanel stackPanel = (StackPanel)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "URLCateStP");
                StackPanel stack = (StackPanel)stackPanel.Children[2];
                WrapPanel wrapPanel = (WrapPanel)stack.Children[0];
                StackPanel panel = (StackPanel)wrapPanel.Children[4];
                Button Btn = (Button)(panel.Children.Cast<UIElement>().First(x => x.Uid == "DeleteURLCateguryBtn"));

                if (URLCategoryList.SelectedIndex > -1)
                {
                    Btn.IsEnabled = true;
                }
                else
                {
                    Btn.IsEnabled = false;
                }
                MainWindow.DS.Tables.Remove("URLsInCat");
            });
            
        }

        private void URLBLOCkCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeleteURLCateguryCilicked(object sender, RoutedEventArgs e)
        {
            MainDataGrid.Dispatcher.Invoke(() =>
            {
                MainWindow.DataBaseAgent.ExequteWithCommand("Delete From URLCategury where ChildID = '" + ChildsCombo.SelectedItem.ToString().ToString() + "' And Name = N'" + URLCategoryList.GetDataAt(URLCategoryList.SelectedIndex)[0] + "'");
                WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
                StackPanel stackPanel = (StackPanel)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "BlockURLStP");
                MainTemp = (WrapPanel)stackPanel.Children[1];
                stackPanel = (StackPanel)MainTemp.Children[1];
                MainTemp = (WrapPanel)stackPanel.Children[1];
                ComboBox Target = (ComboBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "URLCateguryCombo");
                URLsList.Clear();
                Target.Items.RemoveAt(Target.Items.IndexOf(URLCategoryList.GetDataAt(URLCategoryList.SelectedIndex)[0]));
                URLCategoryList.RemoveAt(URLCategoryList.SelectedIndex);
            });
            

        }

        private void Dialog_Closed(object sender, EventArgs e)
        {
            if (SelectedURLCat != null)
            {
                DataSet TaregtDs = new DataSet();
                MainWindow.DataBaseAgent.SelectData("URLCategury", ref TaregtDs, "*", "URLCategury", SelectedURLCat, "Name");
                MainWindow.DataBaseAgent.ExequteWithCommand("Delete From UsedColor where Name ='" + TaregtDs.Tables[0].Rows[0]["Color"].ToString() + "'");
                TaregtDs.Tables[0].Rows.RemoveAt(0);
                MainWindow.DataBaseAgent.UpdateData(TaregtDs.Tables[0]);
            }
        }

        private void BlockURLView_SelectionChanged(object sender)
        {
            MainDataGrid.Dispatcher.Invoke(() =>
            {
                WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
                StackPanel stackPanel = (StackPanel)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "BlockURLStP");
                MainTemp = (WrapPanel)stackPanel.Children[1];
                stackPanel = (StackPanel)MainTemp.Children[2];
                Button Remove = (Button)stackPanel.Children[1];
                if (BlockURLList.SelectedIndex > -1)
                {
                    Remove.IsEnabled = true;
                }
                else
                {
                    Remove.IsEnabled = false;
                }
            });
            
        }

        private void MoreLimitExpander_Expanded(object sender, RoutedEventArgs e)
        {
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            LimitExpand = new List<object>();
            Border ListTarget = (Border)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "ShowLimitList");
            Expander Target = sender as Expander;
            ImageBrush RemoveImage = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/x-mark-4-64.ico")));
            Button RemoveLimitBtn = new Button();
            RemoveLimitBtn.Click += RemoveLimitBtnClick;
            RemoveLimitBtn.Background = RemoveImage;
            RemoveLimitBtn.Template = MaxBtn.Template;
            RemoveLimitBtn.BorderThickness = new Thickness(0);
            RemoveLimitBtn.Margin = new Thickness(Target.Margin.Left + 20, Target.Margin.Top +20+ Target.ActualHeight, 10, 0);
            RemoveLimitBtn.Height = 40;
            RemoveLimitBtn.Width = 40;
            RemoveLimitBtn.Cursor = Cursors.Hand;
            RemoveLimitBtn.Uid = "LimitRemoveBtn";
            RemoveLimitBtn.Loaded += RemoveLimitBtn_Loaded;
            RemoveLimitBtn.VerticalAlignment = VerticalAlignment.Top;
            RemoveLimitBtn.Click += RemoveLimitBtn_Click;
            RemoveLimitBtn.HorizontalAlignment = HorizontalAlignment.Left;
            if(LimitList.SelectedIndex == -1)
            {
                RemoveLimitBtn.IsEnabled = false;
            }
            MainTemp.Children.Add(RemoveLimitBtn);
            LimitExpand.Add(RemoveLimitBtn);
            TextBlock ActionResult = new TextBlock();
            ActionResult.HorizontalAlignment = HorizontalAlignment.Left;
            ActionResult.VerticalAlignment = VerticalAlignment.Top;
            ActionResult.Text = "";
            ActionResult.Foreground = new SolidColorBrush(Colors.Green);
            ActionResult.Uid = "LimitActionResult";
            ActionResult.Margin = new Thickness(RemoveLimitBtn.Margin.Left + 50, RemoveLimitBtn.Margin.Top + 30 + RemoveLimitBtn.MinHeight, 0, 20);
            MainTemp.Children.Add(ActionResult);
            LimitExpand.Add(ActionResult);
            ImageBrush AddImage = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/add-64.ico")));
            Button AddLimitBtn = new Button();
            AddLimitBtn.Uid = "LimitAddBtn";
            AddLimitBtn.Height = 40;
            AddLimitBtn.Width = 40;
            AddLimitBtn.Template = MaxBtn.Template;
            AddLimitBtn.Cursor = Cursors.Hand;
            AddLimitBtn.BorderThickness = new Thickness(0);
            AddLimitBtn.Background = AddImage;
            AddLimitBtn.Click += AddLimitaionBtnClick;
            AddLimitBtn.Margin = new Thickness(RemoveLimitBtn.Width + 30 + RemoveLimitBtn.Margin.Left, RemoveLimitBtn.Margin.Top, 0, 20);
            
            AddLimitBtn.VerticalAlignment = VerticalAlignment.Top;
            AddLimitBtn.HorizontalAlignment = HorizontalAlignment.Left;
            MainTemp.Children.Add(AddLimitBtn);
            LimitExpand.Add(AddLimitBtn);
            ImageBrush EditImage = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/edit-8-64.ico")));
            Button EditLimitBtn = new Button();
            EditLimitBtn.Uid = "LimitEditBtn";
            EditLimitBtn.Click += EditLimitaionBtnClick;
            EditLimitBtn.Margin = new Thickness(AddLimitBtn.MinWidth + 70 + AddLimitBtn.Margin.Left, AddLimitBtn.Margin.Top, 0, 0);
            EditLimitBtn.Width = 40;
            EditLimitBtn.Height = 40;
            EditLimitBtn.Template = MaxBtn.Template;
            EditLimitBtn.BorderThickness = new Thickness(0);
            EditLimitBtn.Background = EditImage;
            EditLimitBtn.VerticalAlignment = VerticalAlignment.Top;
            EditLimitBtn.HorizontalAlignment = HorizontalAlignment.Left;
        }

        private void RemoveLimitBtn_Click(object sender, RoutedEventArgs e)
        {
            WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
            ListView TargetList = (ListView)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "ShowLimitList");
            ComboBox TypeCombo = (ComboBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "LimitTypeCombo");
            DataSet TempDS = new DataSet();
            switch (TypeCombo.SelectedIndex)
            {
                case 0:
                    {
                        SystemLimit NewLimit = new SystemLimit();
                        try
                        {
                            MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From SystemLimit where ChildID ='" + ChildsCombo.SelectedItem.ToString().ToString() + "' AND ID ='" + ((SystemLimit) TargetList.SelectedItems).ID + "'", ref TempDS, "SystemLimit");
                            if (MainWindow.DS.Tables["SystemLimit"].Rows.Count != 0)
                            {
                                
                                DataRow NewRow = MainWindow.DS.Tables["SystemLimit"].Rows[0];
                                
                                Packet PC = new Packet();
                                Packet.SystemLimition3Time SYSData = new Packet.SystemLimition3Time();
                                SYSData.Start = (TimeSpan)NewRow["StartTime"];
                                SYSData.End = (TimeSpan)NewRow["EndTime"];
                                SYSData.Duration = NewLimit.Duration;
                                SYSData.Act = (short)Packet.SystemLimitAct.Disable;
                                SYSData.Id = NewRow["ID"].ToString();
                                Packet.PSProPacket ProSys = new Packet.PSProPacket();
                                string Data = PC.ToString(SYSData);
                                ProSys.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                                ProSys.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
                                ProSys.Type = (short)Packet.PacketType.SystemLimition3Time;
                                ProSys.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                                string ProData = PC.ToString(ProSys);
                                Packet.MainPacket MainData = new Packet.MainPacket();
                                MainData.Data = Data;
                                MainData.PPacket = PC.ToString(ProSys);
                                string MainStrData = PC.ToString<Packet.MainPacket>(MainData);
                                MainWindow.Connection.SendDataSM.WaitOne();
                                MainWindow.Connection.SendToServer(MainStrData);
                                MainWindow.Connection.SendDataSM.Release();
                                MainWindow.DataBaseAgent.ExequteWithCommand("Delete From SystemLimit where ChildID ='" + ChildsCombo.SelectedItem.ToString().ToString() + "' AND ID ='" + ((SystemLimit)TargetList.SelectedItems).ID + "'");
                                TargetList.Items.RemoveAt(TargetList.SelectedIndex);
                            }
                            


                        }
                        catch (Exception E)
                        {

                        }
                        LimitList.AddNewItem(NewLimit);
                    }; break;
                case 1:
                    {
                        AppsLimit NewLimit = new AppsLimit();
                        try
                        {
                            MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From AppsLimit where ChildID ='" + ChildsCombo.SelectedItem.ToString().ToString() + "' AND ID ='" + ((SystemLimit)TargetList.SelectedItems).ID + "'", ref TempDS, "SystemLimit");
                            if (MainWindow.DS.Tables["AppsLimit"].Rows.Count != 0)
                            {

                                DataRow NewRow = MainWindow.DS.Tables["AppsLimit"].Rows[0];
                                Packet PC = new Packet();
                                Packet.AppLimition3Time SYSData = new Packet.AppLimition3Time();
                                SYSData.Start = (TimeSpan)NewRow["StartTime"];
                                SYSData.End = (TimeSpan)NewRow["EndTime"];
                                SYSData.Act = (short)Packet.AppLimitAct.Disable;
                                SYSData.AppName = NewRow["ID"].ToString();
                                Packet.PSProPacket ProSys = new Packet.PSProPacket();
                                string Data = PC.ToString(SYSData);
                                ProSys.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                                ProSys.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
                                ProSys.Type = (short)Packet.PacketType.AppLimition3Time;
                                ProSys.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                                string ProData = PC.ToString(ProSys);
                                Packet.MainPacket MainData = new Packet.MainPacket();
                                MainData.Data = Data;
                                MainData.PPacket = PC.ToString(ProSys);
                                string MainStrData = PC.ToString<Packet.MainPacket>(MainData);
                                MainWindow.Connection.SendDataSM.WaitOne();
                                MainWindow.Connection.SendToServer(MainStrData);
                                MainWindow.Connection.SendDataSM.Release();
                                MainWindow.DataBaseAgent.ExequteWithCommand("Delete From AppsLimit where ChildID ='" + ChildsCombo.SelectedItem.ToString().ToString() + "' AND ID ='" + ((SystemLimit)TargetList.SelectedItems).ID + "'");
                                TargetList.Items.RemoveAt(TargetList.SelectedIndex);
                            }

                        }
                        catch (Exception E)
                        {

                        }
                        LimitList.AddNewItem(NewLimit);
                    }
                    break;
                case 2:
                    {
                        NetworkLimit NewLimit = new NetworkLimit();
                        try
                        {
                            MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From NetworkLimit where ChildID ='" + ChildsCombo.SelectedItem.ToString().ToString() + "' AND ID ='" + ((SystemLimit)TargetList.SelectedItems).ID + "'", ref TempDS, "NetworkLimit");
                            if (MainWindow.DS.Tables["NetworkLimit"].Rows.Count != 0)
                            {

                                DataRow NewRow = MainWindow.DS.Tables["NetworkLimit"].Rows[0];

                                Packet PC = new Packet();
                                Packet.NetworkCommands SYSData = new Packet.NetworkCommands();
                                SYSData.Command = NewRow["ID"].ToString();
                                SYSData.Command += ("$" + NewRow["StartTime"].ToString()+"$");
                                SYSData.Command += (TimeSpan)NewRow["EndTime"];
                                SYSData.Type = (short)Packet.NetworkCommandsType.DisableWithTime;
                                Packet.PSProPacket ProSys = new Packet.PSProPacket();
                                string Data = PC.ToString(SYSData);
                                ProSys.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                                ProSys.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
                                ProSys.Type = (short)Packet.PacketType.NetworkCommands;
                                ProSys.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                                string ProData = PC.ToString(ProSys);
                                Packet.MainPacket MainData = new Packet.MainPacket();
                                MainData.Data = Data;
                                MainData.PPacket = PC.ToString(ProSys);
                                string MainStrData = PC.ToString<Packet.MainPacket>(MainData);
                                MainWindow.Connection.SendDataSM.WaitOne();
                                MainWindow.Connection.SendToServer(MainStrData);
                                MainWindow.Connection.SendDataSM.Release();
                                MainWindow.DataBaseAgent.ExequteWithCommand("Delete From NetworkLimit where ChildID ='" + ChildsCombo.SelectedItem.ToString().ToString() + "' AND ID ='" + ((SystemLimit)TargetList.SelectedItems).ID + "'");
                                TargetList.Items.RemoveAt(TargetList.SelectedIndex);
                            }
                        }
                        catch (Exception E)
                        {

                        }
                        LimitList.AddNewItem(NewLimit);
                    }; break;
            }
            
        }

        private void EditLimitaionBtnClick(object sender, RoutedEventArgs e)
        {
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            ListView ListTarget = (ListView)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "ShowLimitList");
            Button Target = sender as Button;
            try
            {
                foreach (object var in MoreLimitExpand)
                {
                    MainTemp.Children.Remove((UIElement)var);
                }
            }
            catch (Exception E)
            {

            }
            try
            {
                Image InfoImage0 = new System.Windows.Controls.Image();
                InfoImage0.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_info-blog_46810.ico"));
                InfoImage0.Width = 30;
                InfoImage0.Height = 30;
                InfoImage0.VerticalAlignment = VerticalAlignment.Top;
                InfoImage0.HorizontalAlignment = HorizontalAlignment.Left;
                InfoImage0.Margin = new Thickness(Target.Margin.Left + Target.Width + 50, Target.Margin.Top, 0, 0);
                MainTemp.Children.Add(InfoImage0);
                TextBlock InfoTb0 = new TextBlock();
                InfoTb0.Text = "Please Fill \"Start Time\"\n and \"End Time\" in 24h\n and hh:mm:ss format";
                InfoTb0.FontSize = 16;
                InfoTb0.VerticalAlignment = VerticalAlignment.Top;
                InfoTb0.HorizontalAlignment = HorizontalAlignment.Left;
                InfoTb0.Foreground = new SolidColorBrush(Colors.Black);
                InfoTb0.Margin = new Thickness(Target.Margin.Left + Target.Width + 50, Target.Margin.Top + 25, 0, 0);
                MainTemp.Children.Add(InfoTb0);
                MoreLimitExpand.Add(InfoTb0);
                MoreLimitExpand.Add(InfoImage0);
                MoreLimitExpand = new List<object>();
                TextBox StartLimitText = new TextBox();
                TextBox EndLimitText = new TextBox();
                TextBox DurationLimitText = new TextBox();
                TextBox NameLimitText = new TextBox();
                TextBlock StartLimitBlock = new TextBlock();
                TextBlock EndLimitBlock = new TextBlock();
                TextBlock DurationLimitBlock = new TextBlock();
                TextBlock NameLimitBlock = new TextBlock();
                TextBlock StarStartLimitBlock = new TextBlock();
                TextBlock StarEndLimitBlock = new TextBlock();
                TextBlock StarDurationLimitBlock = new TextBlock();
                TextBlock StarNameLimitBlock = new TextBlock();
                StartLimitText.Uid = "StartLimitTime";
                EndLimitText.Uid = "EndLimitTime";
                DurationLimitText.Uid = "DurationLimitTime";
                NameLimitText.Uid = "NameLimitInctance";
                StartLimitBlock.Text = "Limitaion Satrt Time :";
                DurationLimitBlock.Text = "Duration Time :";
                NameLimitBlock.Text = "Name :";
                NameLimitBlock.Margin = new Thickness(10, Target.Margin.Top + 50, 0, 0);
                NameLimitBlock.VerticalAlignment = VerticalAlignment.Top;
                NameLimitBlock.HorizontalAlignment = HorizontalAlignment.Left;
                NameLimitBlock.Foreground = new SolidColorBrush(Colors.Black);
                DurationLimitBlock.Margin = new Thickness(10, Target.Margin.Top + 80, 0, 0);
                DurationLimitBlock.VerticalAlignment = VerticalAlignment.Top;
                DurationLimitBlock.HorizontalAlignment = HorizontalAlignment.Left;
                DurationLimitBlock.Foreground = new SolidColorBrush(Colors.Black);
                StartLimitBlock.Margin = new Thickness(10, Target.Margin.Top + 110, 0, 0);
                StartLimitBlock.VerticalAlignment = VerticalAlignment.Top;
                StartLimitBlock.HorizontalAlignment = HorizontalAlignment.Left;
                StartLimitBlock.Foreground = new SolidColorBrush(Colors.Black);
                EndLimitBlock.Text = "Limitaion End Time :";
                EndLimitBlock.Margin = new Thickness(10, Target.Margin.Top + 140, 0, 0);
                EndLimitBlock.VerticalAlignment = VerticalAlignment.Top;
                EndLimitBlock.HorizontalAlignment = HorizontalAlignment.Left;
                EndLimitBlock.Foreground = new SolidColorBrush(Colors.Black);
                StartLimitText.Margin = new Thickness(125 , Target.Margin.Top + 140, 0, 0);
                StartLimitText.VerticalAlignment = VerticalAlignment.Top;
                StartLimitText.HorizontalAlignment = HorizontalAlignment.Left;
                StartLimitText.Text = ((NetworkLimit)(ListTarget.SelectedItem)).StartTime.ToString();
                EndLimitText.Margin = new Thickness(125, Target.Margin.Top + 110, 0, 0);
                EndLimitText.MinWidth = 100;
                EndLimitText.Text = ((NetworkLimit)(ListTarget.SelectedItem)).EndTime.ToString();
                StartLimitText.MinWidth = 100;
                EndLimitText.VerticalAlignment = VerticalAlignment.Top;
                EndLimitText.HorizontalAlignment = HorizontalAlignment.Left;
                DurationLimitText.Margin = new Thickness(125, Target.Margin.Top + 80, 0, 0);
                DurationLimitText.MinWidth = 100;
                DurationLimitText.Text = ((NetworkLimit)(ListTarget.SelectedItem)).Duration.ToString();
                NameLimitText.MinWidth = 100;
                NameLimitText.Text = ((NetworkLimit)(ListTarget.SelectedItem)).Name.ToString();
                NameLimitText.VerticalAlignment = VerticalAlignment.Top;
                NameLimitText.HorizontalAlignment = HorizontalAlignment.Left;
                NameLimitText.Margin = new Thickness(125, Target.Margin.Top + 50, 0, 0);
                DurationLimitText.HorizontalAlignment = HorizontalAlignment.Left;
                DurationLimitText.VerticalAlignment = VerticalAlignment.Top;
                
                StarStartLimitBlock.VerticalAlignment = VerticalAlignment.Top;
                StarStartLimitBlock.HorizontalAlignment = HorizontalAlignment.Left;
                StarStartLimitBlock.Margin = new Thickness(125 + StartLimitText.MinWidth + 20, Target.Margin.Top + 110, 0, 0);
                StarStartLimitBlock.Text = "*";
                StarStartLimitBlock.FontSize = 70;
                StarStartLimitBlock.Foreground = new SolidColorBrush(Colors.Red);
                MainTemp.Children.Add(StartLimitText);
                MainTemp.Children.Add(StarStartLimitBlock);
                MainTemp.Children.Add(EndLimitText);
                MainTemp.Children.Add(StartLimitBlock);
                MainTemp.Children.Add(EndLimitBlock);
                MainTemp.Children.Add(DurationLimitBlock);
                MainTemp.Children.Add(DurationLimitText);
                MainTemp.Children.Add(NameLimitText);
                MainTemp.Children.Add(NameLimitBlock);
                MoreLimitExpand.Add(StartLimitText);
                MoreLimitExpand.Add(StarStartLimitBlock);
                MoreLimitExpand.Add(EndLimitText);
                MoreLimitExpand.Add(StartLimitBlock);
                MoreLimitExpand.Add(EndLimitBlock);
                MoreLimitExpand.Add(DurationLimitBlock);
                MoreLimitExpand.Add(DurationLimitText);
                MoreLimitExpand.Add(NameLimitText);
                MoreLimitExpand.Add(NameLimitBlock);
                ComboBox ActTypeLimit = new ComboBox();
                TextBlock ActTypeLimitBlock = new TextBlock();
                ActTypeLimit.Margin = new Thickness(135, Target.Margin.Top + 170, 0, 0);
                ActTypeLimit.VerticalAlignment = VerticalAlignment.Top;
                ActTypeLimit.HorizontalAlignment = HorizontalAlignment.Left;
                ActTypeLimit.MinWidth = 80;
                ActTypeLimit.SelectedItem = ((NetworkLimit)(ListTarget.SelectedItem)).Act.ToString();
                ActTypeLimit.Uid = "LImitActCombo";
                MainTemp.Children.Add(ActTypeLimit);
                MoreLimitExpand.Add(ActTypeLimit);
                ActTypeLimitBlock.Text = "Limitaion Action Type :";
                ActTypeLimitBlock.Margin = new Thickness(10, Target.Margin.Top + 170, 0, 0);
                ActTypeLimitBlock.VerticalAlignment = VerticalAlignment.Top;
                ActTypeLimitBlock.Foreground = new SolidColorBrush(Colors.Black);
                ActTypeLimitBlock.HorizontalAlignment = HorizontalAlignment.Left;
                MainTemp.Children.Add(ActTypeLimitBlock);
                MoreLimitExpand.Add(ActTypeLimitBlock);
                
                Button AccempLimitChange = new Button();
                AccempLimitChange.VerticalAlignment = VerticalAlignment.Top;
                AccempLimitChange.HorizontalAlignment = HorizontalAlignment.Left;
                AccempLimitChange.Content = "Accept";
                AccempLimitChange.Margin = new Thickness(20 , Target.Margin.Top + 200 , 0 , 0);
                AccempLimitChange.Click += AccempLimitChange_Click;
                MainTemp.Children.Add(AccempLimitChange);
                MoreLimitExpand.Add(AccempLimitChange);
                
            }
            catch(Exception E)
            {

            }
            
        }

        private void AccempLimitChange_Click(object sender, RoutedEventArgs e)
        {
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            TextBlock Result = (TextBlock)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "LimitActionResult");
            Result.Text = "Accept change Successfully";
            TextBox Start = (TextBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "StartLimitTime");
            TextBox End = (TextBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "EndLimitTime");
            TextBox Duration = (TextBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "DurationLimitTime");
            TextBox Name = (TextBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "NameLimitInctance");
            ComboBox ActCombo = (ComboBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "LimitTypeCombo");
            DateTime Base = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            switch (ActCombo.SelectedIndex)
            {
                case 0:
                    {
                        SystemLimit NewLimit = new SystemLimit();
                        try
                        {
                            NewLimit.Act = ActCombo.SelectedIndex.ToString();
                            NewLimit.StartTime = Convert.ToDateTime(Start.Text) - Base;
                            NewLimit.EndTime = Convert.ToDateTime(End.Text) - Base;
                            NewLimit.Duration = Convert.ToDateTime(Duration.Text) - Base;
                        }
                        catch (Exception E)
                        {

                        }
                        LimitList.AddNewItem(NewLimit);
                    }; break;
                case 1:
                    {
                        AppsLimit NewLimit = new AppsLimit();
                        try
                        {
                            
                            NewLimit.AppName = Name.Text;
                            NewLimit.StartTime = Convert.ToDateTime(Start.Text) - Base;
                            NewLimit.EndTime = Convert.ToDateTime(End.Text) - Base;
                            NewLimit.Duratin = Convert.ToDateTime(Duration.Text) - Base;
                        }
                        catch (Exception E)
                        {

                        }
                        LimitList.AddNewItem(NewLimit);
                    }
                    break;
                case 2:
                    {
                        NetworkLimit NewLimit = new NetworkLimit();
                        try
                        {
                            NewLimit.Name = Name.Text;
                            NewLimit.StartTime = Convert.ToDateTime(Start.Text) - Base;
                            NewLimit.EndTime = Convert.ToDateTime(End.Text) - Base;
                            NewLimit.Duration = Convert.ToDateTime(Duration.Text) - Base;
                        }
                        catch (Exception E)
                        {

                        }
                        LimitList.AddNewItem(NewLimit);
                    }; break;
            }
        }

        private void MoreLimitExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            foreach (object var in LimitExpand)
            {
                MainTemp.Children.Remove((UIElement)var);
            }
            foreach (object var in LimitExpand)
            {
                MainTemp.Children.Remove((UIElement)var);
            }
            if(MoreLimitExpand != null)
            {
                foreach (object var in MoreLimitExpand)
                {
                    MainTemp.Children.Remove((UIElement)var);
                }
            }
            
            
        }

        private void MoreLimitExpander_Loaded(object sender, RoutedEventArgs e)
        {
            Expander Target = sender as Expander;
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            Border ListTarget = (Border)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "ShowLimitList");
            Target.Margin = new Thickness(ListTarget.Margin.Left, ListTarget.Margin.Top + ListTarget.ActualHeight + 20, 0, 20);
        }

        private void LimitTypeBlock_Loaded(object sender, RoutedEventArgs e)
        {
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            ComboBox ComboTarget = (ComboBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "LimitTypeCombo");
            TextBlock Target = sender as TextBlock;
            Target.Margin = new Thickness(ComboTarget.Margin.Left, ComboTarget.Margin.Top - 30, 10, 0);
        }

        private void LimitTypeCombo_Loaded(object sender, RoutedEventArgs e)
        {
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            Border ListTarget = (Border)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "ShowLimitList");
            ComboBox Target = sender as ComboBox;
            Target.Margin = new Thickness(ListTarget.ActualWidth + 50 + ListTarget.Margin.Left, 180, 10, 0);
        }

        private void RemoveLimitBtn_Loaded(object sender, RoutedEventArgs e)
        {
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            Border ListTarget = (Border)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "ShowLimitList");
            Button Target = sender as Button;
        }

        private void InfoImage3_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void NewApp_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Uninstall_Loaded(object sender, RoutedEventArgs e)
        {
            Button Target = sender as Button;
            Target.Template = MaxBtn.Template;
        }

        private void Uninstall_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void Uninstall_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void UninstallNoBtnClicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UninstallYesBtnClicked(object sender, RoutedEventArgs e)
        {
            Grid MainTemp = (Grid)(MainDataGrid.Children[0]);
            Button YesBtn = sender as Button;
            Button NoBtn = (Button)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "UninstallNoBtn");
            YesBtn.IsEnabled = false;
            YesBtn.Visibility = Visibility.Hidden;
            NoBtn.IsEnabled = false;
            NoBtn.Visibility = Visibility.Hidden;
            Ellipse InstuctionEllipse0 = new Ellipse();
            InstuctionEllipse0.Fill = new SolidColorBrush(Colors.Green);
            InstuctionEllipse0.Height = 10;
            InstuctionEllipse0.Width = 10;
            InstuctionEllipse0.HorizontalAlignment = HorizontalAlignment.Left;
            InstuctionEllipse0.VerticalAlignment = VerticalAlignment.Top;
            InstuctionEllipse0.Margin = new Thickness(10, YesBtn.Margin.Top, 0, 0);
            MainTemp.Children.Add(InstuctionEllipse0);
            Grid.SetRow(InstuctionEllipse0, 1);
            TextBlock InstuctionText0 = new TextBlock();
            InstuctionText0.Text = "In the child system, the program that name is \"Windiows Agent\" is visible and removable.\n Please remove it From Add\\Remove programs.";
            InstuctionText0.HorizontalAlignment = HorizontalAlignment.Left;
            InstuctionText0.VerticalAlignment = VerticalAlignment.Top;
            InstuctionText0.Margin = new Thickness(25, YesBtn.Margin.Top -2, 0, 0);
            InstuctionText0.FontSize = 14;
            InstuctionText0.Foreground = new SolidColorBrush(Colors.Black);
            MainTemp.Children.Add(InstuctionText0);
            Grid.SetRow(InstuctionText0, 1);
            Ellipse InstuctionEllipse1 = new Ellipse();
            InstuctionEllipse1.Fill = new SolidColorBrush(Colors.YellowGreen);
            InstuctionEllipse1.Height = 10;
            InstuctionEllipse1.Width = 10;
            InstuctionEllipse1.HorizontalAlignment = HorizontalAlignment.Left;
            InstuctionEllipse1.VerticalAlignment = VerticalAlignment.Top;
            InstuctionEllipse1.Margin = new Thickness(10, InstuctionText0.Margin.Top + 50, 0, 0);
            MainTemp.Children.Add(InstuctionEllipse1);
            Grid.SetRow(InstuctionEllipse1, 1);
            TextBlock InstuctionText1 = new TextBlock();
            InstuctionText1.Text = "Thank you for using our application. \n";
            InstuctionText1.HorizontalAlignment = HorizontalAlignment.Left;
            InstuctionText1.VerticalAlignment = VerticalAlignment.Top;
            InstuctionText1.Margin = new Thickness(25, InstuctionText0.Margin.Top + 50 - 2, 0, 0);
            InstuctionText1.FontSize = 14;
            InstuctionText1.Foreground = new SolidColorBrush(Colors.Black);
            MainTemp.Children.Add(InstuctionText1);
            Grid.SetRow(InstuctionText1, 1);
            Packet Pack = new Packet();
            Packet.UnInstall Un = new Packet.UnInstall();
            Un.ParentID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
            string Data = Pack.ToString(Un);
            Packet.PSProPacket ProUn = new Packet.PSProPacket();
            ProUn.ID = Un.ParentID;
            ProUn.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
            ProUn.Type = (short)Packet.PacketType.UnInstall;
            ProUn.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
            string ProData = Pack.ToString(ProUn);
            Packet.MainPacket MainData = new Packet.MainPacket();
            MainData.Data = Data;
            MainData.PPacket = Pack.ToString(ProUn);
            string MainStrData = Pack.ToString<Packet.MainPacket>(MainData);
            MainWindow.Connection.SendDataSM.WaitOne();
            MainWindow.Connection.SendToServer(MainStrData);
            MainWindow.Connection.SendDataSM.Release();
        }

        private void CatListSelectCahnge(object sender, SelectionChangedEventArgs e)
        {
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            ListBox Target = (ListBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "CategoryList");
            ListBox Des = (ListBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "AppsList");
            Button Remove = (Button)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "DeleteInstaledAppCatBtn");
            Button AddApp = (Button)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "NewAppInCatBtn");
            Button RemoveApp = (Button)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "DeleteInstalledAppinCatBtn");
            RemoveApp.IsEnabled = false;
            Des.Items.Clear();
            if(Target.SelectedIndex > -1)
            {
                MainWindow.DataBaseAgent.SelectData("AppCategory", ref MainWindow.DS, "*", "AppCategory", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
                foreach (string var in MainWindow.DS.Tables["AppCategory"].Rows[Target.SelectedIndex]["AppsName"].ToString().Split(','))
                {
                    if (var != "")
                    {
                        Des.Items.Add(var);
                    }

                }
                MainWindow.DS.Tables.Remove("AppCategory");
                if (Target.SelectedIndex > -1)
                {
                    Remove.IsEnabled = true;
                    AddApp.IsEnabled = true;
                    RemoveApp.IsEnabled = true;
                }
                else
                {
                    Remove.IsEnabled = false;
                    AddApp.IsEnabled = false;
                    RemoveApp.IsEnabled = false;
                }
            }
            
        }

        private void NewAppClicked(object sender, RoutedEventArgs e)
        {
            AddApp = new EnterAppInCat(ChildUser);
            
            this.Hide();
            AddApp.ShowDialog();
            AddAppName = SelectedApp;
            WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
            if (AddAppName != null)
            {
                MainWindow.DataBaseAgent.SelectData("AppCategory", ref MainWindow.DS, "*", "AppCategory", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
                if(MainWindow.DS.Tables["AppCategory"].Rows.Count > 0)
                {
                    if (MainWindow.DS.Tables["AppCategory"].Rows[CategoryAppList.SelectedIndex]["AppsName"].ToString() == "")
                    {
                        MainWindow.DS.Tables["AppCategory"].Rows[CategoryAppList.SelectedIndex]["AppsName"] = AddAppName;
                    }
                    else
                    {
                        MainWindow.DS.Tables["AppCategory"].Rows[CategoryAppList.SelectedIndex]["AppsName"] += ("," + AddAppName);

                    }
                }
                else
                {

                }
                MainWindow.DataBaseAgent.UpdateData(MainWindow.DS.Tables["AppCategory"]);
                MainWindow.DS.Tables.Remove("AppCategory");
                CategoryAppAppList.AddNewItem(AddAppName);
            }
           
        }

        private void NewCatClicked(object sender, RoutedEventArgs e)
        {
            AddCat = new EnterAppCat();
            this.Hide();
            AddCat.ShowDialog();
            if (NewCat != "")
            {
                MainWindow.DataBaseAgent.SelectData("AppCategory", ref MainWindow.DS, "*", "AppCategory", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
                System.Data.DataRow NewRow = MainWindow.DS.Tables["AppCategory"].NewRow();
                NewRow["Name"] = NewCat;
                NewRow["ChildID"] = ChildsCombo.SelectedItem.ToString().ToString();
                NewRow["Color"] = SelectColor();
                MainWindow.DS.Tables["AppCategory"].Rows.Add(NewRow);
                MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["AppCategory"]);
                CategoryAppList.AddNewItem(NewRow);
                MainWindow.DS.Tables.Remove("AppCategory");
            }

        }

        private void DisEnNetBtnClick(object sender, RoutedEventArgs e)
        {
            WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
            Packet Pack = new Packet();
            Packet.NetworkCommands NetCommand = new Packet.NetworkCommands();
            NetCommand.Type = (short)Packet.NetworkCommandsType.Disable;
            NetCommand.Command = (NetworkList.GetDataAt(NetworkList.SelectedIndex)[0]);
            NetCommand.Time = DateTime.Now.ToString();
            string Data = Pack.ToString(NetCommand);
            Packet.PSProPacket ProNet = new Packet.PSProPacket();
            ProNet.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
            ProNet.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
            ProNet.Type = (short)Packet.PacketType.NetworkCommands;
            ProNet.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
            string ProStr = Pack.ToString(ProNet);
            Packet.MainPacket MainData = new Packet.MainPacket();
            MainData.Data = Data;
            MainData.PPacket = Pack.ToString(ProNet);
            string MainStrData = Pack.ToString<Packet.MainPacket>(MainData);
            MainWindow.Connection.SendDataSM.WaitOne();
            MainWindow.Connection.SendToServer(MainStrData);
            MainWindow.Connection.SendDataSM.Release();
            NetworkAdaptorSemaphor.WaitOne();
            MainWindow.DataBaseAgent.SelectData("NetworkAdaptor", ref MainWindow.DS, "*", "NetworkAdaptor", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
            MainWindow.DS.Tables["NetworkAdaptor"].Rows[NetworkList.SelectedIndex]["Status"] = "Disable";
            NetworkList.Clear();
            for(int i = 0; i < MainWindow.DS.Tables["NetworkAdaptor"].Rows.Count; i++)
            {
                NetworkList.AddNewItem(MainWindow.DS.Tables["NetworkAdaptor"].Rows[i]);
            }
            MainWindow.DataBaseAgent.UpdateData(MainWindow.DS.Tables["NetworkAdaptor"]);
            MainWindow.DS.Tables.Remove("NetworkAdaptor");
            NetworkAdaptorSemaphor.Release();
            Button Target = sender as Button;
            Target.IsEnabled = false;

        }

        private void TackeScreenShot(object sender, RoutedEventArgs e)
        {
            
            Packet Pack = new Packet();
            Packet.ScrrenShot SCPack = new Packet.ScrrenShot();
            SCPack.Type = (short)Packet.ScrrenShotType.OneTime;
            SCPack.End = DateTime.Now;
            SCPack.Start = SCPack.End;
            string Data = Pack.ToString(SCPack);
            Packet.PSProPacket ProSC = new Packet.PSProPacket();
            ProSC.Type = (short)Packet.PacketType.ScrrenShot;
            ProSC.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
            ProSC.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
            ProSC.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
            Packet.MainPacket MainData = new Packet.MainPacket();
            MainData.Data = Data;
            MainData.PPacket = Pack.ToString(ProSC);
            string MainStrData = Pack.ToString<Packet.MainPacket>(MainData);
            MainWindow.Connection.SendDataSM.WaitOne();
            MainWindow.Connection.SendToServer(MainStrData);
            MainWindow.Connection.SendDataSM.Release();
        }

        private void NetAdaptorReloadElipced(object sender, ElapsedEventArgs e)
        {
            NetAdaptor.Stop();
            ReloadNetAdaptor();
        }

        private void ReloadTimerElipced(object sender, ElapsedEventArgs e)
        {
            
            System.Timers.Timer Target = sender as System.Timers.Timer;
            Target.Stop();
            Target.Interval = 1000*5 ;
            
            this.Dispatcher.Invoke(() =>
            {
                WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
                ListView View = (ListView)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "RunningAppList");
                View.Items.Clear();
                RunningAppsSemaphore.WaitOne();
                MainWindow.DataBaseAgent.SelectData("RunningApps", ref MainWindow.DS, "*", "RunningApps", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
                foreach (System.Data.DataRow Row in MainWindow.DS.Tables["RunningApps"].Rows)
                {
                    View.Items.Add(new RunningApp { Name = Row["Name"].ToString().Split('-')[0], StartTime = Row["StartTime"].ToString() });
                }
                Target.Start();
                MainWindow.DS.Tables.Remove("RunningApps");
                RunningAppsSemaphore.Release();
            });
        }

        public void ReloadNetAdaptor()
        {
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            MainTemp.Dispatcher.Invoke(() =>
            {
                ListView Target = (ListView)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "NetworkAdaptorList");
                Target.Items.Clear();
                NetworkAdaptorSemaphor.WaitOne();
                MainWindow.DataBaseAgent.SelectData("NetworkAdaptor", ref MainWindow.DS, "*", "NetworkAdaptor", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
                foreach (System.Data.DataRow Row in MainWindow.DS.Tables["NetworkAdaptor"].Rows)
                {
                    Target.Items.Add(new NetworkAdaptor
                    {
                        DeviceName = Row["DeviceName"].ToString(),
                        InterfaceName = Row["InterfaceName"].ToString(),
                        Status = Row["Status"].ToString()
                    });
                }
                MainWindow.DS.Tables.Remove("NetworkAdaptor");
                NetworkAdaptorSemaphor.Release();
            });
        }

        private void AddURLCateguryCilicked(object sender, RoutedEventArgs e)
        {
            MainDataGrid.Dispatcher.Invoke(() =>
            {
                WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
                StackPanel stackPanel = (StackPanel)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "URLCateStP");
                StackPanel stack = (StackPanel)stackPanel.Children[2];
                WrapPanel wrapPanel = (WrapPanel)stack.Children[0];
                
                TextBlock TargetTB = (TextBlock)(wrapPanel.Children.Cast<UIElement>().First(x => x.Uid == "CategoryNameErrorTB"));
                TextBox TargetTxB = (TextBox)(wrapPanel.Children.Cast<UIElement>().First(x => x.Uid == "CategoryNameTxB"));
                int Count = (int)MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select Count(*) From URLCategury where ChildID ='" + ChildsCombo.SelectedItem.ToString().ToString() + "' And Name = N'" + TargetTxB.Text.ToString() + "'");
                if (Count > 0)
                {
                    TargetTB.Text = "Same Category is already exist";

                }
                else
                {
                    if (MainWindow.DS.Tables.Contains("URLCategury") == false)
                    {
                        MainWindow.DataBaseAgent.SelectData("URLCategury", ref MainWindow.DS, "*", "URLCategury", Main.ChildUser, "ChildID");
                    }
                    DataRow NewRow = MainWindow.DS.Tables["URLCategury"].NewRow();
                    NewRow["Name"] = TargetTxB.Text.ToString();
                    NewRow["ID"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                    NewRow["ChildID"] = Main.ChildUser;
                    NewRow["Color"] = Main.SelectColor();
                    URLCategoryList.AddNewItem(NewRow);
                    MainWindow.DS.Tables["URLCategury"].Rows.Add(NewRow);
                    MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["URLCategury"]);
                    TargetTB.Text = "";
                    
                    stackPanel = (StackPanel)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "BlockURLStP");
                    WrapPanel Temp = (WrapPanel)stackPanel.Children[1];
                    stackPanel = (StackPanel)Temp.Children[1];
                    Temp = (WrapPanel)stackPanel.Children[1];
                    ComboBox Target = (ComboBox)Temp.Children.Cast<UIElement>().First(x => x.Uid == "URLCateguryCombo");
                    Target.Items.Add(TargetTxB.Text.ToString());
                    MainWindow.DS.Tables.Remove("URLCategury");
                }
            });
            
        }

        private void StartEndMonitorBtnClicked(object sender, RoutedEventArgs e)
        {
            Button Target = sender as Button;

            
            if(StartReal == false)
            {
                StartReal = true;
                Packet Pack = new Packet();
                Target.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/pause-64.ico")));
                Packet.RealTimeMonitor Real = new Packet.RealTimeMonitor();
                NetWorkTools NetTool = new NetWorkTools();
                string IP = NetTool.GetConnectionInterfaceName();
                Real.IP = IP;
                //Real.IP = "127.0.0.1";
                Real.Port = 8804;
                MainWindow.Connection.RealTimeMonitoring(Real.IP, Real.Port, Monitor);
                Real.DeviceType = false;
                Packet.PSProPacket ProReal = new Packet.PSProPacket();
                ProReal.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                ProReal.Type = (short)Packet.PacketType.RealTimeMonitor;
                string Data = Pack.ToString(Real);
                ProReal.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                ProReal.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
                Thread.Sleep(1000);
                Packet.MainPacket MainData = new Packet.MainPacket();
                MainData.Data = Data;
                MainData.PPacket = Pack.ToString(ProReal);
                string MainStrData = Pack.ToString<Packet.MainPacket>(MainData);
                MainWindow.Connection.SendDataSM.WaitOne();
                MainWindow.Connection.SendToServer(MainStrData);
                MainWindow.Connection.SendDataSM.Release();
                Monitor.Source = BITIM;
            }
            else
            {
                StartReal = false;
                MainWindow.Connection.RealTimeEnd();
                Target.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/video-play-3-64.ico")));
            }
        }

        private void EnNetBtnClick(object sender, RoutedEventArgs e)
        {
            WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
            
            Packet Pack = new Packet();
            Packet.NetworkCommands NetCommand = new Packet.NetworkCommands();
            NetCommand.Type = (short)Packet.NetworkCommandsType.Enable;
            NetCommand.Command = (NetworkList.GetDataAt(NetworkList.SelectedIndex))[0];
            NetCommand.Time = DateTime.Now.ToString();
            string Data = Pack.ToString(NetCommand);
            Packet.PSProPacket ProNet = new Packet.PSProPacket();
            ProNet.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
            ProNet.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
            ProNet.Type = (short)Packet.PacketType.NetworkCommands;
            ProNet.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
            string ProStr = Pack.ToString(ProNet);
            Packet.MainPacket MainData = new Packet.MainPacket();
            MainData.Data = Data;
            MainData.PPacket = Pack.ToString(ProNet);
            string MainStrData = Pack.ToString<Packet.MainPacket>(MainData);
            MainWindow.Connection.SendDataSM.WaitOne();
            MainWindow.Connection.SendToServer(MainStrData);
            MainWindow.Connection.SendDataSM.Release();
            NetworkAdaptorSemaphor.WaitOne();
            MainWindow.DataBaseAgent.SelectData("NetworkAdaptor", ref MainWindow.DS, "*", "NetworkAdaptor", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
            MainWindow.DS.Tables["NetworkAdaptor"].Rows[NetworkList.SelectedIndex]["Status"] = "Enable";
            NetworkList.Clear();
            for (int i = 0; i < MainWindow.DS.Tables["NetworkAdaptor"].Rows.Count; i++)
            {
                NetworkList.AddNewItem(MainWindow.DS.Tables["NetworkAdaptor"].Rows[i]);
            }
            MainWindow.DataBaseAgent.UpdateData(MainWindow.DS.Tables["NetworkAdaptor"]);
            MainWindow.DS.Tables.Remove("NetworkAdaptor");
            NetworkAdaptorSemaphor.Release();
            Button Target = sender as Button;
            Target.IsEnabled = false;
        }

        private void RunningActBtnClicked(object sender, RoutedEventArgs e)
        {
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            ListView Target = (ListView)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "RunningAppList");
            Packet Pack = new Packet();
            Packet.Apps RunApp = new Packet.Apps();
            RunningAppsSemaphore.WaitOne();
            MainWindow.DataBaseAgent.SelectData("RunningApps", ref MainWindow.DS, "*", "RunningApps", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
            RunApp.AppName = MainWindow.DS.Tables["RunningApps"].Rows[Target.SelectedIndex]["Name"].ToString();
            MainWindow.DS.Tables.Remove("RunningApps");
            RunningAppsSemaphore.Release();
            RunApp.Type = (short)Packet.AppsType.Close;
            string Data = Pack.ToString(RunApp);
            Packet.PSProPacket RunPro = new Packet.PSProPacket();
            RunPro.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
            RunPro.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
            RunPro.Type = (short)Packet.PacketType.Apps;
            RunPro.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
            string ProData = Pack.ToString(RunPro);
            Packet.MainPacket MainData = new Packet.MainPacket();
            MainData.Data = Data;
            MainData.PPacket = Pack.ToString(RunPro);
            string MainStrData = Pack.ToString<Packet.MainPacket>(MainData);
            MainWindow.Connection.SendDataSM.WaitOne();
            MainWindow.Connection.SendToServer(MainStrData);
            MainWindow.Connection.SendDataSM.Release();
        }

        private void RunningAppsActSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void RunningAppsSelectChange(object sender, SelectionChangedEventArgs e)
        {
            ListView Target = sender as ListView;
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            Button Action = (Button)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "RunningAppsActionBtn");
            if(Target.SelectedIndex > -1)
            {
                Action.IsEnabled = true;
            }
            else
            {
                Action.IsEnabled = false;
            }
        }

        private void AddLimitaionBtnClick(object sender, RoutedEventArgs e)
        {
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            Button Target = sender as Button;
            try
            {
                foreach (object var in MoreLimitExpand)
                {
                    MainTemp.Children.Remove((UIElement)var);
                }
            }
            catch(Exception E)
            {

            }
            MoreLimitExpand = new List<object>();
            Image InfoImage0 = new System.Windows.Controls.Image();
            InfoImage0.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_info-blog_46810.ico"));
            InfoImage0.Width = 30;
            InfoImage0.Height = 30;
            InfoImage0.VerticalAlignment = VerticalAlignment.Top;
            InfoImage0.HorizontalAlignment = HorizontalAlignment.Left;
            InfoImage0.Margin = new Thickness(Target.Margin.Left + Target.MinWidth + 100 + 40, Target.Margin.Top, 0, 0);
            MainTemp.Children.Add(InfoImage0);
            TextBlock InfoTb0 = new TextBlock();
            InfoTb0.Text = "   Please Fill \"Start Time\" , \"End Time\" and \n\"Duration\" in 24h and hh:mm:ss format";
            InfoTb0.FontSize = 16;
            InfoTb0.VerticalAlignment = VerticalAlignment.Top;
            InfoTb0.HorizontalAlignment = HorizontalAlignment.Left;
            InfoTb0.Foreground = new SolidColorBrush(Colors.Black);
            InfoTb0.Margin = new Thickness(Target.Margin.Left + Target.MinWidth + 160, Target.Margin.Top + 25, 0, 0);
            MainTemp.Children.Add(InfoTb0);
            MoreLimitExpand.Add(InfoTb0);
            MoreLimitExpand.Add(InfoImage0);
            TextBox StartLimitText = new TextBox();
            TextBox EndLimitText = new TextBox();
            TextBox DurationLimitText = new TextBox();
            TextBox NameLimitText = new TextBox();
            TextBlock StartLimitBlock = new TextBlock();
            TextBlock EndLimitBlock = new TextBlock();
            TextBlock DurationLimitBlock = new TextBlock();
            TextBlock NameLimitBlock = new TextBlock();
            TextBlock StarStartLimitBlock = new TextBlock();
            TextBlock StarEndLimitBlock = new TextBlock();
            TextBlock StarActLimitBlock = new TextBlock();
            TextBlock StarNameLimitBlock = new TextBlock();
            StartLimitText.Uid = "StartLimitTime";
            EndLimitText.Uid = "EndLimitTime";
            DurationLimitText.Uid = "DurationLimitTime";
            NameLimitText.Uid = "NameLimitInctance";
            StartLimitBlock.Text = "Limitaion Satrt Time :";
            DurationLimitBlock.Text = "Duration Time :";
            NameLimitBlock.Text = "Name :";
            NameLimitBlock.Margin = new Thickness(10, Target.Margin.Top + 50, 0, 0);
            NameLimitBlock.VerticalAlignment = VerticalAlignment.Top;
            NameLimitBlock.HorizontalAlignment = HorizontalAlignment.Left;
            NameLimitBlock.Foreground = new SolidColorBrush(Colors.Black);
            DurationLimitBlock.Margin = new Thickness(10, Target.Margin.Top + 80, 0, 0);
            DurationLimitBlock.VerticalAlignment = VerticalAlignment.Top;
            DurationLimitBlock.HorizontalAlignment = HorizontalAlignment.Left;
            DurationLimitBlock.Foreground = new SolidColorBrush(Colors.Black);
            StartLimitBlock.Margin = new Thickness(10, Target.Margin.Top + 110, 0, 0);
            StartLimitBlock.VerticalAlignment = VerticalAlignment.Top;
            StartLimitBlock.HorizontalAlignment = HorizontalAlignment.Left;
            StartLimitBlock.Foreground = new SolidColorBrush(Colors.Black);
            EndLimitBlock.Text = "Limitaion End Time :";
            EndLimitBlock.Margin = new Thickness(10, Target.Margin.Top + 140, 0, 0);
            EndLimitBlock.VerticalAlignment = VerticalAlignment.Top;
            EndLimitBlock.HorizontalAlignment = HorizontalAlignment.Left;
            EndLimitBlock.Foreground = new SolidColorBrush(Colors.Black);
            StartLimitText.Margin = new Thickness(125, Target.Margin.Top + 140, 0, 0);
            StartLimitText.VerticalAlignment = VerticalAlignment.Top;
            StartLimitText.HorizontalAlignment = HorizontalAlignment.Left;
            EndLimitText.Margin = new Thickness(125, Target.Margin.Top + 110, 0, 0);
            EndLimitText.MinWidth = 100;
            EndLimitText.VerticalAlignment = VerticalAlignment.Top;
            EndLimitText.HorizontalAlignment = HorizontalAlignment.Left;
            StartLimitText.MinWidth = 100;
            
            DurationLimitText.Margin = new Thickness(125, Target.Margin.Top + 80, 0, 0);
            DurationLimitText.MinWidth = 100;
            NameLimitText.MinWidth = 100;
            NameLimitText.VerticalAlignment = VerticalAlignment.Top;
            NameLimitText.HorizontalAlignment = HorizontalAlignment.Left;
            NameLimitText.Margin = new Thickness(125, Target.Margin.Top + 50, 0, 0);
            DurationLimitText.HorizontalAlignment = HorizontalAlignment.Left;
            DurationLimitText.VerticalAlignment = VerticalAlignment.Top;
            StarStartLimitBlock.VerticalAlignment = VerticalAlignment.Top;
            StarStartLimitBlock.HorizontalAlignment = HorizontalAlignment.Left;
            StarStartLimitBlock.Margin = new Thickness(125 + StartLimitText.MinWidth + 20, Target.Margin.Top + 110, 0, 0);
            StarStartLimitBlock.Text = "*";
            StarStartLimitBlock.FontSize = 16;
            StarStartLimitBlock.Foreground = new SolidColorBrush(Colors.Red);
            StarEndLimitBlock.Margin = new Thickness(125+ EndLimitText.MinWidth + 20, Target.Margin.Top + 140, 0, 0);
            StarEndLimitBlock.Text = "*";
            StarEndLimitBlock.FontSize = 16;
            StarEndLimitBlock.Foreground = new SolidColorBrush(Colors.Red);
            StarEndLimitBlock.VerticalAlignment = VerticalAlignment.Top;
            StarEndLimitBlock.HorizontalAlignment = HorizontalAlignment.Left;
            StarNameLimitBlock.Margin = new Thickness(125 + NameLimitText.MinWidth + 20, Target.Margin.Top + 50, 0, 0);
            StarNameLimitBlock.Text = "*";
            StarNameLimitBlock.FontSize = 16;
            StarNameLimitBlock.Foreground = new SolidColorBrush(Colors.Red);
            StarNameLimitBlock.VerticalAlignment = VerticalAlignment.Top;
            StarNameLimitBlock.HorizontalAlignment = HorizontalAlignment.Left;
            StarActLimitBlock.Margin = new Thickness(125 + NameLimitText.MinWidth + 20, Target.Margin.Top + 170, 0, 0);
            StarActLimitBlock.Text = "*";
            StarActLimitBlock.FontSize = 16;
            StarActLimitBlock.Foreground = new SolidColorBrush(Colors.Red);
            StarActLimitBlock.VerticalAlignment = VerticalAlignment.Top;
            StarActLimitBlock.HorizontalAlignment = HorizontalAlignment.Left;
            MainTemp.Children.Add(StartLimitText);
            MainTemp.Children.Add(StarStartLimitBlock);
            MainTemp.Children.Add(StarActLimitBlock);
            MainTemp.Children.Add(StarNameLimitBlock);
            MainTemp.Children.Add(StarEndLimitBlock);
            MainTemp.Children.Add(EndLimitText);
            MainTemp.Children.Add(StartLimitBlock);
            MainTemp.Children.Add(EndLimitBlock);
            MainTemp.Children.Add(DurationLimitBlock);
            MainTemp.Children.Add(DurationLimitText);
            MainTemp.Children.Add(NameLimitText);
            MainTemp.Children.Add(NameLimitBlock);
            MoreLimitExpand.Add(StartLimitText);
            MoreLimitExpand.Add(StarStartLimitBlock);
            MoreLimitExpand.Add(StarEndLimitBlock);
            MoreLimitExpand.Add(StarActLimitBlock);
            MoreLimitExpand.Add(EndLimitText);
            MoreLimitExpand.Add(StartLimitBlock);
            MoreLimitExpand.Add(StarNameLimitBlock);
            MoreLimitExpand.Add(EndLimitBlock);
            MoreLimitExpand.Add(DurationLimitBlock);
            MoreLimitExpand.Add(DurationLimitText);
            MoreLimitExpand.Add(NameLimitText);
            MoreLimitExpand.Add(NameLimitBlock);
            
            Button OkNewLimit = new Button();
            OkNewLimit.HorizontalAlignment = HorizontalAlignment.Left;
            OkNewLimit.VerticalAlignment = VerticalAlignment.Top;
            //OkNewLimit.Content = "Submit";
            OkNewLimit.Uid = "SubmitNewLimitBtn";
            OkNewLimit.Width = 70;
            OkNewLimit.Cursor = Cursors.Hand;
            OkNewLimit.Height = 30;
            OkNewLimit.Template = MaxBtn.Template;
            OkNewLimit.BorderThickness = new Thickness(0);
            ImageBrush AddImage = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/file_4nI_icon.ico")));
            OkNewLimit.Background = AddImage;
            OkNewLimit.Click += OkNewLimit_Click;
            OkNewLimit.Height = 50;
            OkNewLimit.Width = 50;
            OkNewLimit.Margin = new Thickness(30, Target.Margin.Top + 200 , 0, 30);
            MainTemp.Children.Add(OkNewLimit);
            MoreLimitExpand.Add(OkNewLimit);
            ComboBox ActTypeLimit = new ComboBox();
            TextBlock ActTypeLimitBlock = new TextBlock();
            ActTypeLimit.Margin = new Thickness(135, Target.Margin.Top + 170, 0, 0);
            ActTypeLimit.VerticalAlignment = VerticalAlignment.Top;
            ActTypeLimit.HorizontalAlignment = HorizontalAlignment.Left;
            ActTypeLimit.MinWidth = 80;
            ActTypeLimit.Uid = "LImitActCombo";
            ComboBox TargetCombo = (ComboBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "LimitTypeCombo");
            ActTypeLimit.Items.Clear();
            switch (TargetCombo.SelectedIndex)
            {
                case 0:
                    {
                        ActTypeLimit.Items.Add("Shut Down");
                    };break;
                case 1:
                    {
                        OkNewLimit.Margin = new Thickness(30, Target.Margin.Top + 200 + 200, 0, 30);
                        ActTypeLimit.Items.Add("Close");
                        ActTypeLimit.Items.Add("Close with Show Message");
                        OkNewLimit.IsEnabled = false;
                        TextBlock TB = new TextBlock();
                        TB.Text = "All Installed Program :";
                        TB.FontSize = 14;
                        TB.HorizontalAlignment = HorizontalAlignment.Left;
                        TB.VerticalAlignment = VerticalAlignment.Top;
                        TB.Margin = new Thickness(20, Target.Margin.Top + 200, 0, 0);
                        MainTemp.Children.Add(TB);
                        MoreLimitExpand.Add(TB);
                        ListView AppsList = new ListView();
                        GridView InstalledAppGV = new GridView();
                        InstalledAppGV.Columns.Add(new GridViewColumn { Header = "Name", DisplayMemberBinding = new Binding("DisplayName") });
                        InstalledAppGV.Columns.Add(new GridViewColumn { Header = "Installed Date", DisplayMemberBinding = new Binding("InstallDate") });
                        InstalledAppGV.Columns.Add(new GridViewColumn { Header = "Version", DisplayMemberBinding = new Binding("DisplayVersion") });
                        InstalledAppGV.Columns.Add(new GridViewColumn { Header = "Publisher", DisplayMemberBinding = new Binding("Publisher") });
                        AppsList.View = InstalledAppGV;
                        AppsList.Uid = "AppsListInNewLimitPage";
                        AppsList.VerticalAlignment = VerticalAlignment.Top;
                        AppsList.MaxHeight = 150;
                        AppsList.Margin = new Thickness(20, Target.Margin.Top + 200 + 40, 10, 10);
                        AppsList.SelectionChanged += InstalledAppLimitSelectionChenge;
                        MainTemp.Children.Add(AppsList);
                        MoreLimitExpand.Add(AppsList);
                        InstalledAppsSemaphore.WaitOne();
                        MainWindow.DataBaseAgent.SelectData("InstalledApps", ref MainWindow.DS, "*", "InstalledApps", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
                        foreach (System.Data.DataRow Row in MainWindow.DS.Tables["InstalledApps"].Rows)
                        {
                            AppsList.Items.Add(new InstalledApp
                            {
                                DisplayName = Row["DisplayName"].ToString(),
                                DisplayVersion = Row["DisplayVersion"].ToString(),
                                InstallDate = Row["InstallDate"].ToString(),
                                Publisher = Row["Publisher"].ToString()
                            });
                        }
                        MainWindow.DS.Tables.Remove("InstalledApps");
                        InstalledAppsSemaphore.Release();

                    }; break;
                case 2:
                    {
                        OkNewLimit.Margin = new Thickness(30, Target.Margin.Top + 200 + 200, 0, 30);
                        ActTypeLimit.Items.Add("Disable");
                        GridView NetworkAdabtorGV = new GridView();
                       
                        NetworkAdabtorGV.Columns.Add(new GridViewColumn { Header = "Device Name", DisplayMemberBinding = new Binding("DeviceName") });
                        NetworkAdabtorGV.Columns.Add(new GridViewColumn { Header = "Interface Name", DisplayMemberBinding = new Binding("InterfaceName") });
                        NetworkAdabtorGV.Columns.Add(new GridViewColumn { Header = "Status", DisplayMemberBinding = new Binding("Status") });
                        TextBlock TB = new TextBlock();
                        TB.Text = "All Adaptor :";
                        TB.FontSize = 14;
                        TB.HorizontalAlignment = HorizontalAlignment.Left;
                        TB.VerticalAlignment = VerticalAlignment.Top;
                        TB.Margin = new Thickness(20, Target.Margin.Top + 200, 0, 0);
                        MainTemp.Children.Add(TB);
                        MoreLimitExpand.Add(TB);
                        OkNewLimit.IsEnabled = false;
                        ListView NetworkAdaptorList = new ListView();
                        NetworkAdaptorList.View = NetworkAdabtorGV;
                        NetworkAdaptorList.MaxHeight = 150;
                        NetworkAdaptorList.Uid = "AllAdaptorInNewLimitList";
                        NetworkAdaptorList.VerticalAlignment = VerticalAlignment.Top;
                        NetworkAdaptorList.Margin = new Thickness(20, Target.Margin.Top + 200 + 40, 10, 10);
                        NetworkAdaptorList.SelectionChanged += NetworkAdaptorLimitSelectionChange;
                        NetworkAdaptorSemaphor.WaitOne();
                        MainWindow.DataBaseAgent.SelectData("NetworkAdaptor", ref MainWindow.DS, "*", "NetworkAdaptor", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
                        foreach (System.Data.DataRow Row in MainWindow.DS.Tables["NetworkAdaptor"].Rows)
                        {
                            NetworkAdaptorList.Items.Add(new NetworkAdaptor
                            {
                                DeviceName = Row["DeviceName"].ToString(),
                                InterfaceName = Row["InterfaceName"].ToString(),
                                Status = Row["Status"].ToString()
                            });
                        }
                        MainWindow.DS.Tables.Remove("NetworkAdaptor");
                        NetworkAdaptorSemaphor.Release();
                        MainTemp.Children.Add(NetworkAdaptorList);
                        MoreLimitExpand.Add(NetworkAdaptorList);
                    }; break;
            }
            MainTemp.Children.Add(ActTypeLimit);
            MoreLimitExpand.Add(ActTypeLimit);
            ActTypeLimitBlock.Text = "Limitaion Action Type :";
            ActTypeLimitBlock.Margin = new Thickness(10, Target.Margin.Top + 170, 0, 0);
            ActTypeLimitBlock.VerticalAlignment = VerticalAlignment.Top;
            ActTypeLimitBlock.Foreground = new SolidColorBrush(Colors.Black);
            ActTypeLimitBlock.HorizontalAlignment = HorizontalAlignment.Left;
            MainTemp.Children.Add(ActTypeLimitBlock);
            MoreLimitExpand.Add(ActTypeLimitBlock);
        }

        private void OkNewLimit_Click(object sender, RoutedEventArgs e)
        {
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            Border TargetList = (Border)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "ShowLimitList");
            TextBox Start = (TextBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "StartLimitTime");
            TextBox End = (TextBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "EndLimitTime");
            TextBox Duration = (TextBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "DurationLimitTime");
            TextBox Name = (TextBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "NameLimitInctance");
            ComboBox ActCombo = (ComboBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "LImitActCombo");
            ComboBox TypeCombo = (ComboBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "LimitTypeCombo");
            DateTime Base = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            bool NameState = false;
            bool StartState = false;
            bool EndState = false;
            bool DurationState = false;
            bool ActState = false;
            Name.BorderBrush = new SolidColorBrush(Colors.Black);
            if (Start.Text.Split(':').Length == 3)
            {
                StartState = true;
                Start.BorderBrush = new SolidColorBrush(Colors.Black);
            }
            else
            {
                Start.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            if (End.Text.Split(':').Length == 3)
            {
                EndState = true;
                End.BorderBrush = new SolidColorBrush(Colors.Black);
            }
            else
            {
                End.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            if ((Duration.Text.Split(':').Length == 3) || Duration.Text == "")
            {
                DurationState = true;
                Duration.BorderBrush = new SolidColorBrush(Colors.Black);
            }
            else
            {
                Duration.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            if (ActCombo.SelectedIndex > -1)
            {
                ActState = true;
                ActCombo.BorderBrush = new SolidColorBrush(Colors.Black);
            }
            else
            {
                ActCombo.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            if(Name.Text != "")
            {
                NameState = true;
                Name.BorderBrush = new SolidColorBrush(Colors.Black);
            }
            else
            {
                Name.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            if (StartState & EndState & DurationState & ActState)
            {
                switch (TypeCombo.SelectedIndex)
                {
                    case 0:
                        {
                            SystemLimit NewLimit = new SystemLimit();
                            try
                            {
                                MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From SystemLimit where ChildID ='" + ChildsCombo.SelectedItem.ToString().ToString() + "' AND ID ='" + Name.Text + "'", ref MainWindow.DS, "SystemLimit");
                                if(MainWindow.DS.Tables["SystemLimit"].Rows.Count == 0)
                                {
                                    DataRow NewRow = MainWindow.DS.Tables["SystemLimit"].NewRow();
                                    
                                    NewLimit.Act = ActCombo.SelectedIndex.ToString();
                                    NewLimit.StartTime = Convert.ToDateTime(Start.Text) - Base;
                                    NewLimit.EndTime = Convert.ToDateTime(End.Text) - Base;
                                    NewRow["StartTime"] = NewLimit.StartTime;
                                    NewRow["EndTime"] = NewLimit.EndTime;
                                    NewRow["Act"] = NewLimit.Act;
                                    NewRow["ChildID"] = ChildsCombo.SelectedItem.ToString().ToString();
                                    NewRow["Name"] = Name.Text;
                                    NewRow["ID"] = Name.Text;
                                    if (Duration.Text != "")
                                    {
                                        NewLimit.Duration = Convert.ToDateTime(Duration.Text) - Base;
                                        NewRow["Duration"] = NewLimit.Duration;
                                    }
                                    else
                                    {
                                        NewLimit.Duration = new TimeSpan(0);
                                    }
                                    MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["SystemLimit"]);
                                    string Act = "Shut Down";
                                    string Temp = NewRow["Act"].ToString();
                                    NewRow["Act"] = Act;
                                    LimitList.AddNewItem(NewRow);
                                    NewRow["Act"] = Temp;
                                    Packet PC = new Packet();
                                    Packet.SystemLimition3Time SYSData = new Packet.SystemLimition3Time();
                                    SYSData.Start = NewLimit.StartTime;
                                    SYSData.End = NewLimit.EndTime;
                                    SYSData.Duration = NewLimit.Duration;
                                    SYSData.Act = (short)(ActCombo.SelectedIndex + 1);
                                    SYSData.Id = Name.Text;
                                    Packet.PSProPacket ProSys = new Packet.PSProPacket();
                                    string Data = PC.ToString(SYSData);
                                    ProSys.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                                    ProSys.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
                                    ProSys.Type = (short)Packet.PacketType.SystemLimition3Time;
                                    ProSys.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                                    string ProData = PC.ToString(ProSys);
                                    Packet.MainPacket MainData = new Packet.MainPacket();
                                    MainData.Data = Data;
                                    MainData.PPacket = PC.ToString(ProSys);
                                    string MainStrData = PC.ToString<Packet.MainPacket>(MainData);
                                    MainWindow.Connection.SendDataSM.WaitOne();
                                    MainWindow.Connection.SendToServer(MainStrData);
                                    MainWindow.Connection.SendDataSM.Release();
                                }
                                else
                                {
                                    Name.BorderBrush = new SolidColorBrush(Colors.Red);
                                }
                                
                                
                            }
                            catch (Exception E)
                            {

                            }
                            LimitList.AddNewItem(NewLimit);
                        }; break;
                    case 1:
                        {
                            AppsLimit NewLimit = new AppsLimit();
                            try
                            {
                                MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From AppsLimit where ChildID ='" + ChildsCombo.SelectedItem.ToString().ToString() + "' AND ID ='" + Name.Text + "'", ref MainWindow.DS, "AppsLimit");
                                ListView TargetListV = (ListView)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "AppsListInNewLimitPage");
                                if (MainWindow.DS.Tables["AppsLimit"].Rows.Count == 0)
                                {
                                    DataRow NewRow = MainWindow.DS.Tables["AppsLimit"].NewRow();
                                    NewLimit.Act = ActCombo.SelectedIndex.ToString();
                                    NewLimit.AppName = ((InstalledApp)TargetListV.SelectedItem).DisplayName;
                                    NewLimit.StartTime = Convert.ToDateTime(Start.Text) - Base;
                                    NewLimit.EndTime = Convert.ToDateTime(End.Text) - Base;
                                    NewRow["StartTime"] = NewLimit.StartTime;
                                    NewRow["EndTime"] = NewLimit.EndTime;
                                    NewRow["Act"] = NewLimit.Act;
                                    NewRow["ChildID"] = ChildsCombo.SelectedItem.ToString().ToString();
                                    NewRow["AppName"] = ((InstalledApp)TargetListV.SelectedItem).DisplayName;
                                    NewRow["ID"] = Name.Text;
                                    if (Duration.Text != "")
                                    {
                                        NewLimit.Duratin = Convert.ToDateTime(Duration.Text) - Base;
                                        NewRow["Duration"] = NewLimit.Duratin;
                                    }
                                    else
                                    {
                                        NewLimit.Duratin = new TimeSpan(0);
                                    }
                                    MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["AppsLimit"]);
                                    string Act = "Close";
                                    switch (NewLimit.Act)
                                    {
                                        case "0":
                                            {
                                                Act = "Close";
                                            };break;
                                        case "1":
                                            {
                                                Act = "Close with Message";
                                            };break;
                                    }
                                    string Temp = NewRow["Act"].ToString();
                                    NewRow["Act"] = Act;

                                    LimitList.AddNewItem(NewRow);
                                    NewRow["Act"] = Temp;
                                    Packet PC = new Packet();
                                    Packet.AppLimition3Time SYSData = new Packet.AppLimition3Time();
                                    SYSData.Start = NewLimit.StartTime;
                                    SYSData.End = NewLimit.EndTime;
                                    SYSData.Duration = NewLimit.Duratin;
                                    SYSData.Act = (short)(ActCombo.SelectedIndex);
                                    SYSData.AppName = (Name.Text + " - " + ((InstalledApp)TargetListV.SelectedItem).DisplayName);
                                    Packet.PSProPacket ProSys = new Packet.PSProPacket();
                                    string Data = PC.ToString(SYSData);
                                    ProSys.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                                    ProSys.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
                                    ProSys.Type = (short)Packet.PacketType.AppLimition3Time;
                                    ProSys.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                                    string ProData = PC.ToString(ProSys);
                                    Packet.MainPacket MainData = new Packet.MainPacket();
                                    MainData.Data = Data;
                                    MainData.PPacket = PC.ToString(ProSys);
                                    string MainStrData = PC.ToString<Packet.MainPacket>(MainData);
                                    MainWindow.Connection.SendDataSM.WaitOne();
                                    MainWindow.Connection.SendToServer(MainStrData);
                                    MainWindow.Connection.SendDataSM.Release();
                                }
                                else
                                {
                                    Name.BorderBrush = new SolidColorBrush(Colors.Red);
                                }
                                
                            }
                            catch (Exception E)
                            {

                            }
                            LimitList.AddNewItem(NewLimit);
                        }
                        break;
                    case 2:
                        {
                            NetworkLimit NewLimit = new NetworkLimit();
                            ListView Target = (ListView)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "AllAdaptorInNewLimitList");
                            MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From NetworkLimit where ChildID ='" + ChildsCombo.SelectedItem.ToString().ToString() + "' AND ID ='" + Name.Text + "'", ref MainWindow.DS, "NetworkLimit");
                            try
                            {
                                if (MainWindow.DS.Tables["NetworkLimit"].Rows.Count == 0)
                                {
                                    DataRow NewRow = MainWindow.DS.Tables["NetworkLimit"].NewRow();
                                    NewLimit.Act = ActCombo.SelectedIndex.ToString();
                                    NewLimit.Name = Name.Text;
                                    NewLimit.StartTime = Convert.ToDateTime(Start.Text) - Base;
                                    NewLimit.EndTime = Convert.ToDateTime(End.Text) - Base;
                                    NewRow["StartTime"] = NewLimit.StartTime;
                                    NewRow["EndTime"] = NewLimit.EndTime;
                                    NewRow["Act"] = NewLimit.Act;
                                    NewRow["ChildID"] = ChildsCombo.SelectedItem.ToString().ToString();
                                    NewRow["Name"] = ((NetworkAdaptor) Target.SelectedItem).DeviceName;
                                    NewRow["ID"] = Name.Text;
                                    if (Duration.Text != "")
                                    {
                                        NewLimit.Duration = Convert.ToDateTime(Duration.Text) - Base;
                                        NewRow["Duration"] = NewLimit.Duration;
                                    }
                                    else
                                    {
                                        NewLimit.Duration = new TimeSpan(0);
                                    }
                                    MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["NetworkLimit"]);
                                    string Act = "Disable";
                                    string Temp = NewRow["Act"].ToString();
                                    NewRow["Act"] = Act;
                                    LimitList.AddNewItem(NewRow);
                                    NewRow["Act"] = Temp;
                                    Packet PC = new Packet();
                                    Packet.NetworkCommands SYSData = new Packet.NetworkCommands();
                                    SYSData.Command = (Name.Text + "$" + ((NetworkAdaptor)Target.SelectedItem).DeviceName);
                                    SYSData.Command += ("$" + NewLimit.StartTime.ToString() + "$");
                                    SYSData.Command += NewLimit.EndTime.ToString();
                                    SYSData.Type = (short)Packet.NetworkCommandsType.EnableWithTime;
                                    Packet.PSProPacket ProSys = new Packet.PSProPacket();
                                    string Data = PC.ToString(SYSData);
                                    ProSys.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                                    ProSys.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
                                    ProSys.Type = (short)Packet.PacketType.NetworkCommands;
                                    ProSys.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                                    string ProData = PC.ToString(ProSys);
                                    Packet.MainPacket MainData = new Packet.MainPacket();
                                    MainData.Data = Data;
                                    MainData.PPacket = PC.ToString(ProSys);
                                    string MainStrData = PC.ToString<Packet.MainPacket>(MainData);
                                    MainWindow.Connection.SendDataSM.WaitOne();
                                    MainWindow.Connection.SendToServer(MainStrData);
                                    MainWindow.Connection.SendDataSM.Release();
                                }
                                else
                                {
                                    Name.BorderBrush = new SolidColorBrush(Colors.Red);
                                }
                                
                            }
                            catch (Exception E)
                            {

                            }
                            LimitList.AddNewItem(NewLimit);
                        }; break;
                }
            }
            
        }

        private void RemoveLimitBtnClick(object sender, RoutedEventArgs e)
        {
            
            Packet Pack = new Packet();
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            switch (((ComboBox)MainTemp.Children.Cast<UIElement>().First(x=>x.Uid == "LimitTypeCombo")).SelectedIndex)
            {
                case 0:
                    {
                        Packet.SystemLimition3Time SysLimit = new Packet.SystemLimition3Time();
                        MainWindow.DataBaseAgent.SelectData("SystemLimit", ref MainWindow.DS, "*", "SystemLimit", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
                        SysLimit.Act = (short)Packet.SystemLimitAct.Disable;
                        SysLimit.Id = MainWindow.DS.Tables["SystemLimit"].Rows[LimitList.SelectedIndex]["ID"].ToString();
                        SysLimit.Start = DateTime.Now - DateTime.Now;
                        SysLimit.End = DateTime.Now - DateTime.Now;
                        SysLimit.Duration = new TimeSpan(0);
                        Packet.PSProPacket ProSysLimit = new Packet.PSProPacket();
                        ProSysLimit.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
                        ProSysLimit.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                        ProSysLimit.Type = (short)Packet.PacketType.SystemLimition3Time;
                        string Data = Pack.ToString(SysLimit);
                        ProSysLimit.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                        string ProStr = Pack.ToString(ProSysLimit);
                        Packet.MainPacket MainData = new Packet.MainPacket();
                        MainData.Data = Data;
                        MainData.PPacket = Pack.ToString(ProSysLimit);
                        string MainStrData = Pack.ToString<Packet.MainPacket>(MainData);
                        MainWindow.Connection.SendDataSM.WaitOne();
                        MainWindow.Connection.SendToServer(MainStrData);
                        MainWindow.Connection.SendDataSM.Release();
                        MainWindow.DS.Tables["SystemLimit"].Rows.RemoveAt(LimitList.SelectedIndex);
                        MainWindow.DataBaseAgent.UpdateData(MainWindow.DS.Tables["SystemLimit"]);
                    }
                    break;
                case 1:
                    {
                        Packet.AppLimition3Time SysLimit = new Packet.AppLimition3Time();
                        SysLimit.Act = (short)Packet.AppLimitAct.Disable;
                        SysLimit.AppName = (MainWindow.DS.Tables["AppsLimit"].Rows[LimitList.SelectedIndex]["AppName"].ToString() + "$" +
                            MainWindow.DS.Tables["AppsLimit"].Rows[LimitList.SelectedIndex]["AppID"].ToString() + "$" +
                            MainWindow.DS.Tables["AppsLimit"].Rows[LimitList.SelectedIndex]["ID"].ToString());
                        SysLimit.Start = DateTime.Now - DateTime.Now;
                        SysLimit.End = DateTime.Now - DateTime.Now;
                        SysLimit.Duration = new TimeSpan(0);
                        Packet.PSProPacket ProSysLimit = new Packet.PSProPacket();
                        ProSysLimit.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
                        ProSysLimit.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                        ProSysLimit.Type = (short)Packet.PacketType.AppLimition3Time;
                        string Data = Pack.ToString(SysLimit);
                        ProSysLimit.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                        string ProStr = Pack.ToString(ProSysLimit);
                        Packet.MainPacket MainData = new Packet.MainPacket();
                        MainData.Data = Data;
                        MainData.PPacket = Pack.ToString(ProSysLimit);
                        string MainStrData = Pack.ToString<Packet.MainPacket>(MainData);
                        MainWindow.Connection.SendDataSM.WaitOne();
                        MainWindow.Connection.SendToServer(MainStrData);
                        MainWindow.Connection.SendDataSM.Release();
                        MainWindow.DS.Tables["AppsLimit"].Rows.RemoveAt(LimitList.SelectedIndex);
                        MainWindow.DataBaseAgent.UpdateData(MainWindow.DS.Tables["AppsLimit"]);
                    }
                    break;
                case 2:
                    {
                        Packet.NetworkCommands SysLimit = new Packet.NetworkCommands();
                        MainWindow.DataBaseAgent.SelectData("NetworkLimit", ref MainWindow.DS, "*", "NetworkLimit", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
                        SysLimit.Type = (short)Packet.NetworkCommandsType.DisableWithTime;
                        SysLimit.Command = (MainWindow.DS.Tables["NetworkLimit"].Rows[LimitList.SelectedIndex]["Name"].ToString() + "$" +
                        MainWindow.DS.Tables["NetworkLimit"].Rows[LimitList.SelectedIndex]["ID"].ToString());
                        SysLimit.Time = DateTime.Now.ToString();
                        Packet.PSProPacket ProSysLimit = new Packet.PSProPacket();
                        ProSysLimit.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
                        ProSysLimit.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                        ProSysLimit.Type = (short)Packet.PacketType.AppLimition3Time;
                        string Data = Pack.ToString(SysLimit);
                        ProSysLimit.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                        string ProStr = Pack.ToString(ProSysLimit);
                        Packet.MainPacket MainData = new Packet.MainPacket();
                        MainData.Data = Data;
                        MainData.PPacket = Pack.ToString(ProSysLimit);
                        string MainStrData = Pack.ToString<Packet.MainPacket>(MainData);
                        MainWindow.Connection.SendDataSM.WaitOne();
                        MainWindow.Connection.SendToServer(MainStrData);
                        MainWindow.Connection.SendDataSM.Release();
                        MainWindow.DS.Tables["NetworkLimit"].Rows.RemoveAt(LimitList.SelectedIndex);
                        MainWindow.DataBaseAgent.UpdateData(MainWindow.DS.Tables["NetworkLimit"]);
                    };break;
            }
            TextBlock Result = (TextBlock)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "LimitActionResult");
            Result.Text = "Remove Successfully";
            LimitList.RemoveAt(LimitList.SelectedIndex);
        }

        private void ComboLimitSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Target = sender as ComboBox;
            
            Grid MainTemp = (Grid)Target.Parent;
            TextBlock textBlock = (TextBlock)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "LimitaionTypeTxt");
            MainTemp.Children.Remove(MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "ShowLimitList"));
            switch (Target.SelectedItem)
            {
                
                case "System Limitaion":
                    {
                        string[] Acts =
                        {
                            "ShutDown","ShoutDown With Message","Reboot","Sleep","Logoff","Lock"
                        };
                        if(MainWindow.DS.Tables.Contains("SystemLimit") == true)
                        {
                            MainWindow.DS.Tables.Remove("SystemLimit");
                        }
                        MainWindow.DataBaseAgent.SelectData("SystemLimit", ref MainWindow.DS, "SystemLimit");
                        LimitList = new ColorList();
                        Size ItemSize = new Size(0, 25);
                        Size ListSize = new Size(600, 0);
                        double[] ColumnWidth = { 200, 100, 100, 100, 100 };
                        string[] HeadersName = { "Name", "Start Time", "End Time", "Duration", "Action" };
                        string[] ColumnsName = { "ID", "StartTime", "EndTime", "Duration", "Act" };
                        Border border = LimitList.Draw(ColumnWidth, 30, HeadersName, null, ListSize, ItemSize, Color.FromArgb(255, 238, 241, 245), Colors.Transparent,
                            Colors.White, Colors.Black, Color.FromArgb(255, 42, 180, 192), Colors.LightGray, Colors.Transparent, MainWindow.DS.Tables["SystemLimit"],
                            new Thickness(30, 165, 10, 10), new Thickness(0), 12, 16, ColumnsName, LimitSelectionChange, "ShowLimitList", 300);
                        border.VerticalAlignment = VerticalAlignment.Top;
                        border.HorizontalAlignment = HorizontalAlignment.Left;
                        MainTemp.Children.Add(border);
                        if (MainWindow.DS.Tables.Contains("SystemLimit") == true)
                        {
                            MainWindow.DS.Tables.Remove("SystemLimit");
                        }
                        Target.Margin = new Thickness(650, Target.Margin.Top, Target.Margin.Right, Target.Margin.Bottom);
                        textBlock.Margin = new Thickness(650, textBlock.Margin.Top, textBlock.Margin.Right, textBlock.Margin.Bottom);
                        foreach (UIElement var in MainTemp.Children)
                        {
                            if ((var is ComboBox) && ((ComboBox)var).Uid == "LImitActCombo")
                            {
                                ((ComboBox)var).ItemsSource = Acts;
                                ((ComboBox)var).SelectedIndex = 0;
                            }
                        }

                    }; break;
                case "App Limitaion":
                    {
                        string[] Acts =
                        {
                            "Close","Close With Show Message"
                        };
                        if (MainWindow.DS.Tables.Contains("AppsLimit") == true)
                        {
                            MainWindow.DS.Tables.Remove("AppsLimit");
                        }
                        MainWindow.DataBaseAgent.SelectData("AppsLimit", ref MainWindow.DS, "AppsLimit");
                        LimitList = new ColorList();
                        Size ItemSize = new Size(0, 25);
                        Size ListSize = new Size(600, 0);
                        double[] ColumnWidth = { 200, 100, 100, 100, 100 };
                        string[] HeadersName = { "Name", "Start Time", "End Time", "Duration", "Action" };
                        string[] ColumnsName = { "AppName", "StartTime", "EndTime", "Duration", "Act" };
                        Border border = LimitList.Draw(ColumnWidth, 30, HeadersName, null, ListSize, ItemSize, Color.FromArgb(255, 238, 241, 245), Colors.Transparent,
                            Colors.White, Colors.Black, Color.FromArgb(255, 42, 180, 192), Colors.LightGray, Colors.Transparent, MainWindow.DS.Tables["AppsLimit"],
                            new Thickness(30, 165, 10, 10), new Thickness(0), 12, 16, ColumnsName, LimitSelectionChange, "ShowLimitList", 300);
                        border.VerticalAlignment = VerticalAlignment.Top;
                        border.HorizontalAlignment = HorizontalAlignment.Left;
                        MainTemp.Children.Add(border);
                        if (MainWindow.DS.Tables.Contains("AppsLimit") == true)
                        {
                            MainWindow.DS.Tables.Remove("AppsLimit");
                        }
                        Target.Margin = new Thickness(650, Target.Margin.Top, Target.Margin.Right, Target.Margin.Bottom);
                        textBlock.Margin = new Thickness(650, textBlock.Margin.Top, textBlock.Margin.Right, textBlock.Margin.Bottom);
                    }; break;
                case "Network Limitaion":
                    {
                        string[] Acts =
                        {
                            "Disable" , "Disable With Show Message"
                        };
                        if (MainWindow.DS.Tables.Contains("NetworkLimit") == true)
                        {
                            MainWindow.DS.Tables.Remove("NetworkLimit");
                        }
                        MainWindow.DataBaseAgent.SelectData("NetworkLimit", ref MainWindow.DS, "NetworkLimit");
                        LimitList = new ColorList();
                        Size ItemSize = new Size(0, 25);
                        Size ListSize = new Size(800, 0);
                        double[] ColumnWidth = { 200, 200,100, 100, 100, 100 };
                        string[] HeadersName = { "Name", "Device","Start Time", "End Time", "Duration", "Action" };
                        string[] ColumnsName = { "ID","Name", "StartTime", "EndTime", "Duration", "Act" };
                        Border border = LimitList.Draw(ColumnWidth, 30, HeadersName, null, ListSize, ItemSize, Color.FromArgb(255, 238, 241, 245), Colors.Transparent,
                            Colors.White, Colors.Black, Color.FromArgb(255, 42, 180, 192), Colors.LightGray, Colors.Transparent, MainWindow.DS.Tables["NetworkLimit"],
                            new Thickness(30, 165, 10, 10), new Thickness(0), 12, 16, ColumnsName, LimitSelectionChange, "ShowLimitList", 300);
                        border.VerticalAlignment = VerticalAlignment.Top;
                        border.HorizontalAlignment = HorizontalAlignment.Left;
                        MainTemp.Children.Add(border);
                        if (MainWindow.DS.Tables.Contains("NetworkLimit") == true)
                        {
                            MainWindow.DS.Tables.Remove("NetworkLimit");
                        }

                        Target.Margin = new Thickness(850, Target.Margin.Top, Target.Margin.Right, Target.Margin.Bottom);
                        textBlock.Margin = new Thickness(850, textBlock.Margin.Top, textBlock.Margin.Right, textBlock.Margin.Bottom);
                    }; break;
            }

        }

        private void NetworkAdaptorLimitSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            ListView TargetList = sender as ListView;
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            Button TargetBtn = (Button)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "SubmitNewLimitBtn");
            if(TargetList.SelectedIndex > -1)
            {
                TargetBtn.IsEnabled = true;
            }
            else
            {
                TargetBtn.IsEnabled = false;
            }
        }

        private void InstalledAppLimitSelectionChenge(object sender, SelectionChangedEventArgs e)
        {
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            ListView TargetList =  sender as ListView; 
            Button TargetBtn = (Button)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "SubmitNewLimitBtn");
            if (TargetList.SelectedIndex > -1)
            {
                TargetBtn.IsEnabled = true;
            }
            else
            {
                TargetBtn.IsEnabled = false;
            }
        }

        private void LimitSelectionChange(object sender)
        {
            try
            {
                WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
                Button TargetBtn = (Button)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "LimitRemoveBtn");
                
                if (LimitList.SelectedIndex > -1)
                {
                    if (TargetBtn != null)
                    {
                        TargetBtn.IsEnabled = true;
                    }
                }
                else
                {
                    if (TargetBtn != null)
                    {
                        TargetBtn.IsEnabled = false;
                    }
                }
            }
            catch(Exception E)
            {

            }
            
        }

        private void RemoveImageClick(object sender, RoutedEventArgs e)
        {
            ScreenShotSemaphore.WaitOne();
            MainWindow.DataBaseAgent.ExequteWithCommand("Delete From ScreenShot where Date ='" + "" + "'");
            ScreenShotSemaphore.Release();
            Button Target = sender as Button;
            StackPanel TargetStack = (StackPanel)((ScrollViewer)((Border)((Grid)Target.Parent).Children.Cast<UIElement>().First(x => x.Uid == "ImageListBorder")).Child).Content;
            TargetStack.Children.RemoveAt(SCSelected);
        }

        private void UninstallClick(object sender, RoutedEventArgs e)
        {
            WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
            Border Target= (Border)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "InstalledAppsList");
            Packet Pack = new Packet();
            Packet.Apps Uninstall = new Packet.Apps();
            StackPanel SP = (StackPanel) Target.Child;
            InstalledAppsSemaphore.WaitOne();
            string AppID = MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select AppID From InstalledApps where ChildID ='" + ChildsCombo.SelectedItem.ToString() + "'And DisplayName='" + SP.Children[0] + "' And InstallDate ='" + SP.Children[1] + "'").ToString();
            InstalledAppsSemaphore.Release();
            Uninstall.AppName = (SP.Children[0] + "$" + AppID + "$" + SP.Children[1]);
            Uninstall.Type = (short)Packet.AppsType.Uninstall;
            Packet.PSProPacket ProUninstall = new Packet.PSProPacket();
            ProUninstall.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
            ProUninstall.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
            ProUninstall.Type = (short)Packet.PacketType.Apps;
            string Data = Pack.ToString(Uninstall);
            ProUninstall.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
            string ProStr = Pack.ToString(ProUninstall);
            Packet.MainPacket MainData = new Packet.MainPacket();
            MainData.Data = Data;
            MainData.PPacket = Pack.ToString(ProUninstall);
            string MainStrData = Pack.ToString<Packet.MainPacket>(MainData);
            MainWindow.Connection.SendDataSM.WaitOne();
            MainWindow.Connection.SendToServer(MainStrData);
            //MainWindow.Connection.SendToServer(Data);
            MainWindow.Connection.SendDataSM.Release();
        }

        private void URLBAddClick(object sender, RoutedEventArgs e)
        {
            
            Packet Pack = new Packet();
            WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
            StackPanel stackPanel = (StackPanel)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "BlockURLStP");
            MainTemp = (WrapPanel)stackPanel.Children[1];
            stackPanel = (StackPanel)MainTemp.Children[1];
            MainTemp = (WrapPanel)stackPanel.Children[0];
            TextBox URLDomain = (TextBox)MainTemp.Children[1];
            URLDomain.BorderBrush = new SolidColorBrush(Colors.Black);
            MainTemp = (WrapPanel)stackPanel.Children[1];
            ComboBox TargetCat = (ComboBox)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "URLCateguryCombo");
            MainTemp = (WrapPanel)URLDomain.Parent;
            TextBlock textBlock = (TextBlock)MainTemp.Children[2];
            int Find = (int)MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select Count(*) From BlockUrls where ChildID ='" + ChildsCombo.SelectedItem.ToString().ToString() + "' And URL ='" + URLDomain.Text + "'");
            if(Find == 0)
            {
                textBlock.Text = "";
                MainWindow.DataBaseAgent.SelectData("BlockUrls", ref MainWindow.DS, "*", "BlockUrls", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
                System.Data.DataRow Row = MainWindow.DS.Tables["BlockUrls"].NewRow();
                Row["ID"] = MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select NEWID()");
                Row["URL"] = URLDomain.Text;
                Row["Act"] = "Redirect";
                Row["ChildID"] = ChildsCombo.SelectedItem.ToString().ToString();
                if(TargetCat.SelectedIndex == -1)
                {
                    Row["Cat"] = "";
                }
                else
                {
                    Row["Cat"] = TargetCat.SelectedItem.ToString();
                }
                MainWindow.DS.Tables["BlockUrls"].Rows.Add(Row);
                MainWindow.DataBaseAgent.InsertData(MainWindow.DS.Tables["BlockUrls"]);
                BlockUrl NewBlok = new BlockUrl();
                NewBlok.URL = URLDomain.Text;
                NewBlok.Act = "Redirect";
                NewBlok.ID = MainWindow.DS.Tables["BlockUrls"].Rows[BlockURLList.Count]["ID"].ToString();
                BlockURLList.AddNewItem(Row);
                MainWindow.DS.Tables.Remove("BlockUrls");
                Packet.URL DataPack = new Packet.URL();
                DataPack.Address = (NewBlok.URL + "$" + "google.com");
                DataPack.Browser = "";
                DataPack.Time = DateTime.Now;
                DataPack.Type = (short)Packet.URLType.Block;
                string Data = Pack.ToString(DataPack);
                Packet.PSProPacket ProURL = new Packet.PSProPacket();
                ProURL.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
                ProURL.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
                ProURL.Type = (short)Packet.PacketType.URL;
                ProURL.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
                string ProData = Pack.ToString(ProURL);
                Packet.MainPacket MainData = new Packet.MainPacket();
                MainData.Data = Data;
                MainData.PPacket = Pack.ToString(ProURL);
                string MainStrData = Pack.ToString<Packet.MainPacket>(MainData);
                MainWindow.Connection.SendDataSM.WaitOne();
                MainWindow.Connection.SendToServer(MainStrData);
                //MainWindow.Connection.SendToServer(Data);
                MainWindow.Connection.SendDataSM.Release();
            }
            else
            {
               
                textBlock.Text = "Same URL Exist In List ";
            }

        }

        private void BlockRemoveClick(object sender, RoutedEventArgs e)
        {
            Grid MainTemp = (Grid)((Border)MainDataGrid.Children[1]).Child;
            ListView Target = (ListView)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "BlockUrlList");
            Packet Pack = new Packet();
            Packet.URL BlockURL = new Packet.URL();
            MainWindow.DataBaseAgent.SelectData("BlockUrls", ref MainWindow.DS, "*", "BlockUrls", ChildsCombo.SelectedItem.ToString().ToString(), "ChildID");
            BlockURL.Address = MainWindow.DS.Tables["BlockUrls"].Rows[Target.SelectedIndex]["URL"].ToString();
            BlockURL.Browser = "";
            BlockURL.Time = DateTime.Now;
            BlockURL.Type = (short)Packet.URLType.Disable;
            string Data = Pack.ToString(BlockURL);
            Packet.PSProPacket ProURL = new Packet.PSProPacket();
            ProURL.Reciver = ChildsCombo.SelectedItem.ToString().ToString();
            ProURL.ID = MainWindow.DS.Tables["Data"].Rows[2]["DataContent"].ToString();
            ProURL.Type = (short)Packet.PacketType.URL;
            ProURL.TotalSize = Encoding.Unicode.GetBytes(Data).Length;
            string ProStr = Pack.ToString(ProURL);
            Packet.MainPacket MainData = new Packet.MainPacket();
            MainData.Data = Data;
            MainData.PPacket = Pack.ToString(ProURL);
            string MainStrData = Pack.ToString<Packet.MainPacket>(MainData);
            MainWindow.Connection.SendDataSM.WaitOne();
            MainWindow.Connection.SendToServer(MainStrData);
            MainWindow.Connection.SendDataSM.Release();
            MainWindow.DataBaseAgent.ExequteWithCommand("Delete From BlockUrls where ChildID ='" + ChildUser + "' And URL='" + BlockURL.Address + "'");
            Target.Items.RemoveAt(Target.SelectedIndex);
            MainWindow.DS.Tables.Remove("BlockUrls");
        }

        private void ExpanderHistoryClick(object sender, RoutedEventArgs e)
        {
            
            WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
            Expander Target = sender as Expander;
            if (Target.IsExpanded == true)
            {
                MoreLocation = new List<object>();
                TextBlock TitleTB0 = new TextBlock();
                TitleTB0.Text = "Location History";
                TitleTB0.FontSize = 24;
                TitleTB0.Foreground = new SolidColorBrush(Colors.Gray);
                TitleTB0.Margin = new Thickness(-60,80, 0, 10);
                TitleTB0.VerticalAlignment = VerticalAlignment.Top;
                TitleTB0.HorizontalAlignment = HorizontalAlignment.Left;
                MainTemp.Children.Add(TitleTB0);
                MoreLocation.Add(TitleTB0);
                Image InfoImage1 = new System.Windows.Controls.Image();
                InfoImage1.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_info-blog_46810.ico"));
                InfoImage1.Width = 30;
                InfoImage1.Height = 30;
                InfoImage1.VerticalAlignment = VerticalAlignment.Top;
                InfoImage1.HorizontalAlignment = HorizontalAlignment.Left;
                InfoImage1.Margin = new Thickness(-170, 150, 0,  20);
                MainTemp.Children.Add(InfoImage1);
                MoreLocation.Add(InfoImage1);
                TextBlock InfoTb1 = new TextBlock();
                InfoTb1.Text = "For show location please Select one location from blow list";
                InfoTb1.FontSize = 16;
                InfoTb1.VerticalAlignment = VerticalAlignment.Top;
                InfoTb1.Foreground = new SolidColorBrush(Colors.Black);
                InfoTb1.Margin = new Thickness(-140, 180, 0, 0);
                MainTemp.Children.Add(InfoTb1);
                MoreLocation.Add(InfoTb1);
                LocationSemapore.WaitOne();
                if (MainWindow.DS.Tables.Contains("Location") == true)
                {
                    MainWindow.DS.Tables.Remove("Location");
                }
                MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From Location where ChildID ='" + ChildsCombo.SelectedItem.ToString() + "' order by Date Desc", ref MainWindow.DS, "Location");
                double[] ColumnWidth = { 200, 300, 300 };
                string[] HeadersName = { "Date", "Latitude", "Longitude" };
                string[] ColumnsName = { "Date", "Latitude", "Longitude" };
                ColorList NetworkList = new ColorList();
                Size ItemSize = new Size(0, 25);
                Size ListSize = new Size(802, 0);
                Border LocationListBorder = NetworkList.Draw(ColumnWidth, 30, HeadersName, null, ListSize, ItemSize, Color.FromArgb(255, 238, 241, 245), Colors.Transparent,
                    Colors.White, Colors.Black, Color.FromArgb(255, 42, 180, 192), Colors.LightGray, Colors.Transparent, MainWindow.DS.Tables["Location"],
                    new Thickness(100, 20, 15, 30), new Thickness(0), 12, 16, ColumnsName, LocationSelectionChange, "LocationHistoryList",
                   300);
                MainTemp.Children.Add(LocationListBorder);
                MoreLocation.Add(LocationListBorder);
                MainWindow.DS.Tables.Remove("Location");
                LocationSemapore.Release();
                History.VerticalAlignment = VerticalAlignment.Top;

            }
            else
            {
                foreach( object var in MoreLocation)
                {
                    MainTemp.Children.Remove((UIElement)var);
                }
                
                History.VerticalAlignment = VerticalAlignment.Bottom;
            }
        }

        private void LocationSelectionChange(object sender)
        {
            MainDataGrid.Dispatcher.Invoke(() =>
            {
                Border List = sender as Border;
                WrapPanel MainTemp = (WrapPanel)((Border)MainDataGrid.Children[1]).Child;
                WebBrowser Target = (WebBrowser)MainTemp.Children.Cast<UIElement>().First(x => x.Uid == "Google");
                string Cordinate = ((TextBlock)((Border)((StackPanel)List.Child).Children[1]).Child).Text + "," + ((TextBlock)((Border)((StackPanel)List.Child).Children[2]).Child).Text;
                Target.Source = new Uri("http://maps.google.com/maps?q=loc:" + Cordinate, UriKind.Absolute);
            });
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(this.WindowState != System.Windows.WindowState.Maximized)
            {
                OldWindowsState = this.WindowState;
                this.WindowState = System.Windows.WindowState.Maximized;
                MaxBtn.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/minimizered.png")));

            }
            else
            {
                this.WindowState = OldWindowsState;
                MaxBtn.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/maximize.png")));
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
        }

        private void MainW_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void MainDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Grid GridTarget = sender as Grid;
            BtnTemp = MBtn.Template;
            TextBlock Target = (TextBlock)BMain.Children.Cast<UIElement>().First(x => x.Uid == "PageTitle");
            Target.Margin = new Thickness((GridTarget.ActualWidth / 2) - 60, 0, 0, 0);
            
        }

        private void MainW_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        public static  string SelectColor()
        {
            PropertyInfo[] ColorArray = typeof(Colors).GetProperties();
            Random Rand = new Random();
            int Index = Rand.Next(141);
            string ColorName = "";
            try
            {
                ColorName = MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select * From UsedColor where Name ='" + ColorArray[Index].Name + "'").ToString();
            }
            catch (Exception E)
            {

            }
            
            while (ColorName == "")
            {
                Index = Rand.Next(141);
                try
                {
                    MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select * From UsedColor where Name ='" + ColorArray[Index].Name + "'").ToString();
                }
                catch(Exception E)
                {
                    ColorName = ColorArray[Index].Name;
                }
                
            }
            MainWindow.DataBaseAgent.ExequteWithCommand("insert into UsedColor values ('" + ColorArray[Index].Name + "')");
            if(ColorName == "")
            {
                ColorName = ColorArray[Index].Name;
            }
            return ColorName;
        }

        private void MainW_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public class ListItems
        {
            public Image ItemImage;
            public Border Bord;
            public Label Web;
            public ListItems(Uri PicAddress)
            {
                ItemImage = new Image();
                ItemImage.Source = new BitmapImage(PicAddress);
            }

        }
        public class RectDataWindows
        {
            public Border Bord;
            public TextBlock Name;
            public Image Pic;
            public TextBlock NewData;
            public TextBlock TotalTB;
            public Border Initial(string NameData , string NewDataString  ,string TotalData , Uri PicAddress )
            {
                Grid BgGrid = new Grid();
                Bord = new Border();
                Name = new TextBlock();
                Pic = new Image();
                Pic.Source = new BitmapImage(PicAddress);
                NewData = new TextBlock();
                TotalTB = new TextBlock();
                Bord.Background = new SolidColorBrush(Colors.White);
                Name.Text = NameData;
                Name.FontSize = 16;
                Name.FontWeight = FontWeights.Bold;
                Name.Foreground = new SolidColorBrush(Colors.LightGray);
                Name.VerticalAlignment = VerticalAlignment.Top;
                Name.HorizontalAlignment = HorizontalAlignment.Left;
                Name.Margin = new Thickness(10, 8, 0, 0);
                Pic.HorizontalAlignment = HorizontalAlignment.Left;
                Pic.VerticalAlignment = VerticalAlignment.Top;
                Pic.Width = 40;
                Pic.Height = 40;
                Pic.Margin = new Thickness(10, 50, 0, 0);
                NewData.Text = "+" + NewDataString;
                NewData.Foreground = new SolidColorBrush(Colors.Black);
                NewData.FontSize = 18;
                NewData.FontWeight = FontWeights.Bold;
                NewData.HorizontalAlignment = HorizontalAlignment.Left;
                NewData.VerticalAlignment = VerticalAlignment.Top;
                NewData.Margin = new Thickness(60, 55, 0, 0);
                TotalTB.Text = TotalData + "Total";
                TotalTB.Foreground = new SolidColorBrush(Colors.LightGray);
                TotalTB.FontSize = 10;
                Bord.Width = 190;
                Bord.Height = 100;
                TotalTB.HorizontalAlignment = HorizontalAlignment.Left;
                TotalTB.VerticalAlignment = VerticalAlignment.Top;
                TotalTB.Margin = new Thickness(60, 75, 0, 0);
                BgGrid.Children.Add(Name);
                BgGrid.Children.Add(Pic);
                BgGrid.Children.Add(NewData);
                BgGrid.Children.Add(TotalTB);
                Bord.Child = BgGrid;
                return Bord;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(MenueCol.MaxWidth == 50)
            {
                MenueCol.MaxWidth = 220;
            }
            else
            {
                MenueCol.MaxWidth = 50;
            }
            
        }

        public class VoiceRect
        {
            public Border OutBorder;
            public TextBlock AppTextBlok;
            public TextBlock DateTextBlock;
            public Button PlayBtn;
            public Line SepratorLine;
            Color MoseOverColor;
            public Border Draw(string AppName , string Date,bool LeftToRight)
            {
                OutBorder = new Border();
                AppTextBlok = new TextBlock();
                DateTextBlock = new TextBlock();
                PlayBtn = new Button();
                SepratorLine = new Line();
                OutBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 211, 215, 233));
                OutBorder.BorderThickness = new Thickness(2);
                OutBorder.Background = new SolidColorBrush(Color.FromArgb(255,245,246,250));
                OutBorder.Margin = new Thickness(20, 0, 0, 0);
                OutBorder.Width = 550;
                OutBorder.Height = 110;
                Grid wrapPanel = new Grid();
                wrapPanel.Background = new SolidColorBrush(Colors.Transparent);
                wrapPanel.Margin = new Thickness(0);
                if(LeftToRight == true)
                {
                    PlayBtn.Cursor = Cursors.Hand;
                    wrapPanel.FlowDirection = FlowDirection.LeftToRight;
                    AppTextBlok.Margin = new Thickness(20, 10, 0, 0);
                    PlayBtn.HorizontalAlignment = HorizontalAlignment.Right;
                    PlayBtn.Margin = new Thickness(20, -60, 20, 0);
                    DateTextBlock.Margin = new Thickness(20, 80, 0, 0);
                    SepratorLine.HorizontalAlignment = HorizontalAlignment.Left;
                    
                }
                else
                {
                    wrapPanel.FlowDirection = FlowDirection.RightToLeft;
                    PlayBtn.FlowDirection = FlowDirection.LeftToRight;
                    DateTextBlock.FlowDirection = FlowDirection.LeftToRight;
                    DateTextBlock.Margin = new Thickness(20, 80, 20, 0);
                    SepratorLine.FlowDirection = FlowDirection.LeftToRight;
                    SepratorLine.HorizontalAlignment = HorizontalAlignment.Right;
                    PlayBtn.Cursor = Cursors.Hand;
                }
                AppTextBlok.Foreground = new SolidColorBrush(Colors.Black);
                AppTextBlok.FontSize = 16;
                AppTextBlok.Margin = new Thickness(20, 10, 0, 0);
                PlayBtn.HorizontalAlignment = HorizontalAlignment.Right;
                PlayBtn.Margin = new Thickness(20, -60, 20, 0);
                AppTextBlok.FontWeight = FontWeights.Bold;
                AppTextBlok.Text = AppName;
                AppTextBlok.Margin = new Thickness(20, 10, 0, 0);
                wrapPanel.Children.Add(AppTextBlok);
                PlayBtn.HorizontalAlignment = HorizontalAlignment.Right;
                PlayBtn.Margin = new Thickness(20, -60, 20, 0);
                PlayBtn.BorderBrush = new SolidColorBrush(Colors.Transparent);
                PlayBtn.BorderThickness = new Thickness(0);
                PlayBtn.Template = Main.BtnTemp;
                ImageBrush Image = new ImageBrush();
                Image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/1495055-64.png"));
                PlayBtn.Background = Image;
                PlayBtn.Width = 30;
                PlayBtn.Height = 30;
                PlayBtn.Click += PlayBtn_Click;
                wrapPanel.Children.Add(PlayBtn);
                SepratorLine.Margin = new Thickness(20, 30, 20, 0);
                SepratorLine.VerticalAlignment = VerticalAlignment.Top;
                
                SepratorLine.Margin = new Thickness(10, 55, 15, 0);
                SepratorLine.Stroke = new SolidColorBrush(Color.FromArgb(255, 211, 215, 233));
                SepratorLine.StrokeThickness = 1;
                SepratorLine.X1 = 5;
                SepratorLine.Y1 = 10;
                SepratorLine.X2 = 500;
                SepratorLine.Y2 = 10;
                wrapPanel.Children.Add(SepratorLine);
                DateTextBlock.Foreground = new SolidColorBrush(Colors.LightBlue);
                DateTextBlock.FontSize = 12;
                DateTextBlock.FontWeight = FontWeights.Bold;
                DateTextBlock.Text = Date;
                wrapPanel.Children.Add(DateTextBlock);
                OutBorder.Child = wrapPanel;
                return OutBorder;
            }

            private void PlayBtn_Click(object sender, RoutedEventArgs e)
            {
                Button Target = sender as Button;
                if(VoiceBtn != null && VoiceBtn != Target)
                {
                    try
                    {
                        MP.Pause();
                    }
                    catch(Exception EE)
                    {

                    }
                    ImageBrush ImagePlay = new ImageBrush();
                    ImagePlay.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/1495055-64.png"));
                    VoiceBtn.Background = ImagePlay;
                }
                if (MediaIsPlay == true)
                {
                    MP.Pause();
                    VoiceBtn = null;
                    ImageBrush ImagePlay = new ImageBrush();
                    ImagePlay.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/1495055-64.png"));
                    Target.Background = ImagePlay;
                }
                else
                {
                    VoiceBtn = Target;
                    MediaIsPlay = true;
                    string Date = ((TextBlock)((Grid)Target.Parent).Children[3]).Text;
                    VoiceSemaphore.WaitOne();
                    string Row = (string)MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select VoiceData From Voice where ChildID ='" + ChildUser + "' And Date='"+ Date + "'");
                    VoiceSemaphore.Release();
                    byte[] SoundData = Convert.FromBase64String((string)Row);
                    File.WriteAllBytes(System.AppDomain.CurrentDomain.BaseDirectory + Convert.ToDateTime(Date).Millisecond + "Sound.mp3", SoundData);
                    MP = new MediaPlayer();
                    MP.Open(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + Convert.ToDateTime(Date).Millisecond + "Sound.mp3"));
                    ImageBrush ImagePuse = new ImageBrush();
                    ImagePuse.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/1495056-64.png"));
                    Target.Background = ImagePuse;
                    MP.Play();
                    MP.MediaEnded += MP_MediaEnded;
                }
                
            }

            private void MP_MediaEnded(object sender, EventArgs e)
            {
                
            }
        }

        public class ColorList
        {
            Color ItemsForeground;
            int Number = 0;
            Color ListItemHover;
            StackPanel ItemListStackPanel;
            Size ItemSize;
            int ItemFontSize;
            double[] ColumnWidth;
            Color ItemBorder;
            Color FirstColor;
            string[] ColumnNames;
            Color SecondColor;
            Action<object> Event;
            bool UnSelect = false;
            public int Count = 0; 
            public int SelectedIndex = -1;
            public void RemoveAt(int index)
            {
                ItemListStackPanel.Children.RemoveAt(index);
                Count--;
                SelectedIndex = -1;
            }
            public string[] GetDataAt(int Index)
            {
                string[] Data = new string[ColumnNames.Length];
                for(int i= 0; i < ColumnNames.Length; i++)
                {
                    Data[i] = ((TextBlock)((Border)((StackPanel)((Border)ItemListStackPanel.Children[Index]).Child).Children[i]).Child).Text.ToString();
                }
                
                return Data;
            }
            public void Clear()
            {
                
                ItemListStackPanel.Children.Clear();
                SelectedIndex = -1;
                Count = 0;
            }
            public void AddNewItem(object Data)
            {
                Border border = new Border();
                border.MouseEnter += ListBorder_MouseEnter;
                border.MouseLeave += ListBorder_MouseLeave;
                if (Number % 2 != 0)
                {
                    border.Background = new SolidColorBrush(FirstColor);
                }
                else
                {
                    border.Background = new SolidColorBrush(SecondColor);
                }
                border.BorderBrush = new SolidColorBrush(ItemBorder);
                border.BorderThickness = new Thickness(0.2, 0.2, 0.2, 0.2);
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                border.MouseLeftButtonUp += Border_MouseLeftButtonUp1;
                for (int j = 0; j < ColumnNames.Length; j++)
                {
                    Border NewBorder = new Border();
                    NewBorder.Width = ColumnWidth[j];
                    NewBorder.Height = ItemSize.Height;
                    NewBorder.BorderBrush = new SolidColorBrush(ItemBorder);
                    NewBorder.BorderThickness = new Thickness(0.2, 0, 0.2, 0);
                    NewBorder.Background = new SolidColorBrush(Colors.Transparent);
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = ((DataRow)Data)[ColumnNames[j]].ToString();
                    textBlock.Margin = new Thickness(5, 8, 0, 0);
                    textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                    textBlock.FontSize = ItemFontSize;
                    textBlock.Foreground = new SolidColorBrush(ItemsForeground);
                    NewBorder.Child = textBlock;
                    NewBorder.Margin = new Thickness(0.2, 0, 0.2, 0);
                    stackPanel.Children.Add(NewBorder);
                }
                border.Child = stackPanel;
                ItemListStackPanel.Children.Add(border);
                Number++;
                Count++;
            }
            public Border Draw(double [] ColumnWidth ,double ColumnHeight, string [] Column  , string [] ItemImages, Size ListSize , Size ItemSize,
                Color FirstColor , Color SecondColor , Color HeadersForeground , Color ItemsForeground , Color HeaderBackGround , 
                Color ItemBorder , Color ListBorder , DataTable ItemData , Thickness ListMargin , Thickness ListBorderSize ,
                int ItemFontSize , int HeaderFontSize , string[]ColumnNames , Action<object> SelectionChangeEvent , string UID , double MaxHeight )
            {
                Event = SelectionChangeEvent;
                Border ListOuterBorder = new Border();
                this.FirstColor = FirstColor;
                this.SecondColor = SecondColor;
                this.ColumnNames = ColumnNames;
                this.ItemBorder = ItemBorder;
                this.ItemSize = ItemSize;
                this.ColumnWidth = ColumnWidth;
                this.ItemsForeground = ItemsForeground;
                this.ItemFontSize = ItemFontSize;
                ListOuterBorder.Width = ListSize.Width;
                ListOuterBorder.MaxHeight = MaxHeight;
                ListOuterBorder.Background = new SolidColorBrush(Colors.Transparent);
                ListOuterBorder.Margin = ListMargin;
                ListOuterBorder.BorderBrush = new SolidColorBrush(ListBorder);
                ListOuterBorder.BorderThickness = ListBorderSize;
                if(UID != null)
                {
                    ListOuterBorder.Uid = UID;
                }
                StackPanel MainStackPanel = new StackPanel();
                StackPanel HeaderStackPanel = new StackPanel();
                HeaderStackPanel.Orientation = Orientation.Horizontal;
                for(int i = 0; i < ColumnNames.Length; i++)
                {
                    Border HeaderBorder = new Border();
                    HeaderBorder.Background = new SolidColorBrush(HeaderBackGround);
                    HeaderBorder.Height = ColumnHeight;
                    HeaderBorder.Width = ColumnWidth[i];
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = Column[i];
                    textBlock.Margin = new Thickness(5, 5, 0, 0);
                    textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                    textBlock.FontSize = HeaderFontSize;
                    textBlock.Foreground = new SolidColorBrush(HeadersForeground);
                    HeaderBorder.Child = textBlock;
                    HeaderBorder.Margin = new Thickness(0.5, 0, 0.5, 0);
                    HeaderStackPanel.Children.Add(HeaderBorder);
                }
                
                MainStackPanel.Children.Add(HeaderStackPanel);
                ScrollViewer Scroll = new ScrollViewer();
                Scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                ItemListStackPanel = new StackPanel();
                for (int i = 0; ItemData != null &&  i < ItemData.Rows.Count; i++)
                {
                    Count++;
                    Border border = new Border();
                    border.MouseEnter += ListBorder_MouseEnter;
                    border.MouseLeave += ListBorder_MouseLeave;
                    if (i % 2 != 0)
                    {
                        border.Background = new SolidColorBrush(FirstColor);
                    }
                    else
                    {
                        border.Background = new SolidColorBrush(SecondColor);
                    }
                    border.BorderBrush = new SolidColorBrush(ItemBorder);
                    border.BorderThickness = new Thickness(0.2, 0.2, 0.2, 0.2);
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    border.MouseLeftButtonUp += Border_MouseLeftButtonUp1;
                    bool ISNull = true;
                    for (int j = 0; j < ColumnNames.Length; j++)
                    {
                        if(ItemData.Rows[i][ColumnNames[j]].ToString()!= "")
                        {
                            ISNull = false;
                        }
                        Border NewBorder = new Border();
                        NewBorder.Width = ColumnWidth[j];
                        NewBorder.Height = ItemSize.Height;
                        NewBorder.BorderBrush = new SolidColorBrush(ItemBorder);
                        NewBorder.BorderThickness = new Thickness(0.2, 0, 0.2, 0);
                        NewBorder.Background = new SolidColorBrush(Colors.Transparent);
                        TextBlock textBlock = new TextBlock();
                        textBlock.Text = ItemData.Rows[i][ColumnNames[j]].ToString();
                        textBlock.Margin = new Thickness(5, 8, 0, 0);
                        textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                        textBlock.FontSize = ItemFontSize;
                        textBlock.Foreground = new SolidColorBrush(ItemsForeground);
                        NewBorder.Child = textBlock;
                        NewBorder.Margin = new Thickness(0.2, 0, 0.2, 0);
                        stackPanel.Children.Add(NewBorder);
                    }
                    if(ISNull == true)
                    {
                        stackPanel.Children.RemoveAt(stackPanel.Children.Count - 1);
                    }
                    border.Child = stackPanel;
                    ItemListStackPanel.Children.Add(border);
                    Number++;
                }
                Scroll.MaxHeight = MaxHeight;
                Scroll.Content = ItemListStackPanel;
                MainStackPanel.Children.Add(Scroll);
                ListOuterBorder.Child = MainStackPanel;
                return ListOuterBorder;
            }

            public Border DrawWithCombo(double[] ColumnWidth, double ColumnHeight, string[] Column, string[] ItemImages, Size ListSize, Size ItemSize,
                Color FirstColor, Color SecondColor, Color HeadersForeground, Color ItemsForeground, Color HeaderBackGround,
                Color ItemBorder, Color ListBorder, ItemCollection ItemData, Thickness ListMargin, Thickness ListBorderSize,
                int ItemFontSize, int HeaderFontSize, string[] ColumnNames, SelectionChangedEventHandler SelectionChangeEvent, string UID, double MaxHeight , List<string> AllPosibleComboData,
                List<string> DefaultComboData , out List<ComboBox> AllCombo)
            {
                Border ListOuterBorder = new Border();
                AllCombo = new List<ComboBox>();
                this.FirstColor = FirstColor;
                this.SecondColor = SecondColor;
                this.ColumnNames = ColumnNames;
                this.ItemBorder = ItemBorder;
                this.ItemSize = ItemSize;
                this.ColumnWidth = ColumnWidth;
                this.ItemsForeground = ItemsForeground;
                this.ItemFontSize = ItemFontSize;
                ListOuterBorder.Width = ListSize.Width;
                ListOuterBorder.MaxHeight = MaxHeight;
                ListOuterBorder.Background = new SolidColorBrush(Colors.Transparent);
                ListOuterBorder.Margin = ListMargin;
                ListOuterBorder.BorderBrush = new SolidColorBrush(ListBorder);
                ListOuterBorder.BorderThickness = ListBorderSize;
                if (UID != null)
                {
                    ListOuterBorder.Uid = UID;
                }
                StackPanel MainStackPanel = new StackPanel();
                StackPanel HeaderStackPanel = new StackPanel();
                HeaderStackPanel.Orientation = Orientation.Horizontal;
                for (int i = 0; i < ColumnNames.Length; i++)
                {
                    Border HeaderBorder = new Border();
                    HeaderBorder.Background = new SolidColorBrush(HeaderBackGround);
                    HeaderBorder.Height = ColumnHeight;
                    HeaderBorder.Width = ColumnWidth[i];
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = Column[i];
                    textBlock.Margin = new Thickness(5, 5, 0, 0);
                    textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                    textBlock.FontSize = HeaderFontSize;
                    textBlock.Foreground = new SolidColorBrush(HeadersForeground);
                    HeaderBorder.Child = textBlock;
                    HeaderBorder.Margin = new Thickness(0.5, 0, 0.5, 0);
                    HeaderStackPanel.Children.Add(HeaderBorder);
                }

                MainStackPanel.Children.Add(HeaderStackPanel);
                ScrollViewer Scroll = new ScrollViewer();
                Scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                ItemListStackPanel = new StackPanel();
                for (int i = 0; ItemData != null && i < ItemData.Count; i++)
                {
                    Count++;
                    Border border = new Border();
                    border.MouseEnter += ListBorder_MouseEnter;
                    border.MouseLeave += ListBorder_MouseLeave;
                    if (i % 2 != 0)
                    {
                        border.Background = new SolidColorBrush(FirstColor);
                    }
                    else
                    {
                        border.Background = new SolidColorBrush(SecondColor);
                    }
                    border.BorderBrush = new SolidColorBrush(ItemBorder);
                    border.Height = 30;
                    border.BorderThickness = new Thickness(0.2, 0.2, 0.2, 0.2);
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    border.MouseLeftButtonUp += Border_MouseLeftButtonUp1;
                    bool ISNull = true;
                    for (int j = 0; j < ColumnNames.Length; j++)
                    {
                        if (ItemData[j].ToString() != "")
                        {
                            ISNull = false;
                        }
                        Border NewBorder = new Border();
                        NewBorder.Width = ColumnWidth[j];
                        NewBorder.Height = ItemSize.Height;
                        NewBorder.BorderBrush = new SolidColorBrush(ItemBorder);
                        NewBorder.BorderThickness = new Thickness(0.2, 0, 0.2, 0);
                        NewBorder.Background = new SolidColorBrush(Colors.Transparent);
                        if (j == 0)
                        {
                            TextBlock textBlock = new TextBlock();
                            textBlock.Text = ItemData[i].ToString();
                            textBlock.Margin = new Thickness(5, 3, 0, 0);
                            textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                            textBlock.FontSize = ItemFontSize;
                            textBlock.Foreground = new SolidColorBrush(ItemsForeground);
                            NewBorder.Child = textBlock;
                        }
                        else
                        {
                            ComboBox Combo = new ComboBox();
                            Combo.Width = ColumnWidth[j] - 15;
                            if (DefaultComboData != null )
                            {
                                Combo.SelectedItem = DefaultComboData[i];
                            }
                            foreach(string var in AllPosibleComboData)
                            {
                                Combo.Items.Add(var);
                            }
                            Combo.Margin = new Thickness(5, 3, 5, 0);
                            Combo.HorizontalAlignment = HorizontalAlignment.Center;
                            Combo.FontSize = ItemFontSize;
                            Combo.SelectionChanged += SelectionChangeEvent;
                            Combo.Foreground = new SolidColorBrush(ItemsForeground);
                            NewBorder.Child = Combo;
                            AllCombo.Add(Combo);
                        }
                        
                        
                        NewBorder.Margin = new Thickness(0.2, 0, 0.2, 0);
                        stackPanel.Children.Add(NewBorder);
                    }
                    if (ISNull == true)
                    {
                        stackPanel.Children.RemoveAt(stackPanel.Children.Count - 1);
                    }
                    border.Child = stackPanel;
                    ItemListStackPanel.Children.Add(border);
                    Number++;
                }
                Scroll.MaxHeight = MaxHeight;
                Scroll.Content = ItemListStackPanel;
                MainStackPanel.Children.Add(Scroll);
                ListOuterBorder.Child = MainStackPanel;
                return ListOuterBorder;
            }

            private void Border_MouseLeftButtonUp1(object sender, MouseButtonEventArgs e)
            {
                Border Target = sender as Border;
                if(((StackPanel)Target.Parent).Children.IndexOf((UIElement)Target) == SelectedIndex)
                {
                    UnSelect = true;
                    if(SelectedIndex %2 == 0)
                    {
                        Target.Background = new SolidColorBrush(SecondColor);
                    }
                    else
                    {
                        Target.Background = new SolidColorBrush(FirstColor);
                    }
                    SelectedIndex = -1;
                }
                else
                {
                    UnSelect = false;
                    if(SelectedIndex > -1)
                    {
                        if (SelectedIndex % 2 == 0)
                        {
                            ((Border)((StackPanel)Target.Parent).Children[SelectedIndex]).Background = new SolidColorBrush(SecondColor);
                        }
                        else
                        {
                            ((Border)((StackPanel)Target.Parent).Children[SelectedIndex]).Background = new SolidColorBrush(FirstColor);
                        }
                        
                    }
                    SelectedIndex = ((StackPanel)Target.Parent).Children.IndexOf((UIElement)Target);
                    Target.Background = new SolidColorBrush(Colors.Cornsilk);
                }
                Task.Run(() =>
                {
                    if(Event != null)
                    {
                        Event.Invoke(Target);
                    }
                    
                });
                
            }

            private void ListBorder_MouseLeave(object sender, MouseEventArgs e)
            {
                if(UnSelect== false)
                {
                    Border border = sender as Border;
                    if (SelectedIndex == ((StackPanel)border.Parent).Children.IndexOf((UIElement)border))
                    {

                    }
                    else
                    {
                        border.Background = new SolidColorBrush(ListItemHover);
                    }
                }
                else
                {
                    UnSelect = false;
                }
                
                
            }

            private void ListBorder_MouseEnter(object sender, MouseEventArgs e)
            {
                Border border = sender as Border;
                if (SelectedIndex == ((StackPanel)border.Parent).Children.IndexOf((UIElement)border))
                {

                }
                else
                {
                    ListItemHover = ((SolidColorBrush)border.Background).Color;
                    border.Background = new SolidColorBrush(Colors.LightGray);
                }
                
            }
        }

        private void Panel_Loaded(object sender, RoutedEventArgs e)
        {
            MouseButtonEventArgs E = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left);
            E.RoutedEvent = StackPanel.MouseLeftButtonDownEvent;
            Panel.Children[0].RaiseEvent(E);
        }

        private void SettingBtn_Click(object sender, RoutedEventArgs e)
        {
            MainDataGrid.Children.Clear();
            MainDataGrid.ColumnDefinitions.Clear();
            MainDataGrid.RowDefinitions.Clear();
            TextBlock TitleTB2 = new TextBlock();
            TitleTB2.Text = "Settings";
            TitleTB2.FontSize = 24;
            TitleTB2.Foreground = new SolidColorBrush(Colors.Gray);
            TitleTB2.Margin = new Thickness(20, -30, 0, 10);
            TitleTB2.VerticalAlignment = VerticalAlignment.Top;
            TitleTB2.HorizontalAlignment = HorizontalAlignment.Left;
            MainDataGrid.Children.Add(TitleTB2);
            WrapPanel MainTemp = new WrapPanel();
            MainTemp.Orientation = Orientation.Vertical;
            MainTemp.Margin = new Thickness(0);
            MainTemp.HorizontalAlignment = HorizontalAlignment.Left;
            Border BG = new Border();
            BG.Background = new SolidColorBrush(Colors.White);
            BG.Margin = new Thickness(20, 20, 20, 50);
            BG.VerticalAlignment = VerticalAlignment.Top;
            BG.Child = MainTemp;
            MainDataGrid.Children.Add(BG);
            double[] ColumnWidth = { 250, 350};
            string[] HeadersName = { "Child ID", "License" };
            string[] ColumnsName = { "ID", "LicenseID" };
            ColorList NetworkList = new ColorList();
            Size ItemSize = new Size(0, 25);
            Size ListSize = new Size(600, 0);
            DataSet LicenseDS = new DataSet();
            MainWindow.DataBaseAgent.SelectData("License", ref LicenseDS, "License");
            List<string> SelectedLicense = new List<string>();
            for(int i = 0; i < ChildsCombo.Items.Count; i++)
            {
                SelectedLicense.Add("");
            }
            SelectedLicense.Add("");
            List<string> AllLicense = new List<string>();
            
            foreach(DataRow Data in LicenseDS.Tables[0].Rows)
            {
                AllLicense.Add(Data["ID"].ToString());
                if(Data["ChildID"] != null && Data["ChildID"].ToString() !="")
                {
                    int Index = -1;
                    try
                    {
                        Index = ChildsCombo.Items.IndexOf(Data["ChildID"].ToString());
                    }
                    catch(Exception E)
                    {

                    }
                    if(Index > -1)
                    {
                        SelectedLicense[Index] = Data["ID"].ToString();
                    }
                }
            }
            SelectedLicense.Add("");
            List<string> Childs = new List<string>();
            AllCombo = new List<ComboBox>();
            Border border = NetworkList.DrawWithCombo(ColumnWidth, 30, HeadersName, null, ListSize, ItemSize, Color.FromArgb(255, 238, 241, 245), Colors.Transparent,
                Colors.White, Colors.Black, Color.FromArgb(255, 42, 180, 192), Colors.LightGray, Colors.Transparent, ChildsCombo.Items,
                new Thickness(50, 30, 15, 20), new Thickness(0), 12, 16, ColumnsName, LicenseChanged_SelectionChanged, "ChilderenList", 300, AllLicense, SelectedLicense, out AllCombo);
            border.VerticalAlignment = VerticalAlignment.Top;
            border.HorizontalAlignment = HorizontalAlignment.Left;
            MainTemp.Children.Add(border);
            double[] LicenseColumnWidth = { 250, 350 };
            string[] LicenseHeadersName = { "Child ID", "License" };
            string[] LicenseColumnsName = { "ID", "LicenseID" };
            ColorList LicenseList = new ColorList();
            Size LicenseItemSize = new Size(0, 25);
            Size LicenseListSize = new Size(1100, 0);
            Border Licenseborder = LicenseList.Draw(LicenseColumnWidth, 30, LicenseHeadersName, null, LicenseListSize, LicenseItemSize, Color.FromArgb(255, 238, 241, 245), Colors.Transparent,
                Colors.White, Colors.Black, Color.FromArgb(255, 42, 180, 192), Colors.LightGray, Colors.Transparent, null,
                new Thickness(50, 30, 15, 20), new Thickness(0), 12, 16, LicenseColumnsName, null, "ChilderenList", 300);
            Licenseborder.VerticalAlignment = VerticalAlignment.Top;
            Licenseborder.HorizontalAlignment = HorizontalAlignment.Left;
            MainTemp.Children.Add(Licenseborder);
        }

        private void LicenseChanged_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Target = sender as ComboBox;
            if(Target.SelectedItem != null && Target.SelectedItem.ToString() != "")
            {
                int index = AllCombo.IndexOf(Target);
                for(int i= 0; i < AllCombo.Count ; i++)
                {
                    if(i == index)
                    {
                        continue;
                    }
                    if(AllCombo[i].SelectedItem != null && Target.SelectedItem.ToString() == AllCombo[i].SelectedItem.ToString())
                    {
                        Target.SelectedItem = null;
                    }
                }
            }
        }
   
    }
}

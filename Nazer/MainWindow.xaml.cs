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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.IO;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Window LoginPage;
        public static Main MyMain;
        public static DataBaseHandler DataBaseAgent;
        public static DataSet DS;
        public static ConnectionClass Connection;
        Brush OldBrush;
        Brush OldTextBrush;
        public MainWindow()
        {
            InitializeComponent();
            LoginPage = this;
            DataBaseAgent = new DataBaseHandler(@"Data Source=.\sqlexpress;Initial Catalog=ParentDB;Integrated Security=True", 10);
            if(DataBaseAgent.CheckDataBase(@"Data Source=.\sqlexpress;Initial Catalog=ParentDB;Integrated Security=True") != true)
            {
                GenerateDatabase();
            }
            DS = new DataSet();
            DataBaseAgent.SelectData("Data", ref DS, "Data");
            //DataBaseAgent.SelectData("InstalledApps", ref DS, "InstalledApps");
            //DataBaseAgent.SelectData("AppUsage", ref DS, "AppUsage");
            //DataBaseAgent.SelectData("RunningApps", ref DS, "RunningApps");
            //DataBaseAgent.SelectData("BlockUrls", ref DS, "BlockUrls");
            //DataBaseAgent.SelectData("HistoryURL", ref DS, "HistoryURL");
            //DataBaseAgent.SelectData("URLCategury", ref DS, "URLCategury");
            //DataBaseAgent.SelectData("SystemLimit", ref DS, "SystemLimit");
            //DataBaseAgent.SelectData("NetworkAdaptor", ref DS, "NetworkAdaptor");
            //DataBaseAgent.SelectData("Network", ref DS, "Network");
            //DataBaseAgent.SelectData("VPN", ref DS, "VPN");
            //DataBaseAgent.SelectData("AppsLimit", ref DS, "AppsLimit");
            //DataBaseAgent.SelectData("NetworkLimit", ref DS, "NetworkLimit");
            //DataBaseAgent.SelectData("Location", ref DS, "Location");
            //DataBaseAgent.SelectData("AppsLog", ref DS, "AppsLog");
            //DataBaseAgent.SelectData("AppCategory", ref DS, "AppCategory");
            
            if (DS.Tables["Data"].Rows.Count == 0)
            {
                DataRow Row = DS.Tables["Data"].NewRow();
                //Row["DataContent"] = "178.32.129.19";
                Row["DataContent"] = "127.0.0.1";
                Row["ID"] = 0;
                DS.Tables["Data"].Rows.Add(Row);
                Row = DS.Tables["Data"].NewRow();
                Row["DataContent"] = "8800,8801";
                Row["ID"] = 1;
                DS.Tables["Data"].Rows.Add(Row);
                Row = DS.Tables["Data"].NewRow();
                Row["DataContent"] = "";
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
                Row["DataContent"] = "0";
                Row["ID"] = 5;
                DS.Tables["Data"].Rows.Add(Row);
                Row = DS.Tables["Data"].NewRow();
                Row["DataContent"] = "8803";
                Row["ID"] = 6;
                DS.Tables["Data"].Rows.Add(Row);
                Row = DS.Tables["Data"].NewRow();
                Row["DataContent"] = "";
                Row["ID"] = 7; // Licenses
                DS.Tables["Data"].Rows.Add(Row);
                DataBaseAgent.InsertData(DS.Tables["Data"]);
            }
            UserTB.Text = "User name...";
            UserTB.Foreground = new SolidColorBrush(Colors.LightGray);
            PassTXTTB.Text = "Password...";
            PassTXTTB.Foreground = new SolidColorBrush(Colors.LightGray);
            
        }
        public void GenerateDatabase()
        {
            List<string> Commasnds = new List<string>();
            Commasnds.Add("USE [master]\r\n");
            Commasnds.Add("Create Database [ParentDB]\r\n");
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
                Command.Connection = new SqlConnection(@"Data source = .\sqlexpress; Initial Catalog=MASTER;Integrated security = true");
                Command.CommandType = CommandType.Text;
                Command.Connection.Open();
                for (int i = 0; i < Commasnds.Count; i++)
                {
                    Command.CommandText = Commasnds[i];
                    Command.ExecuteNonQuery();
                }
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            //Thread.Sleep(1000);
            Login.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                if (DS.Tables["Data"].Rows[5]["DataContent"].ToString() != "1")
                {
                    DS.Tables["Data"].Rows[2]["DataContent"] = UserTB.Text;
                    DS.Tables["Data"].Rows[3]["DataContent"] = Hash(PassTB.Password);
                }
                if((DS.Tables["Data"].Rows[2]["DataContent"].ToString() == UserTB.Text) && (Hash(DS.Tables["Data"].Rows[3]["DataContent"].ToString()) == Hash(PassTB.Password)))
                {
                    Connection = new ConnectionClass(DS.Tables["Data"].Rows[1]["DataContent"].ToString(), DS.Tables["Data"].Rows[0]["DataContent"].ToString(), 1024 * 4, 3, Connection_LoginEventHandler);
                    
                    
                }
                else
                {
                    Login.IsEnabled = true;
                    Mouse.OverrideCursor = null;
                    Result.Content = "Username or Password is incorrect !!!";
                    Result.Foreground = new SolidColorBrush(Colors.Red);
                }
                
            }
            catch(Exception E)
            {
                Login.IsEnabled = true;
                Mouse.OverrideCursor = null;
                if(Connection != null && Connection.ParentSocket != null)
                {
                    Monitor.Exit(Connection.ParentSocket);
                }
                
                //MessageBox.Show(E.Message);
            }
            
        }

        private void Connection_LoginEventHandler(object sender, string e)
        {
            Result.Content = "";
            //Monitor.Enter(Connection.ParentSocket);
            if (DS.Tables["Data"].Rows[5]["DataContent"].ToString() == "1")
            {
                switch(e)
                {
                    case "True":
                        {
                            RegistryKey Key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", RegistryKeyPermissionCheck.ReadWriteSubTree);
                            Key.SetValue("UI.exe", "11001", RegistryValueKind.DWord);
                            MyMain = new Main();
                            this.Close();
                            Login.IsEnabled = false;
                            Mouse.OverrideCursor = null;
                            MyMain.ShowDialog();
                        };break;
                    case "False":
                        {
                            Result.Dispatcher.Invoke(() =>
                            {
                                Result.Content = "Username or Password is incorrect !!!";
                                Result.Foreground = new SolidColorBrush(Colors.Red);
                                Login.IsEnabled = true;
                                Mouse.OverrideCursor = null;
                            });
                        };break;
                    case "Connection":
                        {
                            Result.Dispatcher.Invoke(() =>
                            {
                                Result.Content = "Connection To Server Failed !!!";
                                Result.Foreground = new SolidColorBrush(Colors.Red);
                                Login.IsEnabled = true;
                                Mouse.OverrideCursor = null;
                            });
                        };break;
                }
                /*if (e == "True")
                {
                    RegistryKey Key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", RegistryKeyPermissionCheck.ReadWriteSubTree);
                    Key.SetValue("UI.exe", "11001", RegistryValueKind.DWord);
                    MyMain = new Main();
                    this.Close();
                    Login.IsEnabled = false;
                    Mouse.OverrideCursor = null;
                    MyMain.ShowDialog();

                }
                else
                {
                    
                    Result.Dispatcher.Invoke(() =>
                    {
                        Result.Content = "Username or Password is incorrect !!!";
                        Result.Foreground = new SolidColorBrush(Colors.Red);
                        Login.IsEnabled = true;
                        Mouse.OverrideCursor = null;
                    });
                    
                }*/
                //System.Security.AccessControl.RegistrySecurity Sec = new System.Security.AccessControl.RegistrySecurity();
               
                //Key.SetValue("UI.exe", "00002af9", RegistryValueKind.DWord);

            }

            else 
            {

                switch (e)
                {
                    case "False":
                        {
                            Result.Dispatcher.Invoke(() =>
                            {
                                Result.Content = "Username or Password is incorrect !!!";
                                Result.Foreground = new SolidColorBrush(Colors.Red);
                                Login.IsEnabled = true;
                                Mouse.OverrideCursor = null;
                            });
                        }; break;
                    case "Connection":
                        {
                            Result.Dispatcher.Invoke(() =>
                            {
                                Result.Content = "Connection To Server Failed !!!";
                                Result.Foreground = new SolidColorBrush(Colors.Red);
                                Login.IsEnabled = true;
                                Mouse.OverrideCursor = null;
                            });
                        }; break;
                }

            }
            //Monitor.Exit(Connection.ParentSocket);
        }

        string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            byte[] hashBytes;
            using (var algorithm = new System.Security.Cryptography.SHA512Managed())
            {
                hashBytes = algorithm.ComputeHash(bytes);
            }
            return Convert.ToBase64String(hashBytes);
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);
            }
            
        }

        private void UserTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if(UserTB.Text == "User name...")
            {
                UserTB.Text = "";
                UserTB.Foreground = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void UserTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (UserTB.Text == "")
            {
                UserTB.Text = "User name...";
                UserTB.Foreground = new SolidColorBrush(Colors.LightGray);
            }
        }
        private void PassTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PassTXTTB.Text == "Password...")
            {
                PassTXTTB.Text = "";
                PassTB.Foreground = new SolidColorBrush(Colors.LightGray);
            }
        }
        private void PassTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PassTB.Password == "")
            {
                PassTXTTB.Text = "Password...";

                PassTXTTB.Foreground = new SolidColorBrush(Colors.LightGray);
            }
        }
        private void UserNameBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            OldBrush = UserNameBorder.BorderBrush;
            UserNameBorder.BorderBrush = new SolidColorBrush(Colors.Yellow);
        }

        private void UserNameBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            UserNameBorder.BorderBrush = OldBrush;
        }
        private void PasswordBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            OldBrush = PasswordBorder.BorderBrush;
            PasswordBorder.BorderBrush = new SolidColorBrush(Colors.Yellow);
        }

        private void PasswordBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            PasswordBorder.BorderBrush = OldBrush;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void LoginBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            OldBrush = LoginBtnBorder.BorderBrush;
            OldTextBrush = Login.Foreground;
            LoginBtnBorder.BorderBrush = new SolidColorBrush(Colors.Red);
            Login.Foreground = new SolidColorBrush(Colors.MidnightBlue);
        }

        private void LoginBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            LoginBtnBorder.BorderBrush = OldBrush;
            Login.Foreground = OldTextBrush;
        }

        private void Singup_MouseEnter(object sender, MouseEventArgs e)
        {
            OldBrush = SingUpBtnBorder.BorderBrush;
            OldTextBrush = Singup.Foreground;
            SingUpBtnBorder.BorderBrush = new SolidColorBrush(Colors.Red);
            Singup.Foreground = new SolidColorBrush(Colors.MidnightBlue);
        }

        private void Singup_MouseLeave(object sender, MouseEventArgs e)
        {
            SingUpBtnBorder.BorderBrush = OldBrush;
            Singup.Foreground = OldTextBrush;
        }

        private void Singup_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            SIngUpPage TempSingUp = new SIngUpPage();
            TempSingUp.Show();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

        }
    }
}

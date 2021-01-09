using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Net.Sockets;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Security.Cryptography;
using System.IO;

namespace UI
{
    /// <summary>
    /// Interaction logic for SIngUpPage.xaml
    /// </summary>
    public partial class SIngUpPage : Window
    {
        FieldStatus[] DataStatus = new FieldStatus[11];
        // 0 First name
        // 1 Last name
        // 2 Country
        // 3 Sex
        // 4 Email
        // 5 Phone
        // 6 Username
        // 7 Birth Date
        // 8 Password
        // 9 Confirm Password
        // 10 Agreement
        ConnectionClass Connection;
        string Password = "@bridleAdmin$0013579#برایدل علی مهدوی";
        byte[] PassBytes;
        byte[] ReciveData;

        public SIngUpPage()
        {
            InitializeComponent();
            for(int i = 0; i < DataStatus.Length; i++)
            {
                DataStatus[i] = FieldStatus.Empty;
            }
            PassBytes = Encoding.Unicode.GetBytes(Password);
            ReciveData = new byte[2048];
        }
        enum  FieldStatus
        {
            Empty,
            Correct,
            Faild
        }
        public  IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj)
       where T : DependencyObject
        {
            if (depObj != null)
            {
                
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public  childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {
            foreach (childItem child in FindVisualChildren<childItem>(obj))
            {
                return child;
            }

            return null;
        }

        private void BirthDateDP_Loaded(object sender, RoutedEventArgs e)
        {
            DatePicker Date = sender as DatePicker;
            System.Windows.Controls.Primitives.DatePickerTextBox DPTB = FindVisualChild<System.Windows.Controls.Primitives.DatePickerTextBox>(Date);
            if(DPTB != null)
            {
                ContentControl WateMark = DPTB.Template.FindName("PART_Watermark", DPTB) as ContentControl;
                if(WateMark != null)
                {
                    WateMark.Content = "Birth Date";
                }
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow.LoginPage.Show();
        }

        private void FirstNameTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FirstNameTB.Text != "First Name" && FirstNameTB.Text != "")
            {
                //FirstNameTB.Text = "";
                //FirstNameTB.Foreground = new SolidColorBrush(Colors.Black);
                DataStatus[0] = FieldStatus.Correct;
            }
        }

        private void FirstNameTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if(FirstNameTB.Text == "First Name")
            {
                FirstNameTB.Text = "";
                FirstNameTB.Foreground = new SolidColorBrush(Colors.Black);
                DataStatus[0] = FieldStatus.Empty;
            }
            
        }

        private void FirstNameTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if(FirstNameTB.Text == "")
            {
                FirstNameTB.Text = "First Name";
                FirstNameTB.Foreground = new SolidColorBrush(Colors.LightGray);
                DataStatus[0] = FieldStatus.Empty;
            }
            
        }

        private void LastNameTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LastNameTB.Text != "Last Name" && LastNameTB.Text != "")
            {
                //FirstNameTB.Text = "";
                //FirstNameTB.Foreground = new SolidColorBrush(Colors.Black);
                DataStatus[1] = FieldStatus.Correct;
            }
        }

        private void LastNameTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LastNameTB.Text == "Last Name")
            {
                LastNameTB.Text = "";
                LastNameTB.Foreground = new SolidColorBrush(Colors.Black);
                DataStatus[1] = FieldStatus.Empty;
            }
        }

        private void LastNameTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (LastNameTB.Text == "")
            {
                LastNameTB.Text = "Last Name";
                LastNameTB.Foreground = new SolidColorBrush(Colors.LightGray);
                DataStatus[1] = FieldStatus.Empty;
            }
        }
        private void EmailTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EmailTB.Text != "Email" && EmailTB.Text != "")
            {
                //FirstNameTB.Text = "";
                //FirstNameTB.Foreground = new SolidColorBrush(Colors.Black);
                DataStatus[4] = FieldStatus.Correct;
            }
        }

        private void EmailTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if (EmailTB.Text == "Email")
            {
                EmailTB.Text = "";
                EmailTB.Foreground = new SolidColorBrush(Colors.Black);
                DataStatus[4] = FieldStatus.Empty;
            }
        }

        private void EmailTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (EmailTB.Text == "")
            {
                EmailTB.Text = "Email";
                EmailTB.Foreground = new SolidColorBrush(Colors.LightGray);
                DataStatus[4] = FieldStatus.Empty;
            }
        }

        private void UserNameTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (UserNameTB.Text != "Username" && EmailTB.Text != "")
            {
                //FirstNameTB.Text = "";
                //FirstNameTB.Foreground = new SolidColorBrush(Colors.Black);
                DataStatus[6] = FieldStatus.Correct;
            }
        }

        private void UserNameTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UserNameTB.Text == "Username")
            {
                UserNameTB.Text = "";
                UserNameTB.Foreground = new SolidColorBrush(Colors.Black);
                DataStatus[6] = FieldStatus.Empty;
            }
        }

        private void UserNameTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (UserNameTB.Text == "")
            {
                UserNameTB.Text = "Username";
                UserNameTB.Foreground = new SolidColorBrush(Colors.LightGray);
                DataStatus[6] = FieldStatus.Empty;
            }
        }

        private void SexTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SexTB.Text != "Sex" && SexTB.Text != "")
            {
                //FirstNameTB.Text = "";
                //FirstNameTB.Foreground = new SolidColorBrush(Colors.Black);
                DataStatus[3] = FieldStatus.Correct;
            }
        }

        private void SexTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SexTB.Text == "Sex")
            {
                SexTB.Text = "";
                SexTB.Foreground = new SolidColorBrush(Colors.Black);
                DataStatus[3] = FieldStatus.Empty;
            }
        }

        private void SexTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SexTB.Text == "")
            {
                SexTB.Text = "Sex";
                SexTB.Foreground = new SolidColorBrush(Colors.LightGray);
                DataStatus[3] = FieldStatus.Empty;
            }
        }

        private void CountryTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CountryTB.Text != "Country" && CountryTB.Text != "")
            {
                //FirstNameTB.Text = "";
                //FirstNameTB.Foreground = new SolidColorBrush(Colors.Black);
                DataStatus[2] = FieldStatus.Correct;
            }
        }

        private void CountryTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CountryTB.Text == "Country")
            {
                CountryTB.Text = "";
                CountryTB.Foreground = new SolidColorBrush(Colors.Black);
                DataStatus[2] = FieldStatus.Empty;
            }
        }

        private void CountryTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CountryTB.Text == "")
            {
                CountryTB.Text = "Country";
                CountryTB.Foreground = new SolidColorBrush(Colors.LightGray);
                DataStatus[2] = FieldStatus.Empty;
            }
        }

        private void PhoneNumberTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PhoneNumberTB.Text != "Phone" && PhoneNumberTB.Text != "")
            {
                //FirstNameTB.Text = "";
                //FirstNameTB.Foreground = new SolidColorBrush(Colors.Black);
                DataStatus[5] = FieldStatus.Correct;
            }
        }

        private void PhoneNumberTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PhoneNumberTB.Text == "Phone")
            {
                PhoneNumberTB.Text = "";
                PhoneNumberTB.Foreground = new SolidColorBrush(Colors.Black);
                DataStatus[5] = FieldStatus.Empty;
            }
        }

        private void PhoneNumberTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PhoneNumberTB.Text == "")
            {
                PhoneNumberTB.Text = "Phone";
                PhoneNumberTB.Foreground = new SolidColorBrush(Colors.LightGray);
                DataStatus[5] = FieldStatus.Empty;
            }
        }

        private void BirthDateDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BirthDateDP.Text != "Birth Date" && BirthDateDP.Text != "")
            {
                //FirstNameTB.Text = "";
                //FirstNameTB.Foreground = new SolidColorBrush(Colors.Black);
                DataStatus[7] = FieldStatus.Correct;
            }
        }

        private void BirthDateDP_GotFocus(object sender, RoutedEventArgs e)
        {
            if (BirthDateDP.Text == "Birth Date")
            {
                BirthDateDP.Text = "";
                BirthDateDP.Foreground = new SolidColorBrush(Colors.Black);
                DataStatus[7] = FieldStatus.Empty;
            }
        }

        private void BirthDateDP_LostFocus(object sender, RoutedEventArgs e)
        {
            if (BirthDateDP.Text == "")
            {
                BirthDateDP.Text = "Birth Date";
                BirthDateDP.Foreground = new SolidColorBrush(Colors.LightGray);
                DataStatus[7] = FieldStatus.Empty;
            }
        }

        private void AgreementCH_Checked(object sender, RoutedEventArgs e)
        {
            DataStatus[10] = FieldStatus.Correct;
        }

        private void AgreementCH_Unchecked(object sender, RoutedEventArgs e)
        {
            DataStatus[10] = FieldStatus.Empty;
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            RegisterResultTB.Text = "";
            RegisterResultTB.Visibility = Visibility.Hidden;
            bool Result = true;
            for(int i = 0; i < DataStatus.Length; i++)
            {
                if(DataStatus[i] != FieldStatus.Correct)
                {
                    HighLigthEmpty(i);
                    Result &= false;
                    break;
                }
                else
                {
                    Result &= true;
                }
            }
            if(Result)
            {
                Result = CheckPass();
                if(Result)
                {
                    Packet Convertor = new Packet();
                    Packet.MainPacket MainDataPacket = new Packet.MainPacket();
                    Packet.ProPacket ProPacketData = new Packet.ProPacket();
                    Packet.Register RegisterPacket = new Packet.Register();
                    RegisterPacket.FirstName = FirstNameTB.Text;
                    RegisterPacket.LastName = LastNameTB.Text;
                    RegisterPacket.Country = CountryTB.Text;
                    RegisterPacket.Sex = SexTB.Text;
                    RegisterPacket.Email = EmailTB.Text;
                    RegisterPacket.Phone = PhoneNumberTB.Text;
                    RegisterPacket.Username = UserNameTB.Text;
                    RegisterPacket.BirthDate = BirthDateDP.SelectedDate.Value.ToString();
                    RegisterPacket.Password = PasswordTB.Password;
                    string RowData = Convertor.ToString(RegisterPacket);
                    ProPacketData.Type = (short)Packet.PacketType.Register;
                    ProPacketData.TotalSize = RowData.Length;
                    ProPacketData.ID = UserNameTB.Text;
                    string ProData = Convertor.ToString(ProPacketData);
                    MainDataPacket.Data = RowData;
                    MainDataPacket.PPacket = ProData;
                    string ValidData = Convertor.ToString(MainDataPacket);
                    Connection = new ConnectionClass(MainWindow.DS.Tables["Data"].Rows[1]["DataContent"].ToString(), MainWindow.DS.Tables["Data"].Rows[0]["DataContent"].ToString(), 1024 * 4, 3 ,true);
                    Connection.SendToServerRegister(ValidData , RegisterREciveData , ref ReciveData);
                }
                else
                {
                    ShowPassError();
                }
            }
            
        }

        private void RegisterREciveData(IAsyncResult ar)
        {
            Socket ParentSocket = (Socket)ar.AsyncState;
            int Count = ParentSocket.EndReceive(ar);
            if (Count > 0)
            {
                string ValidData = Encoding.Unicode.GetString(AES_Decrypt(ReciveData, PassBytes));
                Packet.MainPacket Data;
                Packet Pack = new Packet();
                string RegisterProPack = ValidData.Replace("\0", "");
                Data = Pack.ToPacket<Packet.MainPacket>(RegisterProPack);
                Packet.ProPacket ProData = new Packet.ProPacket();
                ProData = Pack.ToPacket<Packet.ProPacket>(Data.PPacket);
                string RegisterRecivedID = ProData.ID;
                //RemainingData = ProData.TotalSize;
                short Type = ProData.Type;
                if (Type == (short)Packet.PacketType.RegisterResult)
                {
                    Packet.RegisterResult R = new Packet.RegisterResult();
                    R = Pack.ToPacket<Packet.RegisterResult>(Data.Data);
                    RegisterResultTB.Dispatcher.Invoke(() =>
                    {
                        RegisterResultTB.Text = R.Result;
                        Thread.Sleep(2000);
                        this.Close();
                        MainWindow.LoginPage.Show();
                    });
                }
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
                    AES.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    //AES.Padding = PaddingMode.None;
                    AES.Mode = CipherMode.ECB;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
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
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    //AES.Padding = PaddingMode.None;
                    AES.Mode = CipherMode.ECB;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        public void HighLigthEmpty(int DataIndex )
        {
            string Text = "Please fill ";
            string Field = "";
            switch(DataIndex)
            {
                case 0:
                    {
                        Field = "First Name";
                    };break;
                case 1:
                    {
                        Field = "Last Name";
                    }; break;
                case 2:
                    {
                        Field = "Country";
                    }; break;
                case 3:
                    {
                        Field = "Sex";
                    }; break;
                case 4:
                    {
                        Field = "Email";

                    }; break;
                case 5:
                    {
                        Field = "Phone";
                    }; break;
                case 6:
                    {
                        Field = "Username";
                    }; break;
                case 7:
                    {
                        Field = "Birth Date";
                    }; break;
                case 8:
                    {
                        Field = "Password";
                    }; break;
                case 9:
                    {
                        Field = "Confirm Password";
                    }; break;
                case 10:
                    {
                        Field = "Agrrement ";
                    }; break;
            }
            Text += Field;
            RegisterResultTB.Visibility = Visibility.Visible;
            RegisterResultTB.Text = Text;
        }

        public bool CheckPass()
        {
            bool Result = false;
            if(PasswordTB.Password == ConfermPasswordTB.Password)
            {
                Result = true;
            }
            return Result;
        }

        public void ShowPassError()
        {
            RegisterResultTB.Text = "Password and Confirm Password do not match";
            RegisterResultTB.Visibility = Visibility.Visible;
        }

        private void PasswordTB_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(PasswordTB.Password != "")
            {
                DataStatus[8] = FieldStatus.Correct;
            }
            else
            {
                DataStatus[8] = FieldStatus.Empty;
            }
        }

        private void PasswordTB_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordTB.Background = new SolidColorBrush(Colors.White);
            if(PasswordTB.Password == "")
            {
                DataStatus[8] = FieldStatus.Empty;
            }
            else
            {
                DataStatus[8] = FieldStatus.Correct;
            }
        }

        private void PasswordTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if(PasswordTB.Password == "")
            {
                DataStatus[8] = FieldStatus.Empty;
                PasswordTB.Background = new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                DataStatus[8] = FieldStatus.Correct;
            }
        }
        private void ConfermPasswordTB_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (ConfermPasswordTB.Password != "")
            {
                DataStatus[9] = FieldStatus.Correct;
            }
            else
            {
                DataStatus[9] = FieldStatus.Empty;
            }
        }

        

        private void ConfermPasswordTB_GotFocus(object sender, RoutedEventArgs e)
        {
            ConfermPasswordTB.Background = new SolidColorBrush(Colors.White);
            if (ConfermPasswordTB.Password == "")
            {
                DataStatus[9] = FieldStatus.Empty;
            }
            else
            {
                DataStatus[9] = FieldStatus.Correct;
            }
        }

        private void ConfermPasswordTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ConfermPasswordTB.Password == "")
            {
                DataStatus[9] = FieldStatus.Empty;
                ConfermPasswordTB.Background = new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                DataStatus[9] = FieldStatus.Correct;
            }
        }

        private void TermsTB_Loaded(object sender, RoutedEventArgs e)
        {
            string Conditions = "1. Use of the Service and Conditions \r\n1.1 Authorisation.Subject to the Terms set out herein, you are hereby granted a limited, revocable, non-exclusive and non-transferable licence(without right to sub - licence) to access and use the Services with respect to your Devices for your own internal and private (domestic) use only."
                + "\r\n1.2 Conditions.You are responsible for all activities undertaken under your Account, including the activities undertaken by your Users.You agree to (a) not use the Services for any illegal or unauthorized purpose or any activity that infringes third party rights or breaches these Terms, (b) use the Services in any manner that may deteriorate the Services, (c) violate any laws in your jurisdiction while using the Services, and(d) indemnify and hold us harmless to the fullest extent allowed by law regarding all matters related to your use of the Services."
                + "\r\n1.3 Prohibition on Use.You agree not to use or permit use of the Services to monitor or control Users Devices and activities in any of the following conditions:"
                + "\r\na) When the Devices are not your property(or of your immediate family members) or have not been leased to you."
                + "\r\nb) When it is not clear in your jurisdiction that controlling and monitoring use of the Devices through the functionalities offered by the _Services are permitted by law or regulation."
                + "\r\nc) When it is not clear in your jurisdiction that that controlling and monitoring use of the Devices through the functionalities offered by the Services does not require express User consent, unless such express consent is obtained and documented by you."
                + "\r\nd) When there are no guarantees that the use of information obtained through using the Services will respect third party(including Users’) rights and applicable regulation."
                + "\r\n1.4 Exclusion.We reserve the right to exclude you from the Bridle Platform and Services without prior warning should you, or any of your Users, breach these Terms.";
            string Terms = "2. Term & Termination"
                + "\r\n2.1 Term.Subject to payment, when applicable, of the fees, your Bridle Account shall be activated and remain in force for the term indicated below, unless terminated by either us or you hereunder: "
                + "\r\na) Free Trial: use of the Services for a period of 30 days from the registration of the User Account."
                + "\r\nb) Premium User: subject to payment of the fees, use of the Services for the contracted term, in accordance with the plan you have chosen.Premium terms plans are automatically renewed for the same term, unless written notice of cancellation is provided by either party at least 30 days’ prior to renewal."
                + "\r\n2.2 Cancellations.You may cancel your Account at any time.All cancelations should be addressed to: support@Bridle.com.No refunds will be given for early termination unless we are in breach of these terms.In addition, we reserve the right to cancel this agreement with you if (a) the provision of the Services is, in our sole opinion, no longer commercially or otherwise viable or(b) your Account is inactive for more than 1 month(in this latter event, we will provide you 15 days’ notice of termination, sent to your registered email address)."
                + "\r\n2.3 Termination for breach. We may suspend or cancel your registration immediately in case of breach by you of these Terms, by written notice. We may cancel the account of any Free Trial User at any time, with 30 days’ notice of termination, and of Premium Users by providing you with 30 days’ notice prior to the end of any Premium User term."
                + "\r\n2.4 Upon Termination. On termination for any reason, your access to your Bridle Account and all of its content will be disabled and your content deleted, except as maintained in backups (for back-up retrieval purposes only or for any legal contingency). You shall uninstall all Bridle Device Software following the instructions of your Device operating system.";
            TermsTB.Text = Conditions + "\r\n" +Terms;
        }
    }
}

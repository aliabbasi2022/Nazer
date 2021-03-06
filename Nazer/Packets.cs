using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace UI
{
    class Packet
    {
        public struct MainPacket
        {
            public string PPacket;
            public string Data;
        }
        public  string ToString<T>(T Value)
        {
            System.Runtime.Serialization.FormatterConverter Formater = new System.Runtime.Serialization.FormatterConverter();
            if (Value is ParentIdentify)
            {
                ParentIdentify Temp = (ParentIdentify)Formater.Convert(Value, typeof(ParentIdentify));
                return (Temp.ID + "," + Temp.Password + "," + Temp.Childrens);
            }
            if (Value is IdentifyResult)
            {
                IdentifyResult Temp = (IdentifyResult)Formater.Convert(Value, typeof(IdentifyResult));
                return (Temp.State);
            }
            if (Value is ChildIdentify)
            {
                ChildIdentify Temp = (ChildIdentify)Formater.Convert(Value, typeof(ChildIdentify));
                return (Temp.ID + "," + Temp.Parent);
            }
            if (Value is Goodbay)
            {
                Goodbay Temp = (Goodbay)Formater.Convert(Value, typeof(Goodbay));
                return (Temp.IsGoodbay.ToString());
            }
            if (Value is Request)
            {
                Request Temp = (Request)Formater.Convert(Value, typeof(Request));
                return (Temp.Time.ToString());
            }
            if (Value is ParentSingup)
            {
                ParentSingup Temp = (ParentSingup)Formater.Convert(Value, typeof(ParentSingup));
                return (Temp.ID + "," + Temp.Password + "," + Temp.Mac);
            }
            if (Value is ChildSingup)
            {
                ChildSingup Temp = (ChildSingup)Formater.Convert(Value, typeof(ChildSingup));
                return (Temp.ID + "," + Temp.Parents + "," + Temp.Mac);
            }
            if (Value is ProPacket)
            {
                ProPacket Temp = (ProPacket)Formater.Convert(Value, typeof(ProPacket));
                return (Temp.ID + "," + Temp.TotalSize + "," + Temp.Type);
            }
            if (Value is PSProPacket)
            {
                PSProPacket Temp = (PSProPacket)Formater.Convert(Value, typeof(PSProPacket));
                return (Temp.ID + "," + Temp.TotalSize + "," + Temp.Type + "," + Temp.Reciver);
            }
            if (Value is License)
            {
                License Temp = (License)Formater.Convert(Value, typeof(License));
                return (Temp.ID + "," + Temp.ChildID + "," + Temp.BuyDate + ","+ Temp.ExDate);
            }
            if (Value is Assignment)
            {
                Assignment Temp = (Assignment)Formater.Convert(Value, typeof(Assignment));
                return (Temp.LicenseID + "," + Temp.ChildID + "," + Convert.ToString(Temp.Set));
            }
            //if (Value is ChildProPacket)
            //{
            //    ChildProPacket Temp = (ChildProPacket)Formater.Convert(Value, typeof(ChildProPacket));
            //    return ( Temp.TotalSize + "," + Temp.Type);
            //}
            if (Value is Location)
            {
                Location Temp = (Location)Formater.Convert(Value, typeof(Location));
                return (Temp.Latitude + "," + Temp.Longitude + "," + Temp.Time.ToString());
            }
            if (Value is URL)
            {
                URL Temp = (URL)Formater.Convert(Value, typeof(URL));
                return (Temp.Address + "," + Temp.Type + "," + Temp.Time.ToString() + "," + Temp.Browser);
            }
            if (Value is Apps)
            {
                Apps Temp = (Apps)Formater.Convert(Value, typeof(Apps));
                return (Temp.AppName + "," + Temp.Type + ",");
            }
            if (Value is InstalledApps)
            {
                InstalledApps Temp = (InstalledApps)Formater.Convert(Value, typeof(InstalledApps));
                return (Temp.Name + "," + Temp.InstallDate.ToString() + "," + Temp.Publisher + "," + Temp.Version +"," + Temp.AppIcon );
            }
            if (Value is UnInstalledApps)
            {
                UnInstalledApps Temp = (UnInstalledApps)Formater.Convert(Value, typeof(UnInstalledApps));
                return (Temp.Name + "," + Temp.UnInstallDate.ToString());
            }
            if (Value is AppsLog)
            {
                AppsLog Temp = (AppsLog)Formater.Convert(Value, typeof(AppsLog));
                return (Temp.Name + "," + Temp.Time.ToString() + "," + Temp.Type);
            }
            if (Value is ScrrenShot)
            {
                ScrrenShot Temp = (ScrrenShot)Formater.Convert(Value, typeof(ScrrenShot));
                return (Temp.Picture + "," + Temp.Start.ToString() + "," + Temp.End.ToString() + "," + Temp.Type + "," + Temp.Header);
            }
            if (Value is SystemStatus)
            {
                SystemStatus Temp = (SystemStatus)Formater.Convert(Value, typeof(SystemStatus));
                return (Temp.Type + "," + Temp.Time);
            }
            if (Value is NetworkStatus)
            {
                NetworkStatus Temp = (NetworkStatus)Formater.Convert(Value, typeof(NetworkStatus));
                return (Temp.Data + "," + Temp.Type + "," + Temp.Time.ToString() );
            }
            if (Value is NetworkCommands)
            {
                NetworkCommands Temp = (NetworkCommands)Formater.Convert(Value, typeof(NetworkCommands));
                return (Temp.Type + "," + Temp.Command + "," + Temp.Time);
            }
            if (Value is SystemLimition2Time)
            {
                SystemLimition2Time Temp = (SystemLimition2Time)Formater.Convert(Value, typeof(SystemLimition2Time));
                return (Temp.Start.ToString() + "," + Temp.End.ToString() + "," + Temp.Act + "," +Temp.Id);
            }
            if (Value is SystemLimition3Time)
            {
                SystemLimition3Time Temp = (SystemLimition3Time)Formater.Convert(Value, typeof(SystemLimition3Time));
                return (Temp.Start + "," + Temp.End + "," + Temp.Duration + "," + Temp.Act + "," + Temp.Id);
            }
            if (Value is AppLimition2Time)
            {
                AppLimition2Time Temp = (AppLimition2Time)Formater.Convert(Value, typeof(AppLimition2Time));
                return (Temp.AppName + "," + Temp.Start + "," + Temp.End + "," + Temp.Act);
            }
            if (Value is AppLimition3Time)
            {
                AppLimition3Time Temp = (AppLimition3Time)Formater.Convert(Value, typeof(AppLimition3Time));
                return (Temp.AppName + "," + Temp.Start + "," + Temp.End + "," + Temp.Duration + "," + Temp.Act);
            }
            if (Value is RealTimeMonitor)
            {
                RealTimeMonitor Temp = (RealTimeMonitor)Formater.Convert(Value, typeof(RealTimeMonitor));
                return (Temp.IP + "," + Temp.Port + "," + Temp.DeviceType);
            }
            if (Value is UnInstall)
            {
                UnInstall Temp = (UnInstall)Formater.Convert(Value, typeof(UnInstall));
                return (Temp.ParentID);
            }
            if (Value is KeyLogger)
            {
                KeyLogger Temp = (KeyLogger)Formater.Convert(Value, typeof(KeyLogger));
                return (Temp.State.ToString() + "," + Temp.ProcessName + "," + Temp.Data + "," + Temp.Time);
            }
            if (Value is RecordVoice)
            {
                RecordVoice Temp = (RecordVoice)Formater.Convert(Value, typeof(RecordVoice));
                return (Temp.State.ToString() + "," + Temp.ProcessName + "," + Temp.Data + "," + Temp.Time);
            }
            if (Value is WebCam)
            {
                WebCam Temp = (WebCam)Formater.Convert(Value, typeof(WebCam));
                return (Temp.State.ToString() + "," + Temp.Type + "," + Temp.ProcessName + "," + Temp.Time);
            }
            if (Value is Register)
            {
                Register Temp = (Register)Formater.Convert(Value, typeof(Register));
                return (Temp.FirstName + "," + Temp.LastName + "," + Temp.Country + "," + Temp.Sex + "," + Temp.Email + "," + Temp.Phone+ "," + Temp.Username + "," + Temp.BirthDate + "," + Temp.Password);
            }
            if (Value is RegisterResult)
            {
                RegisterResult Temp = (RegisterResult)Formater.Convert(Value, typeof(RegisterResult));
                return (Temp.Result+ ",");
            }
            if (Value is MainPacket)
            {
                MainPacket Temp = (MainPacket)Formater.Convert(Value, typeof(MainPacket));
                return (Temp.PPacket + "☺" + Temp.Data);
            }
            return "";
        }
        public  T ToPacket<T>(string Value)
        {
            T Result;
            if (typeof(T) == typeof(ParentIdentify))
            {
                string[] Data = Value.Split(',');
                ParentIdentify Temp = new ParentIdentify();
                Temp.ID = Data[0];
                Temp.Password = Data[1];
                Temp.Childrens = Data[2];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(IdentifyResult))
            {
                string[] Data = Value.Split(',');
                IdentifyResult Temp = new IdentifyResult();
                for (int i = 0; i < Data.Length; i++)
                {
                    Temp.State += Data[i];
                }
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(ChildIdentify))
            {
                string[] Data = Value.Split(',');
                ChildIdentify Temp = new ChildIdentify();
                Temp.ID = Data[0];
                Temp.Parent = Data[1];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(Goodbay))
            {
                string[] Data = Value.Split(',');
                Goodbay Temp = new Goodbay();
                Temp.IsGoodbay = Convert.ToBoolean(Data[0]);
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(ProPacket))
            {
                string[] Data = Value.Split(',');
                ProPacket Temp = new ProPacket();
                Temp.ID = Data[0];
                Temp.TotalSize = Convert.ToInt32(Data[1]);
                Temp.Type = Convert.ToInt16(Data[2]);
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(PSProPacket))
            {
                string[] Data = Value.Split(',');
                PSProPacket Temp = new PSProPacket();
                Temp.ID = Data[0];
                Temp.TotalSize = Convert.ToInt32(Data[1]);
                Temp.Type = Convert.ToInt16(Data[2]);
                Temp.Reciver = Data[3];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(SingupResult))
            {
                string[] Data = Value.Split(',');
                SingupResult Temp = new SingupResult();
                Temp.Result = Data[0];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(License))
            {
                string[] Data = Value.Split(',');
                License Temp = new License();
                Temp.ID = Data[0];
                Temp.ChildID = Data[1];
                Temp.BuyDate = Data[2];
                Temp.ExDate = Data[3];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(Assignment))
            {
                string[] Data = Value.Split(',');
                Assignment Temp = new Assignment();
                Temp.LicenseID = Data[0];
                Temp.ChildID = Data[1];
                Temp.Set = Convert.ToBoolean(Data[2]);
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(ParentSingup))
            {
                string[] Data = Value.Split(',');
                ParentSingup Temp = new ParentSingup();
                Temp.ID = Data[0];
                Temp.Password = Data[1];
                Temp.Mac = Data[2];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(ChildSingup))
            {
                string[] Data = Value.Split(',');
                ChildSingup Temp = new ChildSingup();
                Temp.ID = Data[0];
                Temp.Parents = Data[1];
                Temp.Mac = Data[2];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(Request))
            {
                string[] Data = Value.Split(',');
                Request Temp = new Request();
                Temp.Time = Convert.ToDateTime(Data[0]);
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(InstalledApps))
            {
                string[] Data = Value.Split('#');
                InstalledApps Temp = new InstalledApps();
                Temp.Name =Data[0];
                Temp.InstallDate = Data[1];
                Temp.Publisher = Data[3];
                Temp.Version = Data[2];
                //Temp.AppIcon = Data[4];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(UnInstalledApps))
            {
                string[] Data = Value.Split('#');
                UnInstalledApps Temp = new UnInstalledApps();
                Temp.Name = Data[0];
                Temp.UnInstallDate = Data[1];
                //Temp.Publisher = Data[2];
                //Temp.Version = Data[3];
                //Temp.AppIcon = Data[4];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(Location))
            {
                string[] Data = Value.Split(',');
                Location Temp = new Location();
                Temp.Latitude = Data[0];
                Temp.Longitude = Data[1];
                Temp.Time = Convert.ToDateTime(Data[2]);
                //Temp.Version = Data[3];
                //Temp.AppIcon = Data[4];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(URL))
            {
                string[] Data = Value.Split(',');
                URL Temp = new URL();
                Temp.Address = Data[0];
                Temp.Type = Convert.ToInt16(Data[1]);
                Temp.Time = Convert.ToDateTime(Data[2]);
                Temp.Browser = Data[3];
                //Temp.Version = Data[3];
                //Temp.AppIcon = Data[4];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(Apps))
            {
                string[] Data = Value.Split(',');
                Apps Temp = new Apps();
                Temp.AppName = Data[0];
                Temp.Type = Convert.ToInt16(Data[1]);
                //Temp.Time = Convert.ToDateTime(Data[2]);
                //Temp.Browser = "";
                //Temp.Version = Data[3];
                //Temp.AppIcon = Data[4];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(ScrrenShot))
            {
                string[] Data = Value.Split(',');
                ScrrenShot Temp = new ScrrenShot();
                Temp.Picture = Data[0];
                try
                {
                    Temp.Start = Convert.ToDateTime(Data[1]);
                }
                catch(Exception E)
                {

                }
                try
                {
                    Temp.End = Convert.ToDateTime(Data[2]);
                }
                catch(Exception E)
                {

                }
                Temp.Type = Convert.ToInt16(Data[3]);
                Temp.Header = Data[4];
                //Temp.Time = Convert.ToDateTime(Data[2]);
                //Temp.Browser = "";
                //Temp.Version = Data[3];
                //Temp.AppIcon = Data[4];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(SystemStatus))
            {
                string[] Data = Value.Split(',');
                SystemStatus Temp = new SystemStatus();
                Temp.Type = Convert.ToInt16(Data[0]);
                Temp.Time = Data[1];
                //Temp.Time = Convert.ToDateTime(Data[2]);
                //Temp.Browser = "";
                //Temp.Version = Data[3];
                //Temp.AppIcon = Data[4];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(NetworkCommands))
            {
                string[] Data = Value.Split(',');
                NetworkCommands Temp = new NetworkCommands();
                Temp.Type = Convert.ToInt16(Data[0]);
                Temp.Command = Data[1];
                Temp.Time = Data[2];
                //Temp.Time = Convert.ToDateTime(Data[2]);
                //Temp.Browser = "";
                //Temp.Version = Data[3];
                //Temp.AppIcon = Data[4];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(SystemLimition2Time))
            {
                string[] Data = Value.Split(',');
                SystemLimition2Time Temp = new SystemLimition2Time();
                DateTime TempTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                Temp.Start = Convert.ToDateTime(Data[0]) - TempTime;
                Temp.End = Convert.ToDateTime(Data[1]) - TempTime;
                Temp.Act = Convert.ToInt16(Data[2]);
                Temp.Id = Data[3];
                //Temp.Time = Convert.ToDateTime(Data[2]);
                //Temp.Browser = "";
                //Temp.Version = Data[3];
                //Temp.AppIcon = Data[4];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(SystemLimition3Time))
            {
                string[] Data = Value.Split(',');
                SystemLimition3Time Temp = new SystemLimition3Time();
                DateTime TempTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                Temp.Start = Convert.ToDateTime(Data[0])- TempTime;
                Temp.End = Convert.ToDateTime(Data[1])- TempTime;
                Temp.Duration = Convert.ToDateTime(Data[2])- TempTime;
                Temp.Act = Convert.ToInt16(Data[3]);
                Temp.Id = Data[4];
                //Temp.Time = Convert.ToDateTime(Data[2]);
                //Temp.Browser = "";
                //Temp.Version = Data[3];
                //Temp.AppIcon = Data[4];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(AppLimition2Time))
            {
                string[] Data = Value.Split(',');
                AppLimition2Time Temp = new AppLimition2Time();
                Temp.AppName = Data[0];
                Temp.Start = Convert.ToDateTime(Data[1]);
                Temp.End = Convert.ToDateTime(Data[2]);
                Temp.Act = Convert.ToInt16(Data[3]);
                //Temp.Id = Data[3];
                //Temp.Time = Convert.ToDateTime(Data[2]);
                //Temp.Browser = "";
                //Temp.Version = Data[3];
                //Temp.AppIcon = Data[4];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(AppLimition3Time))
            {
                string[] Data = Value.Split(',');
                AppLimition3Time Temp = new AppLimition3Time();
                DateTime TempTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                Temp.AppName = Data[0];
                Temp.Start = Convert.ToDateTime(Data[1])- TempTime;
                Temp.End = Convert.ToDateTime(Data[2])- TempTime;
                Temp.Duration = Convert.ToDateTime(Data[3]) - TempTime;
                Temp.Act = Convert.ToInt16(Data[4]);
                //Temp.Id = Data[4];
                //Temp.Time = Convert.ToDateTime(Data[2]);
                //Temp.Browser = "";
                //Temp.Version = Data[3];
                //Temp.AppIcon = Data[4];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(RealTimeMonitor))
            {
                string[] Data = Value.Split(',');
                RealTimeMonitor Temp = new RealTimeMonitor();
                Temp.IP = Data[0];
                Temp.Port = Convert.ToInt32(Data[1]);
                Temp.DeviceType = Convert.ToBoolean(Data[2]);
                //Temp.Version = Data[3];
                //Temp.AppIcon = Data[4];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(NetworkStatus))
            {
                string[] Data = Value.Split(',');
                NetworkStatus Temp = new NetworkStatus();
                Temp.Data = (Data[0] + "#" + Data[1] + "#" + Data[2]);
                if (Data.Length > 3)
                {
                    try
                    {
                        Temp.Time = Convert.ToDateTime(Data[4]);
                        Temp.Type = Convert.ToInt16(Data[3]);
                    }
                    catch (Exception E)
                    {

                    }
                    
                }
                else
                {
                    Temp.Time = Convert.ToDateTime(Data[2]);
                    Temp.Type = Convert.ToInt16(Data[1]);
                }
                
                //Temp.Version = Data[3];
                //Temp.AppIcon = Data[4];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(AppsLog))
            {
                string[] Data = Value.Split(',');
                AppsLog Temp = new AppsLog();
                Temp.Name = Data[0];
                Temp.Time = Data[1];
                Temp.Type = Convert.ToInt16(Data[2]);
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
                /*try
                {
                    
                }
                catch(Exception E)
                {
                    string[] Datasd = Value.Split(',');
                    Result = (T)Convert.ChangeType(0, typeof(int));
                    return Result;
                }*/

                //Temp.Version = Data[3];
                //Temp.AppIcon = Data[4];
                //Result = (T)Convert.ChangeType(Temp, typeof(T));

            }
            if (typeof(T) == typeof(UnInstall))
            {
                string[] Data = Value.Split(',');
                UnInstall Temp = new UnInstall();
                Temp.ParentID = Data[0];
                
                //Temp.Version = Data[3];
                //Temp.AppIcon = Data[4];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(KeyLogger))
            {
                string[] Data = Value.Split(',');
                KeyLogger Temp = new KeyLogger();
                Temp.State = Convert.ToBoolean(Data[0]);
                Temp.ProcessName = Data[1];
                Temp.Data = Data[2];
                Temp.Time = Data[3];
                //Temp.Version = Data[3];
                //Temp.AppIcon = Data[4];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(RecordVoice))
            {
                string[] Data = Value.Split(',');
                RecordVoice Temp = new RecordVoice();
                Temp.State = Convert.ToBoolean(Data[0]);
                Temp.ProcessName = Data[1];
                Temp.Time = Data[3];
                Temp.Data = Data[2];
                //Temp.Version = Data[3];
                //Temp.AppIcon = Data[4];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(WebCam))
            {
                string[] Data = Value.Split(',');
                WebCam Temp = new WebCam();
                Temp.State = Convert.ToBoolean(Data[0]);
                Temp.ProcessName = Data[2];
                Temp.Type = Convert.ToInt16(Data[1]);
                Temp.Time = Data[3];
                //Temp.Version = Data[3];
                //Temp.AppIcon = Data[4];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(MainPacket))
            {
                string[] Data = Value.Split('☺');
                MainPacket Temp = new MainPacket();
                Temp.PPacket = Data[0];
                Temp.Data = Data[1];
                
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(Register))
            {
                string[] Data = Value.Split(',');
                Register Temp = new Register();
                Temp.FirstName = Data[0];
                Temp.LastName = Data[1];
                Temp.Sex = Data[3];
                Temp.Country = Data[2];
                Temp.Email = Data[4];
                Temp.Phone = Data[5];
                Temp.Username = Data[6];
                Temp.BirthDate = Data[7];
                Temp.Password = Data[8];
                //Temp.Version = Data[3];
                //Temp.AppIcon = Data[4];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            if (typeof(T) == typeof(RegisterResult))
            {
                string[] Data = Value.Split(',');
                RegisterResult Temp = new RegisterResult();
                Temp.Result = Data[0];
                
                //Temp.Version = Data[3];
                //Temp.AppIcon = Data[4];
                Result = (T)Convert.ChangeType(Temp, typeof(T));
                return Result;
            }
            Result = (T)Convert.ChangeType(0, typeof(int));
            return Result;
        }
        public ProPacket ConverToProPack(PSProPacket Input)
        {
            ProPacket OutPut = new ProPacket();
            OutPut.ID = Input.ID;
            OutPut.TotalSize = Input.TotalSize;
            OutPut.Type = (short)Packet.PacketType.ProPacket;
            return OutPut;
        }
        public struct ParentIdentify
        {
            public string ID;
            public string Password;
            public string Childrens;
        }
        public struct IdentifyResult
        {
            public string State;
        }
        public struct ChildIdentify
        {
            public string ID;
            public string Parent;
        }
        public struct KeyLogger
        {
            public bool State;
            public string ProcessName;
            public string Data;
            public string Time;
        }
        public struct RecordVoice
        {
            public bool State;
            public string ProcessName;
            public string Data;
            public string Time;
        }
        public struct WebCam
        {
            public bool State;
            public short Type;
            public string ProcessName;
            public string Time;
        }
        public enum WebCamType
        {
            Video = 0,
            Picture
        }
        public struct ParentSingup
        {
            public string ID;
            public string Password;
            public string Mac;
        }
        public struct ChildSingup
        {
            public string ID;
            public string Parents;
            public string Mac;
        }
        public struct SingupResult
        {
            public string Result;
        }
        public struct Goodbay
        {
            public bool IsGoodbay;
        }
        public struct Request
        {
            public DateTime Time;
        }
        public struct ProPacket
        {
            public string ID;
            public int TotalSize;
            public short Type;
        }
        public struct PSProPacket
        {
            public string ID;
            public int TotalSize;
            public short Type;
            public string Reciver;
        }
        public enum PacketType
        {
            Identify = 0,
            IdentifyResult,
            ProPacket,
            PSProPacket,
            Goodbay,
            ChildSingup,
            ParentSingup,
            
            Location,
            URL,
            Apps,
            InstalledApps,
            UnInstalledApps,
            AppsLog,
            ScrrenShot,
            SystemStart_Down,
            NetworkStatusNow,
            NetworkCommands,
            SystemLimition2Time,
            SystemLimition3Time,
            AppLimition2Time,
            AppLimition3Time,
            RealTimeMonitor,
            UnInstall,
            Keylogger,
            RecordVoice,
            WebCam, 
            License = 29,
            Assignment ,
            Register,
            RegisterResult,
            SingupResult
        }

        public struct License
        {
            public string ID;
            public string ExDate;
            public string BuyDate;
            public string ChildID;
        }
        public struct Register
        {
            public string FirstName;
            public string LastName;
            public string Sex;
            public string Username;
            public string BirthDate;
            public string Password;
            public string Email;
            public string Phone;
            public string Country;
        }
        public struct RegisterResult
        {
            public string Result;
        }
        public struct Assignment
        {
            public string LicenseID;
            public string ChildID;
            public bool Set;
        }
        public struct UnInstall
        {
            public string ParentID;
        }
        public struct Location
        {

            public string Latitude;
            public string Longitude;
            public DateTime Time;
        }
        public struct URL
        {

            public string Address;
            public short Type;
            public DateTime Time;
            public string Browser;
        }
        public enum URLType
        {
            History = 0,
            BlockWithError,
            Block,
            Disable
        }
        public struct Apps
        {
            public string AppName;
            public short Type;
        }
        public enum AppsType
        {
            Uninstall = 0,
            Close,
            Run,
            BlockWithErro,
            Block,
            RemoveBlock
        }
        public struct InstalledApps
        {

            public string Name;
            public string InstallDate;
            public string AppIcon;
            public string Version;
            public string Publisher;
        }
        public struct UnInstalledApps
        {

            public string Name;
            public string UnInstallDate;
            
        }
        public struct AppsLog
        {

            public string Name;
            public string Time;
            public short Type;
        }
        public enum AppsLogType
        {
            Start = 0,
            End,
            Runnig
        }
        public struct ScrrenShot
        {

            public string Picture;
            public DateTime Start;
            public DateTime End;
            public short Type;
            public string Header;
        }
        public enum ScrrenShotType
        {
            RealTime = 0,
            OneTime,
            Sequence,
            Counter,
        }
        public struct SystemStatus
        {

            public short Type;
            public string Time;
        }
        public enum SystemStatusType
        {
            Start = 0,
            Down,
            Reboot,
            Sleep,
            Hiber,
            Logoff,
            Lock
        }
        public struct NetworkStatus
        {
            public string Data;
            public short Type;
            public DateTime Time;
        }
        public enum NetworkStatusType
        {
            Connected = 0,
            DisConnected,
            VPNOn,
            VpnOff,
            NoInternet,
            Internet
        }
        public struct NetworkCommands
        {

            public short Type;
            public string Command;
            public string Time;
        }
        public enum NetworkCommandsType
        {
            Enable = 0,
            Disable,
            EnableWithTime,
            DisableWithTime
        }
        public struct SystemLimition2Time
        {

            public TimeSpan Start;
            public TimeSpan End;
            public short Act;
            public string Id;
        }
        public struct SystemLimition3Time
        {

            public Nullable<System.TimeSpan> Start;
            public Nullable<System.TimeSpan> End;
            public Nullable<System.TimeSpan> Duration;
            public short Act;
            public string Id;
        }
        public enum SystemLimitAct
        {
            Start= 0,
            ShutDown,
            Reboot,
            Sleep,
            Logoff,
            Lock,
            Hiber,
            Fireez,
            Disable
        }
        public struct AppLimition2Time
        {

            public string AppName;// ID
            public DateTime Start;
            public DateTime End;
            public short Act;
        }
        public struct AppLimition3Time
        {
            public string AppName;
            public Nullable<System.TimeSpan> Start;
            public Nullable<System.TimeSpan> End;
            public Nullable<System.TimeSpan> Duration;
            public short Act;
        }
        public enum AppLimitAct
        {
            Close = 0,
            CloseWithError,
            Disable
        }
        public struct RealTimeMonitor
        {
            public string IP;
            public int Port;
            public bool DeviceType;
        }
    }
    //class ChildeSide
    //{
    //    public struct Identify
    //    {
    //        public string ID;
    //        public string Password;
    //        public string Childrens;
    //    }
    //    public struct ProPacket
    //    {
    //        int TotalSize;
    //        int Type;
    //    }
    //    public enum PacketType
    //  {
    //      Identify = 0,
    //      Location,
    //      URL,
    //      Apps,
    //      InstalledApps,
    //      AppsLog,
    //      ScrrenShot,
    //      SystemStart_Down,
    //      NetworkStatusNow,
    //      NetworkCommands,
    //      SystemLimition2Time,
    //      SystemLimition3Time,
    //      AppLimition2Time,
    //      AppLimition3Time,
    //      RealTimeMonitor
    //  }
    //  public struct Location
    //  {
    //      string Latitude;
    //      string Longitude;
    //      DateTime Time;
    //  }
    //  public struct URL
    //  {
    //      string Address;
    //      short Type;
    //      DateTime Time;
    //  }
    //  public enum URLType
    //  {
    //      History = 0,
    //      Block
    //  }
    //  public struct Apps
    //  {
    //      string AppName;
    //      short Type;
    //  }
    //  public enum AppsType
    //  {
    //      Uninstall = 0,
    //      Close,
    //      Run,
    //      Block
    //  }
    //  public struct InstalledApps
    //  {
    //      string Name;
    //      DateTime InstallDate;
    //      Icon AppIcon;
    //      string Version;
    //  }
    //  public struct AppsLog
    //  {
    //      string Name;
    //      DateTime Time;
    //      short Type;
    //  }
    //  public enum AppsLogType
    //  {
    //      Start = 0,
    //      End,
    //      Runnig
    //  }
    //  public struct ScrrenShot
    //  {
    //      Bitmap Picture;
    //      DateTime Start;
    //      DateTime End;
    //      short Type;
    //      string Header;
    //  }
    //  public enum ScrrenShotType
    //  {
    //      RealTime = 0,
    //      OneTime,
    //      Sequence,
    //      Counter
    //  }
    //  public struct SystemStatus
    //  {
    //      short Type;
    //      DateTime Time;
    //  }
    //  public enum SystemStatusType
    //  {
    //      Start = 0,
    //      Down
    //  }
    //  public struct NetworkStatus
    //  {
    //      string Data;
    //      short Type;
    //      DateTime Time;
    //  }
    //  public enum NetworkStatusType
    //  {
    //      Connected = 0,
    //      DisConnected,
    //      VPNOn,
    //      VpnOff
    //  }
    //  public struct NetworkCommands
    //  {
    //      short Type;
    //      string Command;
    //      DateTime Time;
    //  }
    //  public enum NetworkCommandsType
    //  {
    //      Enable = 0,
    //      Disable,
    //      EnableWithTime,
    //      DisableWithTime
    //  }
    //  public struct SystemLimition2Time
    //  {
    //      DateTime Start;
    //      DateTime End;
    //
    //  }
    //  public struct SystemLimition3Time
    //  {
    //      DateTime Start;
    //      DateTime End;
    //      DateTime Duration;
    //  }
    //  public struct AppLimition2Time
    //  {
    //      string AppName;
    //      DateTime Start;
    //      DateTime End;
    //  }
    //  public struct AppLimition3Time
    //  {
    //      string AppName;
    //      DateTime Start;
    //      DateTime End;
    //      DateTime Duration;
    //  }
    //  public struct RealTimeMonitor
    //  {
    //      string IP;
    //      int Port;
    //  }
    //}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Xml;
using System.Collections.Specialized;
using Microsoft.Win32;
using System.Diagnostics;
using System.Security.Principal;
using System.Management;
using System.Device.Location;

namespace Child
{
    class Location
    {
        protected ManagementEventWatcher LocationServiceWatcher;
        protected GeoCoordinateWatcher LocationWatcher;
        protected GeoPosition<GeoCoordinate> CurrentLocation;
        public bool TurnOFF = false;

        public Location()
        {
            
            RegistryKey PathKey = Registry.CurrentUser;
            CurrentLocation = new GeoPosition<GeoCoordinate>();
            LocationServiceWatcher = new ManagementEventWatcher();
            string Path = @"Software\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\";
            using (Microsoft.Win32.RegistryKey key = PathKey.OpenSubKey(Path))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    if(subkey_name.Contains("BFA794E4-F964-4FDB-90F6-51056BFE4B44"))
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        try
                        {
                                string LocationStatus = subkey.GetValue("Value").ToString();
                                if (LocationStatus != "Allow")
                                {
                                    ProcessStartInfo StartInfo = new ProcessStartInfo("cmd.exe");
                                    StartInfo.UseShellExecute = true;
                                    StartInfo.Arguments = ("/C reg add  \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\DeviceAccess\\Global\\{BFA794E4-F964-4FDB-90F6-51056BFE4B44}\" /v \"Value\" /d \"Allow\" /f");
                                    StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                    StartInfo.Verb = "runas";
                                    Process process = new Process();
                                    process.StartInfo = StartInfo;
                                    process.Start();
                                    process.WaitForExit();
                                }
                        }
                        catch (Exception ee)
                        {

                        }

                    }
                }
            }
            
        }
        public void SetEventOnLocationService(EventArrivedEventHandler Handler)
        {
            string keyPath = "Software\\Microsoft\\Windows\\CurrentVersion\\DeviceAccess\\Global\\{BFA794E4-F964-4FDB-90F6-51056BFE4B44}";
            var currentUser = WindowsIdentity.GetCurrent();
            var query = new WqlEventQuery(string.Format(
            "SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\\\{1}' AND ValueName='{2}'",
            currentUser.User.Value, keyPath.Replace("\\", "\\\\"), "Value"));
            LocationServiceWatcher = new ManagementEventWatcher(query);
            LocationServiceWatcher.EventArrived += Handler;
            LocationServiceWatcher.Start();
        }
        public void PositionEvent(EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>> Handler)
        {
            LocationWatcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            LocationWatcher.PositionChanged += Handler;
        }
        public void TurnOnWiFi()
        {
            ProcessStartInfo StartInfo = new ProcessStartInfo("cmd.exe");
            StartInfo.UseShellExecute = true;
            StartInfo.Arguments = ("/C netsh interface set interface \"Wi-Fi\" enabled");
            StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            StartInfo.Verb = "runas";
            Process process = new Process();
            process.StartInfo = StartInfo;
            process.Start();
            process.WaitForExit();
        }
        public void StartLocating()
        {
            NetWorkTools Tools = new NetWorkTools();
            if ((Tools.AdaptorISOn("Wi-Fi")== false))
            {
                TurnOnWiFi();
                TurnOFF = true;
            }
            LocationWatcher.Start();

        }
        public void StopLocating()
        {
            LocationWatcher.Stop();
        }

        public void SetLocation(GeoPosition<GeoCoordinate> Data)
        {
            CurrentLocation = Data;
        }
        public GeoPosition<GeoCoordinate> GetLocation()
        {
            return CurrentLocation;
        }

    }
}

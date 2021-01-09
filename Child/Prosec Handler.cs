using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;


namespace Child
{
    public class Process_Handler
    {
        protected ManagementEventWatcher StartWatcher;// the wathcer that looking for process that started and call function;
        protected ManagementEventWatcher StopWatcher;// the wathcer that looking for process that stoped and call function;
        //protected ManagementBaseObject StartEventData;
        //protected ManagementBaseObject StopEventData;
        /// <summary>
        /// Initial All Propertis to Start listening to Events that incoming From Process Events
        /// </summary>
        public Process_Handler()
        {
            StartWatcher = new ManagementEventWatcher();
            StopWatcher = new ManagementEventWatcher();
            //string StartqueryString = 
            //"SELECT TargetInstance.Name FROM __InstanceDeletionEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process'";// Query that connect to system data base that store all Process and return 
            //// edge when new process is coming
            //// The dot in the scope means use the current machine
            //string StopqueryString =
            //"SELECT TargetInstance.Name FROM __InstanceDeletionEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process'";// Query that connect to system data base that store all Process and return 
            //ManagementScope scope = new ManagementScope(@"\\.\root\CIMV2");
            //// Create a watcher and listen for events
            //StartWatcher = new ManagementEventWatcher(StartqueryString) ;
            //StopWatcher = new ManagementEventWatcher(StopqueryString);
            //StartWatcher.Scope = scope;
            //StopWatcher.Scope = scope;
            //Other Way 
            // ---------------Start --------------
            WqlEventQuery StartQuery = new WqlEventQuery("__InstanceCreationEvent", new TimeSpan(0, 0, 1), "TargetInstance isa \"Win32_Process\"");
            WqlEventQuery StopQuery = new WqlEventQuery("__InstanceDeletionEvent", new TimeSpan(0, 0, 1), "TargetInstance isa \"Win32_Process\"");
            StartWatcher.Query = StartQuery;
            StopWatcher.Query = StopQuery;
            //----End---
        }
        /// <summary>
        /// Listen to Incoming Start Process Event and Pass Event to Specific Function
        /// </summary>
        /// <param name="Name"> Event Function Name </param>
        public void FindeStartedProcess(System.Management.EventArrivedEventHandler Name )
        {
            StartWatcher.EventArrived += Name;//Send Event to This Fuction 
            StartWatcher.Start();// Start Listening for Events
        }
        /// <summary>
        /// Listen to Incoming Stop Process Event and Pass Event to Specific Function
        /// </summary>
        /// <param name="Name"> Event Function Name </param>
        public void FindStopedProcess(System.Management.EventArrivedEventHandler Name)
        {
            StopWatcher.EventArrived += Name;
            StopWatcher.Start();      
        }
        //public void FindeStartedProcess1(Action<ManagementBaseObject> Name)
        //{
        //    //StartWatcher.EventArrived += Name;//Send Event to This Fuction 
        //    //StartWatcher.Start();// Start Listening for Events
        //    Task.Run(() =>
        //    {
        //        while (true)
        //        {
        //            StartEventData = StartWatcher.WaitForNextEvent();
        //            Name(StartEventData);
        //        }
        //    });
        //
        //}
        ///// <summary>
        ///// Listen to Incoming Stop Process Event and Pass Event to Specific Function
        ///// </summary>
        ///// <param name="Name"> Event Function Name </param>
        //public void FindStopedProcess1(Action<ManagementBaseObject> Name)
        //{
        //    //StopWatcher.EventArrived += Name;
        //    //StopWatcher.Start();
        //    Task.Run(() =>
        //    {
        //        while (true)
        //        {
        //            StopEventData = StopWatcher.WaitForNextEvent();
        //            Name(StopEventData);
        //        }
        //    });
        //
        //
        //}
        /// <summary>
        /// return List That fill with All Process Names
        /// </summary>
        /// <param name="ProcessNames">List that Fill With Selected Process</param>
        public void ShowAllProcessName(ref List<string> ProcessNames)
        {
            Process[] AllProcess = Process.GetProcesses();
            foreach(Process Var in AllProcess)
            {
                ProcessNames.Add(Var.ProcessName);
            }
        }
        /// <summary>
        /// Fill List That Store All Process
        /// </summary>
        /// <param name="Proces">List that Fill With Selected Process</param>
        public void GetAllProcess(ref List<Process> Proces)
        {
            Process[] AllProcess = Process.GetProcesses();
            foreach (Process Var in AllProcess)
            {
                Proces.Add(Var);
            }
        }
        /// <summary>
        /// Fill List with Specific Process 
        /// </summary>
        /// <param name="Name">Process Name</param>
        /// <param name="Proces">List that Fill With Selected Process</param>
        public void GetSpecificProcess(string Name, ref List<Process> Proces)
        {
            Process[] AllProcess = Process.GetProcessesByName(Name);
            foreach(Process Var in  AllProcess)
            {
                Proces.Add(Var);
            }
        }
        public Process GetSpecificProcess(int ID)
        {
            Process AllProcess = Process.GetProcessById(ID);
            //foreach (Process Var in AllProcess)
            //{
            //    Proces.Add(Var);
            //}
            return AllProcess;
        }
        public void StopRiseWatching()
        {
            StartWatcher.Stop();
        }
        public void StopFallWatching()
        {
            StopWatcher.Stop();
        }
    }
}

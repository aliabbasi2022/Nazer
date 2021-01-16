using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;//Provides access to a rich set of management information and management events about the system, 
using System.Diagnostics;//Provides classes that allow you to interact with system processes, event logs, and performance counters.


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
    
 
        ///// <summary>
        ///// Listen to Incoming Stop Process Event and Pass Event to Specific Function
        ///// </summary>

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
namespace Spare
{
    class Program
    {
        protected ManagementEventWatcher StartWatcher;// the wathcer that looking for process that started and call function;
        protected ManagementEventWatcher StopWatcher;// the wathcer that looking for process that stoped and call function;
        static void Main(string[] args)
        {
            StartWatcher = new ManagementEventWatcher();
            StopWatcher = new ManagementEventWatcher();
        }
    }
}

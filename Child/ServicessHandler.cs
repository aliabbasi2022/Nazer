using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;//Provides classes that allow you to implement, install, and control Windows service applications. 

namespace Child
{
    
    public class ServicessHandler 
    {
        ServiceController [] services;
        ServiceController SC;
        public void Start(string ServiceName)
        {
            services = ServiceController.GetDevices();
            SC = new ServiceController();
            SC.ServiceName = ServiceName;
            if(SC.Status == ServiceControllerStatus.Stopped)
            {
                SC.Start();
            }
      

        }
    }
}

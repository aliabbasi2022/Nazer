using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpareWF
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();//Enables visual styles for the application.
            Application.SetCompatibleTextRenderingDefault(false);//Sets the program to the UseCompatibleTextRendering attribute defined in specific controls. Which is disabled here
            Application.Run(new Form1());
        }
    }
}

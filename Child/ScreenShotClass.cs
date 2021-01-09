using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; // namespace of process
using System.Windows.Forms;
using System.Drawing; //nameSpace of Bitmap
using System.Threading;
using System.Runtime.InteropServices; // nameSpace of dll
using System.Drawing.Imaging; // nameSpace of PixelFprmat
using Microsoft.Win32;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace Child
{
    class ScreenShotClass
    {
        public Semaphore SCSM;
        public Semaphore SCSM1;
        public ScreenShotClass()
        {
            SCSM = new Semaphore(1, 1);
            SCSM1 = new Semaphore(1, 1);
        }
        public void CaptureApplication(string procName)
        { //ScreenShot faghat az yek barname k baze !
            var proc = Process.GetProcessesByName(procName)[0];
            var rect = new User32.Rect();
            User32.GetWindowRect(proc.MainWindowHandle, ref rect);
            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;
            
            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb); // ijade yek Bitmap ba andazeE k ma mikhaym
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.CopyFromScreen(rect.left, rect.top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
            // un objecti k az graphics sakhtimo miad andazasho moshakhas mikone az screen ax mindaze mirize tu Bitmap
            bmp.Save("C:\\tmp\\test.png", ImageFormat.Png);
        }
        /// <summary>
        /// tabe User32 ro ijad mikone k kolle safharo shamel mishe
        /// ba ijad Bitmap screen un lahzaro tuye object bitmap store mikone
        /// dar akhar bmp ro tuye Databasei k bhsh daDm zakhire mikone
        /// </summary>
        /// 
        private class User32
        {
            [StructLayout(LayoutKind.Sequential)] //mesl nameSpace mimune !!
            public struct Rect
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            [DllImport("User32.dll")] //mesl nameSpace mimune !!
            public static extern IntPtr GetWindowRect(IntPtr hwnd, ref Rect rect);
        }

        /// <summary>
        /// aval ye object az Bitmap o Graphics misaze
        /// bad miyad kolle safharo, location sho mizare o screenshot migire azashun
        /// dar akhar tuye data base save mikone 
        /// </summary>
        public string FullScreenShot(int Type,ref byte[]Result )
        {
            int Width = 0;
            int Height= 0;

            Size PicSize = new Size();
            string RegPath = @"SYSTEM\CurrentControlSet\Control\UnitedVideo\CONTROL\VIDEO";
            RegistryKey RegKey = Registry.LocalMachine;
            //string[] D = RegKey.GetSubKeyNames();
            RegistryKey RootKey = RegKey.OpenSubKey(RegPath);
            RegPath += ("\\" +RootKey.GetSubKeyNames()[0]);
            using (Microsoft.Win32.RegistryKey key = RegKey.OpenSubKey(RegPath))
            {
                using (RegistryKey subkey = key.OpenSubKey("0000"))
                {
                    try
                    {
                        
                        Width = (int)subkey.GetValue("DefaultSettings.XResolution");
                        Height = (int)subkey.GetValue("DefaultSettings.YResolution");
                    }
                    catch (Exception ee)
                    {

                    }

                }
            }
            PicSize.Width = Width;
            PicSize.Height = Height;
            //Create a new Bitmap
            var bmpScreenshot = new Bitmap(Width,
                Height, PixelFormat.Format32bppArgb);

            //Create a graphics object from the Bitmap
            var gfxScreenShot = Graphics.FromImage(bmpScreenshot);
            SCSM.WaitOne();
            //take the screenShot from the upper left corner to the right bottom corner
            gfxScreenShot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X
                , Screen.PrimaryScreen.Bounds.Y
                , 0, 0, PicSize, CopyPixelOperation.SourceCopy);
            SCSM.Release();
            //Savethe ScreenShot to my DB
            if(Type == 0)
            {
                //bmpScreenshot.Save(Application.StartupPath + "\\Screenshot.png", ImageFormat.Png);
                Form1.DataBaseAgent.SelectData("ScreenShot", ref Form1.DS, "ScreenShot");
                DataRow Row = Form1.DS.Tables["ScreenShot"].NewRow();
                Row["ScreenShot"] = imageToByteArray(bmpScreenshot);
                Row["Date"] = DateTime.Now;
                Form1.DS.Tables["ScreenShot"].Rows.Add(Row);
                Form1.DataBaseAgent.InsertData(Form1.DS.Tables["ScreenShot"]);
                Form1.DS.Tables["ScreenShot"].AcceptChanges();
                return "";
                //
            }
            else
            {
                //bmpScreenshot.Save("E:\\we.jpeg");
                MemoryStream ms = new MemoryStream();
                bmpScreenshot.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                Result = ms.ToArray();
                //return imageToByteArray(bmpScreenshot); 
                return Convert.ToString(Result);
            }
        }
        public string imageToByteArray(System.Drawing.Image imageIn)
        {
            SCSM1.WaitOne();
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //var str = System.Text.Encoding.ASCII.GetString(ms.ToArray());
            SCSM1.Release();
            return "";
            
        }



    }




}

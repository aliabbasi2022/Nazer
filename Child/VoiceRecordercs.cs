using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Child
{
    class VoiceRecordercs
    {
        public bool Runing = false;
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);
        /// <summary>
        /// Stop And Save Recorded voice in File
        /// </summary>
        /// <param name="Path">Directory Path that ypu want to Save Voice File</param>
        /// <param name="FileName">File Name </param>
        /// <param name="FileFormat">Format that youy want to Save Voice</param>
        public void StopRecording(string Path , string FileName , string FileFormat)
        {
            Runing = false;
            mciSendString(@"save recsound " + Path +FileName + "." + FileFormat, "", 0, 0);
            mciSendString("close recsound ", "", 0, 0);
        }
        /// <summary>
        /// Start Recording Voice 
        /// </summary>
        public void StartRecording()
        {
            Runing = true;
            mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
            mciSendString("record recsound", "", 0, 0);
        }
    }
}

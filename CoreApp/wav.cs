using System;
using System.Runtime.InteropServices;

namespace ca.CoreApp
{
    public class WINMM
    {
        //API Sonido
        [DllImport("winmm.dll")]
        private static extern bool PlaySound(string lpszName, int hModule, int dwFlags); 

        public const int SND_SYNC = 0x00000000; // Wait until finished playing
        public const int SND_ASYNC = 0x00000001; // Continue in program
        public const int SND_NODEFAULT = 0x00000002; // No default sound if not found
        public const int SND_NOSTOP = 0x00000010; // Play only if not already busy
        public const int SND_LOOP = 0x00000008;
        // Play it again Sam, and again... call again with null string to stop

        public bool Play(string fileName)
        {
            return PlaySoundW(fileName, SND_ASYNC);
        }

        [DllImport("winmm.dll", EntryPoint = "sndPlaySoundA")]
        public extern static bool PlaySoundW(string lpszName, int dwFlags);
    }
}

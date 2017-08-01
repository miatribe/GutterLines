using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace GutterLines
{
    public class MemRead
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr process, IntPtr baseAddress, [Out] byte[] buffer, int size, out IntPtr bytesRead);

        private int processNum;
        private Process curProcess;
        private const int latAddress = 0x00D26694;
        private const int lonAddress = 0x00D26698;
        private const int nameAddress = 0x00D3CBE0;

        public void GetProcess()
        {
            try
            {
                var prosesses = Process.GetProcessesByName("Ragexe");
                if (processNum >= prosesses.Count()) processNum = 0;
                curProcess = prosesses[processNum++];
            }
            catch
            {
                processNum = 0;
                curProcess = null;
            }
        }

        public GameInfo GetValues()
        {
            try
            {
                var lat = ReadInt(curProcess.Handle, (IntPtr)latAddress);
                var lon = ReadInt(curProcess.Handle, (IntPtr)lonAddress);
                var name = ReadString(curProcess.Handle, (IntPtr)nameAddress);

                return new GameInfo
                {
                    Name = name,
                    Lat = lat,
                    Lon = lon
                };
            }
            catch
            {
                return null;
            }
        }

        public static string ReadString(IntPtr process, IntPtr baseAddress)
        {
            var buffer = new byte[8];
            ReadProcessMemory(process, baseAddress, buffer, 8, out IntPtr bytesRead);
            return System.Text.Encoding.Default.GetString(buffer).Replace("\0",string.Empty);
        }

        public static int ReadInt(IntPtr process, IntPtr baseAddress)
        {
            var buffer = new byte[4];
            ReadProcessMemory(process, baseAddress, buffer, 4, out IntPtr bytesRead);
            return BitConverter.ToInt32(buffer, 0);
        }
    }
}

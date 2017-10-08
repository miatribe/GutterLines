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
        private IntPtr latAddress;
        private IntPtr lonAddress;
        private IntPtr nameAddress;

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
            if (curProcess != null)
            {
                SigScan sigScan = new SigScan {Process = curProcess, DumpSize = 0x5B8D80};
                latAddress = sigScan.FindAddress(new byte[] { 0x89, 0x0D, 0x00, 0x00, 0x00, 0x00, 0x89, 0x3D, 0x00, 0x00, 0x00, 0x00, 0x85, 0xD2 }, "xx????xx????xx", 2);
                lonAddress = latAddress + 4;
                nameAddress = sigScan.FindAddress(new byte[] { 0x0F, 0xB6, 0x84, 0x0F, 0x00, 0x00, 0x00, 0x00, 0x30, 0x81 }, "xxxx????xx", 4);
            }
        }

        public GameInfo GetValues()
        {
            try
            {
                var lat = ReadInt(curProcess.Handle, latAddress);
                var lon = ReadInt(curProcess.Handle, lonAddress);
                var name = ReadString(curProcess.Handle, nameAddress);
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

        private static string ReadString(IntPtr process, IntPtr baseAddress)
        {
            var buffer = new byte[8];
            ReadProcessMemory(process, baseAddress, buffer, 8, out IntPtr bytesRead);
            return System.Text.Encoding.Default.GetString(buffer).Replace("\0",string.Empty);
        }

        private static int ReadInt(IntPtr process, IntPtr baseAddress)
        {
            var buffer = new byte[4];
            ReadProcessMemory(process, baseAddress, buffer, 4, out IntPtr bytesRead);
            return BitConverter.ToInt32(buffer, 0);
        }
    }
}

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
        private int latAddress;
        private int lonAddress;
        private int nameAddress;

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

        public void UpdateAddresses()
        {
            if (curProcess == null) return;
            var roStart = curProcess.MainModule.BaseAddress;

            var latlonBaseOffset = 0x002BCD74;
            var latOffsets = new[] { 0x8 };
            var lonOffsets = new[] { 0xC };
            var nameBaseOffset = 0x0039D7A4;
            var nameOffsets = new[] { 0x0 };
            var latlonBaseAddress = roStart.ToInt32() + latlonBaseOffset;

            latAddress = GetRealAddress(curProcess.Handle, latlonBaseAddress, latOffsets);
            lonAddress = GetRealAddress(curProcess.Handle, latlonBaseAddress, lonOffsets);

            var nameBaseAddress = roStart.ToInt32() + nameBaseOffset;
            nameAddress = GetRealAddress(curProcess.Handle, nameBaseAddress, nameOffsets);
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

        public static int GetRealAddress(IntPtr process, int baseAddress, int[] offsets)
        {
            var address = baseAddress;
            foreach (var offset in offsets)
            {
                address = ReadInt(process, (IntPtr)address) + offset;
            }
            return address;
        }

    }
}

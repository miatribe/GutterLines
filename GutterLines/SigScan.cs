using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GutterLines
{
    class SigScan
    {
        private byte[] _MemoryDump;
        public Process Process { get; set; }
        public IntPtr StartingAddress { get; set; }
        public int DumpSize { get; set; }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr process, IntPtr baseAddress, [Out] byte[] buffer, int size, out IntPtr bytesRead);

        public IntPtr FindAddress(byte[] bytePattern, string mask, int bytesToSkip)
        {
            try
            {
                if (!GetMemoryDump()) return IntPtr.Zero;
                for (int i = 0; i < _MemoryDump.Length; i++)
                {
                    if (CheckMask(i, bytePattern, mask)) return GetAddressAtAddress((StartingAddress + i) + bytesToSkip);
                }
                return IntPtr.Zero;
            }
            catch
            {
                return IntPtr.Zero;
            }
        }

        private bool CheckMask(int bytePosition, byte[] bytePattern, string mask)
        {
            for (int i = 0; i < bytePattern.Length; i++)
            {
                if (mask[i] == 'x' && bytePattern[i] != _MemoryDump[i + bytePosition]) return false;
            }
            return true;
        }

        private bool GetMemoryDump()
        {
            _MemoryDump = new byte[DumpSize];
            return ReadProcessMemory(Process.Handle, StartingAddress, _MemoryDump, DumpSize, out IntPtr BytesRead);
        }

        private IntPtr GetAddressAtAddress(IntPtr baseAddress)
        {
            var buffer = new byte[4];
            ReadProcessMemory(Process.Handle, baseAddress, buffer, 4, out IntPtr bytesRead);
            return (IntPtr)BitConverter.ToInt32(buffer, 0);
        }
    }
}

using System.Runtime.InteropServices;

namespace MemoryAccess.Core
{
    public class PsxRam
    {
        private PsxRam() { }
        
        public PsxRam(ProcessMemory mem)
        {
            byte[] sig =
            [
                0xA0, 0x00, 0x0A, 0x24, 0x08, 0x00, 0x40, 0x01,
                0x44, 0x00, 0x09, 0x24, 0x00, 0x00, 0x00, 0x00,
                0xA0, 0x00, 0x0A, 0x24, 0x08, 0x00, 0x40, 0x01,
                0x49, 0x00, 0x09, 0x24, 0x00, 0x00, 0x00, 0x00,
                0xA0, 0x00, 0x0A, 0x24, 0x08, 0x00, 0x40, 0x01,
                0x70, 0x00, 0x09, 0x24, 0x00, 0x00, 0x00, 0x00,
                0xA0, 0x00, 0x0A, 0x24, 0x08, 0x00, 0x40, 0x01,
                0x72, 0x00, 0x09, 0x24, 0x00, 0x00, 0x00, 0x00
            ];

            Base = ScanForPattern(mem.Handle, sig);
            if (Base == IntPtr.Zero)
            {
                throw new Exception("PSX RAM base not found. Signature mismatch or DuckStation build not compatible.");
            }

            Base -= 0x90800;
            Console.WriteLine($"PSX RAM base dynamically calculated: 0x{Base.ToInt64():X}");
        }
        
        public static PsxRam Empty { get; } = new EmptyPsxRam();
        
        public IntPtr Base { get; }

        public virtual IntPtr A(int offset) => Base + offset;

        private static IntPtr ScanForPattern(IntPtr processHandle, byte[] pattern)
        {
            IntPtr address = IntPtr.Zero;
            while (VirtualQueryEx(processHandle, address, out MemoryBasicInformation mbi, Marshal.SizeOf<MemoryBasicInformation>()) != 0)
            {
                bool readable = (mbi.State == MEM_COMMIT) &&
                                ((mbi.Protect & PAGE_READWRITE) != 0 || (mbi.Protect & PAGE_EXECUTE_READWRITE) != 0);

                if (readable)
                {
                    byte[] buffer = new byte[(int)mbi.RegionSize];
                    if (ReadProcessMemory(processHandle, mbi.BaseAddress, buffer, buffer.Length, out _))
                    {
                        int index = IndexOf(buffer, pattern);
                        if (index >= 0)
                        {
                            return mbi.BaseAddress + index;
                        }
                    }
                }

                address = mbi.BaseAddress + mbi.RegionSize;
            }

            return IntPtr.Zero;
        }

        private static int IndexOf(byte[] buffer, byte[] pattern)
        {
            for (int i = 0; i <= buffer.Length - pattern.Length; i++)
            {
                bool match = true;
                for (int j = 0; j < pattern.Length; j++)
                {
                    if (buffer[i + j] == pattern[j])
                    {
                        continue;
                    }

                    match = false;
                    break;
                }

                if (match)
                {
                    return i;
                }
            }

            return -1;
        }

        // WinAPI constants
        private const uint MEM_COMMIT = 0x1000;
        private const uint PAGE_READWRITE = 0x04;
        private const uint PAGE_EXECUTE_READWRITE = 0x40;

        // WinAPI structures and functions
        [StructLayout(LayoutKind.Sequential)]
        private struct MemoryBasicInformation
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public uint AllocationProtect;
            public IntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }

        [DllImport("kernel32.dll")]
        private static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MemoryBasicInformation lpBuffer, int dwLength);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        private class EmptyPsxRam : PsxRam
        {
            public override IntPtr A(int offset) => IntPtr.Zero;
        }
    }
}
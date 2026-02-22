using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MemoryAccess.Core;

public class ProcessMemory
{
    private ProcessMemory()
    {
    }

    public ProcessMemory(Process process)
    {
        Handle = OpenProcess(0x1F0FFF, false, process.Id);
        if (Handle == IntPtr.Zero)
        {
            throw new Exception("Failed to open process");
        }
    }

    public IntPtr Handle { get; }

    public static ProcessMemory Empty { get; } = new EmptyProcessMemory();

    public virtual byte ReadByte(IntPtr addr)
    {
        byte[] buf = new byte[1];
        ReadProcessMemory(Handle, addr, buf, buf.Length, out _);
        return buf[0];
    }

    public virtual byte[] ReadBytes(IntPtr addr, int length)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(length);

        byte[] buffer = new byte[length];

        bool success = ReadProcessMemory(
            Handle,
            addr,
            buffer,
            length,
            out int bytesRead);

        if (!success)
        {
            int error = Marshal.GetLastWin32Error();

            throw new System.ComponentModel.Win32Exception(error, $"ReadProcessMemory failed at address 0x{addr.ToInt64():X}");
        }

        if (bytesRead != length)
        {
            throw new IOException($"Partial memory read at 0x{addr.ToInt64():X}. Requested {length} bytes, received {bytesRead}.");
        }

        return buffer;
    }

    public virtual short ReadInt16(IntPtr addr)
    {
        byte[] buf = new byte[2];
        ReadProcessMemory(Handle, addr, buf, buf.Length, out _);
        return BitConverter.ToInt16(buf);
    }

    public virtual int ReadInt32(IntPtr addr)
    {
        byte[] buf = new byte[4];
        ReadProcessMemory(Handle, addr, buf, buf.Length, out _);
        return BitConverter.ToInt32(buf);
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool ReadProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        [Out] byte[] lpBuffer,
        int dwSize,
        out int lpNumberOfBytesRead
    );

    private class EmptyProcessMemory : ProcessMemory
    {
        public override byte ReadByte(IntPtr addr) => 0;
        public override short ReadInt16(IntPtr addr) => 0;
        public override int ReadInt32(IntPtr addr) => 0;

        public override byte[] ReadBytes(IntPtr addr, int length) => new byte[length];
    }
}
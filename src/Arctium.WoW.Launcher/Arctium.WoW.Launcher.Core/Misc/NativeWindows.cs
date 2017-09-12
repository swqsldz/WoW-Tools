// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using Arctium.WoW.Launcher.Core.Constants;
using Arctium.WoW.Launcher.Core.Structures;

namespace Arctium.WoW.Launcher.Core.Misc
{
    public class NativeWindows
    {
        /// kernel32.dll
        // Process
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, ref StartupInfo lpStartupInfo, out ProcessInformation lpProcessInformation);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern IntPtr NtResumeProcess(IntPtr ProcessHandle);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern IntPtr NtSuspendProcess(IntPtr ProcessHandle);

        // Thread
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenThread(ThreadAccessFlags dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int ResumeThread(IntPtr hThread);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int SuspendThread(IntPtr hThread);

        // Memory
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpBaseAddress, out MemoryBasicInformation mbi, int dwSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flNewProtect, out uint lpflOldProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", EntryPoint = "FlushInstructionCache", SetLastError = true)]
        public static extern bool FlushInstructionCache(IntPtr hProcess, IntPtr lpBaseAddress, uint dwSize);

        /// ntdll.dll
        // Process
        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern NtStatus NtQueryInformationProcess(IntPtr hProcess, int pic, ref ProcessBasicInformation pbi, int cb, out int pSize);

        // Page/View
        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern NtStatus NtCreateSection(ref IntPtr sectionHandle, uint accessMask, IntPtr zero, ref LargeInteger maximumSize, uint protection, uint allocationAttributes, IntPtr zero2);
        
        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern NtStatus NtMapViewOfSection(IntPtr sectionHandle, IntPtr proccessHandle, ref IntPtr baseAddress, IntPtr zero, uint regionSize, out LargeInteger sectionOffset, out uint viewSize, uint viewSection, IntPtr zero2, int protection);
        
        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern NtStatus NtUnmapViewOfSection(IntPtr processHandle, IntPtr baseAddress);
    }
}

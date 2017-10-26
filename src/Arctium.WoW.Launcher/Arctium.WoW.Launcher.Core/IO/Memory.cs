// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using Arctium.WoW.Launcher.Core.Constants;
using Arctium.WoW.Launcher.Core.Misc;
using Arctium.WoW.Launcher.Core.Structures;

using static Arctium.WoW.Launcher.Core.Misc.NativeWindows;

namespace Arctium.WoW.Launcher.Core.IO
{
    public class Memory
    {
        public bool Initialized { get; private set; }
        public int BaseViewSize { get; private set; }

        public IntPtr ProcessHandle { get; }
        public IntPtr BaseAddress   { get; }
        public int PtrSize          { get; }

        ProcessBasicInformation peb;

        public Memory(IntPtr processHandle)
        {
            ProcessHandle = processHandle;
            PtrSize = IntPtr.Size;

            if (processHandle == IntPtr.Zero)
                throw new InvalidOperationException("No valid process found.");
            
            BaseAddress = ReadImageBaseFromPEB(processHandle);

            if (BaseAddress == IntPtr.Zero)
                throw new InvalidOperationException("Error while reading PEB data.");

            Initialized = true;
        }

        public IntPtr Read(IntPtr address)
        {
            try
            {
                var buffer = new byte[IntPtr.Size];

                if (ReadProcessMemory(ProcessHandle, address, buffer, buffer.Length, out var dummy))
                    return buffer.ToIntPtr();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return IntPtr.Zero;
        }

        public IntPtr Read(long address) => Read(new IntPtr(address));

        public byte[] Read(IntPtr address, int size)
        {
            try
            {
                var buffer = new byte[size];

                if (ReadProcessMemory(ProcessHandle, address, buffer, size, out var dummy))
                    return buffer;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public byte[] Read(long address, int size) => Read(new IntPtr(address), size);

        public void Write(IntPtr address, byte[] data, int protect = 0x80)
        {
            try
            {
                VirtualProtectEx(ProcessHandle, address, (uint)data.Length, (uint)protect, out var oldProtect);
               
                WriteProcessMemory(ProcessHandle, address, data, data.Length, out var written);

                FlushInstructionCache(ProcessHandle, address, (uint)data.Length);
                VirtualProtectEx(ProcessHandle, address, (uint)data.Length, oldProtect, out oldProtect);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Write(long address, byte[] data, int protect = 0x80) => Write(new IntPtr(address), data, protect);

        public bool RemapView(IntPtr viewAddress, int viewSize, MemProtection newProtection = MemProtection.ExecuteWriteCopy)
        {
            // Suspend before remapping to prevent crashes.
            NtSuspendProcess(ProcessHandle);

            var viewBackup = Read(viewAddress, viewSize);

            if (viewBackup != null)
            {
                var newViewHandle = IntPtr.Zero;
                var maxSize = new LargeInteger { Quad = viewSize };

                if (NtCreateSection(ref newViewHandle, 0xF001F, IntPtr.Zero, ref maxSize, 0x40u, 0x8000000, IntPtr.Zero) == NtStatus.Success &&
                    NtUnmapViewOfSection(ProcessHandle, viewAddress) == NtStatus.Success)
                {
                    // Map the view with our new protection
                    var viewBase = viewAddress;

                    if (NtMapViewOfSection(newViewHandle, ProcessHandle, ref viewBase, IntPtr.Zero, (uint)viewSize, out var viewOffset,
                                           out var newViewSize, 2, IntPtr.Zero, (int)newProtection) == NtStatus.Success &&
                        WriteProcessMemory(ProcessHandle, viewAddress, viewBackup, viewSize, out var dummy))
                    {
                        NtResumeProcess(ProcessHandle);
                        return true;
                    }

                    Console.WriteLine("Error while mapping the view with the given protection.");
                }
            }
            else
                Console.WriteLine("Error while creating the view backup.");

            NtResumeProcess(ProcessHandle);

            return false;
        }

        public bool RemapViewBase(MemProtection newProtection = MemProtection.ExecuteWriteCopy)
        {
            var mbi = new MemoryBasicInformation();

            if (VirtualQueryEx(ProcessHandle, BaseAddress, out mbi, mbi.Size) != 0)
                return RemapView(mbi.BaseAddress, mbi.RegionSize.ToInt32(), newProtection);

            return false;
        }

        /// Static functions.
        public static bool ResumeThread(int threadId)
        {
            var threadHandle = OpenThread(ThreadAccessFlags.SuspendResume, false, (uint)threadId);

            if (threadHandle != IntPtr.Zero)
                return NativeWindows.ResumeThread(threadHandle) != -1;

            return false;
        }

        public static bool ResumeThreads(Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                if (!ResumeThread(thread.Id))
                    return false;
            }

            return true;
        }

        public static bool SuspendThread(int threadId)
        {
            var threadHandle = OpenThread(ThreadAccessFlags.SuspendResume, false, (uint)threadId);

            if (threadHandle != IntPtr.Zero)
                return NativeWindows.SuspendThread(threadHandle) != -1;

            return false;
        }

        public static bool SuspendThreads(Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                if (!SuspendThread(thread.Id))
                    return false;
            }

            return true;
        }

        /// Private functions.
        IntPtr ReadImageBaseFromPEB(IntPtr processHandle)
        {
            try
            {
                if (NtQueryInformationProcess(processHandle, 0, ref peb, peb.Size, out int sizeInfoReturned) == NtStatus.Success)
                    return Read(peb.PebBaseAddress.ToInt64() +  (IntPtr.Size == 4 ? 0x8 : 0x10));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return IntPtr.Zero;
        }
    }
}

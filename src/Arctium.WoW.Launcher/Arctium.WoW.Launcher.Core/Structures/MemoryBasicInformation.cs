// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using Arctium.WoW.Launcher.Core.Constants;

namespace Arctium.WoW.Launcher.Core.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MemoryBasicInformation
    {
        public IntPtr BaseAddress;
        public IntPtr AllocationBase;
        public MemProtection AllocationProtect;
        public IntPtr RegionSize;
        public MemState State;
        public MemProtection Protect;
        public MemType Type;

        public int Size => Marshal.SizeOf(typeof(MemoryBasicInformation));
    }
}

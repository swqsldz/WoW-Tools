// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace Arctium.WoW.Launcher.Core.Structures
{
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct LargeInteger
    {
        [FieldOffset(0)]
        public long Quad;
        [FieldOffset(0)]
        public uint Low;
        [FieldOffset(4)]
        public int High;

        public int Size => Marshal.SizeOf(typeof(LargeInteger));
    }
}

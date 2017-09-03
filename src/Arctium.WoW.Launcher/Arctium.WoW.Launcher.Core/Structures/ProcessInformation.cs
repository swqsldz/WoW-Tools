// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace Arctium.WoW.Launcher.Core.Structures
{
	public struct ProcessInformation
	{
		public IntPtr ProcessHandle;
		public IntPtr ThreadHandle;
		public uint ProcessId;
		public uint ThreadId;

        public int Size => Marshal.SizeOf(typeof(ProcessInformation));
	}
}

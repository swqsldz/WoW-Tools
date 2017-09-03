// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Arctium.WoW.Launcher.Core.Constants
{
    [Flags]
    public enum ProcessAccessFlags : uint
	{
		Terminate               = 0x00000001,
		CreateThread            = 0x00000002,
		VirtualMemoryOperation  = 0x00000008,
		VirtualMemoryRead       = 0x00000010,
		VirtualMemoryWrite      = 0x00000020,
		DuplicateHandle         = 0x00000040,
		CreateProcess           = 0x00000080,
		SetQuota                = 0x00000100,
		SetInformation          = 0x00000200,
		QueryInformation        = 0x00000400,
		QueryLimitedInformation = 0x00001000,
		Synchronize             = 0x00100000,

		All = 0x001F0FFF,
	}
}

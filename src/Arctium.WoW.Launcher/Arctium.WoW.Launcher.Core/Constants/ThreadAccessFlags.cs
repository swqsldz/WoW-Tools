// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Arctium.WoW.Launcher.Core.Constants
{
    [Flags]
    public enum ThreadAccessFlags
    {
		Terminate           = 0x0001,
		SuspendResume       = 0x0002,
		GetContext          = 0x0008,
		SetContext          = 0x0010,
        SetInformation      = 0x0020,
        QueryInformation    = 0x0040,
		SetThreadToken      = 0x0080,
		Impersonate         = 0x0100,
		DirectImpersonation = 0x0200
    }
}

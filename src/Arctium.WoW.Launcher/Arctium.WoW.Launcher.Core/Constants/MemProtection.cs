// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Arctium.WoW.Launcher.Core.Constants
{
    public enum MemProtection
    {
        NoAccess         = 0x001,
        ReadOnly         = 0x002,
        ReadWrite        = 0x004,
        WriteCopy        = 0x008,
        Execute          = 0x010,
        ExecuteRead      = 0x020,
        ExecuteReadWrite = 0x040,
        ExecuteWriteCopy = 0x080,
        Guard            = 0x100,
        NoCache          = 0x200,
        WriteCombine     = 0x400
    }
}

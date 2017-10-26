// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Arctium.WoW.Launcher.Windows
{
    public class Patterns
    {
        public static class Common
        {
            public static short[] Modulus = { 0x91, 0xD5, 0x9B, 0xB7, 0xD4, 0xE1, 0x83, 0xA5 };
        }

        public static class Live
        {
            public static class Win32
            {
                // Initialization pointer pattern.
                public static short[] Init = { 0x8B, -1, -1, -1, -1, -1, 0x85, -1, 0x74, -1, -1, 0x02, -1, -1, -1, 0xE8, -1, -1, -1, -1, 0x5D, 0xC3 };

                // TODO: Remove Connect patch. Arctium only.
                public static short[] Connect    = { 0x74, -1, 0x6A, 0x04, -1, 0x8B, -1, 0xE8 };
                public static short[] CertBundle = { 0x00, -1, 0x8D, -1, -1, -1, 0x68, -1, -1, -1, -1, 0xE8, -1, -1, -1, -1, 0x83, 0xC4, 0x14, 0x84, -1, 0x74 };
                public static short[] Signature  = { 0x00, 0x8D, -1, -1, 0xFF, 0xFF, 0xFF, 0xE8, -1, -1, -1, -1, 0x8A, -1, 0xFF };
            }

            public static class Win64
            {
                // Initialization pointer pattern.
                public static short[] Init = { 0x48, -1, -1, -1, -1, -1, -1, 0x48, -1, -1, 0x74, -1, 0x41, -1, 0x02, 0x00, 0x00, 0x00, 0xE9 };

                // TODO: Remove Connect patch. Arctium only.
                public static short[] Connect    = { 0x74, 0x2A, 0x48, -1, -1, -1, -1, 0x04, 0x00, 0x00, 0x00, 0xE8, -1, -1, -1, -1, 0x48 };
                public static short[] CertBundle = { 0x45, 0x33, 0xC9, 0x48, 0x89, -1, -1, -1, -1, 0x00, 0x00, 0x4C, 0x89, -1, -1, -1, 0x89 };
                public static short[] Signature  = { 0xEB, 0x02, -1, -1, 0x48, -1, -1, -1, -1, -1, -1, 0x48, -1, -1, -1, -1, -1, -1, -1, 0x48, -1, -1, -1, -1, 0xE8 };
            }
        }

        public static class Ptr
        {
            public static class Win64
            {
                // Initialization pointer pattern.
                public static short[] Init = { 0x48, -1, -1, -1, -1, -1, -1, 0x48, -1, -1, 0x74, -1, 0x41, -1, 0x02, 0x00, 0x00, 0x00, 0x48, -1, -1, 0x48 };

                // TODO: Remove Connect patch. Arctium only.
                public static short[] Connect    = { 0x74, 0x2A, 0x48, -1, -1, -1, -1, 0x04, 0x00, 0x00, 0x00, 0xE8, -1, -1, -1, -1, 0x48 };
                public static short[] CertBundle = { 0x44, 0x89, 0x74, 0x24, 0x28, 0x48, 0x89, -1, -1, -1, -1, 0x00, 0x00, 0x48, 0x89, 0x44 };
                public static short[] Signature  = { 0x32, -1, 0x48, -1, -1, -1, -1, -1, -1, 0x48, -1, -1, -1, -1, -1, -1, -1, 0x48, -1, -1, -1, -1, 0xE8, -1, -1, -1, -1, 0x48, -1, -1, -1, -1, 0xE8 };
            }
        }
    }
}

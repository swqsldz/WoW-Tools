// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using Arctium.WoW.Launcher.Core.Constants;
using Arctium.WoW.Launcher.Core.Misc;

namespace Arctium.WoW.Launcher.Core.IO
{
    public class Patcher : IDisposable
    {
        public string Binary { get; set; }
        public bool Initialized { get; private set; }
        public BinaryTypes Type { get; private set; }

        public byte[] binary;
        bool success;

        public Patcher(string file)
        {
            Initialized = false;
            success = false;

            using (var stream = new MemoryStream(File.ReadAllBytes(file)))
            {
                Binary = file;
                binary = stream.ToArray();

                if (binary != null)
                {
                    Type = Helpers.GetBinaryType(binary);

                    Initialized = true;
                }
            }
        }

        public bool Patch(byte[] bytes, short[] pattern, long address = 0)
        {
            if (Initialized && (address != 0 || binary.Length >= pattern.Length))
            {
                var offset = pattern == null ? address : binary.FindPattern(pattern);

                if ((offset != 0) && (offset + bytes.Length <= binary.Length))
                {
                    try
                    {
                        for (int i = 0; i < bytes.Length; i++)
                            binary[offset + i] = bytes[i];

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("> Patch done at 0x{0:X}", offset);

                    }
                    catch (Exception ex)
                    {
                        throw new NotSupportedException(ex.Message);
                    }

                    return true;

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("! Wrong patch (invalid pattern?)");
                }
            }

            return false;

        }

        public void Finish()
        {
            success = true;
        }

        public void Dispose()
        {
            if (File.Exists(Binary))
                File.Delete(Binary);

            if (success)
                File.WriteAllBytes(Binary, binary);

            binary = null;
        }
    }
}

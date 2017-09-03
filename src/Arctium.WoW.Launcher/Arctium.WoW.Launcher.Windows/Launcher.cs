// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using Arctium.WoW.Launcher.Core;
using Arctium.WoW.Launcher.Core.Constants;
using Arctium.WoW.Launcher.Core.IO;
using Arctium.WoW.Launcher.Core.Misc;
using Arctium.WoW.Launcher.Core.Structures;

namespace Arctium.WoW.Launcher.Windows
{
    class Launcher
    {
        // Windows only.
        static short[] initPattern;

        // Patterns
        static short[] connectPattern;
        static short[] certBundlePattern;
        static short[] signaturePattern;

        // Patches
        static byte[] connectPatch;
        static byte[] certBundlePatch;
        static byte[] signaturePatch;

        static void Main(string[] args)
        {
            PrintHeader("WoW Client Launcher");

            // TODO: Fix Live/Ptr/Beta detection.
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                initPattern = Patterns.Live.Win64.Init;
                connectPattern = Patterns.Live.Win64.Connect;
                certBundlePattern = Patterns.Live.Win64.CertBundle;
                signaturePattern = Patterns.Live.Win64.Signature;

                connectPatch = Patches.Live.Win64.Connect;
                certBundlePatch = Patches.Live.Win64.CertBundle;
                signaturePatch = Patches.Live.Win64.Signature;

                StartWindows();
            }
            else
                throw new NotSupportedException($"{Environment.OSVersion.Platform} is not supported!");
        }

        static void StartWindows()
        {
            Process process = null;

            try
            {
                var startupInfo = new StartupInfo();
                var processInfo = new ProcessInformation();
                
                // App info
                var curDir = Directory.GetCurrentDirectory();
                var appPath = "";
                
                // Check for existing WoW binaries.
                if (File.Exists($"{ curDir}\\WoW-64.exe"))
                    appPath = $"{curDir}\\WoW-64.exe";
                else if (File.Exists($"{curDir}\\WoWT-64.exe"))
                    appPath = $"{curDir}\\WoWT-64.exe";
                else if (File.Exists($"{curDir}\\WoWB-64.exe"))
                    appPath = $"{curDir}\\WoWB-64.exe";
                
                if (!File.Exists(appPath))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please copy the launcher to your WoW directory.");
                
                    WaitAndExit();
                }
                
                var baseDirectory = Path.GetDirectoryName(curDir);
                
                // Write the cert bundle.
                if (!File.Exists($"{curDir}/ca_bundle.txt.signed"))
                    File.WriteAllBytes($"{curDir}/ca_bundle.txt.signed", Convert.FromBase64String(Patches.Common.CertBundleData));
                
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Starting WoW client...");
                
                if (NativeWindows.CreateProcess(appPath, $"-console -console", IntPtr.Zero, IntPtr.Zero, false, 4U, IntPtr.Zero, null, ref startupInfo, out processInfo))
                {
                    process = Process.GetProcessById((int)processInfo.ProcessId);
                
                    // Wait for process initialization.
                    while (process == null)
                        process = Process.GetProcessById((int)processInfo.ProcessId);
                
                    var memory = new Memory(process.Handle);
                
                    // Get RSA modulus offset from file.
                    var modulusOffset = File.ReadAllBytes(appPath).FindPattern(Patterns.Common.Modulus, memory.BaseAddress.ToInt64()).ToIntPtr();
                    var sectionOffset = memory.Read(modulusOffset, 0x2000).FindPattern(Patterns.Common.Modulus);
                
                    modulusOffset = (modulusOffset.ToInt64() + sectionOffset).ToIntPtr();
                
                    // Be sure that the modulus is written before the client is initialized.
                    while (memory.Read(modulusOffset, 1)[0] != Patches.Common.Modulus[0])
                        memory.Write(modulusOffset, Patches.Common.Modulus);
                
                    Memory.ResumeThreads(process);
                
                    var mbi = new MemoryBasicInformation();
                
                    // Wait for the memory region to be initialized.
                    while (NativeWindows.VirtualQueryEx(process.Handle, memory.BaseAddress, out mbi, mbi.Size) == 0 || mbi.RegionSize.ToInt32() <= 0x1000) { }
                
                    if (mbi.BaseAddress != IntPtr.Zero)
                    {
                        // Get the memory content.
                        var binary = memory.Read(mbi.BaseAddress, mbi.RegionSize.ToInt32());
                        var initOffset = IntPtr.Zero;
                
                        // Search while not initialized.
                        while ((initOffset = binary.FindPattern(initPattern, memory.BaseAddress.ToInt64()).ToIntPtr()) == IntPtr.Zero)
                            binary = memory.Read(mbi.BaseAddress, mbi.RegionSize.ToInt32());
                
                        // Re-read the memory region for each pattern search.
                        var connectOffset = memory.Read(mbi.BaseAddress, mbi.RegionSize.ToInt32()).FindPattern(connectPattern, memory.BaseAddress.ToInt64());
                        var certBundleOffset = memory.Read(mbi.BaseAddress, mbi.RegionSize.ToInt32()).FindPattern(certBundlePattern, memory.BaseAddress.ToInt64());
                        var signatureOffset = memory.Read(mbi.BaseAddress, mbi.RegionSize.ToInt32()).FindPattern(signaturePattern, memory.BaseAddress.ToInt64());
                
                        if (initOffset == IntPtr.Zero || modulusOffset == IntPtr.Zero || connectOffset == 0 ||
                            certBundleOffset == 0 || signatureOffset == 0)
                        {
                            Console.WriteLine($"0x{initOffset.ToInt64():X}");
                            Console.WriteLine($"0x{modulusOffset.ToInt64():X}");
                            Console.WriteLine($"0x{connectOffset:X}");
                            Console.WriteLine($"0x{certBundleOffset:X}");
                            Console.WriteLine($"0x{signatureOffset:X}");

                            process.Dispose();
                            process.Kill();
                
                            WaitAndExit();
                        }
                
                        if (memory.RemapViewBase())
                        {
                            memory.Write(connectOffset, connectPatch);
                            memory.Write(certBundleOffset, certBundlePatch);
                            memory.Write(signatureOffset, signaturePatch);
                
                            Console.WriteLine("Done :) ");
                
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("You can login now.");
                
                            binary = null;
                
                            WaitAndExit();
                        }
                        else
                        {
                            binary = null;
                
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error while launching the client.");

                            process.Dispose();
                            process.Kill();
                
                            WaitAndExit();
                        }
                    }
                }
            }
            catch (Exception)
            {
                process.Dispose();
                process?.Kill();
                
                WaitAndExit();
            }
        }

        static void PrintHeader(string serverName)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(@"_____________World of Warcraft___________");
            Console.WriteLine(@"                   _   _                 ");
            Console.WriteLine(@"    /\            | | (_)                ");
            Console.WriteLine(@"   /  \   _ __ ___| |_ _ _   _ _ __ ___  ");
            Console.WriteLine(@"  / /\ \ | '__/ __| __| | | | | '_ ` _ \ ");
            Console.WriteLine(@" / ____ \| | | (__| |_| | |_| | | | | | |");
            Console.WriteLine(@"/_/    \_\_|  \___|\__|_|\__,_|_| |_| |_|");
            Console.WriteLine(@"           _                             ");
            Console.WriteLine(@"          |_._ _   | __|_ o _._          ");
            Console.WriteLine(@"          |_| | |_||(_||_ |(_| |         ");
            Console.WriteLine("");

            var sb = new StringBuilder();

            sb.Append("_________________________________________");

            var nameStart = (42 - serverName.Length) / 2;

            sb.Insert(nameStart, serverName);
            sb.Remove(nameStart + serverName.Length, serverName.Length);

            Console.WriteLine(sb);
            Console.WriteLine($"{"www.arctium-emulation.org",33}");

            Console.WriteLine();
            Console.WriteLine($"Platform: {Environment.OSVersion.Platform}");
            Console.WriteLine();
        }

        static void WaitAndExit()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Closing in 2 seconds...");

            Thread.Sleep(2000);

            Environment.Exit(0);
        }
    }
}

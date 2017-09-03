// Copyright (c) Arctium Emulation.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Arctium.WoW.Launcher.Core
{
    public static class Extensions
    {
        public static IntPtr ToIntPtr(this byte[] buffer) => new IntPtr(IntPtr.Size == 4 ? BitConverter.ToInt32(buffer, 0) : BitConverter.ToInt64(buffer, 0));
        public static IntPtr ToIntPtr(this long value) => new IntPtr(value);

		public static long FindPattern(this byte[] data, short[] pattern, long baseOffset = 0)
		{
			long matches;

			for (long i = 0; i < data.Length; i++)
			{
				if (pattern.Length > (data.Length - i))
					return 0;

				for (matches = 0; matches < pattern.Length; matches++)
				{
					if ((pattern[matches] != -1) && (data[i + matches] != (byte)pattern[matches]))
						break;
				}

				if (matches == pattern.Length)
					return baseOffset + i;
			}

			return 0;
		}
	}
}

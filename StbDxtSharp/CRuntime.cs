using System;

namespace StbSharp
{
	internal static unsafe class CRuntime
	{
		public static void memcpy(void* a, void* b, long size)
		{
			var ap = (byte*)a;
			var bp = (byte*)b;
			for (long i = 0; i < size; ++i)
			{
				*ap++ = *bp++;
			}
		}

		public static void memcpy(void* a, void* b, ulong size)
		{
			memcpy(a, b, (long)size);
		}

		public static void memset(void* ptr, int value, long size)
		{
			byte* bptr = (byte*)ptr;
			var bval = (byte)value;
			for (long i = 0; i < size; ++i)
			{
				*bptr++ = bval;
			}
		}

		public static void memset(void* ptr, int value, ulong size)
		{
			memset(ptr, value, (long)size);
		}

		public static int abs(int v)
		{
			return Math.Abs(v);
		}

		public static float fabs(double a)
		{
			return (float)Math.Abs(a);
		}
	}
}
using System;

namespace StbSharp
{
	public enum CompressionMode
	{
		None = 0,
		Dithered = 1,
		HighQuality = 2
	}

	public static unsafe partial class StbDxt
	{
		public static void stb__DitherBlock(byte* dest, byte* block)
		{
			int* err = stackalloc int[8];
			var ep1 = err;
			var ep2 = err + 4;
			int ch;
			for (ch = 0; ch < 3; ++ch)
			{
				var bp = block + ch;
				var dp = dest + ch;
				var quantArray = ch == (1) ? stb__QuantGTab : stb__QuantRBTab;
				fixed (byte* quant = quantArray)
				{
					CRuntime.memset(err, 0, (ulong)(8 * sizeof(int)));
					int y;
					for (y = 0; (y) < (4); ++y)
					{
						dp[0] = quant[bp[0] + ((3 * ep2[1] + 5 * ep2[0]) >> 4)];
						ep1[0] = bp[0] - dp[0];
						dp[4] = quant[bp[4] + ((7 * ep1[0] + 3 * ep2[2] + 5 * ep2[1] + ep2[0]) >> 4)];
						ep1[1] = bp[4] - dp[4];
						dp[8] = quant[bp[8] + ((7 * ep1[1] + 3 * ep2[3] + 5 * ep2[2] + ep2[1]) >> 4)];
						ep1[2] = bp[8] - dp[8];
						dp[12] = quant[bp[12] + ((7 * ep1[2] + 5 * ep2[3] + ep2[2]) >> 4)];
						ep1[3] = bp[12] - dp[12];
						bp += 16;
						dp += 16;
						var et = ep1;
						ep1 = ep2;
						ep2 = et;
					}
				}
			}
		}

		private static byte[] CompressDxt(int width, int height, byte[] data, bool hasAlpha, CompressionMode mode)
		{
			if (data.Length != width * height * 4)
			{
				throw new Exception("This method supports only rgba images");
			}

			var osize = hasAlpha ? 16 : 8;
			var result = new byte[width * height * osize / 16];
			byte* block = stackalloc byte[16 * 4];

			fixed (byte* rgba = data)
			{
				fixed (byte* resultPtr = result)
				{
					var p = resultPtr;

					for (var j = 0; j < height; j += 4)
					{
						for (var i = 0; i < width; i += 4)
						{
							for (var y = 0; y < 4; ++y)
							{
								if (j + y >= height) break;
								CRuntime.memcpy(block + y * 16, rgba + width * 4 * (j + y) + i * 4, 16);
							}

							stb_compress_dxt_block(p, block, hasAlpha ? 1 : 0, (int)mode);
							p += hasAlpha ? 16 : 8;
						}
					}
				}
			}

			return result;
		}

		public static byte[] CompressDxt1(int width, int height, byte[] data)
		{
			return CompressDxt(width, height, data, false, CompressionMode.None);
		}

		public static byte[] CompressDxt5(int width, int height, byte[] data)
		{
			return CompressDxt(width, height, data, true, CompressionMode.None);
		}
	}
}
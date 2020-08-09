// Stb.Native.h

#pragma once

using namespace System;
using namespace System::IO;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;
using namespace System::Threading;

#include <stdio.h>
#include <vector>
#include <functional>


#define STB_DXT_IMPLEMENTATION
#include "../../generation/StbDxtSharp.Generator/stb_dxt.h"

namespace StbNative {
	public ref class Native
	{
	public:
		static array<unsigned char> ^ compress_dxt(array<unsigned char> ^input, int w, int h, bool hasAlpha)
		{
			int osize = hasAlpha ? 16 : 8;

			array<unsigned char> ^result = gcnew array<unsigned char>(w * h * osize / 16);

			pin_ptr<unsigned char> ip = &input[0];
			unsigned char *rgba = ip;

			pin_ptr<unsigned char> op = &result[0];
			unsigned char *p = op;

			unsigned char block[16 * 4];
			for (int j = 0; j < h; j += 4)
			{
				int x = 4;
				for (int i = 0; i < w; i += 4)
				{
					int y;
					for (y = 0; y < 4; ++y)
					{
						if (j + y >= h) break;
						memcpy(block + y * 16, rgba + w * 4 * (j + y) + i * 4, x * 4);
					}
					
					stb_compress_dxt_block(p, block, hasAlpha ? 1 : 0, 0);
					p += hasAlpha ? 16 : 8;
				}
			}

			return result;
		}
	};
}

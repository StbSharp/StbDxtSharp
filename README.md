# StbDxtSharp
[![NuGet](https://img.shields.io/nuget/v/StbDxtSharp.svg)](https://www.nuget.org/packages/StbDxtSharp/) [![Build status](https://ci.appveyor.com/api/projects/status/a9g2mnxnd279g2ax?svg=true)](https://ci.appveyor.com/project/RomanShapiro/stbdxtsharp) [![Chat](https://img.shields.io/discord/628186029488340992.svg)](https://discord.gg/ZeHxhCY)

C# port of stb_dxt.h

# FNA/MonoGame Sample Code
```c#
    using (var stream = TitleContainer.OpenStream("image.png"))
    {
      // Load the image using StbImageSharp
      var ir = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

      // Compress it using StbDxtSharp
      var compressedData = StbDxt.CompressDxt5(ir.Width, ir.Height, ir.Data);

      // Create texture from the compressed data
      _texture = new Texture2D(GraphicsDevice, ir.Width, ir.Height, false, SurfaceFormat.Dxt5);
      _texture.SetData(compressedData);
    }
```

# License
Public Domain

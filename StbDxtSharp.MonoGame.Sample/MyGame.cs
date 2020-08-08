using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StbImageSharp;
using StbSharp;

namespace ConsoleApp1
{
	public class MyGame : Game
	{
		private readonly GraphicsDeviceManager _graphics;
		private Texture2D _textureOriginal, _textureDecompressed;
		private SpriteBatch _spriteBatch;

		public MyGame()
		{
			_graphics = new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = 1200,
				PreferredBackBufferHeight = 800
			};
			Window.AllowUserResizing = true;
			IsMouseVisible = true;
		}

		protected override void LoadContent()
		{
			base.LoadContent();

			_spriteBatch = new SpriteBatch(GraphicsDevice);

			using (var stream = TitleContainer.OpenStream("image.png"))
			{
				// Load the image
				var ir = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

				// Compress it
				var compressedData = StbDxt.stb_compress_dxt(ir.Width, ir.Height, ir.Data, true, CompressionMode.HighQuality);

				var decompressed = DxtUtil.DecompressDxt5(compressedData, ir.Width, ir.Height);

				_textureOriginal = new Texture2D(GraphicsDevice, ir.Width, ir.Height);
				_textureOriginal.SetData(ir.Data);

				_textureDecompressed = new Texture2D(GraphicsDevice, ir.Width, ir.Height);
				_textureDecompressed.SetData(decompressed);
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			GraphicsDevice.Clear(Color.Black);

			_spriteBatch.Begin();

			_spriteBatch.Draw(_textureOriginal, Vector2.Zero, Color.White);
			_spriteBatch.Draw(_textureDecompressed, new Vector2(540, 0), Color.White);

			_spriteBatch.End();
		}
	}
}

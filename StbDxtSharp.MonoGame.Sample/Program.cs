using ConsoleApp1;
using System;

namespace StbDxtSharp.MonoGame.Sample
{
	class Program
	{
		static void Main(string[] args)
		{
			using(var game = new MyGame())
			{
				game.Run();
			}
		}
	}
}

using System;
using System.Threading;
using Tetris.Model;
using Tetris.Model.Tetriminos;
using System.Numerics;

namespace Tetris
{
	class Program
	{
		public static Game play = new Game();

		static void Main(string[] args)
		{
			ConsoleKey c;
/*			play.pField = new int[]{
				0,0,0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,2,0,0,0,0,0,
				0,0,0,0,0,0,2,0,0,0,0,0,
				0,0,0,0,0,0,2,2,0,0,0,0,
				0,0,0,0,0,0,2,2,0,0,0,0,
				0,0,0,0,0,0,2,2,2,0,0,0,
				0,0,2,2,0,0,2,2,2,2,0,0,
				2,2,2,2,2,2,2,2,2,2,2,2,
				2,2,2,2,2,2,2,2,2,2,2,2,
			};
*/			play.PrintPiece();
			Show();
			Timer timer = new Timer(TimerCallback, null, 0, 1000);
			while (!play.isGameOver())
			{
				if(Console.KeyAvailable)
				{
					c = Console.ReadKey().Key;

					switch (c)
					{
						case ConsoleKey.UpArrow:
							play.Up();
							break;
						case ConsoleKey.DownArrow:
							play.Down();
							break;
						case ConsoleKey.LeftArrow:
							play.Left();
							break;
						case ConsoleKey.RightArrow:
							play.Right();
							break;
						case ConsoleKey.Enter:
							play.Rotate();
							break;
						case ConsoleKey.P:
							play.SweapPiecePosition();
							play.GetCurrentPiece();
							play.PrintPiece();
							break;
					}
					Show();
				}
			}
			Show();
			Console.Read();
		}

		private static void TimerCallback(Object o)
		{
			if (play.isCurrentPieceFallen())
			{
				int lines = play.CheckLines();
				if (lines > 0)
				{
					play.SetScore(lines);
				}
				play.GetCurrentPiece();
			}
			else
			{
				play.Down();
				Show();
			}
			// Display the date/time when this method got called.
			Console.WriteLine("In TimerCallback: " + DateTime.Now);
			// Force a garbage collection to occur for this demo.
			GC.Collect();
		}

		static void Show()
		{
			Console.Clear();
			for (int y = 0; y < play.nFieldHeight+2; y++)
			{
				for (int x = 0; x < play.nFieldWidth+2; x++)
				{
					switch (play.pField[x + y * (play.nFieldWidth+2)])
					{
						case -1:
							Console.Write("#");
							break;
						case 0:
							Console.Write(" ");
							break;
						case 1:
							Console.Write("A");
							break;
						case 2:
							Console.Write("B");
							break;
						case 3:
							Console.Write("C");
							break;
						case 4:
							Console.Write("D");
							break;
						case 5:
							Console.Write("E");
							break;
						case 6:
							Console.Write("F");
							break;
						case 7:
							Console.Write("G");
							break;
					}
				}
				if (y == 2)
					Console.WriteLine("SCORE : " + play.Score);
				else if (y == 3)
					Console.WriteLine("LEVEL : " + play.Level);
				else if(y == 4)
					Console.WriteLine("LINES : " + play.Lines);
				else
					Console.WriteLine();
			}
			Console.Write(play.isGameOver());
		}
	}
}

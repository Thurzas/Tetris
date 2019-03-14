using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Tetris.Model.Tetriminos;

namespace Tetris.Model
{
	class Game
	{
		public int nFieldHeight;
		public int nFieldWidth;
		public int[] pField;
		public int speed=5000;
		public int score;
		public Tetrimino t;
		private Random rng;
		public Game()
		{
			rng = new Random();
			nFieldHeight = 18;
			nFieldWidth = 12;
			pField = new int[(nFieldHeight+2) * (nFieldWidth+2)];
			for (int x = 0; x < nFieldWidth+2; x++)
			{
				pField[x + (nFieldHeight+1)*(nFieldWidth+2)]=-1;
				pField[x] = -1;
			}

			for (int y = 0; y < nFieldHeight + 2; y++)
			{
				pField[y * (nFieldWidth + 2)] = -1;
				pField[y * (nFieldWidth + 2) + nFieldWidth + 1] = -1;
			}

			for (int y = 1; y < nFieldHeight+1; y++)
			{

				for (int x = 1; x < nFieldWidth+1; x++)
				{
					pField[x + y*(nFieldWidth+2)] = 0;
				}
			}
			score = 0;
			GetCurrentPiece();
		}

		public void GetCurrentPiece()
		{
			int nb=	rng.Next(7);
			switch(nb)
			{
				case 0:
					t = new I(new Vector2((int)((nFieldWidth + 2 ) / 4), 1),0,nb+1);
					break;
				case 1:
					t = new J(new Vector2((int)((nFieldWidth + 2) / 4), 1), 0, nb + 1);
					break;
				case 2:
					t = new L(new Vector2((int)((nFieldWidth + 2) / 4), 1), 0, nb + 1);
					break;
				case 3:
					t = new O(new Vector2((int)((nFieldWidth + 2) / 4), 1), 0, nb + 1);
					break;
				case 4:
					t = new S(new Vector2((int)((nFieldWidth + 2) / 4), 1), 0, nb + 1);
					break;
				case 5:
					t = new T(new Vector2((int)((nFieldWidth + 2) / 4), 1), 0, nb + 1);
					break;
				case 6:
					t = new Z(new Vector2((int)((nFieldWidth + 2) / 4), 1), 0, nb + 1);
					break;
			}
		}

		public void PrintPiece()
		{
			Flood(t.coords, t.pivot, t.colors,t.colors);
		}

		public void SweapPiecePosition()
		{
			Flood(t.coords, t.pivot, t.colors,0);
		}

		public void FloodFill(Tetrimino t, Vector2 pivot, int target, int color, bool[] visited)
		{
			if (pivot.X < 0 || pivot.X > t.Size - 1 || pivot.Y < 0 || pivot.Y > t.Size-1 || visited[(int)(pivot.X + pivot.Y * t.Size)] || t.grid[(int)(pivot.X + pivot.Y * t.Size)] != target)
				return;

			visited[(int)(pivot.X + pivot.Y * t.Size)] = true;
			pField[(int)(t.coords.X + pivot.X + (t.coords.Y + pivot.Y) * (nFieldWidth + 2))] = color;
			FloodFill(t, new Vector2(pivot.X, pivot.Y + 1), target, color, visited);              //NORTH
			FloodFill(t, new Vector2(pivot.X, pivot.Y - 1), target, color, visited);              //SOUTH
			FloodFill(t, new Vector2(pivot.X + 1, pivot.Y), target, color, visited);              //EAST
			FloodFill(t, new Vector2(pivot.X - 1, pivot.Y), target, color, visited);              //WEST
		}

		public void FloodFill(Tetrimino t, Vector2 pivot, int target, bool[] visited,ref bool collide)
		{
			if (pivot.X < 0 || pivot.X > t.Size - 1 || pivot.Y < 0 || pivot.Y > t.Size - 1 || visited[(int)(pivot.X + pivot.Y * t.Size)] || t.grid[(int)(pivot.X + pivot.Y * t.Size)] !=target)
				return;

			visited[(int)(pivot.X + pivot.Y * t.Size)] = true;

			if (t.coords.X < -1 ||
				t.coords.Y < -1 ||
				t.coords.X > nFieldWidth+2 - t.Size  ||
				t.coords.Y > nFieldHeight+2 - t.Size ||
				pField[(int)(t.coords.X + pivot.X + (t.coords.Y + pivot.Y) * ( nFieldWidth + 2 ))] != 0)
			{
				collide = false;
				return;
			}

			FloodFill(t, new Vector2(pivot.X, pivot.Y + 1), target, visited, ref collide);             //NORTH
			FloodFill(t, new Vector2(pivot.X, pivot.Y - 1), target, visited, ref collide);             //SOUTH
			FloodFill(t, new Vector2(pivot.X + 1, pivot.Y), target, visited, ref collide);             //EAST
			FloodFill(t, new Vector2(pivot.X - 1, pivot.Y), target, visited, ref collide);             //WEST
		}

		public void Left()
		{
			if(isCurrentPieceMovable(new Vector2(t.coords.X-1,t.coords.Y)))
			{				
				SweapPiecePosition();
				t.coords.X--;
				PrintPiece();
			}
		}

		public void Right()
		{
			if (isCurrentPieceMovable(new Vector2(t.coords.X + 1, t.coords.Y)))
			{
				SweapPiecePosition();
				t.coords.X++;
				PrintPiece();
			}
		}

		public void Up()
		{
			if (isCurrentPieceMovable(new Vector2(t.coords.X, t.coords.Y-1)))
			{
				SweapPiecePosition();
				t.coords.Y--;
				PrintPiece();
			}
		}

		public void Down()
		{
			if (isCurrentPieceMovable(new Vector2(t.coords.X, t.coords.Y+1)))
			{
				SweapPiecePosition();
				t.coords.Y++;
				PrintPiece();
			}
		}

		public bool isCurrentPieceMovable(Vector2 coords)
		{
			bool[] visited = new bool[16];
			bool res = true;
			Tetrimino tmp = TetrisConsole.Model.Tetriminos.DeepCloner.Clone(t);
			tmp.coords = coords;
			SweapPiecePosition();
			FloodFill(tmp, tmp.pivot, tmp.colors, visited, ref res);
			PrintPiece();
			return res;
		}

		public bool isCurrentPieceRotable()
		{
			bool[] visited = new bool[t.Size * t.Size];
			bool res = true;
			int rotate = 1;
			SweapPiecePosition();
			bool continu = true;
			Tetrimino tmp = TetrisConsole.Model.Tetriminos.DeepCloner.Clone(t);
			while(continu&&rotate<4)
			{
				tmp.Rotate();
				FloodFill(tmp, tmp.pivot, tmp.colors, visited, ref res);
				rotate++;
				continu = !res;
			}
			PrintPiece();
			return res;
		}

		public void Rotate()
		{
			if (isCurrentPieceRotable())
			{
				SweapPiecePosition();
				t.Rotate();
				PrintPiece();
			}
		}

		public void deleteLine(int y)
		{
			SweapPiecePosition();
			for (int Y = y; Y > 0; Y--)
			{
				for (int x = 1; x < nFieldWidth + 1; x++)
				{
					pField[x + (nFieldWidth + 2) * Y] = pField[x + (nFieldWidth + 2) * (Y - 1)];
					pField[x + (nFieldWidth + 2) * 1] = 0;
				}
			}
			PrintPiece();
		}

		public int CheckLines()
		{
			int Line = 0;
			for (int y = 0; y < nFieldHeight+1; y++)
			{
				bool full = true;
				for (int x = 0; x < nFieldWidth + 2; x++)
				{
					if(pField[x + (nFieldWidth + 2) * y]!=-1)
						full = full && pField[x + (nFieldWidth + 2) * y] != 0;
				}
				if(full)
				{
					Line++;
					deleteLine(y);
				}
			}
			return Line;
		}

		public bool isGameOver()
		{
			bool res = false;

			for (int x = 0; x < (nFieldWidth + 2); x++)
			{
				if (pField[x + (nFieldWidth + 2) * 1] > 0)
					res = true;
			}
			return res&&isCurrentPieceFallen();
		}

		public void Flood(Vector2 coords, Vector2 pivot, int target, int color)
		{
			bool[] visited = new bool[t.Size * t.Size];
			FloodFill(t, pivot, target, color, visited);
		}
	
		public void MovePiece()
		{
			while(!isCurrentPieceFallen())
			{
				Down();
			}
		}

		public bool isCurrentPieceFallen()
		{
			bool res = false;
			if (!isCurrentPieceMovable(new Vector2(t.coords.X, t.coords.Y + 1)))
				res = true;
			return res;
		}
	}
}

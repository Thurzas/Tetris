using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
namespace Tetris.Model
{
	[Serializable]
	public abstract class Tetrimino
	{
		public int[] grid;
		public Vector2 coords;
		public int rotateCrt;
		public int colors;
		public Vector2 pivot;
		public int Size;
		public Tetrimino(Vector2 coords, int rotateCrt,int colors)
		{
			this.coords = coords;
			this.rotateCrt = rotateCrt;
			this.colors = colors;
			Size = 4;
			this.grid = new int[Size*Size];
		}

		public int rotate(int x,int y,int r)
		{
			switch(r % 4)
			{
				case 0: return y * Size + x;			//0
				case 1: return Size*(Size-1)+ y - ( x * Size );	//90
				case 2: return Size*Size-1 - ( y * Size ) - x;	//180
				case 3: return Size-1 - y +( x * Size );    //270
				default: return 0;
			}
		}

		public virtual void Rotate(int r)
		{
			int[] tmp = grid;

			for (int x = 0; x < Size; x++)
			{
				for (int y = 0; y < Size; y++)
				{
					tmp[y * Size + x] = grid[rotate(x, y, r)];
				}
			}
			grid = tmp;
		}

		public virtual void Rotate()
		{
			int[] tmp = new int[Size*Size];
			for (int x = 0; x < Size; x++)
			{
				for (int y = 0; y < Size; y++)
				{
					tmp[y * Size + x] = grid[rotate(x, y, 1)];
				}
			}
			grid = tmp;
		}
	}
}

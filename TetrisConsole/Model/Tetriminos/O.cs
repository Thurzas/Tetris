using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Tetris.Model
{
	[Serializable]
	public class O : Tetrimino
	{
		public O(Vector2 coords, int rotateCrt, int colors) : base(coords, rotateCrt, colors)
		{
			this.rotateCrt=-1;
			this.grid = new int[]{
				0,0,0,0,
				0,colors,colors,0,
				0,colors,colors,0,
				0,0,0,0
			};
			pivot = new Vector2(1, 2);
		}
	}
}

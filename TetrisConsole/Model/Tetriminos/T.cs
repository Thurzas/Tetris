using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Tetris.Model
{
	[Serializable]
	public class T : Tetrimino
	{
		public T(Vector2 coords, int rotateCrt, int colors) : base(coords, rotateCrt, colors)
		{
			this.grid = new int[]{
				0,colors,0,
				colors,colors,colors,
				0,0,0
			};
			pivot = new Vector2(1, 1);
			Size = 3;
		}
	}
}

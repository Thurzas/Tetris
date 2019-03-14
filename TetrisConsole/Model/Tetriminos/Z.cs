using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Tetris.Model.Tetriminos
{
	[Serializable]
	class Z : Tetrimino
	{
		public Z(Vector2 coords, int rotateCrt, int colors) : base(coords, rotateCrt, colors)
		{
			this.grid = new int[]{
				colors,colors,0,
				0,colors,colors,
				0,0,0,
			};
			pivot = new Vector2(1,1);
			Size = 3;
		}
	}
}

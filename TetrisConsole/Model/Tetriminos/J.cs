using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Tetris.Model.Tetriminos
{
	[Serializable]
	class J : Tetrimino
	{
		public J(Vector2 coords, int rotateCrt, int colors) : base(coords, rotateCrt, colors)
		{
			grid = new int[]{
				colors,0,0,
				colors,colors,colors,
				0, 0, 0
			};
			pivot = new Vector2(1,1);
			Size = 3;
		}
	}
}

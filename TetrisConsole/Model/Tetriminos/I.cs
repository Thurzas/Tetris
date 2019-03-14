using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Tetris.Model
{
	[Serializable]
	public class I : Tetrimino
	{
		public I(Vector2 coords, int rotateCrt, int colors) : base(coords, rotateCrt, colors)
		{
			this.grid = new int[]{
				0,0,colors,0,
				0,0,colors,0,
				0,0,colors,0,
				0,0,colors,0				
					};
			pivot = new Vector2(2,2);
			rotateCrt = 0;
		}

		public override void Rotate()
		{
			if (rotateCrt == 4)
				rotateCrt = 0;
			
			switch(rotateCrt)
			{
				case 0:
					pivot = new Vector2(2, 2);
					break;

				case 1:
					pivot = new Vector2(1, 2);
					break;

				case 2:
					pivot = new Vector2(1, 1);
					break;
				case 3:
					pivot = new Vector2(2, 2);
					break;

			}
			base.Rotate();
			rotateCrt++;
		}
	}
}

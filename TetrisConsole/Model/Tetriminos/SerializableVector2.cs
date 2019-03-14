using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Numerics;

namespace TetrisConsole.Model.Tetriminos
{
	/// <summary>
	/// Vector2 needs to be serializable in my Tetris
	/// </summary>
	[Serializable]
	public struct SerializableVector2
	{
		public float X;
		public float Y;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public SerializableVector2(float x, float y)
		{
			X = x;
			Y = y;
		}

		public override string ToString()
		{
			return String.Format("[{0}, {1}]", X, Y);
		}

		/// <summary>
		/// Automatic conversion from SerializableVector2 to Vector2
		/// </summary>
		/// <param name="rValue"></param>
		/// <returns></returns>

		public static implicit operator Vector2(SerializableVector2 rValue)
		{
			return new Vector2(rValue.X, rValue.Y);
		}

		/// <summary>
		/// Automatic conversion from Vector2 to SerializableVector2
		/// </summary>
		/// <param name="rValue"></param>
		/// <returns></returns>
		public static implicit operator SerializableVector2(Vector2 rValue)
		{
			return new SerializableVector2(rValue.X, rValue.Y);
		}
	}
}
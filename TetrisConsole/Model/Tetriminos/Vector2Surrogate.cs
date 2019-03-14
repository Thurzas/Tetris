using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Numerics;

namespace TetrisConsole.Model.Tetriminos
{
	sealed class Vector2Surrogate : ISerializationSurrogate
	{
		// Method called to serialize a Vector2 object
		public void GetObjectData(System.Object obj,
								  SerializationInfo info, StreamingContext context)
		{
			Vector2 v2 = (Vector2)obj;
			info.AddValue("X", v2.X);
			info.AddValue("Y", v2.Y);
		}

		// Method called to deserialize a Vector2 object
		public System.Object SetObjectData(System.Object obj,
										   SerializationInfo info, StreamingContext context,
										   ISurrogateSelector selector)
		{
			Vector2 v2 = (Vector2)obj;
			v2.X = (float)info.GetValue("X", typeof(float));
			v2.Y = (float)info.GetValue("Y", typeof(float));
			obj=v2;
			return obj;
		}
	}
}
 
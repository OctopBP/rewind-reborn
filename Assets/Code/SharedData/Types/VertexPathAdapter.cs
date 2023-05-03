using PathCreation;
using UnityEngine;

namespace Rewind.SharedData {
	[GenConstructor]
	public partial class VertexPathAdapter {
		public readonly VertexPath vertexPath;
		public Vector3 getPointAtTime(float time) => vertexPath.GetPointAtTime(time, EndOfPathInstruction.Stop);
	}
}

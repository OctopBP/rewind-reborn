using PathCreation;
using UnityEngine;

public record VertexPathAdapter(VertexPath vertexPath) {
	VertexPath vertexPath { get; } = vertexPath;
	public Vector3 getPointAtTime(float time) => vertexPath.GetPointAtTime(time, EndOfPathInstruction.Stop);
}
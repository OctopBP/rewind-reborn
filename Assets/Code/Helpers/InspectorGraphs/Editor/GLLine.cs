using UnityEngine;

internal static class GLLine
{
	private static bool PointInsideTheRect(Vector2 point, Rect rect) =>
		point.x >= 0 && point.x <= rect.width && point.y >= 0 && point.y <= rect.height;

	private static void VectorToVertex(Vector2 point) => GL.Vertex3(point.x, point.y, 0);

	public static void Draw(Rect rect, float fromX, float fromY, float toX, float toY, Color color)
    {
		GL.Color(color);
		Draw(rect, fromX, fromY, toX, toY);
	}

	public static void Draw(Rect rect, float fromX, float fromY, float toX, float toY) =>
		Draw(rect, new(fromX, fromY), new(toX, toY));

	public static void Draw(Rect rect, Vector2 from, Vector2 to, Color color)
    {
		GL.Color(color);
		Draw(rect, from, to);
	}

	public static void Draw(Rect rect, Vector2 from, Vector2 to)
    {
		if (PointInsideTheRect(from, rect) && PointInsideTheRect(to, rect))
        {
			VectorToVertex(from);
			VectorToVertex(to);
		}
	}
}
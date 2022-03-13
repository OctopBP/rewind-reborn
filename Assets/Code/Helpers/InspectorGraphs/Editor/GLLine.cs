using UnityEngine;

static class GLLine {
	static bool pointInsideTheRect(Vector2 point, Rect rect) =>
		point.x >= 0 && point.x <= rect.width && point.y >= 0 && point.y <= rect.height;

	static void vectorToVertex(Vector2 point) => GL.Vertex3(point.x, point.y, 0);

	public static void draw(Rect rect, float fromX, float fromY, float toX, float toY, Color color) {
		GL.Color(color);
		draw(rect, fromX, fromY, toX, toY);
	}

	public static void draw(Rect rect, float fromX, float fromY, float toX, float toY) =>
		draw(rect, new(fromX, fromY), new(toX, toY));

	public static void draw(Rect rect, Vector2 from, Vector2 to, Color color) {
		GL.Color(color);
		draw(rect, from, to);
	}

	public static void draw(Rect rect, Vector2 from, Vector2 to) {
		if (pointInsideTheRect(from, rect) && pointInsideTheRect(to, rect)) {
			vectorToVertex(from);
			vectorToVertex(to);
		}
	}
}
using UnityEditor;
using UnityEngine;

namespace Rewind.Extensions.Editor {
	public class HandlesExt {
		public static void drawArrow(Vector3 position, float size, float width, Color color, bool flip = false) {
			var r = flip ? Vector3.left : Vector3.right;
			var l = flip ? Vector3.right : Vector3.left;
			var start = position + l * size * .5f;
			var end = position + r * size * .5f;
			var top = position + r * size * .25f + Vector3.up * size * .25f;
			var bottom = position + r * size * .25f + Vector3.down * size * .25f;
			drawLine(start, end, width, color);
			drawLine(top, end, width, color);
			drawLine(bottom, end, width, color);
		}
		
		public static void drawX(Vector3 position, float size, float width, Color color) {
			var topL = position + Vector3.left * size * .5f + Vector3.up * size * .5f;
			var topR = position + Vector3.right * size * .5f + Vector3.up * size * .5f;
			var bottomL = position + Vector3.left * size * .5f + Vector3.down * size * .5f;
			var bottomR = position + Vector3.right * size * .5f + Vector3.down * size * .5f;
			drawLine(topL, bottomR, width, color);
			drawLine(topR, bottomL, width, color);
		}
		
		public static void drawLine(Vector3 start, Vector3 end, float width, Color color) {
			Handles.DrawBezier(start, end, start, end, color, null, width);
		}
	}
}
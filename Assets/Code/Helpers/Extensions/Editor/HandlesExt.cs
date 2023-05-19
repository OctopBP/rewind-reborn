using UnityEditor;
using UnityEngine;

namespace Rewind.Extensions.Editor {
	public class HandlesExt {
		public static void drawArrow(Vector3 position, float size, float width, Color color, bool flip = false) {
			var r = flip ? Vector3.left : Vector3.right;
			var start = position - r * size * .5f;
			var end = position + r * size * .5f;
			var top = position + r * size * .25f + Vector3.up * size * .25f;
			var bottom = position + r * size * .25f - Vector3.up * size * .25f;
			drawLine(start, end, width, color);
			drawLine(top, end, width, color);
			drawLine(bottom, end, width, color);
		}
		
		public static void drawX(Vector3 position, float size, float width, Color color) {
			var halfR = Vector3.right * size * .5f;
			var halfUp = Vector3.up * size * .5f;
			
			var topL = position - halfR + halfUp;
			var topR = position + halfR + halfUp;
			var bottomL = position - halfR - halfUp;
			var bottomR = position + halfR - halfUp;
			
			drawLine(topL, bottomR, width, color);
			drawLine(topR, bottomL, width, color);
		}
		
		public static void drawArrowHeadL(Vector3 position, float size, float width, Color color) {
			var center = position - Vector3.right * size * .25f;
			var top = position + Vector3.right * size * .25f + Vector3.up * size * .5f;
			var bottom = position + Vector3.right * size * .25f - Vector3.up * size * .5f;
			drawLine(top, center, width, color);
			drawLine(center, bottom, width, color);
		}

		public static void drawArrowHeadR(Vector3 position, float size, float width, Color color) {
			var center = position + Vector3.right * size * .25f;
			var top = position - Vector3.right * size * .25f + Vector3.up * size * .5f;
			var bottom = position - Vector3.right * size * .25f - Vector3.up * size * .5f;
			drawLine(top, center, width, color);
			drawLine(center, bottom, width, color);
		}
		
		public static void drawLine(Vector3 start, Vector3 end, float width, Color color) {
			Handles.DrawBezier(start, end, start, end, color, null, width);
		}
	}
}
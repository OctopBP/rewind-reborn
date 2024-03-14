using UnityEditor;
using UnityEngine;

namespace Rewind.Extensions.Editor
{
	public class HandlesExt
    {
		public static void DrawArrow(Vector3 position, float size, float width, Color color, bool flip = false)
        {
			var r = flip ? Vector3.left : Vector3.right;
			var start = position - r * size * .5f;
			var end = position + r * size * .5f;
			var top = position + r * size * .25f + Vector3.up * size * .25f;
			var bottom = position + r * size * .25f - Vector3.up * size * .25f;
			DrawLine(start, end, width, color);
			DrawLine(top, end, width, color);
			DrawLine(bottom, end, width, color);
		}
		
		public static void DrawX(Vector3 position, float size, float width, Color color)
        {
			var halfR = Vector3.right * size * .5f;
			var halfUp = Vector3.up * size * .5f;
			
			var topL = position - halfR + halfUp;
			var topR = position + halfR + halfUp;
			var bottomL = position - halfR - halfUp;
			var bottomR = position + halfR - halfUp;
			
			DrawLine(topL, bottomR, width, color);
			DrawLine(topR, bottomL, width, color);
		}
		
		public static void DrawArrowHeadL(Vector3 position, float size, float width, Color color)
        {
			var center = position - Vector3.right * size * .25f;
			var top = position + Vector3.right * size * .25f + Vector3.up * size * .5f;
			var bottom = position + Vector3.right * size * .25f - Vector3.up * size * .5f;
			DrawLine(top, center, width, color);
			DrawLine(center, bottom, width, color);
		}

		public static void DrawArrowHeadR(Vector3 position, float size, float width, Color color)
        {
			var center = position + Vector3.right * size * .25f;
			var top = position - Vector3.right * size * .25f + Vector3.up * size * .5f;
			var bottom = position - Vector3.right * size * .25f - Vector3.up * size * .5f;
			DrawLine(top, center, width, color);
			DrawLine(center, bottom, width, color);
		}
		
		public static void DrawLine(Vector3 start, Vector3 end, float width, Color color)
		{
			Handles.DrawBezier(start, end, start, end, color, null, width);
		}
	}
}
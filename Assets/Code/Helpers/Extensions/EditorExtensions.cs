using System;
using UnityEngine;

namespace Rewind.Extensions {
	public static class GLExt {
		public static void begin(int mode, Action body) {
			GL.Begin(mode);
			body();
			GL.End();
		}
		
		public static void pushPopMatrix(Action body) {
			GL.PushMatrix();
			body();
			GL.PopMatrix();
		}
	}

	public static class GUIExt {
		public static void beginClip(Rect rect, Action body) {
			GUI.BeginClip(rect);
			body();
			GUI.EndClip();
		}
	}
	
	public static class GUILayoutExt {
		public static void beginHorizontal(Action body) {
			GUILayout.BeginHorizontal();
			body();
			GUILayout.EndHorizontal();
		}

		public static void beginHorizontal(GUIStyle style, Action body) {
			GUILayout.BeginHorizontal(style);
			body();
			GUILayout.EndHorizontal();
		}
		
		public static void beginVertical(Action body) {
			GUILayout.BeginVertical();
			body();
			GUILayout.EndVertical();
		}

		public static void beginVertical(GUIStyle style, Action body) {
			GUILayout.BeginVertical(style);
			body();
			GUILayout.EndVertical();
		}
	}
}
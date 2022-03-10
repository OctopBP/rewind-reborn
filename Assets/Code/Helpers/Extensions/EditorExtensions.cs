using System;
using UnityEngine;

namespace Rewind.Extensions {
	public static partial class GL {
		public static void begin(int mode, Action body) {
			UnityEngine.GL.Begin(mode);
			body();
			UnityEngine.GL.End();
		}
		
		public static void pushPopMatrix(Action body) {
			UnityEngine.GL.PushMatrix();
			body();
			UnityEngine.GL.PopMatrix();
		}
	}

	public static partial class GUI {
		public static void beginClip(Rect rect, Action body) {
			UnityEngine.GUI.BeginClip(rect);
			body();
			UnityEngine.GUI.EndClip();
		}
	}
	
	public static partial class GUILayout {
		public static void beginHorizontal(Action body) {
			UnityEngine.GUILayout.BeginHorizontal();
			body();
			UnityEngine.GUILayout.EndHorizontal();
		}

		public static void beginHorizontal(GUIStyle style, Action body) {
			UnityEngine.GUILayout.BeginHorizontal(style);
			body();
			UnityEngine.GUILayout.EndHorizontal();
		}
	}
}
using System;
using UnityEngine;

namespace Rewind.Extensions
{
	public static partial class GL
	{
		public static void Begin(int mode, Action body)
        {
			UnityEngine.GL.Begin(mode);
			body();
			UnityEngine.GL.End();
		}
		
		public static void PushPopMatrix(Action body)
        {
			UnityEngine.GL.PushMatrix();
			body();
			UnityEngine.GL.PopMatrix();
		}
	}

	public static partial class GUI
    {
		public static void BeginClip(Rect rect, Action body)
        {
			UnityEngine.GUI.BeginClip(rect);
			body();
			UnityEngine.GUI.EndClip();
		}
	}
	
	public static partial class GUILayout
    {
		public static void BeginHorizontal(Action body)
        {
			UnityEngine.GUILayout.BeginHorizontal();
			body();
			UnityEngine.GUILayout.EndHorizontal();
		}

		public static void BeginHorizontal(GUIStyle style, Action body)
        {
			UnityEngine.GUILayout.BeginHorizontal(style);
			body();
			UnityEngine.GUILayout.EndHorizontal();
		}
		
		public static void BeginVertical(Action body)
        {
			UnityEngine.GUILayout.BeginVertical();
			body();
			UnityEngine.GUILayout.EndVertical();
		}

		public static void BeginVertical(GUIStyle style, Action body)
        {
			UnityEngine.GUILayout.BeginVertical(style);
			body();
			UnityEngine.GUILayout.EndVertical();
		}
	}
}
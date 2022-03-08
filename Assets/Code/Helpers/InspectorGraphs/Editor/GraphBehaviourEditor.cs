using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Code.Helpers.InspectorGraphs.Editor {
	[CustomEditor(typeof(GraphBehaviour), true)]
	public class GraphBehaviourEditor : UnityEditor.Editor {
		const float GraphHeight = 120f;

		GraphBehaviour graphBehaviour;

		void OnEnable() {
			graphBehaviour = (GraphBehaviour) target;
		}

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();
			renderGraph();
		}

		void renderGraph() {
			var points = graphBehaviour.data.Select((p, i) => new Vector3(
				i * graphBehaviour.getXScale,
				GraphHeight - p * graphBehaviour.getYScale,
				0
			));

			var width = GUILayoutUtility.GetLastRect().width;
			var rect = GUILayoutUtility.GetRect(10, width - 10, GraphHeight, GraphHeight);
	
			EditorGUI.DrawRect(rect, new(0.04f, 0.05f, 0.08f));

			GUI.BeginClip(rect);
			Handles.color = Color.green;
			Handles.DrawAAPolyLine(Texture2D.whiteTexture, 4, points.ToArray());
			GUI.EndClip();
		}
	}
}
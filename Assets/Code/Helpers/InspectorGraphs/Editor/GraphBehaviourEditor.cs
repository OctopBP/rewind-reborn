using System.Linq;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Code.Helpers.InspectorGraphs.Editor {
	[CustomEditor(typeof(GraphBehaviour), true)]
	public class GraphBehaviourEditor : OdinEditor {
		const float GraphHeight = 150f;
		const int GraphStep = 10;
		const int PointsPerStep = 10;
		
		readonly Color graphMainColor = new(0.5f, 0.5f, 0.5f);
		readonly Color graphSmallColor = new(0.2f, 0.2f, 0.2f);

		GraphBehaviour graphBehaviour;
		Material material;

		public override bool RequiresConstantRepaint() => true;

		protected override void OnEnable() {
			graphBehaviour = (GraphBehaviour) target;
			material = new(Shader.Find("Hidden/Internal-Colored"));
		}

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			foreach (var info in graphBehaviour.infos) {
				drawGrid(info);
				EditorGUILayout.Space();
			}
		}

		void drawGrid(GraphInfo graphInfo) {
			if (!graphInfo.target) return;

			var yStep = GraphStep * graphInfo.yScale;
			var xStep = GraphStep * graphInfo.xScale;
			var pointsXStep = xStep / PointsPerStep;

			EditorGUILayout.LabelField($"{graphInfo.target.name} [{graphInfo.type}]");

			var width = GUILayoutUtility.GetLastRect().width;
			var rect = GUILayoutUtility.GetRect(10, width - 10, GraphHeight, GraphHeight);

			GUILayout.BeginHorizontal();

			if (Event.current.type == EventType.Repaint) {
				GUI.BeginClip(rect);
				GL.PushMatrix();

				GL.Clear(true, false, Color.black);
				material.SetPass(0);

				GL.Begin(GL.QUADS);
				GL.Color(Color.black);
				GL.Vertex3(0, 0, 0);
				GL.Vertex3(rect.width, 0, 0);
				GL.Vertex3(rect.width, rect.height, 0);
				GL.Vertex3(0, rect.height, 0);
				GL.End();

				GL.Begin(GL.LINES);
				
				var yCount = GraphHeight / yStep;
				var xCount = rect.width / xStep;
				var pointsCount = Mathf.RoundToInt(rect.width / pointsXStep);
				var offset = Mathf.Max(0, graphInfo.data.Count - pointsCount + 50) * pointsXStep;

				for (var i = 0; i < yCount; i++) {
					var lineColour = i % 5 == 0 ? graphMainColor : graphSmallColor;
					GL.Color(lineColour);

					var y = i * yStep;
					GL.Vertex3(0, y, 0);
					GL.Vertex3(rect.width, y, 0);
				}

				for (var i = 0; i < xCount; i++) {
					var lineColour = i % 5 == 0 ? graphMainColor : graphSmallColor;
					GL.Color(lineColour);

					var x = (i * GraphStep - offset % xStep) * graphInfo.xScale;
					GL.Vertex3(x, 0, 0);
					GL.Vertex3(x, rect.height, 0);
				}

				GL.Color(Color.green);
				for (var i = Mathf.Max(0, graphInfo.data.Count - pointsCount);
				     i < graphInfo.data.Count - 1;
				     i++
				) {
					drawPoint(i);
					drawPoint(i + 1);

					void drawPoint(int index) => GL.Vertex3(
						index * pointsXStep - offset,
						GraphHeight - graphInfo.data[index] * graphInfo.yScale,
						0
					);
				}

				GL.End();

				GL.PopMatrix();
				GUI.EndClip();
			}

			GUILayout.EndHorizontal();
		}
	}
}
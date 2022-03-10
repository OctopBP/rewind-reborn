using System.Linq;
using LanguageExt;
using Rewind.Extensions;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using GL = UnityEngine.GL;
using GLExt = Rewind.Extensions.GL;
using GUIExt = Rewind.Extensions.GUI;
using GUILayoutExt = Rewind.Extensions.GUILayout;

namespace Code.Helpers.InspectorGraphs.Editor {
	[CustomEditor(typeof(GraphBehaviour), true)]
	public class GraphBehaviourEditor : OdinEditor {
		const float GraphHeight = 150f;
		const int MainLineCount = 5;

		readonly Color graphMainColor = new(0.4f, 0.4f, 0.4f);
		readonly Color graphSmallColor = new(0.15f, 0.15f, 0.15f);

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
				draw(info);
				EditorGUILayout.Space();
			}
		}

		void draw(GraphInfo graphInfo) {
			if (graphInfo.items.Count == 0) return;

			var width = GUILayoutUtility.GetLastRect().width;
			var rect = GUILayoutUtility.GetRect(10, width - 10, GraphHeight, GraphHeight);

			var notEmptyItems = graphInfo.items.Where(item => item.target != null).ToList();

			GUILayoutExt.beginHorizontal(() => {
				var labelStyle = new GUIStyle(EditorStyles.textField);
				foreach (var item in notEmptyItems) {
					labelStyle.normal.textColor = item.color;
					EditorGUILayout.LabelField($"{item.target.name} [{item.type}]", labelStyle);
				}

				if (Event.current.type == EventType.Repaint) {
					GUIExt.beginClip(rect, () => {
						GLExt.pushPopMatrix(() => {
							GL.Clear(true, false, Color.black);
							material.SetPass(0);

							drawBgQuad(rect);

							var maybeIndexOffset = calcIndexOffset(rect, graphInfo);
							if (maybeIndexOffset.valueOut(out var indexOffset)) {
								foreach (var item in notEmptyItems) {
									drawGrid(rect, graphInfo, indexOffset);
									drawLine(rect, graphInfo, item, indexOffset);
								}
							}
						});
					});
				}
			});
		}

		static void drawBgQuad(Rect rect) {
			GLExt.begin(GL.QUADS, () => {
				GL.Color(Color.black);
				GL.Vertex3(0, 0, 0);
				GL.Vertex3(rect.width, 0, 0);
				GL.Vertex3(rect.width, rect.height, 0);
				GL.Vertex3(0, rect.height, 0);
			});
		}

		static Option<int> calcIndexOffset(Rect rect, GraphInfo graphInfo) =>
			rect.width
				.divide(graphInfo.scale.x)
				.Where(_ => graphInfo.items.Count > 0)
				.Map(count => Mathf.Max(0, graphInfo.items[0].data.Count - count.roundToInt()));

		void drawGrid(Rect rect, GraphInfo graphInfo, int indexOffset) {
			var gridCellSize = graphInfo.gridSize * graphInfo.scale;
			var maybeCellCount = rect.size.divide(gridCellSize);

			if (maybeCellCount.valueOut(out var cellCount)) {
				GLExt.begin(GL.LINES, () => {
					var baseXOffset = indexOffset * graphInfo.scale.x;
					var xOffset = baseXOffset.positiveMod(gridCellSize.x * MainLineCount);
					var yOffset = graphInfo.yOffset.positiveMod(gridCellSize.y * MainLineCount);

					for (var i = -MainLineCount; i < cellCount.x + MainLineCount; i++) {
						var x = i * gridCellSize.x - xOffset;

						if (x >= 0 && x <= rect.width) {
							var lineColour = i % MainLineCount == 0 ? graphMainColor : graphSmallColor;
							GL.Color(lineColour);

							GL.Vertex3(x, 0, 0);
							GL.Vertex3(x, rect.height, 0);
						}
					}

					for (var i = -MainLineCount; i < cellCount.y; i++) {
						var y = i * gridCellSize.y + yOffset;

						if (y >= 0 && y <= rect.height) {
							var lineColour = i % MainLineCount == 0 ? graphMainColor : graphSmallColor;
							GL.Color(lineColour);
							GL.Vertex3(0, GraphHeight - y, 0);
							GL.Vertex3(rect.width, GraphHeight - y, 0);
						}
					}
					
					// Axis line
					GL.Color(Color.white);
					GL.Vertex3(0, GraphHeight - graphInfo.yOffset, 0);
					GL.Vertex3(rect.width, GraphHeight - graphInfo.yOffset, 0);
				});
			}
		}

		static void drawLine(Rect rect, GraphInfo graphInfo, GraphItemInfo item, int indexOffset) {
			const float thickness = 0.1f;
			const uint accuracy = 8;

			for (var i = 0; i < accuracy; i++) {
				var angle = i * (360 / accuracy);
				var sin = Mathf.Sin(angle * Mathf.Deg2Rad);
				var cos = Mathf.Cos(angle * Mathf.Deg2Rad);
				drawLine(rect, graphInfo, item, indexOffset, new Vector2(sin, cos) * thickness);
			}
		}

		static void drawLine(
			Rect rect, GraphInfo graphInfo, GraphItemInfo item, int indexOffset, Vector2 thicknessOffset
		) {
			var xStep = graphInfo.scale.x;

			GLExt.begin(GL.LINES, () => {
				GL.Color(item.color);

				var offset = indexOffset * xStep;
				for (var i = indexOffset; i < item.data.Count - 1; i++) {
					var point1 = pointPosition(i);
					var point2 = pointPosition(i + 1);

					if (pointInsideTheRect(point1) && pointInsideTheRect(point2)) {
						drawPoint(point1);
						drawPoint(point2);
					}
				}

				Vector2 pointPosition(int index) => new(
					index * xStep - offset + thicknessOffset.x,
					GraphHeight - (item.data[index] * graphInfo.scale.y + graphInfo.yOffset) + thicknessOffset.y
				);

				void drawPoint(Vector2 point) => GL.Vertex3(point.x, point.y, 0);

				bool pointInsideTheRect(Vector2 point) =>
					point.x >= 0 && point.x <= rect.width && point.y >= 0 && point.y <= rect.height;
			});
		}
	}
}
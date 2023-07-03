using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LanguageExt;
using Rewind.Extensions;
using Sirenix.OdinInspector.Editor;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using GL = UnityEngine.GL;
using GUILayout = UnityEngine.GUILayout;
using static LanguageExt.Prelude;

namespace Code.Helpers.InspectorGraphs.Editor {
	[CustomEditor(typeof(GraphBehaviour), true)]
	public partial class GraphBehaviourEditor : OdinEditor {
		const float GraphHeight = 150f;
		const int MainLineCount = 5;

		static readonly Color graphMainColor = new(0.4f, 0.4f, 0.4f);
		static readonly Color graphSmallColor = new(0.15f, 0.15f, 0.15f);
		static readonly Color axisColor = new(0.65f, 0.65f, 0.65f);

		static Material material;
		GraphBehaviour graphBehaviour;

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

		static void draw(GraphInfo graphInfo) {
			if (graphInfo.items == null || graphInfo.items.Count == 0) return;

			var width = GUILayoutUtility.GetLastRect().width;
			var rect = GUILayoutUtility.GetRect(10, width - 10, GraphHeight, GraphHeight);

			var notEmptyItems = graphInfo.items.Where(item => item.target != null).ToList();

			GUILayoutExt.beginVertical(() => {
				labelAndSaveButton(notEmptyItems);
				if (Event.current.type == EventType.Repaint)
					grid(graphInfo, rect, notEmptyItems);
			});
		}

		static void grid(GraphInfo graphInfo, Rect rect, List<GraphItemInfo> notEmptyItems) {
			GUIExt.beginClip(rect, () => {
				GLExt.pushPopMatrix(() => {
					GL.Clear(true, false, Color.black);
					material.SetPass(0);

					drawBgQuad(rect);

					var maybeIndexOffset = calcIndexOffset(rect, graphInfo);
					maybeIndexOffset.IfSome(indexOffset => {
						if (graphInfo.showGrid) {
							drawGrid(rect, graphInfo, indexOffset);
						} 

						if (graphInfo.showTimelines) {
							notEmptyItems.first().IfSome(
								item => drawTimeLine(rect, graphInfo, item, indexOffset)
							);
						}

						foreach (var item in notEmptyItems) {
							drawLine(rect, graphInfo, item, indexOffset);
						}
					});
				});
			});
		}

		static void labelAndSaveButton(List<GraphItemInfo> notEmptyItems) {
			var labelStyle = new GUIStyle(EditorStyles.textField);
			foreach (var item in notEmptyItems) {
				GUILayoutExt.beginHorizontal(() => {
					labelStyle.normal.textColor = item.color;
					EditorGUILayout.LabelField($"{item.target.name} [{item.type}]", labelStyle);

					var path = $"/Users/boris_proshin/Desktop/{item.target.name}_{item.type}_{DateTime.Now:dd.MM_HH.mm.ss}.json";
					if (GUILayout.Button("Save data")) {
						if (!File.Exists(path)) {
							var json = JsonConvert.SerializeObject(item.data);
							using var sw = File.CreateText(path);
							sw.WriteLine(json);
						}
					}
				});
			}
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

		static void drawGrid(Rect rect, GraphInfo graphInfo, int indexOffset) {
			var gridCellSize = graphInfo.gridSize * graphInfo.scale;
			var maybeCellCount = rect.size.divide(gridCellSize);

			maybeCellCount.IfSome(cellCount => {
				GLExt.begin(GL.LINES, () => {
					var baseXOffset = indexOffset * graphInfo.scale.x;
					var xOffset = baseXOffset.positiveMod(gridCellSize.x * MainLineCount);
					var yOffset = graphInfo.yOffset.positiveMod(gridCellSize.y * MainLineCount);

					for (var i = -MainLineCount; i < cellCount.x + MainLineCount; i++) {
						var x = i * gridCellSize.x - xOffset;

						if (x >= 0 && x <= rect.width) {
							var lineColour = i % MainLineCount == 0 ? graphMainColor : graphSmallColor;
							GLLine.draw(rect, x, 0, x, rect.height, lineColour);
						}
					}

					for (var i = -MainLineCount; i < cellCount.y; i++) {
						var y = i * gridCellSize.y + yOffset;

						if (y >= 0 && y <= rect.height) {
							var lineColour = i % MainLineCount == 0 ? graphMainColor : graphSmallColor;
							GLLine.draw(rect, 0, GraphHeight - y, rect.width, GraphHeight - y, lineColour);
						}
					}

					// Axis line
					GLLine.draw(
						rect, 0, GraphHeight - graphInfo.yOffset, rect.width, GraphHeight - graphInfo.yOffset, Color.white
					);
				});
			});
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
				var offset = indexOffset * xStep;
				for (var i = indexOffset; i < item.data.Count - 1; i++) {
					var point1 = pointPosition(i);
					var point2 = pointPosition(i + 1);
					GLLine.draw(rect, point1, point2, item.color);
				}

				Vector2 pointPosition(int index) => new(
					index * xStep - offset + thicknessOffset.x,
					GraphHeight - (item.data[index].value * graphInfo.scale.y + graphInfo.yOffset) + thicknessOffset.y
				);
			});
		}
		
		static void drawTimeLine(Rect rect, GraphInfo graphInfo, GraphItemInfo item, int indexOffset) {
			var counter = 0;
			var list = new List<(Option<GraphBehaviour.Init.TimeLine.Type> type, float index)>();
			for (var i = 0; i < item.data.Count; i++) {
				var pTime = item.data[i].time;
				if (pTime >= counter) {
					list.Add((None, i));
					counter++;
				}

				if (GraphBehaviour.init != null) {
					var timeline = GraphBehaviour.init.timeLines
						.Where(tl => !list.Exists(it => it.type == tl.type))
						.FirstOrDefault(tl => pTime >= tl.time);

					if (timeline != null)
						list.Add((type: timeline.type, index: i));
				}
			}

			GLExt.begin(GL.LINES, () => {
				foreach (var item in list) {
					var color = item.type.Match(t => t switch {
						GraphBehaviour.Init.TimeLine.Type.StartRecord => ColorA.red,
						GraphBehaviour.Init.TimeLine.Type.Record => ColorA.red,
						GraphBehaviour.Init.TimeLine.Type.Replay => ColorA.green,
						GraphBehaviour.Init.TimeLine.Type.Rewind => ColorA.cyan,
						_ => throw new ArgumentOutOfRangeException(nameof(t), t, null)
					}, () => axisColor);

					var x = (item.index - indexOffset) * graphInfo.scale.x;
					GLLine.draw(rect, x, 0, x, rect.height, color);
				}
			});
		}
	}
}
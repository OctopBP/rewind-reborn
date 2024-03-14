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
using GLExt = Rewind.Extensions.GL;
using GUIExt = Rewind.Extensions.GUI;
using GUILayoutExt = Rewind.Extensions.GUILayout;
using static LanguageExt.Prelude;

namespace Code.Helpers.InspectorGraphs.Editor
{
	[CustomEditor(typeof(GraphBehaviour), true)]
	public partial class GraphBehaviourEditor : OdinEditor
    {
		private const float GraphHeight = 150f;
		private const int MainLineCount = 5;

		private static readonly Color GraphMainColor = new(0.4f, 0.4f, 0.4f);
		private static readonly Color GraphSmallColor = new(0.15f, 0.15f, 0.15f);
		private static readonly Color AxisColor = new(0.65f, 0.65f, 0.65f);

		private static Material material;
		private GraphBehaviour graphBehaviour;

		public override bool RequiresConstantRepaint() => true;

		protected override void OnEnable()
        {
			graphBehaviour = (GraphBehaviour) target;
			material = new(Shader.Find("Hidden/Internal-Colored"));
		}

		public override void OnInspectorGUI()
        {
			base.OnInspectorGUI();

			foreach (var info in graphBehaviour.infos)
            {
				Draw(info);
				EditorGUILayout.Space();
			}
		}

		private static void Draw(GraphInfo graphInfo)
        {
			if (graphInfo.items == null || graphInfo.items.Count == 0) return;

			var width = GUILayoutUtility.GetLastRect().width;
			var rect = GUILayoutUtility.GetRect(10, width - 10, GraphHeight, GraphHeight);

			var notEmptyItems = graphInfo.items.Where(item => item.target != null).ToList();

			GUILayoutExt.BeginVertical(() =>
            {
				LabelAndSaveButton(notEmptyItems);
				if (Event.current.type == EventType.Repaint)
				{
					Grid(graphInfo, rect, notEmptyItems);
				}
			});
		}

		private static void Grid(GraphInfo graphInfo, Rect rect, List<GraphItemInfo> notEmptyItems)
        {
			GUIExt.BeginClip(rect, () =>
            {
				GLExt.PushPopMatrix(() =>
                {
					GL.Clear(true, false, Color.black);
					material.SetPass(0);

					DrawBgQuad(rect);

					var maybeIndexOffset = CalcIndexOffset(rect, graphInfo);
					maybeIndexOffset.IfSome(indexOffset =>
                    {
						if (graphInfo.showGrid)
                        {
							DrawGrid(rect, graphInfo, indexOffset);
						} 

						if (graphInfo.showTimelines)
                        {
							FunctionalExtensions.First(notEmptyItems).IfSome(
								item => DrawTimeLine(rect, graphInfo, item, indexOffset)
							);
						}

						foreach (var item in notEmptyItems)
                        {
							DrawLine(rect, graphInfo, item, indexOffset);
						}
					});
				});
			});
		}

		private static void LabelAndSaveButton(List<GraphItemInfo> notEmptyItems)
        {
			var labelStyle = new GUIStyle(EditorStyles.textField);
			foreach (var item in notEmptyItems)
            {
				GUILayoutExt.BeginHorizontal(() =>
                {
					labelStyle.normal.textColor = item.color;
					EditorGUILayout.LabelField($"{item.target.name} [{item.type}]", labelStyle);

					var path = $"/Users/boris_proshin/Desktop/{item.target.name}_{item.type}_{DateTime.Now:dd.MM_HH.mm.ss}.json";
					if (GUILayout.Button("Save data"))
                    {
						if (!File.Exists(path))
                        {
							var json = JsonConvert.SerializeObject(item.data);
							using var sw = File.CreateText(path);
							sw.WriteLine(json);
						}
					}
				});
			}
		}

		private static void DrawBgQuad(Rect rect)
        {
			GLExt.Begin(GL.QUADS, () =>
            {
				GL.Color(Color.black);
				GL.Vertex3(0, 0, 0);
				GL.Vertex3(rect.width, 0, 0);
				GL.Vertex3(rect.width, rect.height, 0);
				GL.Vertex3(0, rect.height, 0);
			});
		}

		private static Option<int> CalcIndexOffset(Rect rect, GraphInfo graphInfo) =>
			rect.width
				.Divide(graphInfo.scale.x)
				.Where(_ => graphInfo.items.Count > 0)
				.Map(count => Mathf.Max(0, graphInfo.items[0].data.Count - count.RoundToInt()));

		private static void DrawGrid(Rect rect, GraphInfo graphInfo, int indexOffset)
        {
			var gridCellSize = graphInfo.gridSize * graphInfo.scale;
			var maybeCellCount = rect.size.Divide(gridCellSize);

			maybeCellCount.IfSome(cellCount =>
            {
				GLExt.Begin(GL.LINES, () =>
                {
					var baseXOffset = indexOffset * graphInfo.scale.x;
					var xOffset = baseXOffset.PositiveMod(gridCellSize.x * MainLineCount);
					var yOffset = graphInfo.yOffset.PositiveMod(gridCellSize.y * MainLineCount);

					for (var i = -MainLineCount; i < cellCount.x + MainLineCount; i++)
                    {
						var x = i * gridCellSize.x - xOffset;

						if (x >= 0 && x <= rect.width)
                        {
							var lineColour = i % MainLineCount == 0 ? GraphMainColor : GraphSmallColor;
							GLLine.Draw(rect, x, 0, x, rect.height, lineColour);
						}
					}

					for (var i = -MainLineCount; i < cellCount.y; i++)
                    {
						var y = i * gridCellSize.y + yOffset;

						if (y >= 0 && y <= rect.height)
                        {
							var lineColour = i % MainLineCount == 0 ? GraphMainColor : GraphSmallColor;
							GLLine.Draw(rect, 0, GraphHeight - y, rect.width, GraphHeight - y, lineColour);
						}
					}

					// Axis line
					GLLine.Draw(
						rect, 0, GraphHeight - graphInfo.yOffset, rect.width, GraphHeight - graphInfo.yOffset, Color.white
					);
				});
			});
		}

		private static void DrawLine(Rect rect, GraphInfo graphInfo, GraphItemInfo item, int indexOffset)
        {
			const float thickness = 0.1f;
			const uint accuracy = 8;

			for (var i = 0; i < accuracy; i++)
            {
				var angle = i * (360 / accuracy);
				var sin = Mathf.Sin(angle * Mathf.Deg2Rad);
				var cos = Mathf.Cos(angle * Mathf.Deg2Rad);
				DrawLine(rect, graphInfo, item, indexOffset, new Vector2(sin, cos) * thickness);
			}
		}

		private static void DrawLine(
			Rect rect, GraphInfo graphInfo, GraphItemInfo item, int indexOffset, Vector2 thicknessOffset
		)
        {
			var xStep = graphInfo.scale.x;

			GLExt.Begin(GL.LINES, () =>
            {
				var offset = indexOffset * xStep;
				for (var i = indexOffset; i < item.data.Count - 1; i++)
                {
					var point1 = PointPosition(i);
					var point2 = PointPosition(i + 1);
					GLLine.Draw(rect, point1, point2, item.color);
				}

				Vector2 PointPosition(int index) => new(
					index * xStep - offset + thicknessOffset.x,
					GraphHeight - (item.data[index].Value * graphInfo.scale.y + graphInfo.yOffset) + thicknessOffset.y
				);
			});
		}

		private static void DrawTimeLine(Rect rect, GraphInfo graphInfo, GraphItemInfo item, int indexOffset)
        {
			var counter = 0;
			var timelinesCounter = 0;
			var list = new List<(Option<GraphBehaviour.Init.TimeLine.Type> type, float index)>();
			for (var i = 0; i < item.data.Count; i++)
            {
				var pTime = item.data[i].Time;
				if (pTime >= counter)
                {
					list.Add((None, i));
					counter++;
				}

				if (GraphBehaviour.init != null)
                {
					var timeline = GraphBehaviour.init.TimeLines
						.Where(tl => !list.Exists(it => it.type == tl.TimeLineType))
						.FirstOrDefault(tl => pTime >= tl.Time);

					if (timeline != null)
						list.Add((type: timeline.TimeLineType, index: i));
				}
			}

			GLExt.Begin(GL.LINES, () =>
            {
				foreach (var item in list)
                {
					var color = item.type.Match(t => t switch
                    {
						GraphBehaviour.Init.TimeLine.Type.StartRecord => ColorA.Red,
						GraphBehaviour.Init.TimeLine.Type.Record => ColorA.Red,
						GraphBehaviour.Init.TimeLine.Type.Replay => ColorA.Green,
						GraphBehaviour.Init.TimeLine.Type.Rewind => ColorA.Cyan,
						_ => throw new ArgumentOutOfRangeException(nameof(t), t, null)
					}, () => AxisColor);

					var x = (item.index - indexOffset) * graphInfo.scale.x;
					GLLine.Draw(rect, x, 0, x, rect.height, color);
				}
			});
		}
	}
}
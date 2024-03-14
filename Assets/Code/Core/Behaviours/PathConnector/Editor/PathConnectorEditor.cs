using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using Rewind.Behaviours;
using Rewind.Extensions;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using GUI = UnityEngine.GUI;

namespace Rewind.ECSCore.Editor
{
	[CustomEditor(typeof(PathConnector)), CanEditMultipleObjects]
	public class PathConnectorEditor : OdinEditor
	{
		private const float LineDashSize = 3f;
		private static List<WalkPath> paths = new();

		private static GUIStyle distanceLabel => new(GUI.skin.label)
		{
			alignment = TextAnchor.LowerCenter,
			fontSize = 7,
			fontStyle = FontStyle.Bold
		};
		
		protected override void OnEnable()
		{
			base.OnEnable();
			paths = FindObjectsOfType<WalkPath>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(PathConnector pathConnector, GizmoType gizmo) =>
			drawLine(pathConnector);

		private static void drawLine(PathConnector pathConnector)
		{
			var point1 = pathConnector.GetPoint1__EDITOR;
			var point2 = pathConnector.GetPoint2__EDITOR;
			
			if (point1.pathId.IsNullOrEmpty() || point2.pathId.IsNullOrEmpty()) return;

			var maybePath1 = paths.FindById(point1.pathId);
			var maybePath2 = paths.FindById(point2.pathId);
			
			var maybeFrom = getMaybeValue(point1.index, maybePath1);
			var maybeTo = getMaybeValue(point2.index, maybePath2);
			
			maybeTo
				.FlatMap(to => maybeFrom.Map(from => (to, from)))
				.IfSome(tpl =>
			{
				var (to, from) = tpl;
				
				var direction = from - to;
				var distance = direction.magnitude.Abs();
				
				Handles.DrawDottedLine(from, to, LineDashSize);
				Handles.Label((from + to) * .5f, $"{distance:F1}", distanceLabel);
			});

			Option<Vector3> getMaybeValue(int index, Option<WalkPath> pathBehaviour) => pathBehaviour.FlatMap(
				path => path.at_EDITOR(index).Map(point => path.transform.position + (Vector3) point.localPosition)
			);
		}
	}
}
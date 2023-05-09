using System.Collections.Generic;
using System.Linq;
using Code.Helpers;
using LanguageExt;
using Rewind.Behaviours;
using Rewind.Extensions;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using GUI = UnityEngine.GUI;
using static LanguageExt.Prelude;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(Connector)), CanEditMultipleObjects]
	public class ConnectorEditor : OdinEditor {
		const float LineWidth = 3f;
		const float DistanceLineWidth = 20f;
		static List<WalkPath> paths = new();
		
		static GUIStyle distanceLabel => new(GUI.skin.label) {
			alignment = TextAnchor.LowerCenter,
			fontSize = 7,
			fontStyle = FontStyle.Bold
		};
		
		protected override void OnEnable() {
			base.OnEnable();
			paths = FindObjectsOfType<WalkPath>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(Connector connector, GizmoType gizmo) =>
			drawLine(connector);

		static void drawLine(Connector connector) {
			var point1 = connector.getPoint1__EDITOR;
			var point2 = connector.getPoint2__EDITOR;
			
			if (point1.pathId.isNullOrEmpty() || point2.pathId.isNullOrEmpty()) return;

			var maybePath1 = paths.findById(point1.pathId);
			var maybePath2 = paths.findById(point2.pathId);
			
			var maybeFrom = getMaybeValue(point1.index, maybePath1);
			var maybeTo = getMaybeValue(point2.index, maybePath2);
			
			maybeTo
				.Map(to => maybeFrom.Map(from => (to, from)))
				.Flatten()
				.IfSome(tpl =>
			{
				var (to, from) = tpl;

				var activateDistance = connector.getActivateDistance__EDITOR;
				var direction = from - to;
				var distance = direction.magnitude.abs();
				var isOpen = distance <= activateDistance;
				var color = isOpen ? ColorA.green : ColorA.red;

				drawActivateDistance(from, to, direction, activateDistance);

				Handles.DrawBezier(from, to, from, to, color, null, LineWidth);
				Handles.Label((from + to) * .5f, $"{distance:F1}", distanceLabel);
			});

			Option<Vector3> getMaybeValue(int index, Option<WalkPath> pathBehaviour) => pathBehaviour.Map(
				path => path.at_EDITOR(index).Map(point => path.transform.position + (Vector3) point.localPosition)
			).Flatten();

			void drawActivateDistance(Vector3 @from, Vector3 to, Vector3 direction, float activateDistance) {
				var center = (from + to) / 2;
				var halfOfActivateDistance = direction.normalized * activateDistance / 2;
				var f = center - halfOfActivateDistance;
				var t = center + halfOfActivateDistance;
				Handles.DrawBezier(f, t, f, t, Color.green.withAlpha(.3f), null, DistanceLineWidth);
			}
		}
	}
}
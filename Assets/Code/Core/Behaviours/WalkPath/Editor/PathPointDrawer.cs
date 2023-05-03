using System;
using System.Collections.Generic;
using System.Linq;
using Rewind.ECSCore;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class PathPointDrawer : OdinValueDrawer<PathPoint> {
	List<WalkPath> paths = new();

	protected override void Initialize() {
		base.Initialize();
		paths = Object.FindObjectsOfType<WalkPath>().OrderBy(p => p.name).ToList();
	}

	protected override void DrawPropertyLayout(GUIContent label) {
		var rect = EditorGUILayout.GetControlRect();

		if (label != null) {
			rect = EditorGUI.PrefixLabel(rect, label);
		}

		if (paths.Count == 0) {
			EditorGUILayout.HelpBox("No paths found", MessageType.Error);
			return;
		}

		var pathNames = paths.Select(toName()).ToArray();
		var pathIndex = paths.FindIndex(p => p.id_EDITOR.guid == ValueEntry.SmartValue.pathId.guid);
		var newIndex = EditorGUI.Popup(rect.AlignLeft(rect.width * 0.6f), pathIndex, pathNames);
		
		var value = ValueEntry.SmartValue;
		GUIHelper.PushLabelWidth(20);
		if (newIndex < 0) {
			EditorGUILayout.HelpBox("Wrong path", MessageType.Error);
		} else {
			var newPath = paths[newIndex];
			value.pathId = newPath.id_EDITOR;

			if (newPath.length_EDITOR > 0) {
				var points = Enumerable.Range(0, newPath.length_EDITOR);
				var pointsArray = points as int[] ?? points.ToArray();
				var pointsNames = pointsArray.Select(p => $"Point {p}").ToArray();

				value.index = SirenixEditorFields.Dropdown(rect.AlignRight(rect.width * 0.4f), value.index, pointsNames);
			} else {
				EditorGUI.LabelField(rect.AlignRight(rect.width * 0.5f), "Path is empty");
			}
		}
		GUIHelper.PopLabelWidth();
		ValueEntry.SmartValue = value;

		Func<WalkPath, int, string> toName() =>
			(p, i) => $"{i + 1}. {p.gameObject.name} (...{p.id_EDITOR.ToString()[^4..]})";
	}
}
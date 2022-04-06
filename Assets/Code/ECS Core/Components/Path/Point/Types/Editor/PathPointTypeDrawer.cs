using System.Collections.Generic;
using System.Linq;
using Rewind.ECSCore;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public class PathPointTypeDrawer : OdinValueDrawer<PathPointType> {
	List<PathBehaviour> paths = new();

	protected override void Initialize() {
		base.Initialize();
		paths = Object.FindObjectsOfType<PathBehaviour>().ToList();
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
		
		var pathNames = paths.Select(p => $"{p.gameObject.name}").ToArray();
		var pathIndex = paths.FindIndex(p => p.id.guid == ValueEntry.SmartValue.pathId.guid);
		var newIndex = EditorGUI.Popup(rect.AlignLeft(rect.width * 0.6f), pathIndex, pathNames);
		
		var value = ValueEntry.SmartValue;
		GUIHelper.PushLabelWidth(20);
		if (newIndex < 0) {
			EditorGUILayout.HelpBox("Wrong path", MessageType.Error);
		} else {
			var newPath = paths[newIndex];
			value.pathId = newPath.id;

			if (newPath.length > 0) {
				var points = Enumerable.Range(0, newPath.length);
				var pointsArray = points as int[] ?? points.ToArray();
				var pointsNames = pointsArray.Select(p => $"Point {p}").ToArray();

				value.index = SirenixEditorFields.Dropdown(rect.AlignRight(rect.width * 0.4f), value.index, pointsNames);
			} else {
				EditorGUI.LabelField(rect.AlignRight(rect.width * 0.5f), "Path is empty");
			}
		}
		GUIHelper.PopLabelWidth();
		ValueEntry.SmartValue = value;
	}
}
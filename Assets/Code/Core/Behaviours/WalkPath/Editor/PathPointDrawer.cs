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
	List<(WalkPath path, string sceneName)> paths = new();

	protected override void Initialize() {
		base.Initialize();
		paths = Object.FindObjectsOfType<WalkPath>()
			.OrderBy(p => p.name)
			.Select(_ => (_, _.gameObject.scene.name))
			.ToList();
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
		var pathIndex = paths.FindIndex(p => p.path._pathId.guid == ValueEntry.SmartValue.pathId.guid);
		var newIndex = EditorGUI.Popup(rect.AlignLeft(rect.width * 0.75f), pathIndex, pathNames);
		
		var value = ValueEntry.SmartValue;
		GUIHelper.PushLabelWidth(20);
		if (newIndex < 0) {
			EditorGUILayout.HelpBox("Wrong path", MessageType.Error);
		} else {
			var newPath = paths[newIndex];
			value.pathId = newPath.path._pathId;

			if (newPath.path.length_EDITOR > 0) {
				var points = Enumerable.Range(0, newPath.path.length_EDITOR);
				var pointsArray = points as int[] ?? points.ToArray();
				var pointsNames = pointsArray.Select(p => $"{p}").ToArray();
				value.index = SirenixEditorFields.Dropdown(rect.AlignRight(rect.width * 0.25f), value.index, pointsNames);
			} else {
				EditorGUI.LabelField(rect.AlignRight(rect.width * 0.5f), "Path is empty");
			}
		}
		GUIHelper.PopLabelWidth();
		ValueEntry.SmartValue = value;

		Func<(WalkPath path, string sceneName), int, string> toName() =>
			(tpl, i) => $"{tpl.sceneName}/{i + 1}. {tpl.path.gameObject.name} (...{tpl.path._pathId.ToString()[^4..]})";
	}
}
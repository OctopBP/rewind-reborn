using System;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public class SerializableGuidDrawer : OdinValueDrawer<SerializableGuid> {
	protected override void DrawPropertyLayout(GUIContent label) {
		var rect = EditorGUILayout.GetControlRect();

		if (label != null) {
			rect = EditorGUI.PrefixLabel(rect, label);
		}

		var value = ValueEntry.SmartValue;

		GUIHelper.PushLabelWidth(20);
		if (value.isEmpty) { 
			EditorGUILayout.HelpBox("No id", MessageType.Error);
			if (GUI.Button(rect, "new")) {
				value = Guid.NewGuid();
			}
		} else {
			EditorGUI.LabelField(rect.AlignLeft(rect.width * 0.8f), value.ToString());
			if (GUI.Button(rect.AlignRight(rect.width * 0.2f), "new")) {
				value = Guid.NewGuid();
			}
		}

		GUIHelper.PopLabelWidth();
		ValueEntry.SmartValue = value;
	}
}
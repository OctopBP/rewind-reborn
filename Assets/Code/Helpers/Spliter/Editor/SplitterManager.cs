using Rewind.Extensions;
using UnityEditor;
using UnityEngine;

namespace Rewind.Helpers {
	[InitializeOnLoad]
	public class SplitterManager {
		static SplitterManager() {
			EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
		}

		static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect) {
			var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
			if (gameObject == null) return;

			var splitter = gameObject.GetComponent<Splitter>();
			if (splitter == null) return;

			splitter.tag = Tags.EditorOnly;

			var textColor = EditorGUIUtility.isProSkin ? splitter.textColorD : splitter.textColor;
			var styleState = new GUIStyleState() { textColor = textColor };
			var style = new GUIStyle() {
				normal = styleState,
				fontStyle = FontStyle.Bold,
				alignment = splitter.textAlignment
			};

			if (splitter.extend) {
				var parentsCount = splitter.transform.getParentsCount();

				// Числа подобранны вручную
				// Инвормации как получить ширину иерархии не нашёл
				var offset = parentsCount * 14;
				selectionRect.x -= offset + 27.5f;
				selectionRect.width += offset + 43;
			}


			var bgColor = EditorGUIUtility.isProSkin ? splitter.backgroundColorD : splitter.backgroundColor;
			EditorGUI.DrawRect(selectionRect, bgColor);
			EditorGUI.LabelField(selectionRect, splitter.name, style);
		}
	}
}
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
			if (gameObject != null && gameObject.TryGetComponent(out Splitter splitter)) {
				splitter.tag = splitter.editorOnly ? Tags.EditorOnly : Tags.Untagged;

				var theme = splitter.getTheme(EditorGUIUtility.isProSkin);
				var styleState = new GUIStyleState {
					textColor = theme.textColor.withAlpha(gameObject.activeSelf ? 1 : 0.5f)
				};
				var style = new GUIStyle {
					normal = styleState,
					fontStyle = splitter.fontStyle,
					alignment = splitter.textAlignment,
					padding = splitter.padding
				};

				selectionRect.width += 20;
				if (splitter.extend) {
					var parentsCount = splitter.transform.getParentsCount();

					// Числа подобранны вручную
					// Информации как получить ширину иерархии не нашёл
					var offset = parentsCount * 14;
					selectionRect.x -= offset + 27.5f;
					selectionRect.width += offset + 23;
				}

				EditorGUI.DrawRect(selectionRect, theme.backgroundColor);
				EditorGUI.LabelField(selectionRect, splitter.name, style);
			}
		}
	}
}
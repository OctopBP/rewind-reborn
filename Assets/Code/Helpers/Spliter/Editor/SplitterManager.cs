using Rewind.Extensions;
using UnityEditor;
using UnityEngine;

namespace Rewind.Helpers
{
	[InitializeOnLoad]
	public class SplitterManager
    {
		static SplitterManager()
        {
			EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
		}

		private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
			var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
			if (gameObject != null && gameObject.TryGetComponent(out Splitter splitter))
            {
				splitter.tag = splitter.EditorOnly ? Tags.EditorOnly : Tags.Untagged;

				var theme = splitter.GetTheme(EditorGUIUtility.isProSkin);
				var styleState = new GUIStyleState
                {
					textColor = theme.TextColor.WithAlpha(gameObject.activeSelf ? 1 : 0.5f)
				};
				var style = new GUIStyle
                {
					normal = styleState,
					fontStyle = splitter.FontStyle,
					alignment = splitter.TextAlignment,
					padding = splitter.Padding
				};

				selectionRect.width += 20;
				if (splitter.Extend)
                {
					var parentsCount = splitter.transform.GetParentsCount();

					// Числа подобранны вручную
					// Информации как получить ширину иерархии не нашёл
					var offset = parentsCount * 14;
					selectionRect.x -= offset + 27.5f;
					selectionRect.width += offset + 23;
				}

				EditorGUI.DrawRect(selectionRect, theme.BackgroundColor);
				EditorGUI.LabelField(selectionRect, splitter.name, style);
			}
		}
	}
}
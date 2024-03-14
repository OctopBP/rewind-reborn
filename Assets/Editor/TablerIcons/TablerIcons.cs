using UnityEngine;

namespace TablerIcons
{
	public static class TablerIcons
    {
		private const string FolderName = "icons/";
		private const string ResourcesPath = "Assets/Editor/TablerIcons/Resources/";

		private static string IconPath(string iconName) =>
			ResourcesPath + FolderName + iconName + ".png";

		public static Texture Icon(string iconName)
		{
			var path = FolderName + iconName;
			var texture = Resources.Load<Texture>(path);
			return texture;
		}

		public static void DrawIconGizmo(Vector3 position, string iconName) =>
			DrawIconGizmo(position, iconName, Color.white);

		public static void DrawIconGizmo(Vector3 position, string iconName, Color color) {
			var path = IconPath(iconName);
			Gizmos.DrawIcon(position, path, true, color);
		}
	}
}
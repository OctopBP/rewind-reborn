using UnityEngine;

namespace Rewind.Extensions {
	public static class StringExtensions {
		public static string wrapInColorTag(this string self, Color color) =>
			$"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{self}</color>";

		public static string wrapInBoldTag(this string self) => $"<b>{self}</b>";
	}
}




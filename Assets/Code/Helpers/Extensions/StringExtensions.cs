using System;
using UnityEngine;

namespace Rewind.Extensions {
	public static class StringExtensions {
		/// <summary> Wrap text in &lt;color={color}&gt;{text}&lt;/color&gt; tag. </summary>
		public static string wrapInColorTag(this string self, Color color) =>
			$"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{self}</color>";

		/// <summary> Wrap text in &lt;b&gt;{text}&lt;/b&gt; tag. </summary>
		public static string wrapInBoldTag(this string self) => $"<b>{self}</b>";
		
		
		/// <summary> Add tag [tag] on start. </summary>
		public static string addTagOnStart(this string self, string tag) => $"<b>[{tag}]</b> {self}";

		/// <summary>
		/// string methods StartsWith, EndsWith, IndexOf ... by default use
		/// StringComparison.CurrentCulture.
		/// That is about 30 times slower than StringComparison.Ordinal.
		/// </summary>
		public static bool startsWithFast(
			// ReSharper disable once MethodOverloadWithOptionalParameter
			this string s, string value, bool ignoreCase
		) => ignoreCase
			? s.StartsWith(value, StringComparison.OrdinalIgnoreCase)
			: startsWithFast(s, value);
		
		/// <summary> <see><cref>startsWithFast</cref></see> </summary>
		public static bool endsWithFast(
			// ReSharper disable once MethodOverloadWithOptionalParameter
			this string s, string value, bool ignoreCase
		) => ignoreCase
			? s.EndsWith(value, StringComparison.OrdinalIgnoreCase)
			: endsWithFast(s, value);
		
		/// <summary> <see><cref>startsWithFast</cref></see> </summary>
		public static int indexOfFast(
			this string s, string value, bool ignoreCase = false
		) => s.IndexOf(value, ordinalStringComparison(ignoreCase));
		
		static StringComparison ordinalStringComparison(bool ignoreCase) =>
			ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
		
		/// <summary>
		/// Even faster version of StartsWith taken from unity docs
		/// https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html
		/// </summary>
		public static bool startsWithFast(this string a, string b) {
			var aLen = a.Length;
			var bLen = b.Length;
			var ap = 0; 
			var bp = 0;
			while (ap < aLen && bp < bLen && a[ap] == b[bp]) {
				ap++;
				bp++;
			}
			return bp == bLen;
		}
    
		/// <summary>
		/// Even faster version of EndsWith taken from unity docs
		/// https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html
		/// </summary>
		public static bool endsWithFast(this string a, string b) {
			var ap = a.Length - 1;
			var bp = b.Length - 1;
			while (ap >= 0 && bp >= 0 && a[ap] == b[bp]) {
				ap--;
				bp--;
			}
			return bp < 0;
		}
	}
}




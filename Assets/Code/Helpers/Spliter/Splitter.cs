using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.Helpers {
	[ExecuteAlways]
	public class Splitter : MonoBehaviour {
		#region Unity Serialize Fields
		[SerializeField] bool differentColorForDarkTheme;

		[Header("$" + nameof(label))]
		[SerializeField] Color _textColor = Color.white;
		[SerializeField] Color _backgroundColor = Color.black;

		[Header("Dark theme")]
		[ShowIf(nameof(differentColorForDarkTheme))]
		[SerializeField] Color _textColorD = Color.white;
		[ShowIf(nameof(differentColorForDarkTheme))]
		[SerializeField] Color _backgroundColorD = Color.black;

		[Header("Settings")]
		[SerializeField] TextAnchor _textAlignment = TextAnchor.MiddleCenter;
		[SerializeField] FontStyle _fontStyle = FontStyle.Bold;
		[SerializeField] bool _extend;
		[SerializeField] RectOffset _padding = new();
		[SerializeField] bool _editorOnly = true;
		#endregion

		string label => differentColorForDarkTheme ? "Light theme" : "Theme";

		public record Theme(Color textColor, Color backgroundColor) {
			public Color textColor { get; } = textColor;
			public Color backgroundColor { get; } = backgroundColor;
		}

		public Theme getTheme(bool isDarkTheme) =>
			differentColorForDarkTheme && isDarkTheme
				? new(_textColorD, _backgroundColorD)
				: new(_textColor, _backgroundColor);

		public TextAnchor textAlignment => _textAlignment;
		public FontStyle fontStyle => _fontStyle;
		public bool extend => _extend;
		public RectOffset padding => _padding;
		public bool editorOnly => _editorOnly;

		void Update() {
			if (_editorOnly) {
				transform.DetachChildren();
			}
		}
	}
}
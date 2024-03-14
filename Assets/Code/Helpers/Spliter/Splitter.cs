using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.Helpers
{
	[ExecuteAlways]
	public class Splitter : MonoBehaviour
    {
		#region Unity Serialize Fields
		[SerializeField] private bool differentColorForDarkTheme;

		[Header("$" + nameof(Label))]
		[SerializeField] private Color _textColor = Color.white;
		[SerializeField] private Color _backgroundColor = Color.black;

		[Header("Dark theme")]
		[ShowIf(nameof(differentColorForDarkTheme))]
		[SerializeField] private Color _textColorD = Color.white;
		[ShowIf(nameof(differentColorForDarkTheme))]
		[SerializeField] private Color _backgroundColorD = Color.black;

		[Header("Settings")]
		[SerializeField] private TextAnchor _textAlignment = TextAnchor.MiddleCenter;
		[SerializeField] private FontStyle _fontStyle = FontStyle.Bold;
		[SerializeField] private bool _extend;
		[SerializeField] private RectOffset _padding = new();
		[SerializeField] private bool _editorOnly = true;
		#endregion

		private string Label => differentColorForDarkTheme ? "Light theme" : "Theme";

		public record Theme(Color TextColor, Color BackgroundColor)
        {
			public Color TextColor { get; } = TextColor;
			public Color BackgroundColor { get; } = BackgroundColor;
		}

		public Theme GetTheme(bool isDarkTheme) =>
			differentColorForDarkTheme && isDarkTheme
				? new(_textColorD, _backgroundColorD)
				: new(_textColor, _backgroundColor);

		public TextAnchor TextAlignment => _textAlignment;
		public FontStyle FontStyle => _fontStyle;
		public bool Extend => _extend;
		public RectOffset Padding => _padding;
		public bool EditorOnly => _editorOnly;

		private void Update()
        {
			if (_editorOnly)
            {
				transform.DetachChildren();
			}
		}
	}
}
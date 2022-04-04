using UnityEngine;

namespace Rewind.Helpers {
	[ExecuteAlways]
	public class Splitter : MonoBehaviour {
		[Header("Light theme")] [SerializeField]
		Color _textColor = Color.white;

		[SerializeField] Color _backgroundColor = Color.black;

		[Header("Dark theme")] [SerializeField]
		Color _textColorD = Color.white;

		[SerializeField] Color _backgroundColorD = Color.black;

		[Header("Settings")] [SerializeField] TextAnchor _textAlignment = TextAnchor.MiddleCenter;
		[SerializeField] bool _extend;

		public Color textColor => _textColor;
		public Color backgroundColor => _backgroundColor;
		public Color textColorD => _textColorD;
		public Color backgroundColorD => _backgroundColorD;
		public TextAnchor textAlignment => _textAlignment;
		public bool extend => _extend;

		void Update() => transform.DetachChildren();
	}
}
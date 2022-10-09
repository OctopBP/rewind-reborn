using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rewind.ECSCore {
	public partial class ClockBehaviour : IGameTimeListener, IClockStateListener2 {
		[SerializeField] Image bg;
		[SerializeField] Color recordColor;
		[SerializeField] Color rewindColor;
		[SerializeField] Color replayColor;

		[SerializeField] Transform arrow;
		[SerializeField] TMP_Text text;
		[SerializeField] float circleTime = 20;

		public void OnTime(GameEntity _, float value) {
			arrow.localRotation = Quaternion.AngleAxis(value.remap0(circleTime, 360), Vector3.back);
			text.SetText($"{value:F1}");
		}

		public void OnClockState(GameEntity _, ClockState value) {
			bg.color = value switch {
				ClockState.Record => recordColor,
				ClockState.Rewind => rewindColor,
				ClockState.Replay => replayColor,
				_ => Color.white
			};
		}
	}
}
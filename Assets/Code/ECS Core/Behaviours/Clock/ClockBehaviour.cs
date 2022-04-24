using Entitas;
using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;
using Rewind.ViewListeners;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rewind.ECSCore {
	public class ClockBehaviour : SelfInitializedView, IEventListener, IGameTimeListener, IClockStateListener {
		[SerializeField] Transform arrow;
		[SerializeField] Image bg;
		[SerializeField] Color recordColor;
		[SerializeField] Color rewindColor;
		[SerializeField] Color replayColor;
		[SerializeField] TMP_Text text;
		[SerializeField] float circleTime = 20;
		[SerializeField] ClockData data;

		protected override void onAwake() {
			base.onAwake();
			setupClock();
		}

		void setupClock() {
			entity.with(x => x.isClock = true);
			entity.AddClockState(ClockState.Record);
			entity.AddClockData(data);
			entity.AddTime(0);
		}

		public void registerListeners(IEntity _) {
			entity.AddGameTimeListener(this);
			entity.AddClockStateListener(this);
		}

		public void unregisterListeners(IEntity _) {
			entity.RemoveGameTimeListener(this);
			entity.RemoveClockStateListener(this);
		}

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
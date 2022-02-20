using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;
using Rewind.ViewListeners;
using UnityEngine;
using UnityEngine.UI;

namespace Rewind.ECSCore {
	public class ClockBehaviour : SelfInitializedView, IEventListener, IGameTimeListener, IClockStateListener {
		[SerializeField] Transform arrow;
		[SerializeField] Image bg;
		[SerializeField] Color recordColor;
		[SerializeField] Color rewindColor;
		[SerializeField] Color replayColor;

		protected override void onAwake() {
			base.onAwake();
			setupClock();
		}

		void setupClock() {
			entity.with(x => x.isClock = true);
			entity.AddClockState(ClockState.Record);
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

		public void OnTime(GameEntity _, float value) =>
			arrow.localRotation = Quaternion.AngleAxis(value * 6, Vector3.back); // 360 deg to 60 seconds

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
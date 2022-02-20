using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;
using Rewind.ViewListeners;
using UnityEngine;
using UnityEngine.UI;

namespace Rewind.ECSCore {
	public class ClockBehaviour : SelfInitializedView, IEventListener, IGameTickListener, IClockStateListener {
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
			entity.AddTick(0);
		}

		public void registerListeners(IEntity _) {
			entity.AddGameTickListener(this);
			entity.AddClockStateListener(this);
		}

		public void unregisterListeners(IEntity _) {
			entity.RemoveGameTickListener(this);
			entity.RemoveClockStateListener(this);
		}

		public void OnTick(GameEntity _, int value) =>
			arrow.localRotation = Quaternion.AngleAxis((float) value / 10, Vector3.back);

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
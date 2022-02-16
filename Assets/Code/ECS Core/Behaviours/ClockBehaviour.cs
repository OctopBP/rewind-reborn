using Entitas;
using Rewind.Extensions;
using Rewind.Services;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.ECSCore {
	public class ClockBehaviour : SelfInitializedView, IEventListener, IGameTickListener {
		[SerializeField] Transform arrow;

		protected override void onAwake() {
			base.onAwake();
			setupClock();
		}

		void setupClock() {
			entity.with(x => x.isClock = true);
			entity.AddTick(0);
		}

		public void registerListeners(IEntity _) => entity.AddGameTickListener(this);
		public void unregisterListeners(IEntity with) => entity.RemoveGameTickListener(this);

		public void OnTick(GameEntity _, int value) =>
			arrow.localRotation = Quaternion.AngleAxis((float) value / 10, Vector3.back);
	}
}
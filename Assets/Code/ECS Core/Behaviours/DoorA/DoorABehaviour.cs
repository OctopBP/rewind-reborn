using System;
using System.Collections.Generic;
using Code.Base;
using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public class DoorABehaviour : SelfInitializedViewWithId, IEventListener, IDoorAStateListener, IStatusValue {
		[SerializeField] List<PathPointType> pointsIndex;
		[SerializeField] DoorAState state = DoorAState.Closed;

		[SerializeField] Transform doorTransform;
		[SerializeField] float openScale;
		[SerializeField] float closedScale;

		public List<PathPointType> getPointsIndex => pointsIndex;

		public float statusValue => entity.doorAState.value switch {
			DoorAState.Opened => 1,
			DoorAState.Closed => 0,
			_ => throw new ArgumentOutOfRangeException()
		};

		protected override void onAwake() {
			base.onAwake();
			setupDoorA();
		}

		void setupDoorA() {
			entity.with(x => x.isDoorA = true);
			entity.AddDoorAState(state);
			entity.AddDoorAPoints(pointsIndex);
		}

		public void registerListeners(IEntity _) => entity.AddDoorAStateListener(this);
		public void unregisterListeners(IEntity _) => entity.RemoveDoorAStateListener(this);

		public void OnDoorAState(GameEntity _, DoorAState value) {
			state = value;
			doorTransform.localScale = new(1, state.isOpened() ? openScale : closedScale, 1);
		}
	}
}
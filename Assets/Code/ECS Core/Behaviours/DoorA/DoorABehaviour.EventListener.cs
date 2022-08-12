using Rewind.ECSCore.Enums;
using Rewind.Services;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class DoorABehaviour : IEventListener, IDoorAStateListener {
		[SerializeField] Transform doorTransform;
		[SerializeField] float openScale;
		[SerializeField] float closedScale;
		
		public void registerListeners() => entity.AddDoorAStateListener(this);
		public void unregisterListeners() => entity.RemoveDoorAStateListener(this);

		public void OnDoorAState(GameEntity _, DoorAState value) {
			state = value;
			doorTransform.localScale = new(1, state.isOpened() ? openScale : closedScale, 1);
		}
	}
}
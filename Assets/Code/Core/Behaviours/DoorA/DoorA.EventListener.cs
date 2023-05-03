using Rewind.SharedData;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class DoorA : IDoorAStateListener {
		[SerializeField] Transform doorTransform;
		[SerializeField] float openScale;
		[SerializeField] float closedScale;

		public void OnDoorAState(GameEntity _, DoorAState value) =>
			doorTransform.localScale = new(1, value.isOpened() ? openScale : closedScale, 1);
	}
}
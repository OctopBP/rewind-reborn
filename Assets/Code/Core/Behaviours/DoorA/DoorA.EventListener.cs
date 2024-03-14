using Rewind.SharedData;
using UnityEngine;

namespace Rewind.Behaviours
{
	public partial class DoorA : IDoorAStateListener
	{
		[SerializeField] private Transform doorTransform;
		[SerializeField] private float openScale;
		[SerializeField] private float closedScale;

		public void OnDoorAState(GameEntity _, DoorAState value) =>
			doorTransform.localScale = new(1, value.IsOpened() ? openScale : closedScale, 1);
	}
}
using System;
using Code.Base;
using Rewind.ECSCore.Enums;

namespace Rewind.Behaviours {
	public partial class DoorABehaviour : IStatusValue {
		public float statusValue => entity.doorAState.value switch {
			DoorAState.Opened => 1,
			DoorAState.Closed => 0,
			_ => throw new ArgumentOutOfRangeException()
		};
	}
}
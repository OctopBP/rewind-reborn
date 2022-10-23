using System;
using Code.Base;
using Rewind.ECSCore.Enums;

namespace Rewind.Behaviours {
	public partial class GearTypeA : IStatusValue {
		public float statusValue => model.entity.gearTypeAState.value switch {
			GearTypeAState.Closed => 0,
			GearTypeAState.Closing => 0.4f,
			GearTypeAState.Opening => 0.6f,
			GearTypeAState.Opened => 1,
			_ => throw new ArgumentOutOfRangeException()
		};
	}
}
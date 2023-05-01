using Code.Base;
using ExhaustiveMatching;
using Rewind.ECSCore.Enums;

namespace Rewind.Behaviours {
	public partial class DoorA : IStatusValue {
		public float statusValue => model.entity.doorAState.value switch {
			DoorAState.Opened => 1,
			DoorAState.Closed => 0,
			_ => throw ExhaustiveMatch.Failed(model.entity.doorAState.value)
		};
	}
}
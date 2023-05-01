using Code.Base;
using ExhaustiveMatching;
using Rewind.ECSCore.Enums;

namespace Rewind.Behaviours {
	public partial class ButtonA : IStatusValue {
		public float statusValue => model.state switch {
			ButtonAState.Closed => 0,
			ButtonAState.Opened => 1,
			_ => throw ExhaustiveMatch.Failed(model.state)
		};
	}
}
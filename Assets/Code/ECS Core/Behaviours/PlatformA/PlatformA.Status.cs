using Code.Base;
using ExhaustiveMatching;
using Rewind.ECSCore.Enums;

namespace Rewind.Behaviours {
	public partial class PlatformA : IStatusValue {
		public float statusValue => model.entity.platformAState.value switch {
			PlatformAState.Active => 1,
			PlatformAState.NotActive => 0,
			_ => throw ExhaustiveMatch.Failed(model.entity.platformAState.value)
		};
	}
}
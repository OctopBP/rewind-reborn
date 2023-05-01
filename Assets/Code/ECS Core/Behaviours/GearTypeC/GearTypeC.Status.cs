using Code.Base;
using ExhaustiveMatching;
using LanguageExt;
using Rewind.ECSCore.Enums;

namespace Rewind.Behaviours {
	public partial class GearTypeC : IStatusValue {
		public Option<float> statusValue => model.entity.leverAState.value switch {
			LeverAState.Closed => 0,
			LeverAState.Opened => 1,
			_ => throw ExhaustiveMatch.Failed(model.entity.leverAState.value)
		};
	}
}
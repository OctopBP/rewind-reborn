using Code.Base;
using ExhaustiveMatching;
using Rewind.ECSCore.Enums;

namespace Rewind.Behaviours {
	public partial class Pendulum : IStatusValue {
		public float statusValue => model.entity.pendulumState.value switch {
			PendulumState.Active => 1,
			PendulumState.NotActive => 0,
			_ => throw ExhaustiveMatch.Failed(model.entity.pendulumState.value)
		};
	}
}
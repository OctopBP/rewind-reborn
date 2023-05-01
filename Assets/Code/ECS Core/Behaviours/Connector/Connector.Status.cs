using Code.Base;
using ExhaustiveMatching;
using Rewind.ECSCore.Enums;

namespace Rewind.Behaviours {
	public partial class Connector : IStatusValue {
		public float statusValue => model.state.Value switch {
			ConnectorState.Closed => 0,
			ConnectorState.Opened => 1,
			_ => throw ExhaustiveMatch.Failed(model.state.Value)
		};
	}
}
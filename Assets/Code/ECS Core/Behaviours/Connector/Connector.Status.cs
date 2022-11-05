using System;
using Code.Base;
using Rewind.ECSCore.Enums;

namespace Rewind.Behaviours {
	public partial class Connector : IStatusValue {
		public float statusValue => model.state.Value switch {
			ConnectorState.Closed => 0,
			ConnectorState.Opened => 1,
			_ => throw new ArgumentOutOfRangeException()
		};
	}
}
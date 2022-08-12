using System;
using Code.Base;
using Rewind.ECSCore.Enums;

namespace Rewind.Behaviours {
	public partial class ConnectorBehaviour : IStatusValue {
		public float statusValue => entity.connectorState.value switch {
			ConnectorState.Closed => 0,
			ConnectorState.Opened => 1,
			_ => throw new ArgumentOutOfRangeException()
		};
	}
}
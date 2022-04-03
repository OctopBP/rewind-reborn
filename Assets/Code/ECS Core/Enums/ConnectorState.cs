using System;

namespace Rewind.ECSCore.Enums {
	public enum ConnectorState : short { Closed, Opened }

	public static class ConnectorStateExt {
		public static bool isClosed(this ConnectorState self) => self == ConnectorState.Closed;
		public static bool isOpened(this ConnectorState self) => self == ConnectorState.Opened;
	}
}

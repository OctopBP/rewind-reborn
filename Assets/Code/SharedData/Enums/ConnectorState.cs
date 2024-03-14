namespace Rewind.SharedData
{
	public enum ConnectorState : short { Closed, Opened }

	public static class ConnectorStateExt
	{
		public static bool IsClosed(this ConnectorState self) => self == ConnectorState.Closed;
		public static bool IsOpened(this ConnectorState self) => self == ConnectorState.Opened;
	}
}

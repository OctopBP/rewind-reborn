using System;

namespace Rewind.ECSCore.Enums {
	public enum ClockState { Record, Rewind, Replay }

	public static class ClockStateExt {
		public static bool isRecord(this ClockState self) => self == ClockState.Record;
		public static bool isRewind(this ClockState self) => self == ClockState.Rewind;
		public static bool isReplay(this ClockState self) => self == ClockState.Replay;

		public static int timeDirection(this ClockState self) => self switch {
			ClockState.Record => 1,
			ClockState.Rewind => -1,
			ClockState.Replay => 1,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
	}
}
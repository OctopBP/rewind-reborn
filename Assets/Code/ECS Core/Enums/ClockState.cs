using System;

namespace Rewind.ECSCore.Enums {
	public enum ClockState : short { Record, Rewind, Replay }

	public static class ClockStateExt {
		public static bool isRecord(this ClockState self) => self == ClockState.Record;
		public static bool isRewind(this ClockState self) => self == ClockState.Rewind;
		public static bool isReplay(this ClockState self) => self == ClockState.Replay;

		public static short timeDirection(this ClockState self) => self switch {
			ClockState.Record => 1,
			ClockState.Rewind => -1,
			ClockState.Replay => 1,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};

		public static T map<T>(this ClockState self, T onRecord, T onRewind, T onReplay) =>
			self switch {
				ClockState.Record => onRecord,
				ClockState.Rewind => onRewind,
				ClockState.Replay => onReplay,
				_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
			};
	}
}
using System;

namespace Rewind.SharedData
{
	public enum ClockState : short { Record, Rewind, Replay }

	public static class ClockStateExt
	{
		public static bool IsRecord(this ClockState self) => self == ClockState.Record;
		public static bool IsRewind(this ClockState self) => self == ClockState.Rewind;
		public static bool IsReplay(this ClockState self) => self == ClockState.Replay;

		public static short TimeDirectionMultiplayer(this ClockState self) => self switch
		{
			ClockState.Record => 1,
			ClockState.Rewind => -1,
			ClockState.Replay => 1,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};

		public static void Fold(this ClockState self, Action onRecord, Action onRewind, Action onReplay)
		{
			switch (self)
			{
				case ClockState.Record: onRecord?.Invoke(); return;
				case ClockState.Rewind: onRewind?.Invoke(); return;
				case ClockState.Replay: onReplay?.Invoke(); return;
				default: throw new ArgumentOutOfRangeException(nameof(self), self, null);
			};
		}

		public static T Fold<T>(this ClockState self, T onRecord, T onRewind, T onReplay) => self switch
		{
			ClockState.Record => onRecord,
			ClockState.Rewind => onRewind,
			ClockState.Replay => onReplay,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
	}
}
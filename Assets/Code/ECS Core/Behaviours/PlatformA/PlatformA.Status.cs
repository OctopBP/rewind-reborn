using System;
using Code.Base;
using Rewind.ECSCore.Enums;

namespace Rewind.Behaviours {
	public partial class PlatformA : IStatusValue {
		public float statusValue => model.entity.platformAState.value switch {
			PlatformAState.Active => 1,
			PlatformAState.NotActive => 0,
			_ => throw new ArgumentOutOfRangeException()
		};
	}
}
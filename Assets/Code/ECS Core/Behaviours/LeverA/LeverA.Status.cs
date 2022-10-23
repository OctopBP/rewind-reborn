using System;
using Code.Base;
using Rewind.ECSCore.Enums;

namespace Rewind.Behaviours {
	public partial class LeverA : IStatusValue {
		public float statusValue => model.entity.leverAState.value switch {
			LeverAState.Closed => 0,
			LeverAState.Opened => 1,
			_ => throw new ArgumentOutOfRangeException()
		};
	}
}
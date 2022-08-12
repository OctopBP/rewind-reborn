using System;
using Code.Base;
using Rewind.ECSCore.Enums;

namespace Rewind.Behaviours {
	public partial class ButtonABehaviour : IStatusValue {
		public float statusValue => entity.buttonAState.value switch {
			ButtonAState.Closed => 0,
			ButtonAState.Opened => 1,
			_ => throw new ArgumentOutOfRangeException()
		};
	}
}
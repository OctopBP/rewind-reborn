using System;
using Code.Base;
using Rewind.ECSCore.Enums;

namespace Rewind.Behaviours {
	public partial class ButtonA : IStatusValue {
		public float statusValue => model.state switch {
			ButtonAState.Closed => 0,
			ButtonAState.Opened => 1,
			_ => throw new ArgumentOutOfRangeException()
		};
	}
}
using System;
using Code.Base;
using Rewind.ECSCore.Enums;

namespace Rewind.Behaviours {
	public partial class PlatformABehaviour : IStatusValue {
		GameEntity entity;

		public float statusValue => entity.platformAState.value switch {
			PlatformAState.Active => 1,
			PlatformAState.NotActive => 0,
			_ => throw new ArgumentOutOfRangeException()
		};

		void createStatus(GameEntity entity) {
			this.entity = entity;
		}
	}
}
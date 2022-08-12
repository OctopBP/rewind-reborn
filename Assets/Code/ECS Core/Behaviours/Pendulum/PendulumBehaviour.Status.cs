using System;
using Code.Base;
using Rewind.ECSCore.Enums;

namespace Rewind.Behaviours {
	public partial class PendulumBehaviour : IStatusValue {
		GameEntity entity;

		public float statusValue => entity.pendulumState.value switch {
			PendulumState.Active => 1,
			PendulumState.NotActive => 0,
			_ => throw new ArgumentOutOfRangeException()
		};

		void createStatus(GameEntity entity) {
			this.entity = entity;
		}
	}
}
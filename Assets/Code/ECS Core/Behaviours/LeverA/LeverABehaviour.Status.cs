using System;
using Code.Base;
using Rewind.ECSCore.Enums;

namespace Rewind.Behaviours {
	public partial class LeverABehaviour : IStatusValue {
		Status status;
		public float statusValue => status.statusValue;

		class Status : IStatusValue {
			readonly GameEntity entity;

			public Status(GameEntity entity) {
				this.entity = entity;
			}

			public float statusValue => entity.leverAState.value switch {
				LeverAState.Closed => 0,
				LeverAState.Opened => 1,
				_ => throw new ArgumentOutOfRangeException()
			};
		}

		void createStatus(GameEntity entity) {
			status = new(entity);
		}
	}
}
using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Services;
using UnityEngine;

public class PendulumSwaySystem : IExecuteSystem {
	readonly GameEntity clock;
	readonly IGroup<GameEntity> pendulums;

	public PendulumSwaySystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
		pendulums = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Pendulum, GameMatcher.PendulumData, GameMatcher.Rotation, GameMatcher.PendulumSwayTime,
			GameMatcher.PendulumState
		));
	}

	public void Execute() {
		foreach (var pendulum in pendulums.where(p => p.pendulumState.value.isActive())) {
			var data = pendulum.pendulumData.value;
			var swayTime = pendulum.pendulumSwayTime.value;

			var newSwayTime = swayTime + clock.deltaTime.value;
			var angle = Mathf.Sin(swayTime * data.swayPeriod) * data.swayLimit;

			pendulum.ReplacePendulumSwayTime(newSwayTime);
			pendulum.ReplaceRotation(angle);
		}
	}
}
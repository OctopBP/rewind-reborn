using Entitas;
using Rewind.SharedData;
using Rewind.Services;
using UnityEngine;

public class PendulumSwaySystem : IExecuteSystem
{
	private readonly GameEntity clock;
	private readonly IGroup<GameEntity> pendulums;

	public PendulumSwaySystem(Contexts contexts)
	{
		clock = contexts.game.clockEntity;
		pendulums = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Pendulum, GameMatcher.PendulumData, GameMatcher.Rotation, GameMatcher.PendulumSwayTime,
			GameMatcher.PendulumState
		));
	}

	public void Execute()
	{
		foreach (var pendulum in pendulums.Where(p => p.pendulumState.value.IsActive()))
		{
			var data = pendulum.pendulumData.value;
			var swayTime = pendulum.pendulumSwayTime.value;

			var newSwayTime = swayTime + clock.deltaTime.value;
			var angle = Mathf.Sin(swayTime * data._swayPeriod) * data._swayLimit;

			pendulum.ReplacePendulumSwayTime(newSwayTime);
			pendulum.ReplaceRotation(angle);
		}
	}
}
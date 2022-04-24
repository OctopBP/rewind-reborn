using Entitas;
using Rewind.ECSCore.Enums;

public class TimeSystem : IExecuteSystem {
	readonly IGroup<GameEntity> clocks;

	public TimeSystem(Contexts contexts) {
		clocks = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Clock, GameMatcher.ClockData, GameMatcher.ClockState
		));
	}

	public void Execute() {
		foreach (var clock in clocks.GetEntities()) {
			var currentTime = clock.time.value;

			var delta = clock.deltaTime.value * clock.clockState.value.timeDirection();
			clock.ReplaceTime(currentTime + delta);
		}
	}
}
using Entitas;
using Rewind.SharedData;

public class TimeSystem : IExecuteSystem {
	readonly IGroup<GameEntity> clocks;

	public TimeSystem(Contexts contexts) {
		clocks = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Clock, GameMatcher.Time, GameMatcher.ClockState
		));
	}

	public void Execute() {
		foreach (var clock in clocks.GetEntities()) {
			var currentTime = clock.time.value;

			var delta = clock.deltaTime.value * clock.clockState.value.timeDirectionMultiplayer();
			clock.ReplaceTime(currentTime + delta);
		}
	}
}
using Entitas;
using Rewind.SharedData;
using Rewind.ECSCore.Helpers;
using static Rewind.SharedData.GearTypeAState;
using static LanguageExt.Prelude;

public class GearTypeAStateSystem : IExecuteSystem {
	readonly GameContext game;
	readonly IGroup<GameEntity> gears;

	public GearTypeAStateSystem(Contexts contexts) {
		game = contexts.game;
		gears = game.GetGroup(GameMatcher.AllOf(
			GameMatcher.GearTypeA, GameMatcher.GearTypeAData, GameMatcher.Rotation
		));
	}

	public void Execute() {
		var clockState = game.clockEntity.clockState.value;

		foreach (var gear in gears.GetEntities()) {
			var currentState = gear.gearTypeAState.value;
			if (clockState.isRewind() || (clockState.isReplay() && gear.hasHoldedAtTime)) {
				(currentState switch {
					Opening => gear.rotation.value >= gear.gearTypeAData.value._rotateLimit
						? Some(Opened)
						: None,
					Closing => gear.rotation.value <= 0
						? Some(Closed)
						: None,
					_ => None
				}).IfSome(state => gear.ReplaceGearTypeAState(state));
			} else {
				(currentState switch {
					Closed => gear.isActive ? Some(Opening) : None,
					Opened => gear.isActive ? None : Some(Closing),
					Opening => !gear.isActive
						? Some(Closing)
						: gear.rotation.value >= gear.gearTypeAData.value._rotateLimit
							? Some(Opened)
							: None,
					Closing => gear.isActive
						? Some(Opening)
						: gear.rotation.value <= 0
							? Some(Closed)
							: None,
					_ => None
				}).IfSome(newState => {
					game.createGearATimePoint(gear.id.value, currentState, newState, gear.rotation.value);
					gear.ReplaceGearTypeAState(newState);
					if (clockState.isRecord()) {
						gear.ReplaceHoldedAtTime(game.clockEntity.time.value);
					}
				});
			}
		}
	}
}
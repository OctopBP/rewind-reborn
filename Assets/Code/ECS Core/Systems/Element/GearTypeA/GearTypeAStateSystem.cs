using Entitas;
using LanguageExt;
using Rewind.ECSCore.Enums;
using Rewind.ECSCore.Helpers;
using static Rewind.ECSCore.Enums.GearTypeAState;

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
		if (clockState.isRewind()) return;

		foreach (var gear in gears.GetEntities()) {
			if (clockState.isReplay() && gear.hasHoldedAtTime) continue;

			var currentState = gear.gearTypeAState.value;
			(currentState switch {
				Closed => gear.isActive ? Opening : Option<GearTypeAState>.None,
				Opened => gear.isActive ? Option<GearTypeAState>.None : Closing,
				Opening => !gear.isActive
					? Closing
					: gear.rotation.value >= gear.gearTypeAData.value.rotateLimit
						? Opened
						: Option<GearTypeAState>.None,
				Closing => gear.isActive
					? Opening
					: gear.rotation.value <= 0
						? Closed
						: Option<GearTypeAState>.None,
				_ => Option<GearTypeAState>.None
			}).IfSome(newState => {
				game.createGearTimePoint(gear.id.value, currentState, newState, gear.rotation.value);
				gear.ReplaceGearTypeAState(newState);
				if (clockState.isRecord()) {
					gear.ReplaceHoldedAtTime(game.clockEntity.time.value);
				}
			});
		}
	}
}
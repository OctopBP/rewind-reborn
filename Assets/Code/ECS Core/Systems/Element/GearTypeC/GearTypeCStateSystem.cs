using Entitas;
using LanguageExt;
using Rewind.ECSCore.Enums;
using Rewind.ECSCore.Helpers;
using static Rewind.ECSCore.Enums.GearTypeCState;

public class GearTypeCStateSystem : IExecuteSystem {
	readonly GameContext game;
	readonly IGroup<GameEntity> gears;

	public GearTypeCStateSystem(Contexts contexts) {
		game = contexts.game;
		gears = game.GetGroup(GameMatcher.AllOf(
			GameMatcher.GearTypeC, GameMatcher.GearTypeCData, GameMatcher.Rotation
		));
	}

	public void Execute() {
		var clockState = game.clockEntity.clockState.value;

		foreach (var gear in gears.GetEntities()) {
			var currentState = gear.gearTypeCState.value;
			if (clockState.isRewind() || (clockState.isReplay() && gear.hasHoldedAtTime)) {
				(currentState switch {
				// 	RotationLeft => gear.rotation.value > 0
				// 		? Closed
				// 		: Option<GearTypeCState>.None,
					// RotationRight => gear.rotation.value < 0
					// 	? Closed
					// 	: Option<GearTypeCState>.None,
					_ => Option<GearTypeCState>.None
				}).IfSome(gear.ReplaceGearTypeCState);
			} else {
				GearTypeCState newState;
				if (gear.isActive) {
					newState = RotationRight;
				} else if (gear.isActiveSecond) {
					newState = RotationLeft;
				} else {
					newState = gear.rotation.value switch {
						< 0 => RotationLeft,
						> 0 => RotationRight,
						_ => Closed
					};
				}

				if (newState != currentState) {
					game.createGearCTimePoint(gear.id.value, currentState, newState, gear.rotation.value);
					gear.ReplaceGearTypeCState(newState);
					if (clockState.isRecord()) {
						gear.ReplaceHoldedAtTime(game.clockEntity.time.value);
					}
				}
			}
		}
	}
}
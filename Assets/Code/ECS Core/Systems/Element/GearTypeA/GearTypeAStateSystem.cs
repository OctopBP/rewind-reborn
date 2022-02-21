using System;
using Entitas;
using LanguageExt;
using Rewind.ECSCore.Enums;
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
		if (!game.clockEntity.clockState.value.isRecord()) return;

		foreach (var gear in gears.GetEntities()) {
			var currentState = gear.gearTypeAState.value;
			(currentState switch {
				Closed => gear.isActive
					? Opening
					: Option<GearTypeAState>.None,
				Opened => gear.isActive
					? Option<GearTypeAState>.None
					: Closing,
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
			}).IfSome(state => {
				createTimePoint(gear.id.value, currentState);
				gear.ReplaceGearTypeAState(state);
			});
		}
	}

	void createTimePoint(Guid id, GearTypeAState state) {
		var point = game.CreateEntity();
		point.AddIdRef(id);
		point.AddGearTypeAState(state);
		point.AddTimePoint(game.clockEntity.time.value);
	}
}
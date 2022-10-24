using System;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;

namespace Rewind.ECSCore.Helpers {
	public static class TimePoints {
		public static GameEntity createGearATimePoint(
			this GameContext game, Guid id, GearTypeAState from, GearTypeAState to, float angle
		) => game.CreateEntity()
			.with(e => e.AddIdRef(id))
			.with(e => e.AddGearTypeAPreviousState(from))
			.with(e => e.AddGearTypeAState(to))
			.with(e => e.AddTimePoint(game.clockEntity.time.value))
			.with(e => e.AddRotation(angle));

		public static GameEntity createGearCTimePoint(
			this GameContext game, Guid id, GearTypeCState from, GearTypeCState to, float angle
		) => game.CreateEntity()
			.with(e => e.AddIdRef(id))
			.with(e => e.AddGearTypeCPreviousState(from))
			.with(e => e.AddGearTypeCState(to))
			.with(e => e.AddTimePoint(game.clockEntity.time.value))
			.with(e => e.AddRotation(angle));

		public static GameEntity createMoveTimePoint(
			this GameContext game, PathPointType pointIndex,
			PathPointType previousPointIndex, PathPointType rewindPointIndex
		) => game.CreateEntity()
			.with(e => e.AddTimePoint(game.clockEntity.time.value))
			.with(e => e.AddPointIndex(pointIndex))
			.with(e => e.AddPreviousPointIndex(previousPointIndex))
			.with(e => e.AddRewindPointIndex(rewindPointIndex));

		public static GameEntity createButtonATimePoint(this GameContext game, Guid id, ButtonAState to) => game
			.CreateEntity()
			.with(e => e.AddIdRef(id))
			.with(e => e.AddButtonAState(to))
			.with(e => e.AddTimePoint(game.clockEntity.time.value));

		public static GameEntity createLeverATimePoint(this GameContext game, Guid id, LeverAState to) => game
			.CreateEntity()
			.with(e => e.AddIdRef(id))
			.with(e => e.AddLeverAState(to))
			.with(e => e.AddTimePoint(game.clockEntity.time.value));
	}
}
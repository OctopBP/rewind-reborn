using System;
using Rewind.SharedData;

namespace Rewind.ECSCore.Helpers {
	public static class TimePoints {
		public static GameEntity createGearATimePoint(
			this GameContext game, Guid id, GearTypeAState from, GearTypeAState to, float angle
		) => game.CreateEntity()
			.AddIdRef(id)
			.AddGearTypeAPreviousState(from)
			.AddGearTypeAState(to)
			.AddTimestamp(game.clockEntity.time.value)
			.AddRotation(angle);

		public static GameEntity createGearCTimePoint(
			this GameContext game, Guid id, GearTypeCState from, GearTypeCState to, float angle
		) => game.CreateEntity()
			.AddIdRef(id)
			.AddGearTypeCPreviousState(from)
			.AddGearTypeCState(to)
			.AddTimestamp(game.clockEntity.time.value)
			.AddRotation(angle);

		public static GameEntity createMoveTimePoint(
			this GameContext game, PathPoint currentPoint,
			PathPoint previousPoint, PathPoint rewindPoint
		) => game.CreateEntity()
			.AddTimestamp(game.clockEntity.time.value)
			.AddCurrentPoint(currentPoint)
			.AddPreviousPoint(previousPoint)
			.AddRewindPoint(rewindPoint);

		public static GameEntity createMoveCompleteTimePoint(this GameContext game, bool isMoveComplete) => game
			.CreateEntity()
			.AddTimestamp(game.clockEntity.time.value)
			.AddMoveComplete(isMoveComplete);

		public static GameEntity createButtonATimePoint(this GameContext game, Guid id, ButtonAState to) => game
			.CreateEntity()
			.AddIdRef(id)
			.AddButtonAState(to)
			.AddTimestamp(game.clockEntity.time.value);

		public static GameEntity createLeverATimePoint(this GameContext game, Guid id, LeverAState to) => game
			.CreateEntity()
			.AddIdRef(id)
			.AddLeverAState(to)
			.AddTimestamp(game.clockEntity.time.value);
	}
}
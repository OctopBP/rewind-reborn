using System;
using Rewind.SharedData;

namespace Rewind.ECSCore.Helpers
{
	public static class TimePoints
	{
		public static GameEntity CreateGearATimePoint(
			this GameContext game, Guid id, GearTypeAState from, GearTypeAState to, float angle
		)
		{
			return game.CreateEntity()
				.AddIdRef(id)
				.AddGearTypeAPreviousState(from)
				.AddGearTypeAState(to)
				.AddTimestamp(game.clockEntity.time.value)
				.AddRotation(angle);
		}

		public static GameEntity CreateGearCTimePoint(
			this GameContext game, Guid id, GearTypeCState from, GearTypeCState to, float angle
		)
		{
			return game.CreateEntity()
				.AddIdRef(id)
				.AddGearTypeCPreviousState(from)
				.AddGearTypeCState(to)
				.AddTimestamp(game.clockEntity.time.value)
				.AddRotation(angle);
		}

		public static GameEntity CreateMoveTimePoint(
			this GameContext game, PathPoint currentPoint, PathPoint previousPoint, PathPoint rewindPoint
		)
		{
			return game.CreateEntity()
				.AddTimestamp(game.clockEntity.time.value)
				.AddCurrentPoint(currentPoint)
				.AddPreviousPoint(previousPoint)
				.AddRewindPoint(rewindPoint);
		}

		public static GameEntity CreateMoveCompleteTimePoint(this GameContext game, bool isMoveComplete)
		{
			return game
				.CreateEntity()
				.AddTimestamp(game.clockEntity.time.value)
				.AddMoveComplete(isMoveComplete);
		}

		public static GameEntity CreateButtonATimePoint(this GameContext game, Guid id, ButtonAState to)
		{
			return game
				.CreateEntity()
				.AddIdRef(id)
				.AddButtonAState(to)
				.AddTimestamp(game.clockEntity.time.value);
		}

		public static GameEntity CreateLeverATimePoint(this GameContext game, Guid id, LeverAState to)
		{
			return game
				.CreateEntity()
				.AddIdRef(id)
				.AddLeverAState(to)
				.AddTimestamp(game.clockEntity.time.value);
		}
	}
}
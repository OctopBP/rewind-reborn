using System;
using Rewind.ECSCore.Enums;

namespace Rewind.ECSCore.Helpers {
	public static class TimePoints {
		public static GameEntity createGearTimePoint(
			this GameContext game, Guid id, GearTypeAState state
		) {
			var point = game.CreateEntity();
			point.AddIdRef(id);
			point.AddGearTypeAState(state);
			point.AddTimePoint(game.clockEntity.time.value);
			return point;
		}

		public static GameEntity createMoveTimePoint(
			this GameContext game, int pointIndex, int previousPointIndex,
			int pathIndex, int previousPathIndex, int rewindPointIndex
		) {
			var point = game.CreateEntity();
			point.AddTimePoint(game.clockEntity.time.value);
			point.AddPointIndex(pointIndex);
			point.AddPreviousPointIndex(previousPointIndex);
			point.AddRewindPointIndex(rewindPointIndex);
			point.AddPathIndex(pathIndex);
			point.AddPreviousPathIndex(previousPathIndex);
			return point;
		}
	}
}
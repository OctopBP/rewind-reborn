using System;
using Rewind.ECSCore.Enums;

namespace Rewind.ECSCore.Helpers {
	public static class TimePoints {
		public static GameEntity createGearTimePoint(
			this GameContext game, Guid id, GearTypeAState from, GearTypeAState to, float angle
		) {
			var point = game.CreateEntity();
			point.AddIdRef(id);
			point.AddGearTypeAPreviousState(from);
			point.AddGearTypeAState(to);
			point.AddTimePoint(game.clockEntity.time.value);
			point.AddRotation(angle);
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
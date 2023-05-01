using System;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using UnityEngine;

namespace Rewind.ECSCore.Helpers {
	public static class TimePoints {
		public static GameEntity createGearATimePoint(
			this GameContext game, Guid id, GearTypeAState from, GearTypeAState to, float angle
		) => game.CreateEntity()
			.with(e => e.AddIdRef(id))
			.with(e => e.AddGearTypeAPreviousState(from))
			.with(e => e.AddGearTypeAState(to))
			.with(e => e.AddTimestamp(game.clockEntity.time.value))
			.with(e => e.AddRotation(angle));

		public static GameEntity createGearCTimePoint(
			this GameContext game, Guid id, GearTypeCState from, GearTypeCState to, float angle
		) => game.CreateEntity()
			.with(e => e.AddIdRef(id))
			.with(e => e.AddGearTypeCPreviousState(from))
			.with(e => e.AddGearTypeCState(to))
			.with(e => e.AddTimestamp(game.clockEntity.time.value))
			.with(e => e.AddRotation(angle));

		public static GameEntity createMoveTimePoint(
			this GameContext game, PathPoint currentPoint,
			PathPoint previousPoint, PathPoint rewindPoint
		) => game.CreateEntity()
			.with(e => e.AddTimestamp(game.clockEntity.time.value))
			.with(e => e.AddCurrentPoint(currentPoint))
			.with(e => e.AddPreviousPoint(previousPoint))
			.with(e => e.AddRewindPoint(rewindPoint));

		public static GameEntity createMoveCompleteTimePoint(this GameContext game, bool isMoveComplete) => game
			.CreateEntity()
			.with(e => e.AddTimestamp(game.clockEntity.time.value))
			.with(e => e.AddMoveComplete(isMoveComplete));

		public static GameEntity createButtonATimePoint(this GameContext game, Guid id, ButtonAState to) => game
			.CreateEntity()
			.with(e => e.AddIdRef(id))
			.with(e => e.AddButtonAState(to))
			.with(e => e.AddTimestamp(game.clockEntity.time.value));

		public static GameEntity createLeverATimePoint(this GameContext game, Guid id, LeverAState to) => game
			.CreateEntity()
			.with(e => e.AddIdRef(id))
			.with(e => e.AddLeverAState(to))
			.with(e => e.AddTimestamp(game.clockEntity.time.value));
	}
}
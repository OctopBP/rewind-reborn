using System;
using ExhaustiveMatching;
using Rewind.Behaviours;
using Rewind.Extensions;
using Rewind.Services;
using Rewind.SharedData;
using UnityEngine;

namespace Rewind.LogicBuilder {
	[Serializable]
	public class LeverAIsOpenCondition : ICondition {
		[SerializeField] LeverA leverA;

		public Func<GameEntity, bool> entityFilter() {
			return e => e.isLeverA && e.hasId && e.id.value == leverA.id.guid;
		}

		public float calculateValue(GameEntity entity) =>
			(entity.hasLeverAState, entity.leverAState.value) switch {
				(false, _) => 0,
				(_, LeverAState.Closed) => 0,
				(_, LeverAState.Opened) => 1,
				_ => throw ExhaustiveMatch.Failed((entity.hasLeverAState, entity.leverAState.value))
			};
	}
	
	[Serializable]
	public class PlatformAOnStartCondition : ICondition {
		[SerializeField] PlatformA platformA;

		public Func<GameEntity, bool> entityFilter() {
			return e => e.isPlatformA && e.hasId && e.id.value == platformA.id.guid;
		}

		public float calculateValue(GameEntity entity) =>
			(entity.hasPlatformAMoveTime, entity.platformAMoveTime.value) switch {
				(true, 0) => 1,
				_ => 0
			};
	}

	[Serializable]
	public class PlatformAOnEndCondition : ICondition {
		[SerializeField] PlatformA platformA;

		public Func<GameEntity, bool> entityFilter() {
			return e => e.isPlatformA && e.hasId && e.id.value == platformA.id.guid;
		}

		public float calculateValue(GameEntity entity) =>
			(
				entity.hasPlatformAMoveTime
				&& entity.hasPlatformAData 
				&& entity.platformAMoveTime.value >= entity.platformAData.value._time
      ).to0or1();
	}
		
	[Serializable]
	public class PlayerOnPointCondition : ICondition {
		[SerializeField] PathPoint point;

		public Func<GameEntity, bool> entityFilter() {
			return e => e.isPlayer && e.hasCurrentPoint;
		}

		public float calculateValue(GameEntity entity) =>
			entity.isSamePoint(point) && entity.moveComplete.value ? 1 : 0;
	}
	
	[Serializable]
	public class PlayerOnPointOrFurtherCondition : ICondition {
		public enum IndexComparer {
			Equal = 0,
			EqualOrLower = 1,
			EqualOrHigher = 2
		}
		
		[SerializeField] PathPoint point;
		[SerializeField] IndexComparer playerIndex;

		public Func<GameEntity, bool> entityFilter() {
			return e => e.isPlayer && e.hasCurrentPoint;
		}

		public float calculateValue(GameEntity entity) {
			var playerPoint = entity.currentPoint.value;
			return
				(playerPoint.pathId == point.pathId
				&& playerIndex switch {
					IndexComparer.Equal => playerPoint.index == point.index,
					IndexComparer.EqualOrLower => playerPoint.index <= point.index, 
					IndexComparer.EqualOrHigher => playerPoint.index >= point.index,
					_ => throw ExhaustiveMatch.Failed(playerIndex)
				}).to0or1();
		}
	}
			
	[Serializable]
	public class ButtonAPressedCondition : ICondition {
		[SerializeField] ButtonA button;

		public Func<GameEntity, bool> entityFilter() {
			return e => e.isButtonA && e.hasId && e.id.value == button.id.guid;
		}

		public float calculateValue(GameEntity entity) =>
			(entity.hasButtonAState, entity.buttonAState.value) switch {
				(false, _) => 0,
				(_, ButtonAState.Closed) => 0,
				(_, ButtonAState.Opened) => 1,
				_ => throw ExhaustiveMatch.Failed((entity.hasButtonAState, entity.buttonAState.value))
			};
	}
	
	[Serializable]
	public class DoorAIsOpenCondition : ICondition {
		[SerializeField] DoorA doorA;

		public Func<GameEntity, bool> entityFilter() {
			return e => e.isDoorA && e.hasId && e.id.value == doorA.id.guid;
		}

		public float calculateValue(GameEntity entity) =>
			(entity.hasDoorAState, entity.doorAState.value) switch {
				(false, _) => 0,
				(_, DoorAState.Closed) => 0,
				(_, DoorAState.Opened) => 1,
				_ => throw ExhaustiveMatch.Failed((entity.hasLeverAState, entity.leverAState.value))
			};
	}

}
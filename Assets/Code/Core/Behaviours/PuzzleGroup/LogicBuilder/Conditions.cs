using System;
using System.Linq;
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
			return e => e.isLeverA && e.maybeId_value.Contains(leverA.id.guid);
		}

		public float calculateValue(GameEntity entity) =>
			entity.maybeLeverAState_value.Contains(LeverAState.Opened).to0or1();
	}
	
	[Serializable]
	public class PlatformAOnStartCondition : ICondition {
		[SerializeField] PlatformA platformA;

		public Func<GameEntity, bool> entityFilter() {
			return e => e.isPlatformA && e.maybeId_value.Contains(platformA.id.guid);
		}

		public float calculateValue(GameEntity entity) => entity.maybePlatformAMoveTime_value.Contains(0).to0or1();
	}

	[Serializable]
	public class PlatformAOnEndCondition : ICondition {
		[SerializeField] PlatformA platformA;

		public Func<GameEntity, bool> entityFilter() {
			return e => e.isPlatformA && e.maybeId_value.Contains(platformA.id.guid);
		}

		public float calculateValue(GameEntity entity) => entity.maybePlatformAMoveTime_value
			.zip(entity.maybePlatformAData_value.Map(_ => _._time), (moveTime, dataTime) => moveTime >= dataTime)
			.IfNone(false)
			.to0or1();
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
			return e => e.isButtonA && e.maybeId_value.Contains(button.id.guid);
		}

		public float calculateValue(GameEntity entity) =>
			entity.maybeButtonAState_value.Contains(ButtonAState.Opened).to0or1();
	}
	
	[Serializable]
	public class DoorAIsOpenCondition : ICondition {
		[SerializeField] DoorA doorA;

		public Func<GameEntity, bool> entityFilter() {
			return e => e.isDoorA && e.maybeId_value.Contains(doorA.id.guid);
		}

		public float calculateValue(GameEntity entity) =>
			entity.maybeDoorAState_value.Contains(DoorAState.Opened).to0or1();
	}

}
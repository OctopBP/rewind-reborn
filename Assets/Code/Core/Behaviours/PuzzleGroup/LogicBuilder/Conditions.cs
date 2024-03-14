using System;
using System.Linq;
using ExhaustiveMatching;
using Rewind.Behaviours;
using Rewind.Extensions;
using Rewind.Services;
using Rewind.SharedData;
using UnityEngine;

namespace Rewind.LogicBuilder
{
	[Serializable]
	public class LeverAIsOpenCondition : ICondition
    {
		[SerializeField] private LeverA leverA;

		public Func<GameEntity, bool> EntityFilter()
        {
			return e => e.isLeverA && e.maybeId_value.Contains(leverA.id.Guid);
		}

		public float CalculateValue(GameEntity entity) =>
			entity.maybeLeverAState_value.Contains(LeverAState.Opened).To0Or1();
	}
	
	[Serializable]
	public class PlatformAOnStartCondition : ICondition
    {
		[SerializeField] private PlatformA platformA;

		public Func<GameEntity, bool> EntityFilter()
        {
			return e => e.isPlatformA && e.maybeId_value.Contains(platformA.id.Guid);
		}

		public float CalculateValue(GameEntity entity) => entity.maybePlatformAMoveTime_value.Contains(0).To0Or1();
	}

	[Serializable]
	public class PlatformAOnEndCondition : ICondition
    {
		[SerializeField] private PlatformA platformA;

		public Func<GameEntity, bool> EntityFilter()
        {
			return e => e.isPlatformA && e.maybeId_value.Contains(platformA.id.Guid);
		}

		public float CalculateValue(GameEntity entity) => entity.maybePlatformAMoveTime_value
			.Zip(entity.maybePlatformAData_value.Map(_ => _._time), (moveTime, dataTime) => moveTime >= dataTime)
			.IfNone(false)
			.To0Or1();
	}
		
	[Serializable]
	public class PlayerOnPointCondition : ICondition
    {
		[SerializeField] private PathPoint point;

		public Func<GameEntity, bool> EntityFilter()
        {
			return e => e.isPlayer && e.hasCurrentPoint;
		}

		public float CalculateValue(GameEntity entity) =>
			entity.IsSamePoint(point) && entity.moveComplete.value ? 1 : 0;
	}
	
	[Serializable]
	public class PlayerOnPointOrFurtherCondition : ICondition
    {
		public enum IndexComparer
        {
			Equal = 0,
			EqualOrLower = 1,
			EqualOrHigher = 2
		}
		
		[SerializeField] private PathPoint point;
		[SerializeField] private IndexComparer playerIndex;

		public Func<GameEntity, bool> EntityFilter()
        {
			return e => e.isPlayer && e.hasCurrentPoint;
		}

		public float CalculateValue(GameEntity entity)
        {
			var playerPoint = entity.currentPoint.value;
			return
				(playerPoint.pathId == point.pathId
				&& playerIndex switch
                    {
					IndexComparer.Equal => playerPoint.index == point.index,
					IndexComparer.EqualOrLower => playerPoint.index <= point.index, 
					IndexComparer.EqualOrHigher => playerPoint.index >= point.index,
					_ => throw ExhaustiveMatch.Failed(playerIndex)
				}).To0Or1();
		}
	}
			
	[Serializable]
	public class ButtonAPressedCondition : ICondition
    {
		[SerializeField] private ButtonA button;

		public Func<GameEntity, bool> EntityFilter()
        {
			return e => e.isButtonA && e.maybeId_value.Contains(button.id.Guid);
		}

		public float CalculateValue(GameEntity entity) =>
			entity.maybeButtonAState_value.Contains(ButtonAState.Opened).To0Or1();
	}
	
	[Serializable]
	public class DoorAIsOpenCondition : ICondition
    {
		[SerializeField] private DoorA doorA;

		public Func<GameEntity, bool> EntityFilter()
        {
			return e => e.isDoorA && e.maybeId_value.Contains(doorA.id.Guid);
		}

		public float CalculateValue(GameEntity entity) =>
			entity.maybeDoorAState_value.Contains(DoorAState.Opened).To0Or1();
	}

}
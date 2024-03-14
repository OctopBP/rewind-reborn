using System;
using System.Linq;
using Rewind.Extensions;
using UnityEngine;

namespace Rewind.LogicBuilder
{
	[Serializable]
	public partial class ConditionWitOperation
	{
		[SerializeReference, PublicAccessor] private IOperation operation;
		[SerializeReference, PublicAccessor] private ICondition conditions;
	}
	
	[Serializable]
	public class ConditionGroup
	{
		[SerializeField] private ConditionWitOperation[] conditionsWitOperations;

		public bool isEmpty => conditionsWitOperations.IsEmpty();
		
		public float CalculateValue(GameEntity[] gameEntities)
		{
			return conditionsWitOperations.Aggregate(0f, (acc, c) =>
			{
				var entity = FunctionalExtensions.First(gameEntities, c._conditions.EntityFilter())
					.GetOrThrow("Can't find entity by entityFilter");
				return c._operation.Combine(acc, c._conditions.CalculateValue(entity));
			});
		}
	}
}
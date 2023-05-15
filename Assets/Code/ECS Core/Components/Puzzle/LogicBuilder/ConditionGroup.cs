using System;
using System.Linq;
using Rewind.Extensions;
using UnityEngine;

namespace Rewind.LogicBuilder {
	[Serializable]
	public partial class ConditionWitOperation {
		[SerializeReference, PublicAccessor] IOperation operation;
		[SerializeReference, PublicAccessor] ICondition conditions;
	}
	
	[Serializable]
	public class ConditionGroup {
		[SerializeField] ConditionWitOperation[] conditionsWitOperations;
		
		public float calculateValue(GameEntity[] gameEntities) => conditionsWitOperations.Aggregate(0f, (acc, c) => {
			var entity = gameEntities.first(c._conditions.entityFilter()).getOrThrow("Can't find entity by entityFilter");
			return c._operation.combine(acc, c._conditions.calculateValue(entity));
		});
	}
}
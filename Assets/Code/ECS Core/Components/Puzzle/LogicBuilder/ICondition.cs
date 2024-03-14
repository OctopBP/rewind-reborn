using System;

namespace Rewind.LogicBuilder
{
	public interface ICondition
	{
		Func<GameEntity, bool> EntityFilter();
		float CalculateValue(GameEntity entity);
	}
}
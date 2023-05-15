using System;

namespace Rewind.LogicBuilder {
	public interface ICondition {
		Func<GameEntity, bool> entityFilter();
		float calculateValue(GameEntity entity);
	}
}
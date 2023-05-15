using System;

public interface IPuzzleValueReceiver {
	Func<GameEntity, bool> entityFilter();
	void receiveValue(GameEntity entity, float value);
}
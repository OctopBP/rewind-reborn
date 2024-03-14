using System;

public interface IPuzzleValueReceiver
{
	Func<GameEntity, bool> EntityFilter();
	void ReceiveValue(GameEntity entity, float value);
}
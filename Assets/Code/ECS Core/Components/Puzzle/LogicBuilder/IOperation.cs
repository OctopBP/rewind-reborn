namespace Rewind.LogicBuilder
{
	public interface IOperation
	{
		float Combine(float current, float value);
	}
}
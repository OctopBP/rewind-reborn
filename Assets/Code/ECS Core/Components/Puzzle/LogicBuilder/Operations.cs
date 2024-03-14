using System;

namespace Rewind.LogicBuilder
{
	[Serializable]
	public class Value : IOperation
	{
		public float Combine(float _, float value) => value;
	}
	
	[Serializable]
	public class Add : IOperation
	{
		public float Combine(float current, float value) => current + value;
	}
	
	[Serializable]
	public class Subtract : IOperation
	{
		public float Combine(float current, float value) => current - value;
	}
	
	[Serializable]
	public class Divide : IOperation
	{
		public float Combine(float current, float value) => current / value;
	}
	
	[Serializable]
	public class Multiply : IOperation
	{
		public float Combine(float current, float value) => current * value;
	}
	
	[Serializable]
	public class And : IOperation
	{
		public float Combine(float current, float value) => current * value;
	}

	[Serializable]
	public class Or : IOperation
	{
		public float Combine(float current, float value) => current + value;
	}
	
	[Serializable]
	public class Not : IOperation
	{
		public float Combine(float _, float value) => value == 0 ? 1 : 0;
	}
}
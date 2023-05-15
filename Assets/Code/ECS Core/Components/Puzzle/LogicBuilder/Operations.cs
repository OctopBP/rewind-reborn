using System;

namespace Rewind.LogicBuilder {
	[Serializable]
	public class Value : IOperation {
		public float combine(float _, float value) {
			return value;
		}
	}
	
	[Serializable]
	public class Add : IOperation {
		public float combine(float current, float value) {
			return current + value;
		}
	}
	
	[Serializable]
	public class Subtract : IOperation {
		public float combine(float current, float value) {
			return current - value;
		}
	}
	
	[Serializable]
	public class Divide : IOperation {
		public float combine(float current, float value) {
			return current / value;
		}
	}
	
	[Serializable]
	public class Multiply : IOperation {
		public float combine(float current, float value) {
			return current * value;
		}
	}
	
	[Serializable]
	public class And : IOperation {
		public float combine(float current, float value) {
			return current * value;
		}
	}

	[Serializable]
	public class Or : IOperation {
		public float combine(float current, float value) {
			return current + value;
		}
	}
	
	[Serializable]
	public class Not : IOperation {
		public float combine(float _, float value) {
			return value == 0 ? 1 : 0;
		}
	}
}
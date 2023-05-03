#if UNITY_EDITOR
using Rewind.ViewListeners;

namespace Rewind.Behaviours {
	public partial class PuzzleGroup {
		public EntityIdBehaviour[] inputs__EDITOR => inputs;
		public EntityIdBehaviour[] outputs__EDITOR => outputs;
	}
}
#endif

using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.ECSCore {
	public class ParentTransformBehaviour : SelfInitializedView {
		[SerializeField] Transform parent;
		
		protected override void onAwake() {
			base.onAwake();
			setupCharacter();
		}
		
		void setupCharacter() {
			entity.AddParentTransform(parent);
		}
	}
}
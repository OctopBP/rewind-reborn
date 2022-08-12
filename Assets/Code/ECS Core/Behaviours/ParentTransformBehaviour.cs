using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.ECSCore {
	public class ParentTransformBehaviour : ComponentBehaviour {
		[SerializeField] Transform parent;

		protected override void onAwake() {
			entity.AddParentTransform(parent);
		}
	}
}
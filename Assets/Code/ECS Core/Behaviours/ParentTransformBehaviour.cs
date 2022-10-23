using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.ECSCore {
	public class ParentTransformBehaviour : MonoBehaviour {
		[SerializeField] Transform parent;

		public void initialize() => new Model(parent);

		class Model : EntityModel<GameEntity> {
			public Model(Transform parent) => entity.with(e => e.AddParentTransform(parent));
		}
	}
}
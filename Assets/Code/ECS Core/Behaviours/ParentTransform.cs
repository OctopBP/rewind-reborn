using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.ECSCore {
	public class ParentTransform : MonoBehaviour {
		[SerializeField] Transform parent;

		public void initialize() => new Model(parent);

		class Model : EntityModel<GameEntity> {
			public Model(Transform parent) => entity.AddParentTransform(parent);
		}
	}
}
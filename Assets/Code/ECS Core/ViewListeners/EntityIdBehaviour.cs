using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.ViewListeners {
	public class EntityIdBehaviour : MonoBehaviour {
		[SerializeField] SerializableGuid guid;
		public SerializableGuid id => guid;

		public class Model : EntityModel<GameEntity> {
			protected Model(EntityIdBehaviour entityId) =>
				entity.with(e => e.AddId(entityId.guid));
		}
	}
}
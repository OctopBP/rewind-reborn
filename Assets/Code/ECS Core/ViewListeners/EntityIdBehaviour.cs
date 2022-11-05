using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.ViewListeners {
	public class EntityIdBehaviour : MonoBehaviour {
		[SerializeField] SerializableGuid guid;
		public SerializableGuid id => guid;

		public class Model : LinkedEntityModel<GameEntity> {
			protected Model(EntityIdBehaviour entityId) : base(entityId.gameObject) =>
				entity.with(e => e.AddId(entityId.guid));
		}
	}
}
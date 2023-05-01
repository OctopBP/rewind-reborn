using Code.Helpers.Tracker;
using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.ViewListeners {
	public class EntityIdBehaviour : MonoBehaviour {
		[SerializeField] SerializableGuid guid;
		public SerializableGuid id => guid;

		public class Model : LinkedEntityModel<GameEntity> {
			protected Model(EntityIdBehaviour entityId, ITracker tracker) : base(entityId.gameObject, tracker) =>
				entity.with(e => e.AddId(entityId.guid));
		}
	}
}
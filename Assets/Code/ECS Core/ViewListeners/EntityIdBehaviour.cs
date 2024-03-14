using Code.Helpers.Tracker;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.ViewListeners
{
	public class EntityIdBehaviour : MonoBehaviour
	{
		[SerializeField] private SerializableGuid guid;
		public SerializableGuid id => guid;

		public class LinkedModel : LinkedEntityModel<GameEntity>
		{
			protected LinkedModel(EntityIdBehaviour entityId, ITracker tracker) : base(entityId.gameObject, tracker) =>
				entity.AddId(entityId.guid);
		}
		
		public class Model : TrackedEntityModel<GameEntity>
		{
			protected Model(SerializableGuid guid, ITracker tracker) : base(tracker) =>
				entity.AddId(guid);
		}
	}
}
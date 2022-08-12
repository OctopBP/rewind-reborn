using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.ViewListeners {
	public class EntityIdBehaviour : ComponentBehaviour {
		[SerializeField] SerializableGuid guid;
		public SerializableGuid id => guid;

		protected override void onAwake() {
			entity.AddId(guid);
		}
	}
}
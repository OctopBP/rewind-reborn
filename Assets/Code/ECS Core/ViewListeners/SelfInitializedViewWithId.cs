using System;
using UnityEngine;

namespace Rewind.ViewListeners {
	public class SelfInitializedViewWithId : SelfInitializedView {
		[SerializeField] SerializableGuid guid;
		public SerializableGuid id => guid;

		protected override void onAwake() {
			base.onAwake();
			entity.AddId(guid);
		}
	}
}
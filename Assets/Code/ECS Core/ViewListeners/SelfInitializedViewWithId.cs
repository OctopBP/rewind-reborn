using System;
using Sirenix.OdinInspector;

namespace Rewind.ViewListeners {
	public class SelfInitializedViewWithId : SelfInitializedView {
		[ReadOnly, PropertyOrder(1), InfoBox("Invalid id", InfoMessageType.Error, nameof(noId))]
		public string stringId;

		[Button, PropertyOrder(1), ShowIf(nameof(noId))]
		void generateGuid() => stringId = Guid.NewGuid().ToString();

		bool noId => !Guid.TryParse(stringId, out _);

		public Guid id {
			get {
				if (Guid.TryParse(stringId, out var id)) {
					return id;
				}

				var newId = Guid.NewGuid();
				stringId = newId.ToString();
				return newId;
			}
		}

		protected override void onAwake() {
			base.onAwake();
			entity.AddId(id);
		}
	}
}
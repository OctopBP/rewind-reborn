using Rewind.Data;
using Rewind.Extensions;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public class GearTypeBBehaviour : SelfInitializedViewWithId {
		[SerializeField] SelfInitializedViewWithId targetId;
		[SerializeField] GearTypeBData data;

		protected override void onAwake() {
			base.onAwake();
			setupGear();
		}

		void setupGear() {
			entity.with(x => x.isGearTypeB = true);

			entity.AddIdRef(targetId.id);
			entity.AddGearTypeBData(data);

			entity.AddPosition(transform.position);
			entity.AddRotation(transform.localEulerAngles.z);
		}
	}
}
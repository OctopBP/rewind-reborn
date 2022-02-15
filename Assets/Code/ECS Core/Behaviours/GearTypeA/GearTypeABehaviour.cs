using Rewind.Data;
using Rewind.Extensions;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public class GearTypeABehaviour : SelfInitializedView {
		[SerializeField] GearTypeAData data;
		[SerializeField] int pointIndex;
		[SerializeField] int pathIndex;

		protected override void onAwake() {
			base.onAwake();
			setupGear();
		}

		void setupGear() {
			entity.with(x => x.isFocusable = true);
			entity.with(x => x.isGearTypeA = true);
			entity.AddGearTypeAData(data);
			
			entity.AddPathIndex(pathIndex);
			entity.AddPointIndex(pointIndex);
			
			entity.AddPosition(transform.position);
			entity.AddRotation(transform.localEulerAngles.z);
		}
	}
}
using Rewind.Data;
using Rewind.Extensions;
using Rewind.Infrastructure;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public class GearTypeBBehaviour : ComponentBehaviour {
		[SerializeField] EntityIdBehaviour targetIdBehaviour;
		[SerializeField] GearTypeBData data;

		protected override void initialize() {
			entity.with(x => x.isGearTypeB = true);

			entity.AddIdRef(targetIdBehaviour.id);
			entity.AddGearTypeBData(data);

			entity.AddPosition(transform.position);
			entity.AddRotation(transform.localEulerAngles.z);
		}
	}
}
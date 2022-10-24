using Rewind.Data;
using Rewind.Extensions;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public class GearTypeB : EntityIdBehaviour {
		[SerializeField] EntityIdBehaviour targetIdBehaviour;
		[SerializeField] GearTypeBData data;

		public void initialize() => new Model(this);

		new class Model : EntityIdBehaviour.Model {
			public Model(GearTypeB gearTypeB) : base(gearTypeB) => entity
				.with(e => e.isGearTypeB = true)
				.with(e => e.AddIdRef(gearTypeB.targetIdBehaviour.id))
				.with(e => e.AddGearTypeBData(gearTypeB.data))
				.with(e => e.AddPosition(gearTypeB.transform.position))
				.with(e => e.AddRotation(gearTypeB.transform.localEulerAngles.z));
		}
	}
}
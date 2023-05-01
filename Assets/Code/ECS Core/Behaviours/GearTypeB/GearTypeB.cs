using Code.Helpers.Tracker;
using Rewind.Data;
using Rewind.Extensions;
using Rewind.Infrastructure;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public class GearTypeB : EntityIdBehaviour, IInitWithTracker {
		[SerializeField] EntityIdBehaviour targetIdBehaviour;
		[SerializeField] GearTypeBData data;

		public void initialize(ITracker tracker) => new Model(this, tracker);

		new class Model : EntityIdBehaviour.Model {
			public Model(GearTypeB gearTypeB, ITracker tracker) : base(gearTypeB, tracker) => entity
				.with(e => e.isGearTypeB = true)
				.with(e => e.AddView(gearTypeB.gameObject))
				.with(e => e.AddIdRef(gearTypeB.targetIdBehaviour.id))
				.with(e => e.AddGearTypeBData(gearTypeB.data))
				.with(e => e.AddPosition(gearTypeB.transform.position))
				.with(e => e.AddRotation(gearTypeB.transform.localEulerAngles.z));
		}
	}
}
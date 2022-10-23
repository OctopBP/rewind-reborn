using Rewind.Data;
using Rewind.Extensions;
using Rewind.Infrastructure;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public class GearTypeB : MonoBehaviour {
		[SerializeField] EntityIdBehaviour targetIdBehaviour;
		[SerializeField] GearTypeBData data;

		Model model;
		public void initialize() => model = new Model(this);

		class Model : EntityModel<GameEntity> {
			public Model(GearTypeB gearTypeB) => entity
				.with(e => e.isGearTypeB = true)
				.with(e => e.AddIdRef(gearTypeB.targetIdBehaviour.id))
				.with(e => e.AddGearTypeBData(gearTypeB.data))
				.with(e => e.AddPosition(gearTypeB.transform.position))
				.with(e => e.AddRotation(gearTypeB.transform.localEulerAngles.z));
		}
	}
}
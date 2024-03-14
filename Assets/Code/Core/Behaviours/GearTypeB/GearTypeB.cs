using Code.Helpers.Tracker;
using Rewind.SharedData;
using Rewind.Infrastructure;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours
{
	public class GearTypeB : EntityIdBehaviour, IInitWithTracker
	{
		[SerializeField] private EntityIdBehaviour targetIdBehaviour;
		[SerializeField] private GearTypeBData data;

		public void Initialize(ITracker tracker) => new Model(this, tracker);

		private new class Model : EntityIdBehaviour.LinkedModel
		{
			public Model(GearTypeB gearTypeB, ITracker tracker) : base(gearTypeB, tracker) => entity
				.SetGearTypeB(true)
				.AddView(gearTypeB.gameObject)
				.AddIdRef(gearTypeB.targetIdBehaviour.id)
				.AddGearTypeBData(gearTypeB.data)
				.AddPosition(gearTypeB.transform.position)
				.AddRotation(gearTypeB.transform.localEulerAngles.z);
		}
	}
}
using Code.Helpers.Tracker;
using PathCreation;
using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Infrastructure;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class PlatformA : EntityIdBehaviour, IInitWithTracker {
		[SerializeField] PlatformAData data;
		[SerializeField] Transform platformHandler;
		[SerializeField] PathCreator pathCreator;

		Model model;
		public void initialize(ITracker tracker) => model = new Model(this, tracker);
		
		public new class Model : EntityIdBehaviour.Model {
			public Model(PlatformA platformA, ITracker tracker) : base(platformA, tracker) {
				entity
					.SetPlatformA(true)
					.AddPlatformAData(platformA.data)
					.AddPlatformAMoveTime(0)
					.AddPlatformAState(PlatformAState.NotActive)
					.AddVertexPath(new(platformA.pathCreator.path))
					.AddTargetTransform(platformA.platformHandler);

				var pointFollowModel = new PointFollowModel(platformA.platformHandler);
			}
		}
		
		public class PointFollowModel : EntityModel<GameEntity> {
			public PointFollowModel(Transform platformHandler) => entity.AddFollowTransform(platformHandler);
		}
	}
}
using PathCreation;
using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class PlatformA : EntityIdBehaviour {
		[SerializeField] PlatformAData data;
		[SerializeField] Transform platformHandler;
		[SerializeField] PathCreator pathCreator;

		Model model;
		public void initialize() => model = new Model(this);
		
		public new class Model : EntityIdBehaviour.Model {
			public Model(PlatformA platformA) : base(platformA) {
				entity
					.with(e => e.isPlatformA = true)
					.with(e => e.AddPlatformAData(platformA.data))
					.with(e => e.AddPlatformAMoveTime(0))
					.with(e => e.AddPlatformAState(PlatformAState.NotActive))
					.with(e => e.AddVertexPath(new(platformA.pathCreator.path)))
					.with(e => e.AddTargetTransform(platformA.platformHandler));

				var pointFollowModel = new PointFollowModel(platformA.platformHandler);
			}
		}
		
		public class PointFollowModel : EntityModel<GameEntity> {
			public PointFollowModel(Transform platformHandler) =>
				entity.with(e => e.AddFollowTransform(platformHandler));
		}
	}
}
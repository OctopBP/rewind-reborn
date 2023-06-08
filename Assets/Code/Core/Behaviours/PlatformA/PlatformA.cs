using Code.Helpers.Tracker;
using Rewind.Infrastructure;
using Rewind.SharedData;
using Rewind.ViewListeners;
using UnityEngine;
using UnityEngine.Splines;

namespace Rewind.Behaviours {
	public partial class PlatformA : EntityIdBehaviour, IInitWithTracker {
		[SerializeField] PlatformAData data;
		[SerializeField] Transform platformHandler;
		[SerializeField] SplineContainer spline;

		Model model;
		public void initialize(ITracker tracker) => model = new Model(this, tracker);
		
		public new class Model : LinkedModel {
			public Model(PlatformA platformA, ITracker tracker) : base(platformA, tracker) {
				entity
					.SetPlatformA(true)
					.AddPlatformAData(platformA.data)
					.AddPlatformAMoveTime(0)
					.AddPlatformAState(PlatformAState.NotActive)
					.AddSpline(platformA.spline.Spline)
					.AddTargetTransform(platformA.platformHandler);

				var pointFollowModel = new PointFollowModel(platformA.platformHandler);
			}
		}
		
		public class PointFollowModel : EntityModel<GameEntity> {
			public PointFollowModel(Transform platformHandler) => entity.AddFollowTransform(platformHandler);
		}
	}
}
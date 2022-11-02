using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class Pendulum : MonoBehaviour {
		[SerializeField] PendulumData data;
		[SerializeField] Transform pointPosition;
		[SerializeField] PathPoint pointIndex;

		Model model;
		public void initialize() => model = new Model(this);
		
		public class Model : EntityModel<GameEntity> {
			public Model(Pendulum pendulum) {
				entity
					.with(e => e.isPendulum = true)
					.with(e => e.AddPendulumData(pendulum.data))
					.with(e => e.AddPendulumSwayTime(0))
					.with(e => e.AddPendulumState(PendulumState.NotActive))
					.with(e => e.AddCurrentPoint(pendulum.pointIndex))
					.with(e => e.AddPosition(pendulum.transform.position))
					.with(e => e.AddRotation(pendulum.transform.localEulerAngles.z));

				var pointFollowModel = new PointFollowModel(pendulum);
			}
		}

		public class PointFollowModel : EntityModel<GameEntity> {
			public PointFollowModel(Pendulum pendulum) => entity
				.with(e => e.AddFollowTransform(pendulum.pointPosition))
				.with(e => e.AddCurrentPoint(pendulum.pointIndex));
		}
	}
}
using Rewind.SharedData;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.Behaviours
{
	public partial class Pendulum : MonoBehaviour
	{
		[SerializeField] private PendulumData data;
		[SerializeField] private Transform pointPosition;
		[SerializeField] private PathPoint pointIndex;

		private Model model;
		public void Initialize() => model = new Model(this);
		
		public class Model : EntityModel<GameEntity>
		{
			public Model(Pendulum pendulum) {
				entity
					.SetPendulum(true)
					.AddPendulumData(pendulum.data)
					.AddPendulumSwayTime(0)
					.AddPendulumState(PendulumState.NotActive)
					// .AddCurrentPoint(pendulum.pointIndex)
					.AddPosition(pendulum.transform.position)
					.AddRotation(pendulum.transform.localEulerAngles.z);

				// var pointFollowModel = new PointFollowModel(pendulum);
			}
		}

		// public class PointFollowModel : EntityModel<GameEntity> {
		// 	public PointFollowModel(Pendulum pendulum) => entity
		// 		.AddFollowTransform(pendulum.pointPosition)
		// 		.AddCurrentPoint(pendulum.pointIndex);
		// }
	}
}
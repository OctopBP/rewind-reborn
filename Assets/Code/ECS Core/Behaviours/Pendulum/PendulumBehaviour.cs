using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class PendulumBehaviour : ComponentBehaviour {
		[SerializeField] PendulumData data;
		[SerializeField] Transform pointPosition;
		[SerializeField] PathPointType pointIndex;

		protected override void onAwake() {
			entity.with(x => x.isPendulum = true);

			entity.AddPendulumData(data);
			entity.AddPendulumSwayTime(0);
			entity.AddPendulumState(PendulumState.NotActive);

			entity.AddPointIndex(pointIndex);
			entity.AddPosition(transform.position);
			entity.AddRotation(transform.localEulerAngles.z);

			createPointFollow(gameContext);
			createStatus(entity);
		}

		void createPointFollow(GameContext gameContext) {
			var pointFollow = gameContext.CreateEntity();
			pointFollow.AddFollowTransform(pointPosition);
			pointFollow.AddPointIndex(pointIndex);
		}
	}
}
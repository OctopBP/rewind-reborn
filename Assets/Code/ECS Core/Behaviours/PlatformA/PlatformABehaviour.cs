using PathCreation;
using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class PlatformABehaviour : EntityIdBehaviour {
		[SerializeField] PlatformAData data;
		[SerializeField] Transform platformHandler;
		[SerializeField] PathCreator pathCreator;

		protected override void initialize() {
			entity.with(x => x.isPlatformA = true);

			entity.AddPlatformAData(data);
			entity.AddPlatformAMoveTime(0);
			entity.AddPlatformAState(PlatformAState.NotActive);

			entity.AddVertexPath(new(pathCreator.path));
			entity.AddTargetTransform(platformHandler);

			createPointFollow(gameContext);
			createStatus(entity);
		}

		void createPointFollow(GameContext gameContext) {
			var pointFollow = gameContext.CreateEntity();
			pointFollow.AddFollowTransform(platformHandler);
		}
	}
}
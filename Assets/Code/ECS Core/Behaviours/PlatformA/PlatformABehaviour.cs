using System;
using Code.Base;
using PathCreation;
using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public class PlatformABehaviour : SelfInitializedViewWithId, IStatusValue {
		[SerializeField] PlatformAData data;
		[SerializeField] Transform platformHandler;
		[SerializeField] int pointIndex;
		[SerializeField] int pathIndex;
		[SerializeField] PathCreator pathCreator;

		public float statusValue => entity.platformAState.value switch {
			PlatformAState.Active => 1,
			PlatformAState.NotActive => 0,
			_ => throw new ArgumentOutOfRangeException()
		};

		protected override void onAwake() {
			base.onAwake();
			setupPlatformA();
			createPointFollow();
		}

		void setupPlatformA() {
			entity.with(x => x.isPlatformA = true);

			entity.AddPlatformAData(data);
			entity.AddPlatformAMoveTime(0);
			entity.AddPlatformAState(PlatformAState.NotActive);

			entity.AddVertexPath(new(pathCreator.path));
			entity.AddTargetTransform(platformHandler);

			entity.AddPathIndex(pathIndex);
			entity.AddPointIndex(pointIndex);
		}

		void createPointFollow() {
			var pointFollow = game.CreateEntity();
			pointFollow.AddFollowTransform(platformHandler);
			pointFollow.AddPointIndex(pointIndex);
			pointFollow.AddPathIndex(pathIndex);
		}
	}
}
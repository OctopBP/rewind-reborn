using System;
using System.Collections.Generic;
using System.Linq;
using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.ViewListeners;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.Behaviours {
	public class PendulumBehaviour : SelfInitializedViewWithId {
		[SerializeField] PendulumData data;
		[SerializeField] Transform pointPosition;
		[SerializeField] int pointIndex;
		[SerializeField] int pathIndex;
		[SerializeField] List<GearTypeABehaviour> gears;

		protected override void onAwake() {
			base.onAwake();
			setupPendulum();
			createPointFollow();
			setupPuzzleGroup();
		}

		void setupPendulum() {
			entity.with(x => x.isPendulum = true);

			entity.AddPendulumData(data);
			entity.AddPendulumSwayTime(0);
			entity.AddPendulumState(PendulumState.NotActive);

			entity.AddPathIndex(pathIndex);
			entity.AddPointIndex(pointIndex);

			entity.AddPosition(transform.position);
			entity.AddRotation(transform.localEulerAngles.z);
		}

		void createPointFollow() {
			var pointFollow = game.CreateEntity();
			pointFollow.AddFollowTransform(pointPosition);
			pointFollow.AddPointIndex(pointIndex);
			pointFollow.AddPathIndex(pathIndex);
		}
		
		void setupPuzzleGroup() {
			var puzzleGroup = game.CreateEntity();
			puzzleGroup.with(p => p.isPuzzleGroup = true);
			puzzleGroup.AddPuzzleInputs(gears.Select(g => g.id).ToList());
			puzzleGroup.AddPuzzleOutputs(new() { entity.id.value });
		}
	}
}
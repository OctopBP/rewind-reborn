using System.Collections.Generic;
using System.Linq;
using Rewind.Extensions;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public class PuzzleGroupBehaviour : SelfInitializedView {
		[SerializeField] List<SelfInitializedViewWithId> inputs;
		[SerializeField] List<SelfInitializedViewWithId> outputs;

		protected override void onAwake() {
			base.onAwake();
			setupPuzzleGroup();
		}

		void setupPuzzleGroup() {
			entity.with(p => p.isPuzzleGroup = true);
			entity.AddPuzzleInputs(inputs.Select(g => g.id).ToList());
			entity.AddPuzzleOutputs(outputs.Select(g => g.id).ToList());
		}
	}
}
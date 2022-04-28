using System.Linq;
using Rewind.Extensions;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public class PuzzleGroupBehaviour : SelfInitializedView {
		[SerializeField] SerializableGuid id;
		[SerializeField] SelfInitializedViewWithId[] inputs;
		[SerializeField] SelfInitializedViewWithId[] outputs;
		[SerializeField] bool anyInput;
		[SerializeField] bool repeatable;

		public SerializableGuid guid => id;
		public SelfInitializedViewWithId[] getInputs => inputs;
		public SelfInitializedViewWithId[] getOutputs => outputs;

		protected override void onAwake() {
			base.onAwake();
			setupPuzzleGroup();
		}

		void setupPuzzleGroup() {
			entity.AddId(id);
			entity.with(p => p.isPuzzleGroup = true);
			entity.AddPuzzleInputs(inputs.Select(g => g.id.guid).ToList());
			entity.AddPuzzleOutputs(outputs.Select(g => g.id.guid).ToList());

			entity.with(p => p.isPuzzleGroupAnyInput = anyInput);
			entity.with(p => p.isPuzzleGroupRepeatable = repeatable);
		}
	}
}
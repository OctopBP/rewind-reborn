using System.Linq;
using Rewind.Extensions;
using Rewind.Infrastructure;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public class PuzzleGroupBehaviour : ComponentBehaviour {
		[SerializeField] SerializableGuid id;
		[SerializeField] EntityIdBehaviour[] inputs;
		[SerializeField] EntityIdBehaviour[] outputs;
		[SerializeField] bool anyInput;
		[SerializeField] bool repeatable;

		public SerializableGuid guid => id;
		public EntityIdBehaviour[] getInputs => inputs;
		public EntityIdBehaviour[] getOutputs => outputs;

		protected override void initialize() => entity
			.with(e => e.AddId(id))
			.with(e => e.isPuzzleGroup = true)
			.with(e => e.AddPuzzleInputs(inputs.Select(g => g.id.guid).ToList()))
			.with(e => e.AddPuzzleOutputs(outputs.Select(g => g.id.guid).ToList()))
			.with(e => e.isPuzzleGroupAnyInput = anyInput)
			.with(e => e.isPuzzleGroupRepeatable = repeatable);
	}
}
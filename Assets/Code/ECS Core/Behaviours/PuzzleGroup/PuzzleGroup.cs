using System.Linq;
using Rewind.Extensions;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class PuzzleGroup : EntityIdBehaviour {
		[SerializeField] EntityIdBehaviour[] inputs;
		[SerializeField] EntityIdBehaviour[] outputs;
		[SerializeField] bool anyInput;
		[SerializeField] bool repeatable;

		public void initialize() => new Model(this);

		public class Model : EntityIdBehaviour.Model {
			public Model(PuzzleGroup puzzleGroup) : base(puzzleGroup) => entity
				.with(e => e.isPuzzleGroup = true)
				.with(e => e.AddPuzzleInputs(puzzleGroup.inputs.Select(g => g.id.guid).ToList()))
				.with(e => e.AddPuzzleOutputs(puzzleGroup.outputs.Select(g => g.id.guid).ToList()))
				.with(p => p.isPuzzleGroupAnyInput = puzzleGroup.anyInput)
				.with(p => p.isPuzzleGroupRepeatable = puzzleGroup.repeatable);
		}
	}
}
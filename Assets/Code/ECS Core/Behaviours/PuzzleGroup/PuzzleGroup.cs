using System.Linq;
using Code.Helpers.Tracker;
using Rewind.Extensions;
using Rewind.Infrastructure;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class PuzzleGroup : EntityIdBehaviour, IInitWithTracker {
		[SerializeField] EntityIdBehaviour[] inputs;
		[SerializeField] EntityIdBehaviour[] outputs;
		[SerializeField] bool anyInput;
		[SerializeField] bool repeatable;

		public void initialize(ITracker tracker) => new Model(this, tracker);

		public class Model : EntityIdBehaviour.Model {
			public Model(PuzzleGroup puzzleGroup, ITracker tracker) : base(puzzleGroup, tracker) => entity
				.with(e => e.isPuzzleGroup = true)
				.with(e => e.AddPuzzleInputs(puzzleGroup.inputs.Select(g => g.id.guid).ToList()))
				.with(e => e.AddPuzzleOutputs(puzzleGroup.outputs.Select(g => g.id.guid).ToList()))
				.with(p => p.isPuzzleGroupAnyInput = puzzleGroup.anyInput)
				.with(p => p.isPuzzleGroupRepeatable = puzzleGroup.repeatable);
		}
	}
}
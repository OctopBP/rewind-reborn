using System.Linq;
using Code.Helpers.Tracker;
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
				.SetIsPuzzleGroup()
				.AddPuzzleInputs(puzzleGroup.inputs.Select(g => g.id.guid).ToList())
				.AddPuzzleOutputs(puzzleGroup.outputs.Select(g => g.id.guid).ToList())
				.SetPuzzleGroupAnyInput(puzzleGroup.anyInput)
				.SetPuzzleGroupRepeatable(puzzleGroup.repeatable);
		}
	}
}
using Code.Helpers.Tracker;
using Rewind.Extensions;
using Rewind.Infrastructure;
using Rewind.LogicBuilder;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class PuzzleGroup : EntityIdBehaviour, IInitWithTracker {
		[SerializeField, PublicAccessor] ConditionGroup conditionGroup;
		[SerializeReference, PublicAccessor] IPuzzleValueReceiver[] puzzleValueReceivers;

		public void initialize(ITracker tracker) => new Model(this, tracker).forSideEffect();

		public new class Model : LinkedModel {
			public Model(PuzzleGroup puzzleGroup, ITracker tracker) : base(puzzleGroup, tracker) => entity
				.SetPuzzleGroup(true)
				.AddConditionGroup(puzzleGroup.conditionGroup)
				.AddPuzzleValueReceiver(puzzleGroup.puzzleValueReceivers);
		}
	}
}
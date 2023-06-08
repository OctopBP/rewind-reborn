using Code.Helpers.Tracker;
using Rewind.Infrastructure;
using Rewind.LogicBuilder;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public class PuzzleGroup : EntityIdBehaviour, IInitWithTracker {
		[SerializeField] ConditionGroup conditionGroup;
		[SerializeReference] IPuzzleValueReceiver[] puzzleValueReceivers;

		public void initialize(ITracker tracker) => new Model(this, tracker);

		public class Model : LinkedModel {
			public Model(PuzzleGroup puzzleGroup, ITracker tracker) : base(puzzleGroup, tracker) => entity
				.SetPuzzleGroup(true)
				.AddConditionGroup(puzzleGroup.conditionGroup)
				.AddPuzzleValueReceiver(puzzleGroup.puzzleValueReceivers);
		}
	}
}
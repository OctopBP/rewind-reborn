using Code.Helpers.Tracker;
using Rewind.Extensions;
using UnityEngine;

namespace Rewind.Behaviours {
	public class GearCPuzzleGroup : PuzzleGroup {
		[SerializeField] Vector2[] targetRanges;

		public Vector2[] ranges => targetRanges;

		public class Model : PuzzleGroup.Model {
			public Model(GearCPuzzleGroup gearCPuzzleGroup, ITracker tracker) : base(gearCPuzzleGroup, tracker) =>
				entity.with(e => e.AddPuzzleTargetRange(gearCPuzzleGroup.targetRanges));
		}
	}
}
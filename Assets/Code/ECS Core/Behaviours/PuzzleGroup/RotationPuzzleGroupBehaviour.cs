using UnityEngine;

namespace Rewind.Behaviours {
	public class GearCPuzzleGroupBehaviour : PuzzleGroupBehaviour {
		[SerializeField] Vector2[] targetRanges;

		public Vector2[] ranges => targetRanges;

		protected override void onAwake() {
			base.onAwake();
			setupPuzzleGroup();
		}

		void setupPuzzleGroup() => entity.AddPuzzleTargetRange(targetRanges);
	}
}
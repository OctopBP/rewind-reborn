using UnityEngine;

namespace Rewind.Behaviours {
	public class GearCPuzzleGroupBehaviour : PuzzleGroupBehaviour {
		[SerializeField] Vector2[] targetRanges;

		public Vector2[] ranges => targetRanges;

		protected override void initialize() {
			base.initialize();
			entity.AddPuzzleTargetRange(targetRanges);
		}
	}
}
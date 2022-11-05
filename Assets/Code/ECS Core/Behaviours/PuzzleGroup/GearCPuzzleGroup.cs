using Rewind.Extensions;
using UnityEngine;

namespace Rewind.Behaviours {
	public class GearCPuzzleGroup : PuzzleGroup {
		[SerializeField] Vector2[] targetRanges;

		public Vector2[] ranges => targetRanges;

		public void initialize() => new Model(this);

		public class Model : PuzzleGroup.Model {
			public Model(GearCPuzzleGroup gearCPuzzleGroup) : base(gearCPuzzleGroup) =>
				entity.with(e => e.AddPuzzleTargetRange(gearCPuzzleGroup.targetRanges));
		}
	}
}
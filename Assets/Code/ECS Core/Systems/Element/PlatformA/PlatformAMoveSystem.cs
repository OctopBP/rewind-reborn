using Entitas;
using Rewind.SharedData;
using Rewind.Extensions;
using UnityEngine.Splines;

public class PlatformAMoveSystem : IExecuteSystem {
	readonly GameEntity clock;
	readonly IGroup<GameEntity> platforms;

	public PlatformAMoveSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
		platforms = contexts.game.GetGroup(GameMatcher
			.AllOf(
				GameMatcher.PlatformA, GameMatcher.PlatformAData, GameMatcher.PlatformAMoveTime, 
				GameMatcher.PlatformAState, GameMatcher.Spline, GameMatcher.TargetTransform
			)
		);
	}

	public void Execute() {
		foreach (var platform in platforms.GetEntities()) {
			var data = platform.platformAData.value;
			var moveTime = platform.platformAMoveTime.value;

			var deltaTime = clock.deltaTime.value * platform.platformAState.value.sign();
			var newMoveTime = (moveTime + deltaTime).clamp(0, data._time);
			var evaluatedTime = data._curve.Evaluate(newMoveTime / data._time);

			platform.ReplacePlatformAMoveTime(newMoveTime);
			platform.targetTransform.value.position = platform.spline.value.EvaluatePosition(evaluatedTime);
		}
	}
}
using System.Linq;
using Entitas;
using Rewind.Extensions;
using UnityEditor;
using UnityEngine;

public class DrawPathEditorSystem : IExecuteSystem {
	readonly IGroup<GameEntity> points;

	public DrawPathEditorSystem(Contexts contexts) => points = contexts.game.GetGroup(GameMatcher
		.AllOf(GameMatcher.Point, GameMatcher.CurrentPoint, GameMatcher.Depth, GameMatcher.Position)
	);

	public void Execute() {
		var groups = points.GetEntities().GroupBy(_ => _.currentPoint.value.pathId);
		foreach (var path in groups) {
			foreach (var point in path) {
				// TODO:
				Handles.color = Color.green;
				Handles.DrawSolidDisc(point.position.value, Vector3.forward, 0.5f);
			}

			var color = ColorExtensions.randomColorForGuid(path.Key);
			
			var arr = path.ToArray();
			for (var i = 0; i < arr.Length - 1; i++) {
				var from = arr[i].position.value;
				var to = arr[i + 1].position.value;
				Handles.DrawBezier(from, to, from, to, color, null, .2f);
			}
		}
	}
}
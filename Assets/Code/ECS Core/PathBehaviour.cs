using System.Collections.Generic;
using Rewind.Extensions;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.ECSCore {
	public class PathBehaviour : SelfInitializedView {
		[SerializeField] List<Vector2> points;

		protected override void onAwake() {
			base.onAwake();

			for (var i = 0; i < points.Count; i++) {
				createPoint(i);
			}
		}

		void createPoint(int index) {
			var point = game.CreateEntity();
			
			point.with(x => x.isPoint = true);
			// point.AddPathIndex(_pathIndex);
			// point.AddPointIndex(index);
			// point.AddPosition(_pathData.Point(index) + (Vector2) transform.position);
		}
	}
}
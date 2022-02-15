using System.Collections.Generic;
using Rewind.Extensions;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.ECSCore {
	public class PathBehaviour : SelfInitializedView {
		[SerializeField] List<Vector2> points;

		public int length => points.Count;
		public Vector2 this[int i] => points[i];

		public void setPosition(int i, Vector2 position) => points[i] = position;

		protected override void onAwake() {
			base.onAwake();

			for (var i = 0; i < points.Count; i++) {
				createPoint(i);
			}
		}

		void createPoint(int index) {
			var point = game.CreateEntity();
			
			point.with(x => x.isPoint = true);
			point.AddPathIndex(0);
			point.AddPointIndex(index);
			point.AddPosition(points[index] + (Vector2) transform.position);
		}
	}
}
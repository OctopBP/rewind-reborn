using System.Collections.Generic;
using Rewind.Extensions;
using Rewind.Infrastructure;
using Rewind.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.ECSCore {
	public partial class PathBehaviour : ComponentBehaviour, IEventListener {
		[SerializeField] SerializableGuid pathId;
		[TableList(ShowIndexLabels = true), SerializeField] List<PointData> points;
		[SerializeField] UnityOption<Transform> maybeParent;

		public Vector2 getWorldPosition(int i) => points[i].localPosition + transform.position.toVector2();

		readonly List<GameEntity> pointEntities = new();

		protected override void onAwake() {
			for (var i = 0; i < points.Count; i++) {
				createPointEntity(i);
			}
		}

		void createPointEntity(int i) {
			var pointEntity = gameContext.CreateEntity();
			var pointData = points[i];

			pointEntity
				.with(p => p.isPoint = true)
				.with(p => p.AddPointIndex(new(pathId, i)))
				.with(p => p.AddPointOpenStatus(pointData.status))
				.with(p => p.AddDepth(pointData.depth))
				.with(p => p.AddPosition(getWorldPosition(i)));

			maybeParent.value.IfSome(parent => {
				// TODO: create hierarchy in ECS
				var parentPosition = parent.transform.position.toVector2();
				var localPos = getWorldPosition(i) - parentPosition;
				pointEntity
					.with(p => p.AddParentTransform(parent))
					.with(p => p.AddLocalPosition(localPos));
			});

			pointEntities.Add(pointEntity);
		}

		public void registerListeners() {
			foreach (var pointEntity in pointEntities) {
				pointEntity
					.with(p => p.AddPositionListener(this))
					.with(p => p.AddPointOpenStatusListener(this))
					.with(p => p.AddDepthListener(this));
			}
		}

		public void unregisterListeners() {
			foreach (var pointEntity in pointEntities) {
				pointEntity
					.with(p => p.RemovePositionListener(this))
					.with(p => p.RemovePointOpenStatusListener(this))
					.with(p => p.RemoveDepthListener(this));
			}
		}
	}
}

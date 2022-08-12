using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;
using UnityEngine;

namespace Rewind.ECSCore {
	public partial class PathBehaviour : IEventListener, IPositionListener, IPointOpenStatusListener, IDepthListener {
		public void registerListeners() { }

		void registerPointListeners() {
			foreach (var pointEntity in pointEntities) {
				pointEntity.RemovePositionListener(this);
				pointEntity.RemovePointOpenStatusListener(this);
				pointEntity.RemoveDepthListener(this);
			}
		}

		public void unregisterListeners() {
			foreach (var pointEntity in pointEntities) {
				pointEntity.RemovePositionListener(this);
				pointEntity.RemovePointOpenStatusListener(this);
				pointEntity.RemoveDepthListener(this);
			}
		}

		public void OnPosition(GameEntity pointEntity, Vector2 value) =>
			points[pointEntities.IndexOf(pointEntity)].position = value - transform.position.toVector2();
		public void OnPointOpenStatus(GameEntity pointEntity, PointOpenStatus value) =>
			points[pointEntities.IndexOf(pointEntity)].status = value;
		public void OnDepth(GameEntity pointEntity, int value) =>
			points[pointEntities.IndexOf(pointEntity)].depth = value;
	}
}

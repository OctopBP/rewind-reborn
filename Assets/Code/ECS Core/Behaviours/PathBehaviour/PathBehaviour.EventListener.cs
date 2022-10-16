using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using UnityEngine;

namespace Rewind.ECSCore {
	public partial class PathBehaviour : IPositionListener, IPointOpenStatusListener, IDepthListener {
		public void OnPosition(GameEntity pointEntity, Vector2 value) =>
			points[pointEntities.IndexOf(pointEntity)].localPosition = value - transform.position.toVector2();
		public void OnPointOpenStatus(GameEntity pointEntity, PointOpenStatus value) =>
			points[pointEntities.IndexOf(pointEntity)].status = value;
		public void OnDepth(GameEntity pointEntity, int value) =>
			points[pointEntities.IndexOf(pointEntity)].depth = value;
	}
}

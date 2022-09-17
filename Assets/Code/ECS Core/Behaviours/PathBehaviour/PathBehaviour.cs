using System.Collections.Generic;
using Rewind.Extensions;
using Rewind.Infrastructure;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.ECSCore {
	public partial class PathBehaviour : ComponentBehaviour {
		[SerializeField] SerializableGuid pathId;
		[TableList(ShowIndexLabels = true), SerializeField] List<PointData> points;

		[SerializeField] Transform parent;
		[SerializeField] bool useParent;

		public SerializableGuid id => pathId;
		public int length => points.Count;
		public PointData this[int i] => points[i];

		public Vector3 getPosition(int i) => (Vector3) points[i].position + transform.position;
		public void setPosition(int i, Vector2 position) => points[i].position = position;

		readonly List<GameEntity> pointEntities = new();

		protected override void initialize() {
			for (var i = 0; i < points.Count; i++) {
				var pointEntity = gameContext.CreateEntity();

				pointEntity.with(x => x.isPoint = true);
				pointEntity.AddPointIndex(new(pathId, i));
				pointEntity.AddPointOpenStatus(points[i].status);
				pointEntity.AddDepth(points[i].depth);

				var position = transform.position.toVector2() + points[i].position;
				pointEntity.AddPosition(position);

				if (useParent) {
					var parentPosition = parent.transform.position.toVector2();
					var localPos = position - parentPosition;
					pointEntity.AddParentTransform(parent);
					pointEntity.AddLocalPosition(localPos);
				}

				pointEntity.AddPositionListener(this);
				pointEntity.AddPointOpenStatusListener(this);
				pointEntity.AddDepthListener(this);
				
				pointEntities.Add(pointEntity);
			}

			registerPointListeners();
		}
	}
}

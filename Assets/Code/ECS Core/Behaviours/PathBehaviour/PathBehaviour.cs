using System;
using System.Collections.Generic;
using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[Serializable]
public class PointData {
	public Vector2 position;
	public PointOpenStatus status;
	public int depth;

	public PointData(Vector2 position, PointOpenStatus status, int depth) {
		this.position = position;
		this.status = status;
		this.depth = depth;
	}
}

namespace Rewind.ECSCore {
	public class PathBehaviour : MonoBehaviour, IEventListener, IPositionListener, IPointOpenStatusListener, IDepthListener {
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

		void Awake() {
			for (var i = 0; i < points.Count; i++) {
				var pointEntity = Contexts.sharedInstance.game.CreateEntity();

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
		}

		public void registerListeners(IEntity _) { }

		public void unregisterListeners(IEntity _) {
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

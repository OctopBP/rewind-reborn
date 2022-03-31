using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;
using Rewind.ViewListeners;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class PointData {
	public Vector2 position;
	public PointOpenStatus status = PointOpenStatus.Opened;
}

namespace Rewind.ECSCore {
	public class PathBehaviour : SelfInitializedView, IEventListener, IPositionListener, IPointOpenStatusListener {
		[SerializeField] SerializableGuid guid;
		[TableList(ShowIndexLabels = true), SerializeField] List<PointData> points;

		public SerializableGuid id => guid;
		readonly List<(int index, GameEntity entity)> pointEntities = new();

		public int length => points.Count;
		public PointData this[int i] => points[i];

		public void setPosition(int i, Vector2 position) => points[i].position = position;

		protected override void onAwake() {
			for (var i = 0; i < points.Count; i++) {
				createPoint(i);
			}

			base.onAwake();
		}

		void createPoint(int index) {
			var point = game.CreateEntity();

			point.with(x => x.isPoint = true);
			point.AddPointIndex(new (id, index));
			point.AddPointOpenStatus(points[index].status);
			point.AddPosition(points[index].position + (Vector2) transform.position);

			pointEntities.Add((index, point));
		}

		public void registerListeners(IEntity _) {
			foreach (var pointEntity in pointEntities) {
				pointEntity.entity.AddPositionListener(this);
				pointEntity.entity.AddPointOpenStatusListener(this);
			}
		}

		public void unregisterListeners(IEntity _) {
			foreach (var pointEntity in pointEntities) {
				pointEntity.entity.RemovePositionListener(this);
				pointEntity.entity.RemovePointOpenStatusListener(this);
			}
		}

		public void OnPosition(GameEntity pointEntity, Vector2 value) => getPointEntity(pointEntity).position = value;

		public void OnPointOpenStatus(GameEntity pointEntity, PointOpenStatus value) =>
			getPointEntity(pointEntity).status = value;

		PointData getPointEntity(GameEntity pointEntity) =>
			points[pointEntities.FirstOrDefault(p => p.entity.Equals(pointEntity)).index];
	}
}
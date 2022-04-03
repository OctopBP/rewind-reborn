using System;
using System.Collections.Generic;
using Entitas;
using LanguageExt;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class PointData {
	public Vector2 position;
	public PointOpenStatus status;

	public PointData(Vector2 position, PointOpenStatus status) {
		this.position = position;
		this.status = status;
	}
}

namespace Rewind.ECSCore {
	public class PathBehaviour : MonoBehaviour {
		[SerializeField] SerializableGuid pathId;
		[TableList(ShowIndexLabels = true), SerializeField] List<PointData> points;

		[SerializeField] Transform parent;
		[SerializeField] bool useParent;

		public SerializableGuid id => pathId;
		public int length => points.Count;
		public PointData this[int i] => Application.isPlaying ? inits[i].data : points[i];

		readonly List<Init> inits = new();

		public void setPosition(int i, Vector2 position) => points[i].position = position;

		void Awake() {
			for (var i = 0; i < points.Count; i++) {
				// if (i == 0) points[i].status |= PointOpenStatus.ClosedLeft;
				// if (i == points.Count - 1) points[i].status |= PointOpenStatus.ClosedRight;
				inits.Add(Init.create(
					pathId, i, transform.position, points[i],
					useParent ? parent : Option<Transform>.None
				));
			}
		}

		public class Init : IEventListener, IPositionListener, IPointOpenStatusListener {
			readonly GameEntity entity;
			public readonly PointData data;

			public static Init create(
				SerializableGuid pathId, int index, Vector2 pathPosition, PointData data, Option<Transform> parent
			) =>
				new(pathId, index, pathPosition, data, parent);

			Init(SerializableGuid pathId, int index, Vector2 pathPosition, PointData data, Option<Transform> parent) {
				this.data = data;

				entity = Contexts.sharedInstance.game.CreateEntity();

				entity.with(x => x.isPoint = true);
				entity.AddPointIndex(new(pathId, index));
				entity.AddPointOpenStatus(data.status);

				var position = pathPosition + data.position;
				parent.Match(p => {
					var parentPosition = p.transform.position.toVector2();
					var localPos = position - parentPosition;
					entity.AddParentTransform(p);
					entity.AddLocalPosition(localPos);
					entity.AddPosition(position);
				}, () => {
					entity.AddPosition(position);
				});
			}

			public void registerListeners(IEntity _) {
				entity.AddPositionListener(this);
				entity.AddPointOpenStatusListener(this);
			}

			public void unregisterListeners(IEntity _) {
				entity.RemovePositionListener(this);
				entity.RemovePointOpenStatusListener(this);
			}

			public void OnPosition(GameEntity pointEntity, Vector2 value) => data.position = value;
			public void OnPointOpenStatus(GameEntity pointEntity, PointOpenStatus value) => data.status = value;
		}
	}
}

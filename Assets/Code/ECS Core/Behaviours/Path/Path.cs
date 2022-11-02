using System.Collections.Generic;
using System.Linq;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.ECSCore {
	public partial class Path : MonoBehaviour {
		[SerializeField] SerializableGuid pathId;
		[TableList(ShowIndexLabels = true), SerializeField] List<PointData> points;
		[SerializeField] UnityOption<Transform> maybeParent;

		public Vector2 getWorldPosition(int i) => points[i].localPosition + transform.position.xy();

		PointModel[] pointModels;

		public void initialize() {
			pointModels = points
				.Select((point, i) => new PointModel(this, point, i))
				.ToArray();
		}

		public class PointModel : EntityModel<GameEntity>, IPositionListener, IPointOpenStatusListener, IDepthListener {
			public readonly PointData pointData;
			readonly Path path;
			
			public PointModel(Path path, PointData pointData, int i) {
				this.pointData = pointData;
				this.path = path;

				entity
					.with(e => e.isPoint = true)
					.with(e => e.AddCurrentPoint(new(path.pathId, i)))
					.with(e => e.AddPointOpenStatus(pointData.status))
					.with(e => e.AddDepth(pointData.depth))
					.with(e => e.AddPosition(path.getWorldPosition(i)))
					.with(e => path.maybeParent.value.IfSome(parent => e
						.with(p => p.AddParentTransform(parent))
						.with(p => p.AddLocalPosition(
							path.getWorldPosition(i) - parent.transform.position.xy())
						))
					)
					.with(p => p.AddPositionListener(this))
					.with(p => p.AddPointOpenStatusListener(this))
					.with(p => p.AddDepthListener(this));
			}

			public void OnPosition(GameEntity _, Vector2 value) =>
				pointData.localPosition = value - path.transform.position.xy();
			public void OnPointOpenStatus(GameEntity _, PointOpenStatus value) => pointData.status = value;
			public void OnDepth(GameEntity _, int value) => pointData.depth = value;
		}
	}
}

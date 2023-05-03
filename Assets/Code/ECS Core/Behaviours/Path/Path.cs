using System.Collections.Generic;
using System.Linq;
using Code.Helpers.Tracker;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.ECSCore {
	public partial class Path : MonoBehaviour, IInitWithTracker {
		[SerializeField] SerializableGuid pathId;
		[TableList(ShowIndexLabels = true), SerializeField, PublicAccessor] List<PointData> points;
		[SerializeField] UnityOption<Transform> maybeParent;

		public Vector2 getWorldPosition(int i) => points[i].localPosition + transform.position.xy();

		PointModel[] pointModels;

		public void initialize(ITracker tracker) {
			pointModels = points
				.Select((point, idx) => new PointModel(this, tracker, point, idx))
				.ToArray();
		}

		public class PointModel : TrackedEntityModel<GameEntity>, IPositionListener, IPointOpenStatusListener,
			IDepthListener
		{
			public readonly PointData pointData;
			readonly Path path;
			
			public PointModel(Path path, ITracker tracker, PointData pointData, int pointIndex) : base(tracker) {
				this.pointData = pointData;
				this.path = path;

				entity
					.SetPoint(true)
					.AddCurrentPoint(new(path.pathId, pointIndex))
					.AddPointOpenStatus(pointData.status)
					.AddDepth(pointData.depth)
					.AddPosition(path.getWorldPosition(pointIndex))
					.AddPositionListener(this)
					.AddPointOpenStatusListener(this)
					.AddDepthListener(this);

				path.maybeParent.value.IfSome(parent => entity
					.AddParentTransform(parent)
					.AddLocalPosition(path.getWorldPosition(pointIndex) - parent.transform.position.xy())
				);
			}

			public void OnPosition(GameEntity _, Vector2 value) =>
				pointData.localPosition = value - path.transform.position.xy();
			public void OnPointOpenStatus(GameEntity _, PointOpenStatus value) => pointData.status = value;
			public void OnDepth(GameEntity _, int value) => pointData.depth = value;
		}
	}
}

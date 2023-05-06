using System.Collections.Generic;
using System.Linq;
using Code.Helpers.Tracker;
using LanguageExt;
using Rewind.Extensions;
using Rewind.Infrastructure;
using Rewind.SharedData;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.ECSCore {
	public partial class WalkPath : MonoBehaviour, IInitWithTracker {
		[SerializeField, PublicAccessor] SerializableGuid pathId;
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
			readonly WalkPath walkPath;
			
			public PointModel(WalkPath walkPath, ITracker tracker, PointData pointData, int pointIndex) : base(tracker) {
				this.pointData = pointData;
				this.walkPath = walkPath;

				entity
					.SetPoint(true)
					.AddCurrentPoint(new(walkPath.pathId, pointIndex))
					.AddPointOpenStatus(pointData.status)
					.AddDepth(pointData.depth)
					.AddPosition(walkPath.getWorldPosition(pointIndex))
					.AddPositionListener(this)
					.AddPointOpenStatusListener(this)
					.AddDepthListener(this);

				walkPath.maybeParent.value.IfSome(parent => entity
					.AddParentTransform(parent)
					.AddLocalPosition(walkPath.getWorldPosition(pointIndex) - parent.transform.position.xy())
				);
			}

			public void OnPosition(GameEntity _, Vector2 value) =>
				pointData.localPosition = value - walkPath.transform.position.xy();
			public void OnPointOpenStatus(GameEntity _, PointOpenStatus value) => pointData.status = value;
			public void OnDepth(GameEntity _, int value) => pointData.depth = value;
		}
	}

	public static class WalkPathExt {
		public static Option<Vector2> findPositionInPaths(this IEnumerable<WalkPath> paths, PathPoint point) => paths
			.Find(p => p._pathId == point.pathId)
			.Map(p => p.getWorldPosition(point.index));
		
		public static Option<WalkPath> findById(this IEnumerable<WalkPath> paths, SerializableGuid id) => paths
			.Find(p => p._pathId == id);
	}
}

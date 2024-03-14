using System.Collections.Generic;
using System.Linq;
using Code.Helpers.Tracker;
using LanguageExt;
using Rewind.Extensions;
using Rewind.Infrastructure;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.ECSCore
{
	public partial class WalkPath : MonoBehaviour, IInitWithTracker
	{
		[SerializeField, PublicAccessor] private SerializableGuid pathId;
		[TableList(ShowIndexLabels = true), SerializeField, PublicAccessor] private List<PointData> points;
		[SerializeField] private UnityOption<Transform> maybeParent;

		public Option<Vector2> GetMaybeWorldPosition(int i) =>
			points.At(i).Map(point =>point.localPosition + transform.position.XY());

		private PointModel[] pointModels;

		public void Initialize(ITracker tracker)
		{
			pointModels = points
				.Select((point, idx) => new PointModel(this, tracker, point, idx))
				.ToArray();
		}

		public class PointModel : TrackedEntityModel<GameEntity>, IPositionListener, IDepthListener
		{
			public readonly PointData PointData;
			private readonly WalkPath walkPath;
			
			public PointModel(WalkPath walkPath, ITracker tracker, PointData pointData, int pointIndex) : base(tracker)
			{
				PointData = pointData;
				this.walkPath = walkPath;

				var worldPosition = walkPath.GetWorldPositionOrThrow(pointIndex);

				entity
					.SetPoint(true)
					.AddCurrentPoint(new(walkPath.pathId, pointIndex))
					.AddDepth(pointData.depth)
					.AddPosition(worldPosition)
					.AddPositionListener(this)
					.AddLeftPathDirectionBlocks(pointData.leftPathStatus.ToBlockList(this.walkPath._pathId.Guid))
					.AddDepthListener(this);

				walkPath.maybeParent.Value.IfSome(parent => entity
					.AddParentTransform(parent)
					.AddLocalPosition(worldPosition - parent.transform.position.XY())
				);
			}

			public void OnPosition(GameEntity _, Vector2 value) =>
				PointData.localPosition = value - walkPath.transform.position.XY();
			
			public void OnDepth(GameEntity _, int value) => PointData.depth = value;
		}
	}

	public static class WalkPathExt
	{
		public static Option<Vector2> FindPositionInPaths(this IEnumerable<WalkPath> paths, PathPoint point) => paths
			.Find(p => p._pathId == point.pathId)
			.FlatMap(p => p.GetMaybeWorldPosition(point.index));
		
		public static Option<WalkPath> FindById(this IEnumerable<WalkPath> paths, SerializableGuid id) => paths
			.Find(p => p._pathId == id);
	}
}

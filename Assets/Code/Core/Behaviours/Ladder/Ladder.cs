using System;
using System.Collections.Generic;
using Code.Helpers.Tracker;
using LanguageExt;
using Rewind.Extensions;
using Rewind.Infrastructure;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.ECSCore
{
	public partial class Ladder : MonoBehaviour, IInitWithTracker
	{
		[Serializable, GenConstructor]
		public partial class PointWithPosition
		{
			[SerializeField, PublicAccessor] private Vector2 position;
			[SerializeField, PublicAccessor] private MaybePathPoint maybePathPoint;

			public void setPosition(Vector3 newPosition) => position = newPosition;
		}

		[SerializeField, PublicAccessor] private SerializableGuid pathId;
		[TableList(ShowIndexLabels = true), SerializeField, PublicAccessor]
		private List<PointWithPosition> points;

		public void Initialize(ITracker tracker)
        {
			for (var i = 0; i < points.Count; i++)
            {
				var p = points[i];
				new PointModel(
					tracker, pathPoint: new PathPoint(pathId, i), position: transform.position.XY() + p._position,
					maybeConnectorPoint: p._maybePathPoint.OptValue
				).ForSideEffect();
			}
		}

		public class PointModel : TrackedEntityModel<GameEntity>
		{
			public PointModel(
				ITracker tracker, PathPoint pathPoint, Vector2 position, Option<PathPoint> maybeConnectorPoint
			) : base(tracker)
			{
				entity
					.SetLadderStair(true)
					.SetPoint(true)
					.AddCurrentPoint(pathPoint)
					.AddDepth(0) // TODO:
					.AddPosition(position);
				
				maybeConnectorPoint.IfSome(connectorPoint => entity.AddLadderConnector(connectorPoint));
			}
		}
	}
}

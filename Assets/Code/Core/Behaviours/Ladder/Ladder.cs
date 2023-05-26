using System;
using System.Collections.Generic;
using Code.Helpers.Tracker;
using LanguageExt;
using Rewind.Extensions;
using Rewind.Infrastructure;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.ECSCore {
	public partial class Ladder : MonoBehaviour, IInitWithTracker {
		[Serializable, GenConstructor]
		public partial class PointWithPosition {
			[SerializeField, PublicAccessor] Vector2 position;
			[SerializeField, PublicAccessor] MaybePathPoint maybePathPoint;

			public void setPosition(Vector3 newPosition) => position = newPosition;
		}

		[SerializeField, PublicAccessor] SerializableGuid pathId;
		[TableList(ShowIndexLabels = true), SerializeField, PublicAccessor] List<PointWithPosition> points;

		public void initialize(ITracker tracker) {
			for (var i = 0; i < points.Count; i++) {
				var p = points[i];
				new PointModel(
					tracker, pathPoint: new PathPoint(pathId, i), position: transform.position.xy() + p._position,
					maybeConnectorPoint: p._maybePathPoint.optValue
				).forSideEffect();
			}
		}

		public class PointModel : TrackedEntityModel<GameEntity> {
			public PointModel(
				ITracker tracker, PathPoint pathPoint, Vector2 position, Option<PathPoint> maybeConnectorPoint
			) : base(tracker) {
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

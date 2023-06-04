using System;
using System.Linq;
using Code.Helpers.Tracker;
using Rewind.Behaviours;
using Rewind.ECSCore;
using Rewind.Extensions;
using Rewind.Infrastructure;
using Rewind.LogicBuilder;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.Core {
	public partial class Level : MonoBehaviour {
		[Title("Settings")]
		[SerializeField, PropertyOrder(-1)] Vector3 rootPosition;
		[SerializeField, PublicAccessor] PathPoint startPoint;
		[SerializeField, PublicAccessor] PathPoint finishPoint;

		[Space(10)]
		[Title("Elements")]
		[SerializeField, PublicAccessor] WalkPath[] paths;
		[SerializeField, PublicAccessor] Ladder[] ladders;
		[SerializeField, PublicAccessor] PathConnector[] connectors;
		[SerializeField, PublicAccessor] ButtonA[] buttonsA;
		[SerializeField, PublicAccessor] DoorA[] doorsA;
		[SerializeField, PublicAccessor] GearTypeA[] gearTypeA;
		[SerializeField, PublicAccessor] GearTypeB[] gearTypeB;
		[SerializeField, PublicAccessor] GearTypeC[] gearTypeC;
		[SerializeField, PublicAccessor] LeverA[] leversA;
		[SerializeField, PublicAccessor] PlatformA[] platformsA;
		[SerializeField, PublicAccessor] PuzzleGroup[] puzzleGroups;
		[SerializeField, PublicAccessor] ActionTrigger[] actionTriggers;

		[Title("Other")]
		[SerializeField] ConditionGroup[] progressConditions;
		
		public class Init {
			public readonly Vector2 startPosition;
			public readonly PointTrigger startTrigger;
			public readonly PointTrigger finisTrigger;
			public readonly IDisposableTracker tracker;

			public Init(Level backing, LevelAudio levelAudio) {
				backing.transform.position = backing.rootPosition;
				
				tracker = new DisposableTracker();

				startPosition = backing.paths.findPositionInPaths(backing.startPoint)
					.getOrThrow("Cant find start position");
				
				startTrigger = new PointTrigger(backing.startPoint, tracker);
				finisTrigger = new PointTrigger(backing.finishPoint, tracker);

				var levelProgress = new LevelProgress(tracker, backing.progressConditions);
				new LevelAudio.Model(tracker, levelAudio, levelProgress.progress).forSideEffect();
				
				var inits = backing.paths
					.Concat<IInitWithTracker>(backing.ladders)
					.Concat(backing.connectors)
					.Concat(backing.buttonsA)
					.Concat(backing.doorsA)
					.Concat(backing.gearTypeA)
					.Concat(backing.gearTypeB)
					.Concat(backing.gearTypeC)
					.Concat(backing.leversA)
					.Concat(backing.platformsA)
					.Concat(backing.puzzleGroups)
					.Concat(backing.actionTriggers);

				foreach (var initWithTracker in inits) {
					initWithTracker.initialize(tracker);
				}
			}

			public void dispose() => tracker.Dispose();
		}
	}
}
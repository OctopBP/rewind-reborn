using System.Linq;
using Code.Helpers.Tracker;
using Rewind.Behaviours;
using Rewind.ECSCore;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.Core {
	public partial class Level : MonoBehaviour {
		[Header("Settings")]
		[SerializeField] Vector3 rootPosition;
		[SerializeField, PublicAccessor] PathPoint startPoint;
		[SerializeField, PublicAccessor] PathPoint finishPoint;

		[Space(10)]
		[Header("Elements")]
		[SerializeField, PublicAccessor] WalkPath[] paths;
		[SerializeField, PublicAccessor] Connector[] connectors;
		[SerializeField, PublicAccessor] ButtonA[] buttonsA;
		[SerializeField, PublicAccessor] DoorA[] doorsA;
		[SerializeField, PublicAccessor] GearTypeA[] gearTypeA;
		[SerializeField, PublicAccessor] GearTypeB[] gearTypeB;
		[SerializeField, PublicAccessor] GearTypeC[] gearTypeC;
		[SerializeField, PublicAccessor] LeverA[] leversA;
		[SerializeField, PublicAccessor] PlatformA[] platformsA;
		[SerializeField, PublicAccessor] PuzzleGroup[] puzzleGroups;

		public class Init {
			public readonly PointTrigger startTrigger;
			public readonly PointTrigger finisTrigger;
			public readonly IDisposableTracker tracker;

			public Init(Level backing) {
				backing.transform.position = backing.rootPosition;
				
				tracker = new DisposableTracker();
				
				startTrigger = new PointTrigger(backing.startPoint, tracker);
				finisTrigger = new PointTrigger(backing.finishPoint, tracker);

				var inits = backing.paths
					.Concat<IInitWithTracker>(backing.connectors)
					.Concat(backing.buttonsA)
					.Concat(backing.doorsA)
					.Concat(backing.gearTypeA)
					.Concat(backing.gearTypeB)
					.Concat(backing.gearTypeC)
					.Concat(backing.leversA)
					.Concat(backing.platformsA)
					.Concat(backing.puzzleGroups);

				foreach (var initWithTracker in inits) {
					initWithTracker.initialize(tracker);
				}
			}

			public void dispose() => tracker.Dispose();
		}
	}
}
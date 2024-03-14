using System.Linq;
using Code.Helpers.Tracker;
using Rewind.Behaviours;
using Rewind.ECSCore;
using Rewind.Extensions;
using Rewind.Infrastructure;
using Rewind.LogicBuilder;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.Core
{
	public partial class Level : MonoBehaviour
    {
		[Title("Settings")]
		[SerializeField, PropertyOrder(-1)] private Vector3 rootPosition;
		[SerializeField, PublicAccessor] private PathPoint startPoint;
		[SerializeField, PublicAccessor] private PathPoint finishPoint;

		[Space(10)]
		[Title("Elements")]
		[SerializeField, PublicAccessor] private WalkPath[] paths;
		[SerializeField, PublicAccessor] private Ladder[] ladders;
		[SerializeField, PublicAccessor] private PathConnector[] connectors;
		[SerializeField, PublicAccessor] private ButtonA[] buttonsA;
		[SerializeField, PublicAccessor] private DoorA[] doorsA;
		[SerializeField, PublicAccessor] private GearTypeA[] gearTypeA;
		[SerializeField, PublicAccessor] private GearTypeB[] gearTypeB;
		[SerializeField, PublicAccessor] private GearTypeC[] gearTypeC;
		[SerializeField, PublicAccessor] private LeverA[] leversA;
		[SerializeField, PublicAccessor] private PlatformA[] platformsA;
		[SerializeField, PublicAccessor] private PuzzleGroup[] puzzleGroups;
		[SerializeField, PublicAccessor] private ActionTrigger[] actionTriggers;

		[Title("Other")]
		[SerializeField, PropertyOrder(2)] private ConditionGroup[] progressConditions;
		
		public class Init
		{
			public readonly Vector2 StartPosition;
			public readonly PointTrigger StartTrigger;
			public readonly PointTrigger FinisTrigger;
			public readonly IDisposableTracker Tracker;

			public Init(Level backing, LevelAudio levelAudio)
            {
				backing.transform.position = backing.rootPosition;
				
				Tracker = new DisposableTracker();

				StartPosition = backing.paths.FindPositionInPaths(backing.startPoint)
					.GetOrThrow("Cant find start position");
				
				StartTrigger = new PointTrigger(backing.startPoint, Tracker);
				FinisTrigger = new PointTrigger(backing.finishPoint, Tracker);

				// TODO:
				backing.progressConditions.HeadOrNone().IfSome(firstProgressCondition =>
                {
					var levelProgress = new LevelProgress(Tracker, firstProgressCondition);
					new LevelAudio.Model(Tracker, levelAudio, levelProgress.Progress).ForSideEffect();
				});
				
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

				foreach (var initWithTracker in inits)
				{
					initWithTracker.Initialize(Tracker);
				}
			}

			public void Dispose() => Tracker.Dispose();
		}
	}
}
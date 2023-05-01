using System.Linq;
using Code.Helpers.Tracker;
using Rewind.Behaviours;
using Rewind.ECSCore;
using Rewind.Infrastructure;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Rewind.Core {
	public partial class Level : MonoBehaviour {
		[Header("Settings")]
		[SerializeField] Vector3 rootPosition;
		[SerializeField, PublicAccessor] PathPoint startIndex;

		[Space(10)]
		[Header("Elements")]
		[SerializeField, PublicAccessor] Path[] paths;
		[SerializeField, PublicAccessor] Connector[] connectors;
		[SerializeField, PublicAccessor] ButtonA[] buttonsA;
		[SerializeField, PublicAccessor] DoorA[] doorsA;
		[SerializeField, PublicAccessor] GearTypeA[] gearTypeA;
		[SerializeField, PublicAccessor] GearTypeB[] gearTypeB;
		[SerializeField, PublicAccessor] GearTypeC[] gearTypeC;
		[SerializeField, PublicAccessor] LeverA[] leversA;
		[SerializeField, PublicAccessor] PlatformA[] platformsA;
		[SerializeField, PublicAccessor] PuzzleGroup[] puzzleGroups;

		[Space(10)]
		[SerializeField, Required, PublicAccessor] Finish finishTrigger;

		public class Model {
			public readonly Level backing;
			public readonly Finish.Model finishModel;
			readonly IDisposableTracker tracker;
			
			public readonly ReactiveCommand started = new ReactiveCommand();

			public Model(Level backing) {
				this.backing = backing;
				
				backing.transform.position = backing.rootPosition;
				
				tracker = new DisposableTracker();
				finishModel = new Finish.Model(backing.finishTrigger, tracker);

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
				
				started.Execute();
			}

			public void dispose() {
				tracker.Dispose();
			}
		}
	}
}
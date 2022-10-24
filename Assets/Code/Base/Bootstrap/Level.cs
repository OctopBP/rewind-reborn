using Rewind.Behaviours;
using Rewind.Helpers.Interfaces.UnityCallbacks;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UniRx;
using UnityEngine;

namespace Rewind.ECSCore {
	public class Level : MonoBehaviour {
		[Space(10)]
		[SerializeField, Required] Player player;
		[SerializeField, Required] Clone clone;
		[SerializeField] PathPointType startIndex;

		[Space(10)]
		[SerializeField, Required] Clock clock;
		[SerializeField, Required] GameSettingsBehaviour gameSettings;

		[Space(10)]
		[SerializeField] Path[] paths;
		[SerializeField] Connector[] connectors;
		[SerializeField] ButtonA[] buttonsA;
		[SerializeField] DoorA[] doorsA;
		[SerializeField] LeverA[] leversA;
		[SerializeField] PlatformA[] platformsA;
		[SerializeField] PuzzleGroup[] puzzleGroups;

		[Space(10)]
		[SerializeField, Required] Finish finishTrigger;

		[Button]
		void addAllObjectsFromScene() {
			player = FindObjectOfType<Player>();
			clone = FindObjectOfType<Clone>();

			paths = FindObjectsOfType<Path>();
			connectors = FindObjectsOfType<Connector>();
			buttonsA = FindObjectsOfType<ButtonA>();
			doorsA = FindObjectsOfType<DoorA>();
			leversA = FindObjectsOfType<LeverA>();
			platformsA = FindObjectsOfType<PlatformA>();
			puzzleGroups = FindObjectsOfType<PuzzleGroup>();

			clock = FindObjectOfType<Clock>();
			finishTrigger = FindObjectOfType<Finish>();
		}

		public ReactiveCommand levelCompleted;

		public void initialize() {
			gameSettings.initialize();
			clock.initialize();

			paths.ForEach(path => path.initialize());
			connectors.ForEach(connector => connector.initialize());
			buttonsA.ForEach(button => button.initialize());
			doorsA.ForEach(door => door.initialize());
			leversA.ForEach(lever => lever.initialize());
			platformsA.ForEach(platform => platform.initialize());
			puzzleGroups.ForEach(puzzleGroup => puzzleGroup.initialize());
			
			player.initialize(startIndex);
			clone.initialize(startIndex);

			levelCompleted = finishTrigger.initialize().reached;
		}
	}
}
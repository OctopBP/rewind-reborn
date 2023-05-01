#if UNITY_EDITOR
using Rewind.Behaviours;
using Rewind.ECSCore;
using Sirenix.OdinInspector;

namespace Rewind.Core {
	public partial class Level {
		[Button]
		void bakeLevel() {
			paths = FindObjectsOfType<Path>();
			connectors = FindObjectsOfType<Connector>();
			buttonsA = FindObjectsOfType<ButtonA>();
			doorsA = FindObjectsOfType<DoorA>();
			gearTypeA = FindObjectsOfType<GearTypeA>();
			gearTypeB = FindObjectsOfType<GearTypeB>();
			gearTypeC = FindObjectsOfType<GearTypeC>();
			leversA = FindObjectsOfType<LeverA>();
			platformsA = FindObjectsOfType<PlatformA>();
			puzzleGroups = FindObjectsOfType<PuzzleGroup>();
			
			finishTrigger = FindObjectOfType<Finish>();
		}
	}
}
#endif
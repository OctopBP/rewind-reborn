using Rewind.Behaviours;
using Rewind.ECSCore;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Rewind.Core {
	public partial class Level : MonoBehaviour {
		[SerializeField, InfoBox("Can be empty for now")] BackedLevel backedLevel;
		
		[Space(10)]
		[SerializeField, Required, PublicAccessor] Player player;
		[SerializeField, Required, PublicAccessor] Clone clone;
		[SerializeField, PublicAccessor] PathPoint startIndex;

		[Space(10)]
		[SerializeField, Required, PublicAccessor] Clock clock;
		[SerializeField, Required, PublicAccessor] GameSettingsBehaviour gameSettings;

		[Space(10)]
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
			public readonly Finish.Model finishModel;

			public Model(Level backing) {
				finishModel = new Finish.Model(backing.finishTrigger.pointIndex);
				
				var gameSettings = backing.gameSettings;
				
				backing.gameSettings.initialize();
				backing.clock.initialize();

				backing.paths.ForEach(path => path.initialize());
				backing.connectors.ForEach(connector => connector.initialize());
				backing.buttonsA.ForEach(button => button.initialize());
				backing.doorsA.ForEach(door => door.initialize());
				backing.gearTypeA.ForEach(gear => gear.initialize());
				backing.gearTypeB.ForEach(gear => gear.initialize());
				backing.gearTypeC.ForEach(gear => gear.initialize());
				backing.leversA.ForEach(lever => lever.initialize());
				backing.platformsA.ForEach(platform => platform.initialize());
				backing.puzzleGroups.ForEach(puzzleGroup => puzzleGroup.initialize());
				
				backing.player.initialize(new(backing.startIndex, gameSettings.gameSettingsData));
				backing.clone.initialize(new(backing.startIndex, gameSettings.gameSettingsData));
			}
		}
	}
}
#if UNITY_EDITOR
using Rewind.Behaviours;
using Rewind.ECSCore;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rewind.Core {
	public partial class Level {
		[Button]
		void bakeLevel() {
			if (backedLevel == null) {
				var scene = SceneManager.GetActiveScene();
				var asset = ScriptableObject.CreateInstance<BackedLevel>();

				AssetDatabase.CreateAsset(asset, $"{System.IO.Path.GetDirectoryName(scene.path)}/{scene.name}_backed.asset");
				AssetDatabase.SaveAssets();

				backedLevel = asset;
			}
			bake();
		}
		
		void bake() {
			player = FindObjectOfType<Player>();
			clone = FindObjectOfType<Clone>();

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

			clock = FindObjectOfType<Clock>();
			finishTrigger = FindObjectOfType<Finish>();
		}
	}
}
#endif
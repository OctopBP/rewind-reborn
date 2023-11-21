using Cysharp.Threading.Tasks;
using Rewind.Extensions;
using Rewind.Helpers.Interfaces.UnityCallbacks;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Rewind.Core {
	public class GameLoader : MonoBehaviour, IStart {
		[SerializeField] MainMenu mainMenu;
		[SerializeField, Required] AssetReference levelsCoreScene;
		
		public void Start() {
			new Init(this).forSideEffect();
		}

		class Init {
			readonly AssetReference levelsCoreScene;
			
			public Init(GameLoader backing) {
				levelsCoreScene = backing.levelsCoreScene;
				
				var mainMenu = new MainMenu.Init(backing.mainMenu);
				mainMenu._startPressed.Subscribe(_ => {
					mainMenu.disable();
					startGame();
				});
			}

			async void startGame() {
				var scene = await levelsCoreScene.LoadSceneAsync();
				var levelsController = scene.Scene.GetRootGameObjects()
					.Collect(go => go.GetComponent<LevelsController>().optionFromNullable())
					.first()
					.getOrThrow($"{nameof(LevelsController)} should be here");

				var levelsControllerInit = levelsController.init();
				levelsControllerInit.startGame();
			}
		}
	}
}
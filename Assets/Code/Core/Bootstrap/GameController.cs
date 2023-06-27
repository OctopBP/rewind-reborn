using System.Linq;
using Cysharp.Threading.Tasks;
using Rewind.Extensions;
using Rewind.Helpers.Interfaces.UnityCallbacks;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Rewind.Core {
	public class GameController : MonoBehaviour, IStart {
		[SerializeField] MainMenu mainMenu;
		[SerializeField, Required] AssetReference levelsCoreScene;
		
		public void Start() {
			new Init(this).forSideEffect();
		}

		class Init {
			readonly AssetReference levelsCoreScene;
			
			public Init(GameController backing) {
				levelsCoreScene = backing.levelsCoreScene;
				
				var mainMenu = new MainMenu.Init(backing.mainMenu);
				mainMenu._startPressed.Subscribe(_ => {
					mainMenu.disable();
					startGame();
				});
			}

			async void startGame() {
				var scene = await levelsCoreScene.LoadSceneAsync(LoadSceneMode.Additive);
				var levelsController = scene.Scene.GetRootGameObjects()
					.Select(gameObject => gameObject.GetComponent<LevelsController>())
					.first()
					.getOrThrow($"{nameof(LevelsController)} should be here");

				var levelsControllerInit = new LevelsController.Init(levelsController);
				levelsControllerInit.startGame();
			}
		}
	}
}
using Cysharp.Threading.Tasks;
using Rewind.Extensions;
using Rewind.Helpers.Interfaces.UnityCallbacks;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Rewind.Core
{
	public class GameLoader : MonoBehaviour, IStart
    {
		[SerializeField] private MainMenu mainMenu;
		[SerializeField, Required] private AssetReference levelsCoreScene;
		
		public void Start()
        {
			new Init(this).ForSideEffect();
		}

		private class Init
        {
			private readonly AssetReference levelsCoreScene;
			
			public Init(GameLoader backing)
            {
				levelsCoreScene = backing.levelsCoreScene;
				
				var mainMenu = new MainMenu.Init(backing.mainMenu);
				mainMenu._startPressed.Subscribe(_ =>
                {
					mainMenu.Disable();
					StartGame();
				});
			}

			private async void StartGame()
            {
				var scene = await levelsCoreScene.LoadSceneAsync();
				var levelsController = scene.Scene.GetRootGameObjects()
					.Collect(go => go.GetComponent<LevelsController>().OptionFromNullable())
					.First()
					.GetOrThrow($"{nameof(LevelsController)} should be here");

				var levelsControllerInit = levelsController.Initialize();
				levelsControllerInit.StartGame();
			}
		}
	}
}
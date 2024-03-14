using System.Collections.Generic;
using System.Linq;
using Code.Helpers;
using Cysharp.Threading.Tasks;
using LanguageExt;
using Rewind.Behaviours;
using Rewind.Extensions;
using Rewind.Helpers.Interfaces.UnityCallbacks;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Rewind.Core
{
	public partial class LevelsController : MonoBehaviour, IUpdate
    {
		[SerializeField, Required] private CoreBootstrap coreBootstrap;
		
		[Header("Levels")]
		[SerializeField]
		private List<AssetReference> scenes;
		[SerializeField] private int startIndex;

		private Option<Init> maybeInit;
		
		public Init Initialize()
        {
			var init = new Init(this);
			maybeInit = init;
			return init;
		}

		public void Update() => maybeInit.IfSome(m => m.Update());

		public partial class Init
        {
			[GenConstructor]
			public partial class LevelInfo
            {
				public readonly Scene Scene;
				public readonly Level.Init Init;
			}

			private readonly LevelsController backing;
			private readonly MainMenu.Init mainMenu;

			private readonly Option<CoreBootstrap.Init> coreBootstrapInit;

			private LevelInfo currentLevel;
			private Option<LevelInfo> maybeNextLevel;

			public Init(LevelsController backing)
            {
				this.backing = backing;
				coreBootstrapInit = new CoreBootstrap.Init(backing.coreBootstrap);
			}

			public async void StartGame()
            {
				var maybeLevel = await LoadLevel(backing.startIndex);
				currentLevel = maybeLevel.GetOrThrow("There should be at least one level");

				coreBootstrapInit.IfSome(bootstrap => bootstrap.PlaceCharacterToPoint(
					currentLevel.Init.StartTrigger.Point, currentLevel.Init.StartPosition
				));
			}
			
			public async void LoadLevel(string levelSceneName)
            {
				await SceneManager.LoadSceneAsync(levelSceneName, LoadSceneMode.Additive);
				var levelScene = SceneManager.GetSceneByName(levelSceneName); 

				coreBootstrapInit.IfSome(bootstrap =>
                {
					var level = levelScene.GetFirstComponentInGameObjects<Level>()
						.GetOrThrow($"{nameof(Level)} should be defined");
					currentLevel = new LevelInfo(levelScene, new Level.Init(level, bootstrap.LevelAudio));
					bootstrap.PlaceCharacterToPoint(
						currentLevel.Init.StartTrigger.Point, currentLevel.Init.StartPosition
					);
				});
			}

			private async UniTask<Option<LevelInfo>> LoadLevel(int index)
            {
				if (index >= backing.scenes.Count)
                {
					Debug.Log($"There is no levels at index: {index}"
						.WrapInColorTag(ColorA.Red)
						.addTagOnStart(nameof(LevelsController))
					);
					return Option<LevelInfo>.None;
				}
				else
				{
					var nextScene = backing.scenes[index];
					var scene = await nextScene.LoadSceneAsync(LoadSceneMode.Additive);

					return coreBootstrapInit.Map(coreBootstrap =>
                    {
						var levelModel = FunctionalExtensions.First(scene.Scene.GetRootGameObjects()
								.Select(gameObject => gameObject.GetComponent<Level>()))
							.Map(level => new Level.Init(level, coreBootstrap.LevelAudio))
							.GetOrThrow("Can't get level");

						levelModel.StartTrigger.Reached.Subscribe(_ => OnLevelStarted());
						levelModel.FinisTrigger.Reached.Subscribe(_ => OnLevelFinished());
						
						return new LevelInfo(scene.Scene, levelModel);
					});
				}

				async void OnLevelFinished()
                {
					var newLevel = await LoadLevel(++index);
					maybeNextLevel = newLevel;

					maybeNextLevel.IfSome(nextLevel => PathConnector.Model.fromPathPointsPare(
						pathPointsPare: new(currentLevel.Init.FinisTrigger.Point, nextLevel.Init.StartTrigger.Point),
						tracker: currentLevel.Init.Tracker
					).ForSideEffect());
				}
				
				void OnLevelStarted()
                {
					maybeNextLevel.IfSome(nextLevel =>
                    {
						currentLevel.Init.Dispose();
						SceneManager.UnloadSceneAsync(currentLevel.Scene);

						currentLevel = nextLevel;
						maybeNextLevel = Option<LevelInfo>.None;
					});
				}
			}

			public void Update() => coreBootstrapInit.IfSome(_ => _.Update());
		}
	}
}
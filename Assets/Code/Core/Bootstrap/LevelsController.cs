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

namespace Rewind.Core {
	public partial class LevelsController : MonoBehaviour, IStart, IUpdate {
		[SerializeField, Required] CoreBootstrap coreBootstrap;
		
		[Header("Levels")]
		[SerializeField] List<AssetReference> scenes;
		[SerializeField] int startIndex;

		Option<Init> model;
		
		public void Start() {
			model = new Init(this);
		}

		public void Update() => model.IfSome(m => m.update());

		public partial class Init {
			[GenConstructor]
			public partial class LevelInfo {
				public readonly AssetReference sceneRef;
				public readonly Level.Init init;
			}
			
			readonly LevelsController backing;
			readonly MainMenu.Init mainMenu;

			readonly Option<CoreBootstrap.Init> coreBootstrapInit;
			
			LevelInfo currentLevel;
			Option<LevelInfo> maybeNextLevel;

			public Init(LevelsController backing) {
				this.backing = backing;
				coreBootstrapInit = new CoreBootstrap.Init(backing.coreBootstrap);
			}

			public async void startGame() {
				var maybeLevel = await loadLevel(backing.startIndex);
				currentLevel = maybeLevel.getOrThrow("There should be at least one level");

				coreBootstrapInit.IfSome(bootstrap => bootstrap.placeCharacterToPoint(
					currentLevel.init.startTrigger.point, currentLevel.init.startPosition
				));
			}

			async UniTask<Option<LevelInfo>> loadLevel(int index) {
				if (index >= backing.scenes.Count) {
					Debug.Log(
						$"There is no levels at index: {index}"
							.wrapInColorTag(ColorA.red)
							.addTagOnStart(nameof(GameController))
					);
					return Option<LevelInfo>.None;
				}
				else {
					var nextScene = backing.scenes[index];
					var scene = await nextScene.LoadSceneAsync(LoadSceneMode.Additive);

					return coreBootstrapInit.Map(coreBootstrap => {
						var levelModel = scene.Scene.GetRootGameObjects()
							.Select(gameObject => gameObject.GetComponent<Level>()).first()
							.Map(level => new Level.Init(level, coreBootstrap.levelAudio))
							.getOrThrow("Can't get level");

						levelModel.startTrigger.reached.Subscribe(_ => onLevelStarted());
						levelModel.finisTrigger.reached.Subscribe(_ => onLevelFinished());
						
						return new LevelInfo(nextScene, levelModel);
					});
				}

				async void onLevelFinished() {
					var newLevel = await loadLevel(++index);
					maybeNextLevel = newLevel;

					maybeNextLevel.IfSome(nextLevel => PathConnector.Model.fromPathPointsPare(
						pathPointsPare: new(currentLevel.init.finisTrigger.point, nextLevel.init.startTrigger.point),
						tracker: currentLevel.init.tracker
					).forSideEffect());
				}
				
				void onLevelStarted() {
					maybeNextLevel.IfSome(nextLevel => {
						currentLevel.init.dispose();
						currentLevel.sceneRef.UnLoadScene();

						currentLevel = nextLevel;
						maybeNextLevel = Option<LevelInfo>.None;
					});
				}
			}

			public void update() => coreBootstrapInit.IfSome(_ => _.update());
		}
	}
}
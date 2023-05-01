using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageExt;
using Rewind.Extensions;
using Rewind.Helpers.Interfaces.UnityCallbacks;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Rewind.Core {
	public class GameController : MonoBehaviour, IStart, IUpdate {
		[SerializeField] MainMenu mainMenu;
		[SerializeField, Required] AssetReference levelCoreScene;
		
		[Header("Levels"), SerializeField] List<AssetReference> scenes;
		[SerializeField] int index;

		Option<Model> model;
		
		public void Start() {
			model = new Model(this);
		}

		public void Update() => model.IfSome(m => m.update());

		class Model {
			readonly GameController backing;
			readonly MainMenu.Init mainMenu;
			
			Option<CoreBootstrap.Model> coreBootstrapModel;
			Option<(AssetReference sceneRef, Level.Model model)> activeLevel;
			Option<(AssetReference sceneRef, Level.Model model)> previousLevel;

			public Model(GameController backing) {
				this.backing = backing;
				mainMenu = new(backing.mainMenu);
				mainMenu._startPressed.Subscribe(_ => loadGame());
			}

			async void loadGame() {
				mainMenu.disable();
				
				// var levelIndex = new ReactiveProperty<int>(backing.index);
				// levelIndex.Subscribe(onNextLevel);
				
				var scene = await backing.levelCoreScene.LoadSceneAsync(LoadSceneMode.Additive).Task;
				var coreBootstrap = scene.Scene.GetRootGameObjects()
					.Select(gameObject => gameObject.GetComponent<CoreBootstrap>())
					.first()
					.getOrThrow($"{nameof(CoreBootstrap)} should be here");
				
				coreBootstrapModel = new CoreBootstrap.Model(coreBootstrap);
				
				await loadLevel(backing.index);

				coreBootstrapModel
					.Map(bootstrap => activeLevel.Map(level => (bootstrap, level)))
					.Flatten()
					.IfSome(tpl => tpl.bootstrap.placeCharacterToPoint(tpl.level.model.backing._startIndex));
			}

			// async void onNextLevel(int idx) => await loadLevel(idx);

			async Task loadLevel(int index) {
				var nextScene = backing.scenes[index];
				var scene = await nextScene.LoadSceneAsync(LoadSceneMode.Additive).Task;

				var levelModel = scene.Scene.GetRootGameObjects()
					.Select(gameObject => gameObject.GetComponent<Level>()).first()
					.Map(level => new Level.Model(level));

				activeLevel = levelModel.Map(model => (nextScene, model));

				activeLevel.IfSome(tpl => {
					tpl.model.started.Subscribe(_ => unloadLevel());
					tpl.model.finishModel.reached.Subscribe(_ => onLevelFinished());
				});

				async void onLevelFinished() {
					previousLevel = activeLevel;
					await loadLevel(++index);
				}
			}

			void unloadLevel() {
				previousLevel.IfSome(tpl => {
					tpl.model.dispose();
					tpl.sceneRef.UnLoadScene();
				});
				previousLevel = Option<(AssetReference sceneRef, Level.Model model)>.None;
			}

			public void update() => coreBootstrapModel.IfSome(_ => _.update());
		}
	}
}
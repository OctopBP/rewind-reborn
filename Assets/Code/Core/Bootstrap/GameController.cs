using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using Rewind.Extensions;
using Rewind.Helpers.Interfaces.UnityCallbacks;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Rewind.Core {
	public class GameController : MonoBehaviour, IStart, IUpdate {
		[SerializeField] MainMenu mainMenu;

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

			Option<CoreBootstrap.Model> maybeCoreBootstrapMode = Option<CoreBootstrap.Model>.None;

			public Model(GameController backing) {
				this.backing = backing;
				mainMenu = new(backing.mainMenu, loadLevel);
				mainMenu._startPressed.Subscribe(_ => loadLevel(backing.index));
			}
			
			void loadLevel(int index) {
				var nextScene = backing.scenes[index];
				var loadSceneOperation = nextScene.LoadSceneAsync(LoadSceneMode.Additive);
				loadSceneOperation.Completed += setupLevel;

				void setupLevel(AsyncOperationHandle<SceneInstance> scene) {
					mainMenu.disable();

					maybeCoreBootstrapMode = scene.Result.Scene.GetRootGameObjects()
						.Select(gameObject => gameObject.GetComponent<CoreBootstrap>()).first()
						.Map(bootstrap => new CoreBootstrap.Model(bootstrap));

					maybeCoreBootstrapMode.IfSome(
						bootstrapModel => bootstrapModel.levelMode.finishModel.reached.Subscribe(_ => handleFinishLevel())
					);
				}

				void handleFinishLevel() {
					maybeCoreBootstrapMode.IfSome(bootstrapModel => bootstrapModel.Dispose());
					nextScene.UnLoadScene();
					loadLevel(++index);
				}
			}

			public void update() {
				maybeCoreBootstrapMode.IfSome(bootstrapModel => bootstrapModel.update());
			}
		}
	}
}
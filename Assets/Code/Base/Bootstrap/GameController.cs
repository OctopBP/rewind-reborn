using System.Collections.Generic;
using System.Linq;
using Rewind.ECSCore;
using Rewind.Extensions;
using Rewind.Helpers.Interfaces.UnityCallbacks;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Rewind.Core.Code.Base.Bootstrap {
	public class GameController : MonoBehaviour, IStart {
		[SerializeField] MainMenu mainMenu;

		[Header("Levels"), SerializeField] List<AssetReference> scenes;
		[SerializeField] int index;

		public void Start() => Init.create(this);

		class Init {
			readonly GameController backing;
			readonly MainMenu.Init mainMenu;

			Init(GameController backing) {
				this.backing = backing;
				mainMenu = new(backing.mainMenu, loadLevel);
				mainMenu.backing.startButton.onClick.AddListener(() => loadLevel(backing.index));
			}

			public static void create(GameController backing) => new Init(backing);

			void loadLevel(int index) {
				var nextScene = backing.scenes[index];
				var ao = nextScene.LoadSceneAsync(LoadSceneMode.Additive);
				ao.Completed += scene => {
					mainMenu.backing.setInactive();

					var coreBootstrap = scene.Result.Scene.GetRootGameObjects()
						.Select(gameObject => gameObject.GetComponent<CoreBootstrap>()).ToOption().First();

					coreBootstrap.levelCompleted.Where(_ => _).Subscribe(reached => {
						nextScene.UnLoadScene();
						loadLevel(++index);
					});
				};
			}
		}
	}
}
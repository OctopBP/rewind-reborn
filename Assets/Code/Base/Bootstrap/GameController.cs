using System.Collections.Generic;
using Rewind.Extensions;
using Rewind.Helpers.Interfaces.UnityCallbacks;
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
			readonly MainMenu.Init mainMenu;

			Init(GameController backing) {
				mainMenu = new(backing.mainMenu);
				mainMenu.backing.loadButton.onClick.AddListener(() => {
					var ao = backing.scenes[backing.index].LoadSceneAsync(LoadSceneMode.Additive);
					ao.Completed += _ => mainMenu.backing.setInactive();
				});
			}

			public static void create(GameController backing) => new Init(backing);
		}
	}
}
using LanguageExt;
using UnityEngine.SceneManagement;

namespace Rewind.Extensions {
	public static class SceneExt {
		public static Option<T> getFirstComponentInGameObjects<T>(this Scene scene) =>
			scene.GetRootGameObjects()
				.Collect(go => go.GetComponent<T>().optionFromNullable())
				.first();
	}
}
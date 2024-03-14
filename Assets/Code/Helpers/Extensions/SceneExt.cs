using LanguageExt;
using UnityEngine.SceneManagement;

namespace Rewind.Extensions
{
	public static class SceneExt
    {
		public static Option<T> GetFirstComponentInGameObjects<T>(this Scene scene) =>
			scene.GetRootGameObjects()
				.Collect(go => go.GetComponent<T>().OptionFromNullable())
				.First();
	}
}
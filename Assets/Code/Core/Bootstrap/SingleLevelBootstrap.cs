using UnityEditor;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class SingleLevelBootstrap {
	static SingleLevelBootstrap() => EditorApplication.playModeStateChanged += LoadCore;

	static bool haveBootstrap() {
		for (var i = 0; i < SceneManager.sceneCount; i++) {
			var levelName = SceneManager.GetSceneAt(i).name;
			if (levelName is "Level_Bootstrap" or "Bootstrap") {
				return true;
			}
		}

		return false;
	}
	
	static void LoadCore(PlayModeStateChange state) {
		// if (state != PlayModeStateChange.EnteredPlayMode) return;
		// if (haveBootstrap()) return;
		//
		// SceneManager.LoadScene("Level_Bootstrap", LoadSceneMode.Additive);
	}
}
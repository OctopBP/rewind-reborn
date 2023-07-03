using Cysharp.Threading.Tasks;
using Rewind.Core;
using Rewind.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class SingleLevelBootstrap {
	static SingleLevelBootstrap() => EditorApplication.playModeStateChanged += LoadCore;

	static bool isSingleLevel() =>
		SceneManager.sceneCount == 1
		&& SceneManager.GetActiveScene().getFirstComponentInGameObjects<Level>().IsSome;

	static void LoadCore(PlayModeStateChange state) {
		LoadCoreTask(state).forSideEffect();
	}

	static async UniTask LoadCoreTask(PlayModeStateChange state) {
		if (state != PlayModeStateChange.EnteredPlayMode) return;
		if (!isSingleLevel()) return;

		var activeLevelName = SceneManager.GetActiveScene().name;

		await LoadLevelAsync("LevelBootstrap");
		var levelBootstrapScene = SceneManager.GetActiveScene();
		
		var levelsController = levelBootstrapScene.getFirstComponentInGameObjects<LevelsController>()
			.getOrThrow($"{nameof(LevelsController)} should be here");

		var levelsControllerInit = levelsController.init();
		levelsControllerInit.loadLevel(activeLevelName);
	}
	
	static async UniTask LoadLevelAsync(string sceneName) {
		await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
	}
}
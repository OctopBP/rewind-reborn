using Cysharp.Threading.Tasks;
using Rewind.Core;
using Rewind.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class SingleLevelBootstrap
{
	static SingleLevelBootstrap() => EditorApplication.playModeStateChanged += LoadCore;

	private static bool IsSingleLevel() =>
		SceneManager.sceneCount == 1
		&& SceneManager.GetActiveScene().GetFirstComponentInGameObjects<Level>().IsSome;

	private static void LoadCore(PlayModeStateChange state)
	{
		LoadCoreTask(state).ForSideEffect();
	}

	private static async UniTask LoadCoreTask(PlayModeStateChange state)
	{
		if (state != PlayModeStateChange.EnteredPlayMode) return;
		if (!IsSingleLevel()) return;

		var activeLevelName = SceneManager.GetActiveScene().name;

		await LoadLevelAsync("LevelBootstrap");
		var levelBootstrapScene = SceneManager.GetActiveScene();
		
		var levelsController = levelBootstrapScene.GetFirstComponentInGameObjects<LevelsController>()
			.GetOrThrow($"{nameof(LevelsController)} should be here");

		var levelsControllerInit = levelsController.Initialize();
		levelsControllerInit.LoadLevel(activeLevelName);
	}

	private static async UniTask LoadLevelAsync(string sceneName)
	{
		await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
	}
}
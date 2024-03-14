﻿#if UNITY_EDITOR
using LanguageExt;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Rewind.Services.Autotest
{
	public partial class AutotestInputService
	{
		private static Option<string> getPath => EditorUtility.SaveFilePanelInProject(
			"Save Autotest Input asset",
			"Autotest.asset",
			"asset",
			""
		);

		[Button]
		private void CreateAsset()
		{
			getPath.Filter(p => p.Length > 0).IfSome(path =>
			{
				var asset = ScriptableObject.CreateInstance<AutotestInput>();

				AssetDatabase.CreateAsset(asset, path);
				AssetDatabase.SaveAssets();

				EditorUtility.FocusProjectWindow();
				autotestInput = asset;
			});
		}
	}
}
#endif
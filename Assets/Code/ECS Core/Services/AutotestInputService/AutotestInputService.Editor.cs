#if UNITY_EDITOR
using System;
using LanguageExt;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Rewind.Services.Autotest {
	public partial class AutotestInputService {
		static Option<string> getPath => EditorUtility.SaveFilePanelInProject(
			"Save Autotest Input asset",
			$"Replay {DateTime.Now:d MMM yyyy} at {DateTime.Now:h.mm.ss}.asset",
			"asset",
			""
		);

		[Button]
		static void createAsset() {
			getPath.Filter(p => p.Length > 0).IfSome(path => {
				var asset = ScriptableObject.CreateInstance<AutotestInput>();

				// todo: setup asset

				AssetDatabase.CreateAsset(asset, path);
				AssetDatabase.SaveAssets();

				EditorUtility.FocusProjectWindow();
				Selection.activeObject = asset;	
			});
		}
	}
}
#endif
using Code.Helpers.Utils;
using Entitas.VisualDebugging.Unity;
using LanguageExt;
using Rewind.Extensions;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class EntitasInspectorWindow : OdinEditorWindow {
  [MenuItem("Tools/Entitas/Entitas Inspector")]
  public static void openWindow() => GetWindow<EntitasInspectorWindow>().Show();

  // Lst<EntityBehaviour> entityBehaviours;
  
  Vector2 scrollPos;
  
  protected override void OnEnable() {
    base.OnEnable();
    titleContent = new GUIContent("Entitas Inspector");
  }

  protected override void OnGUI() {
    // using (new GUIColor(Color.green)) {
    //   if (GUILayout.Button("Refresh")) {
    //     Debug.Log("EI Refresh");
    //     
    //     var go = new GameObject("TEMP");
    //     DontDestroyOnLoad(go);
    //
    //     entityBehaviours = new();
    //
    //     var scene = go.scene;
    //     go.DestroyGameObject();
    //     
    //     Debug.Log($"EI Scene {scene.name}");
    //     
    //     var sceneEntityBehaviours = scene.GetRootGameObjects()
    //       .Collect(_ => _.GetComponentsInChildren<EntityBehaviour>());
    //     
    //     entityBehaviours = entityBehaviours.Append(new(sceneEntityBehaviours));
    //   }
    // }

    GUILayoutExt.beginVertical(() => {
      scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
      // for (var j = 0; j < entityBehaviours.Length(); j++) {
      //   var entityBehaviour = entityBehaviours[j];

      var contexts = Contexts.sharedInstance.allContexts;

      var game = Contexts.sharedInstance.game;
      foreach (var gameEntity in game.GetEntities()) {
        using (new GUILayoutHorizontal()) {
          EditorGUILayout.LabelField($"Entity {gameEntity.creationIndex}", EditorStyles.largeLabel);
          foreach (var componentIndex in gameEntity.GetComponentIndices()) {
            EditorGUILayout.LabelField($"{componentIndex}", EditorStyles.helpBox);
            // EditorGUILayout.LabelField($"{}", EditorStyles.helpBox);
          }
        }
      }
      
      foreach (var context in contexts) {
        EditorGUILayout.LabelField($"{context.contextInfo.name}", EditorStyles.largeLabel);
      }

        // GUILayoutExt.beginHorizontal(() => {
        //   EditorGUILayout.LabelField($"Entity {entityBehaviour.entity.creationIndex}", EditorStyles.largeLabel, GUILayout.ExpandWidth(false));
        //   var componentNames = entityBehaviour.name.Split("(")[1].Split(")")[0].Split(",").Map(_ => _.Trim(' '));
        //   foreach (var componentName in componentNames) {
        //     using (new GUIBackgroundColor(ColorExtensions.randomColorForHashCode(componentName.GetHashCode()))) {
        //       EditorGUILayout.LabelField(componentName, EditorStyles.helpBox, GUILayout.ExpandWidth(false));
        //     }
        //   }
        // });
      // }

      EditorGUILayout.EndScrollView();
    });

    base.OnGUI();
  }
}
using Code.Helpers.Utils;
using Entitas;
using Entitas.VisualDebugging.Unity.Editor;
using LanguageExt;
using Rewind.Extensions;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class EntitasInspectorWindow : OdinEditorWindow {
  [MenuItem("Tools/Entitas/Entitas Inspector")]
  public static void openWindow() => GetWindow<EntitasInspectorWindow>().Show();

  Vector2 scrollPos;
  Option<IEntity> maybeSelectedEntity;
  
  protected override void OnEnable() {
    base.OnEnable();
    titleContent = new GUIContent("Entitas Inspector");
  }

  protected override void OnGUI() {
    using (new GUILayoutHorizontal()) {
      using (new GUILayoutVertical()) {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        var game = Contexts.sharedInstance.game;
        foreach (var gameEntity in game.GetEntities()) {
          using (new GUILayoutHorizontal()) {
            EditorGUILayout.LabelField($"Entity {gameEntity.creationIndex}", EditorStyles.largeLabel);
            foreach (var component in gameEntity.GetComponents()) {
              var componentType = component.GetType();
              var componentName = componentType.Name.RemoveComponentSuffix();
              
              using (new GUIBackgroundColor(ColorExtensions.randomColorForHashCode(componentName.GetHashCode()))) {
                EditorGUILayout.LabelField(componentName, EditorStyles.helpBox);
              }
            }
          }
        }
        
        // var contexts = Contexts.sharedInstance.allContexts;
        // foreach (var context in contexts) {
        //   EditorGUILayout.LabelField($"{context.contextInfo.name}", EditorStyles.largeLabel);
        // }

        EditorGUILayout.EndScrollView();
      }
      
      maybeSelectedEntity.IfSome(EntityDrawer.DrawEntity);
    }

    base.OnGUI();
  }
}
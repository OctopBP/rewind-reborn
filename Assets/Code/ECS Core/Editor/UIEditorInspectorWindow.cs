using System;
using Entitas;
using Rewind.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EntitasInspector : EditorWindow {
	// Parent element for dynamically changing UI elements
	ListView rowsContainer;

	VisualTreeAsset entityRowVisualTree;
	VisualTreeAsset componentTagVisualTree;

	const string EntitasInspectorPath = "Assets/Code/Helpers/UI/EntitasInspector/EntitasInspector.uxml";
	const string EntitasRowPath = "Assets/Code/Helpers/UI/EntitasInspector/EntityRow.uxml";
	const string ComponentTagPath = "Assets/Code/Helpers/UI/EntitasInspector/ComponentTag.uxml";

	[MenuItem("Tools/EntitasInspector")]
	public static void showWindow() {
		// Create a new window instance
		var window = GetWindow<EntitasInspector>();
		window.titleContent = new GUIContent("UI Entitas Inspector");
	}

	void OnEnable() {
		// Load the UI visual tree from a UXML file
		var mainVisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(EntitasInspectorPath);
		var mainUI = mainVisualTree.CloneTree();
		rootVisualElement.Add(mainUI);
		
		// Find the parent element in the main UI where you want to add the additional UI elements
		rowsContainer = mainUI.Q<ListView>("rows");
		
		// Load additional UI elements from a different UXML file
		entityRowVisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(EntitasRowPath);
		componentTagVisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(ComponentTagPath);
	}

	void Update() {
		createRows();
	}

	void createRows() {
		rowsContainer.Clear();

		var game = Contexts.sharedInstance.game;
		var listData = game.GetEntities();

		rowsContainer.itemsSource = listData;
		rowsContainer.bindItem = bindItem;
		rowsContainer.makeItem = makeItem;

		VisualElement makeItem() => entityRowVisualTree.CloneTree();

		void bindItem(VisualElement e, int i) {
			var entity = listData[i];
			var entityName = e.Q<Label>("index");
			entityName.text = $"Entity {entity.creationIndex}";
			
			var tagsContainer = e.Q<VisualElement>("tags");
			createRow(tagsContainer, entity);
		}
	}

	void createRow(VisualElement tagsContainer, IEntity entity) {
		tagsContainer.Clear();

		foreach (var component in entity.GetComponents()) {
			var componentTag = componentTagVisualTree.CloneTree();
			var tagButton = componentTag.Q<Button>("tag");
			
			var componentType = component.GetType();
			var componentName = componentType.Name.RemoveComponentSuffix();
			
			var color = ColorExtensions.randomColorForHashCode(componentName.GetHashCode());
			tagButton.text = componentName;
			tagButton.style.backgroundColor = color;
			
			tagsContainer.Add(componentTag);
		}
	}
}
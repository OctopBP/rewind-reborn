using System.Collections.Generic;
using System.Linq;
using Code.Helpers;
using Code.Helpers.FileSystem;
using Entitas;
using Rewind.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class EntitasInspector : EditorWindow {
	// Parent element for dynamically changing UI elements
	VisualTreeAsset entitasInspectorContextGroup;
	VisualTreeAsset entityRowVisualTree;
	VisualTreeAsset componentTagVisualTree;
	ListView rowsContainer;
	DropdownField contextsDropdown;

	string selectedContext;

	static readonly PathStr FolderPath = new PathStr("Assets/Code/UI/Editor/EntitasInspector");
	static readonly PathStr EntitasInspectorPath = FolderPath / "EntitasInspector.uxml";
	static readonly PathStr EntitasInspectorContextGroupPath = FolderPath / "EntitasInspectorContextGroup.uxml";
	static readonly PathStr EntitasRowPath = FolderPath / "EntityRow.uxml";
	static readonly PathStr ComponentTagPath = FolderPath / "ComponentTag.uxml";

	bool isPaused;
	
	[MenuItem("Tools/Entitas Inspector")]
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

		rowsContainer = mainUI.Q<ListView>("rows");
		
		contextsDropdown = mainUI.Q<DropdownField>("contexts-dropdown");
		var clearBtn = mainUI.Q<Button>("clear-btn");
		var pauseBtn = mainUI.Q<Button>("play-pause-btn");
		
		// clearBtn.clicked += () => rowsContainer.itemsSource.Clear();
		pauseBtn.clicked += () => {
			isPaused = !isPaused;
			pauseBtn.text = isPaused ? "Play" : "Pause";
			pauseBtn.style.backgroundColor = new StyleColor(isPaused ? ColorA.green : ColorA.red);
		};

		selectedContext = "Game";
		
		// Load additional UI elements from a different UXML file
		entitasInspectorContextGroup = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(EntitasInspectorContextGroupPath);
		entityRowVisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(EntitasRowPath);
		componentTagVisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(ComponentTagPath);
	}
	
	void OnDestroy() {
		rowsContainer.itemsSource.Clear();
	}

	void Update() {
		if (isPaused) return;

		var contextGroups = new Dictionary<string, IEntity[]> {
			{ "Config", Contexts.sharedInstance.config.GetEntities().upcast(default(IEntity[])) },
			{ "Input", Contexts.sharedInstance.input.GetEntities().upcast(default(IEntity[])) },
			{ "Game", Contexts.sharedInstance.game.GetEntities().upcast(default(IEntity[])) }
		};

		contextsDropdown.choices = new() { "Config", "Input", "Game" };
		contextsDropdown.RegisterValueChangedCallback(evt => selectedContext = evt.newValue);

		createRows(contextGroups[selectedContext]);
	}

	// void createFoldouts(ContextGroup[] contextGroups) {
	// 	rowsContainer.itemsSource = contextGroups;
	// 	rowsContainer.bindItem = bindItem;
	// 	rowsContainer.makeItem = makeItem;
	//
	// 	VisualElement makeItem() => new VisualElement();
	//
	// 	void bindItem(VisualElement e, int i) {
	// 		var contextGroup = contextGroups[i];
	// 		var foldout = new Foldout();
	// 		foldout.text = contextGroup.name;
	// 		
	// 		var list = createRows(contextGroup.entities);
	// 		foldout.Add(list);
	//
	// 		e.Add(foldout);
	// 	}
	// }
	
	void createRows(IEntity[] entities) {
		rowsContainer.itemsSource = entities;
		rowsContainer.bindItem = bindItem;
		rowsContainer.makeItem = makeItem;
		
		// rowsContainer.selectedIndicesChanged += ints => {
			// var data = Contexts.sharedInstance.game.GetEntities();
			// maybeSelectedGameEntity = ints.HeadOrNone().Map(index => data[index]);
			// maybeSelectedComponentIndex = None;
		// };

		VisualElement makeItem() => entityRowVisualTree.CloneTree();

		void bindItem(VisualElement e, int i) {
			var entity = entities[i];
			var entityName = e.Q<Label>("index");
			entityName.text = $"#{entity.creationIndex}";
			
			var tagsContainer = e.Q<VisualElement>("tags");
			createRow(tagsContainer, entity);
		}
	}

	void createRow(VisualElement tagsContainer, IEntity entity) {
		tagsContainer.Clear();

		var componentsWithName = entity.GetComponents()
			.Map(component => (component, name: component.GetType().Name.RemoveComponentSuffix()));

		foreach (var (_, componentName) in componentsWithName.OrderBy(tpl => isListener(tpl.name))) {
			var componentTag = componentTagVisualTree.CloneTree();
			var tagButton = componentTag.Q<Button>("tag");
			
			tagButton.text = componentName;
			
			var color = ColorExtensions.randomColorForHashCode(componentName.GetHashCode());
			if (isListener(componentName)) {
				tagButton.style.backgroundColor = Color.clear;
				tagButton.style.borderBottomColor = tagButton.style.borderLeftColor = 
					tagButton.style.borderRightColor = tagButton.style.borderTopColor = color.withAlpha(.5f);
			} else {
				tagButton.style.backgroundColor = color.withAlpha(.3f);
			}
			
			tagsContainer.Add(componentTag);
		}

		bool isListener(string cName) => cName.endsWithFast("Listener");
	}
}
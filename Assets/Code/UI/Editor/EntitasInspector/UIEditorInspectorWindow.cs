using DesperateDevs.Reflection;
using Entitas;
using Entitas.VisualDebugging.Unity.Editor;
using LanguageExt;
using Rewind.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static LanguageExt.Prelude;

public class EntitasInspector : EditorWindow {
	// Parent element for dynamically changing UI elements
	ListView rowsContainer;

	VisualTreeAsset entityRowVisualTree;
	VisualTreeAsset componentTagVisualTree;

	Option<GameEntity> maybeSelectedGameEntity;
	Option<int> maybeSelectedComponentIndex;

	const string BasePath = "Assets/Code/UI/Editor/EntitasInspector";
	const string EntitasInspectorPath = BasePath + "/EntitasInspector.uxml";
	const string EntitasRowPath = BasePath + "/EntityRow.uxml";
	const string ComponentTagPath = BasePath + "/ComponentTag.uxml";

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
		
		var inspectorContainer = mainUI.Q<VisualElement>("inspector");
		var imguiContainer = new IMGUIContainer(drawEntityInspector);
		inspectorContainer.Add(imguiContainer);
	}

	void Update() {
		createRows();
	}

	void drawEntityInspector() {
		maybeSelectedGameEntity.IfSome(entity => maybeSelectedComponentIndex.Match(
			componentIndex => drawComponent(entity, entity.GetComponents()[componentIndex], componentIndex),
			() => EntityDrawer.DrawComponents(entity)
		));
	}

	void drawComponent(IEntity entity, IComponent component, int index) {
		var type = component.GetType();
		
		// getTypeDrawer(type).DrawAndGetNewValue(type, "", component)
		
		var component1 = entity.CreateComponent(index, type);
		component.CopyPublicMemberValues(component1);
		
		var componentDrawer = getComponentDrawer(type);
		componentDrawer?.DrawComponent(component1);
	}
	
	static ITypeDrawer getTypeDrawer(System.Type type) {
		foreach (var typeDrawer in EntityDrawer._typeDrawers) {
			if (typeDrawer.HandlesType(type))
				return typeDrawer;
		}
		return null;
	}

	static IComponentDrawer getComponentDrawer(System.Type type) {
		foreach (var componentDrawer in EntityDrawer._componentDrawers) {
			if (componentDrawer.HandlesType(type))
				return componentDrawer;
		}
		return null;
	}

	void createRows() {
		rowsContainer.Clear();

		var game = Contexts.sharedInstance.game;
		var listData = game.GetEntities();

		rowsContainer.itemsSource = listData;
		rowsContainer.bindItem = bindItem;
		rowsContainer.makeItem = makeItem;
		rowsContainer.selectedIndicesChanged += ints => {
			var data = Contexts.sharedInstance.game.GetEntities();
			maybeSelectedGameEntity = ints.HeadOrNone().Map(index => data[index]);
			maybeSelectedComponentIndex = None;
		};

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

		var components = entity.GetComponents();
		for (var index = 0; index < components.Length; index++) {
			var component = components[index];
			var componentTag = componentTagVisualTree.CloneTree();
			var tagButton = componentTag.Q<Button>("tag");

			var idx = index;
			tagButton.clicked += () => maybeSelectedComponentIndex = idx;

			var componentType = component.GetType();
			var componentName = componentType.Name.RemoveComponentSuffix();

			var color = ColorExtensions.randomColorForHashCode(componentName.GetHashCode());
			tagButton.text = componentName;
			tagButton.style.backgroundColor = color.withAlpha(.2f);

			tagsContainer.Add(componentTag);
		}
	}
}
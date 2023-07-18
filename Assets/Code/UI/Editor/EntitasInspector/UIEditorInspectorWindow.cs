using System.Linq;
using Code.Helpers.FileSystem;
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
	VisualTreeAsset entitasInspectorContextGroup;
	VisualTreeAsset entityRowVisualTree;
	VisualTreeAsset componentTagVisualTree;

	static readonly PathStr FolderPath = new PathStr("Assets/Code/Helpers/UI/EntitasInspector");
	static readonly PathStr EntitasInspectorPath = FolderPath / "EntitasInspector.uxml";
	static readonly PathStr EntitasInspectorContextGroupPath = FolderPath / "EntitasInspectorContextGroup.uxml";
	static readonly PathStr EntitasRowPath = FolderPath / "EntityRow.uxml";
	static readonly PathStr ComponentTagPath = FolderPath / "ComponentTag.uxml";

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

		// Load additional UI elements from a different UXML file
		entitasInspectorContextGroup = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(EntitasInspectorContextGroupPath);
		entityRowVisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(EntitasRowPath);
		componentTagVisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(ComponentTagPath);
		
		var inspectorContainer = mainUI.Q<VisualElement>("inspector");
		var imguiContainer = new IMGUIContainer(drawEntityInspector);
		inspectorContainer.Add(imguiContainer);
	}

	void Update() {
		var game = Contexts.sharedInstance.game;
		var entities = game.GetEntities();
		createContextGroup("Game", entities.upcast(default(IEntity[])));
	}

	void createContextGroup(string contextName, IEntity[] entities) {
		var foldout = new Foldout();
		foldout.text = contextName;
	}

	void drawEntityInspector() {
		// maybeSelectedGameEntity.IfSome(entity => maybeSelectedComponentIndex.Match(
		// 	componentIndex => drawComponent(entity, entity.GetComponents()[componentIndex], componentIndex),
		// 	() => EntityDrawer.DrawComponents(entity)
		// ));
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
		// rowsContainer.Clear();

		var contextGroup = entitasInspectorContextGroup.CloneTree();
		// foldout.contentContainer.Add(contextGroup);
		
		// Find the parent element in the main UI where you want to add the additional UI elements
		var rowsContainer = contextGroup.Q<ListView>("rows");

		// rowsContainer.itemsSource = entities;
		// rowsContainer.bindItem = bindItem;
		// rowsContainer.makeItem = makeItem;
		// rowsContainer.selectedIndicesChanged += ints => {
		// 	var data = Contexts.sharedInstance.game.GetEntities();
		// 	maybeSelectedGameEntity = ints.HeadOrNone().Map(index => data[index]);
		// 	maybeSelectedComponentIndex = None;
		// };

		VisualElement makeItem() => entityRowVisualTree.CloneTree();

		void bindItem(VisualElement e, int i) {
			// var entity = entities[i];
			// var entityName = e.Q<Label>("index");
			// entityName.text = $"#{entity.creationIndex}";
			//
			// var tagsContainer = e.Q<VisualElement>("tags");
			// createRow(tagsContainer, entity);
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
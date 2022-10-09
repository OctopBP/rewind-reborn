using System.IO;
using System.Linq;
using DesperateDevs.Extensions;
using Entitas;
using Entitas.CodeGeneration.Plugins;
using Jenny;

namespace Octop.ComponentModel.CodeGenerators;

public class EntityBuilderGenerator : AbstractGenerator {
    public override string Name => "ComponentBuilder";

    const string BUILDER_TEMPLATE =
        @"using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using Rewind.Services;

namespace Octop.ComponentModel {
    public class ${EntityType}EntityBuilder : MonoBehaviour, IEventListener {
        [SerializeReference, ValueDropdown(nameof(Models))] List<I${ContextName}ComponentModel> models = new();
        [SerializeReference, ValueDropdown(nameof(Listeners))] List<I${ContextName}ComponentListener> listeners = new();

	    ${ContextName}Entity entity;

		static IEnumerable Models = new ValueDropdownList<I${ContextName}ComponentModel>() {
${ModelItems}
        };

	    static IEnumerable Listeners = new ValueDropdownList<I${ContextName}ComponentListener>() {
${ListenersItems}
        };

        void Awake() {
		    entity = Contexts.sharedInstance.${contextName}.CreateEntity();
		    models.Aggregate(entity, (e, model) => model.Initialize(e));
	    }

        public void registerListeners() => listeners.ForEach(l => l.Register(entity));
        public void unregisterListeners() => listeners.ForEach(l => l.Unregister(entity));
    }
}";

    const string MODEL_ITEM = @"            { ""${DisplayName}"", new ${ModelType}() }";
    const string LISTENER_ITEM = @"            { ""${DisplayName}"", new ${ListenerType}() }";

    public override CodeGenFile[] Generate(CodeGeneratorData[] data) {
        var components = data.OfType<ComponentModelData>();
        var listeners = data.OfType<ComponentModelData>();
        
        return data
            .OfType<ContextData>()
            .Select(contextData => generate(
                contextData,
                components.Where(c => c.componentData.GetContextNames().Contains(contextData.GetContextName())).ToArray(),
                listeners.Where(c => c.componentData.GetContextNames().Contains(contextData.GetContextName())).ToArray()
            ))
            .ToArray();
    }

    CodeGenFile generate(ContextData data, ComponentModelData[] models, ComponentModelData[] listeners) =>
        generate(data.GetContextName(), models, listeners);

    CodeGenFile generate(string contextName, ComponentModelData[] models, ComponentModelData[] listeners) {
        var modelsNames = models.Select(component => MODEL_ITEM
            .Replace("${DisplayName}", component.componentData.GetTypeName().RemoveComponentSuffix().ToSpacedCamelCase())
            .Replace("${ModelType}", contextName + component.componentData.ComponentName() + "Model")
        );
        
        var listenersNames = listeners.Select(component => LISTENER_ITEM
            .Replace("${DisplayName}", component.componentData.GetTypeName().RemoveComponentSuffix().ToSpacedCamelCase())
            .Replace("${ListenerType}", contextName + component.componentData.ComponentName())
        );

        var fileContent = BUILDER_TEMPLATE
            .Replace("${ContextName}", contextName)
            .Replace("${contextName}", contextName.ToLowerFirst())
            .Replace("${EntityType}", contextName)
            .Replace("${ModelItems}", string.Join(",\n", modelsNames))
            .Replace("${ListenersItems}", string.Join(",\n", listenersNames));

        return new CodeGenFile(
            contextName + Path.DirectorySeparatorChar +
            "ModelsBuilder" + Path.DirectorySeparatorChar +
            contextName + "EntityBuilder" + ".cs",
            fileContent,
            GetType().FullName
        );
    }
}
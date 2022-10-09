using System.IO;
using System.Linq;
using DesperateDevs.Extensions;
using Entitas;
using Entitas.CodeGeneration.Plugins;
using Jenny;

namespace Octop.ComponentModel.CodeGenerators;

public class ComponentListenerGenerator : AbstractGenerator {
    public override string Name => "ComponentListener";

    const string TEMPLATE =
        @"using Rewind.Extensions;
using UnityEngine;
using Octop.ComponentModel;

public class ${ContextName}${ComponentName} : I${ContextName}ComponentListener, I${ComponentName} {
	public void Register(${EntityType} entity) => entity.Add${OnlyComponentName}Listener(this);
	public void Unregister(${EntityType} entity) => entity.Remove${OnlyComponentName}Listener(this);

    
	public void On${OnlyComponentName}(${EntityType} entity${Value}) { }
}";

    const string VALUE_TEMPLATE = ", ${ValueType} value";

    public override CodeGenFile[] Generate(CodeGeneratorData[] data) =>
        data
        // .OfType<ComponentListenerData>()
        .OfType<ComponentModelData>()
        .Where(d => d.test)
        .SelectMany(generate)
        .ToArray();

    CodeGenFile[] generate(ComponentModelData data) => data.componentData.GetContextNames()
        .Select(contextName => generate(contextName, data))
        .ToArray();

    CodeGenFile generate(string contextName, ComponentModelData data) {
        var fileContent = TEMPLATE
            .Replace("${Value}", VALUE_TEMPLATE)
            .Replace("${ComponentName}", data.componentData.ComponentName())
            .Replace("${OnlyComponentName}", data.componentData.ComponentName().RemoveListenerSuffix())
            .Replace("${componentName}", data.componentData.ComponentName().ToLowerFirst())
            .Replace("${ContextName}", contextName)
            .Replace("${ValueType}", "TODO")
            .Replace("${EntityType}", contextName.AddEntitySuffix());

        // var fileContent = TEMPLATE
        //     .Replace("${methodParameters}", data.GetEventMethodArgs(eventData, $", {memberData.GetMethodParameters(false)}"))
        //     .Replace(data, contextName, eventData);

        return new CodeGenFile(
            contextName + Path.DirectorySeparatorChar +
            "ModelsBuilder" + Path.DirectorySeparatorChar +
            "Listeners" + Path.DirectorySeparatorChar +
            contextName + data.componentData.ComponentName() + ".cs",
            fileContent,
            GetType().FullName
        );
    }
}
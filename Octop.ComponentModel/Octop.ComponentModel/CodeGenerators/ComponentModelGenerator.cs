using System.IO;
using System.Linq;
using DesperateDevs.Extensions;
using Entitas;
using Entitas.CodeGeneration.Plugins;
using Jenny;

namespace Octop.ComponentModel.CodeGenerators;

public class ComponentModelGenerator : AbstractGenerator {
    public override string Name => "ComponentModel (Attribute)";

    const string STANDARD_MODEL_TEMPLATE =
        @"using Rewind.Extensions;
using UnityEngine;
using Octop.ComponentModel;

public class ${ComponentName}ModelBehaviour : MonoBehaviour, I${ContextName}ComponentModel {
    [SerializeField] ${ComponentName} ${componentName};

    public ${EntityType} Initialize(${EntityType} entity) => entity.with(e => e.Add${ComponentName}(${componentName}));
}";

    const string FLAG_MODEL_TEMPLATE =
        @"using Rewind.Extensions;
using UnityEngine;
using Octop.ComponentModel;

public class ${ComponentName}ModelBehaviour : MonoBehaviour, I${ContextName}ComponentModel {
    public ${EntityType} Initialize(${EntityType} entity) => entity.with(e => e.is${ComponentName} = true);
}";

    public override CodeGenFile[] Generate(CodeGeneratorData[] data) => data
        .OfType<ComponentModelData>()
        .SelectMany(generate)
        .ToArray();

    CodeGenFile[] generate(ComponentModelData data) => data.componentData.GetContextNames()
        .Select(contextName => generate(contextName, data))
        .ToArray();

    CodeGenFile generate(string contextName, ComponentModelData data) {
        var template = data.componentType == ComponentModelData.ComponentType.FlagComponent
            ? FLAG_MODEL_TEMPLATE
            : STANDARD_MODEL_TEMPLATE;

        var fileContent = template
            .Replace("${ComponentName}", data.componentData.ComponentName())
            .Replace("${componentName}", data.componentData.ComponentName().ToLowerFirst())
            .Replace("${ContextName}", contextName)
            .Replace("${EntityType}", contextName.AddEntitySuffix());

        return new CodeGenFile(
            contextName + Path.DirectorySeparatorChar +
            "Models" + Path.DirectorySeparatorChar +
            data.componentData.ComponentName() + "ModelBehaviour" + ".cs",
            fileContent,
            GetType().FullName
        );
    }
}
using System.IO;
using System.Linq;
using Entitas.CodeGeneration.Plugins;
using Jenny;

namespace Octop.ComponentModel.CodeGenerators;

public class ComponentModelGenerator : AbstractGenerator {
    public override string Name => "ComponentModel";

    const string STANDARD_MODEL_TEMPLATE =
@"using Rewind.Extensions;
using UnityEngine;
using Octop.ComponentModel;

public class ${OptionalContextName}${ComponentName}Model : I${ContextName}ComponentModel {
${SerializeFields}

    public ${EntityType} Initialize(${EntityType} entity) => entity.with(e => e.Add${ComponentName}(${methodArgs}));
}";

    const string FLAG_MODEL_TEMPLATE =
@"using Rewind.Extensions;
using UnityEngine;
using Octop.ComponentModel;

public class ${OptionalContextName}${ComponentName}Model : I${ContextName}ComponentModel {
    public ${EntityType} Initialize(${EntityType} entity) => entity.with(e => e.is${ComponentName} = true);
}";

    const string SERIALIZE_FIELD = "    [SerializeField] ${ComponentType} ${componentName};";

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

        var optionalContextName = data.componentData.GetContextNames().Length > 1 ? contextName : string.Empty;

        var fileContent = template
            .Replace("${SerializeFields}", string.Join("\n", data.componentData.GetMemberData()
                .Select(memberData => SERIALIZE_FIELD
                    .Replace("${ComponentType}", memberData.type)
                    .Replace("${componentName}", memberData.name)
                )
            ))
            .Replace(data.componentData, contextName)
            .Replace("${OptionalContextName}", optionalContextName);

        return new CodeGenFile(
            contextName + Path.DirectorySeparatorChar +
            "ModelsBuilder" + Path.DirectorySeparatorChar +
            "Models" + Path.DirectorySeparatorChar +
            optionalContextName + data.componentData.ComponentName() + "Model" + ".cs",
            fileContent,
            GetType().FullName
        );
    }
}
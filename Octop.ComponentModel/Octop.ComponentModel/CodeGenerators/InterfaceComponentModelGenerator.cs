using System.IO;
using System.Linq;
using Entitas;
using Entitas.CodeGeneration.Plugins;
using Jenny;

namespace Octop.ComponentModel.CodeGenerators;

public class InterfaceComponentModelGenerator : AbstractGenerator {
    public override string Name => "ComponentBuilder (Interface)";

const string BUILDER_INTERFACE_TEMPLATE =
        @"namespace Octop.ComponentModel {
	public interface I${ContextName}ComponentModel {
		${EntityType} Initialize(${EntityType} entity);
	}
}";

    public override CodeGenFile[] Generate(CodeGeneratorData[] data) => data
        .OfType<ContextData>()
        .Select(generate)
        .ToArray();

    CodeGenFile generate(ContextData data) => generate(data.GetContextName());

    CodeGenFile generate(string contextName) {
        var fileContent = BUILDER_INTERFACE_TEMPLATE
            .Replace("${ContextName}", contextName)
            .Replace("${EntityType}", contextName.AddEntitySuffix());

        return new CodeGenFile(
            contextName + Path.DirectorySeparatorChar +
            "Models" + Path.DirectorySeparatorChar +
            "I" + contextName + "Entity" + "ComponentModel" + ".cs",
            fileContent,
            GetType().FullName
        );
    }
}
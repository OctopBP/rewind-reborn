using System.IO;
using System.Linq;
using Entitas;
using Entitas.CodeGeneration.Plugins;
using Jenny;

namespace Octop.ComponentModel.CodeGenerators;

public class InterfaceComponentListenerGenerator : AbstractGenerator {
    public override string Name => "ComponentBuilder (Interface)";

    const string BUILDER_INTERFACE_TEMPLATE =
@"namespace Octop.ComponentModel {
	public interface I${ContextName}ComponentListener {
		void Register(${EntityType} entity);
		void Unregister(${EntityType} entity);
        void Validate();
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
            "ModelsBuilder" + Path.DirectorySeparatorChar +
            "I" + contextName + "ComponentListener" + ".cs",
            fileContent,
            GetType().FullName
        );
    }
}
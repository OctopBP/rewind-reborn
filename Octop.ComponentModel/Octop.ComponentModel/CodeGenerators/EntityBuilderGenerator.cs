using System.IO;
using System.Linq;
using DesperateDevs.Extensions;
using Entitas.CodeGeneration.Plugins;
using Jenny;

namespace Octop.ComponentModel.CodeGenerators;

public class EntityBuilderGenerator : AbstractGenerator {
    public override string Name => "ComponentBuilder";

    const string BUILDER_TEMPLATE =
        @"using UnityEngine;
using System.Linq;

namespace Octop.ComponentModel {
    public class ${EntityType}Builder : MonoBehaviour {
	    void Start() {
		    var entity = Contexts.sharedInstance.${contextName}.CreateEntity();
		    var models = GetComponents<I${ContextName}ComponentModel>();
		    models.Aggregate(entity, (e, model) => model.Initialize(e));
	    }
    }
}";

    public override CodeGenFile[] Generate(CodeGeneratorData[] data) => data
        .OfType<ContextData>()
        .Select(generate)
        .ToArray();

    CodeGenFile generate(ContextData data) => generate(data.GetContextName());

    CodeGenFile generate(string contextName) {
        var fileContent = BUILDER_TEMPLATE
            .Replace("${ContextName}", contextName)
            .Replace("${contextName}", contextName.ToLowerFirst())
            .Replace("${EntityType}", contextName + "Entity");

        return new CodeGenFile(
            contextName + Path.DirectorySeparatorChar +
            "Models" + Path.DirectorySeparatorChar +
            contextName + "EntityBuilder" + ".cs",
            fileContent,
            GetType().FullName
        );
    }
}
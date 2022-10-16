using System.IO;
using System.Linq;
using Entitas.CodeGeneration.Attributes;
using Entitas.CodeGeneration.Plugins;
using Jenny;

namespace Octop.ComponentModel.CodeGenerators;

public class ComponentListenerGenerator : AbstractGenerator {
    const string TEMPLATE =
@"using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Octop.ComponentModel;

public class ${EventListener} : I${ContextName}ComponentListener, I${EventListener} {
    [SerializeField] List<Component> listeners = new();

	public void Validate() {
		listeners = listeners.Where(m => m is I${EventListener}).ToList();
	}

	public void Register(${EntityType} entity) => entity.Add${EventListener}(this);
	public void Unregister(${EntityType} entity) => entity.Remove${EventListener}(this);

	public void On${EventComponentName}${EventType}(${methodParameters}) =>
		listeners.ForEach(l => ((I${EventListener}) l).On${EventComponentName}${EventType}(${methodArgs}));
}";

    public override string Name => "ComponentListener";

    public override CodeGenFile[] Generate(CodeGeneratorData[] data) => data
        .OfType<ComponentData>()
        .Where(d => d.IsEvent())
        .SelectMany(generate)
        .ToArray();

    CodeGenFile[] generate(ComponentData data) => data
        .GetContextNames()
        .SelectMany(contextName => generate(contextName, data))
        .ToArray();

    CodeGenFile[] generate(string contextName, ComponentData data) => data
        .GetEventData()
        .Select(eventData => {
            var memberData = data.GetMemberData();
            var maybeSeparator = memberData.Length > 0 && eventData.eventType == EventType.Added ? ", " : string.Empty;

            var methodParameters = eventData.eventType == EventType.Added
                ? data.GetEventMethodArgs(eventData, memberData.GetMethodParameters(false))
                : string.Empty;

            var methodArgs = eventData.eventType == EventType.Added
                ? CodeGeneratorExtensions.GetMethodArgs(data.GetMemberData(), false)
                : string.Empty;

            var fileContent = TEMPLATE
                .Replace("${methodParameters}", string.Join(maybeSeparator, "${EntityType} entity", methodParameters))
                .Replace("${methodArgs}", string.Join(maybeSeparator, "entity", methodArgs))
                .Replace(data, contextName, eventData);

            return new CodeGenFile(
                contextName + Path.DirectorySeparatorChar +
                "ModelsBuilder" + Path.DirectorySeparatorChar +
                "Listeners" + Path.DirectorySeparatorChar +
                data.EventListener(contextName, eventData) + ".cs",
                fileContent,
                GetType().FullName
            );
        }).ToArray();
}
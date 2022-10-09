using System;
using System.Collections.Generic;
using System.Linq;
using Jenny;
using Jenny.Plugins;
using DesperateDevs.Extensions;
using DesperateDevs.Roslyn;
using DesperateDevs.Serialization;
using Entitas;
using Entitas.CodeGeneration.Plugins;
using Microsoft.CodeAnalysis;
using Octop.ComponentModel.Attribute;
using ComponentDataProvider = Entitas.Roslyn.CodeGeneration.Plugins.ComponentDataProvider;

namespace Octop.ComponentModel.DataProvider;

public class ComponentModelDataProvider : IDataProvider, IConfigurable, ICachable {
    public string Name => "ComponentModel (DataProvider)";
    public int Order => 0;
    public bool RunInDryMode => true;

    public Dictionary<string, string> DefaultProperties => projectPathConfig.DefaultProperties;

    public Dictionary<string, object> ObjectCache { get; set; }

    readonly ProjectPathConfig projectPathConfig = new ProjectPathConfig();
    readonly INamedTypeSymbol[] types;

    Preferences preferences;
    ComponentDataProvider componentDataProvider;

    public ComponentModelDataProvider() : this(null) { }

    public ComponentModelDataProvider(INamedTypeSymbol[] types) {
        this.types = types;
    }

    public void Configure(Preferences preferences) {
        this.preferences = preferences;
        projectPathConfig.Configure(preferences);
    }

    static (ComponentModelData.ComponentType type, ComponentModelData.FieldInfo[] fieldInfos) getComponentType(
        INamedTypeSymbol type
    ) {
        var publicMembers = type.GetPublicMembers(true);
        return publicMembers.Length == 0
            ? (ComponentModelData.ComponentType.FlagComponent, Array.Empty<ComponentModelData.FieldInfo>())
            : (ComponentModelData.ComponentType.StandardComponent, publicMembers
                .Select(m => (space: m.PublicMemberType().ContainingNamespace, name: m.PublicMemberType().ToString() ?? ""))
                .Select(tpl => (fieldNamespace: tpl.space.IsGlobalNamespace ? "" : tpl.space.ToString() ?? "", tpl.name))
                .Select(tpl => new ComponentModelData.FieldInfo(tpl.fieldNamespace, tpl.name))
                .ToArray());
    }

    public CodeGeneratorData[] GetData() {
        var types = this.types ?? Jenny.Plugins.Roslyn.PluginUtil
            .GetCachedProjectParser(ObjectCache, projectPathConfig.ProjectPath)
            .GetTypes();

        var componentInterface = typeof(IComponent).ToCompilableString();

        var componentType = types
            .Where(type => type.AllInterfaces.Any(i => i.ToCompilableString() == componentInterface))
            .Where(type => !type.IsAbstract)
            .Where(type => type.GetAttribute<ComponentModelAttribute>() != null)
            .ToArray();

        var componentTypeLookup = componentType.ToDictionary(type => type.ToCompilableString(), getComponentType);

        componentDataProvider = new ComponentDataProvider(componentType);
        componentDataProvider.Configure(preferences);

        return componentDataProvider
            .GetData()
            .Where(data => !((ComponentData) data).GetTypeName().RemoveComponentSuffix().HasListenerSuffix())
            .Select(data => (data, info: componentTypeLookup[((ComponentData) data).GetTypeName()]))
            .Select(tpl => new ComponentModelData(tpl.data, componentType: tpl.info.type, fieldsInfo: tpl.info.fieldInfos))
            .ToArray();
    }
}


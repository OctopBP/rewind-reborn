using Entitas.CodeGeneration.Plugins;
using Jenny;

namespace Octop.ComponentModel;

public class ComponentModelData : CodeGeneratorData {
    public enum ComponentType {
        StandardComponent,
        FlagComponent
    }

    public readonly ComponentType componentType;
    public readonly string[] usings;
    public ComponentData componentData { get; }
    
    public ComponentModelData(CodeGeneratorData data, ComponentType componentType, string[] usings) : base(data) {
        componentData = new ComponentData(data);
        this.componentType = componentType;
        this.usings = usings;
    }
}


using Entitas.CodeGeneration.Plugins;
using Jenny;

namespace Octop.ComponentModel;

public class ComponentModelData : CodeGeneratorData {
    public enum ComponentType {
        StandardComponent,
        FlagComponent
    }

    public readonly ComponentType componentType;
    public ComponentData componentData { get; }
    
    public ComponentModelData(CodeGeneratorData data, ComponentType componentType) : base(data) {
        componentData = new ComponentData(data);
        this.componentType = componentType;
    }
}
using Entitas.CodeGeneration.Plugins;
using Jenny;

namespace Octop.ComponentModel;

public class ComponentModelData : CodeGeneratorData {
    public enum ComponentType {
        StandardComponent,
        FlagComponent
    }

    public class FieldInfo {
        public readonly string fieldNamespace;
        public readonly string typeName;

        public FieldInfo(string fieldNamespace, string typeName) {
            this.fieldNamespace = fieldNamespace;
            this.typeName = typeName;
        }
    }

    public readonly ComponentType componentType;
    public readonly FieldInfo[] fieldsInfo;
    public ComponentData componentData { get; }
    
    public ComponentModelData(CodeGeneratorData data, ComponentType componentType, FieldInfo[] fieldsInfo) : base(data) {
        componentData = new ComponentData(data);
        this.componentType = componentType;
        this.fieldsInfo = fieldsInfo;
    }
}


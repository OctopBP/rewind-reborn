using Entitas.CodeGeneration.Plugins;
using Jenny;

namespace Octop.ComponentModel;

public class ComponentListenerData : CodeGeneratorData {
    public ComponentData componentData { get; }
    
    public ComponentListenerData(CodeGeneratorData data) : base(data) {
        componentData = new ComponentData(data);
    }
}


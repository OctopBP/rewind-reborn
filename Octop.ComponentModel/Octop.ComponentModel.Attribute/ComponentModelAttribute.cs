using System;

namespace Octop.ComponentModel.Attribute;

[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum)]
public class ComponentModelAttribute : System.Attribute {
    public ComponentModelAttribute() { }
}
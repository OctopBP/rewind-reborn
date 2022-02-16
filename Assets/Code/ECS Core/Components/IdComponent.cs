using System;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public class IdComponent : IComponent {
	[PrimaryEntityIndex] public Guid value;
}
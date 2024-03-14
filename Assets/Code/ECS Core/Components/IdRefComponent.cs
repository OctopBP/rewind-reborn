using System;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public class IdRefComponent : IComponent
{
	[EntityIndex] public Guid value;
}
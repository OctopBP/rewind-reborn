using System;
using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public class IdComponent : IComponent {
	public List<Link> value;

	public class Link {
		[EntityIndex] public Guid value;
		public string type;
	}
}
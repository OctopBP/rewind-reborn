using System;
using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public class PointLinksComponent : IComponent {
	public List<Link> value;

	public class Link {
		public enum LinkType { Left, Right }

		[EntityIndex] public Guid id;
		public LinkType type;

		public Link(Guid id, LinkType type) {
			this.id = id;
			this.type = type;
		}
	}
}
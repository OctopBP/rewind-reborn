using System;
using System.Collections.Generic;
using Entitas.CodeGeneration.Attributes;

public class Link {
	public enum LinkType { Left, Right }

	[EntityIndex] public Guid id;
	public LinkType type;

	public Link(Guid id, LinkType type) {
		this.id = id;
		this.type = type;
	}
}

[Game]
public class PointLinksComponent : ValueComponent<List<Link>> { }
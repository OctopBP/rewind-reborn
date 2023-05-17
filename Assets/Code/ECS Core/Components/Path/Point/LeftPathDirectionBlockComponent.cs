using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.SharedData;

[Game, Event(EventTarget.Self)]
public class LeftPathDirectionBlocksComponent : IComponent {
	public List<PoinLeftPathDirectionBlock> value;
}

public static class LeftPathDirectionBlocksComponentExt {
	public static void removeAllByGuid(this LeftPathDirectionBlocksComponent comp, Guid guid) {
		comp.value = comp.value.Where(b => b._blocker != guid).ToList();
	}
	
	public static void replaceWithBlockToRight(this LeftPathDirectionBlocksComponent comp, Guid guid) {
		comp.value = comp.value
			.Where(b => b._blocker != guid)
			.Append(PoinLeftPathDirectionBlock.blockToRight(guid))
			.ToList();
	}
	
	public static void replaceWithBlockToLeft(this LeftPathDirectionBlocksComponent comp, Guid guid) {
		comp.value = comp.value
			.Where(b => b._blocker != guid)
			.Append(PoinLeftPathDirectionBlock.blockToLeft(guid))
			.ToList();
	}
	
	public static void replaceWithBlockBoth(this LeftPathDirectionBlocksComponent comp, Guid guid) {
		comp.value = comp.value
			.Where(b => b._blocker != guid)
			.Append(PoinLeftPathDirectionBlock.blockToRight(guid))
			.Append(PoinLeftPathDirectionBlock.blockToLeft(guid))
			.ToList();
	}
}
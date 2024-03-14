using System;
using System.Collections.Generic;
using System.Linq;
using Entitas.CodeGeneration.Attributes;
using Rewind.SharedData;

[Game, Event(EventTarget.Self)]
public class LeftPathDirectionBlocksComponent : ValueComponent<List<PoinLeftPathDirectionBlock> > { }

public static class LeftPathDirectionBlocksComponentExt
{
	public static void RemoveAllByGuid(this LeftPathDirectionBlocksComponent comp, Guid guid)
	{
		comp.value = comp.value.Where(b => b._blocker != guid).ToList();
	}
	
	public static void ReplaceWithBlockToRight(this LeftPathDirectionBlocksComponent comp, Guid guid)
	{
		comp.value = comp.value
			.Where(b => b._blocker != guid)
			.Append(PoinLeftPathDirectionBlock.BlockToRight(guid))
			.ToList();
	}
	
	public static void ReplaceWithBlockToLeft(this LeftPathDirectionBlocksComponent comp, Guid guid)
	{
		comp.value = comp.value
			.Where(b => b._blocker != guid)
			.Append(PoinLeftPathDirectionBlock.BlockToLeft(guid))
			.ToList();
	}
	
	public static void ReplaceWithBlockBoth(this LeftPathDirectionBlocksComponent comp, Guid guid)
	{
		comp.value = comp.value
			.Where(b => b._blocker != guid)
			.Append(PoinLeftPathDirectionBlock.BlockToRight(guid))
			.Append(PoinLeftPathDirectionBlock.BlockToLeft(guid))
			.ToList();
	}
}
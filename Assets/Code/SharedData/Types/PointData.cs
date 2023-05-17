using System;
using System.Collections.Generic;
using ExhaustiveMatching;
using Rewind.SharedData;
using UnityEngine;

[Serializable, GenConstructor]
public partial class PointData {
	public Vector2 localPosition;
	public UnityLeftPathDirectionBlock leftPathStatus;
	public int depth;
}

[Flags]
public enum UnityLeftPathDirectionBlock {
	None = 0,
	BlockToRight = 1,
	BlockToLeft = 2,
	Both = BlockToRight + BlockToLeft
}

public static class UnityLeftPathDirectionBlockExt {
	public static List<PoinLeftPathDirectionBlock> toBlockList(
		this UnityLeftPathDirectionBlock unityLeftPathDirectionBlock, Guid guid
	) => unityLeftPathDirectionBlock switch {
		UnityLeftPathDirectionBlock.None => new List<PoinLeftPathDirectionBlock>(),
		UnityLeftPathDirectionBlock.BlockToRight => new() {
			PoinLeftPathDirectionBlock.blockToRight(guid)
		},
		UnityLeftPathDirectionBlock.BlockToLeft => new() {
			PoinLeftPathDirectionBlock.blockToLeft(guid)
		},
		UnityLeftPathDirectionBlock.Both => new() {
			PoinLeftPathDirectionBlock.blockToRight(guid),
			PoinLeftPathDirectionBlock.blockToLeft(guid)
		},
		_ => throw ExhaustiveMatch.Failed(unityLeftPathDirectionBlock)
	};
	
	public static void fold(
		this UnityLeftPathDirectionBlock self, Action onNone = null, Action onBlockToRight = null,
		Action onBlockToLeft = null, Action onBoth = null
	) {
		switch (self) {
			case UnityLeftPathDirectionBlock.None: onNone?.Invoke(); return;
			case UnityLeftPathDirectionBlock.BlockToRight: onBlockToRight?.Invoke(); return;
			case UnityLeftPathDirectionBlock.BlockToLeft: onBlockToLeft?.Invoke(); return;
			case UnityLeftPathDirectionBlock.Both: onBoth?.Invoke(); return;
			default: throw ExhaustiveMatch.Failed(self);
		}
	}
}
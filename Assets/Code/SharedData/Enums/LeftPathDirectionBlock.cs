using System;

namespace Rewind.SharedData {
	[GenConstructor]
	public partial class PoinLeftPathDirectionBlock {
		public enum LeftPathDirectionBlock {
			BlockToRight,
			BlockToLeft
		}

		public static PoinLeftPathDirectionBlock blockToLeft(Guid blocker) =>
			new PoinLeftPathDirectionBlock(blocker, LeftPathDirectionBlock.BlockToLeft);

		public static PoinLeftPathDirectionBlock blockToRight(Guid blocker) =>
			new PoinLeftPathDirectionBlock(blocker, LeftPathDirectionBlock.BlockToRight);
	
		[PublicAccessor] readonly Guid blocker;
		[PublicAccessor] readonly LeftPathDirectionBlock leftPathDirectionBlock;
	}
}

using System;

namespace Rewind.SharedData
{
	[GenConstructor]
	public partial class PoinLeftPathDirectionBlock
    {
		public enum LeftPathDirectionBlock
        {
			BlockToRight,
			BlockToLeft
		}

		public static PoinLeftPathDirectionBlock BlockToLeft(Guid blocker) =>
			new PoinLeftPathDirectionBlock(blocker, LeftPathDirectionBlock.BlockToLeft);

		public static PoinLeftPathDirectionBlock BlockToRight(Guid blocker) =>
			new PoinLeftPathDirectionBlock(blocker, LeftPathDirectionBlock.BlockToRight);
	
		[PublicAccessor] private readonly Guid blocker;
		[PublicAccessor] private readonly LeftPathDirectionBlock leftPathDirectionBlock;
	}
}

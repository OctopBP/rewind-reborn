using System;

namespace Rewind.SharedData
{
	public enum CharacterLookDirection : short { Left, Right }
	
	public static class CharacterLookDirectionExt
	{
		public static int Value(this CharacterLookDirection self) => self switch
		{
			CharacterLookDirection.Left => -1,
			CharacterLookDirection.Right => 1,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
	}
}

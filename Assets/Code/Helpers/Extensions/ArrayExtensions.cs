using System.Collections.Generic;
using System.Linq;
using LanguageExt;

namespace Rewind.Extensions
{
	public static class ArrayExtensions
	{
		public static bool IsEmpty<T>(this T[] array) => array.Length == 0;
		public static bool IsEmpty<T>(this IEnumerable<T> array) => !array.Any();
		
		public static Option<T> At<T>(this T[] array, int index) =>
			index < 0 || index >= array.Length ? Option<T>.None : array[index];
	}
}




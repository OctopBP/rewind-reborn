using LanguageExt;

namespace Rewind.Extensions {
	public static class ArrayExtensions {
		public static Option<T> at<T>(this T[] array, int index) =>
			index < 0 || index >= array.Length ? Option<T>.None : array[index];
	}
}




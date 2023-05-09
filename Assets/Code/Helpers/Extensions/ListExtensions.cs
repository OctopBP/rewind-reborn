using System.Collections.Generic;
using LanguageExt;

namespace Rewind.Extensions {
	public static class ListExtensions {
		public static Option<T> at<T>(this List<T> list, int index) =>
			index < 0 || index >= list.Count ? Option<T>.None : list[index];
	}
}




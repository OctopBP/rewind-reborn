using System;
using Code.Helpers;
using UnityEngine;

namespace Rewind.Extensions
{
	public static class ColorExtensions
	{
		public static Color RandomColorForGuid(Guid guid)
		{
			var hash = guid.GetHashCode();
			
			var colors = new[]
			{
				ColorA.Red, ColorA.Orange, ColorA.Yellow, ColorA.Green,
				ColorA.Mint, ColorA.Teal, ColorA.Cyan, ColorA.Blue,
				ColorA.Indigo, ColorA.Purple, ColorA.Pink
			};
			
			return colors[Math.Abs(hash) % colors.Length];
		}
	}
}




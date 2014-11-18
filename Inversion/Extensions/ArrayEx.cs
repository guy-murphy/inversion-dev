using System;

namespace Inversion.Extensions {
	
	/// <summary>
	/// An extension class providing extensions for arrays.
	/// </summary>
	public static class ArrayEx {
		/// <summary>
		/// A simple extension that constructs a new array from one
		/// provided by calling `.Clone()` on each element of the
		/// provided array.
		/// </summary>
		/// <typeparam name="T">The type of the array elements.</typeparam>
		/// <param name="self">The array to act upon.</param>
		/// <returns>Provides a new array with elements cloned from the originating array.</returns>
		public static T[] DeepClone<T>(this T[] self) where T : class, ICloneable {
			T[] other = new T[self.Length];
			for (int i = 0; i < self.Length; i++) {
				other[i] = self[i].Clone() as T;
			}
			return other;
		}
	}
}

using System;

namespace Inversion.Data {
	// Move this to IEnumerable, it has no need
	// to be bound against array
	public static class ArrayEx {
		public static T[] DeepClone<T>(this T[] self) where T : class, ICloneable {
			T[] other = new T[self.Length];
			for (int i = 0; i < self.Length; i++) {
				other[i] = self[i].Clone() as T;
			}
			return other;
		}
	}
}

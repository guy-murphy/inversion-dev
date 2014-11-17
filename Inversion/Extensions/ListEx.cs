using System.Collections.Generic;

namespace Inversion.Extensions {

	/// <summary>
	/// Utility extension methods provided for lists.
	/// </summary>
	/// <remarks>
	/// Just some methods to allow a list to be treated as a stack.
	/// If a stack is being used as a context in tree processing,
	/// sometimes being able to peek at more than the last element,
	/// or also treat the stack like a list is useful.
	/// </remarks>

	public static class ListEx {

		/// <summary>
		/// Pushes an elelent onto the list as if it were a stack.
		/// </summary>
		/// <typeparam name="T">The type of the list elements.</typeparam>
		/// <param name="self">The list being acted on.</param>
		/// <param name="item">The element being pushed onto the list.</param>
		/// <returns>The list being acted on.</returns>

		public static List<T> Push<T>(this List<T> self, T item) {
			self.Add(item);
			return self;
		}

		/// <summary>
		/// Pops an element from the list as if it were a stack.
		/// </summary>
		/// <typeparam name="T">The type of the list elements.</typeparam>
		/// <param name="self">The list being acted on.</param>
		/// <returns>The element that was poped.</returns>

		public static T Pop<T>(this List<T> self) {
			T last = self[self.Count - 1];
			self.RemoveAt(self.Count - 1);
			return last;
		}

		/// <summary>
		/// Provides an index of the list in reverse order,
		/// with `list.Peek(0)` considering the last element
		/// of the list, and `list.Peek(1)` being the penultimate
		/// element of the list. No bounds checking is provided.
		/// </summary>
		/// <typeparam name="T">The type of the list elements.</typeparam>
		/// <param name="self">The list being acted on.</param>
		/// <param name="i">The index of the peek.</param>
		/// <returns>The element found at the index.</returns>

		public static T Peek<T>(this List<T> self, int i) {
			return self[self.Count - (1 + i)];
		}

		/// <summary>
		/// Takes a look at the last element of a list without removing it,
		/// as if it were a stack.
		/// </summary>
		/// <typeparam name="T">The type of the list elements.</typeparam>
		/// <param name="self">The list being acted on.</param>
		/// <returns>The last element of the list.</returns>

		public static T Peek<T>(this List<T> self) {
			return self.Peek(0);
		}

	}
}

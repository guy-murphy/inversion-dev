using System;
using System.Text;

namespace Inversion.Extensions {

	/// <summary>
	/// Some utility extension methods provided for string builders.
	/// </summary>
	public static class StringBuilderEx {

		/// <summary>
		/// Filters a `StringBuilder`, removing any elements
		/// that the provided predicate returns true for.
		/// </summary>
		/// <param name="self">The string builder being acted upon.</param>
		/// <param name="test">The predicate to test each element for removal.</param>
		/// <returns>The string builder being acted upon.</returns>

		public static StringBuilder Filter(this StringBuilder self, Predicate<char> test) {
			for (int i = 0; i < self.Length; i++) {
				if (!test(self[i])) {
					self.Remove(i, 1);
				}
			}
			return self;
		}

		/// <summary>
		/// Removes all non-numeric characters from the string builder.
		/// </summary>
		/// <param name="self">The string builder being acted upon.</param>
		/// <returns>The string builder being acted upon.</returns>

		public static StringBuilder RemoveNonNumeric(this StringBuilder self) {
			return self.Filter(c => char.IsNumber(c));
		}

		/// <summary>
		/// Removes all the non-alphabetic characters from the string builder.
		/// </summary>
		/// <param name="self">The string builder being acted upon.</param>
		/// <returns>The string builder being acted upon.</returns>

		public static StringBuilder RemoveNonAlpha(this StringBuilder self) {
			return self.Filter(c => char.IsLetter(c));
		}

		/// <summary>
		/// Removes all non-alphanumeric characters from the string builder.
		/// </summary>
		/// <param name="self">The string builder being acted upon.</param>
		/// <returns>The string builder being acted upon.</returns>

		public static StringBuilder RemoveNonAlphaNumeric(this StringBuilder self) {
			return self.Filter(c => char.IsLetterOrDigit(c));
		}

		/// <summary>
		/// Removes all whitespace from the string builder.
		/// </summary>
		/// <param name="self">The string builder being acted upon.</param>
		/// <returns>The string builder being acted upon.</returns>

		public static StringBuilder RemoveWhitespace(this StringBuilder self) {
			return self.Filter(c => char.IsWhiteSpace(c));
		}

		/// <summary>
		/// Removes a specified number of characters from the left-side
		/// of the string builder.
		/// </summary>
		/// <param name="self">The string builder being acted upon.</param>
		/// <param name="amount"></param>
		/// <returns>The string builder being acted upon.</returns>

		public static StringBuilder TrimLeftBy(this StringBuilder self, int amount) {
			return self.Remove(0, amount);
		}

		/// <summary>
		/// Removes a specified number of characters from the right-side
		/// of the string builder.
		/// </summary>
		/// <param name="self">The string builder being acted upon.</param>
		/// <param name="amount"></param>
		/// <returns>The string builder being acted upon.</returns>
		public static StringBuilder TrimRightBy(this StringBuilder self, int amount) {
			return self.Remove(self.Length - (amount + 1), amount);
		}

		/// <summary>
		/// Removes a specified number of characters from each end
		/// of the string builder.
		/// </summary>
		/// <param name="self">The string builder being acted upon.</param>
		/// <param name="amount"></param>
		/// <returns>The string builder being acted upon.</returns>
		public static StringBuilder TrimEndsBy(this StringBuilder self, int amount) {
			return self.TrimLeftBy(amount).TrimRightBy(amount);
		}

	}
}

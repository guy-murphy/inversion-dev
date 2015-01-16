using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace Inversion.Extensions {
	/// <summary>
	/// An extension class providing extensions for string.
	/// </summary>
	public static class StringEx {

		private static readonly Regex _xmlName = new Regex("[_:A-Za-z][-._:A-Za-z0-9]*");

		/// <summary>
		/// Determines if the string is not null
		/// and has a length greater than zero.
		/// </summary>
		/// <param name="self">The subject of extension.</param>
		/// <returns>
		/// Returns <b>true</b> if the string has a values;
		/// otherwise returns <b>false</b>.
		/// </returns>

		public static bool HasValue(this string self) {
			return !String.IsNullOrEmpty(self);
		}

		/// <summary>
		/// Checks if a string has a value and if not
		/// throws an <see cref="ArgumentNullException"/>.
		/// </summary>
		/// <param name="self">The subject of extension.</param>
		/// <param name="message">
		/// The message to use as part of the exception.
		/// </param>
		/// <seealso cref="HasValue"/>

		public static void AssertHasValue(this string self, string message) {
			if (!self.HasValue()) throw new ArgumentNullException(message);
		}

		/// <summary>
		/// Places the 
		/// </summary>
		/// <param name="self"></param>
		/// <param name="number"></param>
		/// <param name="character"></param>
		/// <returns></returns>

		public static string Prepend(this string self, int number, char character) {
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < number; i++) {
				sb.Append(character);
			}
			sb.Append(self);
			return sb.ToString();
		}

		/// <summary>
		/// Determines if a string is a valid XML tag name.
		/// </summary>
		/// <param name="self">The subject of extension.</param>
		/// <returns>
		/// Returns <b>true</b> if the string is a valid XML name;
		/// otherwise, returns <b>false</b>.
		/// </returns>

		public static bool IsXmlName(this string self) {
			return _xmlName.IsMatch(self);
		}

		/// <summary>
		/// Filters out characters from string by testing them with a predicate.
		/// </summary>
		/// <param name="self">The string to act upon.</param>
		/// <param name="test">The predicate to test each character with.</param>
		/// <returns>Returns a new string containing only those charcters for which the test returned true.</returns>
		public static string Filter(this string self, Predicate<char> test) {
			StringBuilder sb = new StringBuilder();
			foreach (char c in self) {
				if (test(c)) {
					sb.Append(c);
				}
			}
			return sb.ToString();
		}

		/// <summary>
		/// Produces a new string by removing all non-numeric characters from the sting provided.
		/// </summary>
		/// <param name="self">The string to act upon.</param>
		/// <returns>The new filtered string.</returns>
		public static string RemoveNonNumeric(this string self) {
			return self.Filter(c => !char.IsNumber(c));
		}

		/// <summary>
		/// Produces a new string by removing all alphabetic characters from the sting provided.
		/// </summary>
		/// <param name="self">The string to act upon.</param>
		/// <returns>The new filtered string.</returns>
		public static string RemoveNonAlpha(this string self) {
			return self.Filter(c => !char.IsLetter(c));
		}

		/// <summary>
		/// Produces a new string by removing all non-alpha-numeric characters from the sting provided.
		/// </summary>
		/// <param name="self">The string to act upon.</param>
		/// <returns>The new filtered string.</returns>
		public static string RemoveNonAlphaNumeric(this string self) {
			return self.Filter(c => !char.IsLetterOrDigit(c));
		}

		/// <summary>
		/// Produces a new string by removing all whitespace characters from the sting provided.
		/// </summary>
		/// <param name="self">The string to act upon.</param>
		/// <returns>The new filtered string.</returns>
		public static string RemoveWhitespace(this string self) {
			return self.Filter(c => !char.IsWhiteSpace(c));
		}

		/// <summary>
		/// This method ensures that the returned string has only valid XML unicode
		/// charcters as specified in the XML 1.0 standard. For reference please see
		/// http://www.w3.org/TR/2000/REC-xml-20001006#NT-Char for the
		/// standard reference.
		/// </summary>
		/// <param name="self">The string being acted upon.</param>
		/// <returns>A copy of the input string with non-valid charcters removed.</returns>
		public static string RemoveInvalidXmlCharacters(this string self) {
			return self.Filter(c => !((c == 0x9)
									|| (c == 0xA)
									|| (c == 0xD)
									|| ((c >= 0x20) && (c <= 0xD7FF))
									|| ((c >= 0xE000) && (c <= 0xFFFD)))
				/*|| ((c >= 0x10000) && (c <= 0x10FFFF))*/
			);
		}

		/// <summary>
		/// Removes characters from the left side of a string.
		/// </summary>
		/// <param name="self">The string to be acted upon.</param>
		/// <param name="amount">The number of charcters to remove.</param>
		/// <returns>Returns a new string with the characters removed.</returns>
		public static string TrimLeftBy(this string self, int amount) {
			return self.Substring(amount);
		}

		/// <summary>
		/// Removes characters from the right side of a string.
		/// </summary>
		/// <param name="self">The string to be acted upon.</param>
		/// <param name="amount">The number of charcters to remove.</param>
		/// <returns>Returns a new string with the characters removed.</returns>
		public static string TrimRightBy(this string self, int amount) {
			return self.Substring(0, self.Length - amount);
		}

		/// <summary>
		/// Removes characters from the left and right sides of a string.
		/// </summary>
		/// <param name="self">The string to be acted upon.</param>
		/// <param name="amount">The number of charcters to remove.</param>
		/// <returns>Returns a new string with the characters removed.</returns>
		public static string TrimEndsBy(this string self, int amount) {
			return self.Substring(amount, self.Length - amount);
		}

		/// <summary>
		/// Generates a simple hash for a string. 
		/// </summary>
		/// <remarks>
		/// This hash is not asserted to be fit for any particular purpose
		/// other than simple features where you just need a hash of a string.
		/// </remarks>
		/// <param name="self">The string to be acted upon.</param>
		/// <returns>
		/// Returns a simple hash of a string.
		/// </returns>
		public static string Hash(this string self) {
			MD5 md5Hasher = MD5.Create();
			byte[] data = md5Hasher.ComputeHash(System.Text.Encoding.Default.GetBytes(self));
			string hash = BitConverter.ToString(data);
			return hash;
		}

		/// <summary>
		/// Regards all occurrences of substrings starting and finishing with `|`
		/// pipe charcters as potential keys, and if those keys occur within the
		/// provided dictionary, replaces those keys in the provided text
		/// with the corresposponding value in the dictionary.
		/// </summary>
		/// <remarks>
		/// This is performed as a single scan of characters and should be used
		/// in preference in those situations where you find yourself
		/// doing multiple replacements on a large string, as this will do them in one go.
		/// </remarks>
		/// <param name="text">The text to act upon.</param>
		/// <param name="kv">The dictionary of key-value pairs for substitution.</param>
		/// <returns>Returns a new string with any matching keys replaced.</returns>
		public static string ReplaceKeys(this string text, IDictionary<string, string> kv) {
			bool readingKey = false;
			StringBuilder currentKey = new StringBuilder();
			StringBuilder result = new StringBuilder();
			foreach (char c in text) {
				if (readingKey) {
					if (c == '|') { // finish reading key
						if (kv.ContainsKey(currentKey.ToString())) {
							result.Append(kv[currentKey.ToString()]);
						}
						readingKey = false;
					} else { // reading the key
						currentKey.Append(c);
					}
				} else {
					if (c == '|') { // start reading key
						readingKey = true;
						currentKey.Clear();
					} else { // just reading regular text
						result.Append(c);
					}
				}
			}
			return result.ToString();
		}

		/// <summary>
		/// Loads the string into an xml document.
		/// </summary>
		/// <param name="self">The string being acted upon.</param>
		/// <returns>
		/// Returns an xml document with the string loaded.
		/// </returns>
		public static XmlDocument AsXmlDocument(this string self) {
			XmlDocument xml = new XmlDocument();
			xml.LoadXml(self);
			return xml;
		}

		/// <summary>
		/// Loads the string into an XDocument.
		/// </summary>
		/// <param name="self">The string being acted upon.</param>
		/// <returns>
		/// Returns an XDocument with the string loaded.
		/// </returns>
		public static XDocument AsXDocument(this string self) {
			XDocument xml = XDocument.Parse(self);
			return xml;
		}

		/// <summary>
		/// Loads the string into an XElement.
		/// </summary>
		/// <param name="self">The string being acted upon.</param>
		/// <returns>Returns an XElement with the string loaded.</returns>
		public static XElement AsXElement(this string self) {
			XElement xml = XElement.Parse(self);
			return xml;
		}
	}
}

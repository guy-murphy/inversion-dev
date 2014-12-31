using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;

namespace Inversion.Extensions {

	/// <summary>
	/// An extension class providing extensions for `IEnumerable{T}` objects.
	/// </summary>
	public static class EnumerableEx {

		/// <summary>
		/// Produces an XML representation of an enumerable by iterating over
		/// the elements of the enumerable and calling `.ToXml()` on them.
		/// </summary>
		/// <typeparam name="T">The type of elements in the enumerable.</typeparam>
		/// <param name="self">The enumerable to act upon.</param>
		/// <param name="label">The label of the enclosing element.</param>
		/// <returns>An XML representation of the provided enumerable.</returns>
		public static string ToXml<T>(this IEnumerable<T> self, string label) where T : IData {
			using (StringWriter str = new StringWriter()) {
				self.ToXml(str, label);
				return str.ToString();
			}
		}

		/// <summary>
		/// Produces an XML representation of an enumerable by iterating over
		/// the elements of the enumerable and calling `.ToXml()` on them.
		/// </summary>
		/// <typeparam name="T">The type of elements in the enumerable.</typeparam>
		/// <param name="self">The enumerable to act upon.</param>
		/// <param name="writer">The text writer the XML should be written to.</param>
		/// <param name="label">The label of the enclosing element.</param>
		public static void ToXml<T>(this IEnumerable<T> self, TextWriter writer, string label) where T : IData {
			using (XmlTextWriter xml = new XmlTextWriter(writer)) {
				xml.Formatting = Formatting.Indented;
				self.ToXml(xml, label);
			}
		}

		/// <summary>
		/// Produces an XML representation of an enumerable by iterating over
		/// the elements of the enumerable and calling `.ToXml()` on them.
		/// </summary>
		/// <typeparam name="T">The type of elements in the enumerable.</typeparam>
		/// <param name="self">The enumerable to act upon.</param>
		/// <param name="xml">The xml writer the XML should be written to.</param>
		/// <param name="label">The label of the enclosing element.</param>
		public static void ToXml<T>(this IEnumerable<T> self, XmlWriter xml, string label) where T : IData {
			xml.WriteStartElement(label);
			foreach (T item in self) {
				item.ToXml(xml);
			}
			xml.WriteEndElement();
		}

		/// <summary>
		/// Produces a JSON representation of an enumerable by iterating over
		/// the elements of the enumerable and calling `.ToJson()` on them.
		/// </summary>
		/// <typeparam name="T">The type of elements in the enumerable.</typeparam>
		/// <param name="self">The enumerable to act upon.</param>
		/// <returns>An JSON representation of the provided enumerable.</returns>
		public static string ToJson<T>(this IEnumerable<T> self) where T : IData {
			using (StringWriter str = new StringWriter()) {
				self.ToJson(str);
				return str.ToString();
			}
		}

		/// <summary>
		/// Produces a JSON representation of an enumerable by iterating over
		/// the elements of the enumerable and calling `.ToJson()` on them.
		/// </summary>
		/// <typeparam name="T">The type of elements in the enumerable.</typeparam>
		/// <param name="self">The enumerable to act upon.</param>
		/// <param name="writer">The text writer the JSON should be written to.</param>
		public static void ToJson<T>(this IEnumerable<T> self, TextWriter writer) where T : IData {
			using (JsonTextWriter json = new JsonTextWriter(writer)) {
				json.Formatting = Newtonsoft.Json.Formatting.Indented;
				self.ToJson(json);
			}
		}

		/// <summary>
		/// Produces a JSON representation of an enumerable by iterating over
		/// the elements of the enumerable and calling `.ToJson()` on them.
		/// </summary>
		/// <typeparam name="T">The type of elements in the enumerable.</typeparam>
		/// <param name="self">The enumerable to act upon.</param>
		/// <param name="json">The json writer the JSON should be written to.</param>
		public static void ToJson<T>(this IEnumerable<T> self, JsonWriter json) where T : IData {
			json.WriteStartArray();
			foreach (T item in self) {
				item.ToJson(json);
			}
			json.WriteEndArray();
		}

		/// <summary>
		/// Produces a hash from all the string elements
		/// of an enumerable.
		/// </summary>
		/// <param name="self">The enumerable of strings to act upon.</param>
		/// <returns>Returns a has of all the elements.</returns>
		public static int CalculateHash(this IEnumerable<string> self) {
			return self.Aggregate(17, (current, item) => current*31 + item.GetHashCode());
		}
	}
}

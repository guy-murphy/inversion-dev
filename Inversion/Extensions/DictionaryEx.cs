using System.Collections.Generic;
using System.Xml;

namespace Inversion.Extensions {

	/// <summary>
	/// Utility extension methods provided for dictionaries.
	/// </summary>

	public static class DictionaryEx {

		/// <summary>
		/// Copies the elements from one dictionary to another.
		/// </summary>
		/// <typeparam name="TKey">The type of the dictionary keys.</typeparam>
		/// <typeparam name="TValue">The type of the dictionary values.</typeparam>
		/// <param name="self">The dictionary being acted on.</param>
		/// <param name="other">The dictionary being copied from.</param>

		public static void Import<TKey, TValue>(IDictionary<TKey, TValue> self, IDictionary<TKey, TValue> other) {
			foreach (TKey key in other.Keys) {
				self.Add(key, other[key]);
			}
		}

		/// <summary>
		/// Produces an XML representation of the elements of a dictionary.
		/// </summary>
		/// <param name="self">The dictionary being acted upon.</param>
		/// <param name="writer">
		/// The <see cref="XmlWriter"/> the representation
		/// is written to.
		/// </param>

		public static void ContentToXml(IDictionary<string, IData> self, XmlWriter writer) {
			foreach (KeyValuePair<string, IData> item in self) {
				if (item.Value != null) {
					writer.WriteStartElement("item");
					writer.WriteAttributeString("name", item.Key);
					item.Value.ToXml(writer);
					writer.WriteEndElement();
				} else {
					writer.WriteStartElement("item");
					writer.WriteAttributeString("name", item.Key);
					writer.WriteAttributeString("value", "null");
					writer.WriteEndElement();
				}
			}
		}

	}
}

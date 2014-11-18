using System;
using System.Collections.Generic;
using System.Xml;
using Newtonsoft.Json;

namespace Inversion.Collections {

	/// <summary>
	/// A collection of key/value pairs, where the key is a string.
	/// </summary>
	/// <typeparam name="TValue">The type of the element values.</typeparam>

	[Serializable]
	public class DataDictionary<TValue> : Dictionary<string, TValue>, IDataDictionary<TValue> {

		/// <summary>
		/// Instantiates a new empty instance of the dictionary.
		/// </summary>

		public DataDictionary() : base() { }

		/// <summary>
		/// instantiates a new dictionary with the elements
		/// copied over from the dictionary provided.
		/// </summary>
		/// <param name="other">
		/// The dictionary to copy elements from.
		/// </param>

		public DataDictionary(IDictionary<string, TValue> other) : base(other) { }

		/// <summary>
		/// instantiates a new dictionary with the elements
		/// copied from iterating over the key/value pairs provided.
		/// </summary>
		/// <param name="other">The key/value pairs to copy.</param>

		public DataDictionary(IEnumerable<KeyValuePair<string, TValue>> other) {
			this.Import(other);
		}


		object ICloneable.Clone() {
			return new DataDictionary<TValue>(this);
		}

		/// <summary>
		/// Clones the data dictionary by instantiating a new one
		/// populated by the elemens of this one.
		/// </summary>
		/// <returns></returns>
		public virtual DataDictionary<TValue> Clone() {
			return new DataDictionary<TValue>(this);
		}

		/// <summary>
		/// Instantiates a dictionary populating it with the elements
		/// of the provided dictionary.
		/// </summary>
		/// <param name="other">The other dictionary to populate the new collection with.</param>
		public IDataDictionary<TValue> Import(IEnumerable<KeyValuePair<string, TValue>> other) {
			foreach (KeyValuePair<string, TValue> entry in other) {
				this.Add(entry.Key, entry.Value);
			}
			return this;
		}

		/// <summary>
		/// Produces an xml representation of the elements of the dictionary.
		/// </summary>
		/// <param name="writer">The xml writer the representation should be written to.</param>
		public virtual void ContentToXml(XmlWriter writer) {
			foreach (KeyValuePair<string, TValue> item in this) {
				if (item.Value is IData) {
					writer.WriteStartElement("item");
					writer.WriteAttributeString("name", item.Key);
					((IData)item.Value).ToXml(writer);
					writer.WriteEndElement();
				} else {
					writer.WriteStartElement("item");
					writer.WriteAttributeString("name", item.Key);
					if (item.Value is ValueType || item.Value != null) {
						writer.WriteAttributeString("value", item.Value.ToString());
					}
					writer.WriteEndElement();
				}
			}
		}

		/// <summary>
		/// Produces and xml representation of the dictionary.
		/// </summary>
		/// <param name="writer">The xml writer the xml should be written to.</param>
		public virtual void ToXml(XmlWriter writer) {
			writer.WriteStartElement("records");
			this.ContentToXml(writer);
			writer.WriteEndElement();
		}

		/// <summary>
		/// Produces a json representation of the dictionaries elements.
		/// </summary>
		/// <param name="writer">The json writer the representation should be written to.</param>
		public virtual void ContentToJson(JsonWriter writer) {
			foreach (KeyValuePair<string, TValue> item in this) {
				if (item.Value is IData) {
					writer.WritePropertyName(item.Key);
					((IData)item.Value).ToJson(writer);
				} else {
					writer.WritePropertyName(item.Key);
					if (item.Value is ValueType || item.Value != null) {
						writer.WriteValue(item.Value.ToString());
					}
				}
			}
		}

		/// <summary>
		/// Produces a json representation of the dictionary.
		/// </summary>
		/// <param name="writer">The json writer the representation should be written to.</param>
		public virtual void ToJson(JsonWriter writer) {
			writer.WriteStartObject();
			this.ContentToJson(writer);
			writer.WriteEndObject();
		}

	}
}

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

		public virtual DataDictionary<TValue> Clone() {
			return new DataDictionary<TValue>(this);
		}

		public IDataDictionary<TValue> Import(IEnumerable<KeyValuePair<string, TValue>> other) {
			foreach (KeyValuePair<string, TValue> entry in other) {
				this.Add(entry.Key, entry.Value);
			}
			return this;
		}

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

		public virtual void ToXml(XmlWriter writer) {
			writer.WriteStartElement("records");
			this.ContentToXml(writer);
			writer.WriteEndElement();
		}

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

		public virtual void ToJson(JsonWriter writer) {
			writer.WriteStartObject();
			this.ContentToJson(writer);
			writer.WriteEndObject();
		}

	}
}

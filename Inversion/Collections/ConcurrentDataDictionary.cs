using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Xml;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Inversion.Collections {

	/// <summary>
	/// A thread-safe dictionary of key-value pairs where the key is a string and the dictionary itself implements
	/// `IData`
	/// </summary>
	/// <typeparam name="TValue">The type of the element values.</typeparam>
	public class ConcurrentDataDictionary<TValue> : ConcurrentDictionary<string, TValue>, IDataDictionary<TValue> {

		/// <summary>
		/// Provides an abstract representation
		/// of the objects data expressed as a JSON object.
		/// </summary>
		/// <remarks>
		/// For this type the json object is created each time
		/// it is accessed.
		/// </remarks>
		public JObject Data {
			get { return this.ToJsonObject(); }
		}

		/// <summary>
		/// Instantiates a new empty dictionary.
		/// </summary>
		public ConcurrentDataDictionary() : base() { }
		/// <summary>
		/// Instantiates a new dictionary populated with the enumerable of key-value pairs provided.
		/// </summary>
		/// <param name="other">The key-value pairs to populate the dictionary with.</param>
		public ConcurrentDataDictionary(IEnumerable<KeyValuePair<string, TValue>> other) : base(other) { }
		/// <summary>
		/// Instantiates a new dictionary populated from the dictionary provided.
		/// </summary>
		/// <param name="other">The other dictionary to populate this dictionary from.</param>
		public ConcurrentDataDictionary(DataDictionary<TValue> other)
			: base(other) {

		}
		/// <summary>
		/// Clones the dictionary.
		/// </summary>
		/// <returns>Returnes a new dictionary instance populated by this one.</returns>
		public object Clone() {
			return new ConcurrentDataDictionary<TValue>(this);
		}
		/// <summary>
		/// Imports the key-value pairs from a provided dictionary into this one.
		/// </summary>
		/// <param name="other">The other dictionary to import into this one.</param>
		/// <returns>Returns the current instance of this dictionary.</returns>
		public IDataDictionary<TValue> Import(IEnumerable<KeyValuePair<string, TValue>> other) {
			foreach (KeyValuePair<string, TValue> entry in other) {
				this[entry.Key] = entry.Value;
			}
			return this;
		}

		/// <summary>
		/// Produces an XML representation of the dictionary elements.
		/// </summary>
		/// <param name="writer">The xml writer the xml should be written to.</param>
		public virtual void ContentToXml(XmlWriter writer) {
			foreach (KeyValuePair<string, TValue> item in this) {
				IData data = item.Value as IData;
				if (data != null) {
					writer.WriteStartElement("item");
					writer.WriteAttributeString("name", item.Key);
					data.ToXml(writer);
					writer.WriteEndElement();
				} else {
					writer.WriteStartElement("item");
					writer.WriteAttributeString("name", item.Key);
					if (item.Value is ValueType) {
						writer.WriteAttributeString("value", item.Value.ToString());
					} else {
						if (item.Value != null) {
							writer.WriteAttributeString("value", item.Value.ToString());
						}
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
		/// Produces a json representation of the dictionary elements.
		/// </summary>
		/// <param name="writer">The json writer the json should be written to.</param>
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
		/// <param name="writer">The json writer the json should be written to.</param>
		public void ToJson(JsonWriter writer) {
			writer.WriteStartObject();
			this.ContentToJson(writer);
			writer.WriteEndObject();
		}

	}
}


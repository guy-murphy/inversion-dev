using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Xml;

using Newtonsoft.Json;

namespace Inversion.Collections {

	public class ConcurrentDataDictionary<TValue> : ConcurrentDictionary<string, TValue>, IDataDictionary<TValue> {

		public ConcurrentDataDictionary() : base() { }
		public ConcurrentDataDictionary(IEnumerable<KeyValuePair<string, TValue>> other) : base(other) { }
		public ConcurrentDataDictionary(DataDictionary<TValue> other)
			: base(other) {

		}

		public object Clone() {
			return new ConcurrentDataDictionary<TValue>(this);
		}

		public IDataDictionary<TValue> Import(IEnumerable<KeyValuePair<string, TValue>> other) {
			foreach (KeyValuePair<string, TValue> entry in other) {
				this[entry.Key] = entry.Value;
			}
			return this;
		}

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

		public void ToJson(JsonWriter writer) {
			writer.WriteStartObject();
			this.ContentToJson(writer);
			writer.WriteEndObject();
		}

	}
}


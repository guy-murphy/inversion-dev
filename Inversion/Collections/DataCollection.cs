using System;
using System.Collections.Generic;
using System.Xml;

using Newtonsoft.Json;

namespace Inversion.Collections {

	/// <summary>
	/// An implementation of <see cref="IDataCollection{T}"/> as a simple <see cref="List{T}"/>. 
	/// </summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>

	public class DataCollection<T> : List<T>, IDataCollection<T> {

		private readonly string _label;

		public string Label { get { return _label ?? "list"; } }

		public DataCollection(string label) {
			_label = label;
		}

		/// <summary>
		/// Instanciates a new, empty data collection.
		/// </summary>
		public DataCollection() : this("list") { }

		/// <summary>
		/// Instanciates a new data collection with elements
		/// copied from the provided collection.
		/// </summary>
		/// <param name="collection">
		/// The collection whose elements are copied into the
		/// new data collection.
		/// </param>

		public DataCollection(IEnumerable<T> collection) : base(collection) { }

		public DataCollection(IDataCollection<T> other)
			: base(other) {
			_label = other.Label;
		}

		object ICloneable.Clone() {
			return new DataCollection<T>(this);
		}

		public DataCollection<T> Clone() {
			return new DataCollection<T>(this);
		}

		/// <summary>
		/// Produces an XML representation of the dictionaries elements,
		/// to a provided writer.
		/// </summary>
		/// <param name="writer">
		/// The <see cref="XmlWriter"/> the representation is written to.
		/// </param>

		public virtual void ContentToXml(XmlWriter writer) {
			foreach (T item in this) {
				if (item is ValueType) {
					writer.WriteElementString("item", item.ToString());
				} else if (item != null) {
					if (item is IData) {
						((IData)item).ToXml(writer);
					} else {
						writer.WriteElementString("item", item.ToString());
					}
				}
			}
		}

		/// <summary>
		/// Produces an JSON representation of the dictionaries elements,
		/// to a provided writer.
		/// </summary>
		/// <param name="writer">
		/// The <see cref="JsonWriter"/> the representation is written to.
		/// </param>

		public virtual void ContextToJson(JsonWriter writer) {
			foreach (T item in this) {
				if (item is ValueType) {
					writer.WriteValue(item.ToString());
				} else if (item != null) {
					if (item is IData) {
						((IData)item).ToJson(writer);
					} else {
						writer.WriteValue(item.ToString());
					}
				}
			}
		}

		/// <summary>
		/// Produces an XML representation of the dictionaries  to a provided writer.
		/// </summary>
		/// <param name="writer">
		/// The <see cref="XmlWriter"/> the representation is written to.
		/// </param>

		public void ToXml(XmlWriter writer) {
			writer.WriteStartElement(this.Label);
			this.ContentToXml(writer);
			writer.WriteEndElement();
		}

		/// <summary>
		/// Produces an JSON representation of the dictionaries  to a provided writer.
		/// </summary>
		/// <param name="writer">
		/// The <see cref="JsonWriter"/> the representation is written to.
		/// </param>

		public void ToJson(JsonWriter writer) {
			writer.WriteStartArray();
			this.ContextToJson(writer);
			writer.WriteEndArray();
		}

	}
}

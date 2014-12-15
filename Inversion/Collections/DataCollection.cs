using System;
using System.Collections.Generic;
using System.Xml;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Inversion.Collections {

	/// <summary>
	/// An implementation of <see cref="IDataCollection{T}"/> as a simple <see cref="List{T}"/>. 
	/// </summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>

	public class DataCollection<T> : List<T>, IDataCollection<T> {

		private readonly string _label;

		/// <summary>
		/// The label that should be used for the collection in
		/// any notation presenting the collection. 
		/// </summary>
		/// <remarks>This will default to "list".</remarks>
		public string Label { get { return _label ?? "list"; } }

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
		/// Instantiates a new empty collection with the lable provided.
		/// </summary>
		/// <param name="label">The label of the collection.</param>
		public DataCollection(string label) {
			_label = label;
		}

		/// <summary>
		/// Instantiates a new, empty data collection.
		/// </summary>
		public DataCollection() : this("list") { }

		/// <summary>
		/// Instantiates a new data collection with elements
		/// copied from the provided collection.
		/// </summary>
		/// <param name="collection">
		/// The collection whose elements are copied into the
		/// new data collection.
		/// </param>

		public DataCollection(IEnumerable<T> collection) : base(collection) { }

		/// <summary>
		/// Instantiates a collection populating it with the elements
		/// of the provided collection.
		/// </summary>
		/// <param name="other">The other collection to populate the new collection with.</param>
		public DataCollection(IDataCollection<T> other)
			: base(other) {
			_label = other.Label;
		}

		object ICloneable.Clone() {
			return new DataCollection<T>(this);
		}

		/// <summary>
		/// Creates a clone of the collection by instantiating
		/// a new collection populated with the elements of this one.
		/// </summary>
		/// <returns>Returns a new collection populated by this one.</returns>
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

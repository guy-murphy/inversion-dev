using System;
using System.Xml;
using Newtonsoft.Json;

namespace Inversion {

	/// <summary>
	/// An implementation of <see cref="IData"/> that
	/// represents a simple text node within a model.
	/// </summary>

	public class TextData : IData {

		/// <summary>
		/// Implicitly casts a string of text into a `TextData` object,
		/// by instantiating a `TextData` object from the string.
		/// </summary>
		/// <param name="text">The string of text to be cast.</param>
		/// <returns>
		/// Returns the `TextData` object created.
		/// </returns>

		public static implicit operator TextData(string text) {
			return new TextData(text);
		}

		/// <summary>
		/// Implicitly casts a `TextData` object into a string.
		/// </summary>
		/// <param name="text">The `TextData` object to cast.</param>
		/// <returns>Returns the string value of the `TextData` object.</returns>

		public static implicit operator string(TextData text) {
			return text.Value;
		}

		private readonly string _text;

		/// <summary>
		/// The string value of the text data.
		/// </summary>

		public string Value {
			get {
				return _text;
			}
		}

		/// <summary>
		/// Instantiates a new `TextData` object with the value
		/// of the text provided.
		/// </summary>
		/// <param name="text">The text to initialise from.</param>

		public TextData(string text) {
			_text = text;
		}

		/// <summary>
		/// Instantiates a new `TextData` object as a copy
		/// of the one provided.
		/// </summary>
		/// <param name="text">The `TextData` to copy.</param>

		public TextData(TextData text) {
			_text = text.Value;
		}

		object ICloneable.Clone() {
			return this.Clone();
		}

		/// <summary>
		/// Creates a new instance as a copy
		/// of the original.
		/// </summary>
		/// <returns>
		/// A copy as a `TextData` object.
		/// </returns>

		public TextData Clone() {
			return new TextData(this);
		}

		/// <summary>
		/// Produces an xml representation of the text data.
		/// </summary>
		/// <param name="writer">The xml writer the representation should be written to.</param>
		public void ToXml(XmlWriter writer) {
			writer.WriteStartElement("text-data");
			writer.WriteCData(this.Value);
			writer.WriteEndElement();
		}

		/// <summary>
		/// Produces a json representation of the text data.
		/// </summary>
		/// <param name="writer">The json writer the representation should be written to.</param>
		public void ToJson(JsonWriter writer) {
			writer.WriteStartObject();
			writer.WritePropertyName("text-data");
			writer.WriteValue(this.Value);
			writer.WriteEndObject();
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		public override string ToString() {
			return this.Value ?? "[no value]";
		}
	}
}

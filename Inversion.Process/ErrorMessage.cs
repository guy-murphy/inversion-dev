using System;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Inversion.Process {
	/// <summary>
	/// Represents an error message that occurred
	/// during application processing that may be suitable
	/// for presenting in any user agent.
	/// </summary>
	public class ErrorMessage : IData {

		private readonly string _message;
		private readonly Exception _exception;
		private JObject _data;

		/// <summary>
		/// Provides an abstract representation
		/// of the objects data expressed as a JSON object.
		/// </summary>
		/// <remarks>
		/// For this type the json object is only created the once.
		/// </remarks>
		public JObject Data {
			get {
				return _data ?? (_data = this.ToJsonObject());
			}
		}

		/// <summary>
		/// A human readable message summarising the error.
		/// </summary>
		public string Message {
			get {
				return _message;
			}
		}

		/// <summary>
		/// The exception if any that gave rise to this error.
		/// </summary>
		public Exception Exception {
			get {
				return _exception;
			}
		}

		/// <summary>
		/// Clones a new error message as a copy of this one.
		/// </summary>
		/// <returns>The newly cloned error message.</returns>
		object ICloneable.Clone() {
			return this.Clone();
		}

		/// <summary>
		/// Clones a new error message as a copy of this one.
		/// </summary>
		/// <returns>The newly cloned error message.</returns>
		public ErrorMessage Clone() {
			return new ErrorMessage(this.Message, this.Exception);
		}

		/// <summary>
		/// Instantiates a new error message.
		/// </summary>
		/// <param name="message">The human readable message.</param>
		public ErrorMessage(string message) : this(message, null) { }

		/// <summary>
		/// Instantiates a new error message.
		/// </summary>
		/// <param name="message">The human readable message.</param>
		/// <param name="err">The exception that gave rise to this error.</param>
		public ErrorMessage(string message, Exception err) {
			_message = message;
			_exception = err;
		}

		/// <summary>
		/// Produces an xml representation of the model.
		/// </summary>
		/// <param name="writer">The writer to used to write the xml to. </param>
		public void ToXml(XmlWriter writer) {
			writer.WriteStartElement("error");
			writer.WriteAttributeString("message", this.Message);
			if (this.Exception != null) {
				writer.WriteStartElement("exception");
				writer.WriteCData(this.Exception.Message);
				writer.WriteEndElement();
				writer.WriteStartElement("fullmessage");
				writer.WriteCData(this.Exception.ToString());
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		/// <summary>
		/// Produces a json respresentation of the model.
		/// </summary>
		/// <param name="writer">The writer to use for producing json.</param>
		public void ToJson(JsonWriter writer) {
			writer.WriteStartObject();
			writer.WritePropertyName("_type");
			writer.WriteValue("error-message");
			writer.WritePropertyName("message");
			writer.WriteValue(this.Message);
			if (this.Exception != null) {
				writer.WritePropertyName("exception");
				writer.WriteValue(this.Exception.Message);
				writer.WritePropertyName("fullmessage");
				writer.WriteValue(this.Exception.ToString());
			}
			writer.WriteEndObject();
		}

		/// <summary>
		/// Provides a string representation of this error message.
		/// </summary>
		/// <returns>Returns a new string representing this error message.</returns>
		public override string ToString() {
			return this.ToJson();
		}
	}
}

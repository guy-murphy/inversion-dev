using System;
using System.Xml;
using Newtonsoft.Json;

namespace Inversion.Process {
	public class ErrorMessage : IData {

		private readonly string _message;
		private readonly Exception _exception;

		public string Message {
			get {
				return _message;
			}
		}

		public Exception Exception {
			get {
				return _exception;
			}
		}

		object ICloneable.Clone() {
			return this.Clone();
		}

		public ErrorMessage Clone() {
			return new ErrorMessage(this.Message, this.Exception);
		}

		public ErrorMessage(string message) : this(message, null) { }
		public ErrorMessage(string message, Exception err) {
			_message = message;
			_exception = err;
		}

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

		public override string ToString() {
			return this.ToJson();
		}
	}
}

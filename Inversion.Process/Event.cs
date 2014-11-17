using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Inversion.Process {
	public class Event : IEvent, IEnumerable<KeyValuePair<string, string>> {

		private readonly string _message;
		private readonly ProcessContext _context;
		private readonly IDictionary<string, string> _params;
		private object _object;

		public string this[string key] {
			get { return this.Params.ContainsKey(key) ? this.Params[key] : null; }
		}

		public string Message {
			get {
				return _message;
			}
		}

		public IDictionary<string, string> Params {
			get {
				return _params;
			}
		}

		public ProcessContext Context {
			get {
				return _context;
			}
		}

		public object Object {
			get { return _object; }
			set {
				if (_object == null) {
					_object = value;
				} else {
					throw new InvalidOperationException("You may only set the Object value of an event the once. Thereafter it is readonly.");
				}
			}
		}

		public Event(ProcessContext context, string message, IDictionary<string, string> parameters) : this(context, message, null, parameters) { }

		public Event(ProcessContext context, string message, object obj, IDictionary<string, string> parameters) {
			_context = context;
			_message = message;
			_object = obj;
			_params = (parameters == null) ? new Dictionary<string, string>() : new Dictionary<string, string>(parameters);
		}

		public Event(ProcessContext context, string message, params string[] parms) : this(context, message, null, parms) { }

		public Event(ProcessContext context, string message, object obj, params string[] parms) {
			_context = context;
			_message = message;
			_object = obj;
			_params = new Dictionary<string, string>();

			foreach (string parm in parms) {
				if (context.Params.ContainsKey(parm)) {
					this.Add(parm, context.Params[parm]);
				}
			}
		}

		public Event(IEvent ev) {
			_context = ev.Context;
			_message = ev.Message;
			_object = ev.Object;
			_params = new Dictionary<string, string>(ev.Params);
		}

		object ICloneable.Clone() {
			return new Event(this);
		}

		public Event Clone() {
			return new Event(this);
		}

		public void Add(string key, string value) {
			this.Params.Add(key, value);
		}

		public IEvent Fire() {
			return this.Context.Fire(this);
		}

		public bool HasParams(params string[] parms) {
			return parms.Length > 0 && parms.All(parm => this.Params.ContainsKey(parm));
		}

		public bool HasParamValues(IEnumerable<KeyValuePair<string, string>> match) {
			return match.All(entry => this.Params.Contains(entry));
		}

		public bool HasRequiredParams(params string[] parms) {
			bool has = parms.Length > 0;
			foreach (string parm in parms) {
				if (!this.Params.ContainsKey(parm)) {
					has = false;
					this.Context.Errors.CreateMessage("The parameter '{0}' is required and was not present.", parm);
				}
			}
			return has;
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();

			sb.AppendFormat("(event @message {0}\n", this.Message);
			foreach (KeyValuePair<string, string> entry in this.Params) {
				sb.AppendFormat("   ({0} -{1})\n", entry.Key, entry.Value);
			}
			sb.AppendLine(")");

			return sb.ToString();
		}

		public IEnumerator<KeyValuePair<string, string>> GetEnumerator() {
			return this.Params.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return this.Params.GetEnumerator();
		}

		public void ToXml(XmlWriter xml) {
			xml.WriteStartElement("event");
			xml.WriteAttributeString("message", this.Message);
			xml.WriteStartElement("params");
			foreach (KeyValuePair<string, string> entry in this.Params) {
				xml.WriteStartElement("item");
				xml.WriteAttributeString("name", entry.Key);
				xml.WriteAttributeString("value", entry.Value);
			}
			xml.WriteEndElement();
			xml.WriteEndElement();
		}

		public void ToJson(JsonWriter json) {
			json.WriteStartObject();
			json.WritePropertyName("_type");
			json.WriteValue("event");
			json.WritePropertyName("message");
			json.WriteValue(this.Message);
			json.WritePropertyName("params");
			json.WriteStartObject();
			foreach (KeyValuePair<string, string> kvp in this.Params) {
				json.WritePropertyName(kvp.Key);
				json.WriteValue(kvp.Value);
			}
			json.WriteEndObject();
			json.WriteEndObject();
		}

		public static Event FromXml(ProcessContext context, string xml) {
			try {
				XElement ev = XElement.Parse(xml);
				if (ev.Name == "event") {
					return new Event(
						context,
						ev.Attribute("message").Value,
						ev.Elements().ToDictionary(el => el.Attribute("name").Value, el => el.Attribute("value").Value)
					);
				} else {
					throw new ParseException("The expressed type of the json provided does not appear to be an event.");
				}
			} catch (Exception err) {
				throw new ParseException("An unexpected error was encoutered parsing the provided json into an event object.", err);
			}
		}

		public static Event FromJson(ProcessContext context, string json) {
			try {
				JObject job = JObject.Parse(json);
				if (job.Value<string>("_type") == "event") {
					return new Event(
						context,
						job.Value<string>("message"),
						job.Value<JObject>("params").Properties().ToDictionary(p => p.Name, p => p.Value.ToString())
					);
				} else {
					throw new ParseException("The expressed type of the json provided does not appear to be an event.");
				}
			} catch (Exception err) {
				throw new ParseException("An unexpected error was encoutered parsing the provided json into an event object.", err);
			}
		}

		public class ParseException : InvalidOperationException {
			public ParseException(string message) : base(message) { }
			public ParseException(string message, Exception err) : base(message, err) { }
		}
	}
}

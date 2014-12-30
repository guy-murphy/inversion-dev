using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Inversion.Process {

	/// <summary>
	/// Represents an event occuring in the system.
	/// </summary>
	/// <remarks>
	/// Exactly what "event" means is application specific
	/// and can range from imperative to reactive.
	/// </remarks>
	public class Event : IEvent, IEnumerable<KeyValuePair<string, string>> {

		private readonly string _message;
		private readonly ProcessContext _context;
		private readonly IDictionary<string, string> _params;
		private IData _object;

		/// <summary>
		/// Provides indexed access to the events parameters.
		/// </summary>
		/// <param name="key">The key of the parameter to look up.</param>
		/// <returns>Returns the parameter indexed by the key.</returns>
		public string this[string key] {
			get { return this.Params.ContainsKey(key) ? this.Params[key] : null; }
		}

		/// <summary>
		/// The simple message the event represents.
		/// </summary>
		/// <remarks>
		/// Again, exactly what this means is application specific.
		/// </remarks>
		public string Message {
			get {
				return _message;
			}
		}

		/// <summary>
		/// The parameters for this event represented
		/// as key-value pairs.
		/// </summary>
		public IDictionary<string, string> Params {
			get {
				return _params;
			}
		}

		/// <summary>
		/// The context upon which this event is being fired.
		/// </summary>
		/// <remarks>
		/// And event always belongs to a context.
		/// </remarks>
		public ProcessContext Context {
			get {
				return _context;
			}
		}

		/// <summary>
		/// Any object that the event may be carrying.
		/// </summary>
		/// <remarks>
		/// This is a dirty escape hatch, and
		/// can even be used as a "return" value
		/// for the event.
		/// </remarks>
		public IData Object {
			get { return _object; }
			set {
				if (_object == null) {
					_object = value;
				} else {
					throw new InvalidOperationException("You may only set the Object value of an event the once. Thereafter it is readonly.");
				}
			}
		}

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
		/// Instantiates a new event bound  to a context.
		/// </summary>
		/// <param name="context">The context to which the event is bound.</param>
		/// <param name="message">The simple message the event represents.</param>
		/// <param name="parameters">The parameters of the event.</param>
		public Event(ProcessContext context, string message, IDictionary<string, string> parameters) : this(context, message, null, parameters) { }

		/// <summary>
		/// Instantiates a new event bound  to a context.
		/// </summary>
		/// <param name="context">The context to which the event is bound.</param>
		/// <param name="message">The simple message the event represents.</param>
		/// <param name="obj">An object being carried by the event.</param>
		/// <param name="parameters">The parameters of the event.</param>
		public Event(ProcessContext context, string message, IData obj, IDictionary<string, string> parameters) {
			_context = context;
			_message = message;
			_object = obj;
			_params = (parameters == null) ? new Dictionary<string, string>() : new Dictionary<string, string>(parameters);
		}

		/// <summary>
		/// Instantiates a new event bound  to a context.
		/// </summary>
		/// <param name="context">The context to which the event is bound.</param>
		/// <param name="message">The simple message the event represents.</param>
		/// <param name="parms">
		/// A sequnce of context parameter names that should be copied from the context
		/// to this event.
		/// </param>
		public Event(ProcessContext context, string message, params string[] parms) : this(context, message, null, parms) { }

		/// <summary>
		/// Instantiates a new event bound  to a context.
		/// </summary>
		/// <param name="context">The context to which the event is bound.</param>
		/// <param name="message">The simple message the event represents.</param>
		/// <param name="obj">An object being carried by the event.</param>
		/// <param name="parms">
		/// A sequnce of context parameter names that should be copied from the context
		/// to this event.
		/// </param>
		public Event(ProcessContext context, string message, IData obj, params string[] parms) {
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

		/// <summary>
		/// Instantiates a new event as a copy of the event provided.
		/// </summary>
		/// <param name="ev">The event to copy for this new instance.</param>
		public Event(IEvent ev) {
			_context = ev.Context;
			_message = ev.Message;
			_object = ev.Object;
			_params = new Dictionary<string, string>(ev.Params);
		}

		/// <summary>
		/// Creates a clone of this event by copying
		/// it into a new instance.
		/// </summary>
		/// <returns>The newly cloned event.</returns>
		object ICloneable.Clone() {
			return new Event(this);
		}

		/// <summary>
		/// Creates a clone of this event by copying
		/// it into a new instance.
		/// </summary>
		/// <returns>The newly cloned event.</returns>
		public Event Clone() {
			return new Event(this);
		}

		/// <summary>
		/// Adds a key-value pair as a parameter to the event.
		/// </summary>
		/// <param name="key">The key of the parameter.</param>
		/// <param name="value">The value of the parameter.</param>
		public void Add(string key, string value) {
			this.Params.Add(key, value);
		}

		/// <summary>
		/// Fires the event on the context to which it is bound.
		/// </summary>
		/// <returns>
		/// Returns the event that has just been fired.
		/// </returns>
		public IEvent Fire() {
			return this.Context.Fire(this);
		}

		/// <summary>
		/// Determines whether or not the parameters 
		/// specified exist in the event.
		/// </summary>
		/// <param name="parms">The parameters to check for.</param>
		/// <returns>Returns true if all the parameters exist; otherwise return false.</returns>
		public bool HasParams(params string[] parms) {
			return parms.Length > 0 && parms.All(parm => this.Params.ContainsKey(parm));
		}

		/// <summary>
		/// Determines whether or not all the key-value pairs
		/// provided exist in the events parameters.
		/// </summary>
		/// <param name="match">The key-value pairs to check for.</param>
		/// <returns>
		/// Returns true if all the key-value pairs specified exists in the events
		/// parameters; otherwise returns false.
		/// </returns>
		public bool HasParamValues(IEnumerable<KeyValuePair<string, string>> match) {
			return match.All(entry => this.Params.Contains(entry));
		}

		/// <summary>
		/// Determines whether or not each of the prameters specified
		/// exist on the event, and creates an error for each one that
		/// does not.
		/// </summary>
		/// <param name="parms">The paramter names to check for.</param>
		/// <returns>
		/// Returns true if each of the parameters exist on the event;
		/// otherwise returns false.
		/// </returns>
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

		/// <summary>
		/// Creates a string representation of the event.
		/// </summary>
		/// <returns>Returns a new string representing the event.</returns>
		public override string ToString() {
			StringBuilder sb = new StringBuilder();

			sb.AppendFormat("(event @message {0}\n", this.Message);
			foreach (KeyValuePair<string, string> entry in this.Params) {
				sb.AppendFormat("   ({0} -{1})\n", entry.Key, entry.Value);
			}
			sb.AppendLine(")");

			return sb.ToString();
		}

		/// <summary>
		/// Obtains an enumerator for the events parameters.
		/// </summary>
		/// <returns>Returns an enumerator suitable for iterating through the events parameters.</returns>
		public IEnumerator<KeyValuePair<string, string>> GetEnumerator() {
			return this.Params.GetEnumerator();
		}

		/// <summary>
		/// Obtains an enumerator for the events parameters.
		/// </summary>
		/// <returns>Returns an enumerator suitable for iterating through the events parameters.</returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return this.Params.GetEnumerator();
		}

		/// <summary>
		/// Produces an xml representation of the model.
		/// </summary>
		/// <param name="xml">The writer to used to write the xml to. </param>
		public void ToXml(XmlWriter xml) {
			xml.WriteStartElement("event");
			xml.WriteAttributeString("message", this.Message);
			xml.WriteStartElement("params");
			foreach (KeyValuePair<string, string> entry in this.Params) {
				xml.WriteStartElement("item");
				xml.WriteAttributeString("name", entry.Key);
				xml.WriteAttributeString("value", entry.Value);
				xml.WriteEndElement();
			}
			xml.WriteEndElement();
			xml.WriteEndElement();
		}

		/// <summary>
		/// Produces a json respresentation of the model.
		/// </summary>
		/// <param name="json">The writer to use for producing json.</param>
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

		/// <summary>
		/// Creates a new event from an xml representation.
		/// </summary>
		/// <param name="context">The context to which the new event will be bound.</param>
		/// <param name="xml">The xml representation of an event.</param>
		/// <returns>Returns a new event.</returns>
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

		/// <summary>
		/// Creates a new event from an json representation.
		/// </summary>
		/// <param name="context">The context to which the new event will be bound.</param>
		/// <param name="json">The json representation of an event.</param>
		/// <returns>Returns a new event.</returns>
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

		/// <summary>
		/// An exception thrown when unable to parse the xml or json representations
		/// for creating a new event.
		/// </summary>
		public class ParseException : InvalidOperationException {
			/// <summary>
			/// Instantiates a new parse exception with a human readable message.
			/// </summary>
			/// <param name="message">The human readable message for the exception.</param>
			public ParseException(string message) : base(message) { }
			/// <summary>
			/// instantiates a new exception wrapping a provided inner exception,
			/// with a human readable message.
			/// </summary>
			/// <param name="message">The human readable message for the exception.</param>
			/// <param name="err">The inner exception to wrap.</param>
			public ParseException(string message, Exception err) : base(message, err) { }
		}
	}
}

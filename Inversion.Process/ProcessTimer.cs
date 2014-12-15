using System;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Inversion.Process {

	/// <summary>
	/// Represents a simplistic timer as a start and stop time
	/// as a pair of date time objects.
	/// </summary>
	/// <remarks>
	/// This is **NOT** suitable for adult timings, but introduces no
	/// overhead and is suitable for applications developers to be
	/// able to monitor basic timings of features to know if features
	/// are costing more time that expected or are going into distress.
	/// </remarks>
	public class ProcessTimer : IData {

		private DateTime _start;
		private DateTime _stop;

		/// <summary>
		/// Determines if this timer has been stopped or not.
		/// </summary>
		public bool HasStopped {
			get { return _stop != default(DateTime); }
		}

		/// <summary>
		/// The start time of the timer.
		/// </summary>
		public DateTime Start {
			get { return _start; }
		}

		/// <summary>
		/// The stop time of the timer.
		/// </summary>
		public DateTime Stop {
			get { return _stop; }
		}

		/// <summary>
		/// The time that has elapsed between
		/// the start and the stop times.
		/// </summary>
		public TimeSpan Duration {
			get { return (this.HasStopped) ? _stop - _start : DateTime.Now - _start; }
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
		/// Instances a new process timer, defaulting its
		/// start time as now.
		/// </summary>
		public ProcessTimer() : this(DateTime.Now) { }

		/// <summary>
		/// Instantiates a new process timer with
		/// the start time specified.
		/// </summary>
		/// <param name="start">The start time of the new timer.</param>
		public ProcessTimer(DateTime start) {
			_start = start;
			_stop = default(DateTime);
		}

		/// <summary>
		/// Instantiates a new process timer as a copy of
		/// a provied timer.
		/// </summary>
		/// <param name="timer"></param>
		public ProcessTimer(ProcessTimer timer) {
			_start = timer.Start;
			_stop = timer.Stop;
		}

		/// <summary>
		/// Clones this timer as a copy.
		/// </summary>
		/// <returns>Returns a new process timer.</returns>
		object ICloneable.Clone() {
			return new ProcessTimer(this);
		}

		/// <summary>
		/// Clones this timer as a copy.
		/// </summary>
		/// <returns>Returns a new process timer.</returns>
		public ProcessTimer Clone() {
			return new ProcessTimer(this);
		}

		/// <summary>
		/// Sets the start time of this timer to now.
		/// </summary>
		/// <returns>Returns this timer.</returns>
		public ProcessTimer Begin() {
			_start = DateTime.Now;
			return this;
		}

		/// <summary>
		/// Sets the stop time of this timer to now.
		/// </summary>
		/// <returns>Returns this process timer.</returns>
		public ProcessTimer End() {
			_stop = DateTime.Now;
			return this;
		}

		/// <summary>
		/// Produces an xml representation of the model.
		/// </summary>
		/// <param name="writer">The writer to used to write the xml to. </param>
		public void ToXml(XmlWriter writer) {
			writer.WriteStartElement("timer");
			writer.WriteAttributeString("start", _start.ToString());
			writer.WriteAttributeString("duration", this.Duration.Milliseconds.ToString());
			writer.WriteEndElement();
		}

		/// <summary>
		/// Produces a json respresentation of the model.
		/// </summary>
		/// <param name="writer">The writer to use for producing json.</param>
		public void ToJson(JsonWriter writer) {
			writer.WriteStartObject();
			writer.WritePropertyName("_type");
			writer.WriteValue("timer");
			writer.WritePropertyName("start");
			writer.WriteValue(_start.ToString());
			writer.WritePropertyName("duration");
			writer.WriteValue(this.Duration.Milliseconds.ToString());
			writer.WriteEndObject();
		}

	}
}

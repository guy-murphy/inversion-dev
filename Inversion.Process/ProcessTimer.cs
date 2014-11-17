using System;
using System.Xml;
using Newtonsoft.Json;

namespace Inversion.Process {
	public class ProcessTimer : IData {

		private DateTime _start;
		private DateTime _stop;

		public bool HasStopped {
			get { return _stop != default(DateTime); }
		}

		public DateTime Start {
			get { return _start; }
		}

		public DateTime Stop {
			get { return _stop; }
		}

		public TimeSpan Duration {
			get { return (this.HasStopped) ? _stop - _start : DateTime.Now - _start; }
		}

		public ProcessTimer() : this(DateTime.Now) { }

		public ProcessTimer(DateTime start) {
			_start = start;
			_stop = default(DateTime);
		}

		public ProcessTimer(ProcessTimer timer) {
			_start = timer.Start;
			_stop = timer.Stop;
		}

		object ICloneable.Clone() {
			return new ProcessTimer(this);
		}

		public ProcessTimer Clone() {
			return new ProcessTimer(this);
		}

		public ProcessTimer Begin() {
			_start = DateTime.Now;
			return this;
		}

		public ProcessTimer End() {
			_stop = DateTime.Now;
			return this;
		}

		public void ToXml(XmlWriter writer) {
			writer.WriteStartElement("timer");
			writer.WriteAttributeString("start", _start.ToString());
			writer.WriteAttributeString("duration", this.Duration.Milliseconds.ToString());
			writer.WriteEndElement();
		}

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

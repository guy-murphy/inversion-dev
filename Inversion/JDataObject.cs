using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Inversion {

	/// <summary>
	/// Implements a `JObject` as an `IData` type.
	/// </summary>
	/// <remarks>
	/// This is addressing a concern not disimilar to that being addressed by
	/// `DataView` which is the presentation of data in abstract terms
	/// especially for views or ad-hoc data.
	/// </remarks>
	public class JDataObject: JObject, IData {

		/// <summary>
		/// Instantiates a new `JDataObject` from an other `JObject`.
		/// </summary>
		/// <param name="other">
		/// The `JObject` to copy data from.
		/// </param>
		public JDataObject(JObject other): base(other) {}

		/// <summary>
		/// Instantiates a new `JDataObject` from another `IDataObject`.
		/// </summary>
		/// <param name="other">
		/// The `IData` object to copy data from.
		/// </param>
		public JDataObject(IData other) : base(other.ToJson()) { }

		/// <summary>
		/// Provides an abstract representation
		/// of the objects data expressed as a JSON object.
		/// </summary>
		public JObject Data { get { return this; } }

		/// <summary>
		/// Produces an xml representation of the model.
		/// </summary>
		/// <param name="writer">The writer to used to write the xml to. </param>
		public void ToXml(XmlWriter writer) {
			string rootName = this["_type"].Value<string>() ?? "record";
			XmlDocument xml = JsonConvert.DeserializeXmlNode(this.ToString(), rootName);
			xml.WriteTo(writer);
		}

		/// <summary>
		/// Produces a json respresentation of the model.
		/// </summary>
		/// <param name="writer">The writer to use for producing json.</param>
		public void ToJson(JsonWriter writer) {
			writer.WriteRaw(this.ToString());
		}

        public object Clone()
        {
			return base.DeepClone();
        }
    }
}

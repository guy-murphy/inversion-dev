using System;
using System.Xml;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Inversion {

	/// <summary>
	/// Represents a frozen view of an `IData` object.
	/// </summary>
	/// <remarks>
	/// The idea here is that if you need to be using the JSON representation
	/// of a mutable entity, it's going to be expensive to generate that JSON representation
	/// each time it is accessed. This applies to XML also, but the case is felt to be
	/// more likely with the JSON rep. So the purpose of the data-view is to take a snap-shot
	/// of the entity, with the JSON being generated only the once. Unfortunately `JObject` is
	/// muttable making it unfit for what is supposed to be an immutable view. A guard has been
	/// put in to throw an exception on property change for the JObject, but this is felt to be
	/// only just adequate long-term. I'm going to see how this plays out in actual usage
	/// before deciding if it's appropriate. See `JDataObject` for an alternative but similar approach.
	/// </remarks>
	public class DataView: IData {

		private readonly string _xml;
		private readonly string _json;
		private readonly JObject _data; // muttable object

		/// <summary>
		/// Provides an abstract representation
		/// of the objects data expressed as a JSON object.
		/// </summary>
		public JObject Data {
			get { return _data; }
		}

		/// <summary>
		/// Instantiates a new data view object.
		/// </summary>
		/// <param name="other">The `IData` the data view should be created from.</param>
		public DataView(IData other) {
			_xml = other.ToXml();
			_json = other.ToJson();
			_data = other.ToJsonObject();
			_data.PropertyChanged += (sender, args) => {
				throw new InvalidOperationException("You may not change the data representation participating in a data view.");
			};
		}


		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>
		/// A new object that is a copy of this instance.
		/// </returns>
		object ICloneable.Clone() {
			return new DataView(this);
		}

		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>
		/// A new object that is a copy of this instance.
		/// </returns>
		public DataView Clone() {
			return new DataView(this);
		}

		/// <summary>
		/// Produces an xml representation of the model.
		/// </summary>
		/// <param name="writer">The writer to used to write the xml to. </param>
		public void ToXml(XmlWriter writer) {
			writer.WriteRaw(_xml);
		}

		/// <summary>
		/// Produces a json respresentation of the model.
		/// </summary>
		/// <param name="writer">The writer to use for producing json.</param>
		public void ToJson(JsonWriter writer) {
			writer.WriteRaw(_json);
		}
	}
}

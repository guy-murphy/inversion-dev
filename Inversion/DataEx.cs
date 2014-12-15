using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Formatting = System.Xml.Formatting;

namespace Inversion {

	/// <summary>
	/// Extension methods for <see cref="IData"/>
	/// largely concerned with supporting both
	/// `.ToXml(...)` and `.ToJson(...)`
	/// </summary>

	public static class DataEx {

		/// <summary>
		/// Generates an XML representation of the specified <see cref="IData"/> object.
		/// </summary>
		/// <param name="self">The data model to produce XML for.</param>
		/// <returns>
		/// Returns the XML representation as a `string`.
		/// </returns>
		/// <remarks>
		/// This is implemented by creating a `StringWriter` and
		/// calling `.ToXml(IData, StringWriter)`
		/// </remarks>

		public static string ToXml(this IData self) {
			using (StringWriter str = new StringWriter()) {
				self.ToXml(str);
				return str.ToString();
			}
		}

		/// <summary>
		/// Produces an xml representation of the subject
		/// `IData` object.
		/// </summary>
		/// <param name="self">The `IData` object to act upon.</param>
		/// <param name="writer">The xml writer to write the representation to.</param>

		public static void ToXml(this IData self, TextWriter writer) {
			using (XmlTextWriter xml = new XmlTextWriter(writer)) {
				xml.Formatting = Formatting.Indented;
				self.ToXml(xml);
			}
		}

		/// <summary>
		/// Produces a json representation of the subject `IData` object.
		/// </summary>
		/// <param name="self">The `IData` object to act upon.</param>
		/// <returns>Return the json representation of the `IData` object as a string.</returns>
		public static string ToJson(this IData self) {
			using (StringWriter str = new StringWriter()) {
				self.ToJson(str);
				return str.ToString();
			}
		}

		/// <summary>
		/// Produces a json representation of the subject `IData` object.
		/// </summary>
		/// <param name="self">The `IData` object to act upon.</param>
		/// <param name="writer">The text writer the representation should be writtern to.</param>
		public static void ToJson(this IData self, TextWriter writer) {
			using (JsonTextWriter json = new JsonTextWriter(writer)) {
				json.Formatting = Newtonsoft.Json.Formatting.Indented;
				self.ToJson(json);
			}
		}

		/// <summary>
		/// Provides a JSON Object view of the objects data.
		/// </summary>
		/// <param name="self">The `IData` object to act upon.</param>
		/// <returns>
		/// Returns a `JObject` representation of this objects data.
		/// </returns>
		public static JObject ToJsonObject(this IData self) {
			return JObject.Parse(self.ToJson());
		}


	}
}

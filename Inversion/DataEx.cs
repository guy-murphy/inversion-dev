using System.IO;
using System.Xml;
using Newtonsoft.Json;
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
		/// 
		/// </summary>
		/// <param name="self"></param>
		/// <param name="writer"></param>

		public static void ToXml(this IData self, TextWriter writer) {
			using (XmlTextWriter xml = new XmlTextWriter(writer)) {
				xml.Formatting = Formatting.Indented;
				self.ToXml(xml);
			}
		}

		public static string ToJson(this IData self) {
			using (StringWriter str = new StringWriter()) {
				self.ToJson(str);
				return str.ToString();
			}
		}

		public static void ToJson(this IData self, TextWriter writer) {
			using (JsonTextWriter json = new JsonTextWriter(writer)) {
				json.Formatting = Newtonsoft.Json.Formatting.Indented;
				self.ToJson(json);
			}
		}


	}
}

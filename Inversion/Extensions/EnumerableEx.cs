using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;

namespace Inversion.Extensions {
	public static class EnumerableEx {

		public static string ToXml<T>(this IEnumerable<T> self, string label) where T : IData {
			using (StringWriter str = new StringWriter()) {
				self.ToXml(str, label);
				return str.ToString();
			}
		}

		public static void ToXml<T>(this IEnumerable<T> self, TextWriter writer, string label) where T : IData {
			using (XmlTextWriter xml = new XmlTextWriter(writer)) {
				xml.Formatting = Formatting.Indented;
				self.ToXml(xml, label);
			}
		}

		public static void ToXml<T>(this IEnumerable<T> self, XmlWriter xml, string label) where T : IData {
			xml.WriteStartElement(label);
			foreach (T item in self) {
				item.ToXml(xml);
			}
			xml.WriteEndElement();
		}

		public static string ToJson<T>(this IEnumerable<T> self) where T : IData {
			using (StringWriter str = new StringWriter()) {
				self.ToJson(str);
				return str.ToString();
			}
		}

		public static void ToJson<T>(this IEnumerable<T> self, TextWriter writer) where T : IData {
			using (JsonTextWriter json = new JsonTextWriter(writer)) {
				json.Formatting = Newtonsoft.Json.Formatting.Indented;
				self.ToJson(json);
			}
		}

		public static void ToJson<T>(this IEnumerable<T> self, JsonWriter json) where T : IData {
			json.WriteStartArray();
			foreach (T item in self) {
				item.ToJson(json);
			}
			json.WriteEndArray();
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;

namespace Inversion.Data {
	public static class StreamEx {

		public static XmlReader AsXmlReader(this Stream self) {
			XmlTextReader reader = new XmlTextReader(self);
			return reader;
		}

		public static XmlDocument AsXmlDocument(this Stream self) {
			using (self) {
				XmlDocument document = new XmlDocument();
				document.Load(self);
				return document;
			}
		}

		public static XslCompiledTransform AsXslDocument(this Stream self) {
			using (self) {
				XslCompiledTransform xsl = new XslCompiledTransform(true);
				xsl.Load(self.AsXmlReader());
				return xsl;
			}
		}

		public static string AsText(this Stream self) {
			using (self) {
				TextReader reader = new StreamReader(self);
				string text = reader.ReadToEnd();
				return text;
			}
		}

	}
}

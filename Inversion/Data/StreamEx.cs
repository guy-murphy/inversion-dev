using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;

namespace Inversion.Data {

	/// <summary>
	/// Extensions for stream intended to work with `IResourceAdapter`.
	/// </summary>
	public static class StreamEx {

		/// <summary>
		/// Opens an xml reader on the stream.
		/// </summary>
		/// <param name="self">The stream being acted on.</param>
		/// <returns>Returns an xml reader for the stream.</returns>
		/// <remarks>
		/// Does not close or dispose of the stream. Disposing the xml reader
		/// will dispose the stream.
		/// </remarks>
		public static XmlReader AsXmlReader(this Stream self) {
			XmlTextReader reader = new XmlTextReader(self);
			return reader;
		}

		/// <summary>
		/// Loads the stream into an xml document and disposes of the stream.
		/// </summary>
		/// <param name="self">The stream being acted on.</param>
		/// <returns>Returns an xml document with the stream loaded.</returns>
		public static XmlDocument AsXmlDocument(this Stream self) {
			using (self) {
				XmlDocument document = new XmlDocument();
				document.Load(self);
				return document;
			}
		}

		/// <summary>
		/// Loads the stream into an xsl document and disposes of the stream.
		/// </summary>
		/// <param name="self">The stream being acted on.</param>
		/// <returns>Returns an xsl document with the stram loaded.</returns>
		public static XslCompiledTransform AsXslDocument(this Stream self) {
			using (self) {
				XslCompiledTransform xsl = new XslCompiledTransform(true);
				xsl.Load(self.AsXmlReader());
				return xsl;
			}
		}

		/// <summary>
		/// Reads the contents of the stream as text, and disposes of the stream.
		/// </summary>
		/// <param name="self">The stream being acted on.</param>
		/// <returns>Returns the contents of the stream as text.</returns>
		public static string AsText(this Stream self) {
			using (self) {
				TextReader reader = new StreamReader(self);
				string text = reader.ReadToEnd();
				return text;
			}
		}

	}
}

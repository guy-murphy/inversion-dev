using System.Xml;
using Spring.Objects.Factory.Support;
using Spring.Objects.Factory.Xml;

namespace Inversion.Spring {
	/// <summary>
	/// Implements a parser that is able to parse view behaviour definations
	/// in a Spring XML object config.
	/// </summary>
	public class ViewBehaviourObjectDefinationParser : AbstractSimpleObjectDefinitionParser {

		/// <summary>
		/// Gets a value indicating whether an ID should be generated instead 
		/// if the passed in XmlElement does not specify an "id" attribute explicitly. 
		/// </summary>
		protected override bool ShouldGenerateIdAsFallback {
			get { return true; }
		}

		/// <summary>
		/// Obtains the fully qualified type name of the object represented
		/// by the XML element.
		/// </summary>
		/// <param name="element">The object representation in XML.</param>
		/// <returns>
		/// Returns the fully qualified type name for the object being described.
		/// </returns>
		protected override string GetObjectTypeName(XmlElement element) {
			return element.GetAttribute("type");
		}

		/// <summary>
		/// Parse the supplied XmlElement and populate the supplied ObjectDefinitionBuilder as required.
		/// </summary>
		/// <param name="xml">The obejct representation in XML.</param>
		/// <param name="builder">The builder used to build the object defination in Spring.</param>
		protected override void DoParse(XmlElement xml, ObjectDefinitionBuilder builder) {
			// all behaviours with config being parsed have @respondsTo
			string respondsTo = xml.GetAttribute("responds-to");
			builder.AddConstructorArg(respondsTo);

			// all view behaviours have @content-type
			string contentType = xml.GetAttribute("content-type");
			builder.AddConstructorArg(contentType);
		}

	}
}

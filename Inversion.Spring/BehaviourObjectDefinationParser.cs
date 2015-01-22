using System;
using System.Collections.Generic;
using System.Xml;
using Inversion.Process;
using Spring.Objects.Factory.Support;
using Spring.Objects.Factory.Xml;


namespace Inversion.Spring {

	/// <summary>
	/// Implements a parser that is able to parse behaviour definations
	/// in a Spring XML object config.
	/// </summary>
	public class BehaviourObjectDefinationParser : AbstractSimpleObjectDefinitionParser {

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
			if (xml == null) throw new ArgumentNullException("xml", "The object description provided is null.");
			if (xml.OwnerDocument == null)  throw new ArgumentException("The xml provided to parse must have an owning document to obtain namespace information from.");

			// all behaviours with config being parsed have @respondsTo
			string respondsTo = xml.GetAttribute("responds-to");
			builder.AddConstructorArg(respondsTo);

			// now we're going to read any config defined within our behaviour identified
			// by its namespace "Inversion.Process.Behaviour"
			HashSet<IConfigurationElement> elements = new HashSet<IConfigurationElement>();
			XmlNamespaceManager ns = new XmlNamespaceManager(xml.OwnerDocument.NameTable);
			ns.AddNamespace("inv", "Inversion.Process.Behaviour");
			XmlNodeList frames = xml.SelectNodes("inv:*", ns);

			// do we have any config then?
			if (frames != null && frames.Count > 0) {
				int ordinal = 0;

				// we're going to read the config into tuples
				// of frame, slot, name, value
				foreach (XmlElement frameElement in frames) {
					string frame = frameElement.Name;

					// process any frame attributes as <frame slot="name" />
					foreach (XmlAttribute pair in frameElement.Attributes) {
						string slot = pair.Name;
						string name = pair.Value;
						IConfigurationElement element = new Configuration.Element(ordinal, frame, slot, name, String.Empty);
						elements.Add(element);
						ordinal++;
					}

					foreach (XmlElement slotElement in frameElement.ChildNodes) {
						string slot = slotElement.Name;

						int start = elements.Count;
						// read children of slot as <name>value</name>
						foreach (XmlElement pair in slotElement.ChildNodes) {
							string name = pair.Name;
							string value = pair.InnerText;
							IConfigurationElement element = new Configuration.Element(ordinal, frame, slot, name, value);
							elements.Add(element);
							ordinal++;
						}
						// read attributes of slot as name="value"
						foreach (XmlAttribute pair in slotElement.Attributes) {
							string name = pair.Name;
							string value = pair.Value;
							IConfigurationElement element = new Configuration.Element(ordinal, frame, slot, name, value);
							elements.Add(element);
							ordinal++;
						}

						if (elements.Count == start) { // the slot had no name/value pairs
							IConfigurationElement element = new Configuration.Element(ordinal, frame, slot, String.Empty, String.Empty);
							elements.Add(element);
							ordinal++;
						}
					}
				}
				builder.AddConstructorArg(elements);
			}
		}

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Inversion.Process.Behaviour;
using Spring.Objects.Factory.Support;
using Spring.Objects.Factory.Xml;
using Spring.Util;

namespace Inversion.Spring {
	public class BehaviourObjectDefinationParser : AbstractSimpleObjectDefinitionParser {

		protected override bool ShouldGenerateIdAsFallback {
			get { return true; }
		}
		
		protected override string GetObjectTypeName(XmlElement element) {
			return element.GetAttribute("type");
		}

		protected override void DoParse(XmlElement xml, ObjectDefinitionBuilder builder) {
			string respondsTo = xml.GetAttribute("responds-to");
			builder.AddConstructorArg(respondsTo);

			HashSet<BehaviourConfiguration.Element> elements = new HashSet<BehaviourConfiguration.Element>();
			XmlNamespaceManager ns = new XmlNamespaceManager(xml.OwnerDocument.NameTable);
			ns.AddNamespace("inv", "Inversion.Process.Behaviour");
			XmlNodeList frames = xml.SelectNodes("inv:*", ns);
			if (frames != null) {
				foreach (XmlElement frameElement in frames) {
					string frame = frameElement.Name;

					foreach (XmlElement slotElement in frameElement.ChildNodes) {
						string slot = slotElement.Name;

						int start = elements.Count;

						// read children of slot as <name>value</name>
						foreach (XmlElement pair in slotElement.ChildNodes) {
							string name = pair.Name;
							string value = pair.InnerText;
							BehaviourConfiguration.Element element = new BehaviourConfiguration.Element(frame, slot, name, value);
							elements.Add(element);
						}
						// read attributes of slot as name="value"
						foreach (XmlAttribute pair in slotElement.Attributes) {
							string name = pair.Name;
							string value = pair.Value;
							BehaviourConfiguration.Element element = new BehaviourConfiguration.Element(frame, slot, name, value);
							elements.Add(element);
						}

						if (elements.Count == start) { // the slot had no name/value pairs
							BehaviourConfiguration.Element element = new BehaviourConfiguration.Element(frame, slot, String.Empty, String.Empty);
							elements.Add(element);
						}
					}
				}
			}
			builder.AddConstructorArg(elements);
		}

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			XmlNodeList config = xml.SelectNodes("inv:config/*/*", ns);
			if (config != null) {
				foreach (XmlElement node in config) {
					string value = node.InnerText;
					string name = node.Name;
					string scope = node.ParentNode.Name;
					BehaviourConfiguration.Element element = new BehaviourConfiguration.Element(scope, name, value);
					elements.Add(element);
				}
			}
			builder.AddConstructorArg(elements);
		}

	}
}

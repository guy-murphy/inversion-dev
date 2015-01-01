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

		protected override void DoParse(XmlElement element, ObjectDefinitionBuilder builder) {
			string respondsTo = element.GetAttribute("responds-to");
			builder.AddConstructorArg(respondsTo);
		}

	}
}

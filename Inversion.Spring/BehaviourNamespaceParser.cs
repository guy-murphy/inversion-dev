using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Spring.Objects.Factory.Xml;

namespace Inversion.Spring {
	/// <summary>
	/// Registers tags to be used in configuration with the class that will process
	/// those tags.
	/// </summary>
	[NamespaceParser(
		Namespace = "Inversion.Process.Behaviour", 
		SchemaLocationAssemblyHint = typeof(BehaviourNamespaceParser), 
		SchemaLocation = "/Inversion.Spring/behaviour.xsd"
	)]
	public class BehaviourNamespaceParser : NamespaceParserSupport {
		/// <summary>
		/// Invoked by <see cref="T:Spring.Objects.Factory.Xml.NamespaceParserRegistry"/> after construction but before any
		///             elements have been parsed.
		/// </summary>
		public override void Init() {

			this.RegisterObjectDefinitionParser("configure", new BehaviourObjectDefinationParser());
			this.RegisterObjectDefinitionParser("message-trace", new BehaviourObjectDefinationParser());
			this.RegisterObjectDefinitionParser("message-sequence", new BehaviourObjectDefinationParser());

		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inversion.Process.Behaviour {
	public class BehaviourConfiguration : HashSet<BehaviourConfiguration.Element> {

		public class Element : Tuple<string, string, string> {
			/// <summary>
			/// Initializes a new instance of the element class.
			/// </summary>
			/// <param name="scope">The value of the tuple's first component.</param>
			/// <param name="name">The value of the tuple's second component.</param>
			/// <param name="value">The value of the tuple's third component.</param>
			public Element(string scope, string name, string value) : base(scope, name, value) { }
		}

	}
}

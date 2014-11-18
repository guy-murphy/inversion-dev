using System.Collections.Generic;
using System.Collections.Immutable;

using Inversion.Process;

namespace Inversion.Web.Behaviour {
	public class BootstrapBehaviour : WebBehaviour {

		private readonly ImmutableDictionary<string, string> _params;

		protected IDictionary<string, string> Parameters {
			get { return _params; }
		}

		public BootstrapBehaviour(string name, IEnumerable<KeyValuePair<string, string>> parms)
			: base(name) {
			_params = ImmutableDictionary.CreateRange<string, string>(parms);
		}

		public override void Action(IEvent ev, WebContext context) {
			context.Params.Import(this.Parameters);
		}
	}
}

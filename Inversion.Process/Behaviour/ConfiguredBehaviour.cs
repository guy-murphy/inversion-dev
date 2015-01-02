using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inversion.Process.Behaviour {
	public abstract class ConfiguredBehaviour: ProcessBehaviour {

		private readonly BehaviourConfiguration _config;

		public BehaviourConfiguration Configuration {
			get { return _config; }
		}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The name of the behaviour.</param>
		/// <param name="config">Configuration for the behaviour.</param>
		protected ConfiguredBehaviour(string respondsTo, BehaviourConfiguration config) : base(respondsTo) {
			_config = config;
		}

		protected ConfiguredBehaviour(string respondsTo, IEnumerable<BehaviourConfiguration.Element> config) : base(respondsTo) {
			_config = new BehaviourConfiguration(config);
		}

	}
}

using System.Collections.Generic;

namespace Inversion.Process.Behaviour {
	public interface IConfiguredBehaviour: IProcessBehaviour {
		/// <summary>
		/// Exposes the configuration of the behaviour for querying.
		/// </summary>
		BehaviourConfiguration Configuration { get; }
	}

	/// <summary>
	/// A behaviour that can be configured.
	/// </summary>
	public abstract class ConfiguredBehaviour: ProcessBehaviour, IConfiguredBehaviour {

		private readonly BehaviourConfiguration _config;

		/// <summary>
		/// Exposes the configuration of the behaviour for querying.
		/// </summary>
		public BehaviourConfiguration Configuration {
			get { return _config; }
		}

		/// <summary>
		/// Creates a new instance of the behaviour with no configuration.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		protected ConfiguredBehaviour(string respondsTo) : this(respondsTo, new BehaviourConfiguration()) {}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		/// <param name="config">Configuration for the behaviour.</param>
		protected ConfiguredBehaviour(string respondsTo, BehaviourConfiguration config) : base(respondsTo) {
			_config = config;
		}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		/// <param name="config">Configuration for the behaviour.</param>
		protected ConfiguredBehaviour(string respondsTo, IEnumerable<BehaviourConfiguration.Element> config) : base(respondsTo) {
			_config = new BehaviourConfiguration(config);
		}

	}
}

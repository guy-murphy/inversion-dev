using System.Collections.Generic;

namespace Inversion.Process.Behaviour {
	/// <summary>
	/// A behaviour that can be configured.
	/// </summary>
	public abstract class ConfiguredBehaviour: ProcessBehaviour, IConfiguredBehaviour {

		private readonly IConfiguration _config;

		/// <summary>
		/// Exposes the configuration of the behaviour for querying.
		/// </summary>
		public IConfiguration Configuration {
			get { return _config; }
		}

		/// <summary>
		/// Creates a new instance of the behaviour with no configuration.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		protected ConfiguredBehaviour(string respondsTo) : this(respondsTo, new Configuration()) {}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		/// <param name="config">Configuration for the behaviour.</param>
		protected ConfiguredBehaviour(string respondsTo, IConfiguration config) : base(respondsTo) {
			_config = config;
		}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		/// <param name="config">Configuration for the behaviour.</param>
		protected ConfiguredBehaviour(string respondsTo, IEnumerable<IConfigurationElement> config) : base(respondsTo) {
			_config = new Configuration(config);
		}

	}
}

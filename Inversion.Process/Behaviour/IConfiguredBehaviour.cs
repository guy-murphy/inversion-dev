namespace Inversion.Process.Behaviour {
	/// <summary>
	/// Described a behaviour that has a configuration.
	/// </summary>
	public interface IConfiguredBehaviour : IProcessBehaviour {
		/// <summary>
		/// Exposes the configuration of the behaviour for querying.
		/// </summary>
		BehaviourConfiguration Configuration { get; }
	}
}

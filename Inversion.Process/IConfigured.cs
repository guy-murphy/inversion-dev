namespace Inversion.Process {
	/// <summary>
	/// Expresses access to structured configuration for a
	/// component suitable for querying.
	/// </summary>
	public interface IConfigured {
		/// <summary>
		/// Provices access to component configuration stuiable for querying.
		/// </summary>
		Configuration Configuration { get; }
	}
}

using System.Collections.Generic;

namespace Inversion.Web {

	/// <summary>
	/// Describes a collection of files for a request.
	/// </summary>
	public interface IRequestFilesCollection : IEnumerable<KeyValuePair<string, IRequestFile>> {
		/// <summary>
		/// Provides access to request files by key index.
		/// </summary>
		/// <param name="key">The key of the request file.</param>
		/// <returns>Returns the request file under the specified key.</returns>
		IRequestFile this[string key] { get; }
		/// <summary>
		/// Gets the request file of the specified key.
		/// </summary>
		/// <param name="key">The key of the request file.</param>
		/// <returns>Returns the request file under the specified key.</returns>
		IRequestFile Get(string key);
		/// <summary>
		/// Tries to get the response file of the specified key.
		/// </summary>
		/// <param name="key">The key of the request file.</param>
		/// <param name="value">The request file reference any found file should be assigned to.</param>
		/// <returns>Returns true if the specified file was found; otherwise, returns false.</returns>
		bool TryGetValue(string key, out IRequestFile value);
	}
}

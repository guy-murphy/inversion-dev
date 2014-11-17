using System.Collections.Generic;

namespace Inversion.Collections {

	/// <summary>
	/// Represents a generic dictionary that
	/// implements <see cref="IData"/>, where the keys are strings.
	/// </summary>
	/// <typeparam name="T">The type of the element values.</typeparam>

	public interface IDataDictionary<T> : IDictionary<string, T>, IData {

		/// <summary>
		/// Import the provided key/value pairs into the dictionary.
		/// </summary>
		/// <param name="other">The key/value pairs to copy.</param>
		/// <returns>
		/// This dictionary.
		/// </returns>

		IDataDictionary<T> Import(IEnumerable<KeyValuePair<string, T>> other);
	}
}

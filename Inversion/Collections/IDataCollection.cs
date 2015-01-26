using System.Collections.Generic;

namespace Inversion.Collections {

	/// <summary>
	/// Represents a collection of <see cref="IData"/>  objects,
	/// that can be accessed by index.
	/// </summary>
	///<typeparam name="T">The type of elements in the list.</typeparam>
	public interface IDataCollection<T> : ICollection<T>, IData {
		/// <summary>
		/// The label that should be used for the collection in
		/// any notation presenting the collection. 
		/// </summary>
		string Label { get; }
		
	}

}

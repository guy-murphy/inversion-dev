using System.Collections;
using System.Collections.Generic;

namespace Inversion.Collections {

	/// <summary>
	/// Represents a collection of <see cref="IData"/>  objects,
	/// that can be accessed by index.
	/// </summary>
	///<typeparam name="T">The type of elements in the list.</typeparam>
	public interface IDataCollection<T> : IList<T>, IList, IData {
		string Label { get; }
	}

}

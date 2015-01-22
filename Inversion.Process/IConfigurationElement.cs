namespace Inversion.Process {
	public interface IConfigurationElement {
		/// <summary>
		/// The order or position which this element occupies
		/// relative to its siblings.
		/// </summary>
		int Ordinal { get; }

		/// <summary>
		/// The frame of this element.
		/// </summary>
		string Frame { get; }

		/// <summary>
		/// The slot of this element.
		/// </summary>
		string Slot { get; }

		/// <summary>
		/// The name of this element.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// The value of this element.
		/// </summary>
		string Value { get; }

		bool Equals(object obj);
		int GetHashCode();
		string ToString();
	}
}
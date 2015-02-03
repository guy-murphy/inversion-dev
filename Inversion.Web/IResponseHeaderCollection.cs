namespace Inversion.Web {
	/// <summary>
	/// Describes a collection of response headers.
	/// </summary>
	/// <remarks>
	/// This is a stub for minimal functionality and requires building out.
	/// </remarks>
	public interface IResponseHeaderCollection {
		/// <summary>
		/// Appeands a header to the response.
		/// </summary>
		/// <param name="key">The key of the header.</param>
		/// <param name="value">The value of the header.</param>
		void Append(string key, string value);
	}
}

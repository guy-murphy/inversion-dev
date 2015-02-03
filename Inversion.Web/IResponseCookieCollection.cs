namespace Inversion.Web {
	/// <summary>
	/// Describes a collection of response cookies.
	/// </summary>
	/// <remarks>
	/// This is a stub of minimal functionality that needs matured and fleshed out.
	/// </remarks>
	public interface IResponseCookieCollection {
		/// <summary>
		/// Appends a cookie with the specified key, value and options to the response.
		/// </summary>
		/// <param name="key">The key of the cookie.</param>
		/// <param name="value">The value of the cookie.</param>
		/// <param name="options">The cookies options.</param>
		void Append(string key, string value, CookieOptions options);
		/// <summary>
		/// Appends a cookie with the specified key and value to the response.
		/// </summary>
		/// <param name="key">The key of the cookie.</param>
		/// <param name="value">The value of the cookie.</param>
		void Append(string key, string value);
		/// <summary>
		/// Removes the cookies of the specified key from the response.
		/// </summary>
		/// <param name="key">The key of the cookie to remove.</param>
		void Delete(string key);
	}
}

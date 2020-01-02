using System;

namespace Inversion.Web {

	/// <summary>
	/// A builder class used to describe the properties of
	/// a cookie.
	/// </summary>
	public class CookieOptions {
		/// <summary>
		/// The path of the cookie.
		/// </summary>
		public string Path { get; set; }
		/// <summary>
		/// The domain of the cookie.
		/// </summary>
		public string Domain { get; set; }

		/// <summary>
		/// Whether or not the cookie is http only.
		/// </summary>
		public bool HttpOnly { get; set; }
		/// <summary>
		/// Whether or not the cookie is secured.
		/// </summary>
		public bool Secure { get; set; }

	    public enum CookieOptionSameSite
	    {
            None,
            Lax,
            Strict
	    }

        public CookieOptionSameSite SameSite { get; set; }

		/// <summary>
		/// When the cookie should expire.
		/// </summary>
		public DateTime? Expires { get; set; }

		/// <summary>
		/// Creates a new cookie options object with the default path of "/".
		/// </summary>
		public CookieOptions() : this("/") {}

		/// <summary>
		/// Creates a new cookie options object with the path specified.
		/// </summary>
		/// <param name="path">The path of the cookie.</param>
		public CookieOptions(string path) {
			this.Path = path;
		}

	}
}

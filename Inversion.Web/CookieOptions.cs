using System;

namespace Inversion.Web {
	public class CookieOptions {
		public string Path { get; set; }
		public string Domain { get; set; }

		public bool HttpOnly { get; set; }
		public bool Secure { get; set; }

		public DateTime? Expires { get; set; }

		public CookieOptions() : this("/") {}

		public CookieOptions(string path) {
			this.Path = path;
		}

	}
}

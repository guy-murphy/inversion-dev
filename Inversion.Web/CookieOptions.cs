using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inversion.Web {
	public class CookieOptions {

		private readonly string _path;

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

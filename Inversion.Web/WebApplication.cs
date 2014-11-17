using System;
using System.Web;

namespace Inversion.Web {
	public class WebApplication : HttpApplication {

		private readonly string _baseDirectory;


		public string BaseDirectory {
			get {
				return _baseDirectory;
			}
		}

		public WebApplication() {
			_baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
		}


	}
}

using System;
using System.Web;

namespace Inversion.Web {

	/// <summary>
	/// Represents a running web application application.
	/// </summary>
	public class WebApplication : HttpApplication {

		private readonly string _baseDirectory;

		/// <summary>
		/// The base directory from which the application is running.
		/// </summary>
		public string BaseDirectory {
			get {
				return _baseDirectory;
			}
		}

		/// <summary>
		/// Instantiates a new web application, defaulting to the base directory
		/// of the current app domain.
		/// </summary>
		public WebApplication() {
			_baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
		}


	}
}

using System;
using System.Net;

namespace Inversion.Web {
	public class WebException : ApplicationException {

		private readonly HttpStatusCode _status;

		public HttpStatusCode Status {
			get {
				return _status;
			}
		}

		public WebException(string message) : this(HttpStatusCode.InternalServerError, message) { }

		public WebException(HttpStatusCode status, string message)
			: base(message) {
			_status = status;
		}
	}
}

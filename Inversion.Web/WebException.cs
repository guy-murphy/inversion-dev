using System;
using System.Net;

namespace Inversion.Web {

	/// <summary>
	/// An exception that is thrown when an general error occurs
	/// within a web application that would correspond to a http status code.
	/// </summary>
	public class WebException : ApplicationException {

		private readonly HttpStatusCode _status;

		/// <summary>
		/// The http status code that should be produced for this error
		/// if it is unhandled and recovered from.
		/// </summary>
		public HttpStatusCode Status {
			get {
				return _status;
			}
		}

		/// <summary>
		/// Instantiates a new web exception with the message provided.
		/// </summary>
		/// <param name="message">The message to be output if the exception is unhandled.</param>
		/// <remarks>Defaults the status code to 500.</remarks>
		public WebException(string message) : this(HttpStatusCode.InternalServerError, message) { }

		/// <summary>
		/// Instantiates a new web exception with the status code and message provided.
		/// </summary>
		/// <param name="status">The status code that should be produced for this error if it is unhandled.</param>
		/// <param name="message">The message that should be produced for this error if it is unhandled.</param>
		public WebException(HttpStatusCode status, string message)
			: base(message) {
			_status = status;
		}
	}
}

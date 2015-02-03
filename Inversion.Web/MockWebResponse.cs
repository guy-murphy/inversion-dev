using System;
using System.IO;
using System.Text;
using System.Web;

namespace Inversion.Web {
	
	/// <summary>
	/// Implements a mock MockWebResponse for use with MockWebContext
	/// to facilitate testing.
	/// </summary>
	public class MockWebResponse: IWebResponse {

		private readonly StringBuilder _output = new StringBuilder();

		/// <summary>
		/// The end result of anything that's been written to the mock response.
		/// </summary>
		public string Result {
			get { return _output.ToString(); }
		}

		/// <summary>
		/// The text writer used for writing to the response stream.
		/// </summary>
		public TextWriter Output {
			get { throw new NotImplementedException("The output stream has not been mocked."); }
		}

		/// <summary>
		/// The response stream.
		/// </summary>
		public Stream OutputStream {
			get { throw new NotImplementedException("The output stream has not been mocked."); }
		}

		/// <summary>
		/// The status code of the response.
		/// </summary>
		public int StatusCode { get; set; }

		/// <summary>
		/// The status description of the response stream.
		/// </summary>
		public string StatusDescription { get; set; }

		/// <summary>
		/// The content type of the response stream.
		/// </summary>
		public string ContentType { get; set; }

		/// <summary>
		/// Access to the response cookies.
		/// </summary>
		public IResponseCookieCollection Cookies {
			get { throw new NotImplementedException("The HttpCookieCollection has not been mocked."); }
		}

		/// <summary>
		/// Flushes the response steam and ends the response.
		/// </summary>
		public void End() {
			// no side-effect
		}

		/// <summary>
		/// Writes the provided text to the response stream.
		/// </summary>
		/// <param name="text">The text to write to the response stream.</param>
		public void Write(string text) {
			_output.Append(text);
		}

		/// <summary>
		/// Writes the provided formatted text to the response stream.
		/// </summary>
		/// <param name="text">The text to write to the response stream.</param>
		/// <param name="args">The arguments to interpolate into the text.</param>
		public void WriteFormat(string text, params object[] args) {
			_output.AppendFormat(text, args);
		}

		/// <summary>
		/// Redirects the request to the provided url.
		/// </summary>
		/// <param name="url">The url to redirect to.</param>
		public void Redirect(string url) {
			// no side-effect
		}

		/// <summary>
		/// Redirects the request permanently to the provided url
		/// issuing a `301` in the response.
		/// </summary>
		/// <param name="url"></param>
		public void PermanentRedirect(string url) {
			// no side-effect
		}
	}
}

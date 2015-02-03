using System.IO;

using Microsoft.Owin;

namespace Inversion.Web.Owin {

	/// <summary>
	/// Implements a web response for OWIN
	/// </summary>
	public class OwinResponse: IWebResponse {

		private readonly IOwinResponse _underlyingResponse;

		private IResponseCookieCollection _cookies;
		private IResponseHeaderCollection _headers;
		private TextWriter _writer;

		/// <summary>
		/// The underlying http response being wrapped.
		/// </summary>
		protected IOwinResponse UnderlyingResponse {
			get {
				return _underlyingResponse;
			}
		}

		/// <summary>
		/// The text writer used for writing to the response stream.
		/// </summary>
		public TextWriter Output {
			get { return _writer ?? (_writer = new StreamWriter(this.OutputStream)); }
		}

		/// <summary>
		/// The response stream.
		/// </summary>
		public Stream OutputStream {
			get { return this.UnderlyingResponse.Body; }
		}

		/// <summary>
		/// The status code of the response.
		/// </summary>
		public int StatusCode {
			get { return this.UnderlyingResponse.StatusCode; }
			set { this.UnderlyingResponse.StatusCode = value; }
		}

		/// <summary>
		/// The status description of the response stream.
		/// </summary>
		public string StatusDescription {
			get { return this.UnderlyingResponse.ReasonPhrase; }
			set { this.UnderlyingResponse.ReasonPhrase = value; }
		}

		/// <summary>
		/// The content type of the response stream.
		/// </summary>
		public string ContentType {
			get { return this.UnderlyingResponse.ContentType; }
			set { this.UnderlyingResponse.ContentType = value; }
		}

		/// <summary>
		/// Access to the response cookies.
		/// </summary>
		public IResponseCookieCollection Cookies {
			get {
				if (_cookies == null) {
					_cookies = new OwinResponseCookieCollection(this.UnderlyingResponse.Cookies);
				}
				return _cookies;
			}
		}

		/// <summary>
		/// Access to the response headers.
		/// </summary>
		public IResponseHeaderCollection Headers {
			get {
				if (_headers == null) {
					_headers = new OwinResponseHeadersCollection(this.UnderlyingResponse);
				}
				return _headers;
			}
		}

		public OwinResponse(IOwinResponse underlyingResponse) {
			_underlyingResponse = underlyingResponse;
		}

		/// <summary>
		/// Flushes the response steam and ends the response.
		/// </summary>
		public void End() {
			// nothing to do on an OWIN implementation that is apparent to me at this point.
		}

		/// <summary>
		/// Writes the provided text to the response stream.
		/// </summary>
		/// <param name="text">The text to write to the response stream.</param>
		public void Write(string text) {
			this.UnderlyingResponse.Write(text);
		}

		/// <summary>
		/// Writes the provided formatted text to the response stream.
		/// </summary>
		/// <param name="text">The text to write to the response stream.</param>
		/// <param name="args">The arguments to interpolate into the text.</param>
		public void WriteFormat(string text, params object[] args) {
			_underlyingResponse.Write(string.Format(text, args));
		}

		/// <summary>
		/// Redirects the request to the provided url.
		/// </summary>
		/// <param name="url">The url to redirect to.</param>
		public void Redirect(string url) {
			this.UnderlyingResponse.Redirect(url);
		}

		/// <summary>
		/// Redirects the request permanently to the provided url
		/// issuing a `301` in the response.
		/// </summary>
		/// <param name="url"></param>
		public void PermanentRedirect(string url) {
			this.StatusCode = 301;
			this.StatusDescription = "301 Moved Permanently";
			this.UnderlyingResponse.Headers.Append("Location", url);
		}
	}
}

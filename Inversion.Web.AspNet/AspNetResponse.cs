using System.IO;
using System.Web;

namespace Inversion.Web.AspNet {
	public class AspNetResponse: IWebResponse {

		private readonly HttpResponse _underlyingResponse;

		private IResponseCookieCollection _cookies;

		/// <summary>
		/// The underlying http response being wrapped.
		/// </summary>
		protected HttpResponse UnderlyingResponse {
			get {
				return _underlyingResponse;
			}
		}

		/// <summary>
		/// The text writer used for writing to the response stream.
		/// </summary>
		public TextWriter Output {
			get {
				return _underlyingResponse.Output;
			}
		}

		/// <summary>
		/// The response stream.
		/// </summary>
		public Stream OutputStream {
			get {
				return _underlyingResponse.OutputStream;
			}
		}

		/// <summary>
		/// The status code of the response.
		/// </summary>
		public int StatusCode {
			get {
				return _underlyingResponse.StatusCode;
			}
			set {
				_underlyingResponse.StatusCode = value;
			}
		}

		/// <summary>
		/// The status description of the response stream.
		/// </summary>
		public string StatusDescription {
			get {
				return _underlyingResponse.StatusDescription;
			}
			set {
				_underlyingResponse.StatusDescription = value;
			}
		}

		/// <summary>
		/// The content type of the response stream.
		/// </summary>
		public string ContentType {
			get {
				return _underlyingResponse.ContentType;
			}
			set {
				_underlyingResponse.ContentType = value;
			}
		}

		/// <summary>
		/// Access to the response cookies.
		/// </summary>
		public IResponseCookieCollection Cookies {
			get {
				if (_cookies == null) {
					_cookies = new AspNetResponseCookieCollection(_underlyingResponse.Cookies);
				}
				return _cookies;
			}
		}

		/// <summary>
		/// Instantiates a new web response wrapping the http response provided.
		/// </summary>
		/// <param name="underlyingResponse">The underlying http response to wrap.</param>
		public AspNetResponse(HttpResponse underlyingResponse) {
			_underlyingResponse = underlyingResponse;
		}

		/// <summary>
		/// Flushes the response steam and ends the response.
		/// </summary>
		public void End() {
			_underlyingResponse.End();
		}

		/// <summary>
		/// Writes the provided text to the response stream.
		/// </summary>
		/// <param name="text">The text to write to the response stream.</param>
		public void Write(string text) {
			_underlyingResponse.Write(text);
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
			_underlyingResponse.Redirect(url);
		}

		/// <summary>
		/// Redirects the request permanently to the provided url
		/// issuing a `301` in the response.
		/// </summary>
		/// <param name="url"></param>
		public void PermanentRedirect(string url) {
			this.StatusCode = 301;
			this.StatusDescription = "301 Moved Permanently";
			_underlyingResponse.AddHeader("Location", url);
		}

	}
}

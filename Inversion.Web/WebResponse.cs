using System.Web;
using System.IO;

namespace Inversion.Web {
	public class WebResponse {

		private readonly HttpResponse _underlyingResponse;

		public void Redirect(string url) {
			_underlyingResponse.Redirect(url);
		}

		public void PermanentRedirect(string url) {
			this.StatusCode = 301;
			this.StatusDescription = "301 Moved Permanently";
			_underlyingResponse.AddHeader("Location", url);
		}

		protected HttpResponse UnderlyingResponse {
			get {
				return _underlyingResponse;
			}
		}

		public TextWriter Output {
			get {
				return _underlyingResponse.Output;
			}
		}

		public Stream OutputStream {
			get {
				return _underlyingResponse.OutputStream;
			}
		}

		public int StatusCode {
			get {
				return _underlyingResponse.StatusCode;
			}
			set {
				_underlyingResponse.StatusCode = value;
			}
		}

		public string StatusDescription {
			get {
				return _underlyingResponse.StatusDescription;
			}
			set {
				_underlyingResponse.StatusDescription = value;
			}
		}

		public string ContentType {
			get {
				return _underlyingResponse.ContentType;
			}
			set {
				_underlyingResponse.ContentType = value;
			}
		}

		public HttpCookieCollection Cookies {
			get { return this.UnderlyingResponse.Cookies; }
		}

		public WebResponse(HttpContext context) : this(context.Response) { }
		public WebResponse(HttpResponse underlyingResponse) {
			_underlyingResponse = underlyingResponse;
		}

		public void End() {
			_underlyingResponse.End();
		}

		public void Write(string text) {
			_underlyingResponse.Write(text);
		}

		public void WriteFormat(string text, params object[] args) {
			_underlyingResponse.Write(string.Format(text, args));
		}

		public void WriteLine(string text) {
			_underlyingResponse.Write(string.Format("{0}<br />", text));
		}

	}
}

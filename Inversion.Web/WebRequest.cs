using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Web;

namespace Inversion.Web {
	public class WebRequest {

		private readonly HttpRequest _underlyingRequest;
		private readonly UrlInfo _urlInfo;
		private readonly ImmutableDictionary<string, string> _params;
		private readonly IEnumerable<string> _flags;
		private readonly string _payload;
		private readonly ImmutableDictionary<string, string> _headers;

		protected HttpRequest UnderlyingRequest {
			get {
				return _underlyingRequest;
			}
		}

		public HttpFileCollection Files {
			get {
				return _underlyingRequest.Files;
			}
		}

		public UrlInfo UrlInfo {
			get {
				return _urlInfo;
			}
		}

		public string Method {
			get { return this.UnderlyingRequest.HttpMethod; }
		}

		public bool IsGet {
			get { return this.Method.ToLower() == "get"; }
		}

		public bool IsPost {
			get { return this.Method.ToLower() == "post"; }
		}

		public IImmutableDictionary<string, string> Params {
			get { return _params; }
		}

		public string Payload {
			get { return _payload; }
		}

		public IEnumerable<string> Flags {
			get { return _flags; }
		}

		public IImmutableDictionary<string, string> Headers {
			get { return _headers; }
		}

		public HttpCookieCollection Cookies {
			get { return this.UnderlyingRequest.Cookies; }
		}

		public WebRequest(HttpContext context) : this(context.Request) { }
		public WebRequest(HttpRequest request) {
			_underlyingRequest = request;
			_urlInfo = new UrlInfo(request.Url);
			// setup the request params
			ImmutableDictionary<string, string>.Builder parms = ImmutableDictionary.CreateBuilder<string, string>();
			ImmutableList<string>.Builder flags = ImmutableList.CreateBuilder<string>();
			// import the querystring
			for (int i = 0; i < _underlyingRequest.QueryString.Count; i++) {
				string key = _underlyingRequest.QueryString.GetKey(i);
				string[] values = _underlyingRequest.QueryString.GetValues(i);
				// check for valueless parameters and use as flags
				if (key == null && values != null) {
					flags.InsertRange(0, values);
				} else {
					if (values != null) parms.Add(key, values[0]);
				}
			}

			// import the post values
			foreach (string key in _underlyingRequest.Form.Keys) {
				parms.Add(key, _underlyingRequest.Form.Get(key));
			}

			if (_underlyingRequest.ContentLength > 0 && _underlyingRequest.Files.Count == 0) {
				using (TextReader reader = new StreamReader(_underlyingRequest.InputStream)) {
					_payload = reader.ReadToEnd();
				}
			}

			ImmutableDictionary<string, string>.Builder headers = ImmutableDictionary.CreateBuilder<string, string>();
			foreach (string key in _underlyingRequest.Headers.AllKeys) {
				headers.Add(key, _underlyingRequest.Headers[key]);
			}

			_flags = flags.ToImmutable();
			_params = parms.ToImmutable();
			_headers = headers.ToImmutable();
		}

	}
}

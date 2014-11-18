using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Web;

namespace Inversion.Web {
	/// <summary>
	/// Provides a wrapper for the underlying web request for application developers to use.
	/// </summary>
	/// <remarks>
	/// This wrapping is mindful of providing a common interface that can port to other platforms.
	/// Along with providing a point of extensibility and control.
	/// </remarks>
	public class WebRequest {

		private readonly HttpRequest _underlyingRequest;
		private readonly UrlInfo _urlInfo;
		private readonly ImmutableDictionary<string, string> _params;
		private readonly IEnumerable<string> _flags;
		private readonly string _payload;
		private readonly ImmutableDictionary<string, string> _headers;

		/// <summary>
		/// The underlying http request being wrapped.
		/// </summary>
		protected HttpRequest UnderlyingRequest {
			get {
				return _underlyingRequest;
			}
		}

		/// <summary>
		/// Gives access to any files uploaded by the user agent
		/// as part of this request.
		/// </summary>
		public HttpFileCollection Files {
			get {
				return _underlyingRequest.Files;
			}
		}

		/// <summary>
		/// Gives access to a url-info object that provides
		/// info about the structure of the url of the request.
		/// </summary>
		public UrlInfo UrlInfo {
			get {
				return _urlInfo;
			}
		}

		/// <summary>
		/// The http method of the request.
		/// </summary>
		public string Method {
			get { return this.UnderlyingRequest.HttpMethod; }
		}

		/// <summary>
		/// Returns true if the http method of this request is GET; otherwise returns false.
		/// </summary>
		public bool IsGet {
			get { return this.Method.ToLower() == "get"; }
		}

		/// <summary>
		/// Returns true if the http method of this request is POST; otherwise returns false.
		/// </summary>
		public bool IsPost {
			get { return this.Method.ToLower() == "post"; }
		}

		/// <summary>
		/// Provides access to the request parameters from both the querystring
		/// and those that are posted.
		/// </summary>
		/// <remarks>
		/// First params are read from the querystring and then those posted which
		/// will override any from the querystring.
		/// </remarks>
		public IImmutableDictionary<string, string> Params {
			get { return _params; }
		}

		/// <summary>
		/// Gives access to the payload if any of the request.
		/// </summary>
		public string Payload {
			get { return _payload; }
		}

		/// <summary>
		/// Gives access to any flags present in the querystring.
		/// </summary>
		/// <remarks>
		/// Any querystring parameter that is a single value rather
		/// that a key-value pair is regarded as a flag.
		/// </remarks>
		public IEnumerable<string> Flags {
			get { return _flags; }
		}

		/// <summary>
		/// Gives access to the headers of the reuqest.
		/// </summary>
		public IImmutableDictionary<string, string> Headers {
			get { return _headers; }
		}

		/// <summary>
		/// Gives access to the request cookies.
		/// </summary>
		public HttpCookieCollection Cookies {
			get { return this.UnderlyingRequest.Cookies; }
		}

		/// <summary>
		/// Instantiates a new web request by wrapping the http request
		/// of the http context provided.
		/// </summary>
		/// <param name="context">The http context from which to obtain the http request to wrap.</param>
		public WebRequest(HttpContext context) : this(context.Request) { }

		/// <summary>
		/// Instantiates a new web request wrapping the http request provided.
		/// </summary>
		/// <param name="request">The underlying http request to wrap.</param>
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

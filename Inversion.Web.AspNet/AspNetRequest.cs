using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Web;

namespace Inversion.Web.AspNet {
	public class AspNetRequest: IWebRequest {
		
		private readonly HttpRequest _underlyingRequest;
		private readonly IRequestCookieCollection _cookies;
		private readonly UrlInfo _urlInfo;
		private readonly ImmutableDictionary<string, string> _params;
		private readonly IEnumerable<string> _flags;
		private readonly string _payload;
		private readonly ImmutableDictionary<string, string> _headers;

		private IRequestFilesCollection _files;

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
		public IRequestFilesCollection Files {
			get {
				return _files ?? (_files = new AspNetRequestFilesCollection(_underlyingRequest.Files));
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
		public IDictionary<string, string> Params {
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
		public IDictionary<string, string> Headers {
			get { return _headers; }
		}

		/// <summary>
		/// Gives access to the request cookies.
		/// </summary>
		public IRequestCookieCollection Cookies {
			get { return _cookies; }
		}

		/// <summary>
		/// Instantiates a new web request wrapping the http request provided.
		/// </summary>
		/// <param name="request">The underlying http request to wrap.</param>
		public AspNetRequest(HttpRequest request) {
			_underlyingRequest = request;
			_urlInfo = new UrlInfo(request.Url);
			_cookies = new AspNetRequestCookieCollection(request.Cookies);
			// setup the request params
			ImmutableDictionary<string, string>.Builder parms = ImmutableDictionary.CreateBuilder<string, string>();
			ImmutableList<string>.Builder flags = ImmutableList.CreateBuilder<string>();
			// import the querystring
			for (int i = 0; i < _underlyingRequest.QueryString.Count; i++) {
				string key = _underlyingRequest.QueryString.GetKey(i);
				string[] values = _underlyingRequest.QueryString.GetValues(i);
				// check for valueless parameters and use as flags
				if (values != null) {
					if (key == null) {
						flags.InsertRange(0, values);
					} else {
						parms.Add(key, values[0]);
					}
				}
			}

			// import the post values
			foreach (string key in _underlyingRequest.Form.Keys) {
                // check for null in form keys (can happen in some POST scenarios)
			    if (key != null)
			    {
			        parms.Add(key, _underlyingRequest.Form.Get(key));
			    }
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

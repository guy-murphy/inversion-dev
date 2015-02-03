using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Microsoft.Owin;

using Inversion.Data;

namespace Inversion.Web.Owin {
	public class OwinRequest : IWebRequest {

		private readonly IOwinRequest _request;
		private readonly IRequestFilesCollection _files ;
		private readonly IRequestCookieCollection _cookies;
		private readonly UrlInfo _urlInfo;
		private readonly ImmutableDictionary<string, string> _params;
		private readonly IEnumerable<string> _flags;
		private readonly string _payload;
		private readonly ImmutableDictionary<string, string> _headers;



		/// <summary>
		/// The underlying http request being wrapped.
		/// </summary>
		protected IOwinRequest UnderlyingRequest {
			get {
				return _request;
			}
		}

		/// <summary>
		/// Gives access to any files uploaded by the user agent
		/// as part of this request.
		/// </summary>
		public IRequestFilesCollection Files {
			get { return _files; }
		}

		/// <summary>
		/// Gives access to a url-info object that provides
		/// info about the structure of the url of the request.
		/// </summary>
		public UrlInfo UrlInfo {
			get { return _urlInfo; }
		}

		/// <summary>
		/// The http method of the request.
		/// </summary>
		public string Method {
			get { return this.UnderlyingRequest.Method; }
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

		public OwinRequest(IOwinRequest request) {
			_request = request;
			_urlInfo = new UrlInfo(request.Uri);
			_cookies = new OwinRequestCookieCollection(request.Cookies);
			// setup the request params
			ImmutableDictionary<string, string>.Builder parms = ImmutableDictionary.CreateBuilder<string, string>();
			ImmutableList<string>.Builder flags = ImmutableList.CreateBuilder<string>();
			// import the querystring
			foreach (KeyValuePair<string, string[]> entry in request.Query) {
				if (entry.Value != null) {
					if (entry.Key == null) {
						flags.InsertRange(0, entry.Value);
					} else {
						parms.Add(entry.Key, entry.Value[0]);
					}
				}
			}
			// import the post values
			IFormCollection form = request.Get<IFormCollection>("Microsoft.Owin.Form#collection");
			foreach (KeyValuePair<string, string[]> entry in form) {
				parms.Add(entry.Key, entry.Value[0]);
			}
			// import the payload
			_payload = request.Body.AsMemoryStream().AsText();

			// import the headers
			ImmutableDictionary<string, string>.Builder headers = ImmutableDictionary.CreateBuilder<string, string>();
			foreach (KeyValuePair<string, string[]> entry in request.Headers) {
				headers.Add(entry.Key, entry.Value[0]);
			}


			_flags = flags.ToImmutable();
			_params = parms.ToImmutable();
			_headers = headers.ToImmutable();
		}

	}
}

using System;
using System.Collections.Generic;
using System.Web;

namespace Inversion.Web {

	/// <summary>
	/// Implements a mock IWebRequest for use with MockWebContext
	/// to facilitate testing.
	/// </summary>
	public class MockWebRequest : IWebRequest {
	
		private readonly Dictionary<string, string> _params = new Dictionary<string, string>();
		private readonly HashSet<string> _flags = new HashSet<string>();
		private readonly Dictionary<string, string> _headers = new Dictionary<string, string>();	

		private string _method = "GET";

		/// <summary>
		/// Gives access to any files uploaded by the user agent
		/// as part of this request.
		/// </summary>
		public IRequestFilesCollection Files {
			get { throw new NotImplementedException("The HttpFilesCollection has not been mocked."); }
		}

		/// <summary>
		/// Gives access to a url-info object that provides
		/// info about the structure of the url of the request.
		/// </summary>
		public UrlInfo UrlInfo { get; set; }

		/// <summary>
		/// The http method of the request.
		/// </summary>
		public string Method {
			get { return _method; }
			set { _method = value; }
		}

		/// <summary>
		/// Returns true if the http method of this request is GET; otherwise returns false.
		/// </summary>
		public bool IsGet { get; set; }

		/// <summary>
		/// Returns true if the http method of this request is POST; otherwise returns false.
		/// </summary>
		public bool IsPost { get; set; }

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
		public string Payload { get; set; }

		/// <summary>
		/// Gives access to any flags present in the querystring.
		/// </summary>
		/// <remarks>
		/// Any querystring parameter that is a single value rather
		/// that a key-value pair is regarded as a flag.
		/// </remarks>
		public IEnumerable<string> Flags { get { return _flags; } }

		/// <summary>
		/// Gives access to the headers of the reuqest.
		/// </summary>
		public IDictionary<string, string> Headers { get { return _headers; } }

		/// <summary>
		/// Gives access to the request cookies.
		/// </summary>
		public IRequestCookieCollection Cookies {
			get { throw new NotImplementedException("The HttpCookieCollection has not been mocked."); }
		}

		/// <summary>
		/// Instantiates a new mock web request
		/// from the url provided.
		/// </summary>
		/// <param name="url">The url of the request to be mocked.</param>
		public MockWebRequest(string url) {
			this.UrlInfo = new UrlInfo(url);
			foreach (KeyValuePair<string, string> entry in this.UrlInfo.Query) {
				if (entry.Key == entry.Value) {
					_flags.Add(entry.Key);
				} else {
					this.Params.Add(entry);
				}
			}
		}
	}
}

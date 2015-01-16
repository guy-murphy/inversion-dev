using System.Collections.Generic;
using System.Collections.Immutable;
using System.Web;

namespace Inversion.Web {
	/// <summary>
	/// Describes a request to be processed by a web application.
	/// </summary>
	public interface IWebRequest {
		/// <summary>
		/// Gives access to any files uploaded by the user agent
		/// as part of this request.
		/// </summary>
		HttpFileCollection Files { get; }

		/// <summary>
		/// Gives access to a url-info object that provides
		/// info about the structure of the url of the request.
		/// </summary>
		UrlInfo UrlInfo { get; }

		/// <summary>
		/// The http method of the request.
		/// </summary>
		string Method { get; }

		/// <summary>
		/// Returns true if the http method of this request is GET; otherwise returns false.
		/// </summary>
		bool IsGet { get; }

		/// <summary>
		/// Returns true if the http method of this request is POST; otherwise returns false.
		/// </summary>
		bool IsPost { get; }

		/// <summary>
		/// Provides access to the request parameters from both the querystring
		/// and those that are posted.
		/// </summary>
		/// <remarks>
		/// First params are read from the querystring and then those posted which
		/// will override any from the querystring.
		/// </remarks>
		IDictionary<string, string> Params { get; }

		/// <summary>
		/// Gives access to the payload if any of the request.
		/// </summary>
		string Payload { get; }

		/// <summary>
		/// Gives access to any flags present in the querystring.
		/// </summary>
		/// <remarks>
		/// Any querystring parameter that is a single value rather
		/// that a key-value pair is regarded as a flag.
		/// </remarks>
		IEnumerable<string> Flags { get; }

		/// <summary>
		/// Gives access to the headers of the reuqest.
		/// </summary>
		IDictionary<string, string> Headers { get; }

		/// <summary>
		/// Gives access to the request cookies.
		/// </summary>
		HttpCookieCollection Cookies { get; }
	}
}
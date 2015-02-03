using System.Web;

namespace Inversion.Web.AspNet {

	/// <summary>
	/// Implements a response header collection for a ASP.NET http response.
	/// </summary>
	public class AspNetResponseHeadersCollection : IResponseHeaderCollection {

		private readonly HttpResponse _response;

		/// <summary>
		/// Instantiates a new response header collection.
		/// </summary>
		/// <param name="response">The response to which the headers belong.</param>
		public AspNetResponseHeadersCollection(HttpResponse response) {
			_response = response;
		}

		/// <summary>
		/// Appeands a header to the response.
		/// </summary>
		/// <param name="key">The key of the header.</param>
		/// <param name="value">The value of the header.</param>
		public void Append(string key, string value) {
			_response.AppendHeader(key, value);
		}

	}
}

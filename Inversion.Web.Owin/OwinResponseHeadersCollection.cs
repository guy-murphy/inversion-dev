using Microsoft.Owin;

namespace Inversion.Web.Owin {

	/// <summary>
	/// Implements a response header collection for an IOwinResponse.
	/// </summary>
	public class OwinResponseHeadersCollection: IResponseHeaderCollection {

		private readonly IOwinResponse _response;

		/// <summary>
		/// Instantiates a new response header collection.
		/// </summary>
		/// <param name="response">The response to which the headers belong.</param>
		public OwinResponseHeadersCollection(IOwinResponse response) {
			_response = response;
		}

		/// <summary>
		/// Appeands a header to the response.
		/// </summary>
		/// <param name="key">The key of the header.</param>
		/// <param name="value">The value of the header.</param>
		public void Append(string key, string value) {
			_response.Headers.Append(key, value);
		}

	}
}

using System.Web;

namespace Inversion.Web {

	/// <summary>
	/// A base handler for Conclave.
	/// </summary>
	public interface IInversionHandler : IHttpHandler {

		/// <summary>
		/// Process the current request with the provided `WebContext`.
		/// </summary>
		/// <param name="context">
		/// The `WebContext` being used for the current request.
		/// </param>
		void ProcessRequest(WebContext context);
	}
}

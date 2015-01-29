using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

using Inversion.Process;

namespace Inversion.Web {

	/// <summary>
	/// Extends the process context with web specific
	/// information about an individual request being processed.
	/// </summary>
	/// <remarks>
	/// The context object is threaded through the whole stack
	/// and provides a controled pattern and workflow of state,
	/// along with access to resources and services external
	/// to the application. Everything hangs off the context.
	/// </remarks>

	public interface IWebContext: IProcessContext {
		/// <summary>
		/// Provides access to the running web application
		/// to which this context belongs.
		/// </summary>
		WebApplication Application { get; }

		/// <summary>
		/// Gives access to the web response of this context.
		/// </summary>
		IWebResponse Response { get; }

		/// <summary>
		/// Gives access to the web request for this context.
		/// </summary>
		IWebRequest Request { get; }

		/// <summary>
		/// Gives access to the `IPrinciple` user object that represents
		/// the current user for this context.
		/// </summary>
		IPrincipal User { get; set; }

		/// <summary>
		/// Indicates completion of processing for this context.
		/// </summary>
		void Completed();
	}
}
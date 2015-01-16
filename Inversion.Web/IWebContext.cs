using System.Security.Principal;
using System.Web.Caching;
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
		WebResponse Response { get; }

		/// <summary>
		/// Gives access to the web request for this context.
		/// </summary>
		IWebRequest Request { get; }

		/// <summary>
		/// Gives access to the cache being used for this context.
		/// </summary>
		Cache Cache { // This needs to be changed for an interface that we own
			get; }

		/// <summary>
		/// Gives access to the `IPrinciple` user object that represents
		/// the current user for this context.
		/// </summary>
		IPrincipal User { get; set; }
	}
}
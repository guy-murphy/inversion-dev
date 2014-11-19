using System;
using System.Security.Principal;
using System.Web;
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

	public class WebContext : ProcessContext {

		// while the fields are all readonly, the types are all mutable
		private readonly HttpContext _underlyingContext;
		private readonly WebApplication _application;
		private readonly WebResponse _response;
		private readonly WebRequest _request;

		/// <summary>
		/// The underlying http context that is being wrapped by
		/// this web context.
		/// </summary>
		protected HttpContext UnderlyingContext {
			get { return _underlyingContext; }
		}

		/// <summary>
		/// Provides access to the running web application
		/// to which this context belongs.
		/// </summary>
		public WebApplication Application {
			get { return _application; }
		}

		/// <summary>
		/// Gives access to the web response of this context.
		/// </summary>
		public WebResponse Response {
			get { return _response; }
		}

		/// <summary>
		/// Gives access to the web request for this context.
		/// </summary>
		public WebRequest Request {
			get { return _request; }
		}

		/// <summary>
		/// Gives access to the cache being used for this context.
		/// </summary>
		public Cache Cache { // This needs to be changed for an interface that we own
			get { return this.UnderlyingContext.Cache; }
		}

		/// <summary>
		/// Gives access to the `IPrinciple` user object that represents
		/// the current user for this context.
		/// </summary>
		public IPrincipal User {
			get { return this.UnderlyingContext.User; }
			set {
				// the user will already have a WindowsPrincipal assigned to it
				// we're wanting to latch this value so that it's only set the once, to offer as much immutability as possible
				// this can't be done at instantiation of WebContext, and the value is not likely to be null when we come to it
				// so we endure the identity of the  principal we're using has a custom authentication type that we will recognise
				if (this.UnderlyingContext.User == null || this.UnderlyingContext.User.Identity.AuthenticationType != "Conclave") {
					this.UnderlyingContext.User = value;
				} else {
					throw new InvalidOperationException("You may only set the value of User the once, thereafter it is readonly.");
				}
			}
		}

		/// <summary>
		/// Instantiates a new context object purposed for Web applications.
		/// </summary>
		/// <param name="underlyingContext">The underlying http context to wrap.</param>
		/// <param name="services">The service container the context will use.</param>
		public WebContext(HttpContext underlyingContext, IServiceContainer services): base(services) {
			_underlyingContext = underlyingContext;
			_application = this.UnderlyingContext.ApplicationInstance as WebApplication;
			_response = new WebResponse(this.UnderlyingContext);
			_request = new WebRequest(this.UnderlyingContext);
		}



	}
}

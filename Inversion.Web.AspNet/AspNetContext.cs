using System.Security.Principal;
using System.Web;

using Inversion.Data;
using Inversion.Process;

namespace Inversion.Web.AspNet {
	public class AspNetContext: ProcessContext, IWebContext {

		private readonly HttpContext _underlyingContext;
		private readonly AspNetApplication _application;
		private readonly AspNetResponse _response;
		private readonly AspNetRequest _request;

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
		public IWebApplication Application {
			get { return _application; }
		}

		/// <summary>
		/// Gives access to the web response of this context.
		/// </summary>
		public IWebResponse Response {
			get { return _response; }
		}

		/// <summary>
		/// Gives access to the web request for this context.
		/// </summary>
		public IWebRequest Request {
			get { return _request; }
		}

		/// <summary>
		/// Gives access to the `IPrinciple` user object that represents
		/// the current user for this context.
		/// </summary>
		public IPrincipal User { get; set; }

		/// <summary>
		/// Instantiates a new process contrext for inversion.
		/// </summary>
		/// <remarks>You can think of this type here as "being Inversion". This is the thing.</remarks>
		/// <param name="underlyingContext">The underlying http context to wrap.</param>
		/// <param name="services">The service container the context will use.</param>
		/// <param name="resources">The resources available to the context.</param>
		public AspNetContext(HttpContext underlyingContext, IServiceContainer services, IResourceAdapter resources): base(services, resources) {
			_underlyingContext = underlyingContext;
			_application = this.UnderlyingContext.ApplicationInstance as AspNetApplication;
			_response = new AspNetResponse(this.UnderlyingContext.Response);
			_request = new AspNetRequest(this.UnderlyingContext.Request);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Inversion.Data;
using Inversion.Process;
using Microsoft.Owin;

namespace Inversion.Web.Owin {
	public class OwinProcessContext : ProcessContext, IWebContext {

		private readonly IOwinContext _underlyingContext;
		private readonly IWebApplication _application;
		private readonly IWebResponse _response;
		private readonly IWebRequest _request;

		/// <summary>
		/// The underlying http context that is being wrapped by
		/// this web context.
		/// </summary>
		protected IOwinContext UnderlyingContext {
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
		public OwinProcessContext(IOwinContext underlyingContext, IServiceContainer services, IResourceAdapter resources)
			: base(services, resources) {
			_underlyingContext = underlyingContext;
			_application = null;
			_response = new OwinResponse(this.UnderlyingContext.Response);
			_request = new OwinRequest(this.UnderlyingContext.Request);
		}
	}
}

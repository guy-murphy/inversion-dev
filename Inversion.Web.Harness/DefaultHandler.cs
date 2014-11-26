using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Inversion.Process;
using Inversion.Web;
using Inversion.Spring;

namespace Inversion.Web.Harness {
	public class DefaultHandler : IHttpHandler {

		/// <summary>
		/// Determines whether or not this handler
		/// can be reused.
		/// </summary>
		/// <value>
		/// Always `true` as this handler can be reused.
		/// </value>
		public virtual bool IsReusable {
			get { return true; }
		}

		// <summary>
		/// Processes the current reuqest with the provided `HttpContext`
		/// </summary>
		/// <param name="context">
		/// The `HttpContext` being used  for the request.
		/// </param>
		/// <remarks>
		/// We wrap the `HttpContext` with a `WebContext` and then process that.
		/// The call into this method is from teh web server and (although not
		/// exclusively) can be thought of as the entry point to the application for
		/// a request. In most cases the method `ProcessRequest(WebContext)` can
		/// be thought of as the where the work begins.
		/// </remarks>
		public void ProcessRequest(HttpContext context) {
			this.ProcessRequest(new WebContext(context, ServiceContainer.Instance));
		}

		/// <summary>
		/// Process the current request with the provided `WebContext`.
		/// </summary>
		/// <param name="context">
		/// The `WebContext` being used for the current request.
		/// </param>
		/// <remarks>
		/// We pick up the behaviours associated with the name *request-behaviours*
		/// from the service container. We register each of the behaviours with the
		/// `WebContext`, and then we fire the first event `process-request`.
		/// </remarks>
		public virtual void ProcessRequest(WebContext context) {
			IList<IProcessBehaviour> behaviours = context.Services.GetService<List<IProcessBehaviour>>("request-behaviours");
			context.Register(behaviours);
			context.Timers.Begin("process-request");
			context.Fire("process-request");
			context.Completed();
		}
	}
}

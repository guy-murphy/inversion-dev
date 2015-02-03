using System.Collections.Generic;
using System.Web;
using Inversion.Data;
using Inversion.Process.Behaviour;
using Inversion.Spring;
//using Inversion.Naiad;
using Inversion.Web.AspNet;

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

		/// <summary>
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
			#if DEBUG
				this.ProcessRequest(new AspNetContext(context, ServiceContainer.Instance, FileSystemResourceAdapter.Instance));
			#else
				this.ProcessRequest(new AspNetContext(context, ServiceContainer.Instance, CachingFileSystemResourceAdapter.Instance));
			#endif
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
		public virtual void ProcessRequest(IWebContext context) {
			IList<IProcessBehaviour> behaviours = context.Services.GetService<List<IProcessBehaviour>>("request-behaviours");
			context.Register(behaviours);

			context.Timers.Begin("process-request");
			context.Fire("process-request");
			context.Completed();
		}
	}
}

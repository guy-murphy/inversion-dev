using System;
using System.Security.Principal;

using Inversion.Data;
using Inversion.Process;

namespace Inversion.Web {
	/// <summary>
	/// Implements a mock web context suitable for testing purposes.
	/// Ensures no exposure to http context, and provides mocks
	/// of IWebRequest and IWebResponse.
	/// </summary>
	public class MockWebContext: ProcessContext, IWebContext {

		/// <summary>
		/// Instantiates a new process contrext for inversion.
		/// </summary>
		/// <remarks>You can think of this type here as "being Inversion". This is the thing.</remarks>
		/// <param name="services">The service container the context will use.</param>
		/// <param name="resources">The resources available to the context.</param>
		public MockWebContext(IServiceContainer services, IResourceAdapter resources) : base(services, resources) {
			this.Request = new MockWebRequest(String.Empty);
			this.Response = new MockWebResponse();
		}

		/// <summary>
		/// Provides access to the running web application
		/// to which this context belongs.
		/// </summary>
		public IWebApplication Application {
			get { throw new NotImplementedException("The web application has not been mocked."); }
		}

		/// <summary>
		/// Gives access to the web response of this context.
		/// </summary>
		public IWebResponse Response { get; set; }

		/// <summary>
		/// Gives access to the web request for this context.
		/// </summary>
		public IWebRequest Request { get; set; }

		/// <summary>
		/// Gives access to the `IPrinciple` user object that represents
		/// the current user for this context.
		/// </summary>
		public IPrincipal User { get; set; }
	}
}

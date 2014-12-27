using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using Inversion.Process;
using Inversion.Process.Behaviour;
using Inversion.Naiad;
using Inversion.Web.Behaviour;
using Inversion.Web.Behaviour.View;

namespace Inversion.Web.Harness.Site {
	public class Global : WebApplication {

		protected void Application_Start(object sender, EventArgs e) {

			Naiad.ServiceContainer.Instance.RegisterService("life-cycle",
				container => {
					return new List<string> {
						 "bootstrap",
						 "parse-request",
						 "work",
						 "view-state",
						 "process-views",
						 "render"
					};
				}
			);

			Naiad.ServiceContainer.Instance.RegisterService("request-behaviours",
				container => {
					return new List<IProcessBehaviour> {
						new SimpleSequenceBehaviour("process-request", container.GetService<List<string>>("life-cycle")),
						new BootstrapBehaviour("bootstrap",
							new Dictionary<string,string> {
								{"area", "default"}, 
								{"concern", "default"},
								{"action", "default"},
								{"app-path", "/web.harness"}
							}
						),
						new ParseRequestBehaviour("parse-request", "Inversion.Web.Harness.Site"),
						new ViewStateBehaviour("view-state"),
						new ProcessViewsBehaviour("process-views"),
						new RenderBehaviour("render"),
						new RazorViewBehaviour("rzr::view"),
						new XmlViewBehaviour("xml::view", "text/xml"),
						new JsonViewBehaviour("json::view", "text/json"),
						new XsltViewBehaviour("xslt::view", "text/xml"),
						new XsltViewBehaviour("xsl::view", "text/html"),
						new HelloWorldBehaviour("work", namedMaps: new NamedMappings {
								{
									"matches-all-parameter-values", 
									new Dictionary<string,string> {
										{"action", "hello"}
									}
								}
							})
					};
				}
			);
		}

		protected void Session_Start(object sender, EventArgs e) {

		}

		protected void Application_BeginRequest(object sender, EventArgs e) {

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e) {

		}

		protected void Application_Error(object sender, EventArgs e) {

		}

		protected void Session_End(object sender, EventArgs e) {

		}

		protected void Application_End(object sender, EventArgs e) {

		}
	}
}
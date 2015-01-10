using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using Inversion.Process;
using Inversion.Process.Behaviour;
using Inversion.Naiad;
using Inversion.StringTemplate.Behaviour.View;
using Inversion.Web.Behaviour;
using Inversion.Web.Behaviour.View;

namespace Inversion.Web.Harness.Site {
	public class Global : WebApplication {

		protected void Application_Start(object sender, EventArgs e) {

			Naiad.ServiceContainer.Instance.RegisterService("request-behaviours",
				container => {
					return new List<IProcessBehaviour> {
						new MessageTraceBehaviour("*", 
							new Configuration.Builder {
								{"event", "has", "related-loc"}
							}
						),
						new ParameterisedSequenceBehaviour("process-request", 
							new Configuration.Builder {
								{"fire", "bootstrap"},
								{"fire", "parse-request"},
								{"fire", "work"},
								{"fire", "view-state"},
								{"fire", "process-views"},
								{"fire", "render"}
							}
						),
						new ParameterisedSequenceBehaviour("work", 
							new Configuration.Builder {
								{"context", "has", "name"},
								{"context", "match", "action", "hello"},
								{"context", "exclude", "area", "admin"},
								{"fire", "tm-get-topic", "topic-loc", "current-topic"},
								{"fire", "tm-get-related-for-topic", "topic-loc", "current-topic"},
								{"fire", "tm-get-related-for-topic", "related-loc", "current-topic-related"},
								{"fire", "tm-resolve-topic-blogs", "topic-loc", "current-topic"},
								{"fire", "tm-resolve-topic-blogs", "related-loc", "current-topic-related"},
								{"fire", "tm-resolve-topic-comments ", "topic-loc", "current-topic"}
							}
						),
						new BootstrapBehaviour("bootstrap", 
							new Configuration.Builder {
								{"context", "copy", "area", "default"},
								{"context", "copy", "concern", "default"},
								{"context", "copy", "action", "default"},
								{"context", "copy", "appPath", "/web.harness"},
								{"context", "copy", "basePath", @"e:\Users\User\Documents\GitHub\inversion-dev\Inversion.Web.Harness.Site\"}
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
						new StringTemplateViewBehaviour("st::view", "text/html")
					};
				}
			);
		}

		//protected void Session_Start(object sender, EventArgs e) {

		//}

		//protected void Application_BeginRequest(object sender, EventArgs e) {

		//}

		//protected void Application_AuthenticateRequest(object sender, EventArgs e) {

		//}

		//protected void Application_Error(object sender, EventArgs e) {

		//}

		//protected void Session_End(object sender, EventArgs e) {

		//}

		//protected void Application_End(object sender, EventArgs e) {

		//}
	}
}
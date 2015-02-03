using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

using Inversion.Process;
using Inversion.Process.Behaviour;
using Inversion.Data;
using Inversion.StringTemplate.Behaviour.View;
using Inversion.Web;
using Inversion.Web.Owin;
using Inversion.Web.Behaviour;
using Inversion.Web.Behaviour.View;
using Inversion.Naiad;

[assembly: OwinStartup(typeof(Inversion.Demo.Katana.InversionStartup))]

namespace Inversion.Demo.Katana {
	public class InversionStartup {
		public void Configuration(IAppBuilder app) {

			Naiad.ServiceContainer.Instance.RegisterService("request-behaviours",
				container => {
					return new List<IProcessBehaviour> {
						new MessageTraceBehaviour("*", new Prototype.Builder {
								{"event", "match", "trace", "true"}
							}
						),
						new ParameterisedSequenceBehaviour("process-request", new Configuration.Builder {
								{"fire", "bootstrap"},
								{"fire", "parse-request"},
								{"fire", "work"},
								{"fire", "view-state"},
								{"fire", "process-views"},
								{"fire", "render"}
							}
						),
						new ParameterisedSequenceBehaviour("work", new Configuration.Builder {
								{"context", "match-any", "action", "test1"},
								{"context", "match-any", "action", "test2"},
								{"fire", "work-message-one", "trace", "true"}, 
								{"fire", "work-message-two", "trace", "true"}
							}
						),
						new ParseRequestBehaviour("parse-request"),
						new BootstrapBehaviour("bootstrap", new Configuration.Builder {
								{"context", "set", "area", "default"},
								{"context", "set", "concern", "default"},
								{"context", "set", "action", "default"},
								{"context", "set", "appPath", "/web.harness"}
							}
						),
						new ViewStateBehaviour("view-state"),
						new ProcessViewsBehaviour("process-views", new Configuration.Builder {
								{"config", "default-view", "xml"}
							}
						),
						new RenderBehaviour("render"),
						new JsonViewBehaviour("json::view", "text/json"),
						new XmlViewBehaviour("xml::view", "text/xml"),
						new XsltViewBehaviour("xslt::view", "text/xml"),
						new XsltViewBehaviour("xsl::view", "text/html"),
						new StringTemplateViewBehaviour("st::view", "text/html")
					};
				}
			);

			app.Run(owin => Task.Run(() => {
				IWebContext context = new OwinProcessContext(owin, ServiceContainer.Instance, new FileSystemResourceAdapter(@"e:\Users\User\Documents\GitHub\inversion-dev\Inversion.Demo.Katana"));
				IList<IProcessBehaviour> behaviours = context.Services.GetService<List<IProcessBehaviour>>("request-behaviours");
				context.Register(behaviours);

				context.Timers.Begin("process-request");
				context.Fire("process-request");
				context.Completed();
			}));
		}
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Xml;
using System.Xml.Linq;
using Inversion.Data;
using Inversion.Process;
using Inversion.Process.Behaviour;
using Inversion.StringTemplate.Behaviour.View;
using Inversion.Web.Behaviour;
using Inversion.Web.Behaviour.View;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Inversion.Naiad;

namespace Inversion.Web.Tests.Behaviour {
	[TestClass]
	public class ViewPipelineTests {

		[TestInitialize]
		public void Init() {
			Naiad.ServiceContainer.Instance.RegisterService("test-behaviours",
				container => {
					return new List<IProcessBehaviour> {
						new MessageTraceBehaviour("*", new Configuration.Builder {
								{"event", "match", "trace", "true"}
							}
						),
						new ParameterisedSequenceBehaviour("test", new Configuration.Builder {
								{"fire", "bootstrap"},
								{"fire", "work"},
								{"fire", "view-state"},
								{"fire", "process-views"}
							}
						),
						new ParameterisedSequenceBehaviour("work", new Configuration.Builder {
								{"fire", "work-message-one", "trace", "true"}, 
								{"fire", "work-message-two", "trace", "true"}
							}
						),
						new BootstrapBehaviour("bootstrap", new Configuration.Builder {
								{"context", "copy", "area", "default"},
								{"context", "copy", "concern", "default"},
								{"context", "copy", "action", "default"},
								{"context", "copy", "appPath", "/web.harness"}
							}
						),
						new ViewStateBehaviour("view-state"),
						new ProcessViewsBehaviour("process-views", new Configuration.Builder {
								{"config", "default-view", "xml"}
							}
						),
						new JsonViewBehaviour("xml::view", "text/json"),
						new XmlViewBehaviour("xml::view", "text/xml"),
						new XsltViewBehaviour("xslt::view", "text/xml"),
						new XsltViewBehaviour("xsl::view", "text/html"),
						new StringTemplateViewBehaviour("st::view", "text/html")
					};
				}
			);
		}

		protected IProcessContext GetContext() {
			IProcessContext context = new ProcessContext(ServiceContainer.Instance, new AssemblyResourceAdapter(Assembly.GetExecutingAssembly()));
			IList<IProcessBehaviour> behaviours = context.Services.GetService<List<IProcessBehaviour>>("test-behaviours");
			context.Register(behaviours);
			return context;
		}

		[TestMethod]
		public void BasicXmlViewTest() {
			IProcessContext context = this.GetContext();
			context.Params["views"] = "xml";
			context.Fire("test");
		}

		public void BasicJsonViewTest() {
			IProcessContext context = this.GetContext();
			context.Params["views"] = "json";
			context.Fire("test");
		}
	}
}

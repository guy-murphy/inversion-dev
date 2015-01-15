using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
			Naiad.ServiceContainer.Instance.RegisterService("request-behaviours",
				container => {
					return new List<IProcessBehaviour> {
						new MessageTraceBehaviour("*", new Configuration.Builder {
								{"event", "match", "trace", "true"}
							}
						),
						new ParameterisedSequenceBehaviour("process-request", new Configuration.Builder {
								{"fire", "bootstrap"},
								{"fire", "work"},
								{"fire", "view-state"},
								{"fire", "process-views"}
							}
						),
						new ParameterisedSequenceBehaviour("work", new Configuration.Builder {
								{"fire", "work-message-one"}, 
								{"fire", "work-message-two"}
							}
						),
						new BootstrapBehaviour("bootstrap", new Configuration.Builder {
								{"context", "copy", "area", "default"},
								{"context", "copy", "concern", "default"},
								{"context", "copy", "action", "default"},
								{"context", "copy", "appPath", "/web.harness"}, 
								{"context", "copy", "basePath", @"e:\Users\User\Documents\GitHub\inversion-dev\Inversion.Web.Harness.Site\"}
							}
						),
						new ViewStateBehaviour("view-state"),
						new ProcessViewsBehaviour("process-views"),
						new XmlViewBehaviour("xml::view", "text/xml"),
						new XsltViewBehaviour("xslt::view", "text/xml"),
						new XsltViewBehaviour("xsl::view", "text/html"),
						new StringTemplateViewBehaviour("st::view", "text/html")
					};
				}
			);
		}

		[TestMethod]
		public void TestMethod1() {

			ProcessContext context = new ProcessContext(ServiceContainer.Instance, FileSystemResourceAdapter.Instance);

			string path = "Inversion.Web.Tests.Behaviour.Resources.Views.Xslt.t1.xslt";

			System.Reflection.Assembly thisAssembly = System.Reflection.Assembly.GetExecutingAssembly();
			string[] resourceNames = thisAssembly.GetManifestResourceNames();
			using (Stream stream = thisAssembly.GetManifestResourceStream(path)) {
				XmlDocument doc = new XmlDocument();
				doc.Load(new XmlTextReader(stream));
				string xml = doc.OuterXml;
				Debug.Write(xml);
			}

			//string path = Path.Combine("Inversion.Web.Tests.Behaviour", "Resources", "Views", "Xslt", "t1.xslt");
			//XmlDocument doc = new XmlDocument();
			//doc.Load(new XmlTextReader(path));
		}
	}
}

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

using Inversion.Data;
using Inversion.Extensions;
using Inversion.Process;
using Inversion.Process.Behaviour;
using Inversion.StringTemplate.Behaviour.View;
using Inversion.Web.Behaviour;
using Inversion.Web.Behaviour.View;
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
						new RenderBehaviour("render"),
						new JsonViewBehaviour("json::view", "text/json"),
						new XmlViewBehaviour("xml::view", "text/xml"),
						new XsltViewBehaviour("xslt::view", "text/xml"),
						new XsltViewBehaviour("xsl::view", "text/html"),
						new StringTemplateViewBehaviour("st::view", "text/html")
					};
				}
			);
		}

		protected IWebContext GetContext() {
			IWebContext context = new MockWebContext(ServiceContainer.Instance, new AssemblyResourceAdapter(Assembly.GetExecutingAssembly()));
			IList<IProcessBehaviour> behaviours = context.Services.GetService<List<IProcessBehaviour>>("test-behaviours");
			context.Register(behaviours);
			return context;
		}

		[TestMethod]
		public void BasicXmlView() {
			IWebContext context = this.GetContext();
			context.Params["action"] = "test1";
			context.Params["views"] = "xml";
			context.Fire("test");

			Assert.IsTrue(context.ViewSteps.HasSteps);
			Assert.IsTrue(context.ViewSteps.Count == 2);
			Assert.IsTrue(context.ViewSteps.Last.HasContent);
			Assert.IsTrue(context.ViewSteps.Last.ContentType == "text/xml");

			XDocument result = context.ViewSteps.Last.Content.AsXDocument();
			Assert.IsTrue(result.XPathSelectElements("/records/item[@name='params']/records/item[@name='action' and @value='test1']").Count() == 1);
			Assert.IsTrue(result.XPathSelectElements("/records/item[@name='params']/records/item[@name='views' and @value='xml']").Count() == 1);
			Assert.IsTrue(result.XPathSelectElements("/records/item[@name='eventTrace']/list/event").Count() == 2);
			Assert.IsTrue(result.XPathSelectElements("/records/item[@name='eventTrace']/list/event[@message='work-message-one']").Count() == 1);
			Assert.IsTrue(result.XPathSelectElements("/records/item[@name='eventTrace']/list/event[@message='work-message-two']").Count() == 1);

			string render = ((MockWebResponse)context.Response).Result;
			Assert.AreEqual(context.ViewSteps.Last.Content, render);

			XElement result1 = context.Resources.Open("Behaviour/Resources/Results/result1.xml").AsXElement();
			Assert.IsTrue(XNode.DeepEquals(result1, render.AsXElement()));		
		}

		[TestMethod]
		public void BasicJsonView() {
			IWebContext context = this.GetContext();
			context.Params["action"] = "test1";
			context.Params["views"] = "json";
			context.Fire("test");

			Assert.IsTrue(context.ViewSteps.HasSteps);
			Assert.IsTrue(context.ViewSteps.Count == 2);
			Assert.IsTrue(context.ViewSteps.Last.HasContent);
			Assert.IsTrue(context.ViewSteps.Last.ContentType == "text/json");

			JObject result = context.ViewSteps.Last.Content.AsJObject();
			Assert.IsTrue(result.SelectToken("$.params.action").Value<string>() == "test1");
			Assert.IsTrue(result.SelectToken("$.params.views").Value<string>() == "json");		
			Assert.IsTrue(result["eventTrace"].Values<JObject>().Count() == 2);
			Assert.IsTrue(result.SelectTokens("$.eventTrace[?(@.message=='work-message-one')]").Count() == 1);
			Assert.IsTrue(result.SelectTokens("$.eventTrace[?(@.message=='work-message-two')]").Count() == 1);

			string render = ((MockWebResponse)context.Response).Result;
			Assert.AreEqual(context.ViewSteps.Last.Content, render);

			JObject result1 = context.Resources.Open("Behaviour/Resources/Results/result1.json").AsJObject();
			Assert.IsTrue(JToken.DeepEquals(result1, render.AsJObject()));
		}
	}
}

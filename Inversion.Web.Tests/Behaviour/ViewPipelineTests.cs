using System.Collections.Generic;
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

	/// <summary>
	/// Some simple tests to ensure the inversion view pipeline
	/// is conforming to basic expectations. The meat and potatoes of
	/// these tests is `ChainedViewTransform` which tests transforms piped between
	/// string template and xslt, where the shape of the data is transformed. The
	/// tests preceding `ChainedViewTransform` are both testing more basic expectations
	/// and serving as an illustration of testing behaviour interactions.
	/// </summary>
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
		}

		/// <summary>
		/// We simply obtain a fully configured context ready to use for a test case.
		/// </summary>
		/// <returns>
		/// Returns a web context.
		/// </returns>
		protected IWebContext GetContext() {
			IWebContext context = new MockWebContext(ServiceContainer.Instance, new AssemblyResourceAdapter(Assembly.GetExecutingAssembly(), "Behaviour"));
			IList<IProcessBehaviour> behaviours = context.Services.GetService<List<IProcessBehaviour>>("test-behaviours");
			context.Register(behaviours);
			return context;
		}

		///// <summary>
		///// In this test we simply test firing some test messages,
		///// get an xml view of the resulting view state, and test
		///// that view for expected articles.
		///// </summary>
		//[TestMethod]
		//public void BasicXmlView() {
		//	IWebContext context = this.GetContext();
		//	context.Params["action"] = "test1";
		//	context.Params["views"] = "xml";
		//	context.Fire("test");

		//	Assert.IsTrue(context.ViewSteps.HasSteps);
		//	Assert.IsTrue(context.ViewSteps.Count == 2);
		//	Assert.IsTrue(context.ViewSteps.Last.HasContent);
		//	Assert.IsTrue(context.ViewSteps.Last.ContentType == "text/xml");

		//	XDocument result = context.ViewSteps.Last.Content.AsXDocument();
		//	Assert.IsTrue(result.XPathSelectElements("/records/item[@name='params']/records/item[@name='action' and @value='test1']").Count() == 1);
		//	Assert.IsTrue(result.XPathSelectElements("/records/item[@name='params']/records/item[@name='views' and @value='xml']").Count() == 1);
		//	Assert.IsTrue(result.XPathSelectElements("/records/item[@name='eventTrace']/list/event").Count() == 2);
		//	Assert.IsTrue(result.XPathSelectElements("/records/item[@name='eventTrace']/list/event[@message='work-message-one']").Count() == 1);
		//	Assert.IsTrue(result.XPathSelectElements("/records/item[@name='eventTrace']/list/event[@message='work-message-two']").Count() == 1);

		//	Assert.IsTrue(context.Response.ContentType == context.ViewSteps.Last.ContentType);
		//	string render = ((MockWebResponse)context.Response).Result;
		//	Assert.AreEqual(context.ViewSteps.Last.Content, render);

		//	XElement result1 = context.Resources.Open("Resources/Results/result-1-0.xml").AsXElement();
  //          XElement compare = render.AsXElement();

		//	Assert.IsTrue(XNode.DeepEquals(result1, compare));
		//}

		/// <summary>
		/// In this test we simply test firing some test messages,
		/// get a json view of the resulting view state, and test
		/// that view for expected articles.
		/// </summary>
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

			Assert.IsTrue(context.Response.ContentType == context.ViewSteps.Last.ContentType);
			string render = ((MockWebResponse)context.Response).Result;
			Assert.AreEqual(context.ViewSteps.Last.Content, render);

			JObject result1 = context.Resources.Open("Resources/Results/result-1-0.json").AsJObject();
			
			Assert.IsTrue(JToken.DeepEquals(result1, render.AsJObject()));
		}

		/// <summary>
		/// In this test we set the current action to "test1" and views to "xslt", then check
		/// that the resulting templated transform has produced what we expect. This test
		/// involves locating the correct view behaviour it resolving the correct template to use.
		/// </summary>
		[TestMethod]
		public void BasicXsltView() {
			IWebContext context = this.GetContext();
			context.Params["action"] = "test1";
			context.Params["views"] = "xslt";
			context.Fire("test");

			Assert.IsTrue(context.ViewSteps.HasSteps);
			Assert.IsTrue(context.ViewSteps.Count == 2);
			Assert.IsTrue(context.ViewSteps.Last.HasContent);
			Assert.IsTrue(context.ViewSteps.Last.ContentType == "text/xml");

			Assert.IsTrue(context.Response.ContentType == context.ViewSteps.Last.ContentType);
			string render = ((MockWebResponse)context.Response).Result;
			Assert.AreEqual(context.ViewSteps.Last.Content, render);

			XElement result1 = context.Resources.Open("Resources/Results/result-1-1.xml").AsXElement();		
			Assert.IsTrue(XNode.DeepEquals(result1, render.AsXElement()));		
		}

		/// <summary>
		/// In this test we set the current action to "test1" and views to "st" indicating StringTemplate, then check
		/// that the resulting templated transform has produced what we expect. This test
		/// involves locating the correct view behaviour it resolving the correct template to use.
		/// </summary>
		[TestMethod]
		public void BasicStringTemplateView() {
			IWebContext context = this.GetContext();
			context.Params["action"] = "test1";
			context.Params["views"] = "st";
			context.Fire("test");

			Assert.IsTrue(context.ViewSteps.HasSteps);
			Assert.IsTrue(context.ViewSteps.Count == 2);
			Assert.IsTrue(context.ViewSteps.Last.HasContent);
			Assert.IsTrue(context.ViewSteps.Last.ContentType == "text/html");

			Assert.IsTrue(context.Response.ContentType == context.ViewSteps.Last.ContentType);
			string render = ((MockWebResponse)context.Response).Result;
			Assert.AreEqual(context.ViewSteps.Last.Content, render);

			XElement result1 = context.Resources.Open("Resources/Results/result-1-1.xml").AsXElement();
			Assert.IsTrue(XNode.DeepEquals(result1, render.AsXElement()));		
		}

		/// <summary>
		/// In this test we specify the chained views of "st;xml" meaning the resulting
		/// view state will be processed first by the string template view behaviour the
		/// result of which is used as input for the xml view behaviour, which in this
		/// case simply ensures the result is xml and outputs it. We confirm that the xml
		/// remained unchanged through the process.
		/// </summary>
		[TestMethod]
		public void BasicViewChain() {
			IWebContext context = this.GetContext();
			context.Params["action"] = "test1";
			context.Params["views"] = "st;xml";
			context.Fire("test");

			Assert.IsTrue(context.ViewSteps.HasSteps);
			Assert.IsTrue(context.ViewSteps.Count == 3);
			Assert.IsTrue(context.ViewSteps.Last.HasContent);
			Assert.IsTrue(context.ViewSteps.Last.ContentType == "text/xml");

			Assert.IsTrue(context.Response.ContentType == context.ViewSteps.Last.ContentType);
			string render = ((MockWebResponse)context.Response).Result;
			Assert.AreEqual(context.ViewSteps.Last.Content, render);

			XElement result1 = context.Resources.Open("Resources/Results/result-1-1.xml").AsXElement();
			Assert.IsTrue(XNode.DeepEquals(result1, render.AsXElement()));		
		}

		/// <summary>
		/// <para>
		/// In this test we specify the chained views "st;xsl;xml" meaning the resulting
		/// view state will be processed first by a located string template the result
		/// of which will be used as the input for a located xslt template, which in turn
		/// will be passed to the xml view behaviour. 
		/// </para>
		/// <para>
		/// In this test we are actually transforming the shape of the data and we test that the
		/// end result of the transforms conforms to expectation.
		/// </para>
		/// </summary>
		[TestMethod]
		public void ChainedViewTransform() {
			IWebContext context = this.GetContext();		
			((MockWebRequest)context.Request).UrlInfo = new UrlInfo("http://something.com/web.test/test2.aspx/st/xsl/xml");
			context.Fire("test");

			Assert.IsTrue(context.ViewSteps.HasSteps);
			Assert.IsTrue(context.ViewSteps.Count == 4);
			Assert.IsTrue(context.ViewSteps.Last.HasContent);
			Assert.IsTrue(context.ViewSteps.Last.ContentType == "text/xml");
			Assert.IsTrue(context.ViewSteps.ElementAt(1).ContentType == "text/html");

			Assert.IsTrue(context.Response.ContentType == context.ViewSteps.Last.ContentType);
			string render = ((MockWebResponse)context.Response).Result;
			Assert.AreEqual(context.ViewSteps.Last.Content, render);

			XElement result2 = context.Resources.Open("Resources/Results/result-2.xml").AsXElement();
			Assert.IsTrue(XNode.DeepEquals(result2, render.AsXElement()));	
		}
	}
}

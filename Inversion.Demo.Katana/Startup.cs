using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Inversion.Process;
using Inversion.Process.Behaviour;
using Inversion.Data;
// using Inversion.StringTemplate.Behaviour.View;
using Inversion.Web;
// using Inversion.Web.Owin;
using Inversion.Web.Behaviour;
using Inversion.Web.Behaviour.View;
using Inversion.Naiad;
using Inversion.Collections;
using System.Security.Principal;
using System.IO;
using Microsoft.AspNetCore.Http.Features;

namespace Inversion.Demo.Katana
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

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
						// new StringTemplateViewBehaviour("st::view", "text/html")
					};
				}
			);


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                IWebContext webContext = new AspNetCoreContext(context, ServiceContainer.Instance, new FileSystemResourceAdapter(@"e:\Users\User\Documents\GitHub\inversion-dev\Inversion.Demo.Katana"));
				IList<IProcessBehaviour> behaviours = webContext.Services.GetService<List<IProcessBehaviour>>("request-behaviours");
				webContext.Register(behaviours);

				webContext.Timers.Begin("process-request");
				webContext.Fire("process-request");
				webContext.Completed();
            });
        }
    }

    public class AspNetCoreContext : ProcessContext, IWebContext
    {
        public IWebApplication Application => throw new NotImplementedException();

        public IWebResponse Response => new AspNetCoreWebResponse(this.context);

        public IWebRequest Request => new AspNetCoreWebRequest(this.context);

        public IPrincipal User { get => this.context.User; set => throw new NotImplementedException(); }


        private HttpContext context;

        public AspNetCoreContext(HttpContext context, ServiceContainer services, FileSystemResourceAdapter resources)
            : base(services, resources)
        {
            this.context = context;
        }
    }

    internal class AspNetCoreWebRequest : IWebRequest
    {
        private HttpContext context;

        public AspNetCoreWebRequest(HttpContext context)
        {
            this.context = context;
        }

        public IRequestFilesCollection Files => throw new NotImplementedException();

        // TODO: convert this less weird. The port is missing.
        public UrlInfo UrlInfo => new UrlInfo(new Uri($"{this.context.Request.Scheme}://{this.context.Request.Host}/{this.context.Request.Path.Value}?{this.context.Request.QueryString}"));

        public string Method => this.context.Request.Method;

        public bool IsGet => this.Method.ToLower() == "get";

        public bool IsPost => this.Method.ToLower() == "post";

        public IDictionary<string, string> Params => new Dictionary<string, string>(this.context.Request.Query.Select(q => new KeyValuePair<string, string>(q.Key, string.Join(",", q.Value))));

        public string Payload => this.context.Request.Body.AsText();

        public IEnumerable<string> Flags => this.context.Request.Query.Where(x => string.IsNullOrEmpty(x.Value)).Select(x => x.Key);

        public IDictionary<string, string> Headers => this.context.Request.Headers.ToDictionary(x => x.Key, x => string.Join(",", x.Value));

        public Web.IRequestCookieCollection Cookies => throw new NotImplementedException();
    }

    internal class AspNetCoreWebResponse : IWebResponse
    {
        private HttpContext context;

        public AspNetCoreWebResponse(HttpContext context)
        {
            this.context = context;
        }

        public TextWriter Output => new StreamWriter(this.OutputStream);

        public Stream OutputStream => this.context.Response.Body;

        public int StatusCode { get => this.context.Response.StatusCode; set => this.context.Response.StatusCode = value; }
        public string StatusDescription {
            get
            {
                return context.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase;
            }
            set
            {
                context.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = value;
            }
        }

        public string ContentType { get => this.context.Response.ContentType; set => this.context.Response.ContentType = value; }

        public IResponseCookieCollection Cookies => throw new NotImplementedException();

        public IResponseHeaderCollection Headers => throw new NotImplementedException();

		/// <summary>
		/// Flushes the response steam and ends the response.
		/// </summary>
		public void End() {
			// nothing to do on an OWIN implementation that is apparent to me at this point.
		}

		/// <summary>
		/// Writes the provided text to the response stream.
		/// </summary>
		/// <param name="text">The text to write to the response stream.</param>
		public void Write(string text) {
			this.Output.Write(text);
		}

		/// <summary>
		/// Writes the provided formatted text to the response stream.
		/// </summary>
		/// <param name="text">The text to write to the response stream.</param>
		/// <param name="args">The arguments to interpolate into the text.</param>
		public void WriteFormat(string text, params object[] args) {
			this.Output.Write(string.Format(text, args));
		}

		/// <summary>
		/// Redirects the request to the provided url.
		/// </summary>
		/// <param name="url">The url to redirect to.</param>
		public void Redirect(string url) {
			this.context.Response.Redirect(url);
		}

		/// <summary>
		/// Redirects the request permanently to the provided url
		/// issuing a `301` in the response.
		/// </summary>
		/// <param name="url"></param>
		public void PermanentRedirect(string url) {
			this.StatusCode = 301;
			this.StatusDescription = "301 Moved Permanently";
			this.context.Response.Headers.Append("Location", url);
		}
    }
}

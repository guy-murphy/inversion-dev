using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Security.Principal;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;

using Inversion.Process;
using Inversion.Web;

namespace Inversion.Web.AspNetCore
{
    public class AspNetCoreWebResponse : IWebResponse
    {
        private HttpContext context;

        public AspNetCoreWebResponse(HttpContext context)
        {
            this.context = context;
        }

        public TextWriter Output => throw new NotImplementedException();

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
            this.context.Response.WriteAsync(text).Wait();
		}

		/// <summary>
		/// Writes the provided formatted text to the response stream.
		/// </summary>
		/// <param name="text">The text to write to the response stream.</param>
		/// <param name="args">The arguments to interpolate into the text.</param>
		public void WriteFormat(string text, params object[] args) {
			this.context.Response.WriteAsync(string.Format(text, args)).Wait();
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
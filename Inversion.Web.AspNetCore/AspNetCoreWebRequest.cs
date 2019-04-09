using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Security.Principal;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

using Inversion.Data;
using Inversion.Web;

namespace Inversion.Web.AspNetCore
{
  public class AspNetCoreWebRequest : IWebRequest
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

        private string _cachedPayload { get; set; }

        private readonly object _payloadCacheLock = new object();

        public string Payload
        {
            get
            {
                if (String.IsNullOrEmpty(_cachedPayload))
                {
                    lock (_payloadCacheLock)
                    {
                        if (String.IsNullOrEmpty(_cachedPayload))
                        {
                            this.context.Request.EnableRewind();
                            using (StreamReader reader = new StreamReader(
                                stream: this.context.Request.Body,
                                encoding: System.Text.Encoding.UTF8,
                                detectEncodingFromByteOrderMarks: true,
                                bufferSize: 1024,
                                leaveOpen: true))
                            {
                                _cachedPayload = reader.ReadToEnd();
                                this.context.Request.Body.Position = 0;
                            }
                        }
                    }
                }

                return _cachedPayload;
            }
        }

        public IEnumerable<string> Flags => this.context.Request.Query.Where(x => string.IsNullOrEmpty(x.Value)).Select(x => x.Key);

        public IDictionary<string, string> Headers => this.context.Request.Headers.ToDictionary(x => x.Key, x => string.Join(",", x.Value));

        public Web.IRequestCookieCollection Cookies => new AspNetCoreWebCookieCollection(this.context.Request.Cookies);
    }
}
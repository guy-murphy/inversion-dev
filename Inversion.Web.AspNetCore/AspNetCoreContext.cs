using System;
using System.Security.Principal;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;

using Inversion.Process;
using Inversion.Web;
using Inversion.Naiad;
using Inversion.Data;

namespace Inversion.Web.AspNetCore
{
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
}

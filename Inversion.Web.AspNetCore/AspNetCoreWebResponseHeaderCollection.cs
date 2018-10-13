using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Inversion.Web.AspNetCore
{
    public class AspNetCoreWebResponseHeaderCollection : IResponseHeaderCollection
    {
        private readonly HttpResponse _response;

        public AspNetCoreWebResponseHeaderCollection(HttpResponse response)
        {
            _response = response;
        }

        public void Append(string key, string value)
        {
            _response.Headers.Append(key, value);
        }
    }
}

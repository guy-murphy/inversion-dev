using System;
using System.Collections.Generic;
using System.Linq;

namespace Inversion.Web.AspNetCore
{
    public class AspNetCoreCookie
    {
        private string[] _values = new String[] { };
        public CookieOptions Options;

        public AspNetCoreCookie(string value, CookieOptions options = null)
        {
            _values = new string[] { value };
            this.Options = options;
        }

        public AspNetCoreCookie(string[] values, CookieOptions options = null)
        {
            _values = values;
            this.Options = options;
        }

        public string GetValue()
        {
            return String.Join('&', _values);
        }

        public string[] GetValues()
        {
            return _values;
        }

        public IDictionary<string, string> AsDictionary()
        {
            return _values.ToDictionary(
                x => x.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)[0],
                x => x.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)[1]);
        }
    }
}
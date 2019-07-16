using System;
using System.Collections.Generic;
using System.Linq;

namespace Inversion.Web.AspNetCore
{
    public class AspNetCoreCookie
    {
        private readonly string[] _values;
        public CookieOptions Options;

        public AspNetCoreCookie(string value, CookieOptions options = null)
        {
            this.Options = options;

            if (value.Contains(','))
            {
                _values = value.Split(',', StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                _values = new string[] { value };
            }
        }

        public AspNetCoreCookie(string[] values, CookieOptions options = null)
        {
            this.Options = options;

            List<string> candidate = new List<string>();

            foreach (string value in values)
            {
                if (value.Contains(','))
                {
                    candidate.AddRange(value.Split(',', StringSplitOptions.RemoveEmptyEntries));
                }
                else
                {
                    candidate.Add(value);
                }
            }

            _values = candidate.ToArray();
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
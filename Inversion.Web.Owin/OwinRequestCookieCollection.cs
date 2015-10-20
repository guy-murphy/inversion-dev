using System;
using System.Collections.Generic;

using Microsoft.Owin;

namespace Inversion.Web.Owin {
	public class OwinRequestCookieCollection : IRequestCookieCollection {

		private readonly RequestCookieCollection _cookies;

		public string this[string key] {
			get { return this.Get(key); }
		}

		public OwinRequestCookieCollection(RequestCookieCollection cookies) {
			_cookies = cookies;
		}

		public string Get(string key) {
			string value;
			this.TryGetValue(key, out value);
			return value;
		}

		public bool TryGetValue(string key, out string value) {
			foreach (KeyValuePair<string, string> entry in _cookies) {
				if (entry.Key == key) {
					value = entry.Value;
					return true;
				}
			}
			value = String.Empty;
			return false;
		}

        public IDictionary<string, string> GetValues(string key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValues(string key, out IDictionary<string, string> values)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _cookies.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _cookies.GetEnumerator();
        }
    }
}
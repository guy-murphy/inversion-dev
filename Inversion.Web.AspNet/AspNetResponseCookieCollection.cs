using System;
using System.Web;

namespace Inversion.Web.AspNet {
	public class AspNetResponseCookieCollection : IResponseCookieCollection {

		private readonly HttpCookieCollection _cookies;


		public AspNetResponseCookieCollection(HttpCookieCollection cookies) {
			_cookies = cookies;
		}

		public void Append(string key, string value, CookieOptions options) {
			HttpCookie cookie = new HttpCookie(key, value) {
				Path = options.Path
			};

			if (!String.IsNullOrEmpty(options.Domain)) cookie.Domain = options.Domain;
			if (options.Secure) cookie.Secure = true;
			if (options.HttpOnly) cookie.HttpOnly = true;
			if (options.Expires != null) cookie.Expires = options.Expires.Value;

			_cookies.Add(cookie); // internally this performs an append
		}

		public void Append(string key, string value) {
			_cookies.Add(new HttpCookie(key, value)); // internally performs an append
		}

		public void Delete(string key) {
			_cookies.Remove(key);
		}

		
	}
}

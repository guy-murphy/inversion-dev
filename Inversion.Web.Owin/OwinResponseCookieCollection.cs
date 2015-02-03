using Microsoft.Owin;

namespace Inversion.Web.Owin {
	public class OwinResponseCookieCollection : IResponseCookieCollection {

		private readonly ResponseCookieCollection _cookies;

		public OwinResponseCookieCollection(ResponseCookieCollection cookies) {
			_cookies = cookies;
		}

		public void Append(string key, string value, CookieOptions options) {
			Microsoft.Owin.CookieOptions owinOptions = new Microsoft.Owin.CookieOptions {
				Path = options.Path,
				Domain = options.Domain,
				Secure = options.Secure,
				HttpOnly = options.HttpOnly,
				Expires = options.Expires
			};
			_cookies.Append(key, value, owinOptions);
		}

		public void Append(string key, string value) {
			_cookies.Append(key, value);
		}

		public void Delete(string key) {
			_cookies.Delete(key);
		}
	}
}

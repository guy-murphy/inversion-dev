using System;

namespace Inversion.Web.AspNetCore
{
    public class AspNetCoreWebResponseCookieCollection : IResponseCookieCollection
    {
        private readonly Microsoft.AspNetCore.Http.IResponseCookies _cookies;

        public AspNetCoreWebResponseCookieCollection(Microsoft.AspNetCore.Http.IResponseCookies cookies)
        {
            _cookies = cookies;
        }

        public void Append(string key, string value, CookieOptions options)
        {
            Microsoft.AspNetCore.Http.CookieOptions cookieOptions = new Microsoft.AspNetCore.Http.CookieOptions();
            if (!String.IsNullOrEmpty(options.Domain)) cookieOptions.Domain = options.Domain;
            if (options.Secure) cookieOptions.Secure = true;
            if (options.HttpOnly) cookieOptions.HttpOnly = true;
            if (options.Expires != null) cookieOptions.Expires = options.Expires.Value;

            switch (options.SameSite)
            {
                case CookieOptions.CookieOptionSameSite.None: cookieOptions.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None; break;
                case CookieOptions.CookieOptionSameSite.Lax: cookieOptions.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax; break;
                case CookieOptions.CookieOptionSameSite.Strict: cookieOptions.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict; break;
            }

            _cookies.Append(key, value, cookieOptions);
        }

        public void Append(string key, string value)
        {
            _cookies.Append(key, value);
        }

        public void Delete(string key)
        {
            _cookies.Delete(key);
        }

        public void Append(string key, string[] values, CookieOptions options)
        {
            Microsoft.AspNetCore.Http.CookieOptions cookieOptions = new Microsoft.AspNetCore.Http.CookieOptions();
            if (!String.IsNullOrEmpty(options.Domain)) cookieOptions.Domain = options.Domain;
            if (options.Secure) cookieOptions.Secure = true;
            if (options.HttpOnly) cookieOptions.HttpOnly = true;
            if (options.Expires != null) cookieOptions.Expires = options.Expires.Value;

            switch (options.SameSite)
            {
                case CookieOptions.CookieOptionSameSite.None: cookieOptions.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None; break;
                case CookieOptions.CookieOptionSameSite.Lax: cookieOptions.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax; break;
                case CookieOptions.CookieOptionSameSite.Strict: cookieOptions.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict; break;
            }

            _cookies.Append(key, String.Join(',', values), cookieOptions);
        }

        public void Append(string key, string[] values)
        {
            _cookies.Append(key, String.Join(',', values));
        }
    }
}

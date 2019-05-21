using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Inversion.Web.AspNetCore
{
    public class AspNetCoreWebRequestCookieCollection : IRequestCookieCollection
    {
        private readonly Dictionary<string, AspNetCoreCookie> _collection = new Dictionary<string, AspNetCoreCookie>();

        public AspNetCoreWebRequestCookieCollection(IEnumerable<KeyValuePair<string, string>> source)
        {
            foreach (KeyValuePair<string, string> kvp in source)
            {
                _collection.Add(kvp.Key, new AspNetCoreCookie(kvp.Value));
            }
        }

        public string Get(string key)
        {
            return _collection[key].GetValue();
        }

        public bool TryGetValue(string key, out string value)
        {
            AspNetCoreCookie cookie = null;
            if (_collection.TryGetValue(key, out cookie))
            {
                value = cookie.GetValue();
                return true;
            }
            value = null;
            return false;
        }

        public IDictionary<string, string> GetValues(string key)
        {
            return _collection[key].AsDictionary();
        }

        public bool TryGetValues(string key, out IDictionary<string, string> values)
        {
            if (_collection.ContainsKey(key))
            {
                values = this.GetValues(key);
                return true;
            }

            values = null;
            return false;
        }

        public string this[string key] => this.Get(key);

        //public void Append(string key, string value, CookieOptions options)
        //{
        //    // ignore options for now
        //    _collection.Add(key, new AspNetCoreCookie(value));
        //}

        //public void Append(string key, string value)
        //{
        //    _collection.Add(key, new AspNetCoreCookie(value));
        //}

        //public void Delete(string key)
        //{
        //    _collection.Remove(key);
        //}

        //public void Append(string key, string[] values, CookieOptions options)
        //{
        //    _collection.Add(key, new AspNetCoreCookie(values, options));
        //}

        //public void Append(string key, string[] values)
        //{
        //    _collection.Add(key, new AspNetCoreCookie(values));
        //}

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            foreach(string key in _collection.Keys)
            {
                yield return new KeyValuePair<string, string>(key, this[key]);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

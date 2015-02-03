using System.Collections.Generic;

namespace Inversion.Web {
	public interface IRequestCookieCollection : IEnumerable<KeyValuePair<string, string>> {
		string this[string key] { get; }
		string Get(string key);
		bool TryGetValue(string key, out string value);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inversion.Web {
	public interface IResponseCookieCollection {
		void Append(string key, string value, CookieOptions options);
		void Append(string key, string value);
		void Delete(string key);
	}
}

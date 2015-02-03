using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inversion.Web {
	public interface IRequestFilesCollection : IEnumerable<KeyValuePair<string, IRequestFile>> {
		IRequestFile this[string key] { get; }
		IRequestFile Get(string key);
		bool TryGetValue(string key, out IRequestFile value);
	}
}

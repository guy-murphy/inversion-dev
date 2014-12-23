using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inversion.Process.Behaviour {
	public static class BehaviourConditionEx {

		public static bool MacthesAllParameterValues(this ApplicationBehaviour self, ProcessContext ctx) {
			if (self.NamedMaps.ContainsKey("matching-all-parameter-values")) {
				IDictionary<string, string> map = self.NamedMaps["matching-all-parameter-values"];
				if (map != null) {
					bool r = map.All(p => ctx.Params.Contains(p) && ctx.Params[p.Key] == p.Value);
					return r;
				}
			}
			return false;
		}

	}
}

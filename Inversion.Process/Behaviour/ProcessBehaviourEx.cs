using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inversion.Process.Behaviour {
	public static class ProcessBehaviourEx {

		public static void FireSequenceFromConfig(this IPrototypedBehaviour self, IProcessContext ctx, string frame) {
			foreach (string slot in self.Configuration.GetSlots(frame)) {
				ctx.Fire(slot, self.Configuration.GetMap(frame, slot));
			}
		}

	}
}

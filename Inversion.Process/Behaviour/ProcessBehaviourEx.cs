using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inversion.Process.Behaviour {
	/// <summary>
	/// Extension methods acting upon `IPrototypedBehaviour` objects.
	/// </summary>
	public static class ProcessBehaviourEx {
		/// <summary>
		/// Fires a sequence of configured messages configured in a given frame.
		/// </summary>
		/// <param name="self">The prototype behaviour being acted upon.</param>
		/// <param name="ctx">The context to fire the messages on.</param>
		/// <param name="frame">The frame within the behaviours config to obtain messages from.</param>
		public static void FireSequenceFromConfig(this IPrototypedBehaviour self, IProcessContext ctx, string frame) {
			foreach (string slot in self.Configuration.GetSlots(frame)) {
				ctx.Fire(slot, self.Configuration.GetMap(frame, slot));
			}
		}

	}
}

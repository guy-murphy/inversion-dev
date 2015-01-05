using System;
using System.Collections.Generic;
using Inversion.Collections;

namespace Inversion.Process.Behaviour {

	/// <summary>
	/// A simple behaviour to wire up to test the simplest possible output.
	/// </summary>
	public class MessageTraceBehaviour : ProcessBehaviour {

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The name of the behaviour.</param>
		public MessageTraceBehaviour(string respondsTo) : base(respondsTo) {}

		/// <summary>
		/// The action to perform when the `Condition(IEvent)` is met.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		public override void Action(IEvent ev, ProcessContext context) {
			DataCollection<IEvent> events = context.ControlState["eventTrace"] as DataCollection<IEvent> ?? new DataCollection<IEvent>();
			events.Add(ev);
			context.ControlState["eventTrace"] = events;
		}
	}
}

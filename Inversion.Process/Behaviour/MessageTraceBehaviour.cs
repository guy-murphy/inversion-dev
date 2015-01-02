using System;
using System.Collections.Generic;
using Inversion.Collections;

namespace Inversion.Process.Behaviour {

	/// <summary>
	/// A simple behaviour to wire up to test the simplest possible output.
	/// </summary>
	public class MessageTraceBehaviour : ConfiguredBehaviour {
		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The name of the behaviour.</param>
		/// <param name="config">Configuration for the behaviour.</param>
		//public MessageTraceBehaviour(string respondsTo, BehaviourConfiguration config) : base(respondsTo, config) {}

		public MessageTraceBehaviour(string respondsTo, IEnumerable<BehaviourConfiguration.Element> config) : base(respondsTo, config) {}

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

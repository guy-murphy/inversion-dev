using System.Collections.Generic;

using Inversion.Collections;

namespace Inversion.Process.Behaviour {

	/// <summary>
	/// A simple behaviour to wire up to test the simplest possible output.
	/// </summary>
	public class MessageTraceBehaviour : PrototypedBehaviour {
		/// <summary>
		/// Creates a new instance of the behaviour with no configuration.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		public MessageTraceBehaviour(string respondsTo) : base(respondsTo) {}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		/// <param name="prototype">Configuration and prototyping for the behaviour.</param>
		public MessageTraceBehaviour(string respondsTo, Prototype prototype) : base(respondsTo, prototype) { }

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		/// <param name="config">Configuration for the behaviour.</param>
		public MessageTraceBehaviour(string respondsTo, IEnumerable<IConfigurationElement> config) : base(respondsTo, config) { }

		/// <summary>
		/// The action to perform when the `Condition(IEvent)` is met.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		public override void Action(IEvent ev, IProcessContext context) {
			DataCollection<IEvent> events = context.ControlState["eventTrace"] as DataCollection<IEvent> ?? new DataCollection<IEvent>();
			events.Add(ev);
			context.ControlState["eventTrace"] = events;
		}
	}
}

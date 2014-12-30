using System;

using Inversion.Collections;

namespace Inversion.Process.Behaviour {

	/// <summary>
	/// A simple behaviour to wire up to test the simplest possible output.
	/// </summary>
	public class MessageTraceBehaviour : ProcessBehaviour {

		/// <summary>
		/// Creates a new instance of a message trace behaviour,
		/// which normally would be created without a specified message
		/// to respond to.
		/// </summary>
		public MessageTraceBehaviour() : this(String.Empty) { }

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="message">The name of the behaviour.</param>
		public MessageTraceBehaviour(string message) : base(message) {}

		/// <summary>
		/// Determines if the event specifies the behaviour by name.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context to consult.</param>
		/// <returns>
		/// Returns true if true if `ev.Message` is the same as `this.Message`
		///  </returns>
		/// <remarks>
		/// The intent is to override for bespoke conditions.
		/// </remarks>
		public override bool Condition(IEvent ev, ProcessContext context) {
			return true; // match all messages
		}

		/// <summary>
		/// The action to perform when the `Condition(IEvent)` is met.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		public override void Action(IEvent ev, ProcessContext context) {
			DataCollection<IEvent> events = context.ControlState["event-trace"] as DataCollection<IEvent> ?? new DataCollection<IEvent>();
			events.Add(ev);
			context.ControlState["event-trace"] = events;
		}
	}
}

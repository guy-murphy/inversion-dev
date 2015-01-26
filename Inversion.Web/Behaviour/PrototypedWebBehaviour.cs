using System.Collections.Generic;

using Inversion.Process;
using Inversion.Process.Behaviour;

namespace Inversion.Web.Behaviour {
	/// <summary>
	/// A web behaviour that can be configured with a prototype specification
	/// of selection criteria used to drive the behaviours condition.
	/// </summary>
	public abstract class PrototypedWebBehaviour: PrototypedBehaviour, IWebBehaviour {
		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The name of the behaviour.</param>
		protected PrototypedWebBehaviour(string respondsTo) : base(respondsTo) {}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The name of the behaviour.</param>
		/// <param name="prototype">The prototype to use in configuring this behaviour.</param>
		protected PrototypedWebBehaviour(string respondsTo, IPrototype prototype) : base(respondsTo, prototype) { }

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The name of the behaviour.</param>
		/// <param name="config">The configuration elements to use in configuring this behaviour.</param>
		protected PrototypedWebBehaviour(string respondsTo, IEnumerable<IConfigurationElement> config) : base(respondsTo, config) { }

		/// <summary>
		/// Determines if this behaviours action should be executed in
		/// response to the provided event.
		/// </summary>
		/// <param name="ev">The event to consider.</param>
		/// <returns>Returns true if this behaviours action to execute in response to this event; otherwise returns  false.</returns>
		public override bool Condition(IEvent ev) {
			return this.Condition(ev, (IWebContext)ev.Context);
		}

		/// <summary>
		/// The considtion that determines whether of not the behaviours action
		/// is valid to run.
		/// </summary>
		/// <param name="ev">The event to consider with the condition.</param>
		/// <param name="context">The context to use.</param>
		/// <returns>
		/// `true` if the condition is met; otherwise,  returns  `false`.
		/// </returns>
		public abstract bool Condition(IEvent ev, IWebContext context);

		/// <summary>
		/// The action to perform if this behaviours condition is met.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		public override void Action(IEvent ev) {
			this.Action(ev, (IWebContext)ev.Context);
		}

		/// <summary>
		/// The action to perform if this behaviours condition is met.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		public abstract void Action(IEvent ev, IWebContext context);
	}
}

using System;

namespace Inversion.Process.Behaviour {

	/// <summary>
	/// A behaviour that facilitates creating behaviours whose conditions and actions
	/// are assigned at runtime not compile time.
	/// </summary>
	public class RuntimeBehaviour : ProcessBehaviour {

		private readonly Predicate<IEvent> _condition;
		private readonly Action<IEvent, ProcessContext> _action;

		/// <summary>
		/// Instantiates a new runtime behaviour.
		/// </summary>
		/// <param name="name">The name by which the behaviour is known to the system.</param>
		protected RuntimeBehaviour(string name) : base(name) { }

		/// <summary>
		/// Instantiates a new runtime behaviour.
		/// </summary>
		/// <param name="name">The name by which the behaviour is known to the system.</param>
		/// <param name="condition">The predicate that will determine if this behaviours action should be executed.</param>
		/// <param name="action">The action that should be performed if this behaviours conditions are met.</param>
		public RuntimeBehaviour(string name, Predicate<IEvent> condition, Action<IEvent, ProcessContext> action)
			: base(name) {
			_condition = condition;
			_action = action;
		}

		/// <summary>
		/// Determines if this behaviours action should be executed in
		/// response to the provided event.
		/// </summary>
		/// <param name="ev">The event to consider.</param>
		/// <returns>Returns true if this behaviours action to execute in response to this event; otherwise returns  false.</returns>
		public override bool Condition(IEvent ev) {
			return _condition(ev);
		}

		/// <summary>
		/// The action to perform if this behaviours condition is met.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		public override void Action(IEvent ev, ProcessContext context) {
			_action(ev, context);
		}
	}
}

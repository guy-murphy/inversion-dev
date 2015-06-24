using System.Collections.Generic;

namespace Inversion.Process.Behaviour {
	/// <summary>
	/// A behaviour that sets configured flags on the context 
	/// upon which the behaviours action executes.
	/// </summary>
	public class SetFlagBehaviour : PrototypedBehaviour {
		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour responds to.</param>
		public SetFlagBehaviour(string respondsTo) : base(respondsTo) { }
		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour responds to.</param>
		/// <param name="prototype">The prototype to use in configuring this behaviour.</param>
		public SetFlagBehaviour(string respondsTo, IPrototype prototype) : base(respondsTo, prototype) { }
		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour responds to.</param>
		/// <param name="config">The configuration elements to use in configuring this behaviour.</param>
		public SetFlagBehaviour(string respondsTo, IEnumerable<IConfigurationElement> config) : base(respondsTo, config) { }
		/// <summary>
		/// The action to perform when the `Condition(IEvent)` is met.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		public override void Action(IEvent ev, IProcessContext context) {
			// GM.150624: Consider changing this to act on ("config","flag")
			foreach (string name in this.Configuration.GetNames("config", "set")) {
				context.Flags.Add(name);
			}
		}
	}
}
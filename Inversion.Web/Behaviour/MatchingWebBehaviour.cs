using System.Collections.Generic;

using Inversion.Process;
using Inversion.Process.Behaviour;

namespace Inversion.Web.Behaviour {
	/// <summary>
	/// Provides a web behaviour with common config driven selection criteria.
	/// </summary>
	public abstract class MatchingWebBehaviour: MatchingBehaviour, IWebBehaviour {
		/// <summary>
		/// Creates a new instance of the behaviour with no configuration.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		protected MatchingWebBehaviour(string respondsTo) : base(respondsTo) {}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		/// <param name="config">Configuration for the behaviour.</param>
		protected MatchingWebBehaviour(string respondsTo, Configuration config) : base(respondsTo, config) { }

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		/// <param name="config">Configuration for the behaviour.</param>
		protected MatchingWebBehaviour(string respondsTo, IEnumerable<Configuration.Element> config) : base(respondsTo, config) { }

		/// <summary>
		/// The considtion that determines whether of not the behaviours action
		/// is valid to run.
		/// </summary>
		/// <param name="ev">The event to consider with the condition.</param>
		/// <param name="context">The context to use.</param>
		/// <returns>
		/// `true` if the condition is met; otherwise,  returns  `false`.
		/// </returns>
		public bool Condition(IEvent ev, IWebContext context) {
			return base.Condition(ev, context) && 
				this.HasAnyUserRoles(context);
		}

		/// <summary>
		/// The action to perform if this behaviours condition is met.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		public abstract void Action(IEvent ev, IWebContext context);

	}
}

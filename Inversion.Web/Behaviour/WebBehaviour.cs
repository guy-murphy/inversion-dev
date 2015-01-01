using System.Collections.Generic;

using Inversion.Process;
using Inversion.Process.Behaviour;

namespace Inversion.Web.Behaviour {
	/// <summary>
	/// An abstract provision of basic web-centric features for process behaviours
	/// being used in a web application.
	/// </summary>
	public abstract class WebBehaviour : MatchingBehaviour, IWebBehaviour {
		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The name of the behaviour.</param>
		/// <param name="namedLists">Named lists used to configure this behaviour.</param>
		/// <param name="namedMaps">Named maps used to configure this behaviour.</param>
		/// <param name="namedMappedLists">Named maps of lists used to configure this behaviour.</param>
		protected WebBehaviour(string respondsTo, 
			IDictionary<string, IEnumerable<string>> namedLists = null,
			IDictionary<string, IDictionary<string, string>> namedMaps = null,
			IDictionary<string, IDictionary<string, IEnumerable<string>>> namedMappedLists = null)
			: base(respondsTo, namedLists, namedMaps, namedMappedLists) { }

		/// <summary>
		/// Determines if this behaviours action should be executed in
		/// response to the provided event.
		/// </summary>
		/// <param name="ev">The event to consider.</param>
		/// <returns>Returns true if this behaviours action to execute in response to this event; otherwise returns  false.</returns>
		public override bool Condition(IEvent ev) {
			return this.Condition(ev, (WebContext)ev.Context);
		}

		/// <summary>
		/// Determines if this behaviours action should be executed in
		/// response to the provided event and context.
		/// </summary>
		/// <param name="ev">The event to consider.</param>
		/// <param name="context">The context to consider.</param>
		/// <returns></returns>
		public virtual bool Condition(IEvent ev, WebContext context) {
			// check the base condition
			// and then whether the user for the current context
			// has any of the roles specified
			return base.Condition(ev) && this.HasAnyUserRoles(context);
		}

		/// <summary>
		/// The action to perform if this behaviours condition is met.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		public override void Action(IEvent ev) {
			this.Action(ev, (WebContext)ev.Context);
		}

		/// <summary>
		/// The action to perform if this behaviours condition is met.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		public override void Action(IEvent ev, ProcessContext context) {
			this.Action(ev, (WebContext)context);
		}

		/// <summary>
		/// Implementors should impliment this behaviour with the desired action
		/// for their behaviour.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		public abstract void Action(IEvent ev, WebContext context);

	}
}

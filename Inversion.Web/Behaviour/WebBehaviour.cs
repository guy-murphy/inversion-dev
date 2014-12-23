using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Inversion.Process;
using Inversion.Process.Behaviour;

namespace Inversion.Web.Behaviour {
	/// <summary>
	/// An abstract provision of basic web-centric features for process behaviours
	/// being used in a web application.
	/// </summary>
	public abstract class WebBehaviour : ProcessBehaviour, IWebBehaviour {

		private ImmutableList<string> _requiredRoles;

		/// <summary>
		/// Provides access to an enumeration of the user roles for this behaviour,
		/// one of which the user would need to possess for execution of this behaviours
		/// action to occur.
		/// </summary>
		/// <remarks>These roles would normally be configured for the behavior from the service container.</remarks>
		public IEnumerable<string> RequiredRoles {
			get { return _requiredRoles ?? (_requiredRoles = ImmutableList.Create<string>()); }
			set {
				if (_requiredRoles != null) throw new InvalidOperationException("You may not assign RequiredRoles once it has been set.");
				if (value == null) throw new ArgumentNullException("value");
				_requiredRoles = ImmutableList.Create<String>(value.ToArray()); // double pass, change
			}
		}

		/// <summary>
		/// Ensures on instantiattion that the base process behaviour
		/// contructor is called with the provided message.
		/// </summary>
		/// <param name="message">The message this behaviour responds to.</param>
		protected WebBehaviour(string message) : base(message) { }

		/// <summary>
		/// Ensures on instantiattion that the base process behaviour
		/// contructor is called with the provided message.
		/// </summary>
		/// <param name="message">The message this behaviour responds to.</param>
		/// <param name="preprocess">Specifies whether or not preprocessing of this behaviours action should occur.</param>
		/// <param name="postprocess">Specifies whether or not postprocessing of this behaviours action should occur.</param>
		protected WebBehaviour(string message, bool preprocess, bool postprocess) : base(message, preprocess, postprocess) { }

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
			// and then either there are no roles specified
			// or the user is in any of the roles defined
			return base.Condition(ev) && ((_requiredRoles == null || _requiredRoles.Count == 0) || this.RequiredRoles.Any(role => context.User.IsInRole(role)));
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

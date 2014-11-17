using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Inversion.Process;

namespace Inversion.Web.Behaviour {
	public abstract class WebBehaviour : ProcessBehaviour, IWebBehaviour {

		private ImmutableList<string> _requiredRoles;

		public IEnumerable<string> RequiredRoles {
			get { return _requiredRoles ?? (_requiredRoles = ImmutableList.Create<string>()); }
			set {
				if (_requiredRoles != null) throw new InvalidOperationException("You may not assign RequiredRoles once it has been set.");
				if (value == null) throw new ArgumentNullException("value");
				_requiredRoles = ImmutableList.Create<string>(value.ToArray()); // double pass, change
			}
		}

		protected WebBehaviour(string message) : base(message) { }

		public override bool Condition(IEvent ev) {
			return this.Condition(ev, (WebContext)ev.Context);
		}

		public virtual bool Condition(IEvent ev, WebContext context) {
			// check the base condition
			// and then either there are no roles specified
			// or the user is in any of the roles defined
			return base.Condition(ev) && ((_requiredRoles == null || _requiredRoles.Count == 0) || this.RequiredRoles.Any(role => context.User.IsInRole(role)));
		}

		public override void Action(IEvent ev) {
			this.Action(ev, (WebContext)ev.Context);
		}

		public override void Action(IEvent ev, ProcessContext context) {
			this.Action(ev, (WebContext)context);
		}

		public abstract void Action(IEvent ev, WebContext context);

	}
}

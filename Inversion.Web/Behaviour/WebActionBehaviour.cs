using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Inversion.Process;

namespace Inversion.Web.Behaviour {
	public abstract class WebActionBehaviour : WebBehaviour {

		// These fields were originally readonly and were injected via the constructor.
		// These fields are going to get added to over time which is going to make the
		//		constructor fat, so they were made mutable, but their accessors
		//		will only assign to them once, and thereafter throw an exception.
		// Remember that behaviours are normally provided from a service
		//		container as singletons, so we have to ensure that property injection
		//		is part of instantiation.

		private ImmutableList<string> _includedAllControlStates;
		private ImmutableList<string> _nonIncludedControlStates;
		private ImmutableList<string> _includedAllParameters;
		private ImmutableList<string> _nonIncludedParameters;
		private ImmutableDictionary<string, string> _matchingAllParams;
		private ImmutableDictionary<string, string> _nonMatchingAllParams;

		public IEnumerable<string> IncludedAllControlStates {
			get { return _includedAllControlStates ?? (_includedAllControlStates = ImmutableList.Create<string>()); }
			set {
				if (_includedAllControlStates != null) throw new InvalidOperationException("You may not assign IncludedAllControlStates once it has been set.");
				if (value == null) throw new ArgumentNullException("value");

				_includedAllControlStates = ImmutableList.Create<string>(value.ToArray());
			}
		}

		public IEnumerable<string> NonIncludedControlStates {
			get { return _nonIncludedControlStates ?? (_nonIncludedControlStates = ImmutableList.Create<string>()); }
			set {
				if (_nonIncludedControlStates != null) throw new InvalidOperationException("You may not assign NonIncludedControlStates once it has been set.");
				if (value == null) throw new ArgumentNullException("value");

				_nonIncludedControlStates = ImmutableList.Create<string>(value.ToArray());
			}
		}

		public IEnumerable<string> IncludedAllParameters {
			get { return _includedAllParameters ?? (_includedAllParameters = ImmutableList.Create<string>()); }
			set {
				if (_includedAllParameters != null) throw new InvalidOperationException("You may not assign IncludedAllParameters once it has been set.");
				if (value == null) throw new ArgumentNullException("value");

				_includedAllParameters = ImmutableList.Create<string>(value.ToArray());
			}
		}

		public IEnumerable<string> NonIncludedParameters {
			get { return _nonIncludedParameters ?? (_nonIncludedParameters = ImmutableList.Create<string>()); }
			set {
				if (_nonIncludedParameters != null) throw new InvalidOperationException("You may not assign NonIncludedParameters once it has been set.");
				if (value == null) throw new ArgumentNullException("value");

				_nonIncludedParameters = ImmutableList.Create<string>(value.ToArray());
			}
		}

		protected IEnumerable<KeyValuePair<string, string>> MatchingAllParameters {
			get { return _matchingAllParams ?? (_matchingAllParams = ImmutableDictionary.Create<string, string>()); }
			set {
				if (_matchingAllParams != null) throw new InvalidOperationException("You may not assign MatchingAllParameters once it has been set.");
				if (value == null) throw new ArgumentNullException("value");

				_matchingAllParams = ImmutableDictionary.CreateRange<string, string>(value);
			}
		}

		protected IEnumerable<KeyValuePair<string, string>> NonMatchingAllParameters {
			get { return _nonMatchingAllParams ?? (_nonMatchingAllParams = ImmutableDictionary.Create<string, string>()); }
			set {
				if (_nonMatchingAllParams != null) throw new InvalidOperationException("You may not assign NonMatchingAllParameters once it has been set.");
				if (value == null) throw new ArgumentNullException("value");

				_nonMatchingAllParams = ImmutableDictionary.CreateRange<string, string>(value);
			}
		}

		protected WebActionBehaviour(string message) : base(message) { }

		public override bool Condition(IEvent ev, WebContext context) {
			// TODO: if the event is concrete we can use its hashcode to memoize
			// the results of this method, which may be important as this
			// method could get called a lot

			bool parms = this.IncludedAllParameters.All(p => context.Params.Keys.Contains(p));
			bool parmsAndValues = this.MatchingAllParameters.All(p => context.Params.Contains(p) && context.Params[p.Key] == p.Value);
			bool notParmsAndValues = this.NonMatchingAllParameters.All(p => context.Params.Keys.Contains(p.Key) && context.Params[p.Key] != p.Value);
			bool controlStates = this.IncludedAllControlStates.All(p => context.ControlState.Keys.Contains(p));
			bool notParms = this.NonIncludedParameters.All(p => !context.Params.Keys.Contains(p));
			bool notControlStates = this.NonIncludedControlStates.All(p => !context.ControlState.Keys.Contains(p));

			return base.Condition(ev, context) && parms && parmsAndValues && notParmsAndValues && controlStates && notParms && notControlStates;
		}
	}
}

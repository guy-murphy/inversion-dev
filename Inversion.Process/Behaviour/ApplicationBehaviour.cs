using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Inversion.Process.Behaviour {

	/// <summary>
	/// An abstract provision of an application behaviour that includes features
	/// for configuring parameter conditions that must be met for the
	/// behaviours action to execute.
	/// </summary>
	public abstract class ApplicationBehaviour: ProcessBehaviour, IApplicationBehaviour {

		private ImmutableDictionary<string, IEnumerable<string>> _namedLists;
		private ImmutableDictionary<string, IDictionary<string,string>> _namedMaps;

		/// <summary>
		/// Provides access to the behaviours named maps, used to configure
		/// the behaviour.
		/// </summary>
		public IDictionary<string, IDictionary<string,string>> NamedMaps {
			get {
				return _namedMaps ?? (_namedMaps = ImmutableDictionary<string, IDictionary<string, string>>.Empty);
			}
			set {
				if (_namedMaps != null) throw new InvalidOperationException("You may not assign NamedMaps once it has been set.");
				if (value == null) throw new ArgumentNullException("value");

				_namedMaps = value.ToImmutableDictionary();
			}
		}

		/// <summary>
		/// Provides access to the behaviours named lists,
		/// used to configure the behaviour.
		/// </summary>
		public IDictionary<string, IEnumerable<string>> NamedLists {
			get {
				return _namedLists ?? (_namedLists = ImmutableDictionary<string, IEnumerable<string>>.Empty);
			}
			set {
				if (_namedLists != null) throw new InvalidOperationException("You may not assign NamedLists once it has been set.");
				if (value == null) throw new ArgumentNullException("value");

				_namedLists = value.ToImmutableDictionary();
			}
		}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="name">The name of the behaviour.</param>
		protected ApplicationBehaviour(string name) : base(name) {}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="name">The name of the behaviour.</param>
		/// <param name="preprocess">Indicates whether the system should notify before this behaviours action is processed.</param>
		/// <param name="postprocess">Indicates whether the system should notify after this behaviours action has been processed.</param>
		protected ApplicationBehaviour(string name, bool preprocess = false, bool postprocess = false) : base(name, preprocess, postprocess) {}

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
			return base.Condition(ev, context) &&
			       this.HasAllParms(context) &&
			       this.MacthesAllParamValues(context) &&
			       this.HasAllControlStates(context) &&
			       this.HasAllFlags(context);
		}
		
	}
}

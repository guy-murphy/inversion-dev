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
		private ImmutableDictionary<string, IDictionary<string, IEnumerable<string>>> _namedMappedLists;

		/// <summary>
		/// Provides access to the behaviours named maps, used to configure
		/// the behaviour.
		/// </summary>
		public IDictionary<string, IDictionary<string,string>> NamedMaps {
			get {
				return _namedMaps ?? (_namedMaps = ImmutableDictionary<string, IDictionary<string, string>>.Empty);
			}
			protected set {
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
			protected set {
				if (_namedLists != null) throw new InvalidOperationException("You may not assign NamedLists once it has been set.");
				if (value == null) throw new ArgumentNullException("value");

				_namedLists = value.ToImmutableDictionary();
			}
		}

		/// <summary>
		/// Provides access to the behaviours named, mapped lists,
		/// used to configure the behaviour.
		/// </summary>
		public IDictionary<string, IDictionary<string, IEnumerable<string>>> NamedMappedLists {
			get {
				return _namedMappedLists ?? (_namedMappedLists = ImmutableDictionary<string, IDictionary<string, IEnumerable<string>>>.Empty);
			}
			protected set {
				if (_namedMappedLists != null) throw new InvalidOperationException("You may not assign NamedMappedLists once it has been set.");
				if (value == null) throw new ArgumentNullException("value");

				_namedMappedLists = value.ToImmutableDictionary();
			}
		}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="message">The name of the behaviour.</param>
		/// <param name="namedLists">Named lists used to configure this behaviour.</param>
		/// <param name="namedMaps">Named maps used to configure this behaviour.</param>
		/// <param name="namedMappedLists">Named maps of lists used to configure this behaviour.</param>
		protected ApplicationBehaviour(string message, 
			IDictionary<string, IEnumerable<string>> namedLists,
			IDictionary<string, IDictionary<string, string>> namedMaps,
			IDictionary<string, IDictionary<string, IEnumerable<string>>>  namedMappedLists
		)
			: base(message) {
			this.NamedLists = namedLists ?? ImmutableDictionary<string, IEnumerable<string>>.Empty;
			this.NamedMaps = namedMaps ?? ImmutableDictionary<string, IDictionary<string, string>>.Empty;
			this.NamedMappedLists = namedMappedLists ?? ImmutableDictionary<string, IDictionary<string, IEnumerable<string>>>.Empty;
		}

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
			       this.HasAllControlStates(context) &&
			       this.HasAllFlags(context) &&
			       this.MacthesAllParamValues(context);
		}
		
	}
}

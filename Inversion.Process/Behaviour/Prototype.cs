using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Inversion.Collections;

namespace Inversion.Process.Behaviour {
	/// <summary>
	/// A configuration that is able to provide
	/// selection criteria suitable for a behaviours condition
	/// based upon what the behaviour expresses in its
	/// configuration.
	/// </summary>
	public class Prototype: Configuration, IPrototype {

		/// <summary>
		/// The default cases to be used for all prototypes of this class.
		/// </summary>
		public static readonly ConcurrentDataDictionary<IPrototypeCase> NamedCases = new ConcurrentDataDictionary<IPrototypeCase>();

		private readonly ImmutableHashSet<SelectionCriteria> _criteria;

		/// <summary>
		/// Instantiates named cases.
		/// </summary>
		static Prototype() {
			NamedCases["ev-has-all"] = new Case(
				match: (config)	=> config.Has("event", "has"),
				criteria: (config, ev) => ev.HasParams(config.GetNames("event", "has"))
			);
			NamedCases["ev-match-all"] = new Case(
				match: (config) => config.Has("event", "match"),
				criteria: (config, ev) => ev.HasParamValues(config.GetMap("event", "match"))
			);
			NamedCases["ctx-has-all"] = new Case(
				match: (config) => config.Has("context", "has"),
				criteria: (config, ev) => ev.Context.HasParams(config.GetNames("context", "has"))
			);
			NamedCases["ctx-match-all"] = new Case(
				match: (config) => config.Has("context", "match"),
				criteria: (config, ev) => ev.Context.HasParamValues(config.GetMap("context", "match"))
			);
			NamedCases["ctx-match-any"] = new Case(
				match: (config) => config.Has("context", "match-any"),
				criteria: (config, ev) => config.GetElements("context", "match-any").Any(element => ev.Context.HasParamValue(element.Name, element.Value))
			);
			NamedCases["ctx-flagged-all"] = new Case(
				match: (config) => config.Has("context", "flagged"),
				criteria: (config, ev) => config.GetMap("context", "flagged").All(kv => kv.Value == "true" && ev.Context.IsFlagged(kv.Key) || kv.Value != "true" && !ev.Context.IsFlagged(kv.Key))
			);
			NamedCases["ctx-excludes-all"] = new Case(
				match: (config) => config.Has("context", "excludes"),
				criteria: (config, ev) => config.GetMap("context", "excludes").All(kv => !ev.Context.Params.Contains(kv))
			);
			NamedCases["control-has-all"] = new Case(
				match: (config) => config.Has("control-state", "has"),
				criteria: (config, ev) => ev.Context.HasControlState(config.GetNames("control-state", "has"))
			);
			NamedCases["control-excludes-all"] = new Case(
				match: (config) => config.Has("control-state", "excludes"),
				criteria: (config, ev) => config.GetNames("control-state", "excludes").All(key => !ev.Context.ControlState.ContainsKey(key))
			);
		}

		/// <summary>
		/// Instantiates a prototype object.
		/// </summary>
		public Prototype() : this(new IConfigurationElement[] {}) {}
		/// <summary>
		/// Instantiates a prototype object from the configuration elements
		/// provided. Copies down the selection criteria that have a matching case.
		/// </summary>
		/// <param name="config">
		/// The configuration elements to use for configuration.
		/// </param>
		public Prototype(IEnumerable<IConfigurationElement> config) : base(config) {
			var builder = ImmutableHashSet.CreateBuilder<SelectionCriteria>();
			foreach (IPrototypeCase @case in this.Cases) {
				if (@case.Match(this)) builder.Add(@case.Criteria);
			}
			_criteria = builder.ToImmutable();
		}

		/// <summary>
		/// The cases to use in picking selection criteria for a behaviour.
		/// </summary>
		public IEnumerable<IPrototypeCase> Cases {
			get { return NamedCases.Values; }
		}
		/// <summary>
		/// The selection criteria that may be applicable to behaviours.
		/// </summary>
		public IEnumerable<SelectionCriteria> Criteria {
			get { return _criteria; }
		}

		/// <summary>
		/// Specifies a selection criteria which may be used by a behaviours
		/// condition and a match against a configuration to determine if
		/// that selection criteria is applicable to that configuration.
		/// </summary>
		public class Case: IPrototypeCase {
			private readonly Predicate<IConfiguration> _match;
			private readonly SelectionCriteria _criteria;
			/// <summary>
			/// Instantiates a case with the match and criteria provided.
			/// </summary>
			/// <param name="match">The match that determines if this case applies to a configuration.</param>
			/// <param name="criteria">The selection criteria for use by behaviours.</param>
			public Case(Predicate<IConfiguration> match, SelectionCriteria criteria) {
				_match = match;
				_criteria = criteria;
			}
			/// <summary>
			/// The match that determines if this case applies to a configuration.
			/// </summary>
			public Predicate<IConfiguration> Match {
				get { return _match; }
			}
			/// <summary>
			/// The selection criteria for use by behaviours.
			/// </summary>
			public SelectionCriteria Criteria {
				get { return _criteria; }
			}
		}


	}
}

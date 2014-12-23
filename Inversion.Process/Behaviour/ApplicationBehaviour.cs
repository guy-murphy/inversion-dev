using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inversion.Collections;

namespace Inversion.Process.Behaviour {

	/// <summary>
	/// An abstract provision of an application behaviour that includes features
	/// for configuring parameter conditions that must be met for the
	/// behaviours action to execute.
	/// </summary>
	public class ApplicationBehaviour: ProcessBehaviour {

		private ImmutableDictionary<string, IEnumerable<string>> _namedLists;
		private ImmutableDictionary<string, IEnumerable<KeyValuePair<string,string>>> _namedMaps;

		public IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string,string>>>> NamedMaps {
			get { return _namedMaps ?? (_namedMaps = ImmutableDictionary<string, IEnumerable<KeyValuePair<string, string>>>.Empty); }
			set {
				if (_namedMaps != null) throw new InvalidOperationException("You may not assign NamedMaps once it has been set.");
				if (value == null) throw new ArgumentNullException("value");

				_namedMaps = value.ToImmutableDictionary();
			}
		}

		public IEnumerable<KeyValuePair<string, IEnumerable<string>>> NamedLists {
			get { return _namedLists ?? (_namedLists = ImmutableDictionary<string, IEnumerable<string>>.Empty); }
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
		/// The action to perform when the `Condition(IEvent)` is met.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		public override void Action(IEvent ev, ProcessContext context) {
			context.Messages.Add("application behaviour");
			DataDictionary<DataCollection<string>> namedLists = new DataDictionary<DataCollection<string>>();
			foreach (KeyValuePair<string, IEnumerable<string>> entry in this.NamedLists) {
				namedLists[entry.Key] = new DataCollection<string>(entry.Value);
			}
			context.ControlState["named-lists"] = namedLists;

			DataDictionary<DataDictionary<string>> namedMaps = new DataDictionary<DataDictionary<string>>();
			foreach (KeyValuePair<string, IEnumerable<KeyValuePair<string,string>>> map in this.NamedMaps) {
				namedMaps[map.Key] = new DataDictionary<string>(map.Value);
			}

			context.ControlState["named-maps"] = namedMaps;
		}
		
	}
}

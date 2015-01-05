using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Inversion.Process.Behaviour {
	public class BehaviourConfiguration {

		private readonly ImmutableHashSet<Element> _elements;

		public IEnumerable<Element> Elements {
			get { return _elements; }
		}

		public BehaviourConfiguration() {
			_elements = ImmutableHashSet<Element>.Empty;
		}

		public BehaviourConfiguration(IEnumerable<Element> elements) {
			_elements = elements.ToImmutableHashSet();
		}

		public IEnumerable<Element> GetElements(string frame) {
			return this.Elements.Where(element => element.Frame == frame).OrderBy(e => e.Ordinal);
		}

		public IEnumerable<Element> GetElements(string frame, string slot) {
			return this.Elements.Where(element => element.Frame == frame && element.Slot == slot).OrderBy(e => e.Ordinal);
		}

		public IEnumerable<Element> GetElements(string frame, string slot, string name) {
			return this.Elements.Where(element => element.Frame == frame && element.Slot == slot && element.Name == name).OrderBy(e => e.Ordinal);
		}

		public string GetValue(string frame, string slot, string name) {
			return this.GetValues(frame, slot, name).FirstOrDefault();
		}

		public IEnumerable<string> GetValues(string frame, string slot, string name) {
			return this.GetElements(frame, slot, name).Select(element => element.Value);
		}

		public IDictionary<string, string> GetMap(string frame, string slot) {
			Dictionary<string,string> map = new Dictionary<string, string>();
			foreach (Element element in this.GetElements(frame, slot)) {
				if (element.Name != String.Empty) {
					map[element.Name] = element.Value;
				}
			}
			return map;
		}
		
		public IEnumerable<string> GetNames(string frame, string slot) {
			return this.GetElements(frame, slot).Select(element => element.Name).Distinct();
		}

		public IEnumerable<string> GetSlots(string frame) {
			return this.GetElements(frame).Select(element => element.Slot).Distinct();
		}

		public bool Has(string frame, string slot, string name, string value) {
			return this.Elements.Any(e => e.Frame == frame && e.Slot == slot && e.Name == name && e.Value == value);
		}

		public bool Has(string frame, string slot, string name) {
			return this.Elements.Any(e => e.Frame == frame && e.Slot == slot && e.Name == name);
		}
		
		public bool HasAll(string frame, string slot, string name, params string[] values) {
			return this.GetValues(frame, slot, name).All(value => values.Contains(value));
		}

		public bool HasAny(string frame, string slot, string name, params string[] values) {
			return this.GetValues(frame, slot, name).Any(value => values.Contains(value));
		}

		

		public class Element : Tuple<int, string, string, string, string> {

			public int Ordinal { get { return this.Item1; } }
			public string Frame { get { return this.Item2; } }
			public string Slot { get { return this.Item3; } }
			public string Name { get { return this.Item4; } }
			public string Value { get { return this.Item5; } }

			/// <summary>
			/// Initializes a new instance of the element class.
			/// </summary>
			/// <param name="ordinal">The order in which this element comes relative to its siblings.</param>
			/// <param name="frame">The value of the tuple's first component.</param>
			/// <param name="slot">The value of the tuple's second component.</param>
			/// <param name="name">The value of the tuple's third component.</param>
			/// <param name="value">The value of the tuple's third component.</param>
			public Element(int ordinal, string frame, string slot, string name, string value) : base(ordinal, frame, slot, name, value) { }
		}

	}
}

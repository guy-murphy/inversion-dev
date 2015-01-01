using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inversion.Collections;

namespace Inversion.Process.Behaviour {
	public class BehaviourConfiguration {

		private readonly ImmutableHashSet<Element> _elements;

		public IEnumerable<Element> Elements {
			get { return _elements; }
		}

		public BehaviourConfiguration(IEnumerable<Element> elements) {
			_elements = elements.ToImmutableHashSet();
		}

		public IEnumerable<Element> GetElements(string scope) {
			return this.Elements.Where(element => element.Scope == scope);
		}

		public IEnumerable<Element> GetElements(string scope, string name) {
			return this.Elements.Where(element => element.Scope == scope && element.Name == name);
		}

		public IEnumerable<string> GetValues(string scope, string name) {
			return this.GetElements(scope, name).Select(element => element.Value);
		}

		public string GetValue(string scope, string name) {
			return this.GetValues(scope, name).FirstOrDefault();
		}

		public bool Has(string scope, string name) {
			return this.GetElements(scope, name).Any();
		}

		public bool Has(string scope, string name, string value) {
			return this.Elements.Any(it => it.Scope == scope && it.Name == name && it.Value == value);
		}

		public bool HasAll(string scope, string name, params string[] values) {
			return this.GetValues(scope, name).All(value => values.Contains(value));
		}

		public bool HasAny(string scope, string name, params string[] values) {
			return this.GetValues(scope, name).Any(value => values.Contains(value));
		}

		public IDataDictionary<string> GetMap(string scope) {
			DataDictionary<string> map = new DataDictionary<string>();
			foreach (Element element in this.GetElements(scope)) {
				map.Add(element.Name, element.Value);
			}
			return map;
		}

		public class Element : Tuple<string, string, string> {

			public string Scope { get { return this.Item1; } }
			public string Name { get { return this.Item2; } }
			public string Value { get { return this.Item3; } }

			/// <summary>
			/// Initializes a new instance of the element class.
			/// </summary>
			/// <param name="scope">The value of the tuple's first component.</param>
			/// <param name="name">The value of the tuple's second component.</param>
			/// <param name="value">The value of the tuple's third component.</param>
			public Element(string scope, string name, string value) : base(scope, name, value) { }
		}

	}
}

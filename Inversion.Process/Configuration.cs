using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Inversion.Process {

	/// <summary>
	/// Provides an immutable, ordered collection of
	/// configuration elements.
	/// </summary>
	public class Configuration {

		private readonly ImmutableHashSet<Element> _elements;

		/// <summary>
		/// The elements comprising the configuration.
		/// </summary>
		public IEnumerable<Element> Elements {
			get { return _elements; }
		}

		/// <summary>
		/// Instantiates a new, empty configuration.
		/// </summary>
		public Configuration() {
			_elements = ImmutableHashSet<Element>.Empty;
		}

		/// <summary>
		/// Instantiates a new configuration from the elements provided.
		/// </summary>
		/// <param name="elements">The elements to populate the configuration with.</param>
		public Configuration(IEnumerable<Element> elements) {
			_elements = elements.ToImmutableHashSet();
		}

		/// <summary>
		/// Gets the elements for a specified frame.
		/// </summary>
		/// <param name="frame">The frame to get the elements for.</param>
		/// <returns>Returns an enumerable of the matching elements.</returns>
		public IEnumerable<Element> GetElements(string frame) {		
			return this.Elements.Where(element => element.Frame == frame).OrderBy(e => e.Ordinal);
		}

		/// <summary>
		/// Gets the elements for the specified frame and slot.
		/// </summary>
		/// <param name="frame">The frame to get the elements for.</param>
		/// <param name="slot">The slot within a frame to get the elements for.</param>
		/// <returns>Returns an enumerable of the matching elements.</returns>
		public IEnumerable<Element> GetElements(string frame, string slot) {
			return this.Elements.Where(element => element.Frame == frame && element.Slot == slot).OrderBy(e => e.Ordinal);
		}

		/// <summary>
		/// Gets the elements for the specified frame and slot.
		/// </summary>
		/// <param name="frame">The frame to get the elements for.</param>
		/// <param name="slot">The slot within a frame to get the elements for.</param>
		/// <param name="name">The name within the slot to get the elements for.</param>
		/// <returns>Returns an enumerable of the matching elements.</returns>
		public IEnumerable<Element> GetElements(string frame, string slot, string name) {
			return this.Elements.Where(element => element.Frame == frame && element.Slot == slot && element.Name == name).OrderBy(e => e.Ordinal);
		}

		/// <summary>
		/// Gets the specified value from the configuration.
		/// </summary>
		/// <param name="frame">The frame of the value.</param>
		/// <param name="slot">The slot of the value.</param>
		/// <param name="name">The name of the value.</param>
		/// <returns>Returns the value macthing the frame, slot, and name specified.</returns>
		public string GetValue(string frame, string slot, string name) {
			return this.GetValues(frame, slot, name).FirstOrDefault();
		}
		
		/// <summary>
		/// Gets the specified values from the configuration.
		/// </summary>
		/// <param name="frame">The frame of the values.</param>
		/// <param name="slot">The slot of the values.</param>
		/// <param name="name">The name of the values.</param>
		/// <returns>Returns the values matching the frame, slot, and name specified.</returns>
		public IEnumerable<string> GetValues(string frame, string slot, string name) {
			return this.GetElements(frame, slot, name).Select(element => element.Value);
		}

		/// <summary>
		/// Get a map of name/value pairs from the configuration.
		/// </summary>
		/// <param name="frame">The frame of the map.</param>
		/// <param name="slot">The slot of the map.</param>
		/// <returns>Returns a map matching the frame and slot specified.</returns>
		public IDictionary<string, string> GetMap(string frame, string slot) {
			Dictionary<string, string> map = new Dictionary<string, string>();
			foreach (Element element in this.GetElements(frame, slot)) {
				if (element.Name != String.Empty) {
					map[element.Name] = element.Value;
				}
			}
			return map;
		}

		/// <summary>
		/// Gets names from the configuration.
		/// </summary>
		/// <param name="frame">The frame of the names.</param>
		/// <param name="slot">The slot of the names.</param>
		/// <returns>Returns an enumerable of the names matching the frame and slot specified.</returns>
		public IEnumerable<string> GetNames(string frame, string slot) {
			return this.GetElements(frame, slot).Select(element => element.Name).Distinct();
		}

		/// <summary>
		/// Gets the specified name from the configuration.
		/// </summary>
		/// <param name="frame">The frame of the names.</param>
		/// <param name="slot">The slot of the names.</param>
		/// <returns>Gets the first name under the frame and slot specified.</returns>
		public string GetName(string frame, string slot) {
			return this.GetNames(frame, slot).FirstOrDefault();
		}
		/// <summary>
		/// Gets slots from the configuration.
		/// </summary>
		/// <param name="frame">The frame of the slots.</param>
		/// <returns>Returns an enumerable of the slots matching the frame specified.</returns>
		public IEnumerable<string> GetSlots(string frame) {
			return this.GetElements(frame).Select(element => element.Slot).Distinct();
		}

		/// <summary>
		/// Determines whether or not the configuration has an element with the
		/// frame, slot, name and value specified.
		/// </summary>
		/// <param name="frame">The frame of the element.</param>
		/// <param name="slot">The slot of the element.</param>
		/// <param name="name">The name of the element.</param>
		/// <param name="value">The value of the element.</param>
		/// <returns>
		/// Returns true if the configuration containes the specified element;
		/// otherwise, returns false.
		/// </returns>
		public bool Has(string frame, string slot, string name, string value) {
			return this.Elements.Any(e => e.Frame == frame && e.Slot == slot && e.Name == name && e.Value == value);
		}

		/// <summary>
		/// Determines whether or not the configuration has any elements with the
		/// frame, slot, and name specified.
		/// </summary>
		/// <param name="frame">The frame of the element.</param>
		/// <param name="slot">The slot of the element.</param>
		/// <param name="name">The name of the element.</param>
		/// <returns>
		/// Returns true if the configuration containes the specified element;
		/// otherwise, returns false.
		/// </returns>
		public bool Has(string frame, string slot, string name) {
			return this.Elements.Any(e => e.Frame == frame && e.Slot == slot && e.Name == name);
		}

		/// <summary>
		/// Determines whether or not the configuration has any elements with the
		/// frame and slot specified.
		/// </summary>
		/// <param name="frame">The frame of the element.</param>
		/// <param name="slot">The slot of the element.</param>
		/// <returns>
		/// Returns true if the configuration containes the specified element;
		/// otherwise, returns false.
		/// </returns>
		public bool Has(string frame, string slot) {
			return this.Elements.Any(e => e.Frame == frame && e.Slot == slot);
		}

		/// <summary>
		/// Determines whether or not the configuration has all elements with the
		/// frame, slot, name and values specified.
		/// </summary>
		/// <param name="frame">The frame of the element.</param>
		/// <param name="slot">The slot of the element.</param>
		/// <param name="name">The name of the element.</param>
		/// <param name="values">The values of the elements.</param>
		/// <returns>
		/// Returns true if the configuration containes all the specified elements;
		/// otherwise, returns false.
		/// </returns>
		public bool HasAll(string frame, string slot, string name, params string[] values) {
			return this.GetValues(frame, slot, name).All(value => values.Contains(value));
		}

		/// <summary>
		/// Determines whether or not the configuration has any of then elements with the
		/// frame, slot, name and values specified.
		/// </summary>
		/// <param name="frame">The frame of the element.</param>
		/// <param name="slot">The slot of the element.</param>
		/// <param name="name">The name of the element.</param>
		/// <param name="values">The values of the elements.</param>
		/// <returns>
		/// Returns true if the configuration containes any of the specified elements;
		/// otherwise, returns false.
		/// </returns>
		public bool HasAny(string frame, string slot, string name, params string[] values) {
			return this.GetValues(frame, slot, name).Any(value => values.Contains(value));
		}

		/// <summary>
		/// Models an element of a behaviour configuration.
		/// </summary>
		public class Element : Tuple<int, string, string, string, string> {

			/// <summary>
			/// The order or position which this element occupies
			/// relative to its siblings.
			/// </summary>
			public int Ordinal { get { return this.Item1; } }
			/// <summary>
			/// The frame of this element.
			/// </summary>
			public string Frame { get { return this.Item2; } }
			/// <summary>
			/// The slot of this element.
			/// </summary>
			public string Slot { get { return this.Item3; } }
			/// <summary>
			/// The name of this element.
			/// </summary>
			public string Name { get { return this.Item4; } }
			/// <summary>
			/// The value of this element.
			/// </summary>
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

		/// <summary>
		/// Provides a specialised, mutable hashset of elements
		/// that can be used to conveniently build Configuration.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The primary utility of the builder is to faciliate from code
		/// rather than say from Spring config the likes of...
		/// </para>
		/// <code>
		///     new ParameterisedSequenceBehaviour("process-request", 
		///		new Configuration.Builder {
		///			{"fire", "bootstrap"},
		///			{"fire", "parse-request"},
		///			{"fire", "work"},
		///			{"fire", "view-state"},
		///			{"fire", "process-views"},
		///			{"fire", "render"}
		///		}
		///	)
		/// </code>
		/// <para>
		/// This approach become important in unit-tests where use of `Inversion.Naiad`
		/// may be attractive over Spring.
		/// </para>
		/// </remarks>
		public class Builder: HashSet<Element> {
			
			/// <summary>
			/// Produces a configuration from the builder.
			/// </summary>
			/// <returns>Returns a configuration initialised from this builder.</returns>
			public Configuration ToConcrete() {
				return new Configuration(this);
			}

			/// <summary>
			/// Adds an element to the builder.
			/// </summary>
			/// <param name="frame">The frame of the element to add.</param>
			/// <param name="slot">The slot of the element to add.</param>
			public void Add(string frame, string slot) {
				this.Add(frame, slot, String.Empty);
			}
			/// <summary>
			/// Adds an element to the builder.
			/// </summary>
			/// <param name="frame">The frame of the element to add.</param>
			/// <param name="slot">The slot of the element to add.</param>
			/// <param name="name">The name of the element to add.</param>
			public void Add(string frame, string slot, string name) {
				this.Add(frame, slot, name, String.Empty);
			}
			/// <summary>
			/// Adds an element to the builder.
			/// </summary>
			/// <param name="frame">The frame of the element to add.</param>
			/// <param name="slot">The slot of the element to add.</param>
			/// <param name="name">The name of the element to add.</param>
			/// <param name="value">The value of the element to add.</param>
			public void Add(string frame, string slot, string name, string value) {
				this.Add(new Element(this.Count(), frame, slot, name, value));
			}
		}

	}
}

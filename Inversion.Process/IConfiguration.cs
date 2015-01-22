using System.Collections.Generic;

namespace Inversion.Process {

	/// <summary>
	/// Describes ordered collection of
	/// IConfigurationElements.
	/// </summary>
	public interface IConfiguration {
		/// <summary>
		/// The elements comprising the configuration.
		/// </summary>
		IEnumerable<IConfigurationElement> Elements { get; }

		/// <summary>
		/// Gets the elements for a specified frame.
		/// </summary>
		/// <param name="frame">The frame to get the elements for.</param>
		/// <returns>Returns an enumerable of the matching elements.</returns>
		IEnumerable<IConfigurationElement> GetElements(string frame);

		/// <summary>
		/// Gets the elements for the specified frame and slot.
		/// </summary>
		/// <param name="frame">The frame to get the elements for.</param>
		/// <param name="slot">The slot within a frame to get the elements for.</param>
		/// <returns>Returns an enumerable of the matching elements.</returns>
		IEnumerable<IConfigurationElement> GetElements(string frame, string slot);

		/// <summary>
		/// Gets the elements for the specified frame and slot.
		/// </summary>
		/// <param name="frame">The frame to get the elements for.</param>
		/// <param name="slot">The slot within a frame to get the elements for.</param>
		/// <param name="name">The name within the slot to get the elements for.</param>
		/// <returns>Returns an enumerable of the matching elements.</returns>
		IEnumerable<IConfigurationElement> GetElements(string frame, string slot, string name);

		/// <summary>
		/// Gets the specified value from the configuration.
		/// </summary>
		/// <param name="frame">The frame of the value.</param>
		/// <param name="slot">The slot of the value.</param>
		/// <param name="name">The name of the value.</param>
		/// <returns>Returns the value macthing the frame, slot, and name specified.</returns>
		string GetValue(string frame, string slot, string name);

		/// <summary>
		/// Gets the specified values from the configuration.
		/// </summary>
		/// <param name="frame">The frame of the values.</param>
		/// <param name="slot">The slot of the values.</param>
		/// <param name="name">The name of the values.</param>
		/// <returns>Returns the values matching the frame, slot, and name specified.</returns>
		IEnumerable<string> GetValues(string frame, string slot, string name);

		/// <summary>
		/// Get a map of name/value pairs from the configuration.
		/// </summary>
		/// <param name="frame">The frame of the map.</param>
		/// <param name="slot">The slot of the map.</param>
		/// <returns>Returns a map matching the frame and slot specified.</returns>
		IDictionary<string, string> GetMap(string frame, string slot);

		/// <summary>
		/// Gets names from the configuration.
		/// </summary>
		/// <param name="frame">The frame of the names.</param>
		/// <param name="slot">The slot of the names.</param>
		/// <returns>Returns an enumerable of the names matching the frame and slot specified.</returns>
		IEnumerable<string> GetNames(string frame, string slot);

		/// <summary>
		/// Gets the specified name from the configuration.
		/// </summary>
		/// <param name="frame">The frame of the names.</param>
		/// <param name="slot">The slot of the names.</param>
		/// <returns>Gets the first name under the frame and slot specified.</returns>
		string GetName(string frame, string slot);

		/// <summary>
		/// Gets slots from the configuration.
		/// </summary>
		/// <param name="frame">The frame of the slots.</param>
		/// <returns>Returns an enumerable of the slots matching the frame specified.</returns>
		IEnumerable<string> GetSlots(string frame);

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
		bool Has(string frame, string slot, string name, string value);

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
		bool Has(string frame, string slot, string name);

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
		bool Has(string frame, string slot);

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
		bool HasAll(string frame, string slot, string name, params string[] values);

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
		bool HasAny(string frame, string slot, string name, params string[] values);

	}
}
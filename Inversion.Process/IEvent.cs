using System.Collections.Generic;

namespace Inversion.Process {

	/// <summary>
	/// Represents an event occuring in the system.
	/// </summary>
	/// <remarks>
	/// Exactly what "event" means is application specific
	/// and can range from imperative to reactive.
	/// </remarks>
	public interface IEvent : IData {
		/// <summary>
		/// Provides indexed access to the events parameters.
		/// </summary>
		/// <param name="key">The key of the parameter to look up.</param>
		/// <returns>Returns the parameter indexed by the key.</returns>
		string this[string key] { get; }
		/// <summary>
		/// The simple message the event represents.
		/// </summary>
		/// <remarks>
		/// Again, exactly what this means is application specific.
		/// </remarks>
		string Message { get; }
		/// <summary>
		/// The parameters for this event represented
		/// as key-value pairs.
		/// </summary>
		IDictionary<string, string> Params { get; }
		/// <summary>
		/// Adds a key-value pair as a parameter to the event.
		/// </summary>
		/// <param name="key">The key of the parameter.</param>
		/// <param name="value">The value of the parameter.</param>
		void Add(string key, string value);
		/// <summary>
		/// The context upon which this event is being fired.
		/// </summary>
		/// <remarks>
		/// And event always belongs to a context.
		/// </remarks>
		ProcessContext Context { get; }

		/// <summary>
		/// Any object that the event may be carrying.
		/// </summary>
		/// <remarks>
		/// This is a dirty escape hatch, and
		/// can even be used as a "return" value
		/// for the event.
		/// </remarks>
		IData Object { get; set; }

		/// <summary>
		/// Determines whether or not the parameters 
		/// specified exist in the event.
		/// </summary>
		/// <param name="parms">The parameters to check for.</param>
		/// <returns>Returns true if all the parameters exist; otherwise return false.</returns>
		bool HasParams(params string[] parms);
		/// <summary>
		/// Determines whether or not the parameters 
		/// specified exist in the event.
		/// </summary>
		/// <param name="parms">The parameters to check for.</param>
		/// <returns>Returns true if all the parameters exist; otherwise return false.</returns>
		bool HasParams(IEnumerable<string> parms);
		/// <summary>
		/// Determines whether or not all the key-value pairs
		/// provided exist in the events parameters.
		/// </summary>
		/// <param name="match">The key-value pairs to check for.</param>
		/// <returns>
		/// Returns true if all the key-value pairs specified exists in the events
		/// parameters; otherwise returns false.
		/// </returns>
		bool HasParamValues(IEnumerable<KeyValuePair<string, string>> match);
		/// <summary>
		/// Determines whether or not each of the prameters specified
		/// exist on the event, and creates an error for each one that
		/// does not.
		/// </summary>
		/// <param name="parms">The paramter names to check for.</param>
		/// <returns>
		/// Returns true if each of the parameters exist on the event;
		/// otherwise returns false.
		/// </returns>
		bool HasRequiredParams(params string[] parms);
		/// <summary>
		/// Fires the event on the context to which it is bound.
		/// </summary>
		/// <returns>
		/// Returns the event that has just been fired.
		/// </returns>
		IEvent Fire();
	}
}

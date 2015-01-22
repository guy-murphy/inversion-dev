using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace Inversion.Process.Behaviour {

	/// <summary>
	/// Extensions provided for ``IApplicationBehaioour` providing
	/// basic checks performed in behaviour conditions.
	/// </summary>
	public static class BehaviourConditionPredicates {


		/// <summary>
		/// Determines whether or not the parameters 
		/// specified exist in the current event. This does not test
		/// the values of any parameters, merely that they exist.
		/// </summary>
		/// <param name="self">The behaviour to act upon.</param>
		/// <param name="ev">The event to consult.</param>
		/// <returns>Returns true if all the parameters exist; otherwise return false.</returns>
		public static bool EventHasAllParams(this IConfiguredBehaviour self, IEvent ev) {
			return ev.HasParams(self.Configuration.GetNames("event", "has"));
		}

		/// <summary>
		/// Determines whether or not all the key-value pairs
		/// provided exist in the events parameters.
		/// </summary>
		/// <param name="self">The behaviour to act upon.</param>
		/// <param name="ev">The event to consult.</param>
		/// <returns>
		/// Returns true if all the key-value pairs specified exists in the events
		/// parameters; otherwise returns false.
		/// </returns>
		public static bool EventMatchesAllParamValues(this IConfiguredBehaviour self, IEvent ev) {
			return ev.HasParamValues(self.Configuration.GetMap("event", "match"));
		}

		/// <summary>
		/// Determines whether or not the parameters 
		/// specified exist in the current context.
		/// </summary>
		/// <param name="self">The behaviour to act upon.</param>
		/// <param name="ctx">The context to consult.</param>
		/// <returns>Returns true if all the parameters exist; otherwise return false.</returns>
		public static bool ContextHasAllParams(this IConfiguredBehaviour self, IProcessContext ctx) {
			return ctx.HasParams(self.Configuration.GetNames("context", "has"));
		}

		/// <summary>
		/// Determines whether or not all the key-value pairs
		/// provided exist in the contexts parameters.
		/// </summary>
		/// <param name="self">The behaviour to act upon.</param>
		/// <param name="ctx">The context to consult.</param>
		/// <returns>
		/// Returns true if all the key-value pairs specified exists in the contexts
		/// parameters; otherwise returns false.
		/// </returns>
		public static bool ContextMacthesAllParamValues(this IConfiguredBehaviour self, IProcessContext ctx) {
			return ctx.HasParamValues(self.Configuration.GetMap("context", "match"));
		}

		/// <summary>
		/// Determines whether or not any of the key-value pairs
		/// provided exist in the contexts parameters.
		/// </summary>
		/// <param name="self">The behaviour to act upon.</param>
		/// <param name="ctx">The context to consult.</param>
		/// <returns>
		/// Returns true if any of the key-value pairs specified exists in the contexts
		/// parameters; otherwise returns false.
		/// </returns>
		public static bool ContextMatchesAnyParamValues(this IConfiguredBehaviour self, IProcessContext ctx) {
			IEnumerable<Configuration.Element> elements = self.Configuration.GetElements("context", "match-any");
			bool hit = elements.Count() == 0 || elements.Any(element => ctx.HasParamValue(element.Name, element.Value));
			return hit;
		}

		/// <summary>
		/// Determines whether or not all the key-value pairs
		/// provided are NOT in the contexts parameters.
		/// </summary>
		/// <param name="self">The behaviour to act upon.</param>
		/// <param name="ctx">The context to consult.</param>
		/// <returns>
		/// Returns true if all the key-value pairs specified are absent in the contexts
		/// parameters; otherwise returns false.
		/// </returns>
		public static bool ContextExcludes(this IConfiguredBehaviour self, IProcessContext ctx) {
			return self.Configuration.GetMap("context", "excludes").All(kv => !ctx.Params.Contains(kv));
		}

		/// <summary>
		/// Dtermines whether or not the control state has entries indexed
		/// under the keys provided.
		/// </summary>
		/// <param name="self">The behaviour to act upon.</param>
		/// <param name="ctx">The context to consult.</param>
		/// <returns>
		/// Returns true if all the specified keys exist in the control state;
		/// otherwise returns false.
		/// </returns>
		public static bool ContextHasAllControlStates(this IConfiguredBehaviour self, IProcessContext ctx) {
			return ctx.HasControlState(self.Configuration.GetNames("control-state", "has"));
		}

		/// <summary>
		/// Determines whether or not all the key-value pairs
		/// provided are NOT in the contexts control state.
		/// </summary>
		/// <param name="self">The behaviour to act upon.</param>
		/// <param name="ctx">The context to consult.</param>
		/// <returns>
		/// Returns true if all the key-value pairs specified are absent in the contexts
		/// parameters; otherwise returns false.
		/// </returns>
		public static bool ContextExcludesControlState(this IConfiguredBehaviour self, IProcessContext ctx) {
			return self.Configuration.GetNames("control-state", "excludes").All(key => !ctx.ControlState.ContainsKey(key));
		}

		/// <summary>
		/// Determines whether or not each of the specified
		/// is set on the context.
		/// </summary>
		/// <param name="self">The behaviour to act upon.</param>
		/// <param name="ctx">The context to consult.</param>
		/// <returns>Returns true is all flags are set on the context; otherwise, returns false.</returns>
		public static bool ContextHasAllFlags(this IConfiguredBehaviour self, IProcessContext ctx) {
			// because LINQ can be a pain in the arse to debug when
			// you're being too clever for your own good
			foreach (KeyValuePair<string, string> kv in self.Configuration.GetMap("context", "flagged")) {
				if (kv.Value == "true") {
					if (!ctx.IsFlagged(kv.Key)) {
						return false;
					}
				} else {
					if (ctx.IsFlagged(kv.Key)) {
						return false;
					}
				}
			}
			return true;
		}
	}
}

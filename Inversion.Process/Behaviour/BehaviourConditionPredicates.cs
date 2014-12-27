using System.Linq;

namespace Inversion.Process.Behaviour {

	/// <summary>
	/// Extensions provided basic checks performed in behaviour conditions.
	/// </summary>
	public static class BehaviourConditionPredicates {

		/// <summary>
		/// Determines whether or not the parameters 
		/// specified exist in the current context.
		/// </summary>
		/// <param name="self">The behaviour to act upon.</param>
		/// <param name="ctx">The context to consult.</param>
		/// <returns>Returns true if all the parameters exist; otherwise return false.</returns>
		public static bool HasAllParms(this IApplicationBehaviour self, ProcessContext ctx) {
			return !self.NamedLists.ContainsKey("has-all-params") || ctx.HasParams(self.NamedLists["has-all-params"]);
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
		public static bool MacthesAllParamValues(this IApplicationBehaviour self, ProcessContext ctx) {
			return !self.NamedMaps.ContainsKey("matching-all-param-values") || ctx.HasParamValues(self.NamedMaps["matching-all-param-values"]);
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
		public static bool HasAllControlStates(this IApplicationBehaviour self, ProcessContext ctx) {
			return !self.NamedLists.ContainsKey("has-all-control-states") || ctx.HasControlState(self.NamedLists["has-all-control-states"]);
		}

		/// <summary>
		/// Determines whether or not each of the specified
		/// is set on the context.
		/// </summary>
		/// <param name="self">The behaviour to act upon.</param>
		/// <param name="ctx">The context to consult.</param>
		/// <returns>Returns true is all flags are set on the context; otherwise, returns false.</returns>
		public static bool HasAllFlags(this IApplicationBehaviour self, ProcessContext ctx) {
			return !self.NamedLists.ContainsKey("has-all-flags") || self.NamedLists["has-all-flags"].All(flag => ctx.IsFlagged(flag));
		}
	}
}

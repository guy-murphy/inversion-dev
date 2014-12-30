using System.Linq;

namespace Inversion.Web.Behaviour {

	/// <summary>
	/// Extensions provided for ``WebBehaviour` providing
	/// basic checks performed in behaviour conditions.
	/// </summary>
	public static class BehaviourConditionPredicates {

		/// <summary>
		/// Determines whether or not the current context user
		/// is in any of the `required-user-roles` configured for this behaviour.
		/// </summary>
		/// <param name="self">The behaviour to act upon.</param>
		/// <param name="ctx">The context to consult.</param>
		/// <returns>
		/// Returns true if the current context user is in any of the 
		/// `required-user-roles` configured for this behaviour.
		/// </returns>
		public static bool HasAnyUserRoles(this WebBehaviour self, WebContext ctx) {
			return !self.NamedLists.ContainsKey("required-user-roles") || self.NamedLists["required-user-roles"].Any(role => ctx.User.IsInRole(role));
		}

	}
}

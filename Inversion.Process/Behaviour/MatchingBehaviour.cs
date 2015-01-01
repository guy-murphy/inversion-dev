using System.Collections.Generic;

namespace Inversion.Process.Behaviour {
	/// <summary>
	/// Extends `ApplicationBehaviour` with an implemented `Condition` intended
	/// to act upont he behaviours configuration to match with state expressed on the
	/// process context.
	/// </summary>
	public abstract class MatchingBehaviour: ApplicationBehaviour {
		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The name of the behaviour.</param>
		/// <param name="namedLists">Named lists used to configure this behaviour.</param>
		/// <param name="namedMaps">Named maps used to configure this behaviour.</param>
		/// <param name="namedMappedLists">Named maps of lists used to configure this behaviour.</param>
		protected MatchingBehaviour(string respondsTo, IDictionary<string, IEnumerable<string>> namedLists,
			IDictionary<string, IDictionary<string, string>> namedMaps,
			IDictionary<string, IDictionary<string, IEnumerable<string>>> namedMappedLists)
			: base(respondsTo, namedLists, namedMaps, namedMappedLists) { }

		/// <summary>
		/// Determines if the event specifies the behaviour by name.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context to consult.</param>
		/// <returns>
		/// Returns true if true if the configured parameters for the behaviour
		/// match the current context.
		///  </returns>
		/// <remarks>
		/// The intent is to override for bespoke conditions.
		/// </remarks>
		public override bool Condition(IEvent ev, ProcessContext context) {
			return base.Condition(ev, context) &&
				   this.HasAllParms(context) &&
				   this.HasAllControlStates(context) &&
				   this.HasAllFlags(context) &&
				   this.MacthesAllParamValues(context);
		}
	}
}

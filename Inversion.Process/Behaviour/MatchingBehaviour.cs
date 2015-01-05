using System.Collections.Generic;

namespace Inversion.Process.Behaviour {
	/// <summary>
	/// Extends `ConfiguredBehaviour` with an implemented `Condition` intended
	/// to act upont he behaviours configuration to match with state expressed on the
	/// process context.
	/// </summary>
	public abstract class MatchingBehaviour: ConfiguredBehaviour {
		/// <summary>
		/// Creates a new instance of the behaviour with no configuration.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		protected MatchingBehaviour(string respondsTo) : base(respondsTo) {}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		/// <param name="config">Configuration for the behaviour.</param>
		protected MatchingBehaviour(string respondsTo, BehaviourConfiguration config) : base(respondsTo, config) {}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		/// <param name="config">Configuration for the behaviour.</param>
		protected MatchingBehaviour(string respondsTo, IEnumerable<BehaviourConfiguration.Element> config) : base(respondsTo, config) {}

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
			return base.Condition(ev, context) && this.HasAllParams(context);
			//&&
			//   this.HasAllParms(context) &&
			//   this.HasAllControlStates(context) &&
			//   this.HasAllFlags(context) &&
			//   this.MacthesAllParamValues(context);
		}
	}
}

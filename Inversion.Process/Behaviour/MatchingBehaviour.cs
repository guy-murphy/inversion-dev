//using System.Collections.Generic;

//namespace Inversion.Process.Behaviour {
//	/// <summary>
//	/// Extends `ConfiguredBehaviour` with an implemented `Condition` intended
//	/// to act upont he behaviours configuration to match with state expressed on the
//	/// process context.
//	/// </summary>
//	public abstract class MatchingBehaviour: ConfiguredBehaviour {
//		/// <summary>
//		/// Creates a new instance of the behaviour with no configuration.
//		/// </summary>
//		/// <param name="respondsTo">The message the behaviour will respond to.</param>
//		protected MatchingBehaviour(string respondsTo) : base(respondsTo) {}

//		/// <summary>
//		/// Creates a new instance of the behaviour.
//		/// </summary>
//		/// <param name="respondsTo">The message the behaviour will respond to.</param>
//		/// <param name="config">Configuration for the behaviour.</param>
//		protected MatchingBehaviour(string respondsTo, IConfiguration config) : base(respondsTo, config) {}

//		/// <summary>
//		/// Creates a new instance of the behaviour.
//		/// </summary>
//		/// <param name="respondsTo">The message the behaviour will respond to.</param>
//		/// <param name="config">Configuration for the behaviour.</param>
//		protected MatchingBehaviour(string respondsTo, IEnumerable<IConfigurationElement> config) : base(respondsTo, config) {}

//		/// <summary>
//		/// Determines if the event specifies the behaviour by name.
//		/// </summary>
//		/// <param name="ev">The event to consult.</param>
//		/// <param name="context">The context to consult.</param>
//		/// <returns>
//		/// Returns true if true if the configured parameters for the behaviour
//		/// match the current context.
//		///  </returns>
//		/// <remarks>
//		/// The intent is to override for bespoke conditions.
//		/// </remarks>
//		public override bool Condition(IEvent ev, IProcessContext context) {
//			// this is laid out longhand to assist with any debugging
//			// without having to step into each predicate to find out
//			// which one has a problem
//			bool previous = base.Condition(ev, context);
//			bool hasAllParams = this.EventHasAllParams(ev);
//			bool eventMatchesAllParamValues = this.EventMatchesAllParamValues(ev);
//			bool contextHasAllParams = this.ContextHasAllParams(context);
//			bool contextHasAllControlStates = this.ContextHasAllControlStates(context);
//			bool contextHasAllFlags =this.ContextHasAllFlags(context);
//			bool contextMacthesAllParamValues = this.ContextMacthesAllParamValues(context);
//			bool contextMatchesAnyParamValues = this.ContextMatchesAnyParamValues(context);
//			return previous && 
//				hasAllParams && 
//				eventMatchesAllParamValues && 
//				contextHasAllParams && 
//				contextHasAllControlStates &&
//				contextHasAllFlags && 
//				contextMacthesAllParamValues && 
//				contextMatchesAnyParamValues;
//		}
//	}
//}

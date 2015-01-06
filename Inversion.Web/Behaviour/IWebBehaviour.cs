using Inversion.Process;
using Inversion.Process.Behaviour;

namespace Inversion.Web.Behaviour {
	/// <summary>
	/// An specification of basic web-centric features for process behaviours
	/// being used in a web application.
	/// </summary>
	public interface IWebBehaviour : IProcessBehaviour {

		/// <summary>
		/// The considtion that determines whether of not the behaviours action
		/// is valid to run.
		/// </summary>
		/// <param name="ev">The event to consider with the condition.</param>
		/// <param name="context">The context to use.</param>
		/// <returns>
		/// `true` if the condition is met; otherwise,  returns  `false`.
		/// </returns>
		bool Condition(IEvent ev, IWebContext context);

		/// <summary>
		/// The action to perform if this behaviours condition is met.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		void Action(IEvent ev, IWebContext context);
	}
}
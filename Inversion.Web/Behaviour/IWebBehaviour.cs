using Inversion.Process;

namespace Inversion.Web.Behaviour {
	/// <summary>
	/// An specification of basic web-centric features for process behaviours
	/// being used in a web application.
	/// </summary>
	public interface IWebBehaviour : IProcessBehaviour {
		/// <summary>
		/// The action to perform if this behaviours condition is met.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		void Action(IEvent ev, WebContext context);
	}
}
using Inversion.Process;

namespace Inversion.Web.Behaviour {
	public interface IWebBehaviour : IProcessBehaviour {
		void Action(IEvent ev, WebContext context);
	}
}
using System;

using Inversion.Process;

namespace Inversion.Web.Behaviour {

	public class ProcessViewsBehaviour : WebBehaviour {

		public ProcessViewsBehaviour(string name) : base(name) { }

		public override void Action(IEvent ev, WebContext context) {
			// this is the last point we can time until and still output it
			if (context.Timers.ContainsKey("process-request")) context.Timers.End("process-request");
			// check that we have an initial view step laid down at least
			if (context.ViewSteps.HasSteps) {
				// then determine how many views there are to process
				string[] views = (String.IsNullOrWhiteSpace(context.Request.UrlInfo.Tail)) ? new string[] { "xsl" } : context.Request.UrlInfo.Tail.Split('/');
				foreach (string view in views) {
					if (!String.IsNullOrEmpty(view)) {

						string msg = String.Format("{0}::view", view);
						context.Fire(msg);
					}
				}
			} else {
				throw new WebException("There are no view steps to render.");
			}
		}
	}
}

using System;

using Inversion.Process;
using Inversion.Process.Behaviour;

namespace Inversion.Web.Behaviour {

	/// <summary>
	/// Behaviour responsible for driving the view pipeline expressed
	/// as view-steps.
	/// </summary>
	public class ProcessViewsBehaviour : WebBehaviour {

		/// <summary>
		/// Instantiates a new behaviour responsible for processes the inversion view pipeline.
		/// </summary>
		/// <param name="respondsTo">The message that the behaviour will respond to.</param>
		public ProcessViewsBehaviour(string respondsTo) : base(respondsTo) { }


		/// <summary>
		/// Iterates over each view-step object for the provided context
		/// and fires the event for that viiews processing. This is a driving behaviour.
		/// </summary>
		/// <param name="ev">The vent that was considered for this action.</param>
		/// <param name="context">The context to act upon.</param>
		public override void Action(IEvent ev, IWebContext context) {
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Inversion.Process;
using Inversion.Process.Behaviour;

namespace Inversion.Web.Behaviour {

	/// <summary>
	/// Behaviour responsible for driving the view pipeline expressed
	/// as view-steps.
	/// </summary>
	public class ProcessViewsBehaviour : ConfiguredBehaviour {

		/// <summary>
		/// Instantiates a new behaviour responsible for processes the inversion view pipeline.
		/// </summary>
		/// <param name="respondsTo">The message that the behaviour will respond to.</param>
		public ProcessViewsBehaviour(string respondsTo) : base(respondsTo) { }
		/// <summary>
		/// Instantiates a new behaviour responsible for processes the inversion view pipeline.
		/// </summary>
		/// <param name="respondsTo">The message that the behaviour will respond to.</param>
		/// <param name="config">Configuration for the behaviour.</param>
		public ProcessViewsBehaviour(string respondsTo, Configuration config) : base(respondsTo, config) { }
		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		/// <param name="config">Configuration for the behaviour.</param>
		public ProcessViewsBehaviour(string respondsTo, IEnumerable<Configuration.Element> config) : base(respondsTo, config) {}

		/// <summary>
		/// Iterates over each view-step object for the provided context
		/// and fires the event for that viiews processing. This is a driving behaviour.
		/// </summary>
		/// <param name="ev">The vent that was considered for this action.</param>
		/// <param name="context">The context to act upon.</param>
		public override void Action(IEvent ev, IProcessContext context) {
			// this is the last point we can time until and still output it
			if (context.Timers.ContainsKey("process-request")) context.Timers.End("process-request");
			// check that we have an initial view step laid down at least
			if (context.ViewSteps.HasSteps) {
				// then determine how many views there are to process
				// in this convention we take the view as specified by the "tail" of the request url
				IEnumerable<string> views;
				if (context.HasParams("views")) {
					views = context.Params["views"].Split(new string[] {";"}, StringSplitOptions.RemoveEmptyEntries);
				} else if (this.Configuration.Has("config", "default-view")) {
					views = this.Configuration.GetNames("config", "default-view");
				} else {
					views = new String[] {"st"};
				}
				foreach (string view in views) {
					if (!String.IsNullOrEmpty(view)) {
						string msg = String.Format("{0}::view", view);
						context.Fire(msg);
					}
				}
			} else {
				throw new ProcessException("There are no view steps to process.");
			}
		}
	}
}

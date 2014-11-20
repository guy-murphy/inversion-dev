using System;
using Inversion.Collections;

namespace Inversion.Process {

	/// <summary>
	/// A simple dictionary that contains and helps control process timer instances.
	/// </summary>
	public class ProcessTimerDictionary : ConcurrentDataDictionary<ProcessTimer> {

		/// <summary>
		/// Create and start a new timer of the specified name.
		/// </summary>
		/// <param name="name">The name of the new timer.</param>
		/// <returns>Returns the timer that has just been started.</returns>
		public ProcessTimer Begin(string name) {
			ProcessTimer timer = new ProcessTimer();
			this[name] = timer.Begin();
			return timer;
		}

		/// <summary>
		/// Ends the process timer of the corresponding name.
		/// </summary>
		/// <param name="name">The name of the timer to end.</param>
		/// <returns>Returns the process timer that was ended.</returns>
		public ProcessTimer End(string name) {
			return this[name].End();
		}

		/// <summary>
		/// Creates and starts a new timer of a specified name,
		/// starts it, performs the provided action, and then stops the timer.
		/// </summary>
		/// <param name="name">The name of the process timer.</param>
		/// <param name="action">The action to perform.</param>
		/// <returns>Returns the process timer that was run.</returns>
		public ProcessTimer TimeAction(string name, Action action) {
			ProcessTimer timer = new ProcessTimer();
			this[name] = timer.Begin();
			action();
			return timer.End();
		}

	}
}

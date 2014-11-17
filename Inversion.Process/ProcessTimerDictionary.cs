using System;
using Inversion.Collections;

namespace Inversion.Process {
	public class ProcessTimerDictionary : ConcurrentDataDictionary<ProcessTimer> {

		public ProcessTimer Begin(string name) {
			ProcessTimer timer = new ProcessTimer();
			this[name] = timer.Begin();
			return timer;
		}

		public ProcessTimer End(string name) {
			return this[name].End();
		}

		public ProcessTimer TimeAction(string name, Action action) {
			ProcessTimer timer = new ProcessTimer();
			this[name] = timer.Begin();
			action();
			return timer.End();
		}

	}
}

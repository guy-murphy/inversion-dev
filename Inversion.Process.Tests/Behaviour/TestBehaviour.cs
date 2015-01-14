using System;
using System.Collections.Generic;
using System.Linq;
using Inversion.Process.Behaviour;

namespace Inversion.Process.Tests.Behaviour {
	public class TestBehaviour: ConfiguredBehaviour {

		public Action<IEvent, IProcessContext> Perform { get; set; }

		/// <summary>
		/// Creates a new instance of the behaviour with no configuration.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		public TestBehaviour(string respondsTo) : base(respondsTo) {}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		/// <param name="config">Configuration for the behaviour.</param>
		public TestBehaviour(string respondsTo, Configuration config) : base(respondsTo, config) {}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		/// <param name="config">Configuration for the behaviour.</param>
		public TestBehaviour(string respondsTo, IEnumerable<Configuration.Element> config) : base(respondsTo, config) {}

		/// <summary>
		/// The action to perform when the `Condition(IEvent)` is met.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		public override void Action(IEvent ev, IProcessContext context) {
			if (this.Perform != null) this.Perform(ev, context);
		}
	}
}

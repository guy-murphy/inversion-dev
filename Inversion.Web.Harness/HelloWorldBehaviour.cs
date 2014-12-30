using System;
using System.Collections.Generic;
using Inversion.Process;
using Inversion.Process.Behaviour;

namespace Inversion.Web.Harness {

	/// <summary>
	/// A simple behaviour to wire up to test the simplest possible output.
	/// </summary>
	public class HelloWorldBehaviour : ApplicationBehaviour {
		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="message">The name of the behaviour.</param>
		/// <param name="namedLists">Named lists used to configure this behaviour.</param>
		/// <param name="namedMaps">Named maps used to configure this behaviour.</param>
		/// <param name="namedMappedLists">Named maps of lists used to configure this behaviour.</param>
		public HelloWorldBehaviour(string message, IDictionary<string, IEnumerable<string>> namedLists = null,
			IDictionary<string, IDictionary<string, string>> namedMaps = null,
			IDictionary<string, IDictionary<string, IEnumerable<string>>> namedMappedLists = null)
			: base(message, namedLists, namedMaps, namedMappedLists) {}

		/// <summary>
		/// The action to perform if this behaviours condition is met.
		/// </summary>
		/// <param name="ev">The event that gave rise to this action.</param>
		/// <param name="context">The context within which this action is being performed.</param>
		public override void Action(IEvent ev, ProcessContext context) {
			context.Messages.Add("Hello World!");
			context.Messages.Add(DateTime.Now.ToLongTimeString());
		}

	}
}

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
		/// <param name="name">The name of the behaviour.</param>
		public HelloWorldBehaviour(string name) : base(name) {}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="name">The name of the behaviour.</param>
		/// <param name="namedLists">Named lists used to configure this behaviour.</param>	
		public HelloWorldBehaviour(string name, IDictionary<string, IEnumerable<string>> namedLists) : base(name, namedLists) {}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="name">The name of the behaviour.</param>
		/// <param name="namedMaps">Named maps used to configure this behaviour.</param>
		public HelloWorldBehaviour(string name, IDictionary<string, IDictionary<string, string>> namedMaps) : base(name, namedMaps) {}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="name">The name of the behaviour.</param>
		/// <param name="namedLists">Named lists used to configure this behaviour.</param>
		/// <param name="namedMaps">Named maps used to configure this behaviour.</param>
		public HelloWorldBehaviour(string name, IDictionary<string, IEnumerable<string>> namedLists, IDictionary<string, IDictionary<string, string>> namedMaps) : base(name, namedLists, namedMaps) {}

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

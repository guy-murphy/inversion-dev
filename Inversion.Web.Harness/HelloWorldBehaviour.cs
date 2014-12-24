using System;

using Inversion.Process;
using Inversion.Process.Behaviour;

namespace Inversion.Web.Harness {

	/// <summary>
	/// A simple behaviour to wire up to test the simplest possible output.
	/// </summary>
	public class HelloWorldBehaviour : ApplicationBehaviour {
		/// <summary>
		/// Ensures on instantiation that the base web behaviour constructor
		/// is called with the message provided.
		/// </summary>
		/// <param name="message">The message the behaviour has set as responding to.</param>
		public HelloWorldBehaviour(string message, bool preprocess = false, bool postprocess = false) : base(message, preprocess, postprocess) {}

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

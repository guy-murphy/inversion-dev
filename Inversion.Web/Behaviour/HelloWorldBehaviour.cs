using System;
using Inversion.Process;

namespace Inversion.Web.Behaviour {

	/// <summary>
	/// A simple behaviour to wire up to test the simplest possible output.
	/// </summary>
	public class HelloWorldBehaviour : WebActionBehaviour {

		/// <summary>
		/// Instantiates a new hello world behaviour configured to
		/// respond to the provided message.
		/// </summary>
		/// <param name="message">The message this behaviour should respond to.</param>
		public HelloWorldBehaviour(string message) : base(message) { }

		/// <summary>
		/// The action to perform if this behaviours condition is met.
		/// </summary>
		/// <param name="ev">The event that gave rise to this action.</param>
		/// <param name="context">The context within which this action is being performed.</param>
		public override void Action(IEvent ev, WebContext context) {
			context.Messages.Add("Hello World!");
			context.Messages.Add(DateTime.Now.ToLongTimeString());
		}

	}
}

using System;
using Inversion.Process;

namespace Inversion.Web.Behaviour {
	public class HelloWorldBehaviour : WebActionBehaviour {

		public HelloWorldBehaviour(string message) : base(message) { }

		public override void Action(IEvent ev, WebContext context) {
			context.Messages.Add("Hello World!");
			context.Messages.Add(DateTime.Now.ToLongTimeString());
		}

	}
}

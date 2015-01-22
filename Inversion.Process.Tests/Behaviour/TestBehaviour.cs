using System;
using System.Collections.Generic;
using Inversion.Process.Behaviour;

namespace Inversion.Process.Tests.Behaviour {
	public class TestBehaviour: ConfiguredBehaviour {

		private readonly Action<IEvent, IProcessContext> _action;

		public TestBehaviour(string respondsTo) : base(respondsTo) {}

		public TestBehaviour(string respondsTo, IEnumerable<IConfigurationElement> config) : this(respondsTo, config, null) { }

		public TestBehaviour(string respondsTo, Action<IEvent, IProcessContext> action) : base(respondsTo) {
			_action = action;
		}

		public TestBehaviour(string respondsTo, IEnumerable<IConfigurationElement> config, Action<IEvent, IProcessContext> perform) 
			: base(respondsTo, config) {
				_action = perform;
		}

		public override void Action(IEvent ev, IProcessContext context) {
			if (_action != null) _action(ev, context);
		}
	}
}

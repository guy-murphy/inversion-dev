using System;
using System.Collections.Generic;

namespace Inversion.Process {
	public class RuntimeBehaviour : ProcessBehaviour {

		private readonly Predicate<IEvent> _condition;
		private readonly Action<IEvent, ProcessContext> _action;

		protected RuntimeBehaviour(string message) : base(message) { }


		public RuntimeBehaviour(string message, Predicate<IEvent> condition, Action<IEvent, ProcessContext> action)
			: base(message) {
			_condition = condition;
			_action = action;
		}

		public override bool Condition(IEvent ev) {
			return _condition(ev);
		}

		public override void Action(IEvent ev, ProcessContext context) {
			_action(ev, context);
		}
	}
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inversion.Process.Behaviour {
	/// <summary>
	/// A behaviour concerned with driving the processing of a
	/// sequence of messages.
	/// </summary>
	public class ParameterisedSequenceBehaviour: ApplicationBehaviour {
		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="name">The name of the behaviour.</param>
		public ParameterisedSequenceBehaviour(string name) : base(name) {}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="name">The name of the behaviour.</param>
		/// <param name="preprocess">Indicates whether the system should notify before this behaviours action is processed.</param>
		/// <param name="postprocess">Indicates whether the system should notify after this behaviours action has been processed.</param>
		public ParameterisedSequenceBehaviour(string name, bool preprocess = false, bool postprocess = false) : base(name, preprocess, postprocess) {}

		/// <summary>
		/// The action to perform when the `Condition(IEvent)` is met.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		public override void Action(IEvent ev, ProcessContext context) {
			throw new NotImplementedException();
		}
	}
}

using System.Collections.Generic;

namespace Inversion.Process {
	public class SimpleSequenceBehaviour : ProcessBehaviour {

		private readonly IEnumerable<string> _sequence;

		protected SimpleSequenceBehaviour(string name, IEnumerable<string> sequence)
			: base(name) {
			_sequence = sequence;
		}

		public override void Action(IEvent ev, ProcessContext context) {
			foreach (string item in _sequence) {
				context.Fire(item);			
			}
		}

	}
}

namespace Inversion.Process {

	/// <summary>
	/// A simple named behaviour with a default condition
	/// matching that name againts <see cref="IEvent.Message"/>.
	/// </summary>
	public abstract class ProcessBehaviour : IProcessBehaviour {

		private readonly string _message;

		public string Message {
			get {
				return _message;
			}
		}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="message">The name of the behaviour.</param>
		protected ProcessBehaviour(string message) {
			_message = message;
		}

		/// <summary>
		/// Determines if the event specifies the behaviour by name.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <returns>
		/// Returns <b>true</b> if the <see cref="IEvent.Message"/>
		/// is the same as the <see cref="ProcessBehaviour.Message"/>
		/// </returns>
		/// <remarks>
		/// The intent is to override for bespoke conditions.
		/// </remarks>
		public virtual bool Condition(IEvent ev) {
			return this.Condition(ev, ev.Context);
		}

		public virtual bool Condition(IEvent ev, ProcessContext context) {
			// check the base condition
			// and then either there are no roles specified
			// or the user is in any of the roles defined
			return ev.Message == this.Message;
		}

		/// <summary>
		/// The action to perform when the `Condition(IEvent)` is met.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		public virtual void Action(IEvent ev) {
			this.Action(ev, ev.Context);
		}

		/// <summary>
		/// The action to perform when the `Condition(IEvent)` is met.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		public abstract void Action(IEvent ev, ProcessContext context);

	}
}

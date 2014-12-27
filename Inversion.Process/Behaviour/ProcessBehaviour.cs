﻿using System;

namespace Inversion.Process.Behaviour {

	/// <summary>
	/// A simple named behaviour with a default condition
	/// matching that name againts <see cref="IEvent.Message"/>.
	/// </summary>
	public abstract class ProcessBehaviour : IProcessBehaviour {

		private readonly string _name;

		/// <summary>
		/// The name the behaviour is known by to the system.
		/// </summary>
		public string Name {
			get {
				return _name;
			}
		}
		
		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="name">The name of the behaviour.</param>
		protected ProcessBehaviour(string name) {
			_name = name;
		}

		/// <summary>
		/// Determines if the event specifies the behaviour by name.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <returns>
		/// Returns <b>true</b> if the <see cref="IEvent.Message"/>
		/// is the same as the <see cref="ProcessBehaviour.Name"/>
		/// </returns>
		/// <remarks>
		/// The intent is to override for bespoke conditions.
		/// </remarks>
		public virtual bool Condition(IEvent ev) {
			return this.Condition(ev, ev.Context);
		}

		/// <summary>
		/// Determines if the event specifies the behaviour by name.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context to consult.</param>
		/// <returns>
		/// Returns true if true if `ev.Message` is the same as `this.Message`
		///  </returns>
		/// <remarks>
		/// The intent is to override for bespoke conditions.
		/// </remarks>
		public virtual bool Condition(IEvent ev, ProcessContext context) {
			// check the base condition
			// and then either there are no roles specified
			// or the user is in any of the roles defined
			return ev.Message == this.Name;
		}

		/// <summary>
		/// Fires a message on the context of the event announcing
		/// preprocessing for the event.
		/// </summary>
		/// <remarks>
		/// The form the message will take is `"preprocessing::" + ev.Message`, and with
		/// parameters from the original message copied over.
		/// </remarks>
		/// <param name="ev">The event for which preprocessing should take place.</param>
		public virtual void Preprocess(IEvent ev) {
			string message = String.Concat("preprocess::", ev.Message);
			Event announcement = new Event(ev.Context, message, ev.Object, ev.Params);
			announcement.Fire();
		}

		/// <summary>
		/// Fires a message on the context of the event announcing
		/// postprocessing for the event.
		/// </summary>
		/// <remarks>
		/// The form the message will take is `"postprocessing::" + ev.Message`, and with
		/// parameters from the original message copied over.
		/// </remarks>
		/// <param name="ev">The event for which postprocessing should take place.</param>
		public virtual void Postprocess(IEvent ev) {
			string message = String.Concat("postprocess::", ev.Message);
			Event announcement = new Event(ev.Context, message, ev.Object, ev.Params);
			announcement.Fire();
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
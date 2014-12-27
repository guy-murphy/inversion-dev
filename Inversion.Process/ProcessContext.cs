using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading;

using Inversion.Collections;
using Inversion.Process.Behaviour;

namespace Inversion.Process {

	/// <summary>
	/// Provides a processing context as a self-contained and sufficient
	/// channel of application execution. The context manages a set of
	/// behaviours and mediates between them and the outside world.
	/// </summary>
	/// <remarks>
	/// The process context along with the `IBehaviour` objects registered
	/// on its bus *are* Inversion. Everything else is chosen convention about
	/// how those behaviours interact with each other via the context.
	/// </remarks>
	public class ProcessContext : IDisposable {

		private bool _isDisposed;

		private readonly MemoryCache _cache;
		private readonly Subject<IEvent> _bus;
		private readonly ConcurrentDataCollection<string> _messages;
		private readonly ConcurrentDataCollection<ErrorMessage> _errors;
		private readonly ProcessTimerDictionary _timers;
		private readonly ConcurrentDataDictionary<object> _controlState;
		private readonly ConcurrentDataCollection<string> _flags;
		private readonly ViewSteps _steps;
		// move this up from WebContext to facilitate
		// access to the context params from an Event
		// not 100% sure it's the correct move
		private readonly IDataDictionary<string> _params;

		private readonly IServiceContainer _serviceContainer;


		/// <summary>
		/// Exposes the processes service container.
		/// </summary>
		public IServiceContainer Services {
			get {
				return _serviceContainer; 
			}
		}

		/// <summary>
		/// The event bus of the process.
		/// </summary>
		protected ISubject<IEvent> Bus {
			get {
				return _bus;
			}
		}

		/// <summary>
		/// Provsion of a simple object cache for the context.
		/// </summary>
		/// <remarks>
		/// This really needs replaced with our own interface
		/// that we control. This isn't portable.
		/// </remarks>
		protected ObjectCache ObjectCache {
			get { return _cache; }
		}

		/// <summary>
		/// Messages intended for user feedback.
		/// </summary>
		/// <remarks>
		/// This is a poor mechanism for localisation,
		/// and may need to be treated as tokens
		/// by the front end to localise.
		/// </remarks>
		public IDataCollection<string> Messages {
			get {
				return _messages;
			}
		}

		/// <summary>
		/// Error messages intended for user feedback.
		/// </summary>
		/// <remarks>
		/// This is a poor mechanism for localisation,
		/// and may need to be treated as tokens
		/// by the front end to localise.
		/// </remarks>
		public IDataCollection<ErrorMessage> Errors {
			get {
				return _errors;
			}
		}

		/// <summary>
		/// A dictionary of named timers.
		/// </summary>
		/// <remarks>
		/// `ProcessTimer` is only intended
		/// for informal timings, and it not intended
		/// for proper metrics.
		/// </remarks>
		public ProcessTimerDictionary Timers {
			get { return _timers; }
		}

		/// <summary>
		/// Gives access to a collection of view steps
		/// that will be used to control the render
		/// pipeline for this context.
		/// </summary>
		public ViewSteps ViewSteps {
			get {
				return _steps;
			}
		}

		/// <summary>
		/// Gives access to the current control state of the context.
		/// This is the common state that behaviours share and that
		/// provides the end state or result of a contexts running process.
		/// </summary>
		public IDataDictionary<object> ControlState {
			get {
				return _controlState;
			}
		}

		/// <summary>
		/// Flags for the context available to behaviours as shared state.
		/// </summary>
		public ConcurrentDataCollection<string> Flags {
			get {
				return _flags;
			}
		}

		/// <summary>
		/// The parameters of the contexts execution available
		/// to behaviours as shared state.
		/// </summary>
		public IDataDictionary<string> Params {
			get { return _params; }
		}

		/// <summary>
		/// Instantiates a new process contrext for inversion.
		/// </summary>
		/// <remarks>You can think of this type here as "being Inversion". This is the thing.</remarks>
		/// <param name="services">The service container the context will use.</param>
		public ProcessContext(IServiceContainer services) {
			_serviceContainer = services;
			_cache = MemoryCache.Default;
			_bus = new Subject<IEvent>();
			_messages = new ConcurrentDataCollection<string>();
			_errors = new ConcurrentDataCollection<ErrorMessage>();
			_timers = new ProcessTimerDictionary();
			_controlState = new ConcurrentDataDictionary<object>();
			_flags = new ConcurrentDataCollection<string>();
			_steps = new ViewSteps();
			_params = new ConcurrentDataDictionary<string>();
		}

		/// <summary>
		/// Desrtructor for the type.
		/// </summary>
		~ProcessContext() {
			// ensure unmanaged resources are cleaned up
			// this might all be a bit conceipted, I'm not sure
			// we've run into a real use-case requiring this since day one.
			this.Dispose(false);
		}

		/// <summary>
		/// Releases all resources maintained by the current context instance.
		/// </summary>
		public void Dispose() {
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Disposal that allows for partitioning of 
		/// clean-up of managed and unmanaged resources.
		/// </summary>
		/// <param name="disposing"></param>
		/// <remarks>
		/// This is looking conceited and should probably be removed.
		/// I'm not even sure I can explain a use case for it in terms
		/// of an Inversion context.
		/// </remarks>
		protected virtual void Dispose(bool disposing) {
			if (!_isDisposed) {
				if (disposing) {
					// managed resource clean-up
					if (_bus != null) _bus.Dispose();
				}
				// unmanaged resource clean-up
				// ... nothing to do
				// call dispose on base class, and clear data
				// base.Dispose(disposing);
				// mark disposing as done
				_isDisposed = true;
			}
		}

		/// <summary>
		/// Registers a behaviour with the context ensuring
		/// it is consulted for each event fired on this context.
		/// </summary>
		/// <param name="behaviour">The behaviour to register with this context.</param>
		public void Register(IProcessBehaviour behaviour) {
			this.Bus.Where(behaviour.Condition).Subscribe((IEvent ev) => behaviour.Action(ev));
		}

		/// <summary>
		/// Registers a whole bunch of behaviours with this context ensuring
		/// each one is consulted when an event is fired on this context.
		/// </summary>
		/// <param name="behaviours">The behaviours to register with this context.</param>
		public void Register(IEnumerable<IProcessBehaviour> behaviours) {
			foreach (IProcessBehaviour behaviour in behaviours) {
				this.Register(behaviour);
			}
		}

		/// <summary>
		/// Creates and registers a runtime behaviour with this context constructed 
		/// from a predicate representing the behaviours condition, and an action
		/// representing the behaviours action. This behaviour will be consulted for
		/// any event fired on this context.
		/// </summary>
		/// <param name="condition">The predicate to use as the behaviours condition.</param>
		/// <param name="action">The action to use as the behaviours action.</param>
		public void Register(Predicate<IEvent> condition, Action<IEvent, ProcessContext> action) {
			this.Register(new RuntimeBehaviour(String.Empty, condition, action));
		}

		/// <summary>
		/// Fires an event on the context. Each behaviour registered with context
		/// is consulted in no particular order, and for each behaviour that has a condition
		/// that returns true when applied to the event, that behaviours action is executed.
		/// </summary>
		/// <param name="ev">The event to fire on this context.</param>
		/// <returns></returns>
		public IEvent Fire(IEvent ev) {
			if (ev.Context != this) throw new ProcessException("The event has a different context that the one on which it has been fired.");
			try {
				this.Bus.OnNext(ev);
			} catch (ThreadAbortException) {
				// we have probably been redirected
			} catch (Exception err) {
				this.Errors.Add(new ErrorMessage(String.Format("A problem was encountered firing '{0}'", ev.Message), err));
			}
			return ev;
		}

		/// <summary>
		/// Constructs a simple event, with a simple string message
		/// and fires it on this context.
		/// </summary>
		/// <param name="message">The message to assign to the event.</param>
		/// <returns>Returns the event that was constructed and fired on this context.</returns>
		public IEvent Fire(string message) {
			IEvent ev = new Event(this, message);
			this.Fire(ev);
			return ev;
		}

		/// <summary>
		/// Constructs an event using the message specified, and using the dictionary
		/// provided to populate the parameters of the event. This event is then
		/// fired on this context.
		/// </summary>
		/// <param name="message">The message to assign to the event.</param>
		/// <param name="parms">The parameters to populate the event with.</param>
		/// <returns>Returns the event that was constructed and fired on this context.</returns>
		public IEvent Fire(string message, IDictionary<string, string> parms) {
			IEvent ev = new Event(this, message, parms);
			this.Fire(ev);
			return ev;
		}

		//public IEvent Fire(string message, object obj) {
		//	IEvent ev = new Event(this, message, obj);
		//	this.Fire(ev);
		//	return ev;
		//}

		//public IEvent Fire(string message, object obj, IDictionary<string, string> parms) {
		//	IEvent ev = new Event(this, message, obj, parms);
		//	this.Fire(ev);
		//	return ev;
		//}

		//public IEvent Fire(string message, object obj, params string[] parms) {
		//	IEvent ev = new Event(this, message, obj, parms);
		//	this.Fire(ev);
		//	return ev;
		//}

		/// <summary>
		/// Contructs an event with the message specified, using the supplied
		/// parameter keys to copy parameters from the context to the constructed event.
		/// This event is then fired on this context.
		/// </summary>
		/// <param name="message">The message to assign to the event.</param>
		/// <param name="parms">The parameters to copy from the context.</param>
		/// <returns>Returns the event that was constructed and fired on this context.</returns>
		public IEvent FireWith(string message, params string[] parms) {
			IEvent ev = new Event(this, message, parms);
			this.Fire(ev);
			return ev;
		}

		/// <summary>
		/// Instructs the context that operations have finished, and that while it
		/// may still be consulted no further events will be fired.
		/// </summary>
		public void Completed() {
			this.Bus.OnCompleted();
		}

		/// <summary>
		/// Determines whether or not the flag of the
		/// specified key exists.
		/// </summary>
		/// <param name="flag">The key of the flag to check for.</param>
		/// <returns>Returns true if the flag exists; otherwise returns false.</returns>
		public bool IsFlagged(string flag) {
			return this.Flags.Contains(flag);
		}

		/// <summary>
		/// Determines whether or not the parameters 
		/// specified exist in the current context.
		/// </summary>
		/// <param name="parms">The parameters to check for.</param>
		/// <returns>Returns true if all the parameters exist; otherwise return false.</returns>
		public bool HasParams(params string[] parms) {
			return parms.Length > 0 && parms.All(parm => this.Params.ContainsKey(parm));
		}

		public bool HasParams(IEnumerable<string> parms) {
			return parms != null && parms.All(parm => this.Params.ContainsKey(parm));
		}

		/// <summary>
		/// Determines whether or not the parameter name and
		/// value specified exists in the current context.
		/// </summary>
		/// <param name="name">The name of the parameter to check for.</param>
		/// <param name="value">The value of the parameter to check for.</param>
		/// <returns>
		/// Returns true if a parameter with the name and value specified exists
		/// in this conext; otherwise returns false.
		/// </returns>
		public bool HasParamValue(string name, string value) {
			return this.Params.ContainsKey(name) && this.Params[name] == value;
		}

		/// <summary>
		/// Determines whether or not all the key-value pairs
		/// provided exist in the contexts parameters.
		/// </summary>
		/// <param name="match">The key-value pairs to check for.</param>
		/// <returns>
		/// Returns true if all the key-value pairs specified exists in the contexts
		/// parameters; otherwise returns false.
		/// </returns>
		public bool HasParamValues(IEnumerable<KeyValuePair<string, string>> match) {
			return match != null && match.All(entry => this.Params.Contains(entry));
		}

		/// <summary>
		/// Determines whether or not the specified
		/// paramters exist in this context, and produces
		/// and error for each one that does not exist.
		/// </summary>
		/// <param name="parms">The parameter keys to check for.</param>
		/// <returns>Returns true if all the paramter keys are present; otherwise returns false.</returns>
		public bool HasRequiredParams(params string[] parms) {
			bool has = parms.Length > 0;
			foreach (string parm in parms) {
				if (!this.Params.ContainsKey(parm)) {
					has = false;
					this.Errors.CreateMessage(String.Format("The parameter '{0}' is required and was not present.", parm));
				}
			}
			return has;
		}

		/// <summary>
		/// Dtermines whether or not the control state has entries indexed
		/// under the keys provided.
		/// </summary>
		/// <param name="parms">The keys to check for in the control state.</param>
		/// <returns>
		/// Returns true if all the specified keys exist in the control state;
		/// otherwise returns false.
		/// </returns>
		public bool HasControlState(params string[] parms) {
			return parms.Length > 0 && parms.All(parm => this.ControlState.Keys.Contains(parm));
		}

		public bool HasControlState(IEnumerable<string> parms) {
			return parms != null && parms.All(parm => this.ControlState.Keys.Contains(parm));
		}

		/// <summary>
		/// Dtermines whether or not the control state has entries indexed
		/// under the keys provided, and creates an error for each one that doesn't.
		/// </summary>
		/// <param name="parms">The keys to check for in the control state.</param>
		/// <returns>
		/// Returns true if all the specified keys exist in the control state;
		/// otherwise returns false.
		/// </returns>
		public bool HasRequiredControlState(params string[] parms) {
			bool has = parms.Length > 0;
			foreach (string parm in parms) {
				if (!this.ControlState.ContainsKey(parm)) {
					has = false;
					this.Errors.CreateMessage(String.Format("The control state '{0}' is required and was not present.", parm));
				}
			}
			return has;
		}

		/// <summary>
		/// Obtains the context prarameter for the specified
		/// key, or if it doesn't exist uses the default value specified.
		/// </summary>	
		/// <param name="key">The key of the context parameter to use.</param>
		/// <param name="defaultValue">The value to use if the parameter doesn't exist.</param>
		/// <returns>Returns the specified parameter if it exists; otherwise returns false.</returns>
		public string ParamOrDefault(string key, string defaultValue) {
			return this.Params.ContainsKey(key) ? this.Params[key] : defaultValue;
		}

		/// <summary>
		/// Provides a string representation of the context and its current state.
		/// </summary>
		/// <returns>Returns a string representation of the context.</returns>
		public override string ToString() {
			// I'm not sure the value this method adds to the type.
			StringBuilder sb = new StringBuilder();

			if (this.ViewSteps.HasSteps && this.ViewSteps.Last.HasModel) {
				sb.Append(this.ViewSteps.Last.ToString());
			}

			return sb.ToString();
		}

	}
}

using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading;

using Inversion.Collections;
using Inversion.Data;
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
	public class ProcessContext : IProcessContext {

		private bool _isDisposed;

		private readonly MemoryCache _cache;
		private readonly Subject<IEvent> _bus;
		private readonly DataCollection<string> _messages;
		private readonly DataCollection<ErrorMessage> _errors;
		private readonly ProcessTimerDictionary _timers;
		private readonly DataDictionary<object> _controlState;
		private readonly DataCollection<string> _flags;
		private readonly ViewSteps _steps;
		private readonly IDataDictionary<string> _params;

		private readonly IServiceContainer _serviceContainer;
		private readonly IResourceAdapter _resources;


		/// <summary>
		/// Exposes the processes service container.
		/// </summary>
		public IServiceContainer Services {
			get { return _serviceContainer; }
		}

		/// <summary>
		/// Exposes resources external to the process.
		/// </summary>
		public IResourceAdapter Resources {
			get { return _resources; }
		}

		/// <summary>
		/// The event bus of the process.
		/// </summary>
		protected ISubject<IEvent> Bus {
			get { return _bus; }
		}

		/// <summary>
		/// Provsion of a simple object cache for the context.
		/// </summary>
		/// <remarks>
		/// This really needs replaced with our own interface
		/// that we control. This isn't portable.
		/// </remarks>
		public ObjectCache ObjectCache {
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
			get { return _errors; }
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
			get { return _steps; }
		}

		/// <summary>
		/// Gives access to the current control state of the context.
		/// This is the common state that behaviours share and that
		/// provides the end state or result of a contexts running process.
		/// </summary>
		public IDataDictionary<object> ControlState {
			get { return _controlState; }
		}

		/// <summary>
		/// Flags for the context available to behaviours as shared state.
		/// </summary>
		public IDataCollection<string> Flags {
			get { return _flags; }
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
		/// <param name="resources">The resources available to the context.</param>
		public ProcessContext(IServiceContainer services, IResourceAdapter resources) {
			_serviceContainer = services;
			_resources = resources;
			_cache = MemoryCache.Default;
			_bus = new Subject<IEvent>();
			_messages = new DataCollection<string>();
			_errors = new DataCollection<ErrorMessage>();
			_timers = new ProcessTimerDictionary();
			_controlState = new DataDictionary<object>();
			_flags = new DataCollection<string>();
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
		public void Register(Predicate<IEvent> condition, Action<IEvent, IProcessContext> action) {
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading;

using Inversion.Collections;

namespace Inversion.Process {
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

		private IServiceContainer _serviceContainer;


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
		/// 
		/// </summary>
		public ViewSteps ViewSteps {
			get {
				return _steps;
			}
		}

		public IDataDictionary<object> ControlState {
			get {
				return _controlState;
			}
		}

		public ConcurrentDataCollection<string> Flags {
			get {
				return _flags;
			}
		}

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

		~ProcessContext() {
			// ensure unmanaged resources are cleaned up
			this.Dispose(false);
		}

		public void Dispose() {
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

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


		public void Register(IProcessBehaviour behaviour) {
			this.Bus.Where(behaviour.Condition).Subscribe(behaviour.Action);
		}

		public void Register(IEnumerable<IProcessBehaviour> behaviours) {
			foreach (IProcessBehaviour behaviour in behaviours) {
				this.Register(behaviour);
			}
		}

		public void Register(Predicate<IEvent> condition, Action<IEvent, ProcessContext> action) {
			this.Register(new RuntimeBehaviour(String.Empty, condition, action));
		}

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

		public IEvent Fire(string message) {
			IEvent ev = new Event(this, message);
			this.Fire(ev);
			return ev;
		}

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

		public IEvent FireWith(string message, params string[] parms) {
			IEvent ev = new Event(this, message, parms);
			this.Fire(ev);
			return ev;
		}

		public void Completed() {
			this.Bus.OnCompleted();
		}

		public bool IsFlagged(string flag) {
			return this.Flags.Contains(flag);
		}

		public bool HasParams(params string[] parms) {
			return parms.Length > 0 && parms.All(parm => this.Params.ContainsKey(parm));
		}

		public bool HasParamValue(string name, string value) {
			return this.Params.ContainsKey(name) && this.Params[name] == value;
		}

		public bool HasParamValues(IEnumerable<KeyValuePair<string, string>> match) {
			return match.All(entry => this.Params.Contains(entry));
		}

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

		public bool HasControlState(params string[] parms) {
			return parms.Length > 0 && parms.All(parm => this.ControlState.Keys.Contains(parm));
		}

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

		public string ParamOrDefault(string key, string defaultValue) {
			return this.Params.ContainsKey(key) ? this.Params[key] : defaultValue;
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();

			if (this.ViewSteps.HasSteps && this.ViewSteps.Last.HasModel) {
				sb.Append(this.ViewSteps.Last.ToString());
			}

			return sb.ToString();
		}

	}
}

using System;
using System.Collections.Concurrent;
using System.Threading;
using Inversion.Process;

namespace Inversion.Naiad {
	public class ServiceContainer : IServiceContainer, IServiceContainerRegistrar {

		private static readonly ServiceContainer _instance = new ServiceContainer();

		private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

		public static ServiceContainer Instance {
			get { return _instance; }
		}

		private readonly ConcurrentDictionary<string, object> _ctors = new ConcurrentDictionary<string, object>();
		private readonly ConcurrentDictionary<string, object> _objs = new ConcurrentDictionary<string, object>();
        private readonly ConcurrentDictionary<string, bool> _isSingleton = new ConcurrentDictionary<string, bool>();

		~ServiceContainer() {
			Dispose(false);
		}

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing) {
			if (disposing) {
				_ctors.Clear();
				_lock.Dispose();
			}
		}

		public void RegisterService<T>(string name, Func<IServiceContainer, T> ctor) {
			_lock.EnterWriteLock();
			try {
				_ctors[name] = ctor;
			    _isSingleton[name] = true;
			} finally {
				_lock.ExitWriteLock();
			}
		}

	    public void RegisterServiceNonSingleton<T>(string name, Func<IServiceContainer, T> ctor) {
	        _lock.EnterWriteLock();
	        try {
	            _ctors[name] = ctor;
	            _isSingleton[name] = false;
	        } finally {
	            _lock.ExitWriteLock();
	        }
	    }

		public T GetService<T>(string name) where T : class {
			_lock.EnterReadLock();
			try {
				T obj = null;
			    bool singleton = _isSingleton.ContainsKey(name) && _isSingleton[name];
				if (singleton && _objs.ContainsKey(name)) {
					return _objs[name] as T;
				} else {
				    var item = _ctors[name];
				    if (item == null) {
				        return null;
				    }
                    Func<IServiceContainer, T> ctor = null;
                    if(item is Func<IServiceContainer, T>) {
                        ctor = (Func<IServiceContainer, T>) _ctors[name];
				    }
					if (ctor != null) {
						obj = ctor(this);
					}
				    if (singleton) {
				        _objs[name] = obj;
				    }
					return obj;
				}
			} finally {
				_lock.ExitReadLock();
			}
		}

		public bool ContainsService(string name) {
			_lock.EnterReadLock();
			try {
				return _ctors.ContainsKey(name);
			} finally {
				_lock.ExitReadLock();
			}
		}

	}
}

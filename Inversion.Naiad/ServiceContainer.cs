using System;
using System.Collections.Concurrent;
using System.Threading;
using Inversion.Process;

namespace Inversion.Naiad {
	public class ServiceContainer : IServiceContainer {

		private static readonly ServiceContainer _instance = new ServiceContainer();

		private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

		public static ServiceContainer Instance {
			get { return _instance; }
		}

		private readonly ConcurrentDictionary<string, object> _ctors = new ConcurrentDictionary<string, object>();

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
			} finally {
				_lock.ExitWriteLock();
			}
		}

		public T GetService<T>(string name) {
			_lock.EnterReadLock();
			try {
				Func<IServiceContainer, T> ctor = _ctors[name] as Func<IServiceContainer, T>;
				return ctor(this);
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

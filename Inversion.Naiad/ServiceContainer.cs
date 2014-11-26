using System;
using System.Collections.Concurrent;

using Inversion.Process;

namespace Inversion.Naiad {
	public class ServiceContainer: IServiceContainer {

		private static readonly ServiceContainer _instance = new ServiceContainer();

		public static ServiceContainer Instance {
			get { return _instance; }
		}

		private readonly ConcurrentDictionary<string, object> _ctors = new ConcurrentDictionary<string, object>();

		public void RegisterService<T>(string name, Func<IServiceContainer, T> ctor) {
			_ctors[name] = ctor;
		}
		
		public T GetService<T>(string name) {
			Func<IServiceContainer, T> ctor = _ctors[name] as Func<IServiceContainer, T>;
			if (ctor == null) {
				throw new ProcessException("Unable to find the function to create the service named.");
			}
			return ctor(this);
		}

		public bool ContainsService(string name) {
			return _ctors.ContainsKey(name);
		}

		public void Dispose() {
			_ctors.Clear();
		}

	}
}

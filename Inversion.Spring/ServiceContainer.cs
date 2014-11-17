using System;

using Spring.Context;
using Spring.Context.Support;

using Inversion.Process;

namespace Inversion.Spring {
	public class ServiceContainer : IServiceContainer {

		private static IServiceContainer _instance = new ServiceContainer();

		public static IServiceContainer Instance {
			get {
				return _instance;
			}
			set {
				if (_instance != null) {
					throw new ApplicationException("Service container already set");
				}
				_instance = value;
			}
		}

		private readonly IApplicationContext _container;
		
		public object this[string name] {
			get {
				return this.GetObject(name);
			}
		}

		public ServiceContainer() {
			_container = ContextRegistry.GetContext();
		}

		public ServiceContainer(IApplicationContext container) {
			_container = container;
		}

		public void Dispose() {
			//
		}

		public object GetObject(string name) {
			return _container.GetObject(name);
		}

		public object GetObject(string name, Type type) {
			return _container.GetObject(name, type);
		}

		public T GetObject<T>(string name) {
			return (T)this.GetObject(name, typeof(T));
		}

		public bool ContainsObject(string name) {
			return _container.ContainsObject(name);
		}
	}
}

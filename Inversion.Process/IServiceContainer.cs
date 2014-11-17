using System;

namespace Inversion.Process {
	public interface IServiceContainer : IDisposable {
		object this[string name] { get; }
		object GetObject(string name);
		object GetObject(string name, Type type);
		T GetObject<T>(string name);
		bool ContainsObject(string name);
	}
}

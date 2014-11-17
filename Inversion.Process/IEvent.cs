using System.Collections.Generic;

namespace Inversion.Process {
	public interface IEvent : IData {
		string this[string key] { get; }
		string Message { get; }
		IDictionary<string, string> Params { get; }
		void Add(string key, string value);
		ProcessContext Context { get; }

		// a dirty escape hatch for when you just gotta
		// you can use this effectively as a "return"
		object Object { get; set; }

		bool HasParams(params string[] parms);
		bool HasParamValues(IEnumerable<KeyValuePair<string, string>> match);
		bool HasRequiredParams(params string[] parms);
		IEvent Fire();
	}
}

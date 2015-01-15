using System.Collections.Generic;
using System.IO;

namespace Inversion.Data {
	/// <summary>
	/// Describes basic functionality for reading resources
	/// external to the application.
	/// </summary>
	public interface IResourceAdapter {
		
		bool Exists(string path);
		Stream Open(string path);
		byte[] ReadAllBytes(string path);
		IEnumerable<string> ReadLines(string path);
		string[] ReadAllLines(string path);
		string ReadAllText(string path);
	}
}
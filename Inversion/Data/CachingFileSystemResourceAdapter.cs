using System.Collections.Concurrent;
using System.IO;

namespace Inversion.Data {
	/// <summary>
	/// Describes basic functionality for reading resources
	/// external to the application stored on the filesystem.
	/// </summary>
	/// <remarks>
	/// This adapter will perform crude caching for file exists and read all text.
	/// This is to optomise the hot path of the view behaviours, and will be fleshed out
	/// as needed, or killed for an alternative.
	/// </remarks>
	public class CachingFileSystemResourceAdapter: FileSystemResourceAdapter {

		/// <summary>
		/// A default instance.
		/// </summary>
		public new static readonly FileSystemResourceAdapter Instance = new CachingFileSystemResourceAdapter();

		private readonly ConcurrentDictionary<string, bool> _exists = new ConcurrentDictionary<string, bool>();
		private readonly ConcurrentDictionary<string, string> _text = new ConcurrentDictionary<string, string>();

		/// <summary>
		/// Determines whether or not the relative path
		/// specified exists.
		/// </summary>
		/// <param name="path">The relative path to check for.</param>
		/// <returns>
		/// Returns true if the resource exists; otherwise, returns false.
		/// </returns>
		public override bool Exists(string path) {
			string resolvedPath = this.ResolvePath(path);
			if (!_exists.ContainsKey(resolvedPath)) {
				_exists[resolvedPath] = File.Exists(resolvedPath);
			}
			return _exists[resolvedPath];
		}

		/// <summary>
		/// Opens the specified resource, reads its contents, and
		/// then closes the resource.
		/// </summary>
		/// <param name="path">The relative path to the resource.</param>
		/// <returns>Returns the contents of the resource as text.</returns>
		public override string ReadAllText(string path) {
			string resolvedPath = this.ResolvePath(path);
			if (!_text.ContainsKey(resolvedPath)) {
				_text[resolvedPath] = File.ReadAllText(resolvedPath);
			}
			return _text[resolvedPath];
		}

	}
}

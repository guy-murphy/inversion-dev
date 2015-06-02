using System.Collections.Concurrent;
using System.IO;

namespace Inversion.Data {
	/// <summary>
	/// Provides basic functionality for reading resources
	/// external to the application stored on the filesystem.
	/// </summary>
	/// <remarks>
	/// This adapter will perform crude caching for file exists and read all text.
	/// This is to optomise the hot path of the view behaviours using templates on the file-system, and will be fleshed out
	/// as needed, or more likely killed for an alternative. Some work probably
	/// needs to be done on providing an interface for caching resources in this space.
	/// </remarks>
	public class CachingFileSystemResourceAdapter: FileSystemResourceAdapter {

		/// <summary>
		/// A default instance.
		/// </summary>
		public new static readonly CachingFileSystemResourceAdapter Instance = new CachingFileSystemResourceAdapter();

		private readonly ConcurrentDictionary<string, bool> _exists = new ConcurrentDictionary<string, bool>();
		private readonly ConcurrentDictionary<string, string> _text = new ConcurrentDictionary<string, string>();

		/// <summary>
		/// Insrantiates a new  cachingresource adapter backed by a file-system with the base set to the
		/// current app domain base directory.
		/// </summary>
		public CachingFileSystemResourceAdapter() : base() { }
		/// <summary>
		/// Insrantiates a new  cachingresource adapter backed by a file-system with the base set to the
		/// current app domain base directory.
		/// </summary>
		/// <param name="base">The base directory to use for this adapter.</param>
		public CachingFileSystemResourceAdapter(string @base) : base(@base) {}

		/// <summary>
		/// Determines whether or not the relative path
		/// specified exists.
		/// </summary>
		/// <param name="path">The relative path to check for.</param>
		/// <returns>
		/// Returns true if the resource exists; otherwise, returns false.
		/// </returns>
		/// <remarks>
		/// This method caches the results of previous paths, and does not
		/// invalidate that cache meaning this method is only suitable
		/// for resources that are not going to be added or removed from the file-system.
		/// </remarks>
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
		/// <remarks>
		/// This method caches the results of previous reads, and does not
		/// invalidate that cache meaning this method is only suitable
		/// for static resources.
		/// </remarks>
		public override string ReadAllText(string path) {
			string resolvedPath = this.ResolvePath(path);
			if (!_text.ContainsKey(resolvedPath)) {
				_text[resolvedPath] = File.ReadAllText(resolvedPath);
			}
			return _text[resolvedPath];
		}

	}
}

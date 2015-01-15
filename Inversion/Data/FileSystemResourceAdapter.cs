using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Inversion.Data {
	/// <summary>
	/// Describes basic functionality for reading resources
	/// external to the application stored on the filesystem.
	/// </summary>
	public class FileSystemResourceAdapter : IResourceAdapter {

		/// <summary>
		/// A default instance.
		/// </summary>
		public static readonly FileSystemResourceAdapter Instance = new FileSystemResourceAdapter(AppDomain.CurrentDomain.BaseDirectory);

		private readonly string _base;

		/// <summary>
		/// The base path from which all specified paths are relative.
		/// </summary>
		protected string Base {
			get { return _base; }
		}

		/// <summary>
		/// Instantiates a new resource adapter with the base specified.
		/// </summary>
		/// <param name="base">The base directory to use for this adapter.</param>
		public FileSystemResourceAdapter(string @base) {
			_base = @base;
		}

		/// <summary>
		/// Resolves a provided relative path and returns
		/// an absolute path.
		/// </summary>
		/// <param name="path">The relative path to resolve.</param>
		/// <returns>Returns an absolute path from the relative one.</returns>
		protected string ResolvePath(string path) {
			return Path.Combine(this.Base, path);
		}

		/// <summary>
		/// Determines whether or not the relative path
		/// specified exists.
		/// </summary>
		/// <param name="path">The relative path to check for.</param>
		/// <returns>
		/// Returns true if the resource exists; otherwise, returns false.
		/// </returns>
		public bool Exists(string path) {
			return File.Exists(this.ResolvePath(path));
		}

		public Stream Open(string path) {
			return File.Open(this.ResolvePath(path), FileMode.Open);
		}

		public byte[] ReadAllBytes(string path) {
			return File.ReadAllBytes(this.ResolvePath(path));
		}

		public IEnumerable<string> ReadLines(string path) {
			return File.ReadLines(this.ResolvePath(path));
		}

		public string[] ReadAllLines(string path) {
			return this.ReadLines(path).ToArray();
		}

		public string ReadAllText(string path) {
			return File.ReadAllText(this.ResolvePath(path));
		}



	}
}

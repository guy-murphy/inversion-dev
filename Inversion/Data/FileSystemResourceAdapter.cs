using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Inversion.Data {
	/// <summary>
	/// Provides basic functionality for reading resources
	/// external to the application stored on the filesystem.
	/// </summary>
	public class FileSystemResourceAdapter : IResourceAdapter {

		/// <summary>
		/// A default instance.
		/// </summary>
		public static readonly FileSystemResourceAdapter Instance = new FileSystemResourceAdapter();

		private readonly string _base;

		/// <summary>
		/// The base path from which all specified paths are relative.
		/// </summary>
		protected string Base {
			get { return _base; }
		}

		/// <summary>
		/// Instantiates a new resource adapter backed by a file-system with the base set to the
		/// current app domain base directory.
		/// </summary>
		public FileSystemResourceAdapter() : this(AppDomain.CurrentDomain.BaseDirectory) { }

		/// <summary>
		/// Instantiates a new resource adapter backed by a file-system with the base specified.
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
		public virtual bool Exists(string path) {
			return File.Exists(this.ResolvePath(path));
		}

		/// <summary>
		/// Opens a stream on the resource specified
		/// by the relative path.
		/// </summary>
		/// <param name="path">The relative path to the resource.</param>
		/// <returns>Returns a stream to the specified resource.</returns>
		public Stream Open(string path) {
			return File.Open(this.ResolvePath(path), FileMode.Open);
		}


		/// <summary>
		/// Opens a binary resources, copies the contents to a byte array
		/// and then closes the resource.
		/// </summary>
		/// <param name="path">The relative path to the resource.</param>
		/// <returns>Returns a byte array of the resources contents.</returns>
		public byte[] ReadAllBytes(string path) {
			return File.ReadAllBytes(this.ResolvePath(path));
		}

		/// <summary>
		/// Reads the lines of the specified resource as an enumerable.
		/// </summary>
		/// <param name="path">The relative path to the resource.</param>
		/// <returns>Returns an enumerable of the resources lines.</returns>
		public IEnumerable<string> ReadLines(string path) {
			return File.ReadLines(this.ResolvePath(path));
		}

		/// <summary>
		/// Reads all the lines the the specified resource into
		/// and array.
		/// </summary>
		/// <param name="path">The relative path to the resource.</param>
		/// <returns>Returns a string array with all the lines of the resource.</returns>
		public string[] ReadAllLines(string path) {
			return this.ReadLines(path).ToArray();
		}

		/// <summary>
		/// Opens the specified resource, reads its contents, and
		/// then closes the resource.
		/// </summary>
		/// <param name="path">The relative path to the resource.</param>
		/// <returns>Returns the contents of the resource as text.</returns>
		public virtual string ReadAllText(string path) {
			return File.ReadAllText(this.ResolvePath(path));
		}



	}
}

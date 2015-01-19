using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Inversion.Data {

	/// <summary>
	/// Describes basic functionality for reading resources
	/// external to the application from an assembly.
	/// </summary>
	/// <remarks>
	/// The primary utility for this class is in testing contexts with
	/// behaviours that have expectations of normally file-system resources
	/// to run in the context of a unit test with no file-system.
	/// </remarks>
	public class AssemblyResourceAdapter: IResourceAdapter {


		private readonly Assembly _assembly;
		private readonly string _base;

		/// <summary>
		/// The assembly which this adapter is using
		/// to resolve resources.
		/// </summary>
		protected Assembly Assembly {
			get { return _assembly; }
		}

		/// <summary>
		/// The base path from which all specified paths are relative.
		/// </summary>
		protected string Base {
			get { return _base; }
		}

		/// <summary>
		/// Instantiates a new resource adapter for the assembly specified.
		/// </summary>
		/// <param name="assembly"></param>
		/// <param name="appendToBase">Indicates what if anything should be appended to the base assembly name.</param>
		public AssemblyResourceAdapter(Assembly assembly, string appendToBase = null) {
			_assembly = assembly;
			string assemblyName = this.Assembly.GetName().Name;
			_base = String.IsNullOrEmpty(appendToBase) ? assemblyName : String.Concat(assemblyName, ".", appendToBase);
		}

		/// <summary>
		/// Resolves the path specified into a fully
		/// qualified assembly resource name from the
		/// path specified.
		/// </summary>
		/// <param name="path">The path to resolve as a resource name.</param>
		/// <returns>Returns a fully qualified asembly resource name.</returns>
		protected string ResolvePath(string path) {
			string resolvedPath = String.Concat(this.Base, ".", String.Join(".", path.Split(new string[]{"\\", "/"}, StringSplitOptions.RemoveEmptyEntries)));
			return resolvedPath;
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
			string[] names = this.Assembly.GetManifestResourceNames();
			return names.Contains(this.ResolvePath(path));
		}

		/// <summary>
		/// Opens a stream on the resource specified
		/// by the relative path.
		/// </summary>
		/// <param name="path">The relative path to the resource.</param>
		/// <returns>Returns a stream to the specified resource.</returns>
		public Stream Open(string path) {
			return this.Assembly.GetManifestResourceStream(this.ResolvePath(path));
		}

		/// <summary>
		/// Opens a binary resources, copies the contents to a byte array
		/// and then closes the resource.
		/// </summary>
		/// <param name="path">The relative path to the resource.</param>
		/// <returns>Returns a byte array of the resources contents.</returns>
		public byte[] ReadAllBytes(string path) {
			using (Stream stream = this.Open(path)) {
				byte[] content = new byte[stream.Length];
				stream.Read(content, 0, (int)stream.Length);
				return content;
			}
		}

		/// <summary>
		/// Reads the lines of the specified resource as an enumerable.
		/// </summary>
		/// <param name="path">The relative path to the resource.</param>
		/// <returns>Returns an enumerable of the resources lines.</returns>
		public IEnumerable<string> ReadLines(string path) {
			using (StreamReader reader = new StreamReader(this.Open(path))) {
				string line;
				while ((line = reader.ReadLine()) != null) {
					yield return line;
				}
			}
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
		public string ReadAllText(string path) {
			using (StreamReader reader = new StreamReader(this.Open(path))) {
				return reader.ReadToEnd();
			}
		}
	}
}

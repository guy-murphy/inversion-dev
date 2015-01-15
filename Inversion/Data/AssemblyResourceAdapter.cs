using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inversion.Data {

	/// <summary>
	/// 
	/// </summary>
	public class AssemblyResourceAdapter: IResourceAdapter {
		/// <summary>
		/// Determines whether or not the relative path
		/// specified exists.
		/// </summary>
		/// <param name="path">The relative path to check for.</param>
		/// <returns>
		/// Returns true if the resource exists; otherwise, returns false.
		/// </returns>
		public bool Exists(string path) {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Opens a stream on the resource specified
		/// by the relative path.
		/// </summary>
		/// <param name="path">The relative path to the resource.</param>
		/// <returns>Returns a stream to the specified resource.</returns>
		public Stream Open(string path) {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Opens a binary resources, copies the contents to a byte array
		/// and then closes the resource.
		/// </summary>
		/// <param name="path">The relative path to the resource.</param>
		/// <returns>Returns a byte array of the resources contents.</returns>
		public byte[] ReadAllBytes(string path) {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Reads the lines of the specified resource as an enumerable.
		/// </summary>
		/// <param name="path">The relative path to the resource.</param>
		/// <returns>Returns an enumerable of the resources lines.</returns>
		public IEnumerable<string> ReadLines(string path) {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Reads all the lines the the specified resource into
		/// and array.
		/// </summary>
		/// <param name="path">The relative path to the resource.</param>
		/// <returns>Returns a string array with all the lines of the resource.</returns>
		public string[] ReadAllLines(string path) {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Opens the specified resource, reads its contents, and
		/// then closes the resource.
		/// </summary>
		/// <param name="path">The relative path to the resource.</param>
		/// <returns>Returns the contents of the resource as text.</returns>
		public string ReadAllText(string path) {
			throw new NotImplementedException();
		}
	}
}

using System.IO;

namespace Inversion.Web {
	public interface IRequestFile {
		/// <summary>
		/// The name of the file.
		/// </summary>
		string FileName { get; }

		/// <summary>
		/// The content type for the file.
		/// </summary>
		string ContentType { get; }

		/// <summary>
		/// The actual content of the file.
		/// </summary>
		Stream Content { get; }
	}
}
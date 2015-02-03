using System.IO;

namespace Inversion.Web {
	/// <summary>
	/// Represents a file that was posted as part of a http request.
	/// </summary>
	public class PostedFile : IRequestFile {
		private readonly string _fileName;
		private readonly string _contentType;
		private readonly Stream _content;

		/// <summary>
		/// The name of the file.
		/// </summary>
		public string FileName {
			get { return _fileName; }
		}
		
		/// <summary>
		/// The content type for the file.
		/// </summary>
		public string ContentType {
			get { return _contentType; }
		}

		/// <summary>
		/// The actual content of the file.
		/// </summary>
		public Stream Content {
			get { return _content; }
		}

		/// <summary>
		/// Instantiates a new posted file object.
		/// </summary>
		/// <param name="name">The name of the file.</param>	
		/// <param name="contentType">The content type of this file.</param>
		/// <param name="content">The content for this file.</param>
		public PostedFile(string name, string contentType, Stream content) {
			_fileName = name;
			_contentType = contentType;
			_content = content;
		}

	}
}

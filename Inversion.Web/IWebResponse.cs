using System.IO;

namespace Inversion.Web {
	/// <summary>
	/// Describes a response from a web application.
	/// </summary>
	public interface IWebResponse {
		/// <summary>
		/// The text writer used for writing to the response stream.
		/// </summary>
		TextWriter Output { get; }

		/// <summary>
		/// The response stream.
		/// </summary>
		Stream OutputStream { get; }

		/// <summary>
		/// The status code of the response.
		/// </summary>
		int StatusCode { get; set; }

		/// <summary>
		/// The status description of the response stream.
		/// </summary>
		string StatusDescription { get; set; }

		/// <summary>
		/// The content type of the response stream.
		/// </summary>
		string ContentType { get; set; }

		/// <summary>
		/// Access to the response cookies.
		/// </summary>
		IResponseCookieCollection Cookies { get; }

		/// <summary>
		/// Flushes the response steam and ends the response.
		/// </summary>
		void End();

		/// <summary>
		/// Writes the provided text to the response stream.
		/// </summary>
		/// <param name="text">The text to write to the response stream.</param>
		void Write(string text);

		/// <summary>
		/// Writes the provided formatted text to the response stream.
		/// </summary>
		/// <param name="text">The text to write to the response stream.</param>
		/// <param name="args">The arguments to interpolate into the text.</param>
		void WriteFormat(string text, params object[] args);

		/// <summary>
		/// Redirects the request to the provided url.
		/// </summary>
		/// <param name="url">The url to redirect to.</param>
		void Redirect(string url);

		/// <summary>
		/// Redirects the request permanently to the provided url
		/// issuing a `301` in the response.
		/// </summary>
		/// <param name="url"></param>
		void PermanentRedirect(string url);
	}
}
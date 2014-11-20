using System;
using System.Runtime.Serialization;

namespace Inversion.Process {
	/// <summary>
	/// An exception that is thrown when a problem is encountered
	/// in the Inversion processing model.
	/// </summary>
	[Serializable]
	public class ProcessException : ApplicationException {
		/// <summary>
		/// Instantiates a new process exception with the message provided.
		/// </summary>
		/// <param name="message">A simple human readable message that summarises this exceptions cause.</param>
		public ProcessException(string message) : base(message) { }
		/// <summary>
		/// instantiates a new process exception with the details needed to handle
		/// serialisation and deserialisation.
		/// </summary>
		/// <param name="info">The info needed to handle the serialisation of this exception.</param>
		/// <param name="context">The context used to manage the serialisation stream.</param>
		private ProcessException(SerializationInfo info, StreamingContext context) : base(info, context) { }

	}
}

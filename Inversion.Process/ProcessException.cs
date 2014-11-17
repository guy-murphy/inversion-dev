using System;
using System.Runtime.Serialization;

namespace Inversion.Process {
	[Serializable]
	public class ProcessException : ApplicationException {
		public ProcessException(string message) : base(message) { }
		private ProcessException(SerializationInfo info, StreamingContext context) : base(info, context) { }

	}
}

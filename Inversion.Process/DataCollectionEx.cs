using Inversion.Collections;

namespace Inversion.Process {

	/// <summary>
	/// Extension methods acting upon `IDataCollection{ErrorMessage}` objects.
	/// </summary>
	public static class DataCollectionEx {

		/// <summary>
		/// Creates a new error message and adds it to the collection.
		/// </summary>
		/// <param name="self">The collection to add the message to.</param>
		/// <param name="message">The human readable error message.</param>
		/// <returns>Returns the error message object that was created.</returns>
		public static ErrorMessage CreateMessage(this IDataCollection<ErrorMessage> self, string message) {
			ErrorMessage msg = new ErrorMessage(message);
			self.Add(msg);
			return msg;
		}

		/// <summary>
		/// Creates a new error message and adds it to the collection.
		/// </summary>
		/// <param name="self">The collection to add the message to.</param>
		/// <param name="message">The human readable error message as text for string formatting.</param>
		/// <param name="parms">Paramters for formatting the message text.</param>
		/// <returns>Returns the error message object that was created.</returns>
		public static ErrorMessage CreateMessage(this IDataCollection<ErrorMessage> self, string message, params object[] parms) {
			ErrorMessage msg = new ErrorMessage(string.Format(message, parms));
			self.Add(msg);
			return msg;
		}
	}
}

using Inversion.Collections;

namespace Inversion.Process {
	public static class DataCollectionEx {
		public static ErrorMessage CreateMessage(this IDataCollection<ErrorMessage> self, string message) {
			ErrorMessage msg = new ErrorMessage(message);
			self.Add(msg);
			return msg;
		}

		public static ErrorMessage CreateMessage(this IDataCollection<ErrorMessage> self, string message, params object[] parms) {
			ErrorMessage msg = new ErrorMessage(string.Format(message, parms));
			self.Add(msg);
			return msg;
		}
	}
}

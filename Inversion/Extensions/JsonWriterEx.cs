using Newtonsoft.Json;

namespace Inversion.Extensions {

	/// <summary>
	/// Conventient extension methods for the json writer.
	/// </summary>
	public static class JsonWriterEx {

		/// <summary>
		/// Writes both a json property name, and a value at the same time.
		/// </summary>
		/// <param name="self">The json writer to act upon.</param>
		/// <param name="name">The name of the property.</param>
		/// <param name="value">The value of the property.</param>
		public static void WriteProperty(JsonWriter self, string name, string value) {
			self.WritePropertyName(name);
			self.WriteValue(value);
		}

	}
}

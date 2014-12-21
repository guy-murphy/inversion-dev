using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Inversion.Extensions {
	public static class JsonWriterEx {

		public static void WriteProperty(JsonWriter self, string name, string value) {
			self.WritePropertyName(name);
			self.WriteValue(value);
		}

	}
}

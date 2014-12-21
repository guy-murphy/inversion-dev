using Newtonsoft.Json.Linq;

namespace Inversion {
	public interface IConsumeData<out TBuilder, TConcrete> {
		TBuilder FromConcrete(TConcrete other);
		TBuilder FromJson(JObject json);
		TConcrete ToConcrete();
	}
}

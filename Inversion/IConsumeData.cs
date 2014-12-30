using Newtonsoft.Json.Linq;

namespace Inversion {
	/// <summary>
	/// Expresses a type that is able to consume both json and
	/// xml representations of itself.
	/// </summary>
	/// <typeparam name="TBuilder">The type of the builder being used for this type.</typeparam>
	/// <typeparam name="TConcrete">The type of the concrete product of consuming data.</typeparam>
	public interface IConsumeData<out TBuilder, TConcrete> {
		TBuilder FromConcrete(TConcrete other);
		TBuilder FromJson(JObject json);
		TConcrete ToConcrete();
	}
}

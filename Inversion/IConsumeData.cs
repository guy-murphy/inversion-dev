using Newtonsoft.Json.Linq;

namespace Inversion {
	/// <summary>
	/// Expresses a type that is able to consume both json and
	/// xml representations of itself.
	/// </summary>
	/// <typeparam name="TBuilder">The type of the builder being used for this type.</typeparam>
	/// <typeparam name="TConcrete">The type of the concrete product of consuming data.</typeparam>
	public interface IConsumeData<out TBuilder, TConcrete> {
		/// <summary>
		/// Assigns values to this object based on those
		/// of the other object provided.
		/// </summary>
		/// <param name="other">The other object from which to take values.</param>
		/// <returns>Returns a builder populated from the provided concrete object.</returns>
		TBuilder FromConcrete(TConcrete other);
		/// <summary>
		/// Assigns values to this object based on those
		/// in the json provided.
		/// </summary>
		/// <param name="json">The property set from which to take values.</param>
		/// <returns>Returns a builded populated from the json provided.</returns>
		TBuilder FromJson(JObject json);
		/// <summary>
		/// Produced a concrete object from this one.
		/// </summary>
		/// <returns>Returns a concrete object from this one.</returns>
		TConcrete ToConcrete();
	}
}

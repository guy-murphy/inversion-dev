using System;

namespace Inversion {
	
	/// <summary>
	/// Describes a type that manages mutation via a `Mutate` function
	/// using a builder object as an intermediary on which to exercise mutation.
	/// </summary>
	/// <typeparam name="TBuilder">The type of the builder that will be used for mutation.</typeparam>
	/// <typeparam name="TConcrete">The type of the concrete object to be created.</typeparam>
	public interface IMutate<out TBuilder, TConcrete> {
		/// <summary>
		/// Mutates the current object by transforming it to a builder,
		/// applying a mutation function to the builder, and then transforming
		/// the builder back to a specified concrete type.
		/// </summary>
		/// <param name="mutator">The mutation function to apply to the builder.</param>
		/// <returns>
		/// Returns a concrete object which is the product of a builder that has had
		/// the mutation function applied to it.
		/// </returns>
		TConcrete Mutate(Func<TBuilder, TConcrete> mutator);
	}
}

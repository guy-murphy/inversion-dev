using System;

namespace Inversion {
	
	public interface IMutate<out TBuilder, TConcrete> {
		
		TConcrete Mutate(Func<TBuilder, TConcrete> mutator);
	}
}

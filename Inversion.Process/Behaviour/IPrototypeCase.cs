using System;

namespace Inversion.Process.Behaviour {
	/// <summary>
	/// Describes a case of selection criteria that specified a match against
	/// a configuration to determine if this selection criteria is relevant to a behaviour.
	/// </summary>
	public interface IPrototypeCase {
		/// <summary>
		/// The match used to determine if this selection criteria are relevant.
		/// </summary>
		Predicate<IConfiguration> Match { get; }
		/// <summary>
		/// The selection criteria to be used by a behaviour in its condition.
		/// </summary>
		SelectionCriteria Criteria { get; }
	}
}
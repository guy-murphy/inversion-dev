using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inversion.Process.Behaviour {
	/// <summary>
	/// Describes a method that acts upon a configuration and an event
	/// to determine if they meet selection criteria and returns true if they do.
	/// </summary>
	/// <param name="config">The configuration to act upon.</param>
	/// <param name="ev">The event to act upon.</param>
	/// <returns>
	/// Returns true if the selection criteria are met, otherwise returns false.
	/// </returns>
	public delegate bool SelectionCriteria(IConfiguration config, IEvent ev);
}

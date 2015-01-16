using System;
using System.Collections.Generic;
using System.Linq;

namespace Inversion.Process {
	/// <summary>
	/// Extension methods for `IProcessContext` concerned with
	/// performing checks upon that context.
	/// </summary>
	public static class ProcessContextEx {

		/// <summary>
		/// Determines whether or not the flag of the
		/// specified key exists.
		/// </summary>
		/// <param name="flag">The key of the flag to check for.</param>
		/// <param name="self">The context being acted upon.</param>
		/// <returns>Returns true if the flag exists; otherwise returns false.</returns>
		public static bool IsFlagged(this IProcessContext self, string flag) {
			return self.Flags.Contains(flag);
		}

		/// <summary>
		/// Determines whether or not the parameters 
		/// specified exist in the current context.
		/// </summary>
		/// <param name="parms">The parameters to check for.</param>
		/// <param name="self">The context being acted upon.</param>
		/// <returns>Returns true if all the parameters exist; otherwise return false.</returns>
		public static bool HasParams(this IProcessContext self, params string[] parms) {
			return parms.Length > 0 && parms.All(parm => self.Params.ContainsKey(parm));
		}

		/// <summary>
		/// Determines whether or not the parameters 
		/// specified exist in the current context.
		/// </summary>
		/// <param name="parms">The parameters to check for.</param>
		/// <param name="self">The context being acted upon.</param>
		/// <returns>Returns true if all the parameters exist; otherwise return false.</returns>
		public static bool HasParams(this IProcessContext self, IEnumerable<string> parms) {
			return parms != null && parms.All(parm => self.Params.ContainsKey(parm));
		}

		/// <summary>
		/// Determines whether or not the parameter name and
		/// value specified exists in the current context.
		/// </summary>
		/// <param name="name">The name of the parameter to check for.</param>
		/// <param name="value">The value of the parameter to check for.</param>
		/// <param name="self">The context being acted upon.</param>
		/// <returns>
		/// Returns true if a parameter with the name and value specified exists
		/// in this conext; otherwise returns false.
		/// </returns>
		public static bool HasParamValue(this IProcessContext self, string name, string value) {
			return self.Params.ContainsKey(name) && self.Params[name] == value;
		}

		/// <summary>
		/// Determines whether or not all the key-value pairs
		/// provided exist in the contexts parameters.
		/// </summary>
		/// <param name="match">The key-value pairs to check for.</param>
		/// <param name="self">The context being acted upon.</param>
		/// <returns>
		/// Returns true if all the key-value pairs specified exists in the contexts
		/// parameters; otherwise returns false.
		/// </returns>
		public static bool HasParamValues(this IProcessContext self, IEnumerable<KeyValuePair<string, string>> match) {
			return match != null && match.All(entry => self.Params.Contains(entry));
		}

		/// <summary>
		/// Determines whether or not any of the key-value pairs
		/// provided exist in the contexts parameters.
		/// </summary>
		/// <param name="match">The key-value pairs to check for.</param>
		/// <param name="self">The context being acted upon.</param>
		/// <returns>
		/// Returns true if any of the key-value pairs specified exists in the contexts
		/// parameters; otherwise returns false.
		/// </returns>
		public static bool HasAnyParamValues(this IProcessContext self, IEnumerable<KeyValuePair<string, string>> match) {
			return match != null && match.Any(entry => self.Params.Contains(entry));
		}

		/// <summary>
		/// Determines whether or not all any of the values for their associated parameter name
		/// exist in the contexts parameters.
		/// </summary>
		/// <param name="match">The possible mapped values to match against.</param>
		/// <param name="self">The context being acted upon.</param>
		/// <returns>
		/// Returns if each of the keys has at least one value that exists for the conext;
		/// otherwise, returns false.
		/// </returns>
		public static bool HasParamValues(this IProcessContext self, IEnumerable<KeyValuePair<string, IEnumerable<string>>> match) {
			return match != null && match.All(entry => self.Params.ContainsKey(entry.Key) && entry.Value.Any(value => value == self.Params[entry.Key]));
		}

		/// <summary>
		/// Determines whether or not the specified
		/// paramters exist in this context, and produces
		/// and error for each one that does not exist.
		/// </summary>
		/// <param name="parms">The parameter keys to check for.</param>
		/// <param name="self">The context being acted upon.</param>
		/// <returns>Returns true if all the paramter keys are present; otherwise returns false.</returns>
		public static bool HasRequiredParams(this IProcessContext self, params string[] parms) {
			bool has = parms.Length > 0;
			foreach (string parm in parms) {
				if (!self.Params.ContainsKey(parm)) {
					has = false;
					self.Errors.CreateMessage(String.Format("The parameter '{0}' is required and was not present.", parm));
				}
			}
			return has;
		}

		/// <summary>
		/// Dtermines whether or not the control state has entries indexed
		/// under the keys provided.
		/// </summary>
		/// <param name="parms">The keys to check for in the control state.</param>
		/// <param name="self">The context being acted upon.</param>
		/// <returns>
		/// Returns true if all the specified keys exist in the control state;
		/// otherwise returns false.
		/// </returns>
		public static bool HasControlState(this IProcessContext self, params string[] parms) {
			return parms.Length > 0 && parms.All(parm => self.ControlState.Keys.Contains(parm));
		}

		/// <summary>
		/// Dtermines whether or not the control state has entries indexed
		/// under the keys provided.
		/// </summary>
		/// <param name="parms">The keys to check for in the control state.</param>
		/// <param name="self">The context being acted upon.</param>
		/// <returns>
		/// Returns true if all the specified keys exist in the control state;
		/// otherwise returns false.
		/// </returns>
		public static bool HasControlState(this IProcessContext self, IEnumerable<string> parms) {
			return parms != null && parms.All(parm => self.ControlState.Keys.Contains(parm));
		}

		/// <summary>
		/// Dtermines whether or not the control state has entries indexed
		/// under the keys provided, and creates an error for each one that doesn't.
		/// </summary>
		/// <param name="parms">The keys to check for in the control state.</param>
		/// <param name="self">The context being acted upon.</param>
		/// <returns>
		/// Returns true if all the specified keys exist in the control state;
		/// otherwise returns false.
		/// </returns>
		public static bool HasRequiredControlState(this IProcessContext self, params string[] parms) {
			bool has = parms.Length > 0;
			foreach (string parm in parms) {
				if (!self.ControlState.ContainsKey(parm)) {
					has = false;
					self.Errors.CreateMessage(String.Format("The control state '{0}' is required and was not present.", parm));
				}
			}
			return has;
		}

		/// <summary>
		/// Obtains the context prarameter for the specified
		/// key, or if it doesn't exist uses the default value specified.
		/// </summary>	
		/// <param name="key">The key of the context parameter to use.</param>
		/// <param name="defaultValue">The value to use if the parameter doesn't exist.</param>
		/// <param name="self">The context being acted upon.</param>
		/// <returns>Returns the specified parameter if it exists; otherwise returns false.</returns>
		public static string ParamOrDefault(this IProcessContext self, string key, string defaultValue) {
			return self.Params.ContainsKey(key) ? self.Params[key] : defaultValue;
		}


	}
}

﻿using System.Collections.Generic;

namespace Inversion.Web
{
    /// <summary>
    /// Describes a collection of request cookies.
    /// </summary>
    public interface IRequestCookieCollection : IEnumerable<KeyValuePair<string, string>>
    {
        /// <summary>
        /// Provides access to request cookies by key index.
        /// </summary>
        /// <param name="key">The key of the cookie.</param>
        /// <returns>Returns the cookie under the specified key.</returns>
        string this[string key] { get; }
        /// <summary>
        /// Gets the value of the cookie of the specified key.
        /// </summary>
        /// <param name="key">The key of the cookie.</param>
        /// <returns>Returns the value of the cookie under the specified key.</returns>
        string Get(string key);
        /// <summary>
        /// Tries to get the value of the cookie of the specified key.
        /// </summary>
        /// <param name="key">The key of the cookie.</param>
        /// <param name="value">The string reference any found cookie value should be assigned to.</param>
        /// <returns>Returns true if the specified cookie was found; otherwise, returns false.</returns>
        bool TryGetValue(string key, out string value);

        /// <summary>
        /// Get the list of cookie contents as a dictionary.
        /// </summary>
        /// <param name="key">The cookie key</param>
        /// <returns>Returns a dictionary representation of the cookie contents (name=value style).</returns>
        IDictionary<string, string> GetValues(string key);
        /// <summary>
        /// Tries to get the list of cookie contents as a dictionary.
        /// </summary>
        /// <param name="key">The cookie key</param>
        /// <param name="values">The dictionary reference that will have the result assigned to.</param>
        /// <returns>Returns true if the specified cookie was found; otherwise, returns false.</returns>
        bool TryGetValues(string key, out IDictionary<string, string> values);
    }
}
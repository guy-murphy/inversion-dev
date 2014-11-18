using System;
using System.Text.RegularExpressions;

using Inversion.Collections;

namespace Inversion.Web {

	/// <summary>
	/// Represent the structure of a url.
	/// </summary>
	public class UrlInfo : ICloneable {

		private const string DefaultRegexSpec = @"
(?<protocol>http|ftp|https|ftps)://
(?<domain>[^/\r\n\?]+)
(?<path>
    (?<app_path>/[^\r\n\?]*)*
    /
    (?<file>
            (?<resource>[^\#\r\n\?]+)
            \.
            (?<extension>[^\#\r\n\?\./]+)
    )
    (?<tail>/[^\#\r\n\?]*)?
)?
(?<anchor>\#[^\#\r\n\?]*)?
(?:\?)?
(?<query>[^\#\r\n]*)?
";

		/// <summary>
		/// The default regex that is used to deconstruct urls.
		/// </summary>
		public static readonly Regex DefaultRegex = new Regex(
			DefaultRegexSpec,
			RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace |
			RegexOptions.Compiled
		);

		private readonly string _url;
		private readonly Regex _regex;
		private readonly DataDictionary<string> _query;
		private Match _match;

		/// <summary>
		/// The regular expression being used to break URLs
		/// down into their parts.
		/// </summary>
		/// <remarks>
		/// If a regular expression isn't provided via the contructor
		/// then the default <see cref="UrlInfo.DefaultRegex"/> is used.
		/// </remarks>
		public Regex Regex {
			get { return _regex ?? UrlInfo.DefaultRegex; }
		}

		/// <summary>
		/// The matches produced by matching the
		/// <see cref="Regex"/> against the <see cref="Url"/>.
		/// </summary>
		public Match Match {
			get {
				if (_match == null) {
					this.ProcessUrl();
				}
				return _match;
			}
		}

		/// <summary>
		/// The url being processed.
		/// </summary>
		public string Url {
			get { return _url; }
		}

		/// <summary>
		/// The protocol specified by the <see cref="Url"/>
		/// </summary>
		public string Protocol {
			get {
				return this.Match.Groups["protocol"].Value;
			}
		}

		/// <summary>
		/// The domain specified by the <see cref="Url"/>
		/// </summary>
		public string Domain {
			get {
				return this.Match.Groups["domain"].Value;
			}
		}

		/// <summary>
		/// The full path specified by the <see cref="Url"/>
		/// </summary>
		public string FullPath {
			get {
				return this.Match.Groups["path"].Value;
			}
		}

		/// <summary>
		/// The application path specified by the <see cref="Url"/>
		/// </summary>
		public string AppPath {
			get {
				return this.Match.Groups["app_path"].Value;
			}
		}

		/// <summary>
		/// The URL of the current application.
		/// </summary>
		public string AppUrl {
			get {
				return String.Concat(this.Protocol, "://", this.Domain, this.AppPath);
			}
		}

		/// <summary>
		/// The file specified by the <see cref="Url"/>
		/// </summary>
		public string File {
			get {
				return this.Match.Groups["file"].Value;
			}
		}

		/// <summary>
		/// The extension specified by the <see cref="Url"/>
		/// </summary>
		public string Extension {
			get {
				return this.Match.Groups["extension"].Value;
			}
		}

		/// <summary>
		/// The tail specified by the <see cref="Url"/>
		/// </summary>
		public string Tail {
			get {
				return this.Match.Groups["tail"].Value;
			}
		}

		/// <summary>
		/// The query string specified by the <see cref="Url"/>
		/// </summary>

		public string QueryString {
			get {
				return this.Match.Groups["query"].Value;
			}
		}

		/// <summary>
		/// A name / value dictionary as the propduct of
		/// parsing the <see cref="QueryString"/>
		/// </summary>
		public DataDictionary<string> Query {
			get {
				return _query;
			}
		}

		/// <summary>
		/// Instantiates a new url-info object from the uri object provided.
		/// </summary>
		/// <param name="uri">The uri object to contrsut the url-info from.</param>
		public UrlInfo(Uri uri) : this(uri, null) { }
		/// <summary>
		/// Instantiates a new url-info object from the url string representation provided.
		/// </summary>
		/// <param name="url">The url to construct the url-info from.</param>
		public UrlInfo(string url) : this(url, null) { }
		/// <summary>
		/// Instantiates a new url-info object from the uri provided
		/// using the regex provided to deconstruct it.
		/// </summary>
		/// <param name="uri">The uri object to contrsut the url-info from.</param>
		/// <param name="regex">The regex to use in deconstructing the uri.</param>
		public UrlInfo(Uri uri, Regex regex) : this(uri.ToString(), regex) { }
		/// <summary>
		/// Instantiates a new url-info object from the url string representation provided,
		/// using the regex provided to deconstruct it.
		/// </summary>
		/// <param name="url">The url to construct the url-info from.</param>
		/// <param name="regex">The regex to use in deconstructing the uri.</param>
		public UrlInfo(string url, Regex regex) {
			_url = url;
			_regex = regex;
		}

		/// <summary>
		/// Instantiates a url-info object as a copy
		/// of the url-info object provided.
		/// </summary>
		/// <param name="info">The url-info obect to create a copy from.</param>
		public UrlInfo(UrlInfo info) {
			_url = info.Url;
			_regex = info.Regex;
			_query = new DataDictionary<string>();
		}

		/// <summary>
		/// Processes the url with a deconstruction regex.
		/// </summary>
		public void ProcessUrl() {
			_match = this.Regex.Match(_url);
			if (!String.IsNullOrWhiteSpace(this.QueryString)) {

			}
		}

		object ICloneable.Clone() {
			return new UrlInfo(this);
		}

		/// <summary>
		/// Instantiates a new url-info object that is a copy
		/// of the current instance.
		/// </summary>
		/// <returns></returns>
		public UrlInfo Clone() {
			return new UrlInfo(this);
		}
	}
}

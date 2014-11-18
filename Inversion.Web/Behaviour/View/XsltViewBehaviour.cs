using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Caching;
using System.Xml;
using System.Xml.Xsl;

using Inversion.Process;

namespace Inversion.Web.Behaviour.View {

	/// <summary>
	/// A behaviour that will transform the last view step by attempting to find
	/// an appropriate XSL style sheet, based upon the context params
	/// of *area*, *concern*, and *action*. 
	/// </summary>
	/// <remarks>
	/// This is intended for use in Web application, not as a general
	/// purpose XSL transform.
	/// </remarks>

	public class XsltViewBehaviour : WebBehaviour {

		// This is a piece of voodoo I was handed by a friend who had some
		//		similar occasional encoding problems which apparently are
		//		due to an underlying problem in the BCL.
		// Find out if its still relevant and what exactly its doing.

		private class StringWriterWithEncoding : StringWriter {
			private readonly Encoding _encoding;

			public StringWriterWithEncoding(StringBuilder builder, Encoding encoding)
				: base(builder) {
				_encoding = encoding;
			}

			public override Encoding Encoding {
				get {
					return _encoding;
				}
			}
		}

		private readonly string _contentType;
		private readonly bool _enableCache;


		/// <summary>
		/// Instantiates a new xslt view behaviour used to provide xslt templating
		/// primarily for web applications.
		/// </summary>
		/// <param name="message">The message the behaviour has set as responding to.</param>
		/// <remarks>
		/// Defaults to caching compiled xslt, to a content type of "text/xml".
		/// </remarks>
		public XsltViewBehaviour(string message) : this(message, "text/xml") { }

		/// <summary>
		/// Instantiates a new xslt view behaviour used to provide xslt templating
		/// primarily for web applications.
		/// </summary>
		/// <param name="message">The message the behaviour has set as responding to.</param>
		/// <param name="contentType">The content type of the view step produced from this behaviour.</param>
		/// <remarks>
		/// Defaults to caching compiled xslt.
		/// </remarks>
		public XsltViewBehaviour(string message, string contentType)
			: base(message) {
			_contentType = contentType;
			_enableCache = true;
		}

		/// <summary>
		/// Instantiates a new xslt view behaviour used to provide xslt templating
		/// primarily for web applications.
		/// </summary>
		/// <param name="message">The message the behaviour has set as responding to.</param>
		/// <param name="contentType">The content type of the view step produced from this behaviour.</param>
		/// <param name="enableCache">Specifies whether or not the xslt compilation should be cached.</param>
		public XsltViewBehaviour(string message, string contentType, bool enableCache)
			: this(message, contentType) {
			_enableCache = enableCache;
		}

		/// <summary>
		/// Instantiates a new xslt view behaviour used to provide xslt templating
		/// primarily for web applications.
		/// </summary>
		/// <param name="message">The message the behaviour has set as responding to.</param>
		/// <param name="enableCache">Specifies whether or not the xslt compilation should be cached.</param>
		/// <remarks>Defaults to a content type of "text/xml".</remarks>
		public XsltViewBehaviour(string message, bool enableCache)
			: this(message) {
			_enableCache = enableCache;
		}

		// TODO: confirm thread safe
		// The iterator generated for this should be
		//		ThreadLocal and therefore safe to use
		//		in this manner on a singleton, would be
		//		nice to fonfirm this.
		// At some point this will need to move to being
		// and injected strategy.
		private IEnumerable<string> _possibleTemplates(WebContext context) {
			string area = context.Params["area"];
			string concern = context.Params["concern"];
			string action = String.Format("{0}.xslt", context.Params["action"]);

			// area/concern/action
			yield return Path.Combine(area, concern, action);
			yield return Path.Combine(area, concern, "default.xslt");
			// area/action
			yield return Path.Combine(area, action);
			yield return Path.Combine(area, "default.xslt");
			// concern/action
			yield return Path.Combine(concern, action);
			yield return Path.Combine(concern, "default.xslt");
			// action
			yield return action;
			yield return "default.xslt";
		}

		/// <summary>
		/// Takes the content of the last view-step and transforms it with the xslt with the location
		/// that best matches the path of the url. 
		/// </summary>
		/// <remarks>
		/// The locations checked are produced by the following series of yields:-
		/// <code>
		///		//area/concern/action
		///		yield return Path.Combine(area, concern, action);
		///		yield return Path.Combine(area, concern, "default.xslt");
		///		// area/action
		///		yield return Path.Combine(area, action);
		///		yield return Path.Combine(area, "default.xslt");
		///		// concern/action
		///		yield return Path.Combine(concern, action);
		///		yield return Path.Combine(concern, "default.xslt");
		///		// action
		///		yield return action;
		///		yield return "default.xslt"; 
		/// </code>
		/// </remarks>
		/// <param name="ev">The event that gave rise to this action.</param>
		/// <param name="context">The context within which this action is being performed.</param>
		public override void Action(IEvent ev, WebContext context) {
			if (context.ViewSteps.HasSteps && context.ViewSteps.Last.HasContent || context.ViewSteps.Last.HasModel) {

				foreach (string templateName in _possibleTemplates(context)) {

					// check if we have the template cached
					string cacheKey = String.Concat("xsl::", templateName);
					XslCompiledTransform xsl = (_enableCache && context.Flags.Contains("nocache")) ? null : context.Cache.Get(cacheKey) as XslCompiledTransform;
					if (xsl == null) {
						// we dont have it cached
						// does the file exist?
						string templatePath = Path.Combine(context.Application.BaseDirectory, "Resources", "Views", "Xsl", templateName);
						if (File.Exists(templatePath)) {
							xsl = new XslCompiledTransform(true);
							using (XmlReader reader = new XmlTextReader(templatePath)) {
								xsl.Load(reader);
								if (_enableCache) {
									context.Cache.Add(cacheKey, xsl, new CacheDependency(templatePath), Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0), CacheItemPriority.High, null);
								}
							}
						}
					}
					// do the actual transform
					if (xsl != null) {
						// copy forward the parameters from the context
						// to the xsl stylesheet, with no namespace
						XsltArgumentList args = new XsltArgumentList();
						foreach (KeyValuePair<string, string> parm in context.Params) {
							args.AddParam(parm.Key, "", parm.Value);
						}

						StringBuilder result = new StringBuilder();
						XmlDocument input = new XmlDocument();
						// this needs tidied up						
						string inputText = context.ViewSteps.Last.Content ?? context.ViewSteps.Last.Model.ToXml();
						input.LoadXml(inputText);
						xsl.Transform(input, args, new StringWriterWithEncoding(result, Encoding.UTF8));
						context.ViewSteps.CreateStep(templateName, _contentType, result.ToString());
						break;
					}
				}

			}
		}
	}
}

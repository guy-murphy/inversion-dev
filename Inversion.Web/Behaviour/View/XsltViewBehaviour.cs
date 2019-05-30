using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

using Inversion.Data;
using Inversion.Process;
using Inversion.Process.Behaviour;
using log4net;

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

	public class XsltViewBehaviour : ProcessBehaviour {

	    private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
	    private readonly bool _disableDirective;
	    private readonly bool _diagnostics;

		/// <summary>
		/// Instantiates a new xslt view behaviour used to provide xslt templating
		/// primarily for web applications.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour has set as responding to.</param>
		/// <remarks>
		/// Defaults to caching compiled xslt, to a content type of "text/xml".
		/// </remarks>
		public XsltViewBehaviour(string respondsTo) : this(respondsTo, "text/xml") { }

	    /// <summary>
	    /// Instantiates a new xslt view behaviour used to provide xslt templating
	    /// primarily for web applications.
	    /// </summary>
	    /// <param name="respondsTo">The message the behaviour has set as responding to.</param>
	    /// <param name="enableCache">Specifies whether or not the xslt compilation should be cached.</param>
	    /// <remarks>Defaults to a content type of "text/xml".</remarks>
	    public XsltViewBehaviour(string respondsTo, bool enableCache)
	        : this(respondsTo)
	    {
	        _enableCache = enableCache;
	    }

        /// <summary>
        /// Instantiates a new xslt view behaviour used to provide xslt templating
        /// primarily for web applications.
        /// </summary>
        /// <param name="respondsTo">The message the behaviour has set as responding to.</param>
        /// <param name="contentType">The content type of the view step produced from this behaviour.</param>
        /// <param name="diagnostics">Add diagnostics log output</param>
        /// <remarks>
        /// Defaults to caching compiled xslt.
        /// </remarks>
        public XsltViewBehaviour(string respondsTo, string contentType, bool diagnostics = false)
			: base(respondsTo) {
			_contentType = contentType;
			_enableCache = true;
            _diagnostics = diagnostics;
		}

		/// <summary>
		/// Instantiates a new xslt view behaviour used to provide xslt templating
		/// primarily for web applications.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour has set as responding to.</param>
		/// <param name="contentType">The content type of the view step produced from this behaviour.</param>
		/// <param name="enableCache">Specifies whether or not the xslt compilation should be cached.</param>
		public XsltViewBehaviour(string respondsTo, string contentType, bool enableCache, bool diagnostics = false)
			: this(respondsTo, contentType) {
			_enableCache = enableCache;
		    _diagnostics = diagnostics;
        }

        /// <summary>
		/// Instantiates a new xslt view behaviour used to provide xslt templating
		/// primarily for web applications.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour has set as responding to.</param>
		/// <param name="contentType">The content type of the view step produced from this behaviour.</param>
		/// <param name="enableCache">Specifies whether or not the xslt compilation should be cached.</param>
		/// <param name="disableDirective">Specifies whether or not to disable the xml directive.</param>
		public XsltViewBehaviour(string respondsTo, string contentType, bool enableCache, bool disableDirective, bool diagnostics = false)
            : this(respondsTo, contentType)
        {
            _enableCache = enableCache;
            _disableDirective = disableDirective;
            _diagnostics = diagnostics;
        }

		// The iterator generated for this should be
		//		ThreadLocal and therefore safe to use
		//		in this manner on a singleton, would be
		//		nice to fonfirm this.
		// At some point this will need to move to being
		// and injected strategy.
		private IEnumerable<string> _possibleTemplates(IProcessContext context) {
			string area = context.Params["area"];
			string concern = context.Params["concern"];
			string action = String.Format("{0}.xslt", context.Params["action"]);

			// area/concern/action
			yield return Path.Combine(area, concern, action);
			// area/concern/default
			yield return Path.Combine(area, concern, "default.xslt");
			// area/action
			yield return Path.Combine(area, action);
			// area/default
			yield return Path.Combine(area, "default.xslt");
			// concern/action
			yield return Path.Combine(concern, action);
			// concern/default
			yield return Path.Combine(concern, "default.xslt");
			// action
			yield return action;
			// default
			yield return "default.xslt";
		}

		/// <summary>
		/// Takes the content of the last view-step and transforms it with the xslt with the location
		/// that best matches the path of the url.
		/// </summary>
		/// <param name="ev">The event that gave rise to this action.</param>
		/// <param name="context">The context within which this action is being performed.</param>
		public override void Action(IEvent ev, IProcessContext context) {
			if (context.ViewSteps.HasSteps && context.ViewSteps.Last.HasContent || context.ViewSteps.Last.HasModel) {

				foreach (string templateName in _possibleTemplates(context)) {

					// check if we have the template cached
					string cacheKey = String.Concat("xsl::", templateName);
					XslCompiledTransform xsl = null;

					if ( _enableCache && !context.IsFlagged("nocache") ) {
						object cacheEntry = null;
						context.ObjectCache.TryGetValue(cacheKey, out cacheEntry);
						xsl = cacheEntry as XslCompiledTransform;
					}

					if (xsl == null) {
						// we dont have it cached
						// does the file exist?
						string templatePath = Path.Combine("Resources", "Views", "Xslt", templateName);

					    if (_diagnostics)
					    {
					        _log.DebugFormat("Checking path {0}", templatePath);
					    }

						if (context.Resources.Exists(templatePath)){
						    if (_diagnostics)
						    {
						        _log.DebugFormat("Path {0} exists", templatePath);
						    }

						    xsl = new XslCompiledTransform(enableDebug: false);
						    xsl.Load(templatePath);
                            xsl = context.Resources.Open(templatePath).AsXslDocument();
							if (_enableCache) {
								var cacheEntry = context.ObjectCache.CreateEntry(cacheKey);
								cacheEntry.Value = xsl;
							}
						}
					}
					// do the actual transform
					if (xsl != null) {
					    if (_diagnostics)
					    {
					        _log.DebugFormat("XSL loaded");
					    }
                        // copy forward the parameters from the context
                        // to the xsl stylesheet, with no namespace
                        XsltArgumentList args = new XsltArgumentList();
						foreach (KeyValuePair<string, string> parm in context.Params) {
							args.AddParam(parm.Key, "", parm.Value);
						}

					    try
					    {
					        StringBuilder result = new StringBuilder();
					        XmlDocument input = new XmlDocument();

					        string inputText = context.ViewSteps.Last.Content ?? context.ViewSteps.Last.Model.ToXml();
					        input.LoadXml(inputText);


					        if (_disableDirective)
					        {
					            XmlWriterSettings writerSettings = new XmlWriterSettings
					            {
					                OmitXmlDeclaration = true,
					                ConformanceLevel = ConformanceLevel.Auto
					            };
					            using (XmlWriter writer = XmlWriter.Create(result, writerSettings))
					            {
					                xsl.Transform(input, args, writer);
					                if (_diagnostics)
					                {
					                    _log.DebugFormat("Transformed");
					                }
					            }
					        }
					        else
					        {
					            using (StringWriterWithEncoding writer = new StringWriterWithEncoding(result, Encoding.UTF8))
					            {
					                xsl.Transform(input, args, writer);
					                if (_diagnostics)
					                {
					                    _log.DebugFormat("Transformed");
					                }
					            }
					        }

					        context.ViewSteps.CreateStep(templateName, _contentType, result.ToString());
					        if (_diagnostics)
					        {
					            _log.DebugFormat("ViewStep created");
					        }

					        break; // we've found and processed our template, no need to keep looking
					    }
					    catch (Exception ex)
					    {
					        _log.ErrorFormat("Problem during Transform: {0}", ex.ToString());
					        throw;
					    }
					}
				}

			}
		}
	}
}

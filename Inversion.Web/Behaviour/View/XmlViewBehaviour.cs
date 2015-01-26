using System.IO;
using System.Xml;

using Inversion.Process;
using Inversion.Process.Behaviour;

namespace Inversion.Web.Behaviour.View {

	/// <summary>
	/// Serialise the model of the last view step to XML.
	/// </summary>
	public class XmlViewBehaviour : ProcessBehaviour {

		private readonly string _contentType;

		/// <summary>
		/// Instantiates a new xml view behaviour to provide production of xml views.
		/// </summary>
		/// <remarks>Defaults the content-type to "text/xml"</remarks>
		/// <param name="respondsTo">The message the behaviour has set as responding to.</param>
		public XmlViewBehaviour(string respondsTo) : this(respondsTo, "text/xml") { }

		/// <summary>
		/// Instantiates a new xml view behaviour to provide production of xml views.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour has set as responding to.</param>
		/// <param name="contentType">The content type of the view step produced from this behaviour.</param>
		public XmlViewBehaviour(string respondsTo, string contentType)
			: base(respondsTo) {
			_contentType = contentType;
		}

		/// <summary>
		/// Writes the model of the last view-step as xml to the content of a new view-step.
		/// </summary>
		/// <param name="ev">The event that gave rise to this action.</param>
		/// <param name="context">The context within which this action is being performed.</param>
		public override void Action(IEvent ev, IProcessContext context) {
			if (context.ViewSteps.HasSteps) {
				if (context.ViewSteps.Last.HasModel) {
					TextWriter writer = new StringWriter();
					writer.WriteLine(@"<?xml version=""1.0"" ?>");
					// we take the last step and transform it into a new representation
					context.ViewSteps.Last.Model.ToXml(writer);
					// then add the new representation as a new step
					context.ViewSteps.CreateStep("xml", _contentType, writer.ToString());
					// the next step in the pipeline may in turn transform this
					// all previous steps remain accessible via context.ViewSteps
				} else {
					// try and parse the content of the last step as xml
					if (context.ViewSteps.Last.HasContent) {
						try {
							XmlDocument content = new XmlDocument();
							content.LoadXml(context.ViewSteps.Last.Content);
							context.ViewSteps.CreateStep("xml", _contentType, content.OuterXml);
						} catch (XmlException) {
							throw new WebException("Unable to process content that is not XML.");
						}
					} else {
						throw new WebException("Was expecting a model or content to output.");
					}
				}
			} else {
				throw new WebException("There is no initial view state to serialise into XML.");
			}
		}

	}
}

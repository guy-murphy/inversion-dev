using System.IO;
using Inversion.Process;

namespace Inversion.Web.Behaviour.View {

	/// <summary>
	/// Serialise the model of the last view step to XML.
	/// </summary>
	public class XmlViewBehaviour : WebBehaviour {

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
		public override void Action(IEvent ev, IWebContext context) {
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
					throw new WebException("Was expecting a model to serialise but found content.");
				}
			} else {
				throw new WebException("There is no initial view state to serialise into XML.");
			}
		}

	}
}

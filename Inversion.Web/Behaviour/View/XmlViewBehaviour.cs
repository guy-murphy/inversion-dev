using System.IO;
using Inversion.Process;

namespace Inversion.Web.Behaviour.View {

	/// <summary>
	/// Serialise the model of the last view step to XML.
	/// </summary>
	public class XmlViewBehaviour : WebBehaviour {

		private readonly string _contentType;

		public XmlViewBehaviour(string message) : this(message, "text/xml") { }

		public XmlViewBehaviour(string message, string contentType)
			: base(message) {
			_contentType = contentType;
		}

		public override void Action(IEvent ev, WebContext context) {
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

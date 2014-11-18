using Inversion.Process;

namespace Inversion.Web.Behaviour.View {
	/// <summary>
	/// Serialise the model of the last view step to XML.
	/// </summary>
	public class JsonViewBehaviour : WebBehaviour {

		private readonly string _contentType;

		public JsonViewBehaviour(string message) : this(message, "application/json") { }

		public JsonViewBehaviour(string message, string contentType)
			: base(message) {
			_contentType = contentType;
		}

		public override void Action(IEvent ev, WebContext context) {
			if (context.ViewSteps.HasSteps) {
				if (context.ViewSteps.Last.HasModel) {
					context.ViewSteps.CreateStep("json", _contentType, context.ViewSteps.Last.Model.ToJson());
				} else {
					throw new WebException("Was expecting a model to serialise but found content.");
				}
			} else {
				throw new WebException("There is no initial view state to serialise into XML.");
			}
		}

	}
}

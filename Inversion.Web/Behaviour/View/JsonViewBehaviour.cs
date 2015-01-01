using Inversion.Process;

namespace Inversion.Web.Behaviour.View {
	/// <summary>
	/// Serialise the model of the last view step to json.
	/// </summary>
	public class JsonViewBehaviour : WebBehaviour {

		private readonly string _contentType;

		/// <summary>
		/// Instantiates a new xml view behaviour to provide production of json views.
		/// </summary>
		/// <remarks>Defaults the content-type to "application/json"</remarks>
		/// <param name="respondsTo">The message the behaviour has set as responding to.</param>
		public JsonViewBehaviour(string respondsTo) : this(respondsTo, "application/json") { }

		/// <summary>
		/// Instantiates a new xml view behaviour to provide production of json views.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour has set as responding to.</param>
		/// <param name="contentType">The content type of the view step produced from this behaviour.</param>
		public JsonViewBehaviour(string respondsTo, string contentType)
			: base(respondsTo) {
			_contentType = contentType;
		}

		/// <summary>
		/// Writes the model of the last view-step as json to the content of a new view-step.
		/// </summary>
		/// <param name="ev">The event that gave rise to this action.</param>
		/// <param name="context">The context within which this action is being performed.</param>
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
